app.service('datetimeConversion', function () {

    //Http get request wrapper to send data to api.
    this.dateForServer = function (date) {

        var day = date.getDate();     
        var month = date.getMonth() + 1;  //GetMonth is zeroindeed for some reason
        var year = date.getFullYear();  

        return year + "-" + month + "-" + day;
    };
    
});