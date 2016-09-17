(function ()
{
	'use strict';

	angular.module("seedApp").constant("siteConfig",
	{
		app:
		{
			name: 'SEED APP',
			version: '1.1.0.0',
			pageCount: 50,
			keywords: ''
		},
		storageKeys:
		{
			CurrentUser: "CurrentUser",
			AuthenticationTimeStamp: "AuthenticationTimeStamp"
		},
		events:
		{
			AjaxRequest: "AjaxRequest",
			CloseSlideOut: "CloseSlideOut",
			DataAccessError: "DataAccessError",
			ErrorLogged: "ErrorLogged",
			UserAuthenticated: "UserAuthenticated",
			UserLoggedOut: "UserLoggedOut",
			UserUpdated: "UserUpdated"
		},
		errorMessages:
		{
			required: "@name@ is required",
			minlength: "min length of @value@ characters",
			pattern: "don\'t match the pattern",
			email: "email address not valid",
			number: "insert only numbers",
			custom: "custom not valid type \"@value@\"",
			async: "async not valid type \"@value@\""
		},
		options:
		{
			PlaceHolders:
			{
				EmailAddress: "someone@somewhere.com",
				FirstName: "First Name",
				LastName: "Last Name",
				Password: "Password",
				UserName: "User Name"
			}
		},
		global:
		{
			
		}
	});
}());