app.controller('registerController', ['$scope', 'registerService', function ($scope, registerService) {
    'use strict';

    $scope.register = function (credentials) {

        registerService.registerUser(credentials).then(function(data) {
            console.log(data);
        }, function(err) {
            console.error(err);
        });
    };

}]);