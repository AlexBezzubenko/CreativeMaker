//http://www.goat1000.com/tagcanvas.php
$(document).ready(function () {
    if (!$('#myCanvas').tagcanvas({
        textColour: '#ffffff',
        outlineThickness: 1,
        maxSpeed: 0.1,
        depth: 0.75,
        clickToFront: 1000
    })) {
        // TagCanvas failed to load
        $('#myCanvasContainer').hide();
    }
});