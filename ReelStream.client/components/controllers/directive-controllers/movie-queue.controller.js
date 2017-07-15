app.controller('movieQueueController', function ($scope, $location, appSettings, modulo) {
    
    $scope.numberShown = 1;

    var currentPage = 0;

    $scope.changePage = function (change) {
        currentPage += change;
        updateMovieQueue();
    }

    $scope.$watch('numberShown', function () {
        updateMovieQueue();
    });

    $scope.$watch('movies', function () {
        updateMovieQueue();
    });


    //TODO: Keep movies from changing randomly when resizing screen
    var updateMovieQueue = function () {
        if (typeof $scope.movies !== 'undefined') {

            //Wraps the "currentPage" ie, if -1, the it will be changed to the slast page
            //Consider moving this loginc outisde of this function
            var numberOfPages;
            if ($scope.numberShown > $scope.movies.length) { //More shown than needed, so we know we have 1 page (will be zero if computed)
                $scope.numberShown = $scope.movies.length
                numberOfPages = 1;

            }
            else { //Compute number of pages
                numberOfPages = Math.ceil($scope.movies.length / $scope.numberShown) ;
            }
            
            currentPage = modulo(currentPage, numberOfPages);
            var firstIndex = $scope.numberShown * currentPage;
            $scope.shownMovies = $scope.movies.slice(firstIndex, firstIndex + $scope.numberShown);
        }
    }
});
