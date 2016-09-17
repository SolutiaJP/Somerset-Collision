(function ()
{
	'use strict';

	angular.module("seedApp.filters").filter('CountLabel',
	[
	function ()
	{
		return function (count, label)
		{
			label = (label === undefined) ? 'item' : label;

			label = (count === 1) ? label : label + "s";

			return " - " + count + " " + label;
		};
	}]);
}());