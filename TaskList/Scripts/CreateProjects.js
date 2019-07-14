var add = document.getElementById("AddStep");
var num = 0;
var str = '';
var btnstr = '';
add.onclick = function () {
    if (!isEmpty(document.getElementById("InputArea").value)) {
        if (num != 0) {

            str += "<img  class='col-xs-offset-6' src=" + "\\" + "Images/line.png>";

        }
        var id = "step" + num;
        str += '<p style="" id =' + id + ' class = "text-center">';

        str += document.getElementById("InputArea").value;
        str = str.replace(/(?:\r\n|\r|\n)/g, '<br />');
        str += '</p>';
        document.getElementById("TextArea").innerHTML = str;

        btnstr += '<button type="button" class="btn btn-success btn-lg" ">Тест</button>';
        document.getElementById("BtnArea").innerHTML = btnstr;

        func(id);
        num++;
    }
    document.getElementById("InputArea").value = '';
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
};