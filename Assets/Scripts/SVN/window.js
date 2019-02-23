window.size = {
    width: ko.observable($(window).width()),
    height: ko.observable()
};

$(window).resize(() => {
    window.size.width($(window).width());
    window.size.height($(window).height());
});