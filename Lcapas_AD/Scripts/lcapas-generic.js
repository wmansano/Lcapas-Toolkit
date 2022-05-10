
const errorTitle = "An error has occurred!";
const errorMessage = "An error has occurred!<br/><br/>Please contact the Registrar's Office Business Systems Analyst or Business Process Analyst!";

$(document).ready(function () {
    $(".date-mask").mask("9999/99/99");
    $(".phone-mask").mask("(999) 999-9999");
    $(".zip-mask").mask("a9a9a9");
    $(".term-mask").mask("99aa");
    $('.money').inputmask("decimal", {
        alias: 'numeric',
        groupSeparator: ',',
        digits: 2,
        digitsOptional: false,
        prefix: '$',
        placeholder: '0',
        rightAlign: false
    });
    $(".datepicker").datepicker({
            showOn: 'button',
            buttonImageOnly: true,
            buttonImage: CalendarImagePath,
            altFormat: "yy/mm/dd",
            dateFormat: "yy/mm/dd",
    }).focus(function () {
        $(this).mask('9999/99/99');
    });

    $('.datetimepicker').datetimepicker({
        timeFormat: "hh:mm tt"
    });

    $(".menu-disabled").parent().hide();

    $(window).scroll(moveScroll);

    // TODO - append this functionality to default form
    // submit default form on enter key
    $(".searchfield").keypress(function (e) {
        kCode = e.keyCode || e.charCode
        if (kCode === 13) {
            $("#search-btn").click();
            return false;
        }
    });

    // Submit Form on Blur or Change event
    //$(".searchfield").blur(function () { $("#search-btn").click(); });
    $(".searchfield:not(.no-search)").change(function () { $("#search-btn").click(); });

    //$(document).on('click', '[type="submit"][data-form-action]', function (event) {
    //    var $this = $(this);
    //    var formAction = $this.attr('data-form-action');
    //    $this.closest('form').attr('action', formAction);
    //});

    $(".disabled").click(function () {
        alert("Option Disabled!");
    });

    // Checkboxes - Select All
    $("#SelectAll").click(function () {
        var checkedStatus = this.checked;
        $(this).closest('table').find("tbody tr td:first-child input:checkbox").each(function () {
            this.checked = checkedStatus;
        });
    });

    // Check/Uncheck selectAll based on children checkboxes
    $("#SelectAll").closest('table').find("tbody tr td:first-child input:checkbox").click(function () {
        if ($(this).is(":checked")) {
            var isAllChecked = 0;
            $(this).closest('table').find("tbody tr td:first-child input:checkbox").each(function () {
                if (!this.checked)
                    isAllChecked = 1;
            })
            if (isAllChecked == 0) { $("#SelectAll").prop("checked", true); }
        } else {
            $("#SelectAll").prop("checked", false);
        }
    });

    setupPagination();

    // Multi-Selection
    $('.multi-select').multiselect({
        //includeSelectAllOption: true,
        //allSelectedText: '',
        //enableFiltering: true,
        //buttonWidth: 150,
        //maxHeight: 100,
        maxHeight: 400,
        nonSelectedText: '',
        numberDisplayed: 1,
    });

    $('.dropdown').click(function () {
        // If there's no space for popup menu to open
        var topPosition = $(this).offset().top;
        var element = $(this).find('.dropdown-menu').first();
        var elemHeight = element.height() * 2;
        var moveUpPosition = -element.height() * 1.2;

        if ((topPosition + elemHeight) > $(document).height()) {
            element.animate({ 'top': moveUpPosition + 'px', 'right': '100%' }, 50);
        }
    });

});

function cloneli(ul, maxitems, itemNum) {
    if ($(ul + " li").length < maxitems) {
        $(ul + " li:last").clone().find(":input").each(function () {
            $(this).attr({
                'name': function (_, name) { itemNum = parseInt(name.substring(name.lastIndexOf("[") + 1).charAt(0)) + 1; return name.replace("[" + (itemNum - 1) + "]", "[" + itemNum + "]"); },
                'id': function (_, id) { return id.replace("_" + (itemNum - 1) + "_", "_" + itemNum + "_"); }
            });
            $(this).val('');
        }).end().appendTo($(ul));
        itemNum++;
    }
}

function removeli(btn, itemNum) {
    if ($(btn).closest('ul').find('li').length > 1) {
        $(btn).closest('li').remove();
        itemNum--;
    }
    return false;
}

function PopupCenterDual(url, title, w, h) {
    var dualScreenLeft = window.screenLeft !== undefined ? window.screenLeft : screen.left;
    var dualScreenTop = window.screenTop !== undefined ? window.screenTop : screen.top;

    width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
    height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

    var left = ((width / 2) - (w / 2)) + dualScreenLeft;
    var top = ((height / 2) - (h / 2)) + dualScreenTop;
    var newWindow = window.open(url, title, 'scrollbars=yes, location=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);

    if (window.focus) {
        newWindow.focus();
    }
}

function LoadIframe(message) {
    var $iframe = $('#iframeContent');

    $iframe.ready(function () {
        $iframe.contents().find("body").html(message);
        $('.loader').hide();
    });
}

function printFrame(id) {
    var frm = document.getElementById(id).contentWindow;
    frm.focus();  // focus on contentWindow is needed on some ie versions
    frm.print();
}

function ShowPopupIframe(_height, _width) {
    $('#popupIframe').dialog({
        resizable: false,
        modal: true,
        title: 'View Document Screen',
        height: _height,
        width: _width,
        dialogClass: 'dialog-iframe',
        buttons: {
            "Print": {
                text: 'Print',
                "class": 'button',
                click: function () {
                    printFrame('iframeContent');
                }
            },
            "Close": {
                text: 'Close',
                "class": 'button',
                click: function () {
                    $('#iframeContent').contents().find("body").html('');
                    $('.loader').show();
                    $(this).dialog('close');
                }
            }
        },
    });
}

function postdata(url, data, success, failure) {
    $.ajax({
        type: 'POST',
        url: url,
        data: data,
        success: success,
        error: failure
    });
}

function setupPagination() {
        // Result Paging
    $(".page-first").click(function () {
        $("#hf-pIndex").val(1);
        $("#search-btn").click();
    });

    $(".page-last").click(function () {
        var pcount = parseInt($("#hf-pSize").val(), 10);
        var last = parseInt(Math.ceil($("#totalRes").val() / pcount));
        $("#hf-pIndex").val(last);
        $("#search-btn").click();
    });

    $(".page-next").click(function () {
        var pcount = parseInt($("#hf-pIndex").val(), 10);
        $("#hf-pIndex").val(pcount + 1);
        $("#search-btn").click();
    });

    $(".page-prev").click(function () {
        var pcount = parseInt($("#hf-pIndex").val(), 10);
        $("#hf-pIndex").val(pcount - 1);
        $("#search-btn").click();
    });

    $('#pSize').on('change', function (e) {
        $("#hf-pIndex").val(1);
        $("#hf-pSize").val(this.value);
        $("#search-btn").click();
    });
}

function confirm(title, message, fn_apply, fn_cancel) {

    $("#dialog-confirm").html(message);
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: title,
        height: 250,
        width: 400,
        show: { effect: "blind" },
        hide: { effect: "blind" },
        dialogClass: 'dialog-confirm',
        buttons: {
            "Yes": {
                text: 'Yes',
                "class": 'button',
                click: function () {
                    $(this).dialog('close');
                    if (fn_apply != null && typeof fn_apply != 'undefined') fn_apply();
                }
            },
            "No": {
                text: 'No',
                "class": 'button',
                click: function () {
                    $(this).dialog('close');
                    if (fn_cancel != null && typeof fn_cancel != 'undefined') fn_cancel();
                }
            },
            "Cancel": {
                text: 'Cancel',
                "class": 'button',
                click: function () {
                    $(this).dialog('close');
                }
            }
        }
    });
}

function confirmYesNo(title, message, fn_apply, fn_cancel) {

    $("#dialog-confirm").html(message);
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: title,
        height: 250,
        width: 400,
        show: { effect: "blind" },
        hide: { effect: "blind" },
        dialogClass: 'dialog-confirm',
        buttons: {
            "Yes": {
                text: 'Yes',
                "class": 'button',
                click: function () {
                    $(this).dialog('close');
                    if (fn_apply != null && typeof fn_apply != 'undefined') fn_apply();
                }
            },
            "No": {
                text: 'No',
                "class": 'button',
                click: function () {
                    $(this).dialog('close');
                    if (fn_cancel != null && typeof fn_cancel != 'undefined') fn_cancel();
                }
            }
        }
    });
}

function message(title, message, fn_apply) {
    $("#dialog-message").html(message);
    $("#dialog-message").dialog({
        resizable: false,
        modal: true,
        title: title,
        height: 250,
        width: 400,
        show: { effect: "blind" },
        hide: { effect: "blind" },
        dialogClass: 'dialog-message',
        buttons: {
            "Ok": {
                text: 'Ok',
                "class": 'button',
                click: function () {
                    $(this).dialog('close');
                    if (fn_apply != null && typeof fn_apply != 'undefined') fn_apply();
                }
            }
        }
    });
}

function warning(title, message) {
    $("#dialog-warning").html(message);
    $("#dialog-warning").dialog({
        resizable: false,
        modal: true,
        title: 'Warning: ' + title,
        height: 250,
        width: 400,
        show: { effect: "blind" },
        hide: { effect: "blind" },
        dialogClass: 'dialog-warning',
        buttons: {
            "Ok": {
                text: 'Ok',
                "class": 'button',
                click: function () {
                    $(this).dialog('close');
                }
            }
        }
    });
}

function error(title, message, fn_cancel) {
    $("#dialog-error").html(message);
    $("#dialog-error").dialog({
        resizable: false,
        modal: true,
        title: 'Attention: ' + title,
        height: 250,
        width: 400,
        show: { effect: "blind" },
        hide: { effect: "blind" },
        dialogClass: 'dialog-error',
        buttons: {
            "Ok": {
                text: 'Ok',
                "class": 'button',
                click: function () {
                    $(this).dialog('close');
                    if (fn_cancel != null && typeof fn_cancel != 'undefined') fn_cancel();
                }
            }
        }
    });
}

function cancel(title, message) {
    $("#dialog-cancel").html(message);
    $("#dialog-cancel").dialog({
        resizable: false,
        modal: true,
        title: title,
        height: 250,
        width: 400,
        show: { effect: "blind" },
        hide: { effect: "blind" },
        dialogClass: 'dialog-cancel',
        buttons: {
            "Cancel": {
                text: 'Cancel',
                "class": 'button',
                click: function () {
                    $(this).dialog('close');
                }
            }
        }
    });
}


function responseDialog(title, message, fn_respond, fn_return) {

    $("#dialog-create-response").html(message);
    $("#dialog-create-response").dialog({
        resizable: false,
        modal: true,
        title: title,
        height: 250,
        width: 400,
        show: { effect: "blind" },
        hide: { effect: "blind" },
        dialogClass: 'dialog-confirm',
        buttons: {
            "Search": {
                text: 'Search',
                "class": 'button',
                click: function () {
                    $(this).dialog('close');
                }
            },
            "Respond": {
                text: 'Respond',
                "class": 'button',
                click: function () {
                    $(this).dialog('close');
                    if (fn_respond != null && typeof fn_respond != 'undefined') fn_respond();
                }
            },
            "Return": {
                text: 'Return',
                "class": 'button',
                click: function () {
                    $(this).dialog('close');
                    if (fn_return != null && typeof fn_return != 'undefined') fn_return();
                }
            }
        }
    });
}

function restrictionYesNoRespond(title, message, fn_apply, fn_cancel, fn_respond) {

    $("#dialog-confirm").html(message);
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: title,
        height: 250,
        width: 400,
        show: { effect: "blind" },
        hide: { effect: "blind" },
        dialogClass: 'dialog-confirm',
        buttons: {
            "Yes": {
                text: 'Yes',
                "class": 'button',
                click: function () {
                    $(this).dialog('close');
                    if (fn_apply != null && typeof fn_apply != 'undefined') fn_apply();
                }
            },
            "No": {
                text: 'No',
                "class": 'button',
                click: function () {
                    $(this).dialog('close');
                    if (fn_cancel != null && typeof fn_cancel != 'undefined') fn_cancel();
                }
            },
            "Respond": {
                text: 'Respond',
                "class": 'button',
                click: function () {
                    $(this).dialog('close');
                    if (fn_respond != null && typeof fn_respond != 'undefined') fn_respond();
                }
            },
        }
    });
}


// General Ajax Post call to controllers
function ajaxPostControllerCall(updateUrl, parameters, fn_success, fn_error) {
    $.ajax({
        type: 'POST',
        url: updateUrl,
        data: parameters,
        success: function (response) {
            if (fn_success != null && typeof fn_success != 'undefined') fn_success();
        },
        error: function (response) {
            if (fn_error != null && typeof fn_error != 'undefined') fn_error();
            error("An error has occured.", "Unable to retrieve data from database.");
        }
    });
}

// Get Selected Items
function getSelectedItems() {
    var values = [];

    $("#SelectAll").closest('table').find("tbody tr td:first-child input:checkbox").each(function () {
        if (this.checked) {
            values.push(this.name);
        }
    });
    return values;
}

// Convert true/false string to boolean
function toBoolean(str) {
    return str.toLowerCase() == 'true'
};

function moveScroll() {
    var scroll = $(window).scrollTop();
    var anchor_top = $("#data-tbl").offset() !== undefined ? $("#data-tbl").offset().top - 102 : 0;
    var anchor_bottom = $("#bottom_anchor").offset() !== undefined ? $("#bottom_anchor").offset().top : 0;
    if (scroll > anchor_top && scroll < anchor_bottom) {
        //$('.columnScrolling').css({
        //    position: 'sticky',
        //    top: 102,
        //    zIndex: 1
        //});


        clone_table = $("#clone");
        if (clone_table.length == 0) {
            clone_table = $("#data-tbl").clone();
            clone_table.attr('id', 'clone');
            clone_table.css({
                position: 'fixed',
                'pointer-events': 'none',
                top: 102,
                zIndex: 1
            });
            clone_table.find('.hide-scroll').css({ visibility: 'hidden' });
            clone_table.width($("#data-tbl").width());
            $("#table-container").append(clone_table);
            $("#clone").css({ visibility: 'hidden' });
            $("#clone thead").css({ visibility: 'visible', 'pointer-events': 'auto' });
        }
    } else {
        //$('.columnScrolling').css({
        //    position: 'relative'
        //});

        $("#clone").remove();
    }
}

// Print Report Function
function PrintReport(docType, height, width, filterFields, destAction) {
    var reportID = getSelectedItems();

    var allSelected = $('#selectAll').find('input[type="checkbox"]:checked').length > 0;

    if (reportID.length > 0) {
        ShowPopupIframe(height, width);
        $.ajax({
            type: 'POST',
            url: PrintReportPath,
            data: { reportID: reportID, docType: docType, allSelected: allSelected, filterFields: filterFields, destAction: destAction },
            success: function (response) {
                if (response.Success) {
                    LoadIframe(response.Message);
                }
            },
            error: function (response) {
                error(errorTitle, errorMessage);
            }
        });
    } else {
        warning("No Results Selected!", "Please, select at least one Result in order to print the report!");
    }
}

// Export Report to Excel Function
function ExportToExcel(reportType, filterFields, destAction) {
    var reportID = getSelectedItems();

    var allSelected = $('#selectAll').find('input[type="checkbox"]:checked').length > 0;

    if (reportID.length > 0) {
        $("#excelForm").remove();
        $form = $("<form id='excelForm' method='post'></form>");
        $form.append("<input id='reportID' type='hidden' name='reportID' value='" + reportID + "' />");
        $form.append("<input id='reportType' type='hidden' name='reportType' value='" + reportType + "' />");
        $form.append("<input id='allSelected' type='hidden' name='allSelected' value='" + allSelected + "' />");
        $form.append("<input id='filterFields' type='hidden' name='filterFields' value='" + filterFields + "' />");
        $form.append("<input id='destAction' type='hidden' name='destAction' value='" + destAction + "' />");
        $('body').append($form);
        $form.attr('action', ExportReportPath).submit();
    } else {
        warning("No Results Selected!", "Please, select at least one Result in order to export the report!");
    }
}

// Export Report to Xml Function
function ExportToXML(reportType, filterFields) {
    var reportID = getSelectedItems();

    var allSelected = $('#selectAll').find('input[type="checkbox"]:checked').length > 0;

    if (reportID.length > 0) {
        $("#xmlForm").remove();
        $form = $("<form id='xmlForm' method='post'></form>");
        $form.append("<input id='reportID' type='hidden' name='reportID' value='" + reportID + "' />");
        $form.append("<input id='reportType' type='hidden' name='reportType' value='" + reportType + "' />");
        $form.append("<input id='allSelected' type='hidden' name='allSelected' value='" + allSelected + "' />");
        $form.append("<input id='filterFields' type='hidden' name='filterFields' value='" + filterFields + "' />");
        $('body').append($form);
        $form.attr('action', XMLReportPath).submit();
    } else {
        warning("No Results Selected!", "Please, select at least one Result in order to export the report!");
    }
}



// Get Filter Values
function getFilterValues(searchFields) {
    var values = {};

    $.each(searchFields.split(','), function () {
        values[$.trim(this)] = $('#' + $.trim(this)).val();
    });

    return values;
}
