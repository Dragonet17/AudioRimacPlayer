$(document).ready(function () {
    $("html, body").delay(2000).animate({ scrollTop: '50px' }, 2000);
    //$("body").animate({ backgroundColor: '#2B353E' }, 3000);
    $("h2").css({ "color": "#1073fc" });
    $("img:first").fadeTo(5000, 1);
    $("img:first").css({ borderColor: "black", borderStyle: "solid" }).animate({ borderWidth: '5px' }, 1000, 'linear');
    $(".player-buttons").animate({ backgroundColor: '#717070' }, 6000);
    $("#play-button,#pause-button").hover(
        function () {
            $(".player-buttons").animate({ backgroundColor: 'white' }, 500);

        },
        function () {
            $(".player-buttons").animate({ backgroundColor: '#717070' }, 500);

        }
    );

    $("#pause-button").click(function () {
        $(this).toggleClass("display-none");
        $("#play-button").toggleClass("display-none");
    });
    $("#play-button").click(function () {
        $(this).toggleClass("display-none");
        $("#pause-button").toggleClass("display-none");
    });
    $("#youtube-btn-black").click(function () {
        $(this).toggleClass("display-none");
        $("#youtube-btn-red").toggleClass("display-none");
        $("#yt-video-and-covert-img").animate({ width: '6px' }, 1000);
        setTimeout(function () {
            $("#yt-video-and-covert-img").animate({ height: '150px' }, 1000);
        }, 1000);
        setTimeout(function () {
            $("#yt-video-and-covert-img").animate({ width: '300px' }, 1000);
            $("#covert-img").toggleClass("display-none");
            $("#video-placeholder").attr({ width: "300px", height: "150px;" });

        }, 2000);
        setTimeout(function () {
            $("#yt-video-and-covert-img").css({ "width": "302px", "height": "152px", "border": "1px solid #575353" });

        }, 3000);
        //$("#player-background").stop().animate({
        //    'background-position-x': '0px',
        //    'background-position-y': '350px'
        //}, 2500, 'linear');
    });
    $("#youtube-btn-red").click(function () {
        $(this).toggleClass("display-none");
        $("#youtube-btn-black").toggleClass("display-none");
        $("#yt-video-and-covert-img").animate({ width: '6px' }, 1000);
        setTimeout(function () {
            $("#yt-video-and-covert-img").animate({ height: '303px' }, 1000);
            $("#covert-img").toggleClass("display-none");
            $("#video-placeholder").attr({ width: "0.50px", height: "0.5px;" });
        }, 1000);
        setTimeout(function () {
            $("#yt-video-and-covert-img").animate({ width: '303px' }, 1000);
        }, 2000);
        setTimeout(function () {
            $("#yt-video-and-covert-img").css({ "width": "303px", "height": "303px", "border": "1px solid #575353" });

        }, 3000);
        //$("#player-background").stop().animate({
        //    'background-position-x': '0px',
        //    'background-position-y': '250px'
        //}, 2500, 'linear');
    });


});