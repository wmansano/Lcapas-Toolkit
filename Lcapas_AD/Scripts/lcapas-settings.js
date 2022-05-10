 $(document).ready(function () {
    SetupReportSettings();
    SetupPermissionSettings();
});


/*  Begin: Report Settings Functions  */

// Setup Report Settings
function SetupReportSettings() {

    //Setup Masks
    $("#asn").mask("999999999");

    // Print Student Daily Report 
    $("#prntRepBtn").click(function () {
        var filterFields = getFilterValues('Qty, Fromdate, Todate, StuID, Fullname, Type, Del, Comments, RecpFullName, RecpID, Street, City, Prv, PCode, AddOpr, AddDt');
        PrintReport('StudentDailyRequest', 750, 1050, JSON.stringify(filterFields))
    });
    
    // Print Login History Report
    $("#prntHistoryBtn").click(function () {
        var filterFields = getFilterValues('ActionID, ActionDesc, UserFullName, UserID, CreatedBy, CreatedDate, ModifiedBy, ModifiedBydDate');
        PrintReport('LoginHistory', 750, 1050, JSON.stringify(filterFields))
    });

    // Print Exception Error Report
    $("#prntErrorBtn").click(function () {
        PrintReport('ExceptionError', 750, 1200)
    });

    // Print Application Report
    $("#prntAppReportBtn").click(function () {
        var filterFields = getFilterValues('Fullname, ProgramCode, TermCode, CampusCode, sNumber, AgencyAssignedID, LanguageListCode, AdmitStatus, StudyLoad, ReferenceTypeDesc, PreviouslyApplied, Disability, ApplicationID, FromDate, ReceivedDateTime, PaidDateTime, PrevSNumber, AddressLine1, AddressLine2, City, Province, PostalCode, Country, AreaCode, PhoneNumber, Gender, BirthDate, EthnicityRace, FirstEntryIntoCountry, FamilyAttendedCollege, EmailAddress');
        PrintReport('ApplicationReport', 750, 1200, JSON.stringify(filterFields));
    });
    
    // Print Payment Report
    $("#prntPayReportBtn").click(function () {
        var filterFields = getFilterValues('Fullname, PrevSNumber, TermCode, ProgramCode, CampusCode, LanguageCode, BilltoName, BilltoEmail, BilltoStreet, BilltoCountry, BilltoZip, BilltoPhone, CardType, Method, RespMsg, Result, PaidDateTime, AddressLine1, AddressLine2, City, Province, PostalCode, Country, AreaCode, PhoneNumber, BirthDate, EmailAddress');
        PrintReport('PaymentReport', 750, 1200, JSON.stringify(filterFields));
    });

    // Print Hold Type Report
    $("#prntHoldTypeReportBtn").click(function () {
        var destAction = "RecordsInbox";
        if ($('#destAction').val()) { destAction = $('#destAction').val(); }
        var studentFields = getFilterValues('Snumber, FullName, ASN');
        var filterFields = getFilterValues('StudentRecord, DestinationOrganization, ListType, HoldType, HoldTypeData, Status, Operator, FromStatusDate, ToStatusDate');
        filterFields['StudentRecord'] = studentFields;
        PrintReport('HoldTypeReport', 750, 1200, JSON.stringify(filterFields), destAction)
    });

    // Print Sent Report
    $("#prntSentEmailReportBtn").click(function () {
        var filterFields = getFilterValues('EmailType, Subject, Body, From, To, CreatedBy, CreatedDateTime, ModifiedBy, ModifiedDateTime');
        PrintReport('SentEmailReport', 750, 1300, JSON.stringify(filterFields));
    });

    // Print CollProgramApplication Report
    $("#prntProgramApplicationReportBtn").click(function () {
        var filterFields = getFilterValues('ID, PID, FullName, EmailAddress, ASN, Program, StudyLoad, Term, Location, Citizenship, Ethnic, AdmitStatus, AlienStatus, ApplicationStatus, OrgCountry');
        PrintReport('CollProgramApplicationReport', 750, 1200, JSON.stringify(filterFields));
    });

    // Print Program Application Query Report
    $("#prntProgramApplicationQueryReportBtn").click(function () {
        PrintQuery('Application', 750, 1200)
    });

    // Print CollWaitList Report
    $("#prntWaitListReportBtn").click(function () {
        var filterFields = getFilterValues('APPL_APPLICANT, FullName, APPL_START_TERM, APPL_STATUS_DATE, APPL_CITIZENSHIP, APPL_VISA_STATUS, APPL_ACAD_PROGRAM, APPL_RESIDENCE');
        PrintReport('CollWaitListReport', 750, 1200, JSON.stringify(filterFields))
    });

    // Print WaitList Query Report
    $("#prntWaitListQueryReportBtn").click(function () {
        PrintQuery('WaitList', 750, 1200)
    });

    // Print CollAdmissionConditions Report
    $("#prntAdmissionConditionsReportBtn").click(function () {
        var filterFields = getFilterValues('PersonID, ApplicationStatus, FullName, Term, AlienStatus, ConditionProgram, Condition');
        PrintReport('CollAdmissionConditionsReport', 750, 1200, JSON.stringify(filterFields));
    }); 

    // Print AdmissionConditions Query Report
    $("#prntAdmissionConditionsQueryReportBtn").click(function () {
        PrintQuery('Admission', 750, 1200)
    });

    // Print Testing Results Report
    $("#prntTestingResultsReportBtn").click(function () {
        var filterFields = getFilterValues('PERSON_ID, LAST_NAME, FIRST_NAME, MIDDLE_NAME, STATUS_DATE');
        PrintReport('CollTestingResultsReport', 750, 1200, JSON.stringify(filterFields))
    });

    //Print Testing Query Report
    $("#prntTestingResultsQueryReportBtn").click(function () {
        PrintQuery('Testing', 750, 1050)
    });

    // Print Overdues Report
    $("#prntOverduesReportBtn").click(function () {
        var filterFields = getFilterValues('ID, FullName, AlienStatus, Status, CondProgram, CondStatus, Phone1, Type1, Phone2, Type2, Deadline, StartTerm, Location, Comments');
        PrintReport('CollOverduesReport', 750, 1200, JSON.stringify(filterFields))
    });

    //Print Overdues Query Report
    $("#prntOverduesQueryBtn").click(function () {
        PrintQuery('Overdues', 750, 1050)
    });

    //highlight the background color on comments with 'APAS' text
    $('tr.tableBody-student-daily-request').each(function () {
        if ($(this).find('.report-comments:contains("APAS")').length > 0) {
            $(this).addClass("yellow");
        }
    });

    // Export to Excel: Admission Conditions Report
    $("#excelAdmissionConditionsReportBtn").click(function () {
        var filterFields = getFilterValues('PersonID, ApplicationStatus, FullName, Term, AlienStatus, ConditionProgram, Condition');
        ExportToExcel('AdmissionConditionsReport', JSON.stringify(filterFields))
    });

    // Export to Excel: Wait List Report
    $("#excelWaitListReportBtn").click(function () {
        var filterFields = getFilterValues('APPL_APPLICANT, FullName, APPL_START_TERM, APPL_STATUS_DATE, APPL_CITIZENSHIP, APPL_VISA_STATUS, APPL_ACAD_PROGRAM, APPL_RESIDENCE');
        ExportToExcel('WaitListReport', JSON.stringify(filterFields))
    });

    // Export to Excel: Daily Request Report
    $("#excelDailyRequestReportBtn").click(function () {
        var filterFields = getFilterValues('Qty, Fromdate, Todate, StuID, Fullname, Type, Del, Comments, RecpFullName, RecpID, Street, City, Prv, PCode, AddOpr, AddDt');
        ExportToExcel('DailyRequestReport', JSON.stringify(filterFields))
    });

    // Export to Excel: Overdues Report
    $("#excelOverduesReportBtn").click(function () {
        var filterFields = getFilterValues('ID, FullName, AlienStatus, Status, CondProgram, CondStatus, Phone1, Type1, Phone2, Type2, Deadline, StartTerm, Location, Comments');
        ExportToExcel('OverduesReport', JSON.stringify(filterFields))
    });

    // Export to Excel: test Results Report
    $("#excelTestingResultsReportBtn").click(function () {
        var filterFields = getFilterValues('PERSON_ID, LAST_NAME, FIRST_NAME, MIDDLE_NAME, STATUS_DATE');
        ExportToExcel('TestingResultsReport', JSON.stringify(filterFields))
    });
    // Export to Excel: Program Applications Report
    $("#excelProgramApplicationReportBtn").click(function () {
        var filterFields = getFilterValues('ID, PID, FullName, EmailAddress, ASN, Program, StudyLoad, Term, Location, Citizenship, Ethnic, AdmitStatus, AlienStatus, ApplicationStatus, OrgCountry');
        ExportToExcel('WebApplicationReport', JSON.stringify(filterFields))
    });
    // Export to Excel: HoldType Report
    $("#excelHoldTypeReportBtn").click(function () {
        var destAction = "RecordsInbox";
        if ($('#destAction').val()) { destAction = $('#destAction').val(); }
        var studentFields = getFilterValues('Snumber, FullName, ASN');
        var filterFields = getFilterValues('StudentRecord, DestinationOrganization, ListType, HoldType, HoldTypeData, Status, Operator, FromStatusDate, ToStatusDate');
        filterFields['StudentRecord'] = studentFields;
        ExportToExcel('HoldTypeReport', JSON.stringify(filterFields), destAction)
    });

    // Export to Excel: Toolkit Application Report
    $("#excelAppReportBtn").click(function () {
        var filterFields = getFilterValues('Fullname, ProgramCode, TermCode, CampusCode, sNumber, AgenexcelAppReportBtncyAssignedID, LanguageListCode, AdmitStatus, StudyLoad, ReferenceTypeDesc, PreviouslyApplied, Disability, ApplicationID, FromDate, ReceivedDateTime, PaidDateTime, PrevSNumber, AddressLine1, AddressLine2, City, Province, PostalCode, Country, AreaCode, PhoneNumber, Gender, BirthDate, EthnicityRace, FirstEntryIntoCountry, FamilyAttendedCollege, EmailAddress');
        ExportToExcel('ToolkitApplicationReport', JSON.stringify(filterFields))
    });

    // Print Transfer Credit Report 
    $("#prntTransCredBtn").click(function () {
        var filterFields = getFilterValues('ASN, BirthDate, Provider, AcademicYear, ProgramID, SpecializationID, FromInstitution, FromInstitutionLoc, FromInstitutionCourseCode, FromInstitutionAcademicYearCourseTaken, TCACourseCode, TCAForCourseTransfer, TCAAcadmicYear, TCTBYCourse, TCTForPLAR');
        PrintReport('TranserCreditReport', 750, 1050, JSON.stringify(filterFields))
    });

    // Export to Excel: Transfer Credits
    $("#excelTransCredBtn").click(function () {
        var filterFields = getFilterValues('ASN, BirthDate, Provider, AcademicYear, ProgramID, SpecializationID, FromInstitution, FromInstitutionLoc, FromInstitutionCourseCode, FromInstitutionAcademicYearCourseTaken, TCACourseCode, TCAForCourseTransfer, TCAAcadmicYear, TCTBYCourse, TCTForPLAR');
        ExportToExcel('TransferCreditReport', JSON.stringify(filterFields))
    });

    // XML Export Transfer Credit Report 
    $("#xmlTransCredBtn").click(function () {
        var filterFields = getFilterValues('ASN, BirthDate, Provider, AcademicYear, ProgramID, SpecializationID, FromInstitution, FromInstitutionLoc, FromInstitutionCourseCode, FromInstitutionAcademicYearCourseTaken, TCACourseCode, TCAForCourseTransfer, TCAAcadmicYear, TCTBYCourse, TCTForPLAR');
        ExportToXML('TransferCreditReportXML', JSON.stringify(filterFields))
    });
}


function PrintQuery(docType, height, width) {

    ShowPopupIframe(height, width);
    $.ajax({
        type: 'POST',
        url: PrintQueryPath,
        data: {  docType: docType },
        success: function (response) {
            if (response.Success) {
                LoadIframe(response.Message);
            }
        },
        error: function (response) {
            error(errorTitle, errorMessage);
        }
    });
}


/*  End: Report Settings Functions  */



/*  Begin: User Access Settings Functions  */

//Setup DataShare import Event
function SaveUserAccessGroup(userId, userFullName) {
    var confirmmsg = 'Are you sure you wish to save the Access Group changes for the following user? <br><br> ' + userFullName;
    var onsuccess = function () { SaveUserAccessGroupToController(userId); }
    confirm('Save Access Group for User?', confirmmsg, onsuccess, null);
};

function SaveUserAccessGroupToController(userId) {
    var accessGroupIdList = [];

    $('.access-group-' + userId).find('input[type="checkbox"]:checked').each(function () {
        accessGroupIdList.push($(this).attr('class').replace('access-group-check-', ''));
    });

    if (userId !== null) {
        // Save User Access Group
        $.ajax({
            type: 'POST',
            url: SetUsersGroupsPath,
            data: { userId: userId, accessGroupIdList: accessGroupIdList },
            success: function (response) {
                if (response.Success == false) {
                    error(errorTitle, response.Message);
                }
            },
            error: function (response) {
                error(errorTitle, errorMessage);
            }
        });
    }
}

/*  End: Report Settings Functions  */

/*  Begin: Permission Settings Functions  */

// Setup Report Settings
function SetupPermissionSettings() {

    // Create an item
    var itemForm = $('#item-form');
    var itemDialog = $('#dialog-form');

    var itemControllerType = $('#dialog-form #ControllerType');
    var itemControllerDesc = $('#dialog-form #ControllerDesc');

    var itemActionType = $('#dialog-form #ActionType');
    var itemActionDesc = $('#dialog-form #ActionDesc');
    var itemActionControllerId = $('#dialog-form #ControllerId');
    var itemExternalAction = $('#dialog-form #ExternalAction');

    var itemPermissionActionType = $('#dialog-form #PermissionActionType');
    var itemPermissionDesc = $('#dialog-form #PermissionDesc');
    var itemPermissionAccessGroup = $('#dialog-form #PermissionAccessGroup');
    var itemPermissionEnable = $('#dialog-form #PermissionEnable');

    var saveType = $('#permission-save-type');
    var itemFields = $([]).add(itemControllerType).add(itemControllerDesc)
        .add(itemActionType).add(itemActionDesc).add(itemExternalAction).add(itemActionControllerId)
        .add(itemPermissionActionType).add(itemPermissionDesc).add(itemPermissionAccessGroup).add(itemPermissionEnable);

    // Clear Fields
    function clearItems() {
        itemControllerType.val('');
        itemControllerDesc.val('');

        itemActionType.val('');
        itemActionDesc.val('');
        itemExternalAction.val('');

        itemPermissionActionType.val('');
        itemPermissionDesc.val('');
        itemPermissionAccessGroup.val('');
    }

    // Fields validation
    function saveItem() {
        var valid = true;
        itemFields.removeClass("ui-state-error");
        switch (saveType.val()) {
            case '0':  // Controller Type
                if (itemControllerType.val().length < 1) {
                    valid = false;
                    itemControllerType.addClass('ui-state-error');
                }
                if (itemControllerDesc.val().length < 1) {
                    valid = false;
                    itemControllerDesc.addClass('ui-state-error');
                }
                break;
            case '1':  // Action Type
                if (itemActionType.val().length < 1) {
                    valid = false;
                    itemActionType.addClass('ui-state-error');
                }
                if (itemActionDesc.val().length < 1) {
                    valid = false;
                    itemActionDesc.addClass('ui-state-error');
                }
                if (itemActionControllerId.val().length < 1) {
                    valid = false;
                    itemActionControllerId.addClass('ui-state-error');
                }
                break;
            case '2':  // Permission Record
                if (itemPermissionActionType.val().length < 1) {
                    valid = false;
                    itemPermissionActionType.addClass('ui-state-error');
                }
                if (itemPermissionDesc.val().length < 1) {
                    valid = false;
                    itemPermissionDesc.addClass('ui-state-error');
                }
                if (itemPermissionAccessGroup.val().length < 1) {
                    valid = false;
                    itemPermissionAccessGroup.addClass('ui-state-error');
                }
                break;
        }
        if (valid) {
            dialog.dialog("close");
        }
        return valid;
    }

    // Dialog form
    var dialog = itemDialog.dialog({
        autoOpen: false,
        height: 250,
        width: 550,
        modal: true,
        position: { at: "center top+40%" },
        show: { effect: "blind" },
        hide: { effect: "blind" },
        buttons: {
            Save: {
                text: 'Save',
                "id": 'search-btn',
                click: function () {
                    if (saveItem()) {
                        if (itemForm.attr('item') == 'create') {
                            switch (saveType.val()) {
                                case '0':  // Controller Type
                                    createItem(itemControllerType.val(), itemControllerDesc.val(), saveType.val(), false, null, createPermissionTypeUrl);
                                    break;
                                case '1':  // Action Type
                                    createItem(itemActionType.val(), itemActionDesc.val(), saveType.val(), itemExternalAction.is(':checked'), itemActionControllerId.val(), createPermissionTypeUrl);
                                    break;
                                case '2':  // Permission Record
                                    createItem(itemPermissionActionType.val(), itemPermissionDesc.val(), saveType.val(), itemPermissionEnable.is(':checked'), itemPermissionAccessGroup.val(), createPermissionTypeUrl);
                                    break;
                            }
                        }
                    }
                }
            },
            Cancel: function () {
                dialog.dialog("close");
            }
        },
        open: function () {
            $('.ui-dialog-buttonset button').addClass('btn btn-default button');
            $(this).dialog('option', 'height', saveType.val() == 0 ? 200 : 250);
        },
        close: function () {
            itemFields.removeClass("ui-state-error");
        },
    });

    // Create Item
    $(".create-item").click(function () {
        itemForm.attr('item', 'create');
        clearItems();
        var ItemType = $(this).attr('item-type');
        saveType.val(ItemType);
        showHideItems(saveType.val());

        //itemActive.prop('checked', true);
        itemDialog.dialog("option", "title", itemDialog.attr('title')).dialog("open");
    });

    // Show/Hide Items
    function showHideItems(type) {
        itemFields.closest(".form-group").addClass("hide");
        switch (type) {
            case '0':  // Controller Type
                itemDialog.attr('title', 'Create Controller Type');
                itemControllerType.closest(".form-group").removeClass("hide");
                itemControllerDesc.closest(".form-group").removeClass("hide");
                break;
            case '1':  // Action Type
                itemDialog.attr('title', 'Create Action Type');
                itemActionType.closest(".form-group").removeClass("hide");
                itemActionDesc.closest(".form-group").removeClass("hide");
                itemActionControllerId.closest(".form-group").removeClass("hide");
                itemExternalAction.closest(".form-group").removeClass("hide");
                break;
            case '2':  // Permission Record
                itemDialog.attr('title', 'Create Permission Record');
                itemPermissionActionType.closest(".form-group").removeClass("hide");
                itemPermissionDesc.closest(".form-group").removeClass("hide");
                itemPermissionAccessGroup.closest(".form-group").removeClass("hide");
                itemPermissionEnable.closest(".form-group").removeClass("hide");
                break;
        }
    }

    // Create Item
    function createItem(itemCode, itemDesc, itemType, active, objectId, createUrl) {
        $.ajax({
            type: 'POST',
            url: createUrl,
            data: { itemCode: itemCode, itemDesc: itemDesc, type: itemType, active: active, objectId: objectId },
            success: function (response) {
                if (response.Success == false) {
                    error(errorTitle, response.Message);
                }
                else {
                    var onsuccess = function () { location.reload(); }
                    message("Item created with success", "The item was created with success!", onsuccess);
                }
            },
            error: function (response) {
                error(errorTitle, errorMessage);
            }
        });
    }

    // Delete Item
    $(".delete-item").click(function (e) {
        e.preventDefault();
        var itemId = $(this).attr('item');
        var itemDesc = $(this).attr('code') + ' - ' + $(this).attr('desc');
        var itemType = $(this).attr('obj-type');
        var itemTypeCode = $(this).attr('type-code');
        var confirmmsg = 'Are you sure you wish to delete this ' + itemType + '?<br><br> ' + itemDesc;
        var onsuccess = function () { deleteItem(itemId, itemTypeCode, deletePermissionTypeUrl); }
        confirm('Delete this ' + itemType + '?', confirmmsg, onsuccess, null);
    });

    // Delete Item
    function deleteItem(itemId, itemType, deleteUrl) {
        $.ajax({
            type: 'POST',
            url: deleteUrl,
            data: { id: itemId, type: itemType },
            success: function (response) {
                if (response.Success) {
                    var onsuccess = function () { location.reload(); }
                    message("Item deleted with success", "The item was deleted with success!", onsuccess);
                } else {
                    error(errorTitle, response.Message);
                }
            },
            error: function (response) {
                error(errorTitle, errorMessage);
            }
        });
    }
}

/*  End: Permission Settings Functions  */


