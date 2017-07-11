app.controller('browseFilterController', function ($scope, appSettings, serverRequest) {

    $scope.server = appSettings.serverUrl;
    $scope.columnHeight = 5;

    //Simply retunr an array counting up to column height we can ng-repeat over;
    var initializePositions = function () {
        var positions = new Array($scope.columnHeight);
        for (i = 0; i < positions.length; i++) {
            positions[i] = i
        }

        return positions;
    }
    $scope.columnPositions = initializePositions();

    serverRequest.get('/api/genres').then(function (response) {
        $scope.genres = response.data;
    });

    var timeout = null
    $scope.hoverIn = function () {
        clearTimeout(timeout);
        timeout = setTimeout(function () {
            $scope.displaySelection = true;
            $scope.$apply();
        }, 500);
    }

    $scope.hoverOut = function () {
        //selection not yet display, restart hover timer
        clearTimeout(timeout);
        timeout = setTimeout(function () {
            $scope.displaySelection = false;
            $scope.$apply();
        }, 300);
    }

});
