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


var noLoginRequired = ["/login", "/register"]
app.run(function ($rootScope, $location, serverRequest, tokenManager) {
    $rootScope.isLoggedIn = false;
    
    serverRequest.get('/api/user/token').then(function (response) {

        console.log("Fired")
        //Verify user is still logged in at the start of a new session. Also updates their token
        if (response.status == 200) {
            tokenManager.saveToken(response.data.accessToken);
            $rootScope.isLoggedIn = true;
            $location.url("/")
        }
        else {
            console.log("Fired")
            tokenManager.removeToken();
            $rootScope.isLoggedIn = false;
        }
    });
    
    //If the user is logged out, the only page they can view is the login page
    //On route change, check to make sure the user is signed in
    $rootScope.$on("$routeChangeStart", function (event, next, current) {
        if (!$rootScope.isLoggedIn && noLoginRequired.indexOf($location.path()) === -1) {
            $location.url("/login")
        }
    })
});

