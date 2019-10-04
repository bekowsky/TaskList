
$(function () {

    var chat = $.connection.myHub;
    // Объявление функции, которая хаб вызывает при получении сообщений
    chat.client.createReady = function (key) {
        document.location.href = "http://localhost:50250/Home/Find/?id="+ key;
       
    };
    chat.client.enter = function () {

    }
    chat.client.removeReady = function () {
        document.location.href = "http://localhost:50250/Home/Projects";

    };
  
    chat.client.returnUsers = function (users) {
        document.getElementById('search-list').style.display = 'block';
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
            ButtonElem.onclick = function () { AddFriend(name); "this.parentNode.style.display = 'none'"; };
            ButtonElem.innerHTML = "Добавить";
            aElem.innerHTML = users[i];
            aElem.href = "#";
            aElem.className = "nav-link";
            liElem.appendChild(aElem);
            liElem.appendChild(ButtonElem);
            liElem.className = "nav-item  SearchUsers";
            document.getElementById('search-list').appendChild(liElem);
        }
       
    };

    chat.client.appendFriend = function (message) {

        var aElem = document.createElement('a');
        aElem.className = "nav-link friend";
        aElem.innerText = message;
        aElem.href = "#";
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
        $('#friend-list').prepend(liElem);
        
    };
    chat.client.appendSubscriber = function (message) {
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
        document.getElementById("friend-list").appendChild(liElem);

    };
    chat.client.removeFriend = function (message) {
        if ($('#friend-' + message) != null) {
            document.getElementById("friend-list").removeChild(document.getElementById("friend-" + message));

        }

    };
    chat.client.removeSubscriber = function (message) {

        if ($('#subscriber-' + message) != null) {
            document.getElementById("friend-list").removeChild(document.getElementById("subscriber-" + message));

        }
    };




    chat.client.notification = function (message) {
        var liElem = document.createElement('li');
        var liDevider = document.createElement('li');
        var aElem = document.createElement('a');
        var pElem = document.createElement('p');
        aElem.href = "#";
        pElem.innerText = message;
        aElem.appendChild(pElem);

        liElem.appendChild(aElem);
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
        $('.BtnInfo').click(function () {
            var num = this.id;
           
            chat.server.projectsInfo(num,$('#WatchPermission').val(), $('#ChangePermission').val(), $('#SettingPermission').val());

            var id = '#InfoProject' + this.id;
            $(id).modal('toggle');
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
            var name = this.parentNode.children[0].children[0].textContent;
            chat.server.deleteFriend(name);
        });

        $('.SaveSettings').click(function () {
            var key = this.id;
            var colab = document.getElementById('colaborators');
            let arr = [];
            for (let i = 0; i < colab.children.length; i++) {
                arr[i] = colab.children[i].innerText;
            }

            chat.server.saveSettings(key, arr);
      
            $('#ProjectSettings').modal('toggle');
        });


        $('#BtnCreateProjects').click(function () {
            
            chat.server.create($('#NameCreateProject').val(), $('#DescriptionCreateProject').val());
        });

        
        


        $('#Search').click(function () {           
            document.getElementById('friend-list').style.display = 'none';
            chat.server.findUsers($('#SearchInput').val());
        });
        $('#AddStep').click(function () {
           
            chat.server.addStep($('#InputArea').val(), $('#ProjectKey').val());
             document.getElementById("InputArea").value = '';
        });
        
    });

   
    function AddFriend(name) {

        chat.server.addFriend(name);
        $(this).parent().hide();
    }

    function AcceptFriend(name) {

        chat.server.confirmFriend(name);
    }
    function DeleteFriend(name) {

      
        chat.server.deleteFriend(name);

    }

});

// Кодирование тегов
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
    
}
