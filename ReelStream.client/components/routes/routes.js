app.config(['$routeProvider', '$locationProvider', 
    function($routeProvider, $locationProvider){
    $routeProvider
    .when("/",
        {
            controller: 'homeController',
            templateUrl: '/site/home/home.index.html'
        }
    )
    .when("/video-stream/:movieId",
        {
            //controller: 'videoPlayerController',
            templateUrl: '/site/video-stream/video-player.index.html'
        }
    )
    .when("/browse/genre/:genreId/:type",
        {
            controller: 'browseController',
            templateUrl: '/site/browse/browse.index.html'
        }
    )
    .otherwise("/",
    {
        redirectTo: "/"
    })
}]);