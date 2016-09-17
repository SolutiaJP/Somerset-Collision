using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SeedAppTenant.Common.Enums;
using SeedAppTenant.DataContracts.Implementations;
using SeedAppTenant.WebApi.Helpers;

namespace SeedAppTenant.WebApi.Controllers
{
	/// <summary>
	/// REGISTER A FACEBOOK AUTHENTICATED USED
	/// </summary>
	public class RegisterFacebookController : SecureApiController
    {
		private const int ERROR_INVALID_REGISTRATION = 1;

		readonly Dictionary<int, string> errors = new Dictionary<int, string>
        {
			{ ERROR_INVALID_REGISTRATION, "Facebook registration is invalid" }
        };


		/// <summary>
		/// REGISTER A FACEBOOK AUTHENTICATED USED
		/// </summary>
		/// <param name="registrationModel"></param>
		/// <returns></returns>
		/// <exception cref="HttpResponseException"></exception>
		[AllowAnonymous]
		public HttpResponseMessage Post(RegistrationFacebookDataEntryDataContract registrationModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var model = new RegistrationModel
					{
						AuthenticationTypeId = (int)AuthenticationType.Facebook,
						FirstName = registrationModel.FirstName,
						LastName = registrationModel.LastName,
						EmailAddress = registrationModel.EmailAddress,
						UserId = registrationModel.FacebookId,
						HashedPassword = null,
						PasswordSalt = null,
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
