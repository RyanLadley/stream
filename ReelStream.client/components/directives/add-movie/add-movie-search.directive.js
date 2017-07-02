app.directive('addMovieSearch', function () {
    return {
        restrict: 'E',
        controller: 'addMovieSearchController',
        scope: {
            display: '=',
            selectedMovie: '='
        },
        templateUrl: '/components/directives/add-movie/add-movie-search.template.html',
    };
})