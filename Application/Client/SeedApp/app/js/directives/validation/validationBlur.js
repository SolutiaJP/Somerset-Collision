(function ()
{
	'use strict';

	angular.module("seedApp.directives").directive('validationBlur',
    ['$filter', '$timeout',
	function ($filter, $timeout)
	{
		//todo: rewrite directive to use the post-link callback instead?
		return {
			restrict: 'A',
			require: 'ngModel',
			link: function ($scope, element, attributes, ngModel)
			{
				if (!ngModel)
				{
					return null;
				};

				//updates the model's $viewValue and paints it to the screen
				var updateView = function (value)
				{
					ngModel.$viewValue = value;
					ngModel.$render();
				}

				//main directive logic
				var applyDirective = function ()
				{
					//remove existing AngularJS event handlers to prevent conflict
					element.unbind('input').unbind('keydown').unbind('change');

					//updates the $viewValue and triggers execution of the $parsers/$formatters pipeline
					element.on('blur', function ()
					{
						$scope.$apply(function ()
						{
							ngModel.$setViewValue(element.val());
						});
					});

					element.on('focus', function ()
					{
						//replaces any formatted text with the underlying un-formatted value
						updateView(ngModel.$modelValue);
					});

					//intercepts keydown events
					element.on('keydown', function (e)
					{
						switch (e.which)
						{
							case 13:
								e.preventDefault();
								return false;

							case 17:
								return e.ctrlKey;  //chrome sends a CTRL+X/CTRL+V as the same keycode event

							case 8:     //DELETE
							case 9:     //TAB
							case 37:    //LEFT
							case 38:    //UP
							case 39:    //RIGHT
							case 40:    //DOWN
								return true;        //allow these key events to get thru
						}

						return undefined;
					});
				};

				//apply the directive after small timeout to bypass issue caused by Angular's default order / execution of applied directives
				$timeout(applyDirective, 1000);

				return null;
			}
		};
	}]);
}());