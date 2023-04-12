getCountry();
if ($('#cities').val() == null) {
    $('#cities').attr('disabled', true);
}

$('#countryId').change(function () {
    var id = $('#cities').val();
    $('#cities').empty();
    $('#cities').append('<option>Select City</option>')
    $('#cities').attr('disabled', false);
    var countryId = $('#countryId').val();
    $.ajax({
        url: '/Home/City',
        data: {
            id: countryId,
        },
        type: "POST",
        success: function (result) {
            $.each(result, function (i, data) {
                if (data.cityId != id) {
                    $('#cities').append('<option value=' + data.cityId + '>' + data.name + '</option>')
                }
            })
        }
    })
})

function getCountry() {
    var id = $('#countryId').val();
    $.ajax({
        url: '/Mission/Country',
        type: 'POST',
        success: function (result) {
            $.each(result, function (i, data) {
                if (data.countryId != id) {
                    $('#countryId').append('<option value=' + data.countryId + '>' + data.name + '</option>');
                }

            })
        },
    })
}

$('.available-skills div').click(function () {
    $(this).hasClass("bg-light") ? $(this).removeClass("bg-light") : $(this).addClass("bg-light");
})

function selectSkill() {
    $('.selected-skills').empty();
    $('.available-skills div').each(function () {
        if ($(this).hasClass("bg-light")) {
            var div = $(this).clone();
            div.removeClass('bg-light');
            $('.selected-skills').append(div);
        }
    })
}

$(document).on('click', '.selected-skills div', function () {
    $(this).hasClass("bg-light") ? $(this).removeClass("bg-light") : $(this).addClass("bg-light");
});

function deselectSkill() {
    $('.selected-skills div').each(function () {
        if ($(this).hasClass('bg-light')) {
            var skillid = $(this).attr('id');
            $('.available-skills').find('#' + skillid).removeClass('bg-light');
            $(this).remove();

        }
    })
}

function addskill() {
    $('.skill-selected').empty();
    $('.selected-skills div').each(function () {
        $('.skill-selected').append($(this).clone());
    })
}

function saveSkill() {
    console.log("add skills");
    var skillids = [];
    $('.skill-selected div').each(function () {
        skillids.push($(this).attr('id'));
    });
    $.ajax({
        url: "/home/addskill",
        type: "POST",
        data: {
            skillids: skillids,
        },
        success: function (result) {
            console.log("profile updated successfully");
        }
    })
}

function password(passw) {
    console.log(passw);
    var icon = $(passw + ' .bi')
    var pass = $(passw + ' .form-control')
    if (icon.hasClass('bi-eye')) {
        icon.removeClass('bi-eye')
        icon.addClass('bi-eye-slash')
        pass.attr('type', 'text')
    } else {
        icon.addClass('bi-eye')
        icon.removeClass('bi-eye-slash')
        pass.attr('type', 'password')
    }
}