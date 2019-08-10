$(function () {
  
    var chat = $.connection.myHub;
    // Объявление функции, которая хаб вызывает при получении сообщений
    chat.client.createReady = function (key) {
        document.location.href = "http://localhost:50250/Home/CreateProjects/?key="+ key;
       
    };
   
    chat.client.removeReady = function () {
        document.location.href = "http://localhost:50250/Home/Projects";

    };
   
    $.connection.hub.start().done(function () {

        $('.BtnDelete').click(function () {
            var num = this.id.slice(13,15);          
            chat.server.remove(num);
        });

        $('#BtnCreateProjects').click(function () {
            
            chat.server.create($('#NameCreateProject').val(), $('#DescriptionCreateProject').val());
        });
    });
});

// Кодирование тегов
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}
