
        var ajaxLb = "http://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js";

        $.getScript(ajaxLb)
            .done(function(script, textStatus) {
                console.log(textStatus);
                console.log("Ajax library was loaded");

            })
            .fail(function(jqxhr, settings, exception) {
                alert("The ajax library wasn't loaded");
            });


        var ajaxHighlight = "http://cdnjs.cloudflare.com/ajax/libs/highlight.js/8.6/highlight.min.js";

        $.getScript(ajaxHighlight)
            .done(function(script, textStatus) {
                console.log(textStatus);
                console.log("Ajax Highlight library was loaded");

            })
            .fail(function(jqxhr, settings, exception) {
                alert("The ajax library wasn't loaded");
            });

        var youTube = "https://www.youtube.com/iframe_api";

        $.getScript(youTube)
            .done(function(script, textStatus) {
                console.log(textStatus);
                console.log("YouTube library was loaded");

            })
            .fail(function(jqxhr, settings, exception) {
                alert("YouTube  library wasn't loaded");
            });

        var jqUI = "http://localhost:6464/Scripts/jquery-ui-1.12.1.js";

        $.getScript(jqUI)
            .done(function(script, textStatus) {
                console.log(textStatus);
                console.log("JQ UI library was loaded");

            })
            .fail(function(jqxhr, settings, exception) {
                alert("JQ UI library wasn't loaded");
            });

        var player,
            time_update_interval = 0;


        function onYouTubeIframeAPIReady() {
            console.log("Youtube Video was launched");
            player = new YT.Player('video-placeholder',
                {
                    width: 1,
                    height: 1,
                    videoId: youtubeVideoUrl,
                    playerVars: {
                        autoplay: 1,
                        color: 'white'

                    },
                    events: {
                        onReady: initialize
                    }
                });
        }

        var playerFunctions = "http://localhost:6464/Scripts/player/player-functions.js";

        $.getScript(playerFunctions)
            .done(function(script, textStatus) {
                console.log(textStatus);
                console.log("Player Functions  was loaded");

            })
            .fail(function(jqxhr, settings, exception) {
                alert("Player Functions wasn't loaded");
            });


        var tutorialzine = "http://cdn.tutorialzine.com/misc/enhance/v3.js";

        $.getScript(tutorialzine)
            .done(function(script, textStatus) {
                console.log(textStatus);
                console.log("Tutorialzine library was loaded");

            })
            .fail(function(jqxhr, settings, exception) {
                alert("Tutorialzine library wasn't loaded");
            });


        var playerAnimations = "http://localhost:6464/Scripts/player/player-animations.js";

        $.getScript(playerAnimations)
            .done(function(script, textStatus) {
                console.log(textStatus);
                console.log("Player Animations library was loaded");

            })
            .fail(function(jqxhr, settings, exception) {
                alert("Player Animations library wasn't loaded");
            });

        var appendStylesJS = "http://localhost:6464/Scripts/player/AppendStylesJS.js";

        $.getScript(appendStylesJS)
            .done(function (script, textStatus) {
                console.log(textStatus);
                console.log("Append Styles JS  was loaded");

            })
            .fail(function (jqxhr, settings, exception) {
                alert("Append Styles JS wasn't loaded");
            });

