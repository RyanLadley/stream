app.directive('addMovieConfirmation', function () {
    return {
        restrict: 'E',
        controller: 'addMovieConfirmationController',
        scope: {
            display: '=',
            selectedMovie: '='
        },
        templateUrl: '/components/directives/add-movie/add-movie-confirmation.template.html',
    };
})