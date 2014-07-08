//
// Style various parts to match the jQuery UI theme.
//
function jQueryUIStyling() {
    $('input:button, input:submit').button();

    // Style tables.
    $('.webgrid-wrapper').addClass('ui-grid ui-widget ui-widget-content ui-corner-all');
    $('.webgrid-title').addClass('ui-grid-header ui-widget-header ui-corner-top');
    jQueryTableStyling();
}


//same as above jQueryUIStyling(), but applies formatting only to the elements within the html fragment (specified by id)
function jQueryUIStylingPartial(fragment) {
    $('input:button, input:submit', fragment).button();

    // Style tables.
    $('.webgrid-wrapper', fragment).addClass('ui-grid ui-widget ui-widget-content ui-corner-all');
    $('.webgrid-title', fragment).addClass('ui-grid-header ui-widget-header ui-corner-top');
    jQueryTableStylingPartial(fragment);

}


//
// Style tables using jQuery UI theme. This function is 
// split out separately so that it can be part of the AJAX
// callback of the WebGrid WebHelper in ASP.NET MVC.
// IF YOU MAKE CHANGES HERE, MAKE SAME CHANGES TO THE OVERLOADED METHOD BELOW
//
function jQueryTableStyling() {
    $('.webgrid').addClass('ui-grid-content ui-widget-content');
    $('.webgrid-header').addClass('ui-state-default');
    $('.webgrid-footer').addClass('ui-grid-footer ui-widget-header ui-corner-bottom ui-helper-clearfix');
    $(".gridButtonLink").button();
    $(".button").button();

    $(".grid-footer a").addClass('PagerLink'); //setup styling on the webGrid's footer. it's locked otherwise.

    //prevent grid footer and header links from automatically moving scroll to the top of page
    //please note: this will break grids if you don't use ajax to hijack the links
    $(".grid-footer a").attr('href', 'javascript:void(0)');
    $(".webgrid-header a").attr('href', 'javascript:void(0)');

}


function jQueryTableStylingPartial(fragment) {
    $('.webgrid', fragment).addClass('ui-grid-content ui-widget-content');
    $('.webgrid-header', fragment).addClass('ui-state-default');
    $('.webgrid-footer', fragment).addClass('ui-grid-footer ui-widget-header ui-corner-bottom ui-helper-clearfix');
    $(".gridButtonLink", fragment).button();
    $(".button", fragment).button();

    $(".grid-footer a", fragment).addClass('PagerLink'); //setup styling on the webGrid's footer. it's locked otherwise.

    //prevent grid footer and header links from automatically moving scroll to the top of page
    //please note: this will break grids if you don't use ajax to hijack the links
    $(".grid-footer a", fragment).attr('href', 'javascript:void(0)');
    $(".webgrid-header a", fragment).attr('href', 'javascript:void(0)');
}

