app.config(['$routeProvider', '$locationProvider', 
    function($routeProvider, $locationProvider){
    $routeProvider
    .when("/",
        {
            controller: 'homeController',
            templateUrl: '/site/home/home.index.html'
        }
    )
    .when("/video-stream/:videoFileId",
        {
            controller: 'videoPlayerController',
            templateUrl: '/site/video-stream/video-player.index.html'
        }
    )
    .otherwise("/",
    {
        redirectTo: "/"
    })
}]);