$(function () {
    var i = 0;
    $('.addLink').click(function () {
        i++;
        var html2Add = "<div class='image col-md-2'>" +
            "<h4>Изображение № " + (i + 1) + "</h4>" +
            "<div class='col-md-10'> <input type='file' name='uploadImage' />" +
            "</div></div>";
        $('#imageBlock').append(html2Add);
    })
})