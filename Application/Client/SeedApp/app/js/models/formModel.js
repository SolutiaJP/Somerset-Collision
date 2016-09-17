(function ()
{
	'use strict';

	angular.module("seedApp.models").factory('formModel',
    [
    function ()
    {
    	return function (model, onFormSubmit)
    	{
    		var self = this;
    		self.Criteria = new model();
    		self.Submission = null, //this is a handle to the angularJS FormController object created via <form name="Form.Submission">...</form>

    		self.HasError = function (propertyName, errorType)
    		{
    			if (self.Submission)
    			{
    				var form = self.Submission;
    				var property = form[propertyName];

    				if (property)
    				{
    					//only validate if the form has been submitted or the browser is NOT IE and the property is dirty
    					//(necessary to work around IE bug related to placeholders and field validation in AngularJS)
    					return ((form.Attempted || property.$dirty) && property.$invalid) && (errorType == null ? true : property.$error[errorType]);
    				}
    			}
    		};

    		self.IsValid = function ()
    		{
    			self.Submission.Attempted = true;

    			return self.Submission.$valid;
    		};

    		self.GetFormModelData = function ()
    		{
    			return self.Criteria.toJSON();
    		};

    		self.SetFormData = function (data)
    		{
    			self.Criteria = new model(data);
    		};

    		self.SubmitForm = function ()
    		{
    			onFormSubmit();
    		};

    		self.Reset = function ()
    		{
    			self.Submission.Attempted = false;
    			self.Submission.$setUntouched();
    			self.Submission.$setPristine();
    			self.Criteria.Reset();
    		}
    	};
    }]);
}());