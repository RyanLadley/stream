app.directive('movieQueue', function($window) {
    return {
        restrict: 'E',
        controller: 'movieQueueController',
        scope: {
            movies: '=',
            queueTitle: '='
        },
        templateUrl: '/components/directives/movie-queue/movie-queue.template.html',
        link: function (scope, element, attrs) {

            scope.hoverIn = function () {
                scope.displayArrows = true;
            }

            scope.hoverOut = function () {
                scope.displayArrows = false;
            }

        }
    };
})