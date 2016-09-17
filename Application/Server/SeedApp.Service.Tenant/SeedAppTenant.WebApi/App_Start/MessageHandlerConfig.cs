using System.Web.Http;
using SeedAppTenant.WebApi.Handlers;
using StructureMap;

namespace SeedAppTenant.WebApi
{
	public class MessageHandlerConfig
	{
		public static void RegisterMessageHandler(HttpConfiguration config)
		{
			/* HANDLE CORS AND PRE-FLIGHT REQUESTS*/
			var corsHandler = ObjectFactory.GetInstance<CorsHandler>();
			config.MessageHandlers.Add(corsHandler);

			/* TENANT MESSAGE HANDLER */
			var tenantMessageHandler = ObjectFactory.GetInstance<TenantMessageHandler>();
			config.MessageHandlers.Add(tenantMessageHandler);


			/* AUTHORIZATION MESSAGE HANDLER */
			var authorizationHeaderHandler = ObjectFactory.GetInstance<XSeedAppTenantAuthMessageHandler>();
			config.MessageHandlers.Add(authorizationHeaderHandler);
		}
	}
}