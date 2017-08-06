app.service('tokenManager', function ($window) {

    var storageKey = "reelStreamToken"

    this.saveToken = function (token) {
        $window.localStorage[storageKey] = token;
    }

    this.getToken = function () {
        return $window.localStorage[storageKey];
    }

    this.removeToken = function () {
        localStorage.removeItem(storageKey);
    }
});