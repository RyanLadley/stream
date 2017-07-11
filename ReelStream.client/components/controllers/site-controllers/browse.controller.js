app.controller('browseController', function ($scope, $routeParams, serverRequest) {
    
    var genreId = $routeParams.genreId;
    $scope.type = $routeParams.type

    serverRequest.get("/api/genres/" + genreId).then(function (response) {
        $scope.genre = response.data;
    });


    var apiString = '/api/' + $scope.type + '/genre/' + genreId;
    serverRequest.get(apiString).then(function (response) {
        $scope.movies = response.data;
    });
});
