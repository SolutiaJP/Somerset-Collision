using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using SeedAppTenant.Common.Enums;
using SeedAppTenant.Common.Exceptions;
using SeedAppTenant.Common.Helpers;
using SeedAppTenant.DataContracts.Helpers;
using SeedAppTenant.DataContracts.Implementations;
using SeedAppTenant.DataContracts.Interfaces;
using SeedAppTenant.Repository;

namespace SeedAppTenant.WebApi.Helpers
{
	/// <summary>
	/// Security Helper used for Authentication, Registration
	/// </summary>
	public static class SecurityHelper
	{
		/// <summary>
		/// REGISTER A NEW PLAYER
		/// </summary>
		/// <param name="dataModel"></param>
		/// <param name="tenantDataContract"></param>
		/// <returns></returns>
		public static IUserModel CreateNewUser(IRegistrationModel dataModel, ITenantDataContract tenantDataContract)
		{
			return SecurityRepository.CreateNewUser(dataModel, tenantDataContract);
		}


		/// <summary>
		/// LOGIN AND AUTHENTICATE USER VIA A LOGIN FORM
		/// </summary>
		/// <param name="loginModel"></param>
		/// <param name="currentTenant"></param>
		/// <returns></returns>
		public static IUserModel AuthenticateUser(ILoginModel loginModel, ITenantDataContract currentTenant)
		{
			var userModel = SecurityRepository.RegisteredUserByUserId(loginModel, currentTenant);

			if (IsValid(loginModel.Password, userModel))
			{
				return userModel;
			}

			throw new BusinessException("Username and/or password is incorrect.  Please try again.");
		}


		/// <summary>
		/// PASS-THROUGH LOGIN AND AUTHENTICATE USER VIA TENANT
		/// </summary>
		/// <param name="tenantUserModel"></param>
		/// <param name="currentTenant"></param>
		/// <returns></returns>
		public static IUserModel AuthenticateUser(ITenantUserModel tenantUserModel, ITenantDataContract currentTenant)
		{
			return SecurityRepository.TenantRegisteredUser(tenantUserModel, currentTenant);
		}


		/// <summary>
		/// RETRIEVE TENANT BY TENANT KEY
		/// </summary>
		/// <param name="tenantKey"></param>
		/// <returns></returns>
		public static ITenantDataContract GetTenantByTenantKey(string tenantKey)
		{
			return SecurityRepository.GetTenantByTenantKey(tenantKey);
		}


		/// <summary>
		/// GET REGISTERED USER VIA GLOBAL ID
		/// </summary>
		/// <param name="globalId"></param>
		/// <param name="tenantDataContract"></param>
		/// <returns></returns>
		public static IUserModel RegisteredUserByGlobalId(String globalId, ITenantDataContract tenantDataContract)
		{
			return SecurityRepository.RegisteredUserByGlobalId(globalId, tenantDataContract);
		}


		/// <summary>
		/// CONVERTS USER MODEL TO AUTHENTICATED USER (EXTENDS IUserModel)
		/// </summary>
		/// <param name="currentUser"></param>
		/// <param name="tenantDataContract"></param>
		/// <returns></returns>
		public static IAuthenticatedUser ToAuthenticatedUser(this IUserModel currentUser, ITenantDataContract tenantDataContract)
		{
			var token = AuthTokenHelper.GenerateAuthToken(currentUser, tenantDataContract);

			var autehnticatedUser = new AuthenticatedUser
			{
				CurrentUser = currentUser,
				Token = token
			};

			return autehnticatedUser;
		}


		/// <summary>
		/// CREATE SCLAIMS PRINCIPAL
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