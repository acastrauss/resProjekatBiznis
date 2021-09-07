$(document).ready(function () {

    $.ajax({
        url: 'api/Index/GetAllData',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {

            data.forEach(x => {
                let tr = document.createElement('tr');
                let td1 = document.createElement('td');
                let td2 = document.createElement('td');
                let td3 = document.createElement('td');
                let td4 = document.createElement('td');
                let td5 = document.createElement('td');
                let td6 = document.createElement('td');
                let td7 = document.createElement('td');

                $('#tableData').append(tr);
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

                td1.innerHTML = x.NazivDrzave;
                td2.innerHTML = String(x.DatumUTC);
                td3.innerHTML = x.KolicinaEnergije;
                td4.innerHTML = x.Temperatura;
                td5.innerHTML = x.Pritisak;
                td6.innerHTML = x.VlaznostVazduha;
                td7.innerHTML = x.BrzinaVetra;
            });

        },
        error: function (error) {
            alert(JSON.stringify(error));
        }
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