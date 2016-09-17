(function ()
{
	'use strict';

	angular.module("seedApp.controllers").controller("registerController",
    ['$rootScope', '$scope', 'matchmedia', 'scrollService', 'navigationService', 'registerService', 'FoundationApi',
	function ($rootScope, $scope, matchmedia, scrollService, navigationService, registerService, foundationApi)
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
			registerService.ProcessRegistrationForm(data).then
			(
				function ()
				{
					foundationApi.publish('NotificationContainer',
					{
						title: 'Success',
						content: 'Sucessfully registered!',
						autoclose: "2000",
						color: "success"
					});

					//how do I make this state change happen on close of notification?
					navigationService.GoToHomeState();
				}
			);
		};

		$scope.onFormCancel = function ()
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