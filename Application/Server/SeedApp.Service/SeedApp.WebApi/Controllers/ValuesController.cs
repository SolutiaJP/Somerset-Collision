using System.Collections.Generic;
using System.Web.Http;

namespace SeedApp.WebApi.Controllers
{
	/// <summary>
	/// TEST CONTROLLER FOR UN-SECURE ROUTE
	/// </summary>
	[RoutePrefix("api/values")]
	public class ValuesController : ApiController
	{
		// GET api/values
		[Route("values")]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2", "value3", "value4", "value5", "value6" };
		}
	}
}
