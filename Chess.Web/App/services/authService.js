app.factory('authService', ['$injector', '$q', 'modelStateErrorsService', 'notificationService', function ($injector, $q, modelStateErrorsService, notificationService) {
    'use strict';

    var login = function (credentials) {
        var deferred = $q.defer();
        var $http = $injector.get('$http');

        $http.post('/api/authentication/login', credentials).then(function (authenticationResponse) {
            sessionStorage.setItem('authenticationData', JSON.stringify(authenticationResponse.data));
            deferred.resolve();
        }, function (error) {
            var errors = modelStateErrorsService.parseErrors(error);
            for (var i = 0; i < errors.length; i++) {
                notificationService.error(errors[i]);
            }
            deferred.reject(error);
        });

        return deferred.promise;
    };

    var logout = function () {
        sessionStorage.removeItem('authenticationData');
        notificationService.success("Logout successful!");
    };

    var isAuthenticated = function () {
        var authData = sessionStorage.getItem('authenticationData');
        return authData != null;
    }

    var getFullname = function () {
        var authData = JSON.parse(sessionStorage.getItem('authenticationData'));
        return authData ? authData.fullname : '';
    }

    var getUserId = function () {
        var authData = JSON.parse(sessionStorage.getItem('authenticationData'));
        return authData ? authData.userId : '';
    }

    var getAuth = function () {
        if (sessionStorage.authenticationData) {
            return JSON.parse(sessionStorage.authenticationData);
        }
        return null;
    };

    var clearAuth = function () {
        sessionStorage.authenticationData = '';
    };

    return {
        login: login,
        logout: logout,
        isAuthenticated: isAuthenticated,
        getFullname: getFullname,
        getAuth: getAuth,
        clearAuth: clearAuth,
        getUserId: getUserId
    };
}]);
