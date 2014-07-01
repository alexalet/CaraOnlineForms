function confirmActionYesNo(message, header, okButtonText, cancelButtonText, callbackYes, callbackNo) {

    var $confirmDialog = $('<div>' + message + '</div>');

    $confirmDialog
    .dialog({
        autoOpen: false,
        resizable: false,
        width: 420,
        minHeight: 200,
        title: header,
        modal: true,
        buttons: [
          {
              text: okButtonText,
              click: function () {
                  $(this).dialog("close");
                  callbackYes.apply();
              }
          },
          {
              text: cancelButtonText,
              click: function () {
                  $(this).dialog("close");
                  callbackNo.apply();
              }
          }
        ]
    });

    $confirmDialog.dialog({
        close: function (event, ui) { $(this).empty(); }
    });

    
    $confirmDialog.dialog('option', 'position', ['center', $(window.parent).scrollTop() + 30]);
    
    //show the dialog
    $confirmDialog.dialog('open');

};//--- end confirmActionYesNo ---

function confirmActionYesNoCancel(message, header, okButtonText, cancelButtonText, callbackYes, callbackNo) {


    var $confirmDialog = $('<div>' + message + '</div>');

    $confirmDialog
    .dialog({
        autoOpen: false,
        resizable: false,
        width: 420,
        minHeight: 200,
        title: header,
        modal: true,
        buttons: [
          {
              text: okButtonText,
              click: function () {
                  $(this).dialog("close");
                  callbackYes.apply();
              }
          },
          {
              text: cancelButtonText,
              click: function () {
                  $(this).dialog("close");
                  callbackNo.apply();
              }
          },
           {
               text: "Cancel",
               click: function () {
                   $(this).dialog("close");
               }
           }
        ]
    });

    $confirmDialog.dialog({
        close: function (event, ui) { $(this).empty(); }
    });

    //$confirmDialog.dialog('option', 'position', ['center', $btn.offset().top]);
    $confirmDialog.dialog('option', 'position', ['center', $(window.parent).scrollTop() + 30]);


    //show the dialog
    $confirmDialog.dialog('open');

};//--- end confirmActionYesNoCancel ---

//errors is a json object containing an array of errors
function DisplayErrors(errors, header) {
    var $message = $('<ul/>');
    if ($.isArray(errors)) {
        $.each(errors, function () {
            $("<li />").html(this + "").addClass('red paddedTop10 marginLeft15').appendTo($message);
        })
    } else {
        $("<li />").html(errors).addClass('red paddedTop10').appendTo($message);
    }
     
    var $dialog = $($message);

    $dialog
        .dialog({
            modal: true,
            title: header,
            height: "auto",
            minHeight: "auto",
            autoOpen: true,
            minWidth: 500,
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });
 
    return false;
};//-- end DisplayErrors --

/*This dialog will not show a 'submit' button, instead the content will do its thing, and then
either the user will close the dialog, or the content will close the parent dialog by itself.
tag - link/button that called this
dlgId will be used to as id of the created dlg, later it can be used to close dlg*/
function dialogSelfSubmitting(tag, dlgId, title, width, minHeight, showCloseButton) {

    if (typeof width == 'undefined' || width == null)
    { width = $(document).width() - 10; }

    if (typeof dlgId != 'string' || dlgId == '') {
        dlgId = "dialogSelfSubmitting";
    }

    //setup parameters for our dialog
    var $loading = $('<img src="../Content/Images/busy.gif" alt="loading"/>');
    var $url = $(tag).attr('href');
    var $dialog = $('<div></div>').attr('id', dlgId);

    //configure the dialog window
    $dialog.empty();
    $dialog
            .append($loading)
            .load($url, function (response, status, xhr) {
                if (xhr.status == 500) //not authorized, or session ended
                {
                    $.growlUI("Error", xhr.responseText, 7000);
                    $dialog.dialog('close');
                }
            })
		    .dialog({
		        minHeight: 200,
		        minWidth: width,
		        resize: 'auto',
		        height: 'auto',
		        modal: true,
		        autoOpen: false,
		        title: title,
		        zIndex: 3999,
		        show: 'fade',
		        hide: 'fade'
		    });

    $dialog.dialog({
        close: function (event, ui) {
            //must empty the dialog (remove the loaded url e.g.partial view) to prevent weird ajax call results
            $(this).empty();
        }
    });


    if (typeof showCloseButton != 'undefined' && showCloseButton == true) {
        $dialog.dialog({
            buttons: { 'Close': function () { $dialog.dialog('close'); $(this).empty(); } }
        });
    }

    $dialog.dialog('option', 'position', { my: "right", at: "top", of: window });


    //show the dialog
    $dialog.dialog('open');
};

function displayNotifications(notifications, header) {
    var $message = $('<ul/>');
    if (notifications.length) {
        $.each(notifications, function () {
            $("<li />").html(this + "").addClass('paddedTop10').appendTo($message);
        });
    }
    var $dialog = $($message);
    $dialog
                .dialog({
                    modal: true,
                    title: header,
                    height: "auto",
                    minHeight: "auto",
                    minWidth: 500,
                    buttons: {
                        Ok: function () {
                            $(this).dialog("close");
                        }
                    }
                });

    $dialog.dialog('option', 'position', ['center', $(window.parent).scrollTop() + 30]);

    return false;
};

/*Calls an action method (any URL really) through a POST op, 
and either populates the returned HTML into a specified container,
or executes a callback function after executing the URL (e.g. reload grid after a delete operation)
if using the callback approach, return Json with the status info and/or any errors
The button/link calling this wll provide the URL for the action method. the updateTarget is the div/span/etc to be replaced by returned html
if response has field Data it will be passed as param into callback
*/
function callAction($btn, updateTarget, updateTargetInFunction, callback, successMessageHeader, dataElementName) {
    $.ajax({
        url: $btn.attr('href'),
        type: 'POST',
        data: (typeof dataElementName == 'undefined') ? "{}" : $(dataElementName).serialize(),
        success: function (response) {

            if (updateTargetInFunction == true) {
                $(updateTarget).html(response);   //process the result of the ajax call

            }
            else {
                if (response.Success) {

                    if ($.type(response.Data) === 'undefined')
                        callback.apply();
                    else
                        callback.apply(this, [response.Data]);

                    setTimeout(function () { $.growlUI(successMessageHeader, "", 430); }, 500); //allow refresh time if something was hidden/resized

                    if (response.NotificationsAvailable) {
                        displayNotifications(response.Notifications, "Notification Center Alerts"); //notifications (informational) not errors.
                    }
                }
                else { //logic error(s)
                    displayErrors(response.Errors, "Error(s) Detected");
                }
            }
        },
        error: function (xhr) { //system error, or user not authorized / session expired
            $.growlUI(null, xhr.responseText, 10000);
        }
    });

    return false;
};