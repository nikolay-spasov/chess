app.controller('loginController', ['$scope', '$state', '$stateParams', 'authService', 'notificationService',
    function ($scope, $state, $stateParams, authService, notificationService) {
        'use strict';

        $scope.login = function (credentials) {
            authService.login(credentials).then(function () {
                notificationService.success('Login completed!');
                var returnState = $stateParams.returnState || 'home';
                if ($stateParams.params) {
                    $state.go(returnState, $stateParams.params);
                } else {
                    $state.go(returnState);
                }
            });
        };
    }
]);