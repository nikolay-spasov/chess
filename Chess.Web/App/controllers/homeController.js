app.controller('homeController', ['$scope', 'urlService', 'gameService', function ($scope, urlService, gameService) {
    'use strict';

    $scope.findGameCriteria = {
        minutes: 5,
        color: 'whatever'
    };

    $scope.showGamesFilterOptions = false;
    $scope.btnPlayClicked = function() {
        $scope.showGamesFilterOptions = true;
    };

    $scope.cancelBtnClicked = function() {
        $scope.showGamesFilterOptions = false;
    };

    $scope.okBtnClicked = function() {
        gameService.searchGame($scope.findGameCriteria).then(function(data) {
            console.log(data);
        }, function(err) {

        });
    };

}]);