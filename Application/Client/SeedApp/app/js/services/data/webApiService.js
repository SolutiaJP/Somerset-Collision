(function ()
{
	'use strict';

	angular.module("seedApp.services").factory('webApiService',
    ['$q', '$http', '$rootScope', 'siteConfig', 'environmentService', 'FoundationApi', 'principalService',
    function ($q, $http, $rootScope, siteConfig, environmentService, foundationApi, principalService)
    {
    	var setContentTypeHeader = function (contentType)
    	{
    		$http.defaults.headers.common['Content-Type'] = contentType;
    	}

    	var configureRequest = function ()
	    {
    		var token = principalService.GetAuthorizationToken();

			//NOTE: OPTIONAL SETTING.  NOT ALL APPS NEED TO EMPLOY A TENANT KEY (USED TO IDENTIFY TENANT IN A MULTI-TENANT APPLICAITON)
    		//SET TENANT KEY IF APPLICABLE (DEPENDS ON IMPLEMENTAITON
    		//$http.defaults.headers.common['TenantKey'] = siteConfig.app.id;

			//SET AUTHENTICAITON TOKEN FOR EACH AND EVERY REQUEST
    		$http.defaults.headers.common['X-SeedApp-AUTH-TOKEN'] = (token) ? token : undefined;

    		//console.log('das token ya', token);
    		//console.log('headers.common ya', $http.defaults.headers.common['X-SeedApp-AUTH-TOKEN']);
	    };

    	var webApiUrl = environmentService.Environment.webApiUrl;
    	
    	var errorLogger = function (data, status, headers, config)
    	{
    		//errorLogService(new Error("STATUS " + status + ": GET TO '" + config.url + "' FAILED."), "", true);
    	}

    	var errorHandler = function (data, status, headers, config)
	    {
    		//console.log('errorHandler data:', data);
    		//console.log('errorHandler status:', status);
    		//console.log('errorHandler headers:', headers);
    		//console.log('errorHandler config:', config);

    		if (status === 401) //AUTHENTICATION HAS EXPIRED
    		{
    			principalService.Logout();
    		}
    		else if (data && data.ErrorTitle && data.ErrorDetail)
    		{
    			foundationApi.publish('NotificationContainer',
				{
					title: data.ErrorTitle,
					content: data.ErrorDetail,
					autoclose: "4000",
					color: "alert"
				});
    		}
    		else if (data && data.error && data.error_description)
    		{
    			foundationApi.publish('NotificationContainer',
				{
					title: data.error,
					content: data.error_description,
					autoclose: "4000",
					color: "alert"
				});
    		}
    		else if (data && data.Message)
    		{
    			foundationApi.publish('NotificationContainer',
				{
					title: "Errors happen",
					content: data.Message,
					autoclose: "4000",
					color: "alert"
				});
    		}
    		else
    		{
    			$rootScope.$emit(siteConfig.events.DataAccessError);
    		}
    	};

    	//TRIGGER SPINNER START
    	var broadcastEventStart = function ()
	    {
    		$rootScope.$broadcast(siteConfig.events.AjaxRequest, true);
    	};

    	//TRIGGER SPINNER STOP
    	var broadcastEventEnd = function ()
    	{
    		$rootScope.$broadcast(siteConfig.events.AjaxRequest, false);
    	};


    	var self = this;

    	//OPERATIONS
    	self.Get = function (url)
	    {
    		configureRequest();
    		broadcastEventStart();

    		var promise = $http.get(webApiUrl + url).error(errorHandler);

    		//have to attach the finally method seperately, as finally does not return the intial promise with success/error...
    		promise.finally(broadcastEventEnd);

    		return promise;
    	};

    	self.Post = function (url, data, options)
    	{
    		options = angular.extend({ showWaiting: true }, options);

    		if (options.ContentType)
    		{
    			setContentTypeHeader(options.ContentType);
    		}
    		else
    		{
    			setContentTypeHeader('application/json');
    		}

		    configureRequest();
    		
    		if (options.showWaiting)
    		{
    			broadcastEventStart();
    		}

    		var promise = $http.post(webApiUrl + url, data).error(errorHandler);

    		//have to attach the finally method seperately, as finally does not return the intial promise with success/error...
    		promise.finally(broadcastEventEnd);

    		return promise;
    	};

    	self.Put = function (url, data, options)
    	{
    		options = angular.extend({ showWaiting: true }, options);

    		if (options.ContentType)
    		{
    			setContentTypeHeader(options.ContentType);
    		}
    		else
    		{
    			setContentTypeHeader('application/json');
    		}

    		configureRequest();
    		
    		if (options.showWaiting)
    		{
    			broadcastEventStart();
    		}

    		var promise = $http.put(webApiUrl + url, data).error(errorHandler);

    		//have to attach the finally method seperately, as finally does not return the intial promise with success/error...
    		promise.finally(broadcastEventEnd);

    		return promise;
    	};

    	self.Delete = function (url)
    	{
    		configureRequest();
    		broadcastEventStart();

    		var promise = $http.delete(webApiUrl + url).error(errorHandler);

    		//have to attach the finally method seperately, as finally does not return the intial promise with success/error...
    		promise.finally(broadcastEventEnd);

    		return promise;
    	};

    	return self;
    }]);
}());