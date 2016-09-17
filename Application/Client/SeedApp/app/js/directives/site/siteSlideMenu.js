(function ()
{
	'use strict';

	angular.module("seedApp.directives").directive('siteSlideMenu',
    ['$rootScope', 'navigationService', '$state', 'principalService',
	function ($rootScope, navigationService, $state, principalService)
	{
		return {
			restrict: 'E',
			replace: true,
			scope:
			{
			},
			templateUrl: function (elem, attrs)
			{
				return (principalService.IsAuthenticated()) ? "partials/site/siteSlideMenu.html" : "partials/site/siteSlideMenuUnAuthenticated.html";
			},
			link: function ($scope)
			{
				//DATA


				//NAVIGATION
				$scope.GoToHomeState = function ()
				{
					navigationService.GoToHomeState();
				};

				$scope.GoToAboutState = function ()
				{
					navigationService.GoToAboutState();
				};

				$scope.GoToLoginState = function ()
				{
					navigationService.GoToLoginState();
				};

				$scope.GoToRegistrationState = function ()
				{
					navigationService.GoToRegistrationState();
				};

				$scope.Logout = function ()
				{
					principalService.Logout();
					navigationService.GoToHomeState();
				};


				//IS ACTIVE CHECKS		
				$scope.IsHomeState = function ()
				{
					return $state.includes('app.secure.home');
				};

				$scope.IsAboutState = function ()
				{
					return $state.includes('app.unsecure.about');
				};

				$scope.IsLoginState = function ()
				{
					return $state.includes('app.unsecure.login');
				};

				$scope.IsRegistrationState = function ()
				{
					return $state.includes('app.unsecure.register');
				};
			}
		};
	}]);
}());