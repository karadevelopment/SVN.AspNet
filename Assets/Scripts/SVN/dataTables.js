function DataTableViewModel(selector, settings) {
    let vm = this;

    // ------------------------------------------

    vm.init = function () {
        $(selector).DataTable({
            columns: settings.columns,
            ajax: {
                type: "POST",
                url: settings.ajaxUrl,
                contentType: "application/json",
                data: request => {
                    settings.ajaxBefore(request);
                    return JSON.stringify(request);
                },
                beforeSend: (jqXHR, settings) => {
                },
                complete: (jqXHR, textStatus) => {
                    settings.ajaxAfter(jqXHR.responseJSON);
                },
            },
            processing: true,
            serverSide: true,
        });
    };

    // ------------------------------------------

    vm.init();
}