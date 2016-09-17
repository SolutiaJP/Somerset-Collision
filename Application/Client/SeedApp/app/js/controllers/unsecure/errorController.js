(function ()
{
	'use strict';

	angular.module("seedApp.controllers").controller("errorController",
    ['$rootScope', '$scope', 'matchmedia', 'scrollService', 'navigationService',
	function ($rootScope, $scope, matchmedia, scrollService, navigationService)
	{
		scrollService.ScrollTo(0);


		//DATA


		//INIT
		$scope.Init = function ()
		{
			//SET PAGE META DATA			
			$rootScope.SetBreadcrumbSectionClick
			(
				function ()
				{
					navigationService.GoToHomeState();
				}
			);
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