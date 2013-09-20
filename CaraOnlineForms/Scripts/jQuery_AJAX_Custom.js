function confirmActionYesNo($btn, message, header, okButtonText, cancelButtonText, callbackYes, callbackNo) {

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

