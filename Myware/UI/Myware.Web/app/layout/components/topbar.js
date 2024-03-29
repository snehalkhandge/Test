﻿(function () {
    'use strict';

    angular
        .module('app.layout')
        .controller('Topbar', Topbar);

    Topbar.$inject = ['$route', 'routehelper', 'authService', 'permissionService'];

    function Topbar($route, routehelper, authService, permissionService) {
        /*jshint validthis: true */
        var vm = this;
        var routes = routehelper.getRoutes();
        vm.authentication = authService.authentication;
        

        activate();

        function activate() { getNavRoutes(); }

        function getNavRoutes() {
            vm.navRoutes = routes.filter(function (r) {
                return r.settings && r.settings.nav;
            }).sort(function (r1, r2) {
                return r1.settings.nav - r2.settings.nav;
            });
        }
        /*
        function isCurrent(route) {
            if (!route.title || !$route.current || !$route.current.title) {
                return '';
            }
            var menuName = route.title;
            return $route.current.title.substr(0, menuName.length) === menuName ? 'current' : '';
        }*/
    }
})();