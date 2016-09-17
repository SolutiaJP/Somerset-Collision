using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace SeedApp.DataContracts.Interfaces
{
	public interface IUserModel
	{
		[DataMember]
		String UserId { get; set; }

		[DataMember]
		Int32 PersonId { get; set; }

		[DataMember]
		String FirstName { get; set; }

		[DataMember]
		String LastName { get; set; }

		[DataMember]
		String Name { get; set; }

		[DataMember]
		String EmailAddress { get; set; }

		[DataMember]
		Boolean IsAnonymous { get; set; }

		[DataMember]
		Boolean IsTemporaryPassword { get; set; }

		[DataMember]
		Boolean IsLockedOut { get; set; }

		[DataMember]
		DateTime? LastLoginDate { get; set; }

		[DataMember]
		DateTime AccountCreateDate { get; set; }

		Int32 AuthenticationId { get; set; }
		Guid GlobalId { get; set; }
		Int32 AuthenticationTypeId { get; set; }
		String AuthenticationTypeName { get; set; }
		String Password { get; set; }
		Int64? PasswordSalt { get; set; }
	}

	public interface IRegistrationModel
	{
		Int32 AuthenticationTypeId { get; set; }
		String FirstName { get; set; }
		String LastName { get; set; }
		String EmailAddress { get; set; }
		String UserId { get; set; }
		String HashedPassword { get; set; }
		Int64? PasswordSalt { get; set; }
		Boolean IsTemporaryPassword { get; set; }
	}

	public interface IAuthenticatedUser
	{
		IUserModel CurrentUser { get; set; }
		String Token { get; set; }
	}

	public interface ILoginModel
	{
		[Required]
		[DataMember(IsRequired = true)]
		[DataType(DataType.EmailAddress)]
		String UserName { get; set; }

		[Required]
		[DataMember(IsRequired = true)]
		[DataType(DataType.Password)]
		String Password { get; set; }

		[DataMember]
		Boolean RememberMe { get; set; }
	}

    public interface IPersonDetailDataContract
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        int PersonId { get; set; }
        DateTime CreateDate { get; set; }
    }

	#region DATA ENTRY
	public interface IRegistrationFormDataEntryDataContract
	{
		[Required(ErrorMessage = @"first name is required")]
		[DataMember(IsRequired = true)]	
		String FirstName { get; set; }

		[Required(ErrorMessage = @"last name is required")]
		[DataMember(IsRequired = true)]		
		String LastName { get; set; }

		[Required(ErrorMessage = @"email address is required")]
		[DataMember(IsRequired = true)]
		[DataType(DataType.EmailAddress)]
		String EmailAddress { get; set; }

		[Required(ErrorMessage = @"password is required")]
		[DataMember(IsRequired = true)]
		[DataType(DataType.Password)]
		String Password { get; set; }

		[Required(ErrorMessage = @"password confirmation is required")]
		[DataMember(IsRequired = true)]
		[DataType(DataType.Password)]
		String ConfirmPassword { get; set; }
	}

	public interface IRegistrationFacebookDataEntryDataContract
	{
		[Required(ErrorMessage = @"first name is required")]
		[DataMember(IsRequired = true)]
		String FirstName { get; set; }

		[Required(ErrorMessage = @"last name is required")]
		[DataMember(IsRequired = true)]
		String LastName { get; set; }

		[Required(ErrorMessage = @"email address is required")]
		[DataMember(IsRequired = true)]
		[DataType(DataType.EmailAddress)]
		String EmailAddress { get; set; }

		[Required]
		[DataMember(IsRequired = true)]
		[Display(Name = @"Facebook Id")]
		String FacebookId { get; set; }
	}
	#endregion DATA ENTRY
}
