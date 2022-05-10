
$(document).ready(function () {
    SetupApplicationReport();
    SetupApplicationSettings();
    SetupParameterSettings();
    SetupRelationSettings();
});

/*  Begin: Application Report Functions  */

function SetupApplicationReport() {

    //Setup Masks
    $("#asn").mask("999999999");

    //Setup Print Application Events
    $("#prntAppBtn").click(function () {
        PrintApplicationReport();
    });

    // Setup export button
    $("#export").click(function () {
        var confirmmsg = 'Are you sure you wish to export the selected application(s)?';
        var onsuccess = function () { ExportApplications(); }
        confirm('Export Applications?', confirmmsg, onsuccess, null);
    });

}

// Print Application Report
function PrintApplicationReport() {
    var values = getSelectedItems();

    PrintApplications(values);
}

// Print a single application
function PrintSingleApplication(uuid) {
    var values = [];
    values.push(uuid);

    PrintApplications(values);
}

// Print Applications
function PrintApplications(values) {
    if (values.length > 0) {
        ShowPopupIframe(750, 870);

        $.ajax({
            type: 'POST',
            url: PrintApplicationReportPath,
            data: { values: values },
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
        warning("No Applications Selected!", "Please, select at least one Application in order to print the report!");
    }
}

// Export Applications
function ExportApplications() {
    var values = getSelectedItems();

    $.ajax({
        type: 'POST',
        url: ExportApplicationsPath,
        data: { values: values  },
        success: function (response) {
            if (response.Success == null) {
                error(errorTitle, errorMessage);
            } else {
                if (response.Success) {
                    window.location = ApplicationsIndexPath;
                    return false;
                } else {
                    error(errorTitle, response.Message);
                }
            }
        },
        error: function (response) {
            error(errorTitle, errorMessage);

        }
    });
}

/*  End: Application Report Functions  */


/*  Begin: Application Settings: Program, Campus, Term and Reference Type Functions  */

function SetupApplicationSettings() {
    $('#list-item').collapse('show');

    // Draggable and Droppable Options
    $('#list-item').sortable({
        placeholder: 'list-group-item ui-state-highlight',
        revert: 'invalid',
        update: function (event, ui) {
            updateItemOrder(orderItemUrl);
        },
    }).disableSelection();

    // Create an item
    var itemCode = $('#dialog-form #ItemCode');
    var itemDesc = $('#dialog-form #ItemDesc');
    var itemActive = $('#dialog-form #Active');
    var itemMessage = $('#dialog-form #ItemMessage');
    var itemSNumber = $('#userdialog-form #ItemsNumber');
    var itemFirstName = $('#userdialog-form #ItemFirstName');
    var itemLastName = $('#userdialog-form #ItemLastName');
    var useritemActive = $('#userdialog-form #Active');
    var itemFields = $([]).add(itemCode).add(itemDesc);
    var useritemFields = $([]).add(itemSNumber).add(itemFirstName).add(itemLastName);
    var itemForm = $('#item-form');
    var itemType = $('#list-item').attr('item-type');

    // Fields validation
    function saveItem() {
        var valid = true;
        itemFields.removeClass("ui-state-error");
        if (itemCode.val().length < 1) {
            valid = false;
            itemCode.addClass('ui-state-error');
        }
        if (itemDesc.val().length < 1) {
            valid = false;
            itemDesc.addClass('ui-state-error');
        }
        if (valid) {
            dialog.dialog("close");
        }
        return valid;
    }

    // Save User Item
    function saveUserItem() {
        var valid = true;
        useritemFields.removeClass("ui-state-error");
        if (itemSNumber.val().length < 1) {
            valid = false;
            itemSNumber.addClass('ui-state-error');
        }
        if (itemFirstName.val().length < 1) {
            valid = false;
            itemFirstName.addClass('ui-state-error');
        }
        if (itemLastName.val().length < 1) {
            valid = false;
            itemLastName.addClass('ui-state-error');
        }
        if (valid) {
            userdialog.dialog("close");
        }
        return valid;
    }

    // Dialog form
    dialog = $("#dialog-form").dialog({
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
                            createItem(itemCode.val(), itemDesc.val(), itemType, itemMessage.val(), itemActive.is(':checked'), createItemUrl);  // Create Item
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
            // Show/Hide active option
            var showActive = toBoolean($("#item-form").attr('showActive'));
            $(this).find('.active-option').toggle(showActive);
            $(this).dialog('option', 'height', showActive ? 230: 210);
        },
        close: function () {
            itemFields.removeClass("ui-state-error");
        },
    });

    // User Dialog Form
    userdialog = $("#userdialog-form").dialog({
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
                    if (saveUserItem()) {
                        if (itemForm.attr('item') == 'create') {
                            createUserItem(itemSNumber.val(), itemFirstName.val(), itemLastName.val(), itemType, useritemActive.is(':checked'), createItemUrl);  // Create Item
                        }
                    }
                }
            },
            Cancel: function () {
                userdialog.dialog("close");
            }
        },
        open: function () {
            $('.ui-dialog-buttonset button').addClass('btn btn-default button');
            // Show/Hide active option
            var showActive = toBoolean($("#item-form").attr('showActive'));
            $(this).find('.active-option').toggle(showActive);
            $(this).dialog('option', 'height', showActive ? 250 : 230);
        },
        close: function () {
            useritemFields.removeClass("ui-state-error");
        },
    });

    // Create Item
    $(".create-item").click(function () {
        itemForm.attr('action', createItemUrl).attr('item', 'create');
        itemCode.val('');
        itemDesc.val('');
        itemActive.prop('checked', true);
        dialog.dialog("option", "title", "Create " + itemType).dialog("open");
    });

    //Create User Item
    $(".create-useritem").click(function () {
        itemForm.attr('action', createItemUrl).attr('item', 'create');
        itemSNumber.val('');
        itemFirstName.val('');
        itemLastName.val('');
        useritemActive.prop('checked', true);
        userdialog.dialog("option", "title", "Create " + itemType).dialog("open");
    });

    // Delete Item
    $(".delete-item").click(function () {
        var itemId = $(this).attr('item');
        var itemDesc = $(this).attr('code') + ' - ' + $(this).attr('desc');
        var confirmmsg = 'Are you sure you wish to delete this ' + itemType + '?<br><br> ' + itemDesc;
        var onsuccess = function () { deleteItem(itemId, deleteItemUrl); }
        confirm('Delete this ' + itemType + '?', confirmmsg, onsuccess, null);
    });

    // Delete User Item
    $(".delete-useritem").click(function () {
        var itemId = $(this).attr('item');
        var itemUserFullname = $(this).attr('firstName') + ' - ' + $(this).attr('lastName');
        var confirmmsg = 'Are you sure you wish to delete this ' + itemType + '?<br><br> ' + itemUserFullname;
        var onsuccess = function () { deleteItem(itemId, deleteItemUrl); }
        confirm('Delete this ' + itemType + '?', confirmmsg, onsuccess, null);
    });

    // Activate/Deactivate Item
    $(".activate-item").click(function () {
        var itemId = $(this).attr('item');
        var active = toBoolean($(this).attr('active-item'));
        var itemDesc = $(this).attr('code') + ' - ' + $(this).attr('desc');
        var itemFullName = $(this).closest('li#'+itemId);
        var confirmmsg = 'Are you sure you wish to ' + (active ? 'deactivate' : 'activate') + ' this ' + itemType + '?<br><br> ' + itemDesc;
        var onsuccess = function () { activateItem(itemId, itemType, !active, '.activate-item', activateItemUrl); }
        confirm((active ? 'Deactivate' : 'Activate') + ' this ' + itemType + '?', confirmmsg, onsuccess, null);
    });

    // Activate/Deactivate User Item
    $(".activate-useritem").click(function () {
        var itemId = $(this).attr('item');
        var active = toBoolean($(this).attr('active-item'));
        var itemUserFullname = $(this).closest('li#' + itemId).find('.item-content').text();
        var confirmmsg = 'Are you sure you wish to ' + (active ? 'deactivate' : 'activate') + ' this ' + itemType + '?<br><br> ' + itemUserFullname;
        var onsuccess = function () { activateItem(itemId, itemType, !active, '.activate-useritem', activateItemUrl); }
        confirm((active ? 'Deactivate' : 'Activate') + ' this ' + itemType + '?', confirmmsg, onsuccess, null);
    });

    // Add/Remove Government Pending Approval
    $(".pending-item").click(function () {
        var itemId = $(this).attr('item');
        var hasPending = toBoolean($(this).attr('pending-item'));
        var itemDesc = $(this).attr('code') + ' - ' + $(this).attr('desc');
        var confirmmsg = 'Are you sure you wish to ' + (hasPending ? 'remove' : 'add') + ' "Pending Government Approval" to this program?<br><br> ' + itemDesc;
        var onsuccess = function () { pendingItem(itemId, !hasPending, pendingItemUrl); }
        confirm((hasPending ? 'Remove' : 'Add') + ' Pending Government Approval', confirmmsg, onsuccess, null);
    });

    // Order Item
    $(".order-item").click(function () {
        var sortableLists = $('#list-item');
        $(sortableLists).each(function (index, element) {
            var listitems = $('li', element);

            listitems.sort(function (a, b) {
                var ta = $(a).find('.item-content').text().toUpperCase();
                var tb = $(b).find('.item-content').text().toUpperCase();
                return ta.localeCompare(tb);
            });
            $(element).append(listitems);
        });

        // Save new item order
        updateItemOrder(orderItemUrl);
    });

}

// Delete Item
function deleteItem(itemId, deleteUrl) {
    $.ajax({
        type: 'POST',
        url: deleteUrl,
        data: { id: itemId },
        success: function (response) {
            if (response.Success) {
                $('#list-item #' + itemId).remove();
            } else {
                error(errorTitle, response.Message);
            }
        },
        error: function (response) {
            error(errorTitle, errorMessage);
        }
    });
}

// Create Item
function createItem(itemCode, itemDesc, itemType, itemMessage, active, createUrl) {
    if (itemMessage) {
        itemMessage = itemMessage.trim().replaceAll('"', "'");
    }
    var dataToSend = JSON.stringify({ 'itemCode': itemCode, 'itemDesc': itemDesc, 'itemMessage': itemMessage, 'active': active });
    $.ajax({
        type: 'POST',
        url: createUrl,
        data: dataToSend,
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.Success == false) {
                error(errorTitle, response.Message);
            }
            else {
                if (itemMessage) {
                    location.reload();
                } else {
                    addItemList(response.ItemId, itemCode, itemDesc, itemType, active);
                }
            }
        },
        error: function (response) {
            error(errorTitle, errorMessage);
        }
    });
}

// Add new item to the list
function addItemList(itemId, itemCode, itemDesc, itemType, active) {
    // Duplicate an item and change item values
    var ul = $('#list-item');
    var li = ul.find('li:first').clone('true');
    li.attr('id', itemId);
    li.find('.item-content').html(itemDesc + ' (' + itemCode + ')');
    li.find('.delete-item').attr('item', itemId).attr('code', itemCode).attr('desc', itemDesc);

    // Check if item is disabled or not
    li.removeClass('pct-disabled');
    if (!active) { li.addClass('pct-disabled'); }
    li.find('.activate-item').attr('item', itemId).attr('code', itemCode).attr('desc', itemDesc);
    li.find('.activate-item').attr('active-item', active).attr('title', (active ? "Active " : "Inactive ") + itemType).attr('src', imagePath + (!active ? "disable" : "check") + '.png');

    li.prependTo(ul);

    // Save new item order
    updateItemOrder(orderItemUrl);
}

// Activate Item
function activateItem(itemId, itemType, active, activeItem, activateUrl) {
    var parameters = { id: itemId, active: active };
    var onsuccess = function () { activateFrontEndItem(itemId, itemType, active, activeItem); }
    ajaxPostControllerCall(activateUrl, parameters, onsuccess);
}

// Activate Front End Item
function activateFrontEndItem(itemId, itemType, active, activeItemName) {
    activeItem = $('#list-item #' + itemId);
    activeItem.removeClass('pct-disabled');
    input_image = activeItem.find(activeItemName);
    input_image.attr('active-item', active);
    image_src = input_image.attr('src');
    if (!active) {
        activeItem.addClass('pct-disabled');
        image_src = image_src.replace('check.png', 'disable.png');
    } else {
        image_src = image_src.replace('disable.png', 'check.png');
    }
    input_image.attr('src', image_src).attr('title', (active ? "Active " : "Inactive ") + itemType);
}

// Add/Remove Pending Government Approval
function pendingItem(itemId, pending, pendingUrl) {
    var parameters = { id: itemId, pending: pending };
    var onsuccess = function () { window.location.reload(true); }
    ajaxPostControllerCall(pendingUrl, parameters, onsuccess);
}

// Save item order
function updateItemOrder(orderUrl) {
    var parameters = { itemOrder: $('#list-item').sortable('toArray') };
    ajaxPostControllerCall(orderUrl, parameters);
}

/*  End: Application Settings: Program, Campus, Term and Reference Type Functions  */


/*  Begin: Parameter Settings Functions  */

function SetupParameterSettings() {
    var settingList = $('#main-setting-list ul.settingOptions');
    settingList.collapse('show');

    // Draggable and Droppable Options
    settingList.sortable({
        placeholder: 'list-group-item ui-state-highlight',
        revert: 'invalid',
        update: function (event, ui) {
            var categoryId = this.id.replace('list-', '');
            updateParameterOrder(orderItemUrl, categoryId);
        },
    }).disableSelection();

    $('#main-setting-list').sortable({
        placeholder: 'list-group-item ui-state-highlight',
        revert: 'invalid',
        axis: 'y',
        update: function (event, ui) {
            updateParameterOrder(orderCategorygUrl);
        },
    }).disableSelection();

    // Expand/Collapse function
    $('.expand-all').on('click', function () {
        expandCollapse(settingList, 'show');
    });
    $('.collapse-all').on('click', function () {
        expandCollapse(settingList, 'hide');
    });

    // Order Parameter
    $(".order-parameter").click(function () {
        var sortableLists = $('#main-setting-list ul.settingOptions');
        $(sortableLists).each(function (index, element) {
            var categoryId = element.id.replace('list-', '');
            var listitems = $('li', element);

            listitems.sort(function (a, b) {
                var ta = $(a).find('.setting-content').text().toUpperCase();
                var tb = $(b).find('.setting-content').text().toUpperCase();
                return ta.localeCompare(tb);
            });
            $(element).append(listitems);

            // Save new program order
            updateParameterOrder(orderItemUrl, categoryId);
        });
    });

}

// Expand/Collapse and update image icon
function expandCollapse(element, option) {
    if (option == 'show') {
        element.parent('.list-group').find('a.arrow').removeClass('collapsed');
    } else {
        element.parent('.list-group').find('a.arrow').addClass('collapsed');
    }
    element.collapse(option);
}

// Save parameter order
function updateParameterOrder(orderUrl, categoryId) {
    var itemList = null;
    if (categoryId != null) {
        var itemList = $('#main-setting-list #' + categoryId + ' ul.settingOptions').sortable('toArray');
    } else {
        var itemList = $('#main-setting-list').sortable('toArray');
    }
    var parameters = { itemOrder: itemList };
    ajaxPostControllerCall(orderUrl, parameters);
}

/*  End: Parameter Settings Functions  */

/* Begin: User settings Functions  */

// Create User Item
function createUserItem(itemsnumber, itemfirstname, itemlastname, itemType, active, createUrl) {
    $.ajax({
        type: 'POST',
        url: createUrl,
        data: { snumber: itemsnumber, firstname: itemfirstname, lastname: itemlastname,  active: active },
        success: function (response) {
            if (response.Success == false) {
                error(errorTitle, response.Message);
            }
            else {
                addUserItemList(response.ItemId, itemsnumber, itemfirstname, itemlastname, itemType, active);
            }
        },
        error: function (response) {
            error(errorTitle, errorMessage);
        }
    });
}

// Add new user item to the list
function addUserItemList(itemId, itemsnumber, itemfirstname, itemlastname, itemType, active) {
    // Duplicate an item and change item values
    var ul = $('#list-item');
    var li = ul.find('li:first').clone('true');
    li.attr('id', itemId);
    li.find('.item-content').html(itemfirstname + ' ' + itemlastname +' (' + itemsnumber + ')');
    li.find('.delete-useritem').attr('item', itemId).attr('firstname', itemfirstname).attr('lastname', itemlastname);

    // Check if item is disabled or not
    li.removeClass('pct-disabled');
    if (!active) { li.addClass('pct-disabled'); }
    li.find('.activate-useritem').attr('item', itemId).attr('firstname', itemfirstname).attr('lastname', itemlastname);
    li.find('.activate-useritem').attr('active-item', active).attr('title', (active ? "Active " : "Inactive ") + itemType).attr('src', imagePath + (!active ? "disable" : "check") + '.png');

    li.prependTo(ul);

    // Save new user item order
    updateItemOrder(orderItemUrl);
}


// Fields validation
// Activate User Item
//function activateUserItem(itemId, itemType, active, activateUrl) {
//    var parameters = { id: itemId, active: active };
//    var onsuccess = function () { activateFrontEndItem(itemId, itemType, active); }
//    ajaxPostControllerCall(activateUrl, parameters, onsuccess);
//}

/*  End User settings Functions  */

/*  Begin: Relation Settings Functions  */

function SetupRelationSettings() {
    // Count terms selected
    countTermsSelected();

    // Draggable and Droppable Options
    var campusContainer = $('.campusOptions ul.termOptions');

    $('#list-term').sortable({
        connectWith: campusContainer,
        helper: 'clone',
        placeholder: 'list-group-item ui-state-highlight',
        revert: 'invalid',
        remove: function (event, ui) {
            ui.item.clone().insertAfter(ui.item);
            $(this).sortable('cancel');
        },
        receive: function (event, ui) {
            if ($(this).find("#" + ui.item.attr("id")).length > 0) {
                ui.item.remove();
            }
        },
        beforeStop: function (event, ui) {
            if (!$('#ApplicationProgram').val()) {
                $(this).sortable('cancel');
                warning("Select a program.", 'Please, select a program!');
            }
        },
        update: function (event, ui) {
            updateTermOrder(orderTermUrl, $('#list-term'));
        },
    }).disableSelection();

    campusContainer.sortable({
        connectWith: '#list-term',
        placeholder: 'list-group-item ui-state-highlight',
        revert: true,
        remove: function (event, ui) {
            var programId = $('#ApplicationProgram').val();
            var campusId = this.id.replace('list-', '');
            var termId = ui.item.attr('id');
            deleteProgramDetail(programId, campusId, termId, $(this));
            countTermsSelected();  // Count terms selected
        },
        receive: function (event, ui) {
            if ($(this).find(".term-" + ui.item.attr("id")).length > 1) {
                $(this).find(".term-" + ui.item.attr("id") + ':not(:first)').remove();
            } else {
                // Save relation to database
                var programId = $('#ApplicationProgram').val();
                var campusId = this.id.replace('list-', '');
                var termId = ui.item.attr('id');
                var active = !ui.item.hasClass('pct-disabled');
                saveProgramDetail(programId, campusId, termId, active);
            }
            countTermsSelected();  // Count terms selected
        },
        update: function (event, ui) {
            var programId = $('#ApplicationProgram').val();
            var campusId = this.id.replace('list-', '');
            updateProgramDetailOrder(programId, campusId);
        },
    });

    $('#main-campus-list').sortable({
        placeholder: 'list-group-item ui-state-highlight',
        revert: true,
        update: function (event, ui) {
            updateCampusOrder(orderCampusUrl);
        },
    });

    $('#form-container ul, #form-container li').disableSelection();

    // Expand/Collapse function
    $('.expand-all').on('click', function () {
        expandCollapseAll('show');
    });
    $('.collapse-all').on('click', function () {
        expandCollapseAll('hide');
    });

    // Get list of terms and campuses available
    $('#ApplicationProgram').change(function () {
        if (!$(this).val()) {
            $('.campusOptions .termOptions li').remove(); // Clean campus options for terms
            countTermsSelected();  // Count terms selected
        } else {
            $.ajax({
                type: 'POST',
                url: GetProgramDetailsPath,
                data: { programId: $(this).val() },
                success: function (response) {
                    loadTermCampusOptions(response);
                    expandCollapseAll('show');
                },
                error: function (response) {
                    error(errorTitle, errorMessage);
                }
            });
        };
    });


}

// Terms and Campus controls

function loadTermCampusOptions(response) {
    $('.campusOptions .termOptions li').remove();  // Clean campus options for terms
    $.each(response, function (i, item) {
        var newTerm =
            "<li id='" + item.Term + "' class='term-" + item.Term + " list-group-item ui-state-default " + (!item.Active ? 'pct-disabled': '')+ "'>" +
            "  <span class='ui-icon ui-icon-arrowthick-2-n-s'></span>" +
            addActiveImage(item.Term, item.TermCode, item.TermDesc, item.Active) +
            "  <label class='descLabel'>" + item.TermDesc + "</label> (<label class='codeLabel'>" + item.TermCode + "</label>)" +
            "</li>";
        $('.campusOptions #list-' + item.Campus + '.termOptions').append(newTerm);
    });

    countTermsSelected();  // Count terms selected
}

function addActiveImage(termId, termCode, termDesc, active) {
    var htmlCode =
        "  <div class='pull-right'>" +
        "    <input type= 'image' class='icon-delete activate-item' item= '" + termId + "' code= '" + termCode + "' desc= '" + termDesc + "' active-item='" + active + "' src= '" + imagePath + (!active ? 'disable' : 'check') + ".png' width= '15' height= '15' title= '" + (!active ? 'Inactive' : 'Active') + " term' onClick='activateDetailTerm(this);' />" +
        "  </div>";
    return htmlCode;
}

// Activate/Deactivate Item
function activateDetailTerm(Item) {
    var termId = $(Item).attr('item');
    var active = toBoolean($(Item).attr('active-item'));
    var itemDesc = $(Item).attr('code') + ' - ' + $(Item).attr('desc');
    var campusId = $(Item).closest('ul').attr('id').replace('list-', '');
    var programId = $('#ApplicationProgram').val();
    var confirmmsg = 'Are you sure you wish to ' + (active ? 'deactivate' : 'activate') + ' this term?\n ' + itemDesc;
    var onsuccess = function () { activateDetailTermItem(programId, termId, campusId, !active, activateItemUrl); }
    confirm((active ? 'Deactivate' : 'Activate') + ' this term?', confirmmsg, onsuccess, null);
}

// Activate Detail Term
function activateDetailTermItem(programId, termId, campusId, active, activateUrl) {
    var parameters = { programListId: [programId], termId: termId, campusId: campusId, active: active };
    var onsuccess = function () { activateFrontEndDetailTerm(termId, campusId, active); }
    ajaxPostControllerCall(activateUrl, parameters, onsuccess);
}

// Activate Front End Detail Term
function activateFrontEndDetailTerm(termId, campusId, active) {
    activeItem = $('#list-' + campusId + ' #' + termId);
    activeItem.removeClass('pct-disabled');
    if (activeItem.has('.activate-item').length == 0) {
        var termCode = activeItem.find('.codeLabel');
        var termDesc = activeItem.find('.descLabel');
        $(activeItem.find('span:first')).after(addActiveImage(termId, termCode, termDesc, active));
    }
    input_image = activeItem.find('.activate-item');
    input_image.attr('active-item', active);
    image_src = input_image.attr('src');
    if (!active) {
        activeItem.addClass('pct-disabled');
        image_src = image_src.replace('check.png', 'disable.png');
    } else {
        image_src = image_src.replace('disable.png', 'check.png');
    }
    input_image.attr('src', image_src).attr('title', (active ? "Active " : "Inactive ") + 'term');
}

function countTermsSelected() {
    $('.campusOptions').each(function () {
        var count = $(this).children('.termOptions').children('.list-group-item').length;

        if ($(this).children('a').children('.badge').length == 0) {
            $(this).children('a').prepend('<span class="badge">' + count + '</span>');
        } else {
            $(this).find('.badge').html(count);
        }
    })
}

function expandCollapseAll(option) {
    expandCollapse($('#list-term'), option);
    expandCollapse($('#main-campus-list ul.termOptions'), option);
}

// Add program detail
function saveProgramDetail(programId, campusId, termId, active) {
    var parameters = { programListId: [programId], campusId: campusId, termId: termId, active: active };
    var onsuccess = function () { updateProgramDetailOrder(programId, campusId); activateFrontEndDetailTerm(termId, campusId, true); }
    var onerror = function () { $('#list-' + campusId + '.termOptions').find('.term-' + termId).remove(); }
    ajaxPostControllerCall(SaveProgramDetailsPath, parameters, onsuccess, onerror);
}

// Delete program detail
function deleteProgramDetail(programId, campusId, termId, item) {
    $.ajax({
        type: 'POST',
        url: DeleteProgramDetailsPath,
        data: { programId: programId, campusId: campusId, termId: termId },
        success: function (response) {
            if (response.Success == false) {
                item.sortable('cancel');
                countTermsSelected();  // Count terms selected
                error(errorTitle, response.Message);
            }
        },
        error: function (response) {
            error(errorTitle, errorMessage);
        }
    });
}

// Save term order
function updateTermOrder(orderUrl, element) {
    var parameters = { itemOrder: $('#list-term').sortable('toArray') };
    ajaxPostControllerCall(orderUrl, parameters);
}

// Save campus order
function updateCampusOrder(orderUrl) {
    //var itemList = $('#main-campus-list').sortable('toArray');
    var parameters = { itemOrder: $('#main-campus-list').sortable('toArray') };
    ajaxPostControllerCall(orderUrl, parameters);
}

// Save program detail order
function updateProgramDetailOrder(programId, campusId) {
    var termList = $('#main-campus-list #' + campusId + ' ul.list-group').sortable('toArray');
    var parameters = { programId: programId, campusId: campusId, termListOrder: termList };
    ajaxPostControllerCall(orderProgramDetailUrl, parameters);
}

//To activate the program in term
function ProgramDetailActivate() {
    var activateProgramListID = getSelectedItems();
    var campusId = $('#campusId').val();
    var termId = $('#termId').val();

    //check the check box value to activate or deactivate the program terms
    var active = $('#RActive').is(':checked');

    if (activateProgramListID.length > 0 && termId.length > 0 && campusId.length > 0 && $('.activate_selection').is(':checked')) {
        $.ajax({
            type: 'POST',
            url: activateItemUrl,
            data: { programListId: activateProgramListID, campusId: campusId, termId: termId, active: active },
            success: function (success) {
                if (success) {
                    var onsuccess = function () { location.reload(); }
                    message("Activated/Deactivated with success", "The selected programs were Activated/Deactivated with success!", onsuccess);
                }
            },
            error: function () {
                error(errorTitle, errorMessage);
            }
        });
    } else {
        if (termId.length == 0) {
            warning("No Term Selected!", "Please, select a Term!");
        } else {
            if (campusId.length == 0) {
                warning("No Campus Selected!", "Please, select a Campus!");
            } else {
                if (activateProgramListID.length == 0) {
                    warning("No Results Selected!", "Please, select at least one program in order to active/deactivate it!");
                } else {
                    if (!$('.activate_selection').is(':checked')) {
                        warning("No Active/Deactive Selected!", "Please, select a Active/Deactive option!");
                    }
                }
            }
        }
    }
}

//To search program details based on term and campus
function ProgramDetailSearch() {
    var CampusId = $('#campusId').val();
    var TermId = $('#termId').val();

    $.ajax({
        type: 'POST',
        url: SearchProgramDetailsPath,
        data: { CampusID: CampusId, TermID: TermId },
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


// To add the program in a Term 
function SaveProgramDetailsForProgramTerm() {
    if ($('#campusId').val() == null || $('#termId').val() == null) {
        warning("Please Choose the Term and the Campus");
    }
    else
    {
        var addProgramListID = getSelectedItems();

        var campusId = $('#campusId').val();
        var termId = $('#termId').val();

        if (addProgramListID.length > 0) {
            $.ajax({
                type: 'POST',
                url: SaveProgramDetailsPath,
                data: { programListId: addProgramListID, campusId: campusId, termId: termId },
                success: function (success) {
                    if (success) {
                        message("Added with success", "The selected programs were added with success!");
                    }
                },
                error: function () {
                    error(errorTitle, errorMessage);
                }
            });
        } else {
            warning("No Results Selected!", "Please, select at least one Result in order to print the report!");
        }
    }
}

/*  End: Relation Settings Functions  */




