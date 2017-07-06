app.controller('addMovieConfirmationController', function ($scope, $location, appSettings, serverRequest, datetimeConversion) {

    $scope.imageUrl = appSettings.externalMovieDBSettings.imageUrl;
    
    $scope.uploadMovie = function () {
        

        console.log($scope.selectedMovie.flow);
        $scope.selectedMovie.flow.opts.target = appSettings.serverUrl + "/api/upload/movie";
        $scope.selectedMovie.flow.opts.uploadMethod = "POST";
        $scope.selectedMovie.flow.opts.query.releaseDate = datetimeConversion.dateForServer($scope.selectedMovie.flow.opts.query.releaseDate);
        $scope.selectedMovie.flow.upload();
        $scope.display = 'none';
    }
    
});