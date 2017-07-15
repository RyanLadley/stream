app.directive('movieSelect', function () {
    return {
        restrict: 'E',
        controller: 'movieSelectController',
        scope: {
            movie: '=',
            selectStyle: '<?'
        },
        templateUrl: '/components/directives/movie-select/movie-select.template.html',
    };
})