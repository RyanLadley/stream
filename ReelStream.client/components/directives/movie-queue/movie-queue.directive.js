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

            var setQueueWidth = function () {
                scope.queueWidth = element[0].querySelector('.queue-row').offsetWidth;
            }

            setQueueWidth();
            angular.element($window).bind('resize', function () {
                setQueueWidth();
                scope.$apply();
            })

            scope.hoverIn = function () {
                scope.displayArrows = true;
            }

            scope.hoverOut = function () {
                scope.displayArrows = false;
            }

        }
    };
})