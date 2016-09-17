using System;
using SeedAppTenant.DataContracts.Interfaces;

namespace SeedAppTenant.DataContracts.Implementations
{
	public class TenantDataContract: ITenantDataContract
	{
		public Int32 Id { get; set; }
		public String Name { get; set; }
		public Int32 CompanyId { get; set; }
		public String CompanyName { get; set; }
		public Guid TenantKey { get; set; }
		public Guid PrivateKey { get; set; }
	}
}
