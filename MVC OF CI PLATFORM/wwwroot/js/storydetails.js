

$('.slider-for').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    arrows: false,
    fade: true,
    asNavFor: '.slider-nav'
});
$('.slider-nav').slick({
    slidesToShow: 3,
    slidesToScroll: 1,
    asNavFor: '.slider-for',
    dots: true,
    focusOnSelect: true,
    autoplay: true
});

$('a[data-slide]').click(function (e) {
    e.preventDefault();
    var slideno = $(this).data('slide');
    $('.slider-nav').slick('slickGoTo', slideno - 1);
})

function recommend(storyId) {
    if (!Model.attr('data-userid')) {
        showModal();
    }
    else {
        var userids = [];
        $('.modal-body input:checked').each(function () {
            userids.push($(this).attr("id"));
        })
        console.log(userids);
        $.ajax({
            type: "POST",
            url: "/VolunterrStory/recommend",
            data: {
                userIds: userids,
                storyid:storyId,
            },
            success: function (result) {
                toastr.success('Invite Link Sent');

            }
        })
    }
}
var Model = $('#model')
function showModal() {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Please Login!',
    })
}
function getUsers() {
    if (!Model.attr('data-userid')) {
        showModal();
    } else {
        var div = $('.modal-body');
        $.ajax({
            type: "GET",
            url: "/VolunterrStory/User",
            data: {},
            dataType: "json",
            success: function (result) {
                div.empty();
                $.each(result, function (i, data) {
                    div.append('<div class="form-check ms-3"><input class="form-check-input checkbox" type="checkbox" value="' + data.firstName + " " + data.lastName + '" id=' + data.userId + '><label class="form-check-label" for=' + data.userId + '>' + data.firstName + " " + data.lastName + '</label></div>')
                })

            }
        });
    }
}
