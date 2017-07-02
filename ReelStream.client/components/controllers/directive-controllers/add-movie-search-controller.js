app.controller('addMovieSearchController', function ($scope, $location, appSettings, serverRequest) {

    $scope.imageUrl = appSettings.externalMovieDBSettings.imageUrl;

    //This call waits for the user to stop typing the title before sending the api call to get the movies
    var timeout = null;
    $scope.delayedApiCall = function () {
        clearTimeout(timeout);
        timeout = setTimeout(function () {
            if ($scope.movieTitle !== null || $scope.movieTitle !== "") {
                $scope.getMoviesFromApi()
            }
        }, 500);
    };

    //Gets movie information from the server based on the movie title
    $scope.searchResults = null;
    $scope.getMoviesFromApi = function(){
        serverRequest.get('/api/movies/search/' + $scope.searchTerm).then(function (response) {
            $scope.searchResults = response.data;

            stringToDates($scope.searchResults)
        });
    };

    var stringToDates = function(array){
        for(i = 0 ; i < array.length; i++){
            array[i].release_date = new Date(array[i].release_date)
        }
    };

   
    $scope.selectSearchResult = function (movie) {
        movie.flow = $scope.selectedMovie.flow
        $scope.selectedMovie = movie;
        $scope.display.previous = $scope.display.current;
        $scope.display.current = 'confirm';
    }
});
