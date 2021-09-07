﻿
var allData = [];
var filteredData = [];

$(document).ready(function () {

    $.ajax({
        url: 'api/Index/GetNaziveDrzava',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {

            data.forEach(x => {
                let opt = document.createElement('option');
                opt.value = x;
                opt.innerHTML = x;
                $('#selectDrzava').append(opt);
            });
        },
        error: function (error) {
            alert(JSON.stringify(error));
        }
    });

    //$('#startDate').change(function () {

    //    if (filteredData.length == 0) return;

    //    let tableId = 'tableData';
    //    let rows = $('#' + tableId).find('tr');
    //    let startDate = new Date($('#startDate').val());

    //    // skip first header row
    //    for (let i = 1; i < rows.length; i++) {
    //        // row with date is second
    //        if (new Date(rows[i].children.item(1).innerHTML) < startDate) {
    //            rows[i].remove();
    //            filteredData.splice(i - 1);
    //        }
    //    }
    //});

    //$('#endDate').change(function () {

    //    if (filteredData.length == 0) return;

    //    let tableId = 'tableData';
    //    let rows = $('#' + tableId).find('tr');
    //    let endDate = new Date($('#endDate').val());

    //    $('.podaciZaDrzavu').remove();
        

    //    // skip first header row
    //    for (let i = 1; i < rows.length; i++) {
    //        // row with date is second
    //        let thisDate = new Date(rows[i].children.item(1).innerHTML); 
    //        if ( thisDate > endDate) {
    //            filteredData.splice(i - 1);
    //            rows[i].remove();
    //        }
    //    }
    //});

    $('.dateFilter').change(function () {
        
        if (allData.length == 0) return;
        filteredData = [...allData];
        $('.podaciZaDrzavu').remove();
        let tableId = 'tableData';
        
        if ($('#startDate').val()) {
            let startDate = new Date($('#startDate').val());

            for (let i = 0; i < filteredData.length; i++) {
                if (new Date(filteredData[i].DatumUTC) < startDate) {
                    filteredData.splice(i);
                }
            }
        }

        if ($('#endDate').val()) {
            let endDate = new Date($('#endDate').val());

            for (let i = 0; i < filteredData.length; i++) {
                if (new Date(filteredData[i].DatumUTC) > endDate) {
                    filteredData.splice(i);
                }
            }
        }
        
        addDataToTable(filteredData, tableId);
    });

    $('#dateCancelFilter').click(function () {
        $('#endDate').val('');
        $('#startDate').val('');
        $('.podaciZaDrzavu').remove();
        filteredData = [];

        addDataToTable(allData, 'tableData');

    });

    $('#selectDrzava').change(function () {
        if ($('#selectDrzava option:selected').attr('disabled')) return;

        $('.podaciZaDrzavu').remove();

        $.ajax({
            url: 'api/Index/GetDataDrzava',
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json',
            data: $.param({ 'naziv': $('#selectDrzava').val()}, true),
            success: function (data) {

                allData = data;
                filteredData = data;
                addDataToTable(data, 'tableData');
            },
            error: function (error) {
                alert(JSON.stringify(error));
            }
        });

    });

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

function addDataToTable(data, tableId) {
    data.forEach(x => {
        let tr = document.createElement('tr');
        tr.className = 'podaciZaDrzavu';
        let td1 = document.createElement('td');
        let td2 = document.createElement('td');
        let td3 = document.createElement('td');
        let td4 = document.createElement('td');
        let td5 = document.createElement('td');
        let td6 = document.createElement('td');
        let td7 = document.createElement('td');

        $('#' + tableId).append(tr);
        tr.append(td1);
        tr.append(td2);
        tr.append(td3);
        tr.append(td4);
        tr.append(td5);
        tr.append(td6);
        tr.append(td7);

        td1.className = 'nazivShow';
        td2.className = 'dateUTCShow';
        td3.className = 'kolicinaShow';
        td4.className = 'temperaturaShow';
        td5.className = 'pritisakShow';
        td6.className = 'vlaznostShow';
        td7.className = 'vetarShow';

        td1.innerHTML = (x.NazivDrzave ? x.NazivDrzave : 'Podatak nije dostupan');
        td2.innerHTML = (x.DatumUTC ? new Date(x.DatumUTC) : 'Podatak nije dostupan');
        td3.innerHTML = (x.KolicinaEnergije ? x.KolicinaEnergije : 'Podatak nije dostupan');
        td4.innerHTML = (x.Temperatura ? x.Temperatura : 'Podatak nije dostupan');
        td5.innerHTML = (x.Pritisak ? x.Pritisak : 'Podatak nije dostupan');
        td6.innerHTML = (x.VlaznostVazduha ? x.VlaznostVazduha : 'Podatak nije dostupan');
        td7.innerHTML = (x.BrzinaVetra ? x.BrzinaVetra : 'Podatak nije dostupan');
    });
}