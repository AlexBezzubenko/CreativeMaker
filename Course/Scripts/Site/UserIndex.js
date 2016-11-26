$(document).ready(function () {
    $("#creativeTable").tablesorter();
    $(".rateYo").rateYo({
        starWidth: "40px",
        halfStar: true,
        onInit: function (rating, rateYoInstance) {
            var rate = $(this).parent().parent()
                .find('.itemRate').val().replace(',', '.');
            $(this).rateYo("option", "rating", rate);
            $(this).rateYo("option", "readOnly", true);
        }
    });

    $('#create_button').on("click", function () {
        var creative_name = $('#creative-name').val();
        var user_id = $('#user_id').val();

        var data = {
            name: creative_name,
            userId: user_id
        };

        $.ajax({
            type: 'POST',
            async: false,
            data: data,
            url: '/User/CreateCreative'
        }).success(function (id) {
            window.location.href = "/User/Edit/" + id;
        });
    });
});