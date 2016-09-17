using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace SeedAppTenant.BusinessLibrary
{
	public class BusinessLibraryRegistry : Registry
	{
		public BusinessLibraryRegistry()
		{
			Scan(x =>
			{
				x.TheCallingAssembly();
				x.Convention<DefaultConventionScanner>();
			});
		}
	}
}
