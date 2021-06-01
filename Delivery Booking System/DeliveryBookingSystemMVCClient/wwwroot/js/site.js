$(function MinDate() {
    $(document).ready(function () {
        var todaysDate = new Date();
        var year = todaysDate.getFullYear();
        var month = ("0" + (todaysDate.getMonth() + 1)).slice(-2);
        var day = ("0" + todaysDate.getDate()).slice(-2);
        var minDate = (year + "-" + month + "-" + day);
        $('.inspectionDate input').attr('min', minDate);
    });
});