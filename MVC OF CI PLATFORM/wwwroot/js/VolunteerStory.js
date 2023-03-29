
var model = $('#model');
$(document).on('click', '.pagination li', function (e) {
    e.preventDefault();
    $('.pagination li').each(function () {
        $(this).removeClass('active');
    })
    $(this).addClass('active');
    var pageIndex = $('.pagination .active a').attr('id');
    $.ajax({
        url: "/VolunterrStory/volunteerStory",
        type: "GET",
        data: {
            pageIndex: pageIndex,
        },
        success: function (response) {
            $('#result').html($(response).find('#result').html());
            $('.page').html($(response).find('.page').html());
        },
        Error: function () {
            alert('error in skill');
        }
    })
})