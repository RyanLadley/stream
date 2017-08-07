app.controller('registerController', function ($scope, $rootScope, $location, serverRequest) {

    $scope.user = {}

    $scope.sendRegistration = function () {

        if (isCompleteUser($scope.user)) {
            if ($scope.user.passphrase == $scope.confirmPassword) {
                serverRequest.post('/api/user/register', $scope.user).then(function (response) {

                    if (response.status == 200) {
                        $rootScope.loginUser(response.data);
                    }
                    else {
                        $scope.errorMessage = response.data.errorMessage;
                    }
                });
            }
            else {
                $scope.errorMessage = "Passwords Don't Match";
            }
        }
        else {
            $scope.errorMessage = "Please fill out all fields";
        }
        
    }

    var isCompleteUser = function (user) {
        return (
            !isNullOrEmpty(user.username) &&
            !isNullOrEmpty(user.passphrase) &&
            !isNullOrEmpty(user.email) &&
            !isNullOrEmpty(user.firstName) &&
            !isNullOrEmpty(user.lastName)
        );
    }

    var isNullOrEmpty = function (string){
        return (!string || 0 === string.length); 
    }
});
