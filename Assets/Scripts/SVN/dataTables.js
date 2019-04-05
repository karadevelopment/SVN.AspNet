function DataTableViewModel2(selector, settings) {
    let vm = this;

    // ------------------------------------------

    vm.columnsVM = ko.observableArray();

    // ------------------------------------------

    vm.init = function () {
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
                    text: x.text,
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
                    settings.ajaxAfter(jqXHR.responseJSON);
                },
            },
            fnRowCallback: (row, data, colIndex) => {
                for (let i = 1; i <= settings.columns.length; i++) {
                    let column = settings.columns[i - 1];
                    let value = data[column.data];

                    let html = value;

                    if (column.render) {
                        html = $("#" + column.render).html();
                    }

                    let dom = $("td:nth-child(" + i + ")", row);
                    dom.html(html);

                    ko.applyBindings({ item: value }, dom[0]);
                }
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