(function ()
{
	'use strict';

	angular.module("seedApp.filters").filter('ToMomentSince',
	[
	function ()
	{
		return function (date)
		{
			if (date != null)
			{
				return moment(date).fromNow();
			}
			else
			{
				return "None";
			}
		};
	}]);
}());