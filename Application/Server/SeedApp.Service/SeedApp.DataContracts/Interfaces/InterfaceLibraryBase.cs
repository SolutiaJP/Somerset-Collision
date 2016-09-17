using System;

namespace SeedApp.DataContracts.Interfaces
{
	public interface IBaseDataContract
	{
		Int32 Id { get; set; }
		String Name { get; set; }
	}
}
