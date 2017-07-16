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

    $scope.$watch('moviesPerRow', function () {
        updateRows();
    });

    $scope.$watch('movies', function () {
        updateRows();
    });

    $scope.movieCollectionRows = [[]]
    $scope.numberOfRows = 0
    //Creates the the movieCollections for the individial rows. 
    var updateRows = function () {
        $scope.movieCollectionRows = [[]]
        if (typeof $scope.movies !== 'undefined') {
            $scope.numberOfRows = Math.ceil($scope.movies.length / $scope.moviesPerRow )
            var firstIndex = 0;

            for (var i = 0; i < $scope.numberOfRows; i++) {
                firstIndex = i * $scope.moviesPerRow
                $scope.movieCollectionRows[i] = $scope.movies.slice(firstIndex, firstIndex + $scope.moviesPerRow);
            }
        }
    }
});
