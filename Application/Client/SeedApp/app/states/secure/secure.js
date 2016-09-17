(function ()
{
	'use strict';

	angular.module('app.secure',
	[
		'ngRoute',
		'ui.router'
	])
	.config(['$stateProvider',
	function ($stateProvider)
	{
		$stateProvider

			/*------------------------------------------------------------------------------------
			 * SECURE ROOT
			 ------------------------------------------------------------------------------------*/
			.state('app.secure',
			{
				abstract: true,
				url: '',
				data:
				{
				},

				/*
				 * NOTE: OPTIONAL ABILITY TO SEPARATE EXPERIENCE BETWEEN SECURE AND UNSECURE ROUTES
				 * right now, every secure and unsecure routes go through 'states/secure/app.html'
				 * you have the option to create a separate experience by poitint app.secure to 
				 * a different html page where navigation wrappers exist
				 */
				//templateUrl: 'states/secure/_root.html'
				templateUrl: 'states/app.html'
			})


			/*------------------------------------------------------------------------------------
			* HOME PAGE
			------------------------------------------------------------------------------------*/
			.state('app.secure.home',
			{
				url: '/',
				data:
				{
					title: 'Welcome to Seed App',
					description: 'Welcome to Seed App',
					claims: null,
					breadcrumbs:
					{
						sectionName: 'Welcome',
						pageName: 'Seed App'
					}
				},
				views:
				{
					'':
					{
						templateUrl: 'states/secure/home.html',
						controller: 'homeController'
					}
				}
			})


		;//CLOSE OUT $stateProvider
	}]);
}());