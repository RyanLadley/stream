app.controller('userController', function ($scope, $rootScope, serverRequest) {

    $scope.user = {}
    serverRequest.get('/api/user/settings', $scope.credentials).then(function (response) {
        $scope.user = response.data;
        createStorageDisplay();
        console.log($scope.user);
    });

    var createStorageDisplay = function () {
        var usage = ($scope.user.totalSpacedUsed / $scope.user.totalSpacedAllowed) * 100;
        $scope.usageStyle = { 'width': usage + "%" };
    };
});
