(function ()
{
	'use strict';

	angular.module("seedApp.directives").directive('breadcrumb',
    ['$rootScope', 'navigationService',
	function ($rootScope, navigationService)
	{
		return {
			restrict: 'E',
			replace: true,
			templateUrl: "partials/site/breadcrumb.html",
			link: function ($scope, iElement, attrs, controller)
			{
				$rootScope.$watch('PageMetaData', function (newValue, oldValue)
				{
					$scope.Breadcrumbs = $rootScope.PageMetaData.breadcrumbs;
				});

				$scope.GoToHomeState = function ()
				{
					navigationService.GoToHomeState();
				};
			}
		};
	}]);
}());