
function renderImage(canvasId, imageSource) {
    var image = new Image();
    image.src = imageSource;

    var canvas = document.getElementById(canvasId);
    var ctx = canvas.getContext('2d');

    image.onload = function () {
        ctx.drawImage(image, 0, 0, 256, 256);
    };
};