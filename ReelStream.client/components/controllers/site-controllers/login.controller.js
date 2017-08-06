app.controller('loginController', function ($scope, $rootScope, $location, serverRequest, tokenManager) {

    $scope.credentials = {}

    $scope.login = function () {

        if (isFilled($scope.credentials)) {
            serverRequest.post('/api/user/login', $scope.credentials).then(function (response) {
                tokenManager.saveToken(response.data.accessToken);
                $rootScope.isLoggedIn = true;
                $location.url("/");
            });
        }
        else {
            $scope.errorMessage = "Please enter your username and password";
        }
    }

    var isFilled = function (credentials) {
        return (
            !isNullOrEmpty(credentials.username) &&
            !isNullOrEmpty(credentials.passphrase)
        )
    }

    var isNullOrEmpty = function (string) {
        return (!string || 0 === string.length);
    }

});
