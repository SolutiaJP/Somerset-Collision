using System;
using System.Runtime.Serialization;
using SeedAppTenant.DataContracts.Interfaces;

namespace SeedAppTenant.DataContracts.Implementations
{
	[DataContract]
	public class UserModel : IUserModel
	{
		public String UserId { get; set; }
		public Int32 PersonId { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public String Name { get; set; }
		public String EmailAddress { get; set; }
		public Boolean IsAnonymous { get; set; }
		public Boolean IsTemporaryPassword { get; set; }
		public Boolean IsLockedOut { get; set; }
		public DateTime? LastLoginDate { get; set; }
		public DateTime AccountCreateDate { get; set; }
		public Int32 AuthenticationId { get; set; }
		public Guid GlobalId { get; set; }
		public Int32 AuthenticationTypeId { get; set; }
		public String AuthenticationTypeName { get; set; }
		public String Password { get; set; }
		public Int64? PasswordSalt { get; set; }
	}

	public class RegistrationModel : IRegistrationModel
	{
		public Int32 AuthenticationTypeId { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public String EmailAddress { get; set; }
		public String UserId { get; set; }
		public String HashedPassword { get; set; }
		public Int64? PasswordSalt { get; set; }
		public Boolean IsTemporaryPassword { get; set; }
	}

	public class AuthenticatedUser : IAuthenticatedUser
	{
		public IUserModel CurrentUser { get; set; }
		public String Token { get; set; }
	}

	public class LoginModel : ILoginModel
	{
		public String UserName { get; set; }
		public String Password { get; set; }
		public Boolean RememberMe { get; set; }
	}

	public class TenantUserModel : ITenantUserModel
	{
		public String Id { get; set; }
		public String UserName { get; set; }
	};

	#region DATA ENTRY
	[DataContract]
	public class RegistrationFormDataEntryDataContract : IRegistrationFormDataEntryDataContract
	{
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public String EmailAddress { get; set; }
		public String Password { get; set; }
		public String ConfirmPassword { get; set; }
	}

	[DataContract]
	public class RegistrationFacebookDataEntryDataContract : IRegistrationFacebookDataEntryDataContract
	{
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public String EmailAddress { get; set; }
		public String FacebookId { get; set; }
	}
	#endregion DATA ENTRY
}
