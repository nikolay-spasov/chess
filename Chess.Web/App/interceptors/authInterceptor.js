app.factory('authInterceptor', ['authService', '$q', '$injector',
    function (authService, $q, $injector) {
        'use strict';

        var isNeedTokenAuth = function (config) {

            //except login
            if (config.url.toLowerCase().indexOf("/api/authentication/login") >= 0) {
                return false;
            }

            if (config.url.toLowerCase().indexOf("/api/") >= 0) {
                return true;
            }

            return false;

        };

        var addAuthToken = function (config) {
            if (isNeedTokenAuth(config) == false) return;

            config.headers = config.headers || {};
            var authData = authService.getAuth();
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }
        };

        var request = function (config) {
            addAuthToken(config);

            return config;
        };

        var responseError = function (rejection) {
            return $q.reject(rejection);
        };

        return {
            request: request,
            responseError: responseError
        };
    }]);