using System;

namespace SeedAppTenant.DataContracts.Interfaces
{
	public interface ITenantDataContract
	{
		Int32 Id { get; set; }
		String Name { get; set; }
		Int32 CompanyId { get; set; }
		String CompanyName { get; set; }
		Guid TenantKey { get; set; }
		Guid PrivateKey { get; set; }
	}
}
