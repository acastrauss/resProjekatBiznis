$(document).ready(function () {

    $('#exportBtn').click(function () {


        let exportData = [];

        let rows = $('#tableData').find('tr');

        for (let i = 1; i < rows.length; i++) {

            let naziv = rows[i].children.item(0).innerHTML == 'Podatak nije dostupan' ?
                null : rows[i].children.item(0).innerHTML;

            let utc = rows[i].children.item(1).innerHTML == 'Podatak nije dostupan' ?
                null : new Date(rows[i].children.item(1).innerHTML);

            let kolicina = rows[i].children.item(2).innerHTML == 'Podatak nije dostupan' ?
                null : rows[i].children.item(2).innerHTML;

            let temp = rows[i].children.item(3).innerHTML == 'Podatak nije dostupan' ?
                null : rows[i].children.item(3).innerHTML;

            let pritisak = rows[i].children.item(4).innerHTML == 'Podatak nije dostupan' ?
                null : rows[i].children.item(4).innerHTML;

            let vlaznost = rows[i].children.item(5).innerHTML == 'Podatak nije dostupan' ?
                null : rows[i].children.item(5).innerHTML;

            let vetar = rows[i].children.item(6).innerHTML == 'Podatak nije dostupan' ?
                null : rows[i].children.item(6).innerHTML;

            let jsonObj = {};

            if (!$('.nazivShow').is(':hidden'))
                jsonObj.NazivDrzave = naziv.trim();

            if (!$('.dateUTCShow').is(':hidden'))
                jsonObj.DatumUTC = utc;

            if (!$('.kolicinaShow').is(':hidden'))
                jsonObj.KolicinaEnergije = kolicina;

            let hid = $('.temperaturaShow').is(':hidden');

            if (!hid)
                jsonObj.Temperatura = temp;

            if (!$('.pritisakShow').is(':hidden'))
                jsonObj.Pritisak = pritisak;

            if (!$('.vlaznostShow').is(':hidden'))
                jsonObj.VlaznostVazduha = vlaznost;

            if (!$('.vetarShow').is(':hidden'))
                jsonObj.BrzinaVetra = vetar;

            exportData.push(jsonObj);
        }

        let jsonstr = JSON.stringify(exportData);

        

        $.ajax({
            url: 'api/Index/GetCSVFile',
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            data: jsonstr,
            success: function (data) {
                var blob = new Blob([data]);
                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.download = new String(new Date()) + '.csv';
                link.click();
            },
            error: function (error) {
                alert(JSON.stringify(error));
            }
        });
    });

});