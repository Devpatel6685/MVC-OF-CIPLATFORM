
$(document).on('click', '.use li', function (e) {
    e.preventDefault();
    $('.use li').each(function () {
        $(this).removeClass('active');
    })
    $(this).addClass('active');
    console.log($(this).children().attr("id"))
    filteruser();
});
function filteruser() {
    alert('pagination called');
    alert('filteruser called');
    var pageIndex = $('.use .active a').attr('id');
    var keyword = $('#search').val();
    $.ajax({
        url: "/Admin/User",
        type: "POST",
        data: {
            SearchInputdata: keyword,
            pageindex: pageIndex
        },
        success: function (response) {
            //console.log($(response).find('.pagination').html());
            //  $('#nouser').html($(response).find('#nouser').html());
            $('.table').html($(response).find('.table').html());
            $('.page').html($(response).find('.page').html());

        }
    })
}
$(document).ready(function () {

    $('#search').keyup(function () {
        alert('hi');
        $('.pagination .active').removeClass('active');
        filterSearch();

    });
});
function filterSearch() {
    var keyword = $('#search').val();

    $.ajax({
        url: "/Admin/User",
        type: "POST",
        data: {
            SearchInputdata: keyword,

        },
        success: function (response) {
            alert('called');
            console.log(response);
            console.log("the id element", $(response).find("#nouser").html());
            /*   $('#nouser').html($(response).find('#nouser').html());*/
            $('.table').empty().html($(response).find('.table').html());
            $('.page').empty().html($(response).find('.page').html());

        }
    })
}




/*
$(document).on('click', '.pagination li', function (e) {
    e.preventDefault();
    $('.pagination li').each(function () {
        $(this).removeClass('active');
    })
    $(this).addClass('active');
    console.log($(this).children().attr("id"))
    filteruser();
});
function filteruser() {
    var pageIndex = $('.pagination .active a').attr('id');
    $.ajax({
        url: "/Admin/User",
        type: "POST",
        data: {
            //  SearchInputdata: keyword,
            pageindex: pageIndex
        },
        success: function (result) {
            
            $('.table').html($(result).find('.table').html());
            $('.pagination').html($(result).find('.pagination').html());
         *//*   $("#loadPartialView").html(response);*//*

            // Change the URL in the browser to reflect the updated page state
            // history.pushState(null, null, "/Admin/UserpageInAdmin?pageindex=" + pageIndex);

        }
    })
}*/