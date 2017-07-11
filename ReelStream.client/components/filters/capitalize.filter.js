app.filter('capitalize', function () {
    //This Function Capilizes the First Letter of Every Word in the provided string
    return function (input) {
        if (input)
            return input.split(' ').map(function (wrd) { return wrd.charAt(0).toUpperCase() + wrd.substr(1).toLowerCase(); }).join(' ');
    }
});