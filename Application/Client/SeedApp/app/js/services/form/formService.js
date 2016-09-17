(function ()
{
	'use strict';

	angular.module("seedApp.services").factory('formService',
    ['formModel',
    function (formModel)
    {
    	return function (formCriteriaModel)
    	{
    		var self = this;
    		self.onFormSubmit = undefined;

    		//OPERATIONS
    		self.DoSave = function ()
    		{
    			if (self.Form.IsValid() && (typeof self.onFormSubmit === 'function'))
    			{
    				var data = self.Form.GetFormModelData();
    				return self.onFormSubmit(data);
    			}
    		};

    		//INSTANTIATE FORM MODEL AND PASS FORM SUBMISSION HANDLER
    		self.Form = new formModel(formCriteriaModel, self.DoSave);

    		self.Reset = function ()
    		{
    			self.Form.Reset();
    		};

    		self.SetFormData = function (data)
    		{
    			self.Form.SetFormData(data);
    		};
    	};
    }]);
}());