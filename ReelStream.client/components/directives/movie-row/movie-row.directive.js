app.directive('movieRow', function ($window) {
    return {
        restrict: 'E',
        //controller: 'movieRowController',
        scope: {
            rowLength: '=?',
            movieCollection: '='
        },
        templateUrl: '/components/directives/movie-row/movie-row.template.html',
        link: function (scope, element, attrs) {

            //This is a hard coded value from the css (_movie-queue.scss) 
            //TODO: Moake it not hard coded
            var movieSelectSize = 269; 

            var setQueueWidth = function () {
                scope.queueWidth = element[0].offsetWidth;

                //Number of movies that can fit onto the screen at once 
                //QueueWidth calculated in the directive itself
                scope.rowLength = parseInt((scope.queueWidth / movieSelectSize));
                if (scope.rowLength == 0) {
                    scope.rowLength = 1;
                }
            }

            setQueueWidth();
            angular.element($window).bind('resize', function () {
                setQueueWidth();
                scope.$apply();
            })

            scope.movieStyle = []
            scope.enlargeElement = function (index) {
                var smallWidth = movieSelectSize - ((movieSelectSize) / (scope.rowLength-1))-2;
                for (var i = 0; i < scope.movieCollection.length; i++) {
                    scope.movieStyle[i] = {
                        'width': smallWidth
                    };
                }
                scope.movieStyle[index] = '';
            }

            scope.returnElements = function (index) {
                for (var i = 0; i < scope.movieCollection.length; i++) {
                    scope.movieStyle[i] = '';
                }
            }
        }
    };
})