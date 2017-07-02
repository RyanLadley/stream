app.controller('addMovieController', function ($scope, $location, appSettings, serverRequest) {
    $scope.currentDisplay = {
        current: 'file-select',
        previous: null
    };

    $scope.selectedMovie = {};

    //This function holds the logic of controlling the "back button" 
    //TODO: Make this more elegant/scalable/readable
    $scope.back = function () {
        switch ($scope.currentDisplay.current) {
            case 'search':
                $scope.currentDisplay.current = 'file-select'
                $scope.currentDisplay.previous = null;
                break;
            case 'confirm':
                $scope.currentDisplay.current = 'search'
                $scope.currentDisplay.previous = 'file-select'
                break;
        }
    }
});
