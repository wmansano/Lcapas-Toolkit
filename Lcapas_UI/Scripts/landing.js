$(document).ready(function () {
    $("#ApplicationForm").validate();

    // Show/Hide Previously Applied
    showPreviouslyApplied();

    // Disable Terms and Campus options
    disableOptionGroup('termOptions');
    disableOptionGroup('campusOptions');

    // Load selected program, term and campus
    loadProgramOptions($('#ProgramSelected').val());

    // Get list of terms and campuses available
    $('#ProgramSelected').change(function () {
        loadProgramOptions($(this).val());
    });

    $('.collect').bind("cut copy paste", function (e) { e.preventDefault(); });
    $('#ContactName').bind('keyup blur', function () { var node = $(this); node.val(node.val().replace(/[^A-Za-z_\s]/, '')); });
    $('#PreviousStuId').mask('s9999999');
    $("#ContactPhone").mask("999 999-9999");

    // When a Term is selected, enable available campuses
    $('#termOptions input[type=radio]').change(function () {
        AvailableTermCampus($(this).attr('id'));
    });

    $('#certify').change(function () {
        if ($(this).is(':checked')) {
            $('#AgreementSubmit').removeClass('disabled');
        } else {
            $('#AgreementSubmit').addClass('disabled');
        }
    });

    $('span.validation-block').bind('DOMSubtreeModified', function () {
        if ($(this).hasClass('field-validation-error')) {
            $(this).parent('div.alt').addClass('errorBorder');
        } else {
            $(this).parent('div.alt').removeClass('errorBorder');
        };
    });

    // Show/Hide Previously Applied
    $('#previouslyApplied input[type="radio"]').change(function () {
        showPreviouslyApplied();
    });

});

function showPreviouslyApplied() {
    if ($('#previousApplicantN').is(':checked')) {
        $('.hidePrevious').slideUp('fast');
    } else {
        $('.hidePrevious').slideDown('fast');
    }
}

function AvailableTermCampus(termId) {
    if (termId == null) {
        termId = $('#termOptions input[type=radio]:checked').attr('id')
    }

    // Get available campus list
    var campus = $('#termOptions input[type=radio][id=' + termId + ']').attr('campus');
    if (campus) {
        campus = campus.split(',')
    }

    disableOptionGroup('campusOptions');  // Disable all Campus options

    // Enable options available
    $.each(campus, function (index, campusId) {
        $('#campusOptions input:radio[id=' + campusId + ']').removeAttr('disabled');
    });

    checkFirstOptionGroup('campusOptions');  // Select first campus available, if not selected
}

// Make selected program options (campus/term) available
function loadProgramOptions(programId) {
    if (!programId) {
        // Disable all Terms and Campus options
        disableOptionGroup('termOptions');
        disableOptionGroup('campusOptions');

        // Select first term and campus available, if not selected
        checkFirstOptionGroup('termOptions');
        checkFirstOptionGroup('campusOptions');

    } else {
        $.ajax({
            type: 'GET',
            url: FilterProgramDetailsPath,
            contentType: "application/json; charset=utf-8",
            datatype: 'json',
            cache: false,
            data: { programId: programId, uuid: $('#UUID').val() },
            success: function (response) {
                loadTermCampusOptions(response);
            },
            failure: function (response) {
                alert("Unable to retrieve data from database.");
            }
        });
    };
}

// Terms and Campus controls
function loadTermCampusOptions(response) {
    // Disable all Terms and Campus options
    disableOptionGroup('termOptions');
    disableOptionGroup('campusOptions');

    $('#termOptions input[type=radio]').removeAttr('campus'); // Reset campus options for terms
    $('#campusOptions input[type=radio]').removeAttr('terms'); // Reset term options for campuses

    $.each(response, function (i, item) {
        var inputTerm = $('#termOptions input:radio[id=' + item.Term + ']');
        var inputCampus = $('#campusOptions input:radio[id=' + item.Campus + ']');

        // Enable options available
        inputTerm.removeAttr("disabled");
        inputCampus.removeAttr("disabled");

        // Store available options
        setAttribute(inputTerm, 'campus', item.Campus);
        setAttribute(inputCampus, 'terms', item.Term);
    });

    // Select first term and campus available, if not selected
    checkFirstOptionGroup('termOptions');
    checkFirstOptionGroup('campusOptions');

    // Load available campus/Term
    AvailableTermCampus();
}

function disableOptionGroup(groupName) {
    // Disable all radio options
    $('#' + groupName + ' input[type=radio]').attr('disabled', 'disable');
}

function checkFirstOptionGroup(groupName) {
    // Select first option available, if not selected
    if (!$('#' + groupName + ' input[type=radio]:not(:disabled)').is(':checked')) {
        $('#' + groupName + ' input[type=radio]').removeAttr('checked');
        $('#' + groupName + ' input[type=radio]:not(:disabled):first').prop('checked', true);
    }
}

function setAttribute(element, attrName, value) {
    // Store available options on an attribute
    if (element.attr(attrName) !== undefined) {
        element.attr(attrName, element.attr(attrName) + ',' + value);
    } else {
        element.attr(attrName, value);
    };
}

function moveTabs(fromElementId, fromPage, toElementId, toPage) {
    var $img = $('#' + fromElementId + ' a img');
    $img.attr('src', '/Content/Images/check.png');
    $img.removeClass('hide');

    $('#' + fromElementId).removeClass('active').addClass('disabled');
    $('#' + fromPage).removeClass('active');

    $('#' + toElementId).addClass('active');
    $('#' + toPage).addClass('active');
}
