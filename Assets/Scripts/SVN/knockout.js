$(document).keydown((event) => {
    if (event.keyCode == 27) {
        $(".modal").modal("hide");
    }
});

ko.bindingHandlers.enterkey = {
    init: (element, valueAccessor, allBindings, viewModel) => {
        var callback = valueAccessor();

        $(element).keypress((event) => {
            var keyCode = event.which ? event.which : event.keyCode;

            if (keyCode == 13) {
                callback.call(viewModel);
                return false;
            }

            return true;
        });
    }
};