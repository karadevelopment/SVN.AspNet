window.ajax = {
    length: ko.observable(0)
};

function ajaxRequest(url, request, onSuccess, onError, useQueue) {
    if (useQueue != false) window.ajax.length(window.ajax.length() + 1);
    if (useQueue != false) console.log("window.ajax.length.inc", window.ajax.length());

    $.ajax({
        type: "POST",
        url: url,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(request || {}),
        dataType: "json",
        success: response => {
            ajaxResponse(response, onSuccess, onError);
            if (useQueue != false) window.ajax.length(window.ajax.length() - 1);
            if (useQueue != false) console.log("window.ajax.length.dec", window.ajax.length());
        },
        error: (xhr, status, error) => {
            showError(error);
            if (useQueue != false) window.ajax.length(window.ajax.length() - 1);
            if (useQueue != false) console.log("window.ajax.length.dec", window.ajax.length());
        }
    });
}

function ajaxResponse(response, onSuccess, onError) {
    if (response.url) {
        return navigate(response.url);
    }

    if (response.success) {
        if (onSuccess) onSuccess(response.data);
    }
    else {
        if (onError) onError(response.data);
    }

    if (response.messages) {
        $(response.messages).each((i, x) => {
            showMessage(x.content, x.type);
        });
    }
}