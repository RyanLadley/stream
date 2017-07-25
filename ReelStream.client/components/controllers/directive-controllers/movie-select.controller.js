app.controller('movieSelectController', function ($scope, $location, appSettings) {
    
    $scope.imageServer = appSettings.serverUrl;

    $scope.showPlaybackTrack = function () {
        return (($scope.movie.playbackTime > 0)
            &&  ($scope.movie.duration != null || $scope.movie.duration != 0)
            &&  ($scope.movie.playbackTime < $scope.movie.duration) );
    }

    $scope.progressStyle = {}
    if ($scope.showPlaybackTrack) {
        var progress = ($scope.movie.playbackTime / $scope.movie.duration) * 100;
        $scope.progressStyle = { height: progress + '%' };
    }
});
