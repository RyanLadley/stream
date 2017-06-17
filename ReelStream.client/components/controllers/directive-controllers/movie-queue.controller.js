app.controller('movieQueueController', function ($scope, $location, appSettings) {

    $scope.hoverIn = function () {
        $scope.displayArrows = true;
    }

    $scope.hoverOut = function () {
        $scope.displayArrows = false;
    }
});
