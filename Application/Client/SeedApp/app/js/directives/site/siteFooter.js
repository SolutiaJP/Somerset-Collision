(function ()
{
	'use strict';

	angular.module("seedApp.directives").directive('siteFooter',
    [
	function ()
	{
		return {
			restrict: 'E',
			replace: true,
			scope:
			{
				siteFooter: '='
			},
			templateUrl: "partials/site/siteFooter.html",
			link: function (scope, elem, attrs)
			{

			}
		};
	}]);
}());