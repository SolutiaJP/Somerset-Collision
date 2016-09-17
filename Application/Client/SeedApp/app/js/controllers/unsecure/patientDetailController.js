(function ()
{
	'use strict';

	angular.module("seedApp.controllers").controller("patientDetailController",
    ['$rootScope', '$scope', '$stateParams', 'matchmedia', 'scrollService',
	function ($rootScope, $scope, $stateParams, matchmedia, scrollService)
	{
		scrollService.ScrollTo(0);


		//DATA


		//INIT
		$scope.Init = function ()
		{
			if ($stateParams.hasOwnProperty('id'))
			{
				console.log('id from URL parameters:', $stateParams.id);

				//EXAMPLE OF PASSING id TO SERVICE:
				/*
				patientService.GetPatientDetail($stateParams.id).then
				(
					function (jsonFromServer)
					{
						//DO SOMETHING WITH DATA IN jsonFromServer
					}
				);
				 */
			}
		};
		$scope.Init();


		//OPERATIONS


		//EVENTS
		matchmedia.onPhone(function (mediaQueryList)
		{
			//console.log('******************* SMALL *******************');
			//console.log(mediaQueryList);
			//console.log('-- onPhone reached:' + matchmedia.isPhone());
			//console.log('-- onTablet reached:' + matchmedia.isTablet());
			//console.log('-- onDesktop reached:' + matchmedia.isDesktop());
		});
		matchmedia.onTablet(function (mediaQueryList)
		{
			//console.log('******************* MEDIUM *******************');
			//console.log(mediaQueryList);
			//console.log('-- onPhone reached:' + matchmedia.isPhone());
			//console.log('-- onTablet reached:' + matchmedia.isTablet());
			//console.log('-- onDesktop reached:' + matchmedia.isDesktop());
		});
		matchmedia.onDesktop(function (mediaQueryList)
		{
			//console.log('******************* LARGE *******************');
			//console.log(mediaQueryList);
			//console.log('-- onPhone reached:' + matchmedia.isPhone());
			//console.log('-- onTablet reached:' + matchmedia.isTablet());
			//console.log('-- onDesktop reached:' + matchmedia.isDesktop());
		});
	}]);
}());