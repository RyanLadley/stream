app.directive('movieQueue', function () {
    return {
        restrict: 'E',
        controller: 'movieQueueController',
        scope: {
            movies: '=',
            queueTitle: '='
        },
        templateUrl: '/components/directives/movie-queue/movie-queue.template.html',
    };
})