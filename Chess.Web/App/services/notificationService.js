﻿app.factory('notificationService', function () {
    toastr.options = {
        closeButton: true,
        debug: false,
        onclick: null,
        showDuration: "300",
        hideDuration: "1000",
        timeOut: "3600",
        extendedTimeOut: "3600",
        showEasing: "swing",
        hideEasing: "linear",
        showMethod: "fadeIn",
        hideMethod: "fadeOut",
        tapToDismiss: true
    };

    return {
        success: function (text) {
            toastr.success(text, "Success");
        },
        error: function (text) {
            toastr.error(text, "Error");
        },
        info: function (text) {
            toastr.info(text, "Info");
        },
        warning: function (text) {
            toastr.warning(text, "Warning");
        }
    };
});