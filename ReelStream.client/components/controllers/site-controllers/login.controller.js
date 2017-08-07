app.controller('loginController', function ($scope, $rootScope, $location, serverRequest, tokenManager) {

    $scope.credentials = {}

    $scope.login = function () {

        if (isFilled($scope.credentials)) {
            serverRequest.post('/api/user/login', $scope.credentials).then(function (response) {
                
                if (response.status == 200) {
                    $rootScope.loginUser(response.data);
                }
                else {
                    $scope.errorMessage = response.data.errorMessage;
                }
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
