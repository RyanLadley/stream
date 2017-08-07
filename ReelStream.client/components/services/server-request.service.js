app.service('serverRequest', function ($http, $cookies, $location, appSettings, tokenManager) {

    ///Serves as a wrapper for the $HTTP
    
    this.get = function (url, payload) {
        return httpRequest('GET', url, payload);
    };

    this.post = function (url, payload) {
        return httpRequest('POST', url, payload);
    };

    var httpRequest = function (httpMethod, url, payload) {
        var form = JSON.stringify(payload);

        return $http({
            method: httpMethod,
            url: appSettings.serverUrl + url,
            data: form,
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
                'Authorization': 'Bearer ' + tokenManager.getToken()
            }
        })
            .then(
            function success(response) {
                return response;
            }, function error(response) {
                return response;
            });
    }

    var checkAuthorized = function (response) {
        if (response.status == 415) {
            $location.url("/login");
        }
    }
});