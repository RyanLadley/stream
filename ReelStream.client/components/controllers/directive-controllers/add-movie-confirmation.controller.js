app.controller('addMovieConfirmationController', function ($scope, $location, appSettings, serverRequest, datetimeConversion) {

    $scope.imageUrl = appSettings.externalMovieDBSettings.imageUrl;
    
    $scope.uploadMovie = function () {
        var obj = {};
        obj.flow = $scope.selectedMovie.flow;

        delete $scope.selectedMovie['flow'];
        
        obj.flow.opts.target = appSettings.serverUrl + "/api/upload/movie";
        obj.flow.opts.uploadMethod = "POST";
        obj.flow.opts.query = $scope.selectedMovie;
        obj.flow.opts.query.release_date = datetimeConversion.dateForServer(obj.flow.opts.query.release_date);
        console.log(obj.flow.opts);
        obj.flow.upload();
    }
});