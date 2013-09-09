$(document).ready(function () {

    var window = $("#window"),
        undo = $("#undo")
                .bind("click", function () {
                    window.data("kendoWindow").open();
                    undo.hide();
                });

    var onClose = function () {
        undo.show();
    };

    if (!window.data("kendoWindow")) {
        window.kendoWindow({
            width: "600px",
            title: "Are you sure?",
            actions: [
                "Pin",
                "Minimize",
                "Maximize",
                "Close"
            ],
            close: onClose
        });
    }
});