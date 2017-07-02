app.directive('addMovieFileSelect', function () {
    return {
        restrict: 'E',
        controller: 'addMovieFileSelectController',
        scope: {
            display: '=',
            selectedMovie: '='
        },
        templateUrl: '/components/directives/add-movie/add-movie-file-select.template.html',
    };
})