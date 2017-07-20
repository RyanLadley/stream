app.service('stringify', function () {

    //Takes an array of objects and a property field and creates and array string of only those properties
    this.ArrayObjectProperty = function (array, property) {

        var stringifiedArray = ""
        for (var i = 0; i < array.length; i++) {
            stringifiedArray += array[i][property] +","
        }

        return removeTrailingComma(stringifiedArray);
    }



    var removeTrailingComma = function(string) {
        return string.replace(/,(\s+)?$/, '');
    }


});