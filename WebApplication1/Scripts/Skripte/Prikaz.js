
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
                $('.selectDrzava').append(opt);
            });
        },
        error: function (error) {
            alert(JSON.stringify(error));
        }
    });

    $('#dodajDrzavuBtn').click(function () {
        $.ajax({
            url: 'api/Index/DodajDrzavu',
            type: 'POST',
            contentType: 'application/json',
            dataType: 'text',
            data: JSON.stringify({
                naziv: $('#dodajDrzavuText').val()
            }),
            success: function (data) {
                alert(`Dodata:${data}`);
                $('#dodajDrzavuText').val('');
            },  
            error: function (err) {
                alert(err);
            }
        });
    });

    $('#logBtn').click(function () {
        $.ajax({
            url: 'api/Index/GetLogFile',
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                var blob = new Blob([data]);
                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.download = 'Log On_' + new String(new Date()) + '.csv';
                link.click();
            },
            error: function (error) {
                alert(JSON.stringify(error));
            }
        });
    });

    $('.dateFilter').change(function () {
        
        if (allData.length == 0) return;
        filteredData = [...allData];
        let tableId = 'tableData';

        let startDate = $('#startDate').val();
        let endDate = $('#endDate').val();

        if (!startDate) {
            startDate = new Date();
            startDate.setFullYear(startDate.getFullYear() - 500);
            let y = endDate.getFullYear();
            let m = endDate.getMonth() + 1;
            let d = endDate.getDate();
            startDate = `${y}-${m}-${d}`;

        }

        if (!endDate) {
            endDate = new Date();
            endDate.setFullYear(endDate.getFullYear() + 500);

            let y = endDate.getFullYear();
            let m = endDate.getMonth() + 1;
            let d = endDate.getDate();
            endDate = `${y}-${m}-${d}`;
        }
        
        $.ajax({
            url: 'api/Index/FilterDatum',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(filteredData),
            headers: {
                'startDate' : startDate,
                'endDate' : endDate
            },
            success: function (data) {
                addDataToTable(data, tableId);
            },
            error: function (err) {
                alert(JSON.stringify(err));
            }
        });

    });

    $('#dateCancelFilter').click(function () {
        $('#endDate').val('');
        $('#startDate').val('');
        filteredData = [];

        addDataToTable(allData, 'tableData');

    });

    $('#selectDrzava').change(function () {
        if ($('#selectDrzava option:selected').attr('disabled')) return;

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
    $('.podaciZaDrzavu').remove();

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