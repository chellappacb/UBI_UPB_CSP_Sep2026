
$(document).ready(function () {

    $('#cookiecontainer').slideDown('slow');
    $('#cookiecontainer').css('display', 'block');

    $("div.alert-cookie").on("click", "button.close", function () {
        $(this).parents().find('.alert-cookie').animate({ opacity: 0 }, 100).hide('slow');
        $('div.alert-cookie').css('display', 'block');
        });

//    $("div.alert-cookie").on("click", "button.close", function () {
//        $(this).parents().find('.alert-cookie').animate({ opacity: 0 }, 100).hide('slow');
//    });

})



