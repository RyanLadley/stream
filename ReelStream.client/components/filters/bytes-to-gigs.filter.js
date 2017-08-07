app.filter('bytesToGigs', function () {

    //This filter takes bytes and converts them in into gigabytes
    return function (bytes) {
        var gig = 1073741824
        var gb = bytes / gig;

        var result = gb.toFixed(2)
        return result;
    }
});