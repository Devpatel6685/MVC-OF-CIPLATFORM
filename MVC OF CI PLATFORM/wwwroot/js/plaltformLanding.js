
var pageLoaded = false;
$(document).ready(function () {

    if (pageLoaded) return
    pageLoaded = true;
    getCountry();
    getThemes();
    getSkills();
    $('#city').attr('disabled', true);
    $('#city').parent().find('a').attr('disabled', true);
    $('#country').change(function () {
        $('#city').attr('disabled', false);
        $('#city').parent().find('a').attr('disabled', false);
        var link = "/Mission/City?id=";

        var ids = []
        $('.country:checkbox:checked').each(function () {
            link = link + ($(this).attr("id")) + '&id=';
        });

        $('#city').empty();
        $.ajax({
            url: link,
            data: ids,
            method: "POST",
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#city').append('<div class="form-check ms-3"><input class="form-check-input checkbox city" type="checkbox" value="' + data.name + '" id=' + data.cityId + '><label class="form-check-label" for=' + data.id + '>' + data.name + '</label></div>')
                })
            }
        })
        $('.pagination .active').removeClass('active');
        $('.pagination .active').find('#1').parent().addClass('active');
        filterMission()
    })


    $('#city').change(function () {
        $('.pagination .active').removeClass('active');
        $('.pagination .active').find('#1').parent().addClass('active');
        filterMission()
    });
    $('#theme').change(function () {
        $('.pagination .active').removeClass('active');
        $('.pagination .active').find('#1').parent().addClass('active');
        filterMission()
    });
    $('#skill').change(function () {
        $('.pagination .active').removeClass('active');
        $('.pagination .active').find('#1').parent().addClass('active');
        filterMission()
    });
    $('#sort').change(function () {
        $('.pagination .active').removeClass('active');
        $('.pagination .active').find('#1').parent().addClass('active');
        filterMission()

    });
    $('#search-input').keyup(function () {
        filterMission()
    });

    dispalyCout();

});

function filterMission() {

    var country = [];
    $('.filters-section').empty()
    if ($('.filters-section').empty()) {
        $('.filters-section').append('<button class="filter-list ps-3  pe-3 me-2 text-danger" id="clear">  clear  </button>')
    }
    $('.country:checkbox:checked').each(function () {
        country.push($(this).attr("id"));
        $('.filters-section').prepend('<span class="filter-list ps-3  pe-3 me-2">' + $(this).val() + '&nbsp; <button class="border-0 fs-4 cross" onclick="cross(' + "country_" + $(this).attr("id") + ');" style="background-color:white" id=' + "country_" + $(this).attr("id") + ' >  &times;</button> </span>')
    })

    var city = [];
    $('.city:checkbox:checked').each(function () {
        city.push($(this).attr("id"));
        $('.filters-section').prepend('<span class="filter-list ps-3  pe-3 me-2">' + $(this).val() + '&nbsp; <button class="border-0 fs-4 cross" onclick="cross(city_' + $(this).attr("id") + ');" style="background-color:white" id=' + "city_" + $(this).attr("id") + ' >  &times;</button> </span>')
    })

    var theme = [];
    $('.theme:checkbox:checked').each(function () {
        theme.push($(this).attr("id"));
        $('.filters-section').prepend('<span class="filter-list ps-3  pe-3 me-2">' + $(this).val() + '&nbsp; <button class="border-0 fs-4 cross" onclick="cross(theme_' + $(this).attr("id") + ');" style="background-color:white" id=' + "theme_" + $(this).attr("id") + ' >  &times;</button> </span>')
    })

    var skill = [];
    $('.skill:checkbox:checked').each(function () {
        skill.push($(this).attr("id"));
        $('.filters-section').prepend('<span class="filter-list ps-3  pe-3 me-2">' + $(this).val() + '&nbsp; <button class="border-0 fs-4 cross " onclick="cross(skill_' + $(this).attr("id") + ';)" style="background-color:white" id=' + "skill_" + $(this).attr("id") + ' >  &times;</button> </span>')
    })

    if ($('.filters-section .filter-list').length == 1) {
        $('.filters-section').empty();
    }
    var sid = $('#sort').val();
    var keyword = $('#search-input').val();
    var pageIndex = $('.pagination .active a').attr('id');
    var $gridCont = $('.grid-container');
    var list = $gridCont.hasClass('list-view') ? true : false;
    $.ajax({
        url: "Mission/platformLanding",
        type: "POST",
        data: {
            countryids: country,
            cityids: city,
            themeids: theme,
            skillids: skill,
            sortId: sid,
            SearchInputdata: keyword,
            pageIndex: pageIndex

        },
        success: function (response) {
            $result = $(response);
            var filter = $result.find('#result').html();
            console.log(filter);
            $('#result').html(filter);
            dispalyCout();
            $('.page').html($(response).find('.page').html());
            if (list) {
                $('.btn-list').click();
            }

        },
        Error: function () {
            alert('error in skill');
        }
    })

}
$(document).on('click', '#clear', function () {
    console.log("clear")
    $('.country:checkbox:checked').each(function () {
        $(this).prop('checked', false)
    })
    $('.city:checkbox:checked').each(function () {
        $(this).prop('checked', false)
    })
    $('.theme:checkbox:checked').each(function () {
        $(this).prop('checked', false)
    })
    $('.skill:checkbox:checked').each(function () {
        $(this).prop('checked', false)
    })
    $('#search-input').val('');
    $('.filters-section').empty();
    filterMission();
});
function cross(btnid) {
    var lst = $(btnid).attr("id").split("_");
    $('#' + lst[0] + ' #' + lst[1]).prop("checked", false);
    $('.pagination .active').removeClass('active');
    $('.pagination .active').find('#1').parent().addClass('active');
    filterMission();
}

function getCountry() {
    $.ajax({
        url: '/Mission/Country',
        success: function (result) {
            $.each(result, function (i, data) {
                $('#country').append('<div class="form-check ms-3"><input class="form-check-input checkbox country" type="checkbox" value="' + data.name + '" id=' + data.countryId + '><label class="form-check-label" for=' + data.countryId + '>' + data.name + '</label></div>')
            })
        }
    })
}

function getThemes() {
    $.ajax({
        url: '/Mission/Theme',
        success: function (result) {
            $.each(result, function (i, data) {
                $('#theme').append('<div class="form-check ms-3"><input class="form-check-input checkbox theme" type="checkbox" value="' + data.title + '" id=' + data.missionThemeId + '><label class="form-check-label" for=' + data.missionThemeId + '>' + data.title + '</label></div>')
            })
        }
    })
}

function getSkills() {
    $.ajax({
        url: '/Mission/Skill',
        success: function (result) {
            $.each(result, function (i, data) {
                $('#skill').append('<div class="form-check ms-3"><input class="form-check-input checkbox skill" type="checkbox" value="' + data.skillName + '" id=' + data.skillId + '><label class="form-check-label" for=' + data.ski + '>' + data.skillName + '</label></div>')
            })
        }
    })
}



function showList(e) {
    var $gridCont = $('.grid-container');
    e.preventDefault();
    $gridCont.hasClass('list-view') ? $gridCont.removeClass('list-view') : $gridCont.addClass('list-view');
}

function gridList(e) {
    var $gridCont = $('.grid-container')
    // e.preventDefault();
    $gridCont.removeClass('list-view');
}

$(document).on('click', '.btn-grid', gridList);
$(document).on('click', '.btn-list', showList);


function showList(e) {
    var $gridCont = $('.grid-container');
    var $cardstart = $('.card-start');
    var $grid_element = $('.grid-element');
    e.preventDefault();
    $gridCont.addClass('list-view');
    //$gridCont.hasClass('list-view') ? $gridCont.removeClass('list-view') : $gridCont.addClass('list-view');
    $cardstart.removeClass('d-none');
    $cardstart.addClass('d-flex');
    $grid_element.removeClass('d-flex');
    $grid_element.addClass('d-none');
}
function gridList(e) {
    var $gridCont = $('.grid-container')
    var $cardstart = $('.card-start');
    var $grid_element = $('.grid-element');
    // e.preventDefault();
    $gridCont.removeClass('list-view');
    $cardstart.addClass('d-flex');
    $cardstart.addClass('d-none');
    $grid_element.removeClass('d-none');
    $grid_element.addClass('d-flex');
}

$(document).on('click', '.btn-grid', gridList);
$(document).on('click', '.btn-list', showList);




$(document).on('click', '.pagination li', function (e) {
   
    e.preventDefault();
    $('.pagination li').each(function () {
        $(this).removeClass('active');
    })
    $(this).addClass('active');
    console.log($(this).find('a').attr('id'));
    filterMission();
})


var Model = $('#model')
function addFavourite(missionId) {
    if (!Model.attr('data-userid')) {
        showModal();
    }
    else {

        var button = $('.favourite')

        $.ajax({
            url: "/Mission/favbtnlandingpage",
            type: "POST",
            data: {
                userId: Model.attr('data-userid'),
                MissionId: missionId,
            },
            success: function (result) {
                $(`.favbtnlanding-${missionId}`).html($(result).find(`.favbtnlanding-${missionId}`).html());
                console.log(result);
            }
        })
        filterMission()
    }
}


function showModal() {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Please Login!',
    })
}

function dispalyCout() {
    let inputField = document.getElementById("displayOnExplore");
    if (inputField == null || inputField == undefined) return;
    let spanField = document.getElementById("displayOnExploreOnThisFeild");
    let inputValue = inputField.value;
    console.log(inputValue);
    spanField.textContent = inputValue;
}

function recommend(mId) {
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
            url: "/Mission/recommend",
            data: {
                userIds: userids,
                missionid: mId,
            },
            success: function (result) {
                toastr.success('Invite Link Sent');

            }
        })
    }
}

function getUsers(mId) {
    missionId = mId;
    if (!Model.attr('data-userid')) {
        showModal();
    } else {
        var div = $('.modal-body');
        $.ajax({
            type: "GET",
            url: "/Mission/User",
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


