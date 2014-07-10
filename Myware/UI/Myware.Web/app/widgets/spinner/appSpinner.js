(function() {
    'use strict';

    angular
        .module('app.widgets')
        .directive('appSpinner', appSpinner);

    appSpinner.$inject = ['$window'];
    
    function appSpinner($window) {
        // Description:
        //  Creates a new Spinner and sets its options
        // Usage:
        //  <div data-app-spinner="vm.spinnerOptions"></div>
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element, attrs) {
            scope.spinner = null;
            scope.$watch(attrs.appSpinner, function (options) {
                if (scope.spinner) {
                    scope.spinner.stop();
                }
                scope.spinner = new $window.Spinner(options);
                scope.spinner.spin(element[0]);
            }, true);
        }
    }

})();

