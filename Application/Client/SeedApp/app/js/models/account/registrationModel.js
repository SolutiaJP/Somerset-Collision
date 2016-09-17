(function ()
{
	'use strict';

	angular.module("seedApp.models").factory('registrationModel',
	[
	function ()
	{
		return function (data)
		{
			var self = this;
			self.FirstName = undefined;
			self.LastName = undefined;
			self.EmailAddress = undefined;
			self.Password = undefined;


			//EXTEND MODEL WITH DATA PASSED IN
			angular.extend(self, data);


			//OPERATIONS
			self.toJSON = function ()
			{
				//this is needed to work around bug in IE when serializing inherited members to JSON
				var tmp = {};
				for (var key in this)
				{
					if (typeof this[key] !== 'function')
					{
						tmp[key] = this[key];
					}
				}

				//console.log(tmp);
				return tmp;
			};

			self.Reset = function ()
			{
				for (var key in this)
				{
					if (typeof this[key] !== 'function')
					{
						this[key] = undefined;
					}
				}
			};
		};
	}]);
}());