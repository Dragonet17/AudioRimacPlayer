$("input").focus();

function HideResults() {

    $(".song").hide("blind", 600);
    $("#error").hide("blind", 600);

}

function ShowResults() {
    $(".song").css({ "display": "none" });
    $(".song").slideUp(1);
    $(".song").slideDown(1200);
    $("#error").css({ "display": "none" });

    $("#error").slideUp(1);
    $("#error").slideDown(1200);
    
}

function ShowError() {
    $(".row").slideUp(1);
    $(".row").slideDown(1200);

}

$("#button-song").click(function() {
    if ($("#button-song").hasClass("searching-button")) {
        $('#button-song,#button-artist').toggleClass('searching-button searching-button-click');

    }
});

$("#button-artist").click(function() {
    if ($("#button-artist").hasClass("searching-button")) {
        $('#button-artist,#button-song').toggleClass('searching-button searching-button-click');

    }

});


