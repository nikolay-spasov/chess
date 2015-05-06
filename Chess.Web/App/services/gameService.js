app.factory('gameService', ['$http', '$q', 'urlService', function ($http, $q, urlService) {
    'use strict';

    var searchGame = function(criteria) {
        var deferred = $q.defer();

        $http.post(urlService.relative('/searchGame'), criteria)
            .success(function(data) {
                deferred.resolve(data);
            })
            .error(function(err) {
                deferred.reject(err);
            });

        return deferred.promise;
    };

    return {
        searchGame: searchGame
    };

}]);