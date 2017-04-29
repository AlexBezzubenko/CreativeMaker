$(function () {

    $('div#loading').hide();

    var page = 0;
    var _inCallback = false;
    function loadItems() {
        if (page > -1 && !_inCallback) {
            _inCallback = true;
            page++;
            $('div#loading').show();

            $.ajax({
                type: 'GET',
                url: '/Home/Search/' + page,
                data: { query: $('#query').val(), id: page },
                success: function (data, textstatus) {
                    if (data != '') {
                        $("#results tbody").append(data);
                    }
                    else {
                        page = -1;
                    }
                    _inCallback = false;
                    $("div#loading").hide();

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
                }
            });
        }
    }
    
    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {
            loadItems();
        }
    });
})