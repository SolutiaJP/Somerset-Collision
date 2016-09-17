using System;
using System.Data.SqlClient;
using System.Reflection;
using CodeGenerator.Helpers;
using CodeGenerator.Implementations;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeedAppTenant.WebApi.Tests
{
	[TestClass]
	public class CodeGenerationTests
	{
		private readonly String codeGenerationSchemaName;
		private readonly String databaseName;
		private readonly SqlConnection sqlConnection;
		private readonly Server server;
		private readonly Database database;
		private readonly String projectRootPath;
		private readonly String projectName;
		private readonly String connectionString;
		private readonly String nameSpace;

		public CodeGenerationTests()
		{
			codeGenerationSchemaName = @"API";
			databaseName = @"SeedAppTenant";
			projectRootPath = @"C:\\ProjectStoreGit\\Solutia-SeedAppTenant\\Application\\Server\\SeedAppTenant.Service\\SeedAppTenant.";
			projectName = @"SeedAppTenant";
			connectionString = @"Data Source=localhost;Initial Catalog=SeedAppTenant;Integrated Security=True";
			nameSpace = @"SeedAppTenant";


			//codeGenerationSchemaName = @"API";
			//databaseName = @"BASICS1.0";
			//projectRootPath = @"C:\\ProjectStoreGit\\Solutia-CandidateTracking\\Application\\Server\\CandidateTracking.";
			//projectName = @"CandidateTracking";
			//connectionString = @"Data Source=23.253.119.242;Initial Catalog=BASICS1.0;Persist Security Info=True;User ID=CandidateTrackingUser; Password=CandidateTrackingUser";
			//nameSpace = @"CandidateTracking";			 



			sqlConnection = new SqlConnection(connectionString);
			sqlConnection.Open();

			server = new Server(new ServerConnection(sqlConnection));
			database = new Database(server, databaseName);
		}

		[TestMethod]
		public void TestGetStoredProcedureList()
		{
			//ASSEMBLE
			database.Refresh();

			//ACT
			var storedProcList = CodeGeneratorHelper.GetStoredProcedureList(database, codeGenerationSchemaName);

			//ASSERT
			Assert.IsNotNull(storedProcList);
		}

		[TestMethod]
		public void TestGetStoredProcCodeGenerationMetaData()
		{
			//ASSEMBLE
			database.Refresh();
			var storedProcList = CodeGeneratorHelper.GetStoredProcedureList(database, codeGenerationSchemaName);
			var schemaName = storedProcList[0].SchemaName;
			var storeProcName = storedProcList[0].StoredProcName;


			//ACT
			var codeGenerationTargetMetaData = new CodeGenerationTargetMetaData
			{
				ProjectRootPath = projectRootPath,
				ProjectName = projectName,
				ConnectionString = connectionString,
				DatabaseName = databaseName,
				SchemaName = schemaName,
				StoredProcName = storeProcName,
				NameSpace = nameSpace
			};

			var storedProcResultSetMetaData = CodeGeneratorHelper.ProcessStoredProcedure(codeGenerationTargetMetaData);

			var methodName = storedProcResultSetMetaData.MethodName;

			//ASSERT
			Assert.IsTrue(storedProcResultSetMetaData.IsStoredProcValid);
			Assert.IsNotNull(codeGenerationTargetMetaData);
			Assert.IsNotNull(storedProcResultSetMetaData);
		}

		[TestMethod]
		public void TestGetStoredProcCodeGenerationMetaDataByStoredProcName()
		{
			//ASSEMBLE
			database.Refresh();
			const string schemaName = "API";
			const string storeProcName = "GameByGameId";
			const string assemblyPath = @"C:\ProjectStoreGit\Solutia-SeedAppTenant\Application\Server\SeedAppTenant.Service\SeedAppTenant.DataContracts\bin\Debug\SeedAppTenant.DataContracts.dll";
			//var assemblyPath = @"C:\ProjectStoreGit\Solutia-CandidateTracking\Application\Server\CandidateTracking.DataContracts\bin\Debug\CandidateTracking.DataContracts.dll";
			byte[] data;

			using (var fs = System.IO.File.OpenRead(assemblyPath))
			{
				data = new byte[fs.Length];
				fs.Read(data, 0, Convert.ToInt32(fs.Length));
			}

			if (data == null || data.Length == 0)
			{
				throw new ApplicationException("Failed to load " + assemblyPath);
			}

			var interfaceAssembly = System.Reflection.Assembly.Load(data);	

			//ACT
			var codeGenerationTargetMetaData = new CodeGenerationTargetMetaData
			{
				InterfaceAssembly = interfaceAssembly,
				ProjectRootPath = projectRootPath,
				ProjectName = projectName,
				ConnectionString = connectionString,
				DatabaseName = databaseName,
				SchemaName = schemaName,
				StoredProcName = storeProcName,
				NameSpace = nameSpace
			};

			var storedProcResultSetMetaData = CodeGeneratorHelper.ProcessStoredProcedure(codeGenerationTargetMetaData);

			var methodName = storedProcResultSetMetaData.MethodName;

			//ASSERT
			Assert.IsTrue(storedProcResultSetMetaData.IsStoredProcValid);
			Assert.IsNotNull(codeGenerationTargetMetaData);
			Assert.IsNotNull(storedProcResultSetMetaData);
		}
	}
}
