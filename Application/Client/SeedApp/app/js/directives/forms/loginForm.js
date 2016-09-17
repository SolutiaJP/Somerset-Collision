(function ()
{
	'use strict';

	angular.module("seedApp.directives").directive('loginForm',
    ['formService', 'loginModel',
	function (formService, formModel)
	{
		return {
			restrict: 'E',
			replace: true,
			scope:
			{
				onFormCancel: '=',
				onFormSubmit: '='
			},
			require: ['^form'],
			templateUrl: "partials/forms/loginForm.html",
			link: function (scope, element, attrs, form)
			{
				//SET ANGULAR FORM CONTROLLER		
				var dataEntryFormService = new formService(formModel);
				dataEntryFormService.Form.Submission = form[0];


				//SET FORM SUBMIT FUNCTION FROM PARENT CONTROLLER
				if (typeof scope.onFormSubmit === 'function')
				{
					dataEntryFormService.onFormSubmit = scope.onFormSubmit;
				}


				//DATA
				scope.Form = dataEntryFormService.Form;


				//OPERATIONS
				scope.DoCancelClick = function ()
				{
					scope.Form.Reset();

					if (typeof scope.onFormCancel === 'function')
					{
						scope.onFormCancel();
					}
				};
			}
		};
	}]);
}());