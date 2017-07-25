app.controller('videoPlayerController', function ($scope, $routeParams, $sce, $interval, $timeout, serverRequest, appSettings) {
    $scope.videoPlayer = {/*Set in the directive link*/ };

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
        
        //Temp
    });


    //Update the API with the current playback time perisdically; 
    var updatingApi = $interval(function () {

        //Don't update if paused and movie hasn't been populated
        if ($scope.playbackUpdate !== undefined && $scope.playbackUpdate.playbackTime != $scope.videoPlayer.currentTime) {

            $scope.playbackUpdate.playbackTime = $scope.videoPlayer.currentTime;
            serverRequest.post('/api/movies/playback/' + $scope.movieId, $scope.playbackUpdate);
        }
    }, 10000)

    //Stop Updating when we leave the page
    $scope.$on('$destroy', function () {
        if (updatingApi)
            $interval.cancel(updatingApi);
    });



    //Toggle the display controls
    $scope.displayControls = function () {
        setShowControls(true);
        startControlsTimer();
    }

    //This function prevents the flicker that happens when setting the showControls variable
    var flickerLock = false;
    var setShowControls = function (boolean) {
        if (!flickerLock) {
            $scope.showControls = boolean;
        }
    }

    var controlsTimeout = null
    var startControlsTimer = function () {
        $timeout.cancel(controlsTimeout);
        controlsTimeout = $timeout(function () {
            setShowControls(false);
            flickerLock = true;
            $timeout(function () { flickerLock = false; }, 200);
        }, 2000);
    }



    //Play-Pause Logic
    $scope.showPlayButton = false;
    $scope.playOrPauseIcon = "fa-pause"
    $scope.togglePlayOrPause = function () {

        $scope.showPlayButton = !$scope.showPlayButton
        startControlsTimer();

        if ($scope.showPlayButton) {
            pauseVideo();
        }
        else {
            playVideo();
        }
    }

    var pauseVideo = function () {
        $scope.playOrPauseIcon = "fa-play";
        $scope.videoPlayer.pause();
    }

    var playVideo = function () {
        $scope.playOrPauseIcon = "fa-pause";
        $scope.videoPlayer.play();
    }



    //Full Screen Logic
    $scope.showExpandButton = true;
    $scope.fullScreenIcon = "fa-expand"
    $scope.toggleFullScreen = function () {

        $scope.showExpandButton = !$scope.showExpandButton
        startControlsTimer();

        if (!$scope.showExpandButton) {
            $scope.fullScreenIcon = "fa-compress";
            if ($scope.fullScreen.requestFullScreen) {
                $scope.fullScreen.requestFullScreen();
            }
            else if ($scope.fullScreen.webkitRequestFullScreen) {
                $scope.fullScreen.webkitRequestFullScreen()
            }
            else if ($scope.fullScreen.mozRequestFullScreen) {
                $scope.fullScreen.mozRequestFullScreen();
            }
        }
        else {
            $scope.fullScreenIcon = "fa-expand";
            if( document.exitFullscreen) {
                document.exitFullscreen();
            }
            else if (document.webkitExitFullscreen) {
                document.webkitExitFullscreen()
            }
            else if (document.msExitFullscreen) {
                document.msExitFullscreen();
            }
        }
    }

    var pauseVideo = function () {
        $scope.playOrPauseIcon = "fa-play";
        $scope.videoPlayer.pause();
    }

    var playVideo = function () {
        $scope.playOrPauseIcon = "fa-pause";
        $scope.videoPlayer.play();
    }



    //Volume Logic
    $scope.videoPlayer.muted = false;
    $scope.volumeIcon = "fa-volume-up"; //fa-volume-down fa-volume-off
    $scope.toggleMuted = function () {

        $scope.videoPlayer.muted = !$scope.videoPlayer.muted
        startControlsTimer();

        if ($scope.videoPlayer.muted) {
            $scope.volumeIcon = "fa-volume-off"
        }
        else {
            $scope.volumeIcon = "fa-volume-up"
        }
    }



    //Progress Bar Logic
    var updateProgressBar = function () {
        var progress = ($scope.videoPlayer.currentTime / $scope.videoPlayer.duration) * 100;
        $scope.progressStyle = { 'width': progress + "%" };
    }

    $interval(function () {
        if ($scope.videoPlayer.currentTime == $scope.videoPlayer.duration) {
            pauseVideo();
        }
        updateProgressBar()
    }, 1000);

    //Track Width is defined in the directive;
    //This is called when the progress bar is clicked
    $scope.jumpProgress = function (event) {
        var jumpPercentage = event.offsetX / $scope.playbackTrackWidth;
        $scope.videoPlayer.currentTime = $scope.videoPlayer.duration * jumpPercentage;
        updateProgressBar();
    }

    //This is called when hovering over the progress bar
    $scope.getJumpLocation = function (event) {
        var jumpPercentage = event.offsetX / $scope.playbackTrackWidth;
        $scope.jumpLocation = $scope.videoPlayer.duration * jumpPercentage;
        $scope.jumpLocationPosition = { 'left': (event.offsetX - 10) + "px"}
    }
});
