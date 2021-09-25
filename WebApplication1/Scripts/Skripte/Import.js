$(document).ready(function () {

    $('#importBtn').click(function () {
        let consFile = document.getElementById('importConsFile').files[0];
        
        let formData = new FormData();
        formData.append('consFile', consFile);

        let weahterFiles = document.getElementById('importWeatherFile').files;
        weahterFiles = Array.from(weahterFiles);

        for (let i = 0; i < weahterFiles.length; i++) {
            formData.append('weatherFile_' + i, weahterFiles[i]);
        }

        let startDate = new Date($('#startDateImport').val());
        let y1 = startDate.getFullYear();
        let m1 = startDate.getMonth() + 1;
        let d1 = startDate.getDate();
        startDate = `${y1}-${m1}-${d1}`;

        let endDate = new Date($('#endDateImport').val());
        let y2 = endDate.getFullYear();
        let m2 = endDate.getMonth() + 1;
        let d2 = endDate.getDate();
        endDate = `${y2}-${m2}-${d2}`;

        $.ajax({
            url: 'api/Index/PostCSVFile',
            type: 'POST',
            data: formData,
            headers: {
                'countryName': $('#selectImport').val(),
                'startDate': startDate,
                'endDate': endDate
            },
            processData: false,
            contentType: false,
            success: function (data) {
                alert('File uploaded');
            },
            error: function (data) {
                alert(JSON.stringify(data));
            }
        });
    });
});