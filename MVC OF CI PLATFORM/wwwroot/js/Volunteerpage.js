/*var checkboxes = document.querySelectorAll(".checkbox");


let filtersSection = document.querySelector(".filters-section");

var listArray = [];

var filterList = document.querySelector(".filter-list");

var len = listArray.length;

for (var checkbox of checkboxes) {
    checkbox.addEventListener("click", function () {
        if (this.checked == true) {
            addElement(this, this.value);
        } else {

            removeElement(this.value);
            console.log("unchecked");
        }
    })
}
*/

/*function addElement(current, value) {
    let filtersSection = document.querySelector(".filters-section");

    let createdTag = document.createElement('span');
    createdTag.classList.add('filter-list');
    createdTag.classList.add('ps-3');
    createdTag.classList.add('pe-1');
    createdTag.classList.add('me-2');
    createdTag.innerHTML = value;

    createdTag.setAttribute('id', value);
    let crossButton = document.createElement('button');
    crossButton.classList.add("filter-close-button");
    let cross = '&times;'



    crossButton.addEventListener('click', function () {
        let elementToBeRemoved = document.getElementById(value);

        console.log(elementToBeRemoved);
        console.log(current);
        elementToBeRemoved.remove();

        current.checked = false;




    })

    crossButton.innerHTML = cross;


    // let crossButton = '&times;'

    createdTag.appendChild(crossButton);
    filtersSection.appendChild(createdTag);

}

function removeElement(value) {

    let filtersSection = document.querySelector(".filters-section");

    let elementToBeRemoved = document.getElementById(value);
    filtersSection.removeChild(elementToBeRemoved);

}
*/




/*let slideIndex = 1;
showSlides(slideIndex);

function plusSlides(n) {
    showSlides(slideIndex += n);
}

function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    let i;
    let slides = document.getElementsByClassName("mySlides");

    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    
    slides[slideIndex - 1].style.display = "block";

}*/
var Model = $('#model')
var from = parseInt(Model.attr('data-pagenumber') - 1) * 9 + 1;
var to = $('.recVol .col-4').length;
$('.page-info').html(from + "-" + to + " of " + Model.attr('data-totalitem') + " Recent Volunteers")
$(document).on('click', '.pagination .page-item', function (e) {
    e.preventDefault();
    $.ajax({
        url: "/Mission/volunteerpage",
        data: {
            pageIndex: $(this).find('a').attr("id"),
            id: Model.attr('data-missionid'),
        },
        success: function (result) {
            $('.recVol').html($(result).find('.recVol').html());
            $('.page-info').html(from + "-" + to + " of " + Model.attr('data-totalitem') + " Recent Volunteers")
        }
    })
})

function addcomment(missionid) {
    if (!Model.attr('data-userid')) {
        showModal();
    }
    else {
        var comment = $('#cmt').val();
        if (comment == "") {
            toastr.info("comment can not be empty");
        } else {
            $.ajax({
                url: "/Mission/comments",
                type: "POST",
                data: {
                    userid: Model.attr('data-userid'),
                    Missionid: missionid,
                    comment: comment,
                },
                datatype: "html",
                success: function (result) {
                    console.log(result)
                    $('.comment').html($(result).find('.comment').html());
                    $('#cmt').val('');
                }
            })
        }
    }
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


function applymission(missionid) {
    if (!Model.attr('data-userid')) {
        showModal();
    }
    else {
        $.ajax({
            type: 'POST',
            url: '/Mission/applyMission',
            data: {
                userid: Model.attr('data-userid'),
                Missionid: missionid,
            },
            success: function (result) {
                $('.applybtn').html($(result).find('.applybtn').html());
                toastr.success('successfully applied');
                console.log("applied");
            }

        })
    }
}




function changeRating(starNum, missionId) {
    if (!Model.attr('data-userid')) {
        showModal();
    }
    else {
        var star = document.getElementById(`star-${missionId}-${starNum}`);
        console.log(star)
        var isStarSelected = star.getAttribute("src").endsWith("selected-star.png");
        var ratingstar = 0;
        for (var i = 1; i <= 5; i++) {
            var currentStar = document.getElementById(`star-${missionId}-${i}`);
            if (i === starNum && isStarSelected) {
                currentStar.setAttribute("src", "/Assets/star-empty.png");
            } else if (i <= starNum) {
                currentStar.setAttribute("src", "/Assets/selected-star.png");
                ratingstar = i;
            } else {
                currentStar.setAttribute("src", "/Assets/star-empty.png");
            }
        }
    
    $.ajax({
        url: "/Mission/ratingupdate",
        type: "POST",
        data: {
            missionid: missionId,
            rating: ratingstar,
            userId: Model.attr('data-userid')
        },
        success: function (result) {
            $('.rate').html($(result).find('.rate').html());

        }
    });
    }
}


function addFavorite(missionId) {

    if (!Model.attr('data-userid')) {
        showModal();
    }
    else {
        var button = $('.favorite')
        if (button.hasClass("added")) {
            $('.favorite img').attr("src", "/Assets/heart1.png");
            button.removeClass("added")
        } else {
            button.addClass("added");
            $('.favorite img').attr("src", "/Assets/favorite.png");
        }
        $.ajax({

            url: "/Mission/favroitemission",
            type: "POST",
            data: {
                userID: Model.attr('data-userid'),
                MissionId: missionId
            },

            success: function (result) {
                console.log(result);
            }
        })

    }
}
let slideIndex = 1;
showSlides(slideIndex);

function plusSlides(n) {
    showSlides(slideIndex += n);
}

function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    let i;
    let slides = document.getElementsByClassName("mySlides");

    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }

    slides[slideIndex - 1].style.display = "block";

}

