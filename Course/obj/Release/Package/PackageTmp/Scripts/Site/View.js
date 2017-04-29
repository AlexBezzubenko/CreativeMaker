var changeFontSize = function (increaseFont) {
    $('.panel-body').find('*').each(function () {
        var newFontSize;
        var currentFontSize = $(this).css('font-size');
        var currentFontSizeNum = parseFloat(currentFontSize, 10);

        if (increaseFont) {
            $(this).css('font-size', 0);
            newFontSize = currentFontSizeNum * 1.25;
        } else {
            newFontSize = currentFontSizeNum * 0.8;
        }

        $(this).css('font-size', newFontSize);
    });
};

$(document).ready(function () {
    var originalFontSize = $('.panel-body *').css('font-size');

    $(".resetFont").click(function () {
        $('.panel').animate({ marginLeft: '0%', marginRight: '0%' }, 1000);
    });
    $(".increaseFont").on('click', function () {
        changeFontSize(true);
    });
    $(".decreaseFont").on('click', function () {
        changeFontSize(false);
    });

    $(".increasePanelWidth").on('click', function () {
        $('.panel').animate({ marginLeft: '0%', marginRight: '0%' }, 1000);
    });
    $(".decreasePanelWidth").on('click', function () {
        $('.panel').animate({ marginLeft: '20%', marginRight: '20%' }, 1000);
    });

    $("#rateYoGlobalDiv").rateYo({
        numStars: 1,
        fullStar: true,
        starWidth: "40px",
        rating: 5,
        readOnly: true
    });


    $("#rateYoDiv").rateYo({
        starWidth: "40px",
        halfStar: true,
        rating: $('#user_mark').val(),
        readOnly: JSON.parse($('#is_rate_readonly').val())
    });

    $("#rateYoDiv").rateYo()
      .on("rateyo.set", function (e, data) {
          $.ajax({
              async: false,
              data: { id: $('#creative_id').val(), rating: data.rating },
              url: '/User/EstimateCreative'
          }).success(function (newRating) {
              $('#rateText span').html(newRating + " " + $('#rating_string').val());
          });
      });

    $('html, body').animate({
        scrollTop: $('#h' + $('#selected_header_id').val()).position().top - 100
    }, 'slow');
});