var app = angular.module('app', ['ngRoute', 'ngCookies',], function ($locationProvider) {
    $locationProvider.html5Mode(true);
})

.factory('appSettings', function () {
    return {
        serverUrl: "http://localhost:53734"
    }
});
