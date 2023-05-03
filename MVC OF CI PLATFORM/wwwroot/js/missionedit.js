

$('#countryselect').on('change', function () {
    var countryId = $("#countryselect").val();
    $.ajax({
        url: "/Admin/City",
        type: "GET",
        data: {
            countryid: countryId
        },
        success: function (result) {
            $('#cityselect').empty();
            if (result.length === 0) {
                $('#cityselect').html('<option>No cities slected</option>');
            } else {
                $('#cityselect').prepend('<option value="" selected>Select City</option>');
                $.each(result, function (i, data) {
                    /* if (missionid != data.missionId) {*/
                    $('#cityselect').append('<option value="' + data.cityId + '" id="' + data.cityId + '">' + data.name + '</option>');
                    /*}*/

                })
            }
        }
    })
});
$('.skillselect:checkbox:checked').each(function () {
/*    alert('span called');
*/    var skillname = $(this).next('label').text();
    var id = $(this).val();
    $('.skill-section').append('<span class="filter-list ps-3 pe-3 me-2 rounded-pill border btn btn-warning text-white">' + skillname + '</span>')
})
if ($('.skill-section .filter-list').length > 0) {
    $('.skill-section').prepend('<span class="filter-list ps-3 pe-3 me-2 p-2 text-dark fw-bold">Selected Skills</span>');
}
function Addskill() {
    $('.skill-section').empty();
    $('.skillselect:checkbox:checked').each(function () {
        var skillname = $(this).next('label').text();
        var id = $(this).val();
        $('.skill-section').append('<span class="filter-list ps-3 pe-3 me-2 rounded-pill border btn btn-warning text-white">' + skillname + '</span>')
    })
    if ($('.skill-section .filter-list').length > 0) {
        $('.skill-section').prepend('<span class="filter-list ps-3 pe-3 me-2 p-2 text-dark fw-bold">Selected Skills</span>');
    }
}

function getThemes() {

    $.ajax({
        url: "/Admin/GetThemes",
        type: "GET",

        success: function (result) {

            if (result.length === 0) {
                $('#themeselect').html('<option>No countries selected</option>');
            } else {
                $.each(result, function (i, data) {

                    $('#themeselect').append('<option value="' + data.missionThemeId + '" id="' + data.missionThemeId + '">' + data.title + '</option>');


                })
            }
        }
    })
}

if ($("#missionhide").val() == 0) {
    getcountry();
    getThemes();
}

function getcountry() {

    $.ajax({
        url: "/Admin/Country",
        type: "GET",

        success: function (result) {

            if (result.length === 0) {
                $('#mission').html('<option>No countries selected</option>');
            } else {
                $.each(result, function (i, data) {

                    $('#countryselect').append('<option value="' + data.countryId + '" id="' + data.countryId + '">' + data.name + '</option>');

                })
            }
        }
    })
}
if ($("#missionhide").val() != 0) {
    Mission();
}
function Mission() {

    var type = $('#avail12').val();
    console.log("type of mission", type);
    if (type == "time") {
        $('#timefields').removeClass('d-none');
        $('#goalfields').addClass('d-none');
    }
    else {
        $('#goalfields').removeClass('d-none');
        $('#timefields').addClass('d-none');
    }
};
$('#sdate').attr("min", $('#rdead').val());
$('#edate').attr("min", $('#sdate').val());
$('#rdead').change(function () {
    
})
$('#sdate').change(function () {
})

$('#Images').change(function () {
    const files = $('#Images').prop('files');
    handleFiles(files);
})
const uploadedFiles = new Set();
function handleFiles(files) {
    $('#showImage').empty();
    console.log(files);
    for (let i = 0; i < files.length; i++) {
        const file = files[i];
        if (!file.type.startsWith("image/")) continue;
        if (uploadedFiles.has(file.name)) {
           /* alert(`File "${file.name}" has already been uploaded.`);*/
            continue;
        }
        uploadedFiles.add(file.name);
        const image = document.createElement("img");
        image.classList.add("image-preview");
        const imageContainer = document.createElement("div");
        imageContainer.classList.add("image-container");
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
            image.src = reader.result;
            imageContainer.appendChild(image);
            $('#showImage').append(imageContainer);
        };
    }
}
/*$('#missionForm').submit(function (event) {
    alert('mission valid');
    var textarea = tinymce.get("storytext").getContent();
    event.preventDefault();
    if ($('#showImage').children().length == 0) {
        $('#imgValidate').text("Select Images")
    } if (textarea == "") {
        $('#textValidate').text("Text is Required");
    }
    if ($('#showDocument').children().length == 0) {
        $('#docValidate').text("Select Images")
    }
    else if ($('#missionForm').valid()) {
        $('#missionForm')[0].submit();
    }
})*/




const input = document.querySelector('#document');

input.addEventListener('change', () => {
    // Clear the contents of the showDocument div
    document.querySelector('#showDocument').innerHTML = '';

    // Loop through each selected file
    for (let i = 0; i < input.files.length; i++) {
        const file = input.files[i];

        // Check if the file is an image
        if (file.type.startsWith('image/')) {
            // Create an image element for the file
            const img = document.createElement('img');
            img.src = URL.createObjectURL(file);

            // Add some styles to the image
            img.style.maxWidth = '100px';
            img.style.maxHeight = '100px';
            img.style.margin = '5px';

            // Add the image element to the showDocument div
            document.querySelector('#showDocument').appendChild(img);
        } else {
            // Create a div element for the file name
            const div = document.createElement('div');
            div.innerText = file.name;

            // Add some styles to the div
            /* div.style.width = '100px';
             div.style.margin = '5px';*/

            // Add the div element to the showDocument div
            document.querySelector('#showDocument').appendChild(div);

        }
    }
});
tinymce.init({
    selector: '#storytext',
    plugins: 'link image code',
    toolbar: 'undo redo | bold italic | fontsizeselect | alignleft aligncenter alignright alignjustify | superscript subscript ',
    height: 300
});

