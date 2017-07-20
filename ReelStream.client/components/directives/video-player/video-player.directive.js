app.directive('videoPlayer', function ($window) {
    return {
        restrict: 'E',
        controller: 'videoPlayerController',
        scope: {},
        templateUrl: '/components/directives/video-player/video-player.template.html',
        link: function (scope, element, attrs) {
            
            scope.videoPlayer = document.getElementById("main-video-player");

        }
    };
})