function DataTableViewModel(selector, settings) {
    let vm = this;

    // ------------------------------------------

    vm.init = function () {
        vm.initColumns();
        vm.initDataTable();
    };

    vm.initColumns = function () {
        let html = "";

        html += "<thead>";
        html += "<tr>";
        for (let key in settings.columns) {
            html += "<th>" + settings.columns[key].text + "</th>";
        }
        html += "</tr>";
        html += "</thead>";

        $(selector).html(html);
    };

    vm.initDataTable = function () {
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
            searching: false,
            bLengthChange: false,
            bInfo: true,
        });
    };

    // ------------------------------------------

    vm.init();
}