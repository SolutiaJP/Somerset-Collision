(function ()
{
	'use strict';

	angular.module("seedApp.directives").directive('autoFocus',
    [
	function ()
	{
		return {
			restrict: 'A',
			link: function (scope, $element, attrs)
			{
				$element[0].focus();
			}
		}
	}]);
}());