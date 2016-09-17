﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using SeedAppTenant.DataContracts.Interfaces;
using SeedAppTenant.WebApi.Attributes;
using StructureMap;

namespace SeedAppTenant.WebApi.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[Authorize]
	[ApiExceptionHandling]
	public class SecureApiController : ApiController
	{
		/// <summary>
		/// ITenant record from TenantKey in Request Header
		/// </summary>
		public ITenantDataContract CurrentTenant;

		/// <summary>
		/// IUserModel established after registering or authentication
		/// </summary>
		public IUserModel CurrentUser;

		/// <summary>
		/// BASE CONTROLLER
		/// </summary>
		public SecureApiController()
		{
			CurrentTenant = ObjectFactory.GetInstance<ITenantDataContract>();
			CurrentUser = ObjectFactory.GetInstance<IUserModel>();
		}

		protected HttpResponseException ForbidWithMessage(string validationError, string errorDetail)
		{
			return ThrowIfError(-1, HttpStatusCode.Forbidden, new Dictionary<int, string> { { -1, validationError } }, errorDetail);
		}

		protected HttpResponseException ThrowIfError(int? error, HttpStatusCode statusCode, Dictionary<int, string> errors)
		{
			return ThrowIfError(Request, error, statusCode, errors, String.Empty);
		}

		protected HttpResponseException ThrowIfError(int? error, HttpStatusCode statusCode, Dictionary<int, string> errors, ModelStateDictionary modelState)
		{
			var errorDetail = string.Join(",", ModelState.Keys.ToList());
			var message = String.Format("Errors in: {0}", errorDetail);

			return ThrowIfError(Request, error, statusCode, errors, message);
		}

		protected HttpResponseException ThrowIfError(int? error, HttpStatusCode statusCode, Dictionary<int, string> errors, string errorDetail)
		{
			return ThrowIfError(Request, error, statusCode, errors, errorDetail);
		}

		protected HttpResponseException ThrowIfError(HttpRequestMessage request, int? error, HttpStatusCode statusCode, Dictionary<int, string> errors, string errorDetail)
		{
			return ThrowIfError(request, error, statusCode, errors[error.Value], errorDetail);
		}

		public static HttpResponseException ThrowIfError(HttpRequestMessage request, int? errorCode, HttpStatusCode statusCode, string error, string errorDetail)
		{
			return new HttpResponseException(request.CreateErrorResponse(statusCode, new HttpError
				{
					{ "ErrorCode", errorCode.GetValueOrDefault() }, 
					{ "ErrorTitle", error },
					{ "ErrorDetail", errorDetail }
				}));
		}
	}
}