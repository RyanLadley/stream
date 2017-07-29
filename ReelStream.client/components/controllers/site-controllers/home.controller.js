app.controller('homeController', function ($scope, $location, serverRequest) {

    serverRequest.get('/api/movies/queues').then(function (response) {
        $scope.queues = response.data;
        console.log($scope.queues)
    });
    
});
