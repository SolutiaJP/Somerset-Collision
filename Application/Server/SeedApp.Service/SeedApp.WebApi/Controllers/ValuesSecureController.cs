using System.Collections.Generic;
using System.Web.Http;

namespace SeedApp.WebApi.Controllers
{
	/// <summary>
	/// TEST CONTROLLER FOR SECURE ROUTE
	/// </summary>
	[RoutePrefix("api/values")]
	public class ValuesSecureController : SecureApiController
	{
		// GET api/values
		[Route("securevalues")]
		public IEnumerable<string> Get()
		{
			return new string[] { "secure value1", "secure value2", "secure value3", "secure value4", "secure value5", "secure value6" };
		}
	}
}
