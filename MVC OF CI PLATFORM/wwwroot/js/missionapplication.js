

$('.nav-link').each(function () {
    $(this).parent().removeClass('bg-light');
    $(this).css('color', 'white');
});
$('.nav-link.missionapplication').parent().addClass('bg-light');
$('.nav-link.missionapplication ').css('color', 'orange');



$(document).on('click', '.miapp li', function (e) {
    e.preventDefault();
    $('.miapp li').each(function () {
        $(this).removeClass('mipactive');
    })
    $(this).addClass('mipactive');
    filterskills();
});
function filterskills() {

    var pageIndex = $('.miapp .mipactive a').attr('id');
    var keyword = $('#applicationsearch').val();
    $.ajax({
        url: "/Admin/MissionApplication",
        type: "POST",
        data: {
            SearchInputdata: keyword,
            pageindex: pageIndex
        },
        success: function (response) {
            alert("hello");
            $('.table').html($(response).find('.table').html());
            $('.pagination').html($(response).find('.pagination').html());


        }
    })
}
$(document).ready(function () {

    $('#applicationsearch').keyup(function () {
        alert('hi');
        $('.pagination .mipactive').removeClass('mipactive');
        filterSearch();

    });
});

function filterSearch() {
    var keyword = $('#applicationsearch').val();

    $.ajax({
        url: "/Admin/MissionApplication",
        type: "POST",
        data: {
            SearchInputdata: keyword,

        },
        success: function (response) {
            alert('called');

            $('.table').empty().html($(response).find('.table').html());
            $('.page').empty().html($(response).find('.page').html());

        }
    })
}
function approveapplication(miappid) {
    $.ajax({
        url: "/Admin/ApproveApplication",
        type: "POST",
        data: {
            Applicationid: miappid,
        },
        success: function (response) {
            alert('called');
            $('#loadPartialView').html($(result).find('#loadPartialView').html());
            toastr.success("Application is Approved");
        }
    })
}
function declineapplication(miappid) {
    $.ajax({
        url: "/Admin/DeclineApplication",
        type: "POST",
        data: {
            Applicationid: miappid,
        },
        success: function (response) {
            alert('called');
            $('#loadPartialView').html($(result).find('#loadPartialView').html());
            toastr.success("Application is Decline Succesfully");
        }
    })
}