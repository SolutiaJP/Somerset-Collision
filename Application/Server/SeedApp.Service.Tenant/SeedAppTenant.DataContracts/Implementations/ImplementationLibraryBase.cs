using System;
using SeedAppTenant.DataContracts.Interfaces;

namespace SeedAppTenant.DataContracts.Implementations
{
	public class BaseDataContract : IBaseDataContract
	{
		public Int32 Id { get; set; }
		public String Name { get; set; }
	}
}
