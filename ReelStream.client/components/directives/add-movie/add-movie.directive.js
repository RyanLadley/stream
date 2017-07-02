app.directive('addMovie', function () {
    return {
        restrict: 'E',
        controller: 'addMovieController',
        scope: {},
        templateUrl: '/components/directives/add-movie/add-movie.template.html',
    };
})