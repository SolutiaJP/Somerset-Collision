using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace SeedApp.DataContracts
{
	public class DataContractsRegistry : Registry
	{
		public DataContractsRegistry()
		{
			Scan(x =>
			{
				x.TheCallingAssembly();
				x.Convention<DefaultConventionScanner>();
			});
		}
	}
}
