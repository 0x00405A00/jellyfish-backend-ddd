
function renderImage(canvasId, imageSource, renderSizeHeight, renderSizeWidth) {
    var image = new Image();
    image.src = imageSource;

    var canvas = document.getElementById(canvasId);
    if (canvas == null) {
        return;
    }
    var ctx = canvas.getContext('2d');

    image.onload = function () {
        ctx.drawImage(image, 0, 0, renderSizeHeight, renderSizeWidth);
    };
};
function setStyle(elementId, style) {

    var element = document.getElementById(elementId);
    if (element) {
        element.style = style;
    }
};