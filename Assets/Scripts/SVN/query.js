setQuery = function () {
    let properties = [];
    for (let key in window.query) {
        properties.push(key + '=' + window.query[key]);
    }
    if (0 < properties.length) {
        window.history.pushState('', '', '?' + properties.join('&'));
    }
};
setQuery();