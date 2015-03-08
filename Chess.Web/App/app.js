var app = angular.module('app', ['ui.router', 'angular-loading-bar', 'ui.bootstrap']);

app.config(['$stateProvider', '$httpProvider', '$urlRouterProvider', function ($stateProvider, $httpProvider, $urlRouterProvider) {
    'use strict';

    //================================================
    // Make urls case insensitive
    //================================================
    $urlRouterProvider.rule(function ($injector, $location) {
        var path = $location.path(), normalized = path.toLowerCase();
        if (path != normalized) {
            $location.replace().path(normalized);
        }
    });

    //================================================
    // Routes
    //================================================
    $urlRouterProvider.otherwise("/");

    $stateProvider.state('home', {
        url: '/',
        templateUrl: '/App/templates/home.html',
        controller: 'homeController'
    });

    
}]);

//================================================
// Add an request interceptor
//================================================
app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptor');
}]);

app.run([
    '$rootScope', function ($rootScope) {
        $rootScope.$on('$stateChangeStart', function (event, toState) {
            $rootScope.activeScreen = toState.data ? toState.data.activeScreen : 'none';
        });
    }
]);

//================================================
// Add authenticate property to routes
//================================================
app.run(['$rootScope', '$state', 'authService', function ($rootScope, $state, authService) {
    $rootScope.$on("$stateChangeStart", function (event, toState, toParams, fromState, fromParams) {
        if (toState.authenticate && !authService.isAuthenticated()) {
            // User isn’t authenticated
            $state.go("login", { returnState: toState.name });
            event.preventDefault();
        }
    });
}]);