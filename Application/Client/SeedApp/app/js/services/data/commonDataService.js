(function ()
{
	'use strict';

	angular.module("seedApp.services").factory('commonDataService',
	['webApiService', 'siteConfig', '$q',
	function (webApiService, siteConfig, $q)
	{
		var self = this,
			defer = $q.defer();


		//OPERATIONS
		self.GetCommonData = function ()
		{
			//return webApiService.Get('common/data').then
			//(
			//	function (jsonFromServer)
			//	{
			//	}
			//);
		};


		return self;
	}]);
}());