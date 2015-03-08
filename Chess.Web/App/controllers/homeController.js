app.controller('homeController', ['$scope', 'urlService', function ($scope, urlService) {
    'use strict';

    $scope.text = 'This is home';
    $scope.url = urlService.relative('/home');

}]);