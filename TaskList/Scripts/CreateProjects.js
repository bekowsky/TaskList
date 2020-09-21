var add = document.getElementById("AddStep");

var num = 0;
var str = '';

function deleteColab(name) {
    
    var optionElem = document.createElement('option');
    optionElem.innerText = name;
    optionElem.id = 'friend_' + name;
    $('#ChangePermission').append(optionElem);
    $('#colab_' + name).remove();
}


$('#AddColaborators').click(function () {

    var name = $('#ChangePermission').val();

    var PElem = document.createElement('p');
    var BtnElem = document.createElement('button');
    var ImgElem = document.createElement('img');
    ImgElem.src = '\Images/delete_icon.png';
    BtnElem.class = 'DelColab';
    BtnElem.type = 'button';
    BtnElem.style.border = 'none';
    BtnElem.style.backgroundColor = 'white';
    BtnElem.onclick = function () { deleteColab(name); };
    PElem.innerText = name;
    PElem.id = 'colab_' + name;
    BtnElem.appendChild(ImgElem);
    PElem.appendChild(BtnElem);
    document.getElementById('colaborators').appendChild(PElem);
    $('#friend_'+name).remove();

});


if (add != null) {
    add.onclick = function () {
        if (!isEmpty(document.getElementById("InputArea").value)) {
            if (num != 0) {

                str += "<img  class='col-xs-offset-6' src=" + "\\" + "Images/line.png>";

            }
            var id = "step" + num;
            str = '<p style="" id =' + id + ' class = "">';

            str += document.getElementById("InputArea").value;
            str = str.replace(/(?:\r\n|\r|\n)/g, '<br />');
            str += '</p>';
            document.getElementById("TextArea").innerHTML += str;



            func(id);
            num++;
        }

    }
}




function func() {
    for (var i = 0; i < num+1; i ++)
   document.getElementById('step'+ i).style.backgroundColor = 'gainsboro';
    
}
function isEmpty(line) {
    if (line.trim() == '')
        return true;

    return false;
}

window.onload = function () {
    $("#LayoutProjects")
        .parent()
        .addClass("active");


        
    var select = document.getElementById('ChangePermission');
    for (let j = 0; j < $('#colaborators').children().length; j++) {
        for (let i = 0; i < select.childElementCount; i++) {
            if (select.children([i]).innerText == document.getElementById('colaborators').children([j]).innerText) {
                select.removeChild(select.children([i]));

                break;
            }


        }
    }
};