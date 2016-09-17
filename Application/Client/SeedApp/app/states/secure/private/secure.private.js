(function ()
{
	'use strict';

	angular.module('app.secure.private',
	[
		'ngRoute',
		'ui.router'
	])
	.config(['$stateProvider',
	function ($stateProvider)
	{
		$stateProvider
			/*------------------------------------------------------------------------------------
			 * SECURE PRIVATE ROOT
			 ------------------------------------------------------------------------------------*/
			.state('app.secure.private',
			{
				'abstract': true,
				template: '<div ui-view=""></div>'
			})

			/*------------------------------------------------------------------------------------
			 * PRIVATE LIST STATE
			 ------------------------------------------------------------------------------------*/
			.state('app.secure.private.list',
			{
				url: '/private/list',
				data:
				{
					title: 'See My Privaye List',
					description: 'a private list of stuff',
					claims: null,
					breadcrumbs:
					{
						sectionName: 'Private',
						pageName: 'private list'
					}
				},
				views:
				{
					'':
					{
						templateUrl: 'states/secure/private/list.html',
						controller: 'privateListController'
					}
				}
			})


		;//CLOSE OUT $stateProvider
	}]);
}());