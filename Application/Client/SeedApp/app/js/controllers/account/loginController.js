(function ()
{
	'use strict';

	angular.module("seedApp.controllers").controller("loginController",
    ['$rootScope', '$scope', 'matchmedia', 'scrollService', 'navigationService', 'loginService',
	function ($rootScope, $scope, matchmedia, scrollService, navigationService, loginService)
	{
		scrollService.ScrollTo(0);


		//DATA


		//INIT
		$scope.Init = function ()
		{
		};
		$scope.Init();


		//OPERATIONS
		$scope.onFormSubmit = function (data)
		{
			loginService.ProcessLoginForm(data).then
			(
				function ()
				{
					navigationService.GoToHomeState();
				}
			);
		};

		$scope.onFormCancel = function (data)
		{
			navigationService.GoToHomeState();
		};


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