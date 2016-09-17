(function ()
{
	'use strict';

	angular.module("seedApp.services").factory('loginService',
    ['$rootScope', 'webApiService', 'principalService',
    function ($rootScope, webApiService, principalService)
    {
    	var self = this;


    	/*********************************************************************************
		 *
		 * SOME LOGIN APPROACHES POST TO A TOKEN END POINT
		 * 
		 *********************************************************************************/
	    //self.ProcessLoginForm = function(formData)
	    //{
	    //	return webApiService.Post('token', formData, { 'ContentType': 'application/x-www-form-urlencoded' }).then
		//	(
		//		function (jsonFromServer)
		//		{
		//			principalService.SetAuthorizationToken(jsonFromServer.data);

		//			return jsonFromServer;
		//		}
		//	);
	    //};


    	/*********************************************************************************
		 *
		 * POST TO LOGIN/AUTHENTICATE ENDPOINT
		 * 
		 *********************************************************************************/
    	self.ProcessLoginForm = function (formData)
    	{
    		return webApiService.Post('login/authenticate', formData).then
			(
				function (jsonFromServer)
				{
					principalService.SetAuthorizationToken(jsonFromServer.data);
				}
			);
    	};


	    return self;
    }]);
}());