(function ()
{
	'use strict';

	var seedApp = angular.module('seedApp',
    [
		//APLICATION CORE
		'ngAnimate',
		'ngRoute',
		'ngSanitize',
		'ngTouch',
		'ui.router',
		'adaptiveTemplating',
		'matchmedia-ng',
		'rt.debounce',
		'foundation',

		//3RD PARTY PLUGINS


		//APPLICATION STRUCTURE
		'app.unsecure',
		'app.secure',
		'app.secure.private',
		'seedApp.controllers',
		'seedApp.directives',
		'seedApp.filters',
		'seedApp.models',
		'seedApp.services'
    ]);

	angular.module('seedApp.controllers', []);
	angular.module('seedApp.directives', []);
	angular.module('seedApp.filters', []);
	angular.module('seedApp.models', []);
	angular.module('seedApp.services', []);


	//CONFIGURE ROUTES
	seedApp.config(
	['$stateProvider', '$urlRouterProvider', '$locationProvider', 'matchmediaProvider', 'adaptiveTemplatingProvider',
	function ($stateProvider, $urlRouterProvider, $locationProvider, matchmediaProvider, adaptiveTemplatingProvider)
	{
		//USE HTML5 MODE - ROUTES SYNTAX FOR NON-TOUCH DEVICES
		if (!Modernizr.touch)
		{
			$locationProvider.html5Mode(true).hashPrefix('!');
		}

		//ESTABLISH RULES TO BE SAME AS ZURB
		var smallRule = "(max-width: 39.9375rem)";								//max-width 639px
		var mediumRule = "(min-width: 40rem) and (max-width: 74.9375rem)";		//min-width 640px and max-width 1199px
		var largeRule = "(min-width: 75rem)";									//min-width 1200px

		//MAKE MATCH MEDIA USE SAME MEDIA QUERY RULES AS ZURB
		matchmediaProvider.rules.phone = smallRule;
		matchmediaProvider.rules.tablet = mediumRule;
		matchmediaProvider.rules.desktop = largeRule;

		//SET-UP MOBILE, TABLET, DESKTOP TESTS FOR ADAPTIVE TEMPLATES USED IN STATES
		adaptiveTemplatingProvider.addTest('mobile', Modernizr.mq(smallRule));
		adaptiveTemplatingProvider.addTest('tablet', Modernizr.mq(mediumRule));
		adaptiveTemplatingProvider.addTest('desktop', Modernizr.mq(largeRule));

		//console.log('Modernizer MQ is small', Modernizr.mq(smallRule));
		//console.log('Modernizer MQ is medium', Modernizr.mq(mediumRule));
		//console.log('Modernizer MQ is large', Modernizr.mq(largeRule));

		//ROOT STATE ALL OTHER STATES STEM FROM....
		$stateProvider.state('app',
		{
			'abstract': true,
			data:
			{
			},
			url: '',
			template: '<div ui-view=""></div>',
			resolve:
			{
				adaptiveTemplating: 'adaptiveTemplating',
				commonDataService: 'commonDataService',
				matchmedia: 'matchmedia',

				responsiveCheck: function ($rootScope, matchmedia)
				{
					$rootScope.Matchmedia = matchmedia;

					$rootScope.SetBreadcrumbPageName = function (pageName)
					{
						$rootScope.PageMetaData.breadcrumbs.pageName = pageName;
					};

					$rootScope.SetBreadcrumbSectionClick = function (onClick)
					{
						$rootScope.PageMetaData.breadcrumbs.onSectionClick = (typeof onClick === 'function') ? onClick : function ()
						{
						}
					};

					//console.log('config: is phone:', matchmedia.isPhone());
					//console.log('config: is tablet:', matchmedia.isTablet());
					//console.log('config: is desktop:', matchmedia.isDesktop());
					//console.log('config: is touch:', Modernizr.touch);
				},

				setup: function (commonDataService)
				{
					//console.log('config.setup');
					//RETRIEVE COMMON DATA FROM SERVICE
					commonDataService.GetCommonData();
				}
			}
		});

		// if none of the above states are matched, use this as the fallback
		$urlRouterProvider.otherwise('/not-found');
	}]);


	//TRIGGERS ROUTE CHANGES
	seedApp.run(['$rootScope', '$state', '$location', 'navigationService', 'siteConfig', 'principalService', 'FoundationApi',
    function ($rootScope, $state, $location, navigationService, siteConfig, principalService, foundationApi)
	{
		FastClick.attach(document.body);

		console.log = console.log || function () { };

		$rootScope.$on('$viewContentLoaded', function ()
		{
		});

		$rootScope.$on("$routeChangeSuccess", function ()
		{
		});

		$rootScope.$on("$routeChangeError", function (event, current, previous, eventObj)
		{
			//console.log('----------------------- $routeChangeError:');
			//console.log(eventObj.authenticated);

			if (eventObj.authenticated === false)
			{
				$location.path("/login");
			}
		});

		$rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams)
		{
			//console.log('$stateChangeSuccess toState:', toState);
		});

    	//BEFORE EACH STATE CHANGE, CHECK IF USER IS AUTHORIZED TO VIEW STATE
		$rootScope.$on('$stateChangeStart', function (event, toState, toParams, from, fromParams)
		{
			//console.log('$stateChangeStart toState:', toState.data);

			//SET PAGE META DATA FROM ROUTE DATA
			$rootScope.PageMetaData =
			{
				title: toState.data.title,
				description: toState.data.description,
				keywords: siteConfig.app.keywords,
				breadcrumbs:
				{
					sectionName: toState.data.breadcrumbs.sectionName,
					pageName: toState.data.breadcrumbs.pageName,
					onSectionClick: function ()
					{
					}
				}
			};

			//CHECK TO SEE IF USER IS AUTHENTICATED
			principalService.Identity().then
			(
				//IDENTITY IS KNOWN
				function()	
				{
					console.log('USER IS AUTHENTICATED');

					//USER IS AUTHENTICATED, BUT TRYING TO NAVIGATE TO LOGIN PAGE
					if (toState.name === 'app.unsecure.login')
					{
						//STOP PROCESSING.  REDIRECT USER TO SECURE HOME PAGE
						event.preventDefault();

						navigationService.GoToHomeState();
						return false;
					}

					/******************************************************************************************
					 * ALL 'SECURE' ROUTES SHOULD HAVE DATA 'CLAIMS' THAT ARE ALLOWED TO ACCESS ROUTE 
					 * (ALLOWED CLAIMS FOR A ROUTE ARE ENTERED IN DATA PORTION OF ROUTE DEFINITION)
					 * IF PRINCIPLAL IS NOT IN ANY ROLES (OR CLAIMS) ALLOWED FOR ROUTE, THEN ACCESS DENIED
					 ******************************************************************************************/
					if (toState.data.claims
						&& toState.data.claims.length > 0
						&& !principalService.HasAnyClaim(toState.data.claims))
					{
						//STOP PROCESSING.  REDIRECT USER TO ACCESS DENIED PAGE
						event.preventDefault();

						//USER IS SIGNED IN BUT NOT AUTHORIZED FOR DESIRED STATE
						console.log('WHAT DID YOU DO??  GO TO DAS DENIED STATE');
						console.log('route claims:', toState.data.claims);
						console.log('user claims:', principalService.AuthenticatedUserClaimsList());

						navigationService.GoToAccessDeniedState();
						return false;
					}
				},


				//IDENTITY IS NOT KNOWN
				function ()
				{
					console.log('USER IS *NOT* AUTHENTICATED');

					//USER IS NOT AUTHENTICATED BUT IS ATTEMPTING TO ACCESS A SECURE ROUTE
					if (toState.name.indexOf("app.secure") >= 0)
					{
						console.log('WHOA YO.  YOU CANT BE HERE');

						foundationApi.publish('NotificationContainer',
						{
							title: 'Not Authenticated',
							content: 'Please login',
							autoclose: "2000",
							color: "alert"
						});

						// ABORT! STOP PROCESSING.  USER NOT KNOWN AND TRYING TO ACCESS A SECURE PAGE
						event.preventDefault();

						$rootScope.returnState = toState.name;
						$rootScope.returnParams = toParams;

						// redirect to the login page and pass the state parameter
						navigationService.GoToLoginState();
						return false;
					}
				}
			);
		});
    }]);
}());