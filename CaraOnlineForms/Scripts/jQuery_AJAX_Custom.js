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
  
  //  $dialog.dialog('option', 'position', ['center', $(window.parent).scrollTop() + 30]);


    return false;
};//-- end DisplayErrors --

