app.controller('movieQueueController', function ($scope, $location, appSettings) {

    //This is a hard coded value from the css (_movie-queue.scss) 
    //TODO: Moake it not hard coded
    var movieSelectSize = 266; 
    
    var currentPage = 0;

    $scope.changePage = function (change) {
        currentPage += change;
        updateMovieQueue();
    }

    $scope.$watch('queueWidth', function () {
        updateMovieQueue();
    });

    $scope.$watch('movies', function () {
        updateMovieQueue();
    });


    //TODO: Keep movies from changing randomly when resizing screen
    var updateMovieQueue = function () {
        //Number of movies that can fit onto the screen at once 
        //QueueWidth calculated in the directive itself
        var numberShown = parseInt(($scope.queueWidth / movieSelectSize) - 1);
        if (numberShown == 0) {
            numberShown = 1;
        }

        if (typeof $scope.movies !== 'undefined') {

            //Wraps the "currentPage" ie, if -1, the it will be changed to the slast page
            //Consider moving this loginc outisde of this function
            var numberOfPages;
            if (numberShown > $scope.movies.length) { //More shown than needed, so we know we have 1 page (will be zero if computed)
                numberShown = $scope.movies.length
                numberOfPages = 1;

            }
            else { //Compute number of pages
                numberOfPages = Math.ceil($scope.movies.length / numberShown) ;
            }
            
            currentPage = Math.abs(currentPage % numberOfPages); //wth is -0?
            var firstIndex = numberShown * currentPage;
            $scope.shownMovies = $scope.movies.slice(firstIndex, firstIndex + numberShown);
        }
    }


});
