(function ()
{
	'use strict';

	angular.module("seedApp.directives").directive('floatingValidation',
    ['$animate', 'siteConfig',
	function ($animate, siteConfig)
	{
		return {
			scope: true,
			require: ['^form', 'ngModel'],
			restrict: "A",
			link: function (scope, element, attrs, controller)
			{
				scope.formCtrl = controller[0];
				scope.inputCtrl = controller[1];

				var $float = angular.element('<label for="' + attrs.id + '" class="float">' + attrs.placeholder + '</label>');

				scope.showHide = function (show)
				{
					if (show)
					{
						if (!$float.hasClass('top'))
						{
							element.after($float);
							$animate.addClass($float, 'top');
						}
					} else
					{
						$animate.removeClass($float, 'top');
					}
				};

				scope.showErrors = function ()
				{
					angular.forEach(scope.inputCtrl.$error, function (e, i)
					{
						if (e)
						{
							var message = siteConfig.errorMessages[i].replace("@name@", (attrs.alt) ? attrs.alt : attrs.name);
							$float.text(message.replace("@value@", attrs[i]));
						}
					});
					scope.showHide(true);
				};

				scope.$watch('inputCtrl.$error', function (newValue)
				{
					if (JSON.stringify(newValue) !== '{}' && !scope.inputCtrl.$pristine)
					{
						scope.showErrors();
					}
				}, true);

				scope.$watch('inputCtrl.$valid', function (newValue)
				{
					if (newValue && !scope.inputCtrl.$pristine)
					{
						$float.text(attrs.placeholder);
						scope.showHide(true);
					}
				});

				scope.$watch('inputCtrl.$pristine', function (newValue)
				{
					if (newValue && scope.inputCtrl.$touched)
					{
						scope.showHide(false);
						scope.inputCtrl.$setUntouched();
						//immediately reset no debounce
						scope.inputCtrl.$setViewValue(undefined, scope.inputCtrl.$options.updateOn);
						scope.inputCtrl.$setPristine();
					}
				});

				scope.$watch('formCtrl.$submitted', function (newValue)
				{
					if (newValue && scope.inputCtrl.$invalid)
					{
						scope.showErrors();
					}
					else if (!scope.inputCtrl.$dirty)
					{
						//reset if filled void
						scope.showHide(false);
					}
				});
			}
		};
	}]);
}());