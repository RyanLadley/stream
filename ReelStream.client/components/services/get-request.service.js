app.service('getRequest', function ($http, $cookies, $location, appSettings) {

    //Http get request wrapper to send data to api.
    this.request = function (url, payload) {
        var form = new FormData()
        form.append("payload", JSON.stringify(payload))
        //form.append("token", JSON.stringify($cookies.getObject('token')))

        return $http.get(appSettings.serverUrl + url, form, {
            withCredentials: false,
            headers: {
                'Content-Type': undefined
            },
            transformRequest: angular.identity
        }).then(
        function(response){
            return response
        })
    };
});