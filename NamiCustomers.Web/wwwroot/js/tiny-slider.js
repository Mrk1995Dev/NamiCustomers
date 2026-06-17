window.tinySliders = {};

window.initTinySliderOne = function (elementId) {
    if (window.tinySliders[elementId]) {
        window.tinySliders[elementId].destroy();
    }
    window.tinySliders[elementId] = tns({
        container: '#' + elementId,
        items: 1,
        slideBy: 'page',
        autoplay: true,
        mouseDrag: true,
        swipeAngle: false,
        gutter: 15,
        nav: true,
        controls: false
    });
};

window.destroyTinySlider = function (elementId) {
    if (window.tinySliders[elementId]) {
        window.tinySliders[elementId].destroy();
        delete window.tinySliders[elementId];
    }
};