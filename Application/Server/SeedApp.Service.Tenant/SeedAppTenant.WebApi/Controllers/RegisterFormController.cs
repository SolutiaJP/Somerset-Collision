using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SeedAppTenant.Common.Enums;
using SeedAppTenant.Common.Helpers;
using SeedAppTenant.DataContracts.Implementations;
using SeedAppTenant.WebApi.Helpers;

namespace SeedAppTenant.WebApi.Controllers
{
	/// <summary>
	/// REGISTER A FORMS AUTHENTICATED USED (EMAIL ADDRESS, PASSWORD)
	/// </summary>
	public class RegisterFormController : SecureApiController
    {
		private const int ERROR_INVALID_REGISTRATION = 1;

		readonly Dictionary<int, string> errors = new Dictionary<int, string>
        {
			{ ERROR_INVALID_REGISTRATION, "Registration is invalid" }
        };


		/// <summary>
		/// REGISTER A FORMS AUTHENTICATED USED (EMAIL ADDRESS, PASSWORD)
		/// </summary>
		/// <param name="registrationModel"></param>
		/// <returns></returns>
		/// <exception cref="HttpResponseException"></exception>
		[AllowAnonymous]
		public HttpResponseMessage Post(RegistrationFormDataEntryDataContract registrationModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					Int64 passwordSalt;
					var hashedPassword = PasswordHelper.ToSHA512Hash(registrationModel.Password, out passwordSalt);

					var model = new RegistrationModel
					{
						AuthenticationTypeId = (int)AuthenticationType.Form,
						FirstName = registrationModel.FirstName,
						LastName = registrationModel.LastName,
						EmailAddress = registrationModel.EmailAddress,
						UserId = registrationModel.EmailAddress,
						HashedPassword = hashedPassword,
						PasswordSalt = passwordSalt,
						IsTemporaryPassword = false
					};

					var currentUser = SecurityHelper.CreateNewUser(model, CurrentTenant);

					return Request.CreateResponse(HttpStatusCode.OK, currentUser.ToAuthenticatedUser(CurrentTenant));
				}
				catch (Exception ex)
				{
					throw ThrowIfError(ERROR_INVALID_REGISTRATION, HttpStatusCode.BadRequest, errors, ex.Message);
				}
			}

			throw ThrowIfError(ERROR_INVALID_REGISTRATION, HttpStatusCode.BadRequest, errors, ModelState);
		}
    }
}
