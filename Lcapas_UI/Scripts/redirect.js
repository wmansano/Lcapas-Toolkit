$(document).ready(function () {
    var targetURL = "https://applyalberta.ca/"
    var countdownfrom = 10

    function countredirect() {
        if (countdownfrom != 1) {
            countdownfrom -= 1
            setTimeout(function () { countredirect(); }, 1000);
        }
        else {
            window.location = targetURL
            return
        }
    }

    countredirect()
});
