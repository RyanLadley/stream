app.controller('homeController', function ($scope, $location, serverRequest) {

    serverRequest.get('/api/movies').then(function (response) {
        $scope.movies = response.data;
    });

    $scope.send = function () {
        var search = {
            fileName: $scope.movieSearch
        };
        serverRequest.post('/api/movies', search).then(function (response) {
            console.log(response.data);
        });
    };
});
