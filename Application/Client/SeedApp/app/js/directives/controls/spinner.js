(function ()
{
	'use strict';

	angular.module("seedApp.directives").directive('spinner',
    ['$rootScope', 'siteConfig',
	function ($rootScope, siteConfig)
	{
		return {
			restrict: 'E',
			template: '<div class="spinner" ng-show="ShowSpinner"><i class="fa fa-spin fa-spinner fa-5x"></i></div>',
			link: function ($scope, $elem)
			{
				$scope.ShowSpinner = false;

				$rootScope.$on(siteConfig.events.AjaxRequest, function (event, inProgress)
				{
					$scope.ShowSpinner = inProgress;
				});
			}
		};
	}]);
}());