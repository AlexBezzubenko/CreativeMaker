$(document).ready(function () {
    $(".rateYo").rateYo({
        starWidth: "30px",
        halfStar: true,
        onInit: function (rating, rateYoInstance) {
            var rate = $(this).parent().parent()
                .find('.itemRate').val().replace(',', '.');
            $(this).rateYo("option", "rating", rate);
            $(this).rateYo("option", "readOnly", true);
        }
    });
});