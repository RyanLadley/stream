app.controller('homeController', function ($scope, $location, getRequest) {

    getRequest.request('/api/movies').then(function (response) {
        $scope.movies = response.data;
    })
});
