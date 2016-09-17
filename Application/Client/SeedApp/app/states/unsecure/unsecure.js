(function ()
{
	'use strict';

	angular.module('app.unsecure',
	[
		'ngRoute',
		'ui.router'
	])
	.config(['$stateProvider',
	function ($stateProvider)
	{
		$stateProvider

			/*------------------------------------------------------------------------------------
			 * UNSECURE ROOT
			 ------------------------------------------------------------------------------------*/
			.state('app.unsecure',
			{
				abstract: true,
				url: '',
				data:
				{
				},

				/*
				 * NOTE: OPTIONAL ABILITY TO SEPARATE EXPERIENCE BETWEEN SECURE AND UNSECURE ROUTES
				 * right now, every secure and unsecure routes go through 'states/secure/app.html'
				 * you have the option to create a separate experience by poitint app.unsecure to 
				 * a different html page where navigation wrappers exist
				 */
				//templateUrl: 'states/unsecure/_root.html'
				templateUrl: 'states/app.html'
			})


			/*------------------------------------------------------------------------------------
			* UNSECURE HOME
			------------------------------------------------------------------------------------*/
			.state('app.unsecure.home',
			{
				url: '/',
				data:
				{
					title: 'Welcome to SeedApp',
					description: 'Welcome to SeedApp',
					claims: null,
					breadcrumbs:
					{
						sectionName: 'Welcome',
						pageName: 'SeedApp'
					}
				},
				views:
				{
					'':
					{
						templateUrl: 'states/unsecure/home.html',
						controller: 'unsecurePageController'
					}
				}
			})


			/*------------------------------------------------------------------------------------
			* ABOUT STATE
			------------------------------------------------------------------------------------*/
			.state('app.unsecure.about',
			{
				url: "/about",
				data:
				{
					title: 'About Page',
					description: 'About Page',
					claims: null,
					breadcrumbs:
					{
						sectionName: 'About',
						pageName: 'SeedApp'
					}
				},
				views:
				{
					'':
					{
						templateUrl: 'states/unsecure/about.html',
						controller: 'aboutController'
					}
				}
			})


			/*------------------------------------------------------------------------------------
			* LOGIN STATE
			------------------------------------------------------------------------------------*/
			.state('app.unsecure.login',
			{
				url: "/login",
				data:
				{
					title: 'Login Page',
					description: 'Login Page',
					claims: null,
					breadcrumbs:
					{
						sectionName: 'Login',
						pageName: 'Please Login'
					}
				},
				views:
				{
					'':
					{
						templateUrl: 'states/unsecure/login.html',
						controller: 'loginController'
					}
				}
			})


			/*------------------------------------------------------------------------------------
			* REGISTRATION STATE
			------------------------------------------------------------------------------------*/
			.state('app.unsecure.register',
			{
				url: "/register",
				data:
				{
					title: 'Registration Page',
					description: 'Registration Page',
					claims: null,
					breadcrumbs:
					{
						sectionName: 'Register',
						pageName: 'Please Register'
					}
				},
				views:
				{
					'':
					{
						templateUrl: 'states/unsecure/register.html',
						controller: 'registerController'
					}
				}
			})


			/*------------------------------------------------------------------------------------
			* ACCESS DENIED STATE
			------------------------------------------------------------------------------------*/
			.state('app.unsecure.denied',
			{
				url: '/access-denied',
				data:
				{
					title: 'Access Denied Page',
					description: 'Access Denied Page',
					claims: null,
					breadcrumbs:
					{
						sectionName: 'Access Denied',
						pageName: 'Ooops! Sorry, no accesss'
					}
				},
				views:
				{
					'':
					{
						templateUrl: 'states/unsecure/accessDenied.html',
						controller: 'unsecurePageController'
					}
				}
			})


			/*------------------------------------------------------------------------------------
			* ERROR STATE
			------------------------------------------------------------------------------------*/
			.state('app.unsecure.error',
			{
				url: '/error',
				data:
				{
					title: 'Error Page',
					description: 'Error Page',
					claims: null,
					breadcrumbs:
					{
						sectionName: 'Errors',
						pageName: 'Happen'
					}
				},
				views:
				{
					'':
					{
						templateUrl: 'states/unsecure/error.html',
						controller: 'errorController'
					}
				}
			})


			/*------------------------------------------------------------------------------------
			* 404 STATE
			------------------------------------------------------------------------------------*/
			.state('app.unsecure.page-not-found',
			{
				url: '/not-found',
				data:
				{
					title: 'Page Not Found Page',
					description: 'Page Not Found Page',
					claims: null,
					breadcrumbs:
					{
						sectionName: 'Page',
						pageName: 'Not Found'
					}
				},
				views:
				{
					'':
					{
						templateUrl: 'states/unsecure/page-not-found.html',
						controller: 'unsecurePageController'
					}
				}
			})


            /*------------------------------------------------------------------------------------
             * PATIENT DETAIL STATE - EXAMPLE OF SYNTAX FOR PASSING PARAMETER IN BROWSER URL
             
            .state('app.unsecure.patient-detail',
            {
                url: '/patient-detail/:id',
                data:
                {
                    title: 'My Patients',
                    description: 'a list of my patients',
                    claims: null,
                    breadcrumbs:
                    {
                        sectionName: 'Patient Summary',
                        pageName: 'Patient Summary'
                    }
                },
                views:
                {
                    '':
                    {
                        templateUrl: 'states/unsecure/patient-detail.html',
                        controller: 'patientDetailController'
                    }
                }
            })
			------------------------------------------------------------------------------------*/


		;//END $stateProvider
	}]);
}());