using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SeedAppTenant.DataContracts.Implementations;
using SeedAppTenant.DataContracts.Interfaces;
using SeedAppTenant.WebApi.Helpers;

namespace SeedAppTenant.WebApi.Controllers
{
	/// <summary>
	/// LOGIN AND AUTHENTICATE USER
	/// </summary>
	[RoutePrefix("api/login")]
	public class LoginController : SecureApiController
    {
		private const int ERROR_INVALID_LOGIN = 1;

		readonly Dictionary<int, string> errors = new Dictionary<int, string>
        {
			{ ERROR_INVALID_LOGIN, "Login is invalid" }
        };


		/// <summary>
		/// LOGIN AND AUTHENTICATE USER VIA A LOGIN FORM
		/// </summary>
		/// <param name="loginModel"></param>
		/// <returns></returns>
		/// <exception cref="HttpResponseException"></exception>
		[AllowAnonymous]
		[Route("authenticate")]
		public HttpResponseMessage Post(LoginModel loginModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					IUserModel currentUser = SecurityHelper.AuthenticateUser(loginModel, CurrentTenant);

					return Request.CreateResponse(HttpStatusCode.OK, currentUser.ToAuthenticatedUser(CurrentTenant));
				}
				catch (Exception ex)
				{
					throw ThrowIfError(ERROR_INVALID_LOGIN, HttpStatusCode.BadRequest, errors, ex.Message);
				}
			}

			throw ThrowIfError(ERROR_INVALID_LOGIN, HttpStatusCode.BadRequest, errors, ModelState);
		}



		/// <summary>
		/// PASS-THROUGH LOGIN AND AUTHENTICATE USER VIA TENANT
		/// </summary>
		/// <param name="tenantUserModel"></param>
		/// <returns></returns>
		/// <exception cref="HttpResponseException"></exception>
		[AllowAnonymous]
		[Route("authenticate/passthrough")]
		public HttpResponseMessage Post(TenantUserModel tenantUserModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					IUserModel currentUser = SecurityHelper.AuthenticateUser(tenantUserModel, CurrentTenant);

					return Request.CreateResponse(HttpStatusCode.OK, currentUser.ToAuthenticatedUser(CurrentTenant));
				}
				catch (Exception ex)
				{
					throw ThrowIfError(ERROR_INVALID_LOGIN, HttpStatusCode.BadRequest, errors, ex.Message);
				}
			}

			throw ThrowIfError(ERROR_INVALID_LOGIN, HttpStatusCode.BadRequest, errors, ModelState);
		}
    }
}
