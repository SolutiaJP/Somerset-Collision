(function ()
{
	'use strict';

	angular.module("seedApp.services").factory('registerService',
    ['$rootScope', 'webApiService', 'principalService',
    function ($rootScope, webApiService, principalService)
    {
    	var self = this;

    	self.ProcessRegistrationForm = function (formData)
    	{
    		return webApiService.Post('register', formData).then
			(
				function (jsonFromServer)
				{
					console.log('register data:', jsonFromServer.data);
					principalService.SetAuthorizationToken(jsonFromServer.data);
				}
			);
    	};

    	return self;
    }]);
}());