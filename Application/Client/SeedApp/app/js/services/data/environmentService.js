(function ()
{
	'use strict';

	angular.module("seedApp.services").factory('environmentService',
    [
	function ()
	{
		var location = window.location.protocol + "//" + window.location.host;

		var webApi =
		{
			local: { url: 'http://local.webapi2.guessalist.com/api/' },
			dev: { url: 'http://local.webapi2.guessalist.com/api/' },
			qa: { url: 'http://local.webapi2.guessalist.com/api/' },
			production: { url: 'http://local.webapi2.guessalist.com/api/' }
		};

		var environments =
        [
            {
            	name: 'LOCAL',
            	pattern: /local/i,
            	webApiUrl: webApi.local.url,
            	isProduction: false
            },
            {
            	name: 'DEV',
            	pattern: /dev/i,
            	webApiUrl: webApi.dev.url,
            	isProduction: false
            },
            {
            	name: 'QA',
            	pattern: /qa/i,
            	webApiUrl: webApi.qa.url,
            	isProduction: false
            },
            {
            	name: 'PHONEGAP',
            	pattern: /file:/i,
            	webApiUrl: webApi.dev.url,
            	isProduction: false
            }
        ];

		var getEnvironment = function()
		{
			var environment = '';

			for (var i = 0; i < environments.length; i++)
			{
				environment = environments[i];

				if (environment.pattern.test(location))
				{
					return environment;
				}
			}

			return environment;
		};

		var self = this;
		self.Environment = getEnvironment();

		return self;
	}]);
}());