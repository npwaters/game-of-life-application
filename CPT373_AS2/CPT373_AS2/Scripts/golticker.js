$(function () {
    // Reference the auto-generated proxy for the hub.
    var gol = $.connection.golTicker;
    game = @Html.Raw(Json.Encode(Model));



    $.extend(gol.client, {
        addTurnToPage: function (cells) {
            // Add the updated cells to the page.
            $('#cells').html(htmlEncode(cells));
        },

        updateGame: function (currentGame) {
            console.log(currentGame.Name);
            var g = currentGame;
            game = currentGame;
        },
                
        UpdateStoppedSessionGame: function(currentGame){
            var url = '@Url.Action("UpdateStoppedSessionGame")';
            $.post(url, { game: currentGame});
        },
    });


    // Start the connection.
    $.connection.hub.logging = true;
    $.connection.hub.start()
        .done(function () {
            $('#play').click(function () {
                // Call the PlayActiveGame method on the hub.
                // http://forums.asp.net/t/2027743.aspx?How+to+pass+JSON+back+and+forth+from+signalr+hub+js+code
                gol.server.playActiveGame(game);
            });
            $('#stop').click(function () {
                gol.server.stopActiveGame();
            });
            $('#save').click(function () {
                SaveGame(game);
            });
        });
});

// html-encode cells for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<pre style="display: inline-block" />').html(value);
    return encodedValue;
}

function SaveGame(game){
    var url = '@Url.Action("SaveGame")';
    $.post(url, { game: game});
}