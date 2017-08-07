app.controller('headerController', function ($scope, $rootScope) {
    $scope.user = {};
    $scope.initials = "";
    $scope.$watch(function() {return $rootScope.isLoggedIn }, function () {
        $scope.isLoggedIn = $rootScope.isLoggedIn;

        if ($scope.isLoggedIn) {
            $scope.user = $rootScope.user;
            $scope.initials = $scope.user.firstName[0] + $scope.user.lastName[0];
        }
    })

    
});
