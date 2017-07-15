app.factory('modulo', function () {
    //Java Script moduls (%) are stupid with negative numbers. This fixes that. 
    return function (n, m) {
        return ((n % m) + m) % m;
    }
    
});