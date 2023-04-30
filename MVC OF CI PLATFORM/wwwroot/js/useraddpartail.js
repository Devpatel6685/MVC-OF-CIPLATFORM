$('.img-wrapper').mouseover(function () {
    $('#boot-icon').removeClass("d-none");
})
$('.img-wrapper').mouseout(function () {
    $('#boot-icon').addClass("d-none");
})

getcountry();
$(document).ready(function () {
    $('#usercountryselect').on('change', function () {
        var countryId = $('#usercountryselect').val();
        console.log("country id", countryId);
        $.ajax({
            url: "/Admin/City",
            type: "GET",
            data: {
                countryid: countryId
            },
            success: function (result) {
                $('#usercityselect').empty();
                if (result.length === 0) {
                    $('#usercityselect').html('<option>No cities Found</option>');
                } else {
                    $('#usercityselect').prepend('<option value="" selected>Select City</option>');
                    $.each(result, function (i, data) {
                        /* if (missionid != data.missionId) {*/
                        $('#usercityselect').append('<option value="' + data.cityId + '" id="' + data.cityId + '">' + data.name + '</option>');
                        /*}*/

                    })
                }
            }
        })
    });
});
function getcountry() {
    var countryid = $('#usercountryselect').val();
    console.log("id", countryid);
    $.ajax({
        url: "/Admin/Country",
        type: "GET",

        success: function (result) {

            if (result.length === 0) {
                $('#usercountryselect').html('<option>No countries selected</option>');
            } else {
                $.each(result, function (i, data) {
                    if (countryid == "defselect") {
                        $('#usercountryselect').append('<option value="' + data.countryId + '" id="' + data.countryId + '">' + data.name + '</option>');
                    }

                })
            }
        }
    })
}
document.getElementById("profileimg").onclick = function () {

    document.getElementById("file-input").click();

}
document.getElementById("boot-icon").onclick = function () {

    document.getElementById("file-input").click();

}
document.getElementById("file-input").onchange = function (event) {

    const files = event.target.files;
    handleFiles(files);
};


const uploadedFiles = new Set();
function handleFiles(files) {
    $('#showImage').empty();
    const file = files[0];
    console.log(files);
    for (let i = 0; i < files.length; i++) {
        const file = files[i];
        if (!file.type.startsWith("image/")) continue;
        if (uploadedFiles.has(file.name)) {
            alert(`File "${file.name}" has already been uploaded.`);
            continue;
        }
        const reader = new FileReader();
        reader.onload = function (event) {
            alert('reader called');
            $('#profileimg').attr('src', event.target.result);
        }
        reader.readAsDataURL(file);
        uploadedFiles.add(file.name);
    }
}