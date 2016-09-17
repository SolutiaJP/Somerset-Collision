(function ()
{
	'use strict';

	angular.module("seedApp.services").factory('principalService',
    ['$rootScope', '$q', '$injector', '$window', 'siteConfig', 'accountModel', '$state',
    function ($rootScope, $q, $injector, $window, siteConfig, accountModel, $state)
    {
    	var writeUsertoLocalStorage = function ()
    	{
    		//WRITE CURRECT USER TO SESSION SCOPE SO IT'S AVAILABLE ON SUBSEQUENT REFRESHES UNTIL USER LOGS OUT OR SESSION EXPIRES (USER CLOSES BROWSER)
    		$window.localStorage[siteConfig.storageKeys.CurrentUser] = JSON.stringify(self.CurrentUser);
    		$window.localStorage[siteConfig.storageKeys.AuthenticationTimeStamp] = moment();
    	};

    	var deleteUserFromLocalStorageItem = function ()
    	{
    		if (self.CurrentUser)
    		{
    			self.CurrentUser.Reset();
    		}

    		self.CurrentUser = undefined;
    		self.AuthorizationToken = undefined;

    		$window.localStorage.removeItem(siteConfig.storageKeys.CurrentUser);
    		$window.localStorage.removeItem(siteConfig.storageKeys.AuthenticationTimeStamp);
    	};

    	var setCurrentUserFromLocalStorage = function (userData)
    	{
    		//console.log('setCurrentUserFromLocalStorage userData:', userData);

    		self.CurrentUser = new accountModel(userData);

    		if ((self.AuthorizationToken === undefined && userData.Token) || (userData.Token && userData.Token != self.AuthorizationToken))
    		{
    			self.AuthorizationToken = userData.Token;
    		}

    		writeUsertoLocalStorage();

    		//console.log('setCurrentUserFromLocalStorage self.CurrentUser:', self.CurrentUser);
    	}

    	var setCurrentUser = function (userData, token)
    	{
    		//MAKE TOKEN PART OF USER DATA SO TOKEN STORED 
    		userData.Token = token;

    		self.CurrentUser = new accountModel(userData);

    		//console.log('----------------- current user: ' + userData);
    		//console.log(self.CurrentUser);
    		//console.log(claims);

    		writeUsertoLocalStorage();
    	};

    	var init = function ()
    	{
    		//console.log('principalService init', self.CurrentUser);

    		if (angular.isUndefined(self.CurrentUser))
    		{
    			var currentUserData = '';

    			/*
				 * IF AUTHENTICATION TIMESTAMP AND CURRENT USER EXIST IN LOCAL STORAGE,
				 * THEN CHECK TO SEE IF TIME STAMP IS LESS THAN 30 DAYS OLD.  IF SO, THEN
				 * RESET AUTHENTICATION TIMESTAMP TO TODAY (ROLLING TIMESTAMP) AND PROCEED
				 * WITH USING CURRENT USER AS AUTHENTICATED USER
				 */
    			if ($window.localStorage[siteConfig.storageKeys.AuthenticationTimeStamp] && $window.localStorage[siteConfig.storageKeys.CurrentUser])
    			{
    				var authenticationTimeStamp = moment($window.localStorage[siteConfig.storageKeys.AuthenticationTimeStamp]);

    				//IF TIMESTAMP IS LESS THAN 30 DAYS
    				if (moment().diff(authenticationTimeStamp, 'days') < 30)
    				{
    					//console.log('----------------------------------authenticationService TIME SAMP DIFFERENCE');
    					//console.log(moment().diff(authenticationTimeStamp, 'days'));

    					//UPDATE AuthenticationTimeStamp WITH NEW TIME STAMP
    					$window.localStorage[siteConfig.storageKeys.AuthenticationTimeStamp] = moment();

    					currentUserData = JSON.parse($window.localStorage[siteConfig.storageKeys.CurrentUser]);
    					setCurrentUserFromLocalStorage(currentUserData);
    				}
    					//ELSE, REMOVE IT FROM LOCAL STORAGE
    				else
    				{
    					deleteUserFromLocalStorageItem();
    				}
    			}
    			else if ($window.localStorage[siteConfig.storageKeys.CurrentUser])
    			{
    				currentUserData = JSON.parse($window.localStorage[siteConfig.storageKeys.CurrentUser]);
    				setCurrentUserFromLocalStorage(currentUserData);
    			}
    		}
    	};


    	var self = this;
    	self.CurrentUser = undefined;
    	self.AuthorizationToken = undefined;

    	self.IsIdentityResolved = function ()
    	{
    		//return true;
    		return angular.isDefined(self.CurrentUser);
    	};

    	self.IsAuthenticated = function ()
    	{
    		return self.IsIdentityResolved();
    	};

    	self.HasClaim = function (claim)
    	{
    		if (!self.IsIdentityResolved() || !self.CurrentUser.Claims) return false;

    		for (var i = 0; i < self.CurrentUser.Claims.length; i++)
    		{
    			if (self.CurrentUser.Claims[i].name === claim)
    			{
    				return true;
    			}
    		}

    		return false;
    	};

    	self.GetClaimValue = function (claim)
    	{
    		var claimValue = null;

    		for (var i = 0; i < self.CurrentUser.Claims.length; i++)
    		{
    			if (self.CurrentUser.Claims[i].name === claim)
    			{
    				claimValue = self.CurrentUser.Claims[i].value;
    				break;
    			}
    		}

    		return claimValue;
    	};

    	/*
		 * ROUTES (STATES) HAVE A LIST OF CLAIMS THAT ARE NECESSARY 
		 * FOR THE AUTHENTICATED USED TO POSSESS AT LEAST ONE IN ORDER TO
		 * ACCESS ROUTE (STATE).
		 * 
		 * SO, LOOP OVER EACH CLAIM PASSED TO THIS FUNCTION.
		 * FOR EACH CLAIM IN LIST, CHECK TO SEE IF IT EXISTS
		 * IN CLAIMS ASSIGNED TO AUTHENTICATED USER
		 *
		 */
    	self.HasAnyClaim = function (claims)
    	{
    		if (!self.IsIdentityResolved() || !self.CurrentUser.Claims) return false;

    		for (var i = 0; i < claims.length; i++)
    		{
    			if (self.HasClaim(claims[i])) return true;
    		}

    		return false;
    	};

    	self.Identity = function (force)
    	{
    		if (force === true) self.CurrentUser = undefined;

    		var deferred = $q.defer();

    		init();

    		if (self.IsIdentityResolved())
    		{
    			deferred.resolve(self.CurrentUser);
    		}
    		else
    		{
    			deferred.reject();
    		}

    		return deferred.promise;
    	};

    	self.GetAuthorizationToken = function ()
    	{
    		if (self.AuthorizationToken)
    		{
    			return self.AuthorizationToken;
    		}

    		return undefined;
    	};

    	self.SetAuthorizationToken = function (jsonFromServer)
    	{
    		/*
	    	 * SET AuthorizationToken SO IT CAN BE USED IN SUBSEQUENT WEB API CALLS IN HEADER
	    	 */
    		self.AuthorizationToken = (jsonFromServer.Token) ? jsonFromServer.Token : undefined;

    		//RETRIEVE USER CLAIMS BASED OFF OF TOKEN RETURNED FROM SUCCESSFUL LOGIN
    		if (jsonFromServer.Token)
    		{
    			//SAVE CLAIMS FOR NEWLY AUTHENTICATED USER
    			setCurrentUser(jsonFromServer.CurrentUser, jsonFromServer.Token);

    			//IF WE HAVE A RETURN URL
    			if ($rootScope.returnState)
    			{
    				var returnState = $rootScope.returnState;
    				var returnParams = $rootScope.returnParams;

    				$rootScope.returnState = undefined;
    				$rootScope.returnParams = undefined;

    				// GO TO THAT URL
    				$state.go(returnState, returnParams);
    			}
    		}
    		else
    		{
    			var navigationService = $injector.get('navigationService');
    			navigationService.GoToErrorState();
    		}
    	};

    	self.Logout = function ()
    	{
    		var deferred = $q.defer();

    		deleteUserFromLocalStorageItem();

    		deferred.resolve();

    		//BROADCAST USER IS LOGGED OUT
    		$rootScope.$broadcast(siteConfig.events.UserLoggedOut);

    		return deferred.promise;
    	};

    	self.AuthenticatedUserClaimsList = function ()
    	{
    		return (self.IsIdentityResolved()) ? self.CurrentUser.Claims : null;
    	};

    	return self;
    }]);
}());