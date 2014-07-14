(function() {
    'use strict';
    angular
        .module('app.widgets')
        .directive('appWidgetHeader', appWidgetHeader);

    appWidgetHeader.$inject = ['$window'];
    
    
    function appWidgetHeader() {
        //Usage:
        //<div data-app-widget-header title="vm.map.title"></div>
        var directive = {
            link: link,
            scope: {
                'title': '@',
                'subtitle': '@',
                'rightText': '@',
                'allowCollapse': '@'
            },
            templateUrl: 'app/layout/components/widgetheader.html',
            restrict: 'A'
        };
        return directive;

        function link(scope, element, attrs) {
            attrs.$set('class', 'widget-head');
        }
    }

})();

