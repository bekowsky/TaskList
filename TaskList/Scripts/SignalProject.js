
$(function () {

    var chat = $.connection.myHub;
    // Объявление функции, которая хаб вызывает при получении сообщений
    chat.client.createReady = function (key) {
        document.location.href = "http://localhost:50250/Home/CreateProjects/?key="+ key;
       
    };
    chat.client.enter = function () {

    }
    chat.client.removeReady = function () {
        document.location.href = "http://localhost:50250/Home/Projects";

    };
  
    chat.client.returnUsers = function (users) {
        if (document.getElementsByClassName("SearchUsers")[0] != null) {
            $('.SearchUsers').remove();
        }
        var i;
        for (i = 0; i < users.length; i++) {
            var name = users[i];
            var liElem = document.createElement('li');
            var aElem = document.createElement('a');
            var ButtonElem = document.createElement('button')
            ButtonElem.type = "button";
            ButtonElem.className = "btn btn-success";
           ButtonElem.onclick = function () { AddFriend(name); };

            ButtonElem.innerHTML = "Добавить";
            aElem.innerHTML = users[i];
            aElem.href = "#";
            aElem.className = "nav-link";
            liElem.appendChild(aElem);
            liElem.appendChild(ButtonElem);
            liElem.className = "nav-item  SearchUsers";
          

        document.getElementsByClassName("navbar-nav")[1].appendChild(liElem);
        }
       
    };

    chat.client.refreshAdding = function (message) {
        var aElem = document.createElement('a');
        aElem.className = "nav-link friend";
        aElem.innerText = message;
        var ButtonElem = document.createElement('button');
        ButtonElem.type = "button";
        ButtonElem.className = "btn-xs btn-danger";
        ButtonElem.innerText = "Удалить";
        ButtonElem.onclick = function () { DeleteFriend(message); };
        var liElem = document.createElement('li');
        liElem.className = "DeleteFriend nav-item";
        liElem.id = "friend-" + message;
        liElem.appendChild(aElem);
        liElem.appendChild(ButtonElem);
        document.getElementById("friend-list").removeChild(document.getElementById("subscriber-" + message));
        document.getElementById("friend-list").prepend(document.getElementById("divider"));
            //appendChild(liElem);
    };

    chat.client.refreshDeletion = function (message) {
        var aElem = document.createElement('a');
        aElem.className = "nav-link friend";
        aElem.innerText = message;
        var ButtonElem = document.createElement('button');
        ButtonElem.type = "button";
        ButtonElem.className = "AcceptFriend btn-xs btn-success";
        ButtonElem.innerText = "Принять";
        ButtonElem.onclick = function () { AcceptFriend(message); };
        var liElem = document.createElement('li');
        liElem.className = "nav-item";
        liElem.id = "subscriber-" + message;
        liElem.appendChild(aElem);
        liElem.appendChild(ButtonElem);

        document.getElementById("friend-list").removeChild(document.getElementById("friend-" + message));
       document.getElementById("friend-list").appendChild(liElem);
        
    };


    chat.client.notification = function (message) {
        var liElem = document.createElement('li');
        var liDevider = document.createElement('li');
        liElem.innerHTML = message;
        document.getElementById("NotificationBtn").appendChild(liElem);
        liDevider.className = "divider"
        document.getElementById("NotificationBtn").appendChild(liDevider);
        document.getElementById("NotifyImg").src = "\\" + "Images/ringrad.png";
        var audio = new Audio();
        audio.src = "\\"+"Sounds/pam-pam.mp3";
        audio.autoplay = true;

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

        $('.AcceptFriend').click(function () {
            var name = this.parentNode.firstChild.nextSibling.innerHTML;
            chat.server.addFriend(name);
            chat.server.friendshipAccepted(name);
        });
        $('.DeleteFriend').click(function () {
            var name = this.parentNode.firstChild.nextSibling.innerHTML;
            chat.server.deleteFriend(name);
        });


        $('#BtnCreateProjects').click(function () {
            
            chat.server.create($('#NameCreateProject').val(), $('#DescriptionCreateProject').val());
        });





        $('#Search').click(function () {

            chat.server.findUsers($('#SearchInput').val());
        });
        $('#AddStep').click(function () {
           
            chat.server.addStep($('#InputArea').val(), $('#ProjectKey').val());
             document.getElementById("InputArea").value = '';
        });
        
    });


    function AddFriend(name) {

        chat.server.addFriend(name);
    }

    function AcceptFriend(name) {

       // var name = this.parentNode.firstChild.nextSibling.innerHTML;
        chat.server.addFriend(name);
        chat.server.friendshipAccepted(name);
    }
    function DeleteFriend(name) {

       // var name = this.parentNode.firstChild.nextSibling.innerHTML;
        chat.server.deleteFriend(name);
    }

});

// Кодирование тегов
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
    
}
