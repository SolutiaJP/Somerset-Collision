using System;
using SeedApp.DataContracts.Interfaces;

namespace SeedApp.DataContracts.Implementations
{
	public class BaseDataContract : IBaseDataContract
	{
		public Int32 Id { get; set; }
		public String Name { get; set; }
	}
}
