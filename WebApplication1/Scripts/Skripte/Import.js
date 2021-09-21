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

        $.ajax({
            url: 'api/Index/PostCSVFile',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                alert('File uploaded');
                alert(data);
            },
            error: function (data) {
                alert(JSON.stringify(data));
            }
        });
    });
});