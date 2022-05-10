var destinationRowCount = 0;
var currentAttempt = 0;
var maxAttempts = 60;

$(document).ready(function () {
    $("#add-dest-btn").click(function () {
        cloneli('#destinations-list', 5, destinationRowCount);
    });

    $('#preview-btn').click(function () {
        SubmitPreview();
    });

    $('#close_iframe').click(function () { $('#popupIframe').hide('slow'); });

    $('.export-colleague-btn').click(function () {
        var transmissionDataId = $(this).attr('transmissionDataId');
        ExportColleague(transmissionDataId);
    });

    // Student Lookup Selected Index
    $(".set-hidden-btn").click(function () {

        var btn_val = $(this).attr('value');
        var btn_key = $(this).attr('key');

        $('#' + btn_key).val(btn_val);

        var hiddenLinks = $(this).closest("#option-column").find(".set-hidden");

        hiddenLinks.each(function () {

            var key = $(this).attr('key');
            var value = $(this).attr('value');

            $('#' + key).val(value);

        });

        $("#search-btn").click();
        return false;
    });

    $(".select-row-item").click(function () {
        var rowid = $(this).find('.keycell').text().trim();

        if (rowid != '') {
            $('.selSnum').val(rowid);
        }  

        $('#search-btn').click();
    });

    
    // Enable Submit Button
    EnableSubmitButton($('#ReleaseAuthorizedIndicator'));  // First load
    $('#ReleaseAuthorizedIndicator').change(function () {  // When checkbox change value/clicked
        EnableSubmitButton($(this));
    });

    // Turn preview flag off when submiting/sending request/transcript/response
    $("#submit-btn").click(function () {
        $('#previewMessageIndicator').val(false);
    });

    // Allow to modify Request/Transcript
    $("#modify-btn").click(function () {
        $('#previewMessageIndicator').val(false);
        EnableSubmitButton($('#ReleaseAuthorizedIndicator'));
    });

    $("#clear-btn").click(function () {
        $("input.searchfield").each(function () {
            $(this).val("");
        });
        $('#clearFilter').val(true);   
    });

    // Print Requests/Responses
    $("#prntReqResBtn").click(function () {
        PrintRequestResponseReport();
    });

    // Check if it has to show create response dialog
    ShowResponseDialog();

    // Check if Student has any Restriction to show warning dialog
    StudentRestrictionCheck();

    // Check if Student has an ASN number
    StudentMissingASNCheck();

    // Check if Colleague Transaction(s) complete
    LoadCreateMessagePreview();

    // Send Unsolicited Batch Transcript
    $("#sendUnsolicitedBatchTransBtn").click(function () {
        var studentFields = getFilterValues('FullName, Snumber, ASN');
        var filterFields = getFilterValues('StudentRecord, ProgramCode, DeptCode, Term');
        filterFields['StudentRecord'] = studentFields;
        SendUnsolicitedBatchTranscripts(JSON.stringify(filterFields));
    });

    // Generate Mycreds Transcript Batch
    $("#generateMyCredsTransBatch").click(function () {
        var studentFields = getFilterValues('FullName, Snumber, Email');
        var filterFields = getFilterValues('RequestID, StudentRecord, FromRequestDate, toRequestDate, HoldType, FromDateProduced, ToDateProduced, Operator, Comments');
        filterFields['StudentRecord'] = studentFields;
        GenerateMyCredsTranscriptBatch(JSON.stringify(filterFields));
    });

    // Generate Mycreds Bulk Send Batch
    $("#generateMyCredsBulkSend").click(function () {
        var studentFields = getFilterValues('FullName, Snumber, Email');
        var filterFields = getFilterValues('AcadCredentialsID, StudentRecord, Ethnic, AlienStatus, ProgramCode, Campus, CCDType, AcadHonors, AcadGPA, FromAcadCCDDate, ToAcadCCDDate, FromAcadCredAddDate, ToAcadCredAddDate');
        filterFields['StudentRecord'] = studentFields;
        GenerateMyCredsBulkSend(JSON.stringify(filterFields));
    });

    // Set the right Hold Type Mask
    $('.hold-type').change(function () {
        $('.hold-detail').datepicker('destroy');
        switch ($('option:selected', this).attr('data-type')) {
            case 'Date':  // Dates
                $('.hold-detail').mask('9999-99');
                $('.hold-detail').attr('placeholder', 'yyyy-mm');
                $('.hold-detail').datepicker({
                    altFormat: "yy-mm",
                    dateFormat: "yy-mm",
                });
                break;
            case 'Term':  // Term
                $('.hold-detail').mask('99 aa');
                $('.hold-detail').attr('placeholder', 'yy tt');
                break;
            default:  // Now (Text  )
                $('.hold-detail').unmask();
                $('.hold-detail').removeAttr('placeholder');
                break;
        }
    });

    // Parse Received Response
    $('.parse-colleague-btn').click(function () {
        var transmissionDataUuid = $(this).attr('transmissionDataUuid');
        ParseResponse(transmissionDataUuid);
    });

    // Print Unsolicited Batch Transcript Report
    $("#prntUnsolicitedBatchTransReportBtn").click(function () {
        var filterFields = getFilterValues('FullName, Snumber, ASN, ProgramCode, DeptCode, Term');
        PrintReport('UnsolicitedBatchTranscriptReport', 750, 1050, JSON.stringify(filterFields));
    });

    // Export to Excel: Unsolicited Batch Transcript Report
    $("#excelUnsolicitedBatchTransReportBtn").click(function () {
        var filterFields = getFilterValues('FullName, Snumber, ASN, ProgramCode, DeptCode, Term');
        ExportToExcel('UnsolicitedBatchTranscriptReport', JSON.stringify(filterFields))
    });

    // Print MyCreds Transcript Batch
    $("#prntMyCredsTransReportBtn").click(function () {
        var studentFields = getFilterValues('FullName, Snumber, Email');
        var filterFields = getFilterValues('RequestID, StudentRecord, FromRequestDate, toRequestDate, HoldType, FromDateProduced, ToDateProduced, Operator, Comments');
        filterFields['StudentRecord'] = studentFields;
        PrintReport('MyCredsTransReport', 750, 1200, JSON.stringify(filterFields));
    });

    // Export to Excel: MyCreds Transcript Batch
    $("#excelMyCredsBatchTransReportBtn").click(function () {
        var studentFields = getFilterValues('FullName, Snumber, Email');
        var filterFields = getFilterValues('RequestID, StudentRecord, FromRequestDate, toRequestDate, HoldType, FromDateProduced, ToDateProduced, Operator, Comments');
        filterFields['StudentRecord'] = studentFields;
        ExportToExcel('MyCredsBatchTranscriptReport', JSON.stringify(filterFields))
    });

    // Print MyCreds Bulk Send Batch
    $("#prntMyCredsBulkSendReportBtn").click(function () {
        var studentFields = getFilterValues('FullName, Snumber, Email');
        var filterFields = getFilterValues('AcadCredentialsID, StudentRecord, Ethnic, AlienStatus, ProgramCode, Campus, CCDType, AcadHonors, AcadGPA, FromAcadCCDDate, ToAcadCCDDate, FromAcadCredAddDate, ToAcadCredAddDate');
        filterFields['StudentRecord'] = studentFields;
        PrintReport('MyCredsBulkSendReport', 750, 1200, JSON.stringify(filterFields));
    });

    // Export to Excel: MyCreds Bulk Send Batch
    $("#excelMyCredsBulkSendReportBtn").click(function () {
        var studentFields = getFilterValues('FullName, Snumber, Email');
        var filterFields = getFilterValues('AcadCredentialsID, StudentRecord, Ethnic, AlienStatus, ProgramCode, Campus, CCDType, AcadHonors, AcadGPA, FromAcadCCDDate, ToAcadCCDDate, FromAcadCredAddDate, ToAcadCredAddDate');
        filterFields['StudentRecord'] = studentFields;
        ExportToExcel('MyCredsBulkSendReport', JSON.stringify(filterFields))
    });

});

function LoadCreateMessagePreview() {

    var previewMessageIndicator = $('#previewMessageIndicator').val();
    var transmissionDataUUID = $('#transmissionDataUUID').val();
    var destAction = $('#destAction').val();

    if (destAction != 'CreateRequest' && previewMessageIndicator == 'true' && transmissionDataUUID != '') {
        //PopupCenterDual(DisplayTranscriptMessagePath + '/?uuid=' + transmissionDataUUID + '&multiple=true', 'Message Preview', '850', '800');
        ViewDocumentDialog(transmissionDataUUID);
    }
}

function ViewDocumentDialog(transmissionDataUUID) {
    ShowPopupIframe(750, 870);

    // Call controller to get html iframe content
    $.ajax({
        type: 'POST',
        url: DisplayIframeDocumentPath,
        data: { transmissionDataUUID: transmissionDataUUID, multiple: true },
        success: function (response) {
            result = response.Success;
            if (response.Success) {
                LoadIframe(response.Message);

                // Mark Request or Response as viewed
                if (response.DocType == 'Response') {
                    $('#responseLine-' + transmissionDataUUID).removeClass('text-bold').removeClass('yellow').css('background-color', 'white').mouseout(function () { $(this).css('background-color', 'white'); });
                } else {
                    $('#requestLine-' + transmissionDataUUID).removeClass('text-bold').removeClass('yellow').css('background-color', 'white').mouseout(function () { $(this).css('background-color', 'white'); });
                }
            }
        },
        error: function (response) {
            // legitimate error
            error(errorTitle, errorMessage);
        }
    });
}

function ViewErrorMessageDialog(requestTrackingID) {
    ShowPopupIframe(750, 870);

    $.ajax({
        type: 'POST',
        url: DisplayErrorMessagePath,
        data: { requestTrackingID: requestTrackingID },
        success: function (response) {
            result = response.Success;
            if (response.Success) {
                LoadIframe(response.Message);
            }
        },
        error: function (response) {
            // legitimate error
            error(errorTitle, errorMessage);
        }
    });
}

function MarkAsNotViewed(transDataUUID, docType) {
    $.ajax({
        type: 'POST',
        url: MarkAsNotViewedPath,
        data: { transDataUUID: transDataUUID, docType: docType },
        success: function (response) {
            result = response.Success;
            if (response.Success) {
                if (response.DocType == 'Response') {
                    $('#responseLine-' + transDataUUID).addClass('text-bold').addClass('yellow').css('background-color', '#ffff78').mouseout(function () { $(this).css('background-color', '#ffff78'); });
                } else {
                    $('#requestLine-' + transDataUUID).addClass('text-bold').addClass('yellow').css('background-color', '#ffff78').mouseout(function () { $(this).css('background-color', '#ffff78'); });
                }
            }
        },
        error: function (response) {
            // legitimate error
            error(errorTitle, errorMessage);
        }
    });
}

function SendtoColleagueTRRQ(transDataUUID, docType, responseTransDataUUID) {
    $.ajax({
        type: 'POST',
        url: SendtoColleagueTRRQPath,
        data: { transDataUUID: transDataUUID },
        success: function (response) {
            if (response.Success) {
                // Show Line as Sent to TRRQ
                if (docType == 'Response') {
                    $('#responseLine-' + responseTransDataUUID).find('.sendToTRRQ').html('<img src="~/Content/Images/check.png" class="request-status pull-right" />');
                } else {
                    $('#requestLine-' + transDataUUID).find('.sendToTRRQ').html('<img src="~/Content/Images/check.png" class="request-status pull-right" />');
                }
            } else {
                if (response.Message == 'NoSNumberFound') {
                    // Redirect to the Student Lookup page
                    LoadStudentLookup(transDataUUID, 'SaveColleagueRequestTRRQ');
                } else {
                    error(errorTitle, "Unable to send Request to Colleague TRRQ!<br/><br/>Student might be locked in Colleague!<br/><br/>Or Request already exists in Colleague!<br/><br/>" + errorMessage);
                }
            }
        },
        error: function (response) {
            // legitimate error
            error(errorTitle, errorMessage);
        }
    });
}

// Redirect to the Student Lookup page
function LoadStudentLookup(transDataUUID, destAction) {
    var sNumber = $('#sNumber_' + transDataUUID).val() || "";
    var ASN = $('#asn_' + transDataUUID).val() || "";
    var FirstName = $('#firstName_' + transDataUUID).val() || "";
    var MiddleName = $('#middleName_' + transDataUUID).val() || "";
    var LastName = $('#lastName_' + transDataUUID).val() || "";
    var Gender = $('#gender_' + transDataUUID).val() || "";  // "1";
    var BirthDate = $('#birthDate_' + transDataUUID).val() || "";  // "1975-08-11";
    var FromUrlAction = $('#fromUrlAction').val() || "";
    
    $("#studentLookupForm").remove();
    $form = $("<form id='studentLookupForm' method='post'></form>");
    $form.append("<input name='SearchFilter.StudentRecord.Snumber' type='hidden' value='" + sNumber + "' />");
    $form.append("<input name='SearchFilter.StudentRecord.ASN' type='hidden' value='" + ASN + "' />");
    $form.append("<input name='SearchFilter.StudentRecord.FirstName' type='hidden' value='" + FirstName + "' />");
    $form.append("<input name='SearchFilter.StudentRecord.MiddleName' type='hidden' value='" + MiddleName + "' />");
    $form.append("<input name='SearchFilter.StudentRecord.LastName' type='hidden' value='" + LastName + "' />");
    $form.append("<input name='SearchFilter.StudentRecord.GenderCode' type='hidden' value='" + Gender + "' />");
    $form.append("<input name='SearchFilter.StudentRecord.BirthDate' type='hidden' value='" + BirthDate + "' />");
    $form.append("<input name='SearchFilter.FromUrlAction' type='hidden' value='" + FromUrlAction + "' />");
    $form.append("<input name='SearchFilter.TransactionTranscriptUuid' type='hidden' value='" + transDataUUID + "' />");
    $form.append("<input name='SearchFilter.DestAction' type='hidden' value='" + destAction + "' />");

    // Process Former Names
    var formerNameCount = $('#formerNameCount_' + transDataUUID).val() || "";
    if (typeof formerNameCount !== 'undefined' && formerNameCount > 0) {
        // the array is defined and has at least one element
        for (var i = 0; i < formerNameCount; i++) {
            if (typeof $('#formerFirstName_' + i + '_' + transDataUUID).val() !== 'undefined') {
                $form.append("<input name='SearchFilter.StudentRecord.FormerNames[" + i + "].FirstName' type='hidden' value='" + $('#formerFirstName_' + i + '_' + transDataUUID).val() + "' />");
            }
            if (typeof $('#formerMiddleName_' + i + '_' + transDataUUID).val() !== 'undefined') {
                $form.append("<input name='SearchFilter.StudentRecord.FormerNames[" + i + "].MiddleName' type='hidden' value='" + $('#formerMiddleName_' + i + '_' + transDataUUID).val() + "' />");
            }
            if (typeof $('#formerLastName_' + i + '_' + transDataUUID).val() !== 'undefined') {
                $form.append("<input name='SearchFilter.StudentRecord.FormerNames[" + i + "].LastName' type='hidden' value='" + $('#formerLastName_' + i + '_' + transDataUUID).val() + "' />");
            }
        }
    }

    $('body').append($form);
    $form.attr('action', StudentLookupPath).submit();
}

function EnableSubmitButton(releaseAuthorizedIndicator) {

    var submitBtn = $('#submit-btn');
    var previewed = $('#previewMessageIndicator').val();
    var collapseVer = $('#collapseVerification');
    var destinationList = $('#destinations-list li select, #destinations-list li input');
    var destinationIcons = $('.dest-icon');

    if (previewed == 'true') {
        releaseAuthorizedIndicator.attr('checked', true);
        if (releaseAuthorizedIndicator.is(':checked')) {
            submitBtn.removeAttr('disabled', 'disabled');
            submitBtn.removeClass('disabled-btn');
        } else {
            submitBtn.attr('disabled', 'disabled');
            submitBtn.addClass('disabled-btn');
        }
        destinationList.attr('disabled', 'disabled');
        destinationList.addClass('disabled-btn');
        destinationIcons.hide('slow');
        collapseVer.show('slow');
    } else {
        destinationList.removeAttr('disabled', 'disabled');
        destinationList.removeClass('disabled-btn');
        destinationIcons.show('slow');
        collapseVer.hide('slow');
    }
}

function ShowResponseDialog() {
    // Check if it has to show create response dialog
    if ($("#dialog-create-response")[0]) {
        // if response dialog exists
        var confirmmsg = 'Do you want to search again? <br />or send a response?';
        var respond = function () {
            $('#createResponse').val(true);
            $('#submitForm').submit();
        }
        var redirect = function () {
            var red = $('#dialog-create-response').attr('redirect')
            if (red != undefined && red != "")
            {
                location.href = $('#dialog-create-response').attr('redirect');
            }
        }
        responseDialog('Student not found!', confirmmsg, respond, redirect);
    }
}

function StudentRestrictionCheck() {
    var studentRestriction = $('#studentRestriction').val();

    if (studentRestriction == 'True') {
        var message = 'Student has an restriction (Hold)! <br />Do you still want to proceed?';
        var dontAllowPreview = function () {
            $('#preview-btn').hide('slow');
        }
        var respond = function () {
            $('#destAction').val('CreateResponse');
            $('#studentRestriction').val(false);
            $('#createMessageForm').submit();
        }
        //confirmYesNo('Student restriction!', message, null, dontAllowPreview);
        restrictionYesNoRespond('Student restriction!', message, null, dontAllowPreview, respond);
    }
}

function StudentMissingASNCheck() {
    var studentRestriction = $('#studentMissingASN').val();

    if (studentRestriction == 'True') {
        var message = 'Student is missing ASN number!';
        var dontAllowPreview = function () {
            $('#preview-btn').hide('slow');
        }
        error('Student Missing ASN!', message, dontAllowPreview);
    }
}

function SubmitPreview() {
    var destAction = $('#destAction').val();
    if (destAction == 'CreateTranscript') {
        currentAttempt = 0;  // Reset max attempts if "Preview" button is pressed again
        event.preventDefault();
        var transactionTranscriptUuid = $('#transactionTranscriptUuid').val();
        var result = CheckTransTransComplete(transactionTranscriptUuid);
    } else {
        // Create Request or Response: Call CreateMessage Controller Action to preview request/response
        $('#previewMessageIndicator').val(true);
    }
}

function CheckTransTransComplete(transactionTranscriptUuid) {
    var result = false;

    if (transactionTranscriptUuid != undefined && transactionTranscriptUuid != "") {
        cancel('Processing Transcript', 'Please wait while the transcript is being generated...');
        result = CheckTranTranResult(result, transactionTranscriptUuid);
    }

    return result;
}

function CheckTranTranResult(result, transactionTranscriptUuid) {
    
    var initMyLib = function () {
        //alert(currentAttempt + '. initMyLib is invoked\n');
        if (currentAttempt < maxAttempts) {
            currentAttempt++;

            $.ajax({
                type: 'POST',
                url: CheckTransCompletePath,
                data: { transactionTranscriptUuid: transactionTranscriptUuid },
                success: function (response) {
                    //alert('Success: ' + response.Success);
                    if (response.Success) {
                        //alert('Refresh Page!');
                        $('#previewMessageIndicator').val(true);
                        $('#waitingTranscript').val(false);
                        $('#createMessageForm').submit();
                    }
                },
                error: function (response) {
                    // legitimate error
                    error(errorTitle, errorMessage);
                }
            });

            setTimeout(initMyLib, 1000);
        } else {
            //alert(currentAttempt + '. End of Recursive Calls!\n');
            $("#dialog-cancel").html('Unable to contact Colleague! <br />Number of attempts: ' + currentAttempt);
        }
    }

    initMyLib();

    return result;
}

function ExportColleague(transmissionDataId) {
    var confirmmsg = 'Are you sure you wish to export the selected Transcript to Colleague?';
    var onsuccess = function () { ExportTranscripts(transmissionDataId); }
    confirm('Export Transcript to Colleague?', confirmmsg, onsuccess, null);
}

//function getSelectedTranscripts() {
//    var TransIdList = [];

//    $("#SelectAll").closest('table').find("tbody tr td:first-child input:checkbox").each(function () {
//        if (this.checked) {
//            TransIdList.push(this.name);
//        }
//    });
//    return TransIdList;
//}

function ExportTranscripts(transmissionDataId) {
    //var TransIdList = getSelectedTranscripts();
    var TransIdList = [];

    if (transmissionDataId) {
        TransIdList.push(transmissionDataId);
    }

    if (typeof TransIdList !== 'undefined' && TransIdList.length > 0) {
        // the array is defined and has at least one element
        $.ajax({
            type: 'POST',
            url: ExportTranscriptPath,
            data: { transmissionDataIdList: TransIdList },
            success: function (response) {
                if (response.Success) {
                    var refreshPage = function () {
                        $('#search-btn').click();  // Refresh page to not show exported transcript
                    };
                    message("Transcript Export", "Transcript was exported with success!", refreshPage);
                    return false;
                } else {
                    if (response.Message == 'NoSNumberFound') {
                        // Redirect to the Student Lookup page
                        LoadStudentLookup(transmissionDataId, 'ExportTranscriptToColleague');
                    } else {
                        error(errorTitle, response.Message + "<br/><br/>" + errorMessage);
                    }
                }
            },
            error: function (response) {
                error(errorTitle, errorMessage);
            }
        });
    }
}

function ParseResponse(transmissionDataUuid) {
    var confirmmsg = 'Are you sure you wish to parse the selected Transcript/Response?';
    var onsuccess = function () { ParseResponseCall(transmissionDataUuid); }
    confirm('Parse Transcript/Response?', confirmmsg, onsuccess, null);
}

function ParseResponseCall(transmissionDataUuid) {
    if (typeof transmissionDataUuid !== 'undefined' && transmissionDataUuid.length > 0) {
        // the array is defined and has at least one element
        $.ajax({
            type: 'POST',
            url: ParseResponsePath,
            data: { transmissionDataUuid: transmissionDataUuid },
            success: function (response) {
                if (response.Success) {
                    message("Transcript/Response Parse", "Transcript/Response was parsed with success!", null);
                    return false;
                } else {
                    error(errorTitle, response.Message + "<br/><br/>" + errorMessage);
                }
            },
            error: function (response) {
                error(errorTitle, errorMessage);
            }
        });
    }
}

function getSelectedRequestResponse() {
    var values = [];

    $("#SelectAll").closest('table').find("tbody tr td:first-child input:checkbox").each(function () {
        if (this.checked) {
            values.push(this.name);
        }
    });
    return values;
}

function PrintRequestResponseReport() {

    var values = getSelectedRequestResponse();

    if (values.length > 0) {
        ShowPopupIframe(750, 870);

        var keyType = $("#prntReqResBtn").attr("key");

        // Save list of ids for selected Requests/Responses and show report
        $.ajax({
            type: 'POST',
            url: PrintRequestResponseReportPath,
            data: { values: values, type: keyType },
            success: function (response) {
                if (response.Success) {
                    LoadIframe(response.Message);

                    // Mark Request or Response as viewed
                    if (keyType == 'Response') {
                        for (var i = 0; i < values.length; i++) {
                            $('#responseLine-' + values[i]).removeClass('text-bold').removeClass('yellow').css('background-color', 'white').mouseout(function () { $(this).css('background-color', 'white'); });
                        }
                    } else {
                        for (var i = 0; i < values.length; i++) {
                            $('#requestLine-' + values[i]).removeClass('text-bold').removeClass('yellow').css('background-color', 'white').mouseout(function () { $(this).css('background-color', 'white'); });
                        }
                    }
                }
            },
            error: function (response) {
                error(errorTitle, errorMessage);
            }
        });

    } else {
        warning("No Request/Response Selected!", "Please, select at least one Request/Response in order to print the report!");
    }
}


function SendUnsolicitedBatchTranscripts(filterFields) {
    var confirmmsg = 'Are you sure you wish to queue the sending of the Unsolicited Transcript Batch for the selected Students?';
    var onsuccess = function () { SendUnsolicitedBatchTranscriptsCall(filterFields); }
    confirm('Send Unsolicited Transcript Batch?', confirmmsg, onsuccess, null);
}

function SendUnsolicitedBatchTranscriptsCall(filterFields) {
    var values = getSelectedRequestResponse();

    var institutionId = $('#SearchFilter_DestinationInstitution').val();

    var allSelected = $('#selectAll').find('input[type="checkbox"]:checked').length > 0;

    // If at least one student was selected
    if (values.length > 0) {
        $.ajax({
            type: 'POST',
            url: SendUnsolicitedBatchTranscriptsPath,
            data: { values: values, institutionId: institutionId, allSelected: allSelected, filterFields: filterFields },
            success: function (response) {
                if (response.Success) {
                    message("Unsolicited Transcript Batch", "The Unsolicited Transcript Batch was queued with success!", null);
                    return false;
                } else {
                    error(errorTitle, response.Message + "<br/><br/>" + errorMessage);
                }
            },
            error: function (response) {
                error(errorTitle, errorMessage);
            }
        });
    } else {
        warning("No Student Selected!", "Please, select at least one Student in order to queue a Unsolicited Transcript Batch!");
    }
}

function GenerateMyCredsTranscriptBatch(filterFields) {
    var confirmmsg = 'Are you sure you wish to generate the MyCreds Transcript Batch for the selected Requests?';
    var successTitle = "Generate MyCreds Transcript Batch";
    var successMessage = "The MyCreds Transcript Batch was generated with success!";
    var warningTitle = "No Request Selected!";
    var warningMessage = "Please, select at least one Request in order to generate a MyCreds Transcript Batch!";
    var onsuccess = function () { GenerateMyCredsBatchCall(ExportTranscriptBatchPath, filterFields, successTitle, successMessage, warningTitle, warningMessage); }
    confirm('Generate MyCreds Transcript Batch?', confirmmsg, onsuccess, null);
}

function GenerateMyCredsBulkSend(filterFields) {
    var confirmmsg = 'Are you sure you wish to generate the MyCreds Bulk Send Batch for the selected Students?';
    var successTitle = "Generate MyCreds Bulk Send Batch";
    var successMessage = "The MyCreds Bulk Send Batch was generated with success!";
    var warningTitle = "No Student Selected!";
    var warningMessage = "Please, select at least one Student in order to generate a MyCreds Bulk Send Batch!";
    var onsuccess = function () { GenerateMyCredsBatchCall(ExportBulkSendBatchPath, filterFields, successTitle, successMessage, warningTitle, warningMessage); }
    confirm('Generate MyCreds Bulk Send Batch?', confirmmsg, onsuccess, null);
}

function GenerateMyCredsBatchCall(actionUrl, filterFields, successTitle, successMessage, warningTitle, warningMessage) {
    var values = getSelectedRequestResponse();

    var useMyCredsXsl = $('#useMyCredsXsl').is(':checked');

    var uploadMyCredsAPI = $('#uploadMyCredsAPI').is(':checked');

    var allSelected = $('#selectAll').find('input[type="checkbox"]:checked').length > 0;

    // If at least one request was selected
    if (values.length > 0) {
        $.ajax({
            type: 'POST',
            beforeSend: function () {
                $('.ajax-loader').css("visibility", "visible");
            },
            url: actionUrl,
            data: { requestIdList: values, useMyCredsXsl: useMyCredsXsl, allSelected: allSelected, filterFields: filterFields, uploadMyCredsAPI: uploadMyCredsAPI },
            success: function (response) {
                if (response.Success) {
                    message(successTitle, successMessage + "<br/><br/>" + response.Message, null);
                    return false;
                } else {
                    error(errorTitle, response.Message + "<br/><br/>" + errorMessage);
                }
            },
            error: function (response) {
                error(errorTitle, errorMessage);
            },
            complete: function () {
                $('.ajax-loader').css("visibility", "hidden");
            }
        });
    } else {
        warning(warningTitle, warningMessage);
    }
}

//function GenerateMyCredsBulkSendCall(filterFields) {
//    var values = getSelectedRequestResponse();

//    var useMyCredsXsl = $('#useMyCredsXsl').is(':checked');

//    var uploadMyCredsAPI = $('#uploadMyCredsAPI').is(':checked');

//    var allSelected = $('#selectAll').find('input[type="checkbox"]:checked').length > 0;

//    // If at least one request was selected
//    if (values.length > 0) {
//        $.ajax({
//            type: 'POST',
//            beforeSend: function () {
//                $('.ajax-loader').css("visibility", "visible");
//            },
//            url: ExportBulkSendBatchPath,
//            data: { acadCredIdList: values, useMyCredsXsl: useMyCredsXsl, allSelected: allSelected, filterFields: filterFields, uploadMyCredsAPI: uploadMyCredsAPI },
//            success: function (response) {
//                if (response.Success) {
//                    message("Generate MyCreds Bulk Send Batch", "The MyCreds Bulk Send Batch was generated with success!<br/><br/>" + response.Message, null);
//                    return false;
//                } else {
//                    error(errorTitle, response.Message + "<br/><br/>" + errorMessage);
//                }
//            },
//            error: function (response) {
//                error(errorTitle, errorMessage);
//            },
//            complete: function () {
//                $('.ajax-loader').css("visibility", "hidden");
//            }
//        });
//    } else {
//        warning("No Student Selected!", "Please, select at least one Student in order to generate a MyCreds Bulk Send Batch!");
//    }
//}

