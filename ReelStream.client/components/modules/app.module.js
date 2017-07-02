var app = angular.module('app', ['ngRoute', 'ngCookies', 'flow'], function ($locationProvider) {
    $locationProvider.html5Mode(true);
})

.factory('appSettings', function () {
    return {
        serverUrl: "http://localhost:53734",
        externalMovieDBSettings: {
            imageUrl: "https://image.tmdb.org/t/p/w500"
        }
    }
});
