function DataTableViewModel(selector, settings) {
    let vm = this;

    // ------------------------------------------

    vm.columnsVM = ko.observableArray();

    // ------------------------------------------

    vm.init = function () {
        settings.binding = settings.binding || function (x) { return x; };
        settings.columns.data = settings.columns.data || "id";
        settings.columns.text = settings.columns.text || "";
        settings.columns.orderable = settings.columns.orderable || false;

        vm.initColumns();
        vm.initDataTable();
    };

    vm.initColumns = function () {
        let html = "";

        html += "<thead>";
        html += "<tr>";
        for (let i = 1; i <= settings.columns.length; i++) {
            let column = settings.columns[i - 1];

            html += "<th>";
            html += column.text;
            html += "</th>";
        }
        html += "</tr>";
        html += "</thead>";

        $("#" + selector).html(html);
    };

    vm.initDataTable = function () {
        $("#" + selector).DataTable({
            columns: settings.columns.select(x => {
                return {
                    data: x.data,
                    defaultContent: "",
                    text: x.text,
                    orderable: x.orderable,
                };
            }),
            ajax: {
                type: "POST",
                url: settings.ajaxUrl,
                contentType: "application/json",
                data: request => {
                    settings.ajaxBefore(request);
                    return JSON.stringify(request);
                },
                beforeSend: (jqXHR, settings) => {
                    vm.columnsVM([]);
                },
                complete: (jqXHR, textStatus) => {
                    let data = jqXHR.responseJSON;
                    settings.ajaxAfter(data);
                },
            },
            fnRowCallback: (row, data, colIndex) => {
                for (let i = 1; i <= settings.columns.length; i++) {
                    let column = settings.columns[i - 1];
                    let value = data[column.data];

                    let html = value;

                    if (column.template) {
                        html = $("#" + column.template).html();
                    }

                    let dom = $("td:nth-child(" + i + ")", row);
                    dom.html(html);
                }
                ko.applyBindings(settings.binding(data), row);
            },
            fnDrawCallback: (oSettings) => {
                vm.columnsVM.valueHasMutated();
            },
            processing: true,
            serverSide: true,
            searching: false,
            bLengthChange: false,
            bInfo: false,
        });
    };

    // ------------------------------------------

    vm.init();
}