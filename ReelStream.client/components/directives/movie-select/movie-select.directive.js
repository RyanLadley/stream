app.directive('movieSelect', function () {
    return {
        restrict: 'E',
        controller: 'movieSelectController',
        scope: {
            movie: '='
        },
        templateUrl: '/components/directives/movie-select/movie-select.template.html',
    };
})