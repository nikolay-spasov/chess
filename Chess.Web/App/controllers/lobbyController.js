app.controller('lobbyController', ['$scope', function ($scope) {
    'use strict';

    $scope.getFieldColorClass = function (row, col) {
        var cl = row % 2 !== col % 2 ? 'white' : 'black';

        return cl;
    };

}]);