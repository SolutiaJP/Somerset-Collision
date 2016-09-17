using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace SeedApp.Repository
{
	public class RepositoryRegistry : Registry
	{
		public RepositoryRegistry()
		{
			Scan(x =>
			{
				x.TheCallingAssembly();
				x.Convention<DefaultConventionScanner>();
			});
		}
	}
}
