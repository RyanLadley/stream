app.config(['$routeProvider', '$locationProvider', 
    function($routeProvider, $locationProvider){
    $routeProvider
    .when("/",
        {
            controller: 'homeController',
            templateUrl: '/site/home/home.index.html'
        }
    )
    .when("/states/:stateId/regions/:regionId",
        {
            controller: 'regionsController',
            templateUrl: '/site/regions/regions.index.html'
        }
    )
    .otherwise("/",
    {
        redirectTo: "/"
    })
}]);