using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SeedAppTenant.BusinessLibrary.API;
using SeedAppTenant.DataContracts.Interfaces;

namespace SeedAppTenant.WebApi.Controllers
{
	/// <summary>
	/// TEST CONTROLLER FOR UN-SECURE ROUTE
	/// </summary>
	[RoutePrefix("api/person")]
	public class PersonController : SecureApiController
	{
		private const int ERROR_INVALID_PERSON_LIST = 1;

		readonly Dictionary<int, string> errors = new Dictionary<int, string>
        {
			{ ERROR_INVALID_PERSON_LIST, "Person list is invalid" }
        };


		private readonly PersonListBusinessLibrary personListBusinessLibrary;

		public PersonController
		(
			PersonListBusinessLibrary personListBusinessLibrary
		)
		{
			this.personListBusinessLibrary = personListBusinessLibrary;
		}


		// GET api/values
		[Route("list")]
		[AllowAnonymous]
		[ResponseType(typeof(IList<IBaseDataContract>))]
		public HttpResponseMessage Get()
		{
			try
			{
				return Request.CreateResponse(HttpStatusCode.OK, personListBusinessLibrary.GetList());
			}
			catch (Exception ex)
			{
				throw ThrowIfError(ERROR_INVALID_PERSON_LIST, HttpStatusCode.BadRequest, errors, ex.Message);
			}
		}
	}
}
