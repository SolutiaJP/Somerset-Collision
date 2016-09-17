using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SeedAppTenant.DataContracts.Interfaces;
using SeedAppTenant.WebApi.Helpers;
using StructureMap;


namespace SeedAppTenant.WebApi.Handlers
{
	public class TenantMessageHandler : DelegatingHandler
	{
		protected override Task<HttpResponseMessage> SendAsync
		(
			HttpRequestMessage request,
			CancellationToken cancellationToken
		)
		{
			IEnumerable<string> tenantKeyValue;

			var hasTenantKey = request.Headers.TryGetValues("TenantKey", out tenantKeyValue);

			if (hasTenantKey)
			{
				var tenantKey = tenantKeyValue.First();
				var currentTenant = SecurityHelper.GetTenantByTenantKey(tenantKey);

				if (currentTenant == null || currentTenant.Id == 0)
				{
					return CreateUnauthorizedResponse("Tenant is unknown.");
				}

				ObjectFactory.Configure(x => x.For<ITenantDataContract>().Singleton().Use(currentTenant));
			}

			return base.SendAsync(request, cancellationToken);
		}

		private static Task<HttpResponseMessage> CreateUnauthorizedResponse(string responseMessage)
		{
			var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
			{
				Content = new StringContent(responseMessage),
				ReasonPhrase = "Exception"
			};

			var taskCompletionSource = new TaskCompletionSource<HttpResponseMessage>();
			taskCompletionSource.SetResult(response);
			return taskCompletionSource.Task;
		}
	}
}