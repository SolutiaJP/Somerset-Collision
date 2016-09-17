(function ()
{
	'use strict';

	angular.module("seedApp.filters").filter('ToMomentDate',
	[
	function ()
	{
		return function (date)
		{
			if (date == "" || date == null)
			{
				return "Information not Provided";
			}
			else
			{
				return moment(date).format("MMM Do, YYYY");
			}
		};
	}]);
}());