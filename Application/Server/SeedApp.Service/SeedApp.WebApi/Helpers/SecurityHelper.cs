using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using SeedApp.Common.Enums;
using SeedApp.Common.Exceptions;
using SeedApp.Common.Helpers;
using SeedApp.DataContracts.Helpers;
using SeedApp.DataContracts.Implementations;
using SeedApp.DataContracts.Interfaces;
using SeedApp.Repository;

namespace SeedApp.WebApi.Helpers
{
	/// <summary>
	/// Security Helper used for Authentication, Registration
	/// </summary>
	public static class SecurityHelper
	{
		/// <summary>
		/// REGISTER A NEW USER
		/// </summary>
		/// <param name="dataModel"></param>
		/// <returns></returns>
		public static IUserModel CreateNewUser(IRegistrationModel dataModel)
		{
			return SecurityRepository.CreateNewUser(dataModel);
		}

	
		/// <summary>
		/// LOGIN AND AUTHENTICATE USER VIA A LOGIN FORM
		/// </summary>
		/// <param name="loginModel"></param>
		/// <returns></returns>
		public static IUserModel AuthenticateUser(ILoginModel loginModel)
		{
			var userModel = SecurityRepository.RegisteredUserByUserId(loginModel);

			if (IsValid(loginModel.Password, userModel))
			{
				return userModel;
			}

			throw new BusinessException("Username and/or password is incorrect.  Please try again.");
		}


		/// <summary>
		/// GET REGISTERED USER VIA GLOBAL ID
		/// </summary>
		/// <param name="globalId"></param>
		/// <returns></returns>
		public static IUserModel RegisteredUserByGlobalId(String globalId)
		{
			return SecurityRepository.RegisteredUserByGlobalId(globalId);
		}


		/// <summary>
		/// CONVERTS USER MODEL TO AUTHENTICATED USER (EXTENDS IUserModel)
		/// </summary>
		/// <param name="currentUser"></param>
		/// <returns></returns>
		public static IAuthenticatedUser ToAuthenticatedUser(this IUserModel currentUser)
		{
			var token = AuthTokenHelper.GenerateAuthToken(currentUser);

			var autehnticatedUser = new AuthenticatedUser
			{
				CurrentUser = currentUser,
				Token = token
			};

			return autehnticatedUser;
		}


		/// <summary>
		/// CREATES CLAIMS PRINCIPAL (EXTENDS IUserModel)
		/// </summary>
		/// <param name="userModel"></param>
		/// <returns></returns>
		public static ClaimsPrincipal ToClaimsPrincipal(this IUserModel userModel)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Sid, userModel.AuthenticationId.ToString(CultureInfo.InvariantCulture)),
				new Claim(ClaimTypes.NameIdentifier, userModel.UserId),
				new Claim(SecurityClaimTypes.UserKey, userModel.GlobalId.ToString()),
				new Claim(SecurityClaimTypes.PersonId, userModel.PersonId.ToString(CultureInfo.InvariantCulture)),
				new Claim(ClaimTypes.GivenName, userModel.FirstName),
				new Claim(ClaimTypes.Surname, userModel.LastName),
				new Claim(ClaimTypes.Name, userModel.Name),
				new Claim(ClaimTypes.Email, userModel.EmailAddress??String.Empty),
				new Claim(SecurityClaimTypes.IsAnonymous, userModel.IsAnonymous.ToString()),
				new Claim(SecurityClaimTypes.IsTemporaryPassword, userModel.IsTemporaryPassword.ToString()),
				new Claim(SecurityClaimTypes.IsLockedOut, userModel.IsLockedOut.ToString()),
			};

			return new ClaimsPrincipal(new ClaimsIdentity(claims, "CustomAuthentication"));
		}



		#region PRIVATE
		private static Boolean IsValid(String incomingPassword, IUserModel userModel)
		{
			var saltedValue = (userModel.PasswordSalt.HasValue) ? userModel.PasswordSalt.Value : 0;
			var hashedPassword = PasswordHelper.ToSHA512Hash(incomingPassword.Trim(), saltedValue);
			return userModel.Password.IsSameAs(hashedPassword);
		}
		#endregion PRIVATE
	}
}