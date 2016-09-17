﻿using System.Web.Http;
using System.Web.Mvc;
using SeedAppTenant.BusinessLibrary;
using SeedAppTenant.Common;
using SeedAppTenant.DataContracts;
using SeedAppTenant.Repository;
using StructureMap;

namespace SeedAppTenant.WebApi.DependencyResolution
{
	public static class IoC
	{
		public static IContainer Initialize()
		{
			ObjectFactory.Initialize(x =>
			{
				//SCAN ASSEMBLIES
				x.Scan(scan =>
				{
					scan.TheCallingAssembly();
					scan.AddAllTypesOf<IController>();
					scan.AddAllTypesOf<ApiController>();
					scan.WithDefaultConventions();
				});

				//ADD MAPPINGS

				//ADD REGISTRIES
				x.AddRegistry<BusinessLibraryRegistry>();
				x.AddRegistry<CommonRegistry>();
				x.AddRegistry<DataContractsRegistry>();
				x.AddRegistry<RepositoryRegistry>();
			});

			//ObjectFactory.Configure(config => config.For<ITenant>().Use<Tenant>());
			//ObjectFactory.Configure(config => config.For<IAuthorizedResponse>().Use<AuthorizedResponse>());

			return ObjectFactory.Container;
		}
	}
}