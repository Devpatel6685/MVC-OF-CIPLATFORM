

$('.nav-link').each(function () {
    $(this).parent().removeClass('bg-light');
    $(this).css('color', 'white');
});
$('.nav-link.skill').parent().addClass('bg-light');
$('.nav-link.skill ').css('color', 'orange');



$(document).on('click', '.skill li', function (e) {
    e.preventDefault();
    $('.skill li').each(function () {
        $(this).removeClass('skiactive');
    })
    $(this).addClass('skiactive');
    filterskills();
});
function filterskills() {

    var pageIndex = $('.skill .skiactive a').attr('id');
    var keyword = $('#skillsearch').val();
    $.ajax({
        url: "/Admin/Skill",
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

    $('#skillsearch').keyup(function () {
        alert('hi');
        $('.pagination .skiactive').removeClass('skiactive');
        filterSearch();

    });
});

function filterSearch() {
    var keyword = $('#skillsearch').val();

    $.ajax({
        url: "/Admin/Skill",
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

function showModal(skillid) {
    Swal.fire({
        title: 'Are you sure?',
        text: "This Theme will be de-activated",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            DeleteSkill(skillid);
        }
    })
}
function DeleteSkill(skillid) {
    alert("delete theme called");
    $.ajax({
        url: '/admin/DeleteSkill',
        type: 'GET',
        data: {
            skillId: skillid
        },
        success: function (result) {
            alert('result called');
            console.log(result);
            if (result == true) {
                Swal.fire({
                    icon: 'success',
                    title: 'Deleted',
                    text: 'Your skill is been deleted',
                    footer: '<a href="">You cannot de-activate it</a>'
                }).then(() => {
                    window.location = "/Admin/Skill/"
                })

            }
            else if (result == false) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'This Skill is already used by Mission or User',
                    footer: '<a href="">You cannot de-activate it</a>'
                })
            }
        }
    });
}
