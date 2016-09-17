(function ()
{
	'use strict';

	angular.module("seedApp.services").factory('scrollService',
    ['$timeout',
    function ($timeout)
    {
    	var self = this;

    	//DATA
    	self.scrollY = 0;

    	if (!angular.isFunction(window.scrollTo))
    	{
    		window.scrollTo = angular.noop;
    	}


    	//OPERATIONS
    	self.ScrollTo = function (scrollValue)
    	{
    		$timeout(function ()
    		{
    			window.scrollTo(0, scrollValue);
    		}, 0);
    	};

    	self.SetScrollValue = function ()
    	{
    		self.scrollY = window.pageYOffset;
    		//console.log('----- scroll value: ', self.scrollY);
    	}

    	self.ReturnToScrollPosition = function ()
    	{
    		self.ScrollTo(self.scrollY);
    	};

    	self.ResetScrollValue = function ()
    	{
    		self.scrollY = 0;
    	};

    	return self;
    }]);
}());