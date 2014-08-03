﻿(function () {
    'use strict';

    var serviceId = 'authInterceptorService';

    angular.module('app.core')
            .factory(serviceId, authInterceptorService);

    authInterceptorService.$inject = ['$q', '$location', 'localStorageService'];

    function authInterceptorService($q, $location, localStorageService) {
        
        
        var authInterceptorServiceFactory = {};

        var _request = function (config) {

            config.headers = config.headers || {};

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }

            return config;
        }

        var _responseError = function (rejection) {
            if (rejection.status === 403 || rejection.status === 401) {
                $location.path('/login');
            }
            return $q.reject(rejection);
        }

        authInterceptorServiceFactory.request = _request;
        authInterceptorServiceFactory.responseError = _responseError;

        return authInterceptorServiceFactory;

    }
    
})();