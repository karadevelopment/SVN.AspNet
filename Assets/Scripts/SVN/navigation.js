function navigate(url) {
    window.location.href = url;
}

function pushId(id) {
    window.route.id = id;
    window.history.pushState("", "", "/" + window.route.controller + "/" + window.route.action + "/" + window.route.id);
}