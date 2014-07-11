(function () {
    'use strict';

    angular
        .module('app.account')
        .run(['routehelper', function (routehelper) {
            routehelper.configureRoutes(getRoutes());
        }]);

    function getRoutes() {
        return [
            {
                url: '/account/login',
                config: {
                    templateUrl: 'app/account/components/login.html',
                    title: 'Login',
                    settings: {
                        nav: 2,
                        content: '<i class="fa fa-lock"></i> Login'
                    }
                }
            },
            {
                url: '/account/logout',
                config: {
                    title: 'Logout',
                    templateUrl: 'app/account/components/logout.html',
                    settings: {
                        nav: 2,
                        content: '<i class="fa fa-unlock"></i> Logout'
                    }
                }
            }
        ];
    }
})();