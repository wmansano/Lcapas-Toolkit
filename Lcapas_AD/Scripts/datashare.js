$(document).ready(function () {
    showdiv(100);
    SetupEvents();
    SetupDialogs();
    ImportNewApps();
});

function SetupEvents() {

    //Setup Masks
    $("#birthDate").mask("9999/99/99");
    $("#fromDate").mask("9999/99/99");
    $("#toDate").mask("9999/99/99");
    $("#asn").mask("9999999999");

    //Setup Date Pickers
    $("#fromDate").datepicker({
        showOn: 'button',
        buttonImageOnly: true,
        buttonImage: 'Content/Images/calendar.png',
        dateFormat: 'yy/mm/dd'
    });

    $("#toDate").datepicker({
        showOn: 'button',
        buttonImageOnly: true,
        buttonImage: 'Content/Images/calendar.png',
        dateFormat: 'yy/mm/dd'
    });

    $("img[class='ui-datepicker-trigger']").each(function () {
        $(this).attr('style', 'margin-left:2px;');
    });

    //Setup Print Application Events
    $("#prntAppBtn").click(function () {
        showdiv(200);
        PrintDataShareWordApps();
    });

    //Setup Print List Event
    $("#prntListBtn").click(function () {
        showdiv(200);
        PrintDataShareExcelList();
    });

    //Setup Print List Event
    $("#importAppsBtn").click(function () {
        showdiv(200);
        ImportNewApps();
    });

    $("#batchDate").addClass('text ui-widget-content ui-corner-all');

    //setup search link
    $("#search").click(function () {

        // Get object of URL parameters
        //var allVars = $.getUrlVars();
        var batchDate = $.getUrlVar('batchDate');
        var birthDate = $.getUrlVar('birthDate');
        var fromDate = $.getUrlVar('fromDate');
        var toDate = $.getUrlVar('toDate');
        var firstName = $.getUrlVar('firstName');
        var lastName = $.getUrlVar('lastName');
        var middleName = $.getUrlVar('middleName');
        var asn = $.getUrlVar('asn');

        if (batchDate != undefined) {
            $("#batchDate").val(batchDate);
        }
        if (birthDate != undefined) {
            $("#birthDate").val(birthDate);
        }
        if (fromDate != undefined) {
            $("#fromDate").val(fromDate);
        }
        if (toDate != undefined) {
            $("#toDate").val(toDate);
        }
        if (asn != undefined) {
            $("#asn").val(asn);
        }
        if (lastName != undefined) {
            $("#lastName").val(lastName);
        }
        if (firstName != undefined) {
            $("#firstName").val(firstName);
        }
        if (middleName != undefined) {
            $("#middleName").val(middleName);
        }
        $("#searchApps").dialog("open");
    });

    //clear search criteria
    $("#clear").click(function () {
        window.location = DataShareIndexPath;
    });

    //Checkboxes - Select All
    $("#appTbl thead tr th:first input:checkbox").click(function () {
        var checkedStatus = this.checked;
        $("#appTbl tbody tr td:first-child input:checkbox").each(function () {
            this.checked = checkedStatus;
        });
    });
}

function SetupDialogs() {

    // setup search dialog
    $("#searchApps").dialog({
        focus: function () {
            $(this).on("keyup", function (e) {
                if (e.keyCode === 13) {
                    $(this).parent().find("button:contains('Search')").trigger("click");
                    return false;
                }
            });
        },
        autoOpen: false,
        height: 366,
        width: 280,
        modal: true,
        buttons: {
            "Clear": function () {
                $("#birthDate").val("");
                $("#fromDate").val("");
                $("#toDate").val("");
                $("#firstName").val("");
                $("#lastName").val("");
                $("#middleName").val("");
                $("#asn").val("");
            },
            Cancel: function () {
                $(this).dialog("close");
            },
            "Search": function () {
                showdiv(200);
                var con = "?";
                var url = DataShareIndexPath;
                var batchDate = $("#batchDate").val();
                var birthDate = $("#birthDate").val();
                var fromDate = $("#fromDate").val();
                var toDate = $("#toDate").val();
                var firstName = $("#firstName").val();
                var lastName = $("#lastName").val();
                var asn = $("#asn").val();

                if (batchDate != undefined && batchDate != "") { url += con + "strBatch=" + batchDate; con = "&"; }
                if (firstName != undefined && firstName != "") { url += con + "firstName=" + firstName; con = "&"; }
                if (lastName != undefined && lastName != "") { url += con + "lastName=" + lastName; con = "&"; }
                if (birthDate != undefined && birthDate != "") { url += con + "strBirth=" + birthDate; con = "&"; }
                if (fromDate != undefined && fromDate != "") { url += con + "strFrom=" + fromDate; con = "&"; }
                if (toDate != undefined && toDate != "") { url += con + "strTo=" + toDate; con = "&"; }
                if (asn != undefined && asn != "") { url += con + "asn=" + asn; }

                window.location = url;
                hidediv(2000);
                $(this).dialog("close");
            }
        },
        close: function () {
            //allFields.val("").removeClass("ui-state-error");
        }
    });
}

function PrintDataShareWordApps() {
    var values = [];
    $("#appTbl tbody tr td:first-child input:checkbox").each(function () {
        if (this.checked) {
            values.push(this.name);
        }
    });
    $.post(PrepareDataShareWordAppsPath, { data: values }, DownloadDataShareWordApps, 'json');
}

DownloadDataShareWordApps = function (data) {
    if (data.message = "Success") {
        window.location = DownloadDataShareWordAppsPath;
    }
    else {
        message("An error occured. Please contact the helpdesk.");
    }
    hidediv(500);
};

function PrintDataShareExcelList() {
    var values = [];
    $("#appTbl tbody tr td:first-child input:checkbox").each(function () {
        if (this.checked) {
            values.push(this.name);
        }
    });
    $.post(PrepareDataShareExcelListPath, { data: values }, DownloadDataShareExcelList, 'json');
}

DownloadDataShareExcelList = function (data) {
    if (data.message = "Success") {
        window.location = DownloadDataShareExcelListPath;
    }
    else {
        message("An error occured. Please contact the helpdesk.");
    }
    hidediv(500);
};

function ImportNewApps() {
    var result = $.post(ImportNursingApplicationsPath, {}, ShowImportMessage, 'json');
}

ShowImportMessage = function (data) {
    var message = data.Message;
    hidediv(100);
    $('#msg').text(message).fadeIn(1000);
    $('#msg').fadeOut(8000);
};

function showdiv(duration) {
    $('#LoadingDiv').fadeIn(duration);
}

function hidediv(duration) {
    $('#LoadingDiv').fadeOut(duration);
}

$.extend({
    getUrlVars: function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (name) {
        return $.getUrlVars()[name];
    }
});

Date.prototype.toDateInputValue = (function () {
    var local = new Date(this);
    local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
    return local.toJSON().slice(0, 10);
});

function getdate(days) {
    var d = new Date();

    var month = d.getMonth() + 1;
    var day = d.getDate() + (days);

    var output = d.getFullYear() + '/' + (month < 10 ? '0' : '') + month + '/' + (day < 10 ? '0' : '') + day;

    return output;
}
