function showSuccess(message) {
    showMessage(message, "success");
}

function showError(message) {
    showMessage(message, "danger");
}

function showWarning(message) {
    showMessage(message, "warning");
}

function showInfo(message) {
    showMessage(message, "info");
}

function showMessage(message, type) {
    var element = $("#message");

    element
        .queue(() => {
            element
                .removeClass()
                .addClass("alert alert-dismissible alert-" + type)
                .html(message)
                .dequeue();
        })
        .fadeIn(1000)
        .delay(4000)
        .fadeOut(1000);
}