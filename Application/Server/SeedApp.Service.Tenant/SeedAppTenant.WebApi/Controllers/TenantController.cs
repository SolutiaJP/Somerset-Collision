using System.Net;
using System.Net.Http;
using System.Web.Http;
using SeedAppTenant.BusinessLibrary.API;
using SeedAppTenant.DataContracts.API;
using SeedAppTenant.DataContracts.Interfaces;

namespace SeedAppTenant.WebApi.Controllers
{
	[RoutePrefix("api/tenant")]
    public class TenantController : SecureApiController
    {
		private readonly SearchTenantBusinessLibrary searchTenantBusinessLibrary;

	    public TenantController
		(
			SearchTenantBusinessLibrary searchTenantBusinessLibrary
		)
	    {
			this.searchTenantBusinessLibrary = searchTenantBusinessLibrary;
	    }


	    /// <summary>
	    /// List of Tenants
	    /// </summary>
		/// <returns>List of Tenants (ITenant)</returns>
		[Route("list")]
		public HttpResponseMessage GetTenants()
		{
			var parametersDataContract = new SearchTenantParametersDataContract();

			return Request.CreateResponse(HttpStatusCode.OK, searchTenantBusinessLibrary.GetTenantList(parametersDataContract));
		}

	    /// <summary>
		/// Tenant Detail
		/// </summary>
		/// <returns>
		/// ITenant
		/// </returns>		
		[Route("{id:int}")]
		public ITenantDataContract GetTenant(int id)
		{
			var parametersDataContract = new SearchTenantParametersDataContract
			{
				TenantId = id
			};

		    var tenantList = searchTenantBusinessLibrary.GetTenantList(parametersDataContract);

			return tenantList[0];
		}
    }
}
