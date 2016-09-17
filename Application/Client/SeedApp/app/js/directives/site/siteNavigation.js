(function ()
{
	'use strict';

	angular.module("seedApp.directives").directive('siteNavigation',
    ['$rootScope', 'navigationService', 'siteConfig', '$state', 'principalService',
	function ($rootScope, navigationService, siteConfig, $state, principalService)
	{
		var toggleAddContainer = function ()
		{
			$('section#QuickAddContainer').slideToggle('slow');
		};

		return {
			restrict: 'E',
			transclude: true,
			replace: true,
			scope:
			{
			},
			templateUrl: function (elem, attrs)
			{
				return (principalService.IsAuthenticated()) ? "partials/site/siteNavigation.html" : "partials/site/siteNavigationUnAuthenticated.html";
			},
			link: function ($scope, iElement, iAttrs, controller)
			{
				//DATA
				$scope.Title = siteConfig.app.name;
				$scope.ShowBackButton = (typeof $rootScope.BackButtonClick === 'function');

				//NAVIGATION
				$scope.GoToHomeState = function ()
				{
					navigationService.GoToHomeState();
				};

				$scope.GoBack = function ()
				{
					if (typeof $rootScope.BackButtonClick === 'function')
					{
						$rootScope.BackButtonClick();
					}
				};

				$scope.GoToAboutState = function ()
				{
					navigationService.GoToAboutState();
				};

				$scope.GoToLoginState = function()
				{
					navigationService.GoToLoginState();
				};

				$scope.GoToRegistrationState = function ()
				{
					navigationService.GoToRegistrationState();
				};

				$scope.Logout = function()
				{
					principalService.Logout();
					navigationService.GoToHomeState();
				};

				$scope.ShowAddContainer = function ()
				{
					toggleAddContainer();
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



				//LISTENERS
				$rootScope.$watch('BackButtonClick', function (newValue)
				{
					$scope.ShowBackButton = (typeof newValue === 'function');
				}, true);
			}
		};
	}]);
}());