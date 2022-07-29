$("#login-link").click(function() {
    $("#login-form").delay(105).fadeIn(100);
    $("#register-form").fadeOut(100);
    $("#login-link").css("color", "#fc627c");
    $("#login-link").css("border-bottom", "solid 3px #fc627c");
    $("#register-link").css("color", "#4E5166");
    $("#register-link").css("border-bottom", "none");
    $(this).addClass("active");
});

$("#register-link").click(function() {
    $("#register-form").delay(105).fadeIn(100);
    $("#login-form").fadeOut(100);
    $("#login-link").removeClass("active");
    $("#login-link").css("color", "#4E5166");
    $("#login-link").css("border-bottom", "none");
    $("#register-link").css("color", "#fc627c");
    $("#register-link").css("border-bottom", "solid 3px #fc627c");
    $(this).addClass("active");
});

$("#phone-submit").click(function() {
    if($("#PhonenumberId").val().length == 11){
        $("#formR2").delay(105).fadeIn(100);
        $("#formR1").fadeOut(100);
        $("#formR1").css("display", "none");
        $("#formR2").css("display", "");
    }
});

$("#validPhone").click(function() {
    if($("#PhonenumberId").val().length == 11){
        $("#formR3").delay(105).fadeIn(100);
        $("#formR2").fadeOut(100);
        $("#formR2").css("display", "none");
        $("#register-submit").css("display", "");
        $("#formR3").css("display", "");
    }
});