app.directive('movieQueue', function () {
    return {
        restrict: 'E',
        controller: 'movieQueueController',
        scope: {},
        templateUrl: '/components/directives/movie-queue/movie-queue.template.html',
    };
})