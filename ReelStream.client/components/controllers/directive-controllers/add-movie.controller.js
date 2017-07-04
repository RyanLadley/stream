app.controller('addMovieController', function ($scope, $location, appSettings, serverRequest) {
    $scope.currentDisplay = {
        current: 'file-select',
        previous: null
    };

    $scope.selectedMovie = {
        flow: {
            isUploading: function () { },
            progress: function () { return 0; }
        }
    };

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

    var remaining = 0;
    $scope.$watch(function ($scope) { return $scope.selectedMovie.flow.progress()}, function () {
        remaining = ($scope.selectedMovie.flow.progress() * 100);
        console.log(remaining);
        $scope.uploadingStyle = {
            "clip-path": "inset(0px -10px " + remaining + "% 0px)"
        }
        console.log($scope.uploadingStyle);
        $scope.$apply;
    });
});
