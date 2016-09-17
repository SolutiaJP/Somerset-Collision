using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using SeedAppTenant.DataContracts.Implementations;
using SeedAppTenant.DataContracts.Interfaces;
using Microsoft.Data.Extensions;

namespace SeedAppTenant.Repository
{
	public static class SecurityRepository
	{
		public static IUserModel CreateNewUser(IRegistrationModel dataModel, ITenantDataContract tenantDataContract)
		{
			IUserModel userModel;

			using (var connection = new SqlConnection(Database.ConnectionString))
			{
				connection.Open();

				using (var command = new SqlCommand("Security.CreateNewUser"))
				{
					command.Connection = connection;
					command.CommandType = CommandType.StoredProcedure;

					var sqlParams = new List<SqlParameter>
					{
						new SqlParameter("@TenantKey", tenantDataContract.TenantKey),
						new SqlParameter("@AuthenticationTypeId", dataModel.AuthenticationTypeId),
						new SqlParameter("@FirstName", dataModel.FirstName),
						new SqlParameter("@LastName", dataModel.LastName),
						new SqlParameter("@EmailAddress", dataModel.EmailAddress),
						new SqlParameter("@UserId", dataModel.UserId),
						new SqlParameter("@HashedPassword", dataModel.HashedPassword),
						new SqlParameter("@PasswordSalt", dataModel.PasswordSalt),
						new SqlParameter("@IsTemporaryPassword", dataModel.IsTemporaryPassword)
					};

					command.Parameters.AddRange(sqlParams.ToArray());

					using (DbDataReader reader = command.ExecuteReader())
					{
						// 1.) RESULT SET: TENANT META DATA
						userModel = reader.Materialize<UserModel>().FirstOrDefault() ?? new UserModel();
					}
				}
			}

			return userModel;
		}

		public static IUserModel RegisteredUserByUserId(ILoginModel loginModel, ITenantDataContract tenantDataContract)
		{
			IUserModel userModel;

			using (var connection = new SqlConnection(Database.ConnectionString))
			{
				connection.Open();

				using (var command = new SqlCommand("Security.RegisteredUserByUserId"))
				{
					command.Connection = connection;
					command.CommandType = CommandType.StoredProcedure;

					var sqlParams = new List<SqlParameter>
					{
						new SqlParameter("@TenantKey", tenantDataContract.TenantKey),
						new SqlParameter("@UserId", loginModel.UserName)
					};

					command.Parameters.AddRange(sqlParams.ToArray());

					using (DbDataReader reader = command.ExecuteReader())
					{
						// 1.) RESULT SET: TENANT META DATA
						userModel = reader.Materialize<UserModel>().FirstOrDefault() ?? new UserModel();
					}
				}
			}

			return userModel;
		}

		public static IUserModel RegisteredUserByGlobalId(String globalId, ITenantDataContract tenantDataContract)
		{
			IUserModel userModel;

			using (var connection = new SqlConnection(Database.ConnectionString))
			{
				connection.Open();

				using (var command = new SqlCommand("Security.RegisteredUserByGlobalId"))
				{
					command.Connection = connection;
					command.CommandType = CommandType.StoredProcedure;

					var sqlParams = new List<SqlParameter>
					{
						new SqlParameter("@TenantKey", tenantDataContract.TenantKey),
						new SqlParameter("@GlobalId", globalId)
					};

					command.Parameters.AddRange(sqlParams.ToArray());

					using (DbDataReader reader = command.ExecuteReader())
					{
						// 1.) RESULT SET: TENANT META DATA
						userModel = reader.Materialize<UserModel>().FirstOrDefault() ?? new UserModel();
					}
				}
			}

			return userModel;
		}

		public static IUserModel TenantRegisteredUser(ITenantUserModel tenantUserModel, ITenantDataContract tenantDataContract)
		{
			IUserModel userModel;

			using (var connection = new SqlConnection(Database.ConnectionString))
			{
				connection.Open();

				using (var command = new SqlCommand("Security.TenantRegisteredUser"))
				{
					command.Connection = connection;
					command.CommandType = CommandType.StoredProcedure;

					var sqlParams = new List<SqlParameter>
					{
						new SqlParameter("@TenantKey", tenantDataContract.TenantKey),
						new SqlParameter("@TenantUserId", tenantUserModel.Id),
						new SqlParameter("@TenantUserName", tenantUserModel.UserName)
					};

					command.Parameters.AddRange(sqlParams.ToArray());

					using (DbDataReader reader = command.ExecuteReader())
					{
						// 1.) RESULT SET: TENANT META DATA
						userModel = reader.Materialize<UserModel>().FirstOrDefault() ?? new UserModel();
					}
				}
			}

			return userModel;
		}		
	
		public static ITenantDataContract GetTenantByTenantKey(String tenantKey)
		{
			ITenantDataContract tenantDataContract;

			using (var connection = new SqlConnection(Database.ConnectionString))
			{
				connection.Open();

				using (var command = new SqlCommand("Security.TenantByTenantKey"))
				{
					command.Connection = connection;
					command.CommandType = CommandType.StoredProcedure;

					var sqlParams = new List<SqlParameter>
					{
						new SqlParameter("@TenantKey", tenantKey)
					};

					command.Parameters.AddRange(sqlParams.ToArray());

					using (DbDataReader reader = command.ExecuteReader())
					{
						// 1.) RESULT SET: TENANT META DATA
						tenantDataContract = reader.Materialize<TenantDataContract>().FirstOrDefault() ?? new TenantDataContract();
					}
				}
			}

			return tenantDataContract;
		}
	}
}
