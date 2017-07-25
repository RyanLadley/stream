app.directive('videoPlayer', function ($window) {
    return {
        restrict: 'E',
        controller: 'videoPlayerController',
        scope: {},
        templateUrl: '/components/directives/video-player/video-player.template.html',
        link: function (scope, element, attrs) {
            
            scope.videoPlayer = document.getElementById("main-video-player");
            scope.fullScreen = document.getElementById("full-screen-container");

            var setPlaybackTrackWidth = function () {
                var playbackTrack = document.getElementById("playback-track");
                console.log("fired")
                if (playbackTrack.style.pixelWidth) {
                    scope.playbackTrackWidth = playbackTrack.style.pixelWidth;
                }
                else {
                    scope.playbackTrackWidth = playbackTrack.offsetWidth;
                }
            }

            setPlaybackTrackWidth();
            angular.element($window).bind('resize', function () {
                setPlaybackTrackWidth();
                scope.$apply();
            })
        }
    };
})