app.controller('addMovieFileSelectController', function ($scope, appSettings, serverRequest) {

    //When a file is selected, we assign it to the selected movie and automaticly move on to the searchs creen
    //We are using ng-flow for upload -> https://github.com/flowjs/ng-flow
    $scope.selectedMovie = {};
    $scope.$watch('selectedMovie.flow.files[0]', function () {
        console.log($scope.selectedMovie.flow)
        if ($scope.selectedMovie.flow.files.length > 0) {
            $scope.display.previous = $scope.display.current;
            $scope.display.current = 'search';
        }
    })
});
