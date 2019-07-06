var add = document.getElementById("AddStep");
var num = 0;
var str = '';
add.onclick = function () {
    if (num != 0) {
       
        str += "<img  class='col-xs-offset-6' src=" + "\\" + "Images/line.png>";
       
    }
    var id = "step" + num;
    str += '<p id =' + id + ' class = "text-center">';

    str += document.getElementById("InputArea").value;
    str += '</p>';
    
    document.getElementById("TextArea").innerHTML = str;
    func(id);
    num++;
}

function func() {
    for (var i = 0; i < num+1; i ++)
    document.getElementById('step'+ i).style.backgroundColor = 'gainsboro';
    
}

window.onload = function () {
    $("#myModalBox").modal('show');
};