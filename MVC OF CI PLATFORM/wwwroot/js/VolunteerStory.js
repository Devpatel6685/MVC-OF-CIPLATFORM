
var model = $('#model');
var search = $('.col-11');
search.removeClass('col-md-4');

$('#search-input').keyup(function () {
    renderStories();
})

function renderStories() {
    var keyword = $('#search-input').val();
    var pageIndex = $('.pagination .active a').attr('id');
    $.ajax({
        url: "/VolunterrStory/volunteerStory",
        type: "GET",
        data: {
            pageIndex: pageIndex,
            SearchInputdata: keyword,
        },
        success: function (response) {
           
           $('#storyresult').html($(response).find('#storyresult').html());
            $('.pagestory').html($(response).find('.pagestory').html());
        },
        Error: function () {
            alert('error in search');
        }
    })
}

$(document).on('click', '.pagination li', function (e) {
    e.preventDefault();
    $('.pagination li').each(function () {
        $(this).removeClass('active');
    })
    $(this).addClass('active');
    renderStories();
})