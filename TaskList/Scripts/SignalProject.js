$(function () {
  
    var chat = $.connection.myHub;
    // Объявление функции, которая хаб вызывает при получении сообщений
    chat.client.createReady = function (key) {
        document.location.href = "http://localhost:50250/Home/CreateProjects/?key="+ key;
       
    };
    chat.client.enter = function () {
        $("#LayoutProfile")
            .removeClass("disabled")
        $("#LayoutProfile").href = "http://localhost:50250/Home/Profile"
        $("#LayoutProjects")
            .removeClass("disabled")
        $("#LayoutProjects").href = "http://localhost:50250/Home/Projects"
        $("#LayoutCalendar")
            .removeClass("disabled")
        $("#LayoutCalendar").href = "http://localhost:50250/Home/Index"
    }
    chat.client.removeReady = function () {
        document.location.href = "http://localhost:50250/Home/Projects";

    };
   
    $.connection.hub.start().done(function () {

        $('.BtnDelete').click(function () {
            var num = this.id.slice(13,15);          
            chat.server.remove(num);
        });
        $('.CalendarChoice').click(function () {
            var Mounth;
            if (this.id == "BtnPrev" && Mounth > 1) {
                
                mounth = document.getElementById("CurrentMounth") - 1;

            } else if (this.id = "BtnNext" && Mounth < 12) {
                mounth = document.getElementById("CurrentMounth") + 1;
            }
            chat.server.getProjects(Mounth);
        });

        $('#BtnCreateProjects').click(function () {
            
            chat.server.create($('#NameCreateProject').val(), $('#DescriptionCreateProject').val());
        });
        $('#AddStep').click(function () {
           
            chat.server.addStep($('#InputArea').val(), $('#ProjectKey').val());
             document.getElementById("InputArea").value = '';
        });
        
    });
});

// Кодирование тегов
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}
