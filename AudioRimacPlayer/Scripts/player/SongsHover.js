function SongsHover() {

$(".song").hover(
    function () {

        $(this).css({ "background-color": "#5d5c5c", "border-bottom": "2px solid white" });
        $(this).children(".song-date").find(".song-date-title").css({ "font-size": "20px" });
        $(this).children(".song-date").find(".song-date-artist")
            .css({ "font-size": "21px", "color": "#1073fc" });
        $(this).children(".song-image").find("img")
            .css({ "margin-left": "3px", "margin-bottom": "3px", "margin-top": "0px" });
        $(this).children(".btn-undo").show();

        

    },
    function () {
        $(this).css({ "background-color": "#323131", "border-bottom": "2px solid #979797" });
        $(this).children(".song-date").find(".song-date-title").css({ "font-size": "19px" });
        $(this).children(".song-date").find(".song-date-artist")
            .css({ "font-size": "20px", "color": "#4694ff" });
        $(this).children(".song-image").find("img")
            .css({ "margin-left": "0px", "margin-bottom": "0px", "margin-top": "3px" });
        $(this).children(".btn-undo").hide();

        


    }
);

}
$(document).ready(function () {
    $(".btn-undo").hide();
    SongsHover();

});
$(document).ajaxStop(function () {
    $(".btn-undo").hide();
    SongsHover();

});

$(document).ajaxComplete(function () {
    $(".btn-undo").hide();
    SongsHover();
});