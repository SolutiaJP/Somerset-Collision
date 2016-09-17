using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SeedApp.BusinessLibrary.API;
using SeedApp.DataContracts.API;
using SeedApp.DataContracts.Interfaces;

namespace SeedApp.WebApi.Controllers
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
        private readonly PersonDetailBusinessLibrary personDetailBusinessLibrary;

		public PersonController
		(
			PersonListBusinessLibrary personListBusinessLibrary,
            PersonDetailBusinessLibrary personDetailBusinessLibrary
		)
		{
			this.personListBusinessLibrary = personListBusinessLibrary;
            this.personDetailBusinessLibrary = personDetailBusinessLibrary;
		}


		// GET api/values
		[Route("list")]
		[AllowAnonymous]
		[ResponseType(typeof(IList<IBaseDataContract>))]
		public HttpResponseMessage Get()
		{
			try
			{
				return Request.CreateResponse(HttpStatusCode.OK, personListBusinessLibrary.GetDataContract());
			}
			catch (Exception ex)
			{
				throw ThrowIfError(ERROR_INVALID_PERSON_LIST, HttpStatusCode.BadRequest, errors, ex.Message);
			}		
		}


        // GET api/values
        [Route("detail/{id:int}")]
        [AllowAnonymous]
        [ResponseType(typeof(IBaseDataContract))]
        public HttpResponseMessage GetPersonDetail(int id)
        {
            try
            {
                var parametersDataContract = new PersonDetailParametersDataContract();
                parametersDataContract.PersonId = id;
				return Request.CreateResponse(HttpStatusCode.OK, personDetailBusinessLibrary.GetDataContract(parametersDataContract));
            }
            catch (Exception ex)
            {
                throw ThrowIfError(ERROR_INVALID_PERSON_LIST, HttpStatusCode.BadRequest, errors, ex.Message);
            }
        }
	}
}
