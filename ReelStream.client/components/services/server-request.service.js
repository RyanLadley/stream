app.service('serverRequest', function ($http, $cookies, $location, appSettings) {

    //Http get request wrapper to send data to api.
    this.get = function (url, payload) {
        var form = JSON.stringify(payload);

        return $http.get(appSettings.serverUrl + url, form, {
            withCredentials: false,
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
        }).then(
            function (response) {
                return response;
            });
    };

    this.post = function (url, payload) {
        var form = JSON.stringify(payload);

        return $http.post(appSettings.serverUrl + url,form, {
            withCredentials: false,
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            //transformRequest: angular.identity
        }).then(
            function (success) {
                return success;
            });
    };

    var createForm = function(payload){}
});