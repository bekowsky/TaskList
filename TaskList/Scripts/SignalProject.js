$(function () {
  
    var chat = $.connection.myHub;
    // Объявление функции, которая хаб вызывает при получении сообщений
    chat.client.enter = function () {

       
    };

   
    $.connection.hub.start().done(function () {

        $('#sendmessage').click(function () {       
            chat.server.send("ты пидор");
        });
    });
});
// Кодирование тегов
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}
