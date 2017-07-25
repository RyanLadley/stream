app.filter('secondsToTime', function () {
    //This filter Takes Seconds and convers it into the format hh:mm:ss

    //Pad the provided time division with zeros
    function formatDivision(div) {
        return div < 10 ? "0" + div : div;
    }

    return function (seconds) {
        if (typeof seconds !== "number" || seconds < 0)
            return "00:00:00";

        var hours = Math.floor(seconds / 3600);
        var minutes = Math.floor((seconds % 3600) / 60);
        var seconds = Math.floor(seconds % 60);

        var time = formatDivision(minutes) + ":" + formatDivision(seconds);

        if (hours > 0) {
            time = hours + ":" + time
        }

        return time;
    }
});