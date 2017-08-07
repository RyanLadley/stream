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
        
        //Verify user is still logged in at the start of a new session. Also updates their token
        if (response.status == 200) {
            $rootScope.loginUser(response.data);
        }
        else {
            tokenManager.removeToken();
            $rootScope.isLoggedIn = false;
            $location.url("/login")
        }
    });

    $rootScope.loginUser = function(token){
        tokenManager.saveToken(token.accessToken);
        $rootScope.user = token.user;
        $rootScope.isLoggedIn = true;
        if ($location.path() == "/login" || $location.path() == "/register") {
            $location.url("/")
        }
    }
});

