(function () {
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

                var expiresDateObj = authData.expires;
                var currentDataObj = new Date().getTime();

                if ((expiresDateObj - currentDataObj) < 0)
                {                    
                    $location.path('/account/login');
                }

                config.headers.Authorization = 'Bearer ' + authData.token;
            }



            return config;
        };

        var _responseError = function (rejection) {
            if (rejection.status === 401) {
                $location.path('/account/login');
            }
            return $q.reject(rejection);
        };

        var _response = function(response) {
            return  $q.when(response);
            //return response || $q.when(response);
        };

        authInterceptorServiceFactory.request = _request;
        authInterceptorServiceFactory.responseError = _responseError;
        authInterceptorServiceFactory.response = _response;

        return authInterceptorServiceFactory;

    }
    
})();