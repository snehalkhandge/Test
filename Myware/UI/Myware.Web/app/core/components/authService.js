(function () {
    'use strict';

var serviceId = 'authService';


angular.module('app.core')
        .factory(serviceId, authService);

authService.$inject = ['$http', '$q', 'localStorageService'];


function authService($http, $q, localStorageService) {
    var cookieName = "authorizationData";
    var serviceBase = 'http://localhost:10138/';
    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: "",
        name: "",
        userId:""
    };

    var _login = function (loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        var deferred = $q.defer();

        $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

            localStorageService.set(cookieName, { token: response.access_token, userName: loginData.userName, name: response.Name, userId: response.userId });

            _authentication.isAuth = true;
            _authentication.userName = loginData.userName;
            _authentication.name = response.Name;
            _authentication.userId = response.userId;
            deferred.resolve(response);

        }).error(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _logOut = function () {

        localStorageService.remove(cookieName);

        _authentication.isAuth = false;
        _authentication.userName = "";
        _authentication.name = "";
        _authentication.userId = "";
    };

    var _fillAuthData = function () {

        var authData = localStorageService.get(cookieName);
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
            _authentication.name = authData.name;
            _authentication.userId = authData.userId;
        }

    }

   // authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;

    return authServiceFactory;
}

})();
