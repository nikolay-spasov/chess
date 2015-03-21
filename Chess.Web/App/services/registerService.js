app.factory('registerService', ['$http', '$q', 'urlService', function ($http, $q, urlService) {
    'use strict';

    var registerUser = function (registerModel) {
        var deferred = $q.defer();

        $http.post(urlService.relative('/account/register'), registerModel)
            .success(function (data) {
                deferred.resolve(data);
            })
            .error(function (err) {
                deferred.reject(err);
            });

        return deferred.promise;
    };

    return {
        registerUser: registerUser
    };

}]);