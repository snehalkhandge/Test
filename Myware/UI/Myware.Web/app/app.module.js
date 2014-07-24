(function () {

    'use strict';
    var appId = 'app';

    angular.module(appId, [
        /*
         * Order is not important. Angular makes a
         * pass to register all of the modules listed
         * and then when app.dashboard tries to use app.data,
         * it's components are available.
         */

        /*
         * Everybody has access to these.
         * We could place these under every feature area,
         * but this is easier to maintain.
         */
        'angular-data.DSCacheFactory',
        'angularFileUpload',
        'xeditable',
        'ngplus',
        'ngMessages',
        'ui.bootstrap',        
        'LocalStorageModule',
        'app.core',
        'app.widgets',


        /*
         * Feature areas
         */
        'app.avengers',
        'app.account',
        'app.dashboard',
        'app.usermanagement',
        'app.presalesunit',
        'app.presales',
        'app.taskmanager',
        'app.layout'
    ]);




    angular.module(appId).config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptorService');
    });


    angular.module(appId).run(['authService', function (authService) {
        authService.fillAuthData();
    }]);


    angular.module(appId).run(function (editableOptions) {
        editableOptions.theme = 'bs3';
    });


})();


