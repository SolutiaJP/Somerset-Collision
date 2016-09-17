(function ()
{
	'use strict';

	angular.module("seedApp.services").factory('navigationService',
    ['$rootScope', '$location', '$timeout', 'siteConfig', '$state', 'principalService',
    function ($rootScope, $location, $timeout, siteConfig, $state, principalService)
    {
    	var history = [];
    	var self = this;


    	//LISTENERS
    	$rootScope.$on(siteConfig.events.UserLoggedOut, function ()
    	{
    		history = [];
    		self.GoToHomeState();
	    });

    	$rootScope.$on(siteConfig.events.DataAccessError, function ()
    	{
    		$timeout(function ()
    		{
    			self.GoToErrorState();
    		});
    	});


    	//GO HOME
    	self.GoToHomeState = function ()
    	{
    		var route = (principalService.IsAuthenticated()) ? 'app.secure.home' : 'app.unsecure.home';
    		$state.go(route);
    	};


    	//SECURE TASKS ROUTES



		//UNSECURE ROUTES
    	self.GoToLoginState = function ()
    	{
    		$state.go('app.unsecure.login');
    	};

    	self.GoToRegistrationState = function ()
    	{
    		$state.go('app.unsecure.register');
    	};

    	self.GoToAboutState = function ()
    	{
    		$state.go('app.unsecure.about');
    	};

    	self.GoToAccessDeniedState = function ()
    	{
    		$state.go('app.unsecure.denied');
    	};

    	self.GoToErrorState = function ()
    	{
    		$state.go('app.unsecure.error');
    	};


		/*------------------------------------------------------------------------------------

		EXAMPLE SYNTAX FOR NAVIGATING TO A ROUTE WITH PASSING A PARAMETER:

        self.GoToPatientDetailState = function (patientId)
        {
            $state.go('app.unsecure.patient-detail', { id: patientId });
        };
		------------------------------------------------------------------------------------*/


    	return self;
    }]);
}());