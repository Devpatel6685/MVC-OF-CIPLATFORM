

$(document).ready(function () {
    $('#countryId').on('change', function () {
       
        ddlCity = $('#cities');
        $.ajax({
            url: '/Home/City',
            type: 'POST',
            dataType: 'json',
            data: { id: $(this).val() },
            success: function (d) {
                console.log(d);
                ddlCity.empty();
                $('#cities').prepend('<option value="" selected>Select City</option>');

                $.each(d, function (i, data) {
                    ddlCity.append('<option value="' + data.cityId + '" id="' + data.cityId + '">' + data.name + '</option>');
                });
            },
            error: function () {
               /* alert('Error');*/
            }
        });
    });
});

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

/*function saveSkill() {
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
}*/
function saveSkill() {
    console.log("add skills");
    var skillids = [];
    $('.skill-selected div').each(function () {
        skillids.push($(this).attr('id'));
    });
    if ($('#userEditForm').valid()) {
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

function handleSelectedFile(file) {


    var formData = new FormData();
    formData.append("Image", file);
    $.ajax({
        type: 'POST',
        url: '/Home/AddImage',
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            /*alert('success');*/
            window.location = result.redirectUrl;

        }
    });
    // Read the file as a data URL
}
var img1 = $('#model-img');

var img = document.getElementById("profile-photo");
img.src = img1.attr('data-avtar');

$('.img-wrapper').mouseover(function () {
    $('#boot-icon').removeClass("d-none")
})
$('.img-wrapper').mouseout(function () {
    $('#boot-icon').addClass("d-none")
})


document.getElementById("profileimg").onclick = function () {
    /*alert("sucess");*/
    document.getElementById("file-input").click();
}
document.getElementById("boot-icon").onclick = function () {
   /* alert("sucess");*/
    document.getElementById("file-input").click();
}
document.getElementById("file-input").onchange = function () {
   /* alert('inside file');*/
    handleSelectedFile(this.files[0]);
}

function clearform() {
    $('#changePasswordForm')[0].reset();
}