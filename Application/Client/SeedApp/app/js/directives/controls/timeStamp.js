(function ()
{
	'use strict';

	angular.module("seedApp.directives").directive('timeStamp',
    [
	function ()
	{
		return {
			restrict: 'E',
			replace: true,
			scope:
			{
				timestamp: '@',
				label: '@'
			},
			templateUrl: "partials/controls/timeStamp.html",
			link: function (scope, elem, attrs)
			{
			}
		};
	}]);
}());