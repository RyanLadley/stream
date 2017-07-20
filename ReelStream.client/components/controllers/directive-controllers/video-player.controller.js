app.controller('videoPlayerController', function ($scope, $routeParams, $sce, $interval, serverRequest, appSettings) {
    $scope.videoPlayer= {/*Set in the directive link*/ };

    $scope.server = appSettings.serverUrl;
    $scope.movieId = $routeParams.movieId;
    $scope.streamApi = $sce.trustAsResourceUrl($scope.server + "/api/video/" + $scope.movieId);

    serverRequest.get('/api/movies/' + $scope.movieId).then(function (response) {
        $scope.movie = response.data;

        if ($scope.movie.playbackTime != null) {
            $scope.videoPlayer.currentTime = $scope.movie.playbackTime;
        }

        $scope.playbackUpdate = {
            movieId: $scope.movie.movieId,
            playbackTime: null
        }
    });


    $interval(function () {
        if ($scope.playbackUpdate !== undefined) {
            $scope.playbackUpdate.playbackTime = $scope.videoPlayer.currentTime
            serverRequest.post('/api/movies/playback/' + $scope.movieId, $scope.playbackUpdate);
        }
    }, 10000)
});
