(function() {
    'use strict';

    angular
        .module('app.widgets')
        .directive('appWidgetClose', appWidgetClose);
    
    function appWidgetClose ($window) {
        // Usage:
        // <a data-app-widget-close></a>
        // Creates:
        // <a data-app-widget-close="" href="#" class="wclose">
        //     <i class="fa fa-remove"></i>
        // </a>
        var directive = {
            link: link,
            template: '<i class="fa fa-remove"></i>',
            restrict: 'A'
        };
        return directive;

        function link(scope, element, attrs) {
            attrs.$set('href', '#');
            attrs.$set('wclose');
            element.click(closeEl);

            function closeEl(e) {
                e.preventDefault();
                element.parent().parent().parent().hide(100);
            }
        }
    }

})();

