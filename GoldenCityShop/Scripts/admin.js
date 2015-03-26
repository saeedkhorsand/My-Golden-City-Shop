/*############ public ##################*/
var Admin = new Object();
Admin.OneImageUpload = function (inputId) {
    $("#" + inputId).fileinput({
        maxFileCount: 10,
        previewFileType: "image",
        browseClass: "btn btn-primary",
        browseLabel: "انتخاب",
        browseIcon: '<i class="glyphicon glyphicon-picture"></i>',
        removeClass: "btn  btn-danger",
        removeLabel: "حذف",
        maxFileSize: 10000,
        removeIcon: '<i class="glyphicon glyphicon-trash"></i>',
        uploadClass: "btn btn-success",
        uploadLabel: "ارسال به سرور",
        allowedFileExtensions: ['jpg', 'gif', 'png', 'jpeg'],
        msgInvalidFileType: "از تصاویر فقط استفاده کنید",
        msgInvalidFileExtension: "از فایل های مجاز استفاده کنید[jpg,jpeg,png,gif]",
        msgFilesTooMany: "شما قادر به ارسال 10 عدد فایل میباشید",
        msgSizeTooLarge: "شما قادر به ارسال 10 مگا بایت فایل میباشید",
        uploadIcon: '<i class="glyphicon glyphicon-upload"></i>'

    });
};
Admin.CkeditorToolbar = [

	{ name: 'clipboard', groups: ['clipboard', 'undo'], items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
	{ name: 'editing', groups: ['find', 'selection'], items: ['Find', 'Replace', '-', 'SelectAll', '-', 'Preview', 'Scayt'] },
	{ name: 'forms', items: ['Checkbox', 'Radio', 'Select', 'ImageButton'] },
	'/',
	{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
	{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'], items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl', 'Language'] },
	{ name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
	{ name: 'insert', items: ['Image', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak'] },
	'/',
	{ name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
	{ name: 'colors', items: ['TextColor', 'BGColor'] },
	{ name: 'tools', items: ['Maximize', 'ShowBlocks'] },
	{ name: 'others', items: ['-'] }
];

Admin.HighLightMenu = function () {
    
    $(document).ready(function () {
        $("#side-menu li > a").each(function () {
            var $a = $(this);
            var href = $a.attr("href");
            if (href && (location.pathname.toLowerCase().split('/')[2] === href.toLowerCase().split('/')[2])) {
                //صفحه جاری را یافتیم
                $a.closest("ul").addClass("in");
            }
            if (href && (location.pathname.toLowerCase() === href.toLowerCase())) {
                //صفحه جاری را یافتیم
                $a.closest("ul").addClass("in");
                $a.css("color", "#a94442");
            }
        });
    });
};

Admin.CheckAll = function (table, checkAllElement) {

    $("#" + table + " #" + checkAllElement).click(function () {
        if ($("#" + table + " #" + checkAllElement).is(':checked')) {
            $("#" + table + " input[type=checkbox]").each(function () {
                $(this).prop("checked", true);
            });

        } else {
            $("#" + table + " input[type=checkbox]").each(function () {
                $(this).prop("checked", false);
            });
        }
    });
};
Admin.showModal = function () {
    $('#adminModal').modal('show');
};

Admin.hideModal=function() {
    $('#lightBox').modal('hide');
}
Admin.DangerAlert = function (e) {

    swal({
        title: "Are you sure?",
        text: "You will not be able to recover this imaginary file!",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel plx!",
        closeOnConfirm: true,
        closeOnCancel: true
    },
        function (isConfirm) {
            if (isConfirm) {
                alert(isConfirm);
                e.preventDefault();
            }
        });
};
Admin.OnModalClose = function (form) {
    $('#adminModal').on('hidden.bs.modal', function () {
        $("#" + form).submit();
    });
};

var LightBox = new Object();
LightBox.onSuccess = function () {
    $('#lightBox').modal('show');
}