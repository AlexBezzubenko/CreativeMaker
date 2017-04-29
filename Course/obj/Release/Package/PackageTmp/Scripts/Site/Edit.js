$(document).ready(function () {
    $('#nameform').on('keyup keypress', function (e) {
        var keyCode = e.keyCode || e.which;
        if (keyCode === 13) {
            e.preventDefault();
            if ($('#Creative_Name').val() != $('#creative_name').val() && $("#nameform").validate().element("#Creative_Name"))
            {
                console.log('post change name');
                $.ajax({
                    async: false,
                    data: { id: $('#creative_id').val(), Name: $('#Creative_Name').val() },
                    url: '/User/ChangeCreativeName'
                });
            }
            $('#Creative_Name').blur();
            return false;
        }
    });


    $('#Creative_Name').blur(function () {
        if ($('#Creative_Name').val().trim() != '') {
            $('#Creative_Name').hide();
            $('#name_label').html($('#Creative_Name').val());

            if ($("#nameform").validate().element("#Creative_Name"))
            {
                console.log('post change name');
                $.ajax({
                    async: false,
                    data: { id: $('#creative_id').val(), Name: $('#Creative_Name').val() },
                    url: '/User/ChangeCreativeName'
                });
            }
        }
    });

    $('#creative_name_edit > i').click(function () {

        if ($('#name_label').html().trim() != '') {

            $('#Creative_Name').show();

            $('#Creative_Name').val($('#name_label').html());

            $('#name_label').html('');
            $('#Creative_Name').focus();
        }
    });


    $('#header_name_input').blur(function () {
        if ($('#header_name_input').val().trim() != '') {
            $('#header_name_input').hide();
            $('#header_name > h3').html($('#header_name_input').val());
        }
    });

    $('#header_name_edit > i').click(function () {

        if ($('#header_name > h3').html().trim() != '') {

            $('#header_name_input').show();

            $('#header_name_input').val($('#header_name > h3').html());

            $('#header_name > h3').html('');
            $('#header_name_input').focus();
        }
    });


    var simplemde = new SimpleMDE({
        element: document.getElementById("header_text"),
        spellChecker: false,
    });

    $('#myTags').tagit({
        availableTags: JSON.parse($('#json_tags').val())
    });
    $("#headers_list").sortable({
        axis: 'y',
        update: function (event, ui) {
            var headers_id = [];
            var headers = $("#headers_list").children(".header");
            headers.each(function (idx, li) {
                var header = $(li);
                headers_id.push(header.val());
            });
            $.ajax({
                async: false,
                data: { headerOrders: headers_id },
                traditional: true,
                url: '/User/UpdateHeaderOrders'
            }).success(function () {});
        }
    });

    $("#headers").delegate('.header', 'click', function (e) {
        $('.active').removeClass('active');
        $(this).addClass('active');
        var index = $(event.target).val();
        $.ajax({
            async: false,
            data: { id: index },
            url: '/User/SetHeaderContext'
        }).success(function (header) {
            //$('#header_content').replaceWith(partialView);
            var headerJson = JSON.parse(header);
            console.log(headerJson);
            simplemde.value(headerJson.Text);
            $('#header_name_input').blur();
            $('#header_content h3').html(headerJson.Name);


            $("#myTags").tagit("removeAll");
            headerJson.Tags.forEach(function (item, i, arr) {
                $("#myTags").tagit("createTag", item);
            });
            $('#header_id').val(headerJson.Id);
        });

    });

    $("#headers").delegate(".delete_header", "click", function (e) {
        var header_li = $(this).closest(".header");
        var header_id = header_li.val();

        $.ajax({
            async: false,
            data: { id: header_id },
            url: "/User/DeleteHeader"
        }).success(function () {
            if (header_li.hasClass("active")) {
                simplemde.value("");
                $('#header_name_input').blur();
                $('#header_content h3').html("");

                $("#myTags").tagit("removeAll");
                $('#header_id').val("");
            }
            header_li.remove();
        });

    });



    $("#add_header").on('click', function () {
        $.ajax({
            async: false,
            data: { id: $('#creative_id').val() },
            url: '/User/AddHeader'
        }).success(function (partialView) {
            $('.active').removeClass('active');
            var newHeader = $(partialView).appendTo('#headers_list');
            newHeader.addClass('active');
            $("#headers_list").sortable("refresh");

            $('#header_id').val(newHeader.val());
            $('#header_name > h3').html(newHeader.text());
            simplemde.value("");
            $("#myTags").tagit("removeAll");
        });
    });
    $("#update_header").on('click', function () {
        var Id = $('#header_id').val();
        if (Id < 0)
        {
            return;
        }
        var Name = $('#header_name > h3').html();
        var Text = simplemde.value();
        var Tags = $("#myTags").tagit("assignedTags");

        $(".header[value='"+ Id + "']").text(Name);

        var data = { Id: Id, Name: Name, Text: Text, Tags: Tags }
        console.log(data);
        $.ajax({
            async: false,
            data: data,
            traditional:true,
            url: '/User/UpdateHeader'
        });
    });

});