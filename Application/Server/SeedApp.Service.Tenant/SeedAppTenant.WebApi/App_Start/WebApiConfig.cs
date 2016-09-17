using System.Web.Http;

namespace SeedAppTenant.WebApi
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API routes
			config.MapHttpAttributeRoutes();
		}
	}
}
