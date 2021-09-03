$(document).ready(function () {
    $('#drzavaNazivCB').change(function () {
        if ($('#drzavaNazivCB').is(':checked')){
            $('.nazivShow').show();
        }
        else {
            $('.nazivShow').hide();
        }
    });

    $('#datumUTCCB').change(function () {
        if ($('#datumUTCCB').is(':checked')){
            $('.dateUTCShow').show();
        }
        else {
            $('.dateUTCShow').hide();
        }
    });

    $('#kolicinaCB').change(function () {
        if ($('#kolicinaCB').is(':checked')){
            $('.kolicinaShow').show();
        }
        else {
            $('.kolicinaShow').hide();
        }
    });

    $('#temperaturaCB').change(function () {
        if ($('#temperaturaCB').is(':checked')){
            $('.temperaturaShow').show();
        }
        else {
            $('.temperaturaShow').hide();
        }
    });

    $('#pritisakCB').change(function () {
        if ($('#pritisakCB').is(':checked')){
            $('.pritisakShow').show();
        }
        else {
            $('.pritisakShow').hide();
        }
    });

    $('#vlaznostCB').change(function () {
        if ($('#vlaznostCB').is(':checked')){
            $('.vlaznostShow').show();
        }
        else {
            $('.vlaznostShow').hide();
        }
    });

    $('#vetarCB').change(function () {
        if ($('#vetarCB').is(':checked')){
            $('.vetarShow').show();
        }
        else {
            $('.vetarShow').hide();
        }
    });


});