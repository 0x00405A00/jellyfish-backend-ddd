let netAssemblyName = 'WebFrontEnd';

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
function getT(elementId, style) {

    var element = document.getElementById(elementId);
    if (element) {
        element.style = style;
    }
};
function addClassByXPath(xpath, className) {
    var result = document.evaluate(xpath, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null);
    if (result && result.singleNodeValue) {
        result.singleNodeValue.classList.add(className);
        return true;
    }

    console.error('Element not found for given XPath:', xpath);
    return false;
}
function addAttributeByXPath(xpath, attribute, value) {
    var result = document.evaluate(xpath, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null);
    if (result && result.singleNodeValue) {
        result.singleNodeValue.setAttribute(attribute, value);
        return true;
    }

    console.error('Element not found for given XPath:', xpath);
    return false;
}
function getElementByXPath(xpath) {
    var result = document.evaluate(xpath, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null);
    return JSON.stringify(result);
}
function dateNow() {
    var result = Date.now();
    return result.toString();
}
function userListTableLoaded() {
    try {
        DotNet.invokeMethodAsync(netAssemblyName, 'ReturnArrayAsync')
            .then(data => {
                console.log(data);
            });
    }
    catch (err) {
        console.log(err);
    }

    console.log("userListTableLoaded executed");
}