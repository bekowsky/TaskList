var goto = document.getElementById("CreateProjects");

goto.onclick = function () {
    document.location.href = "http://localhost:50250/Home/CreateProjects";
}


window.onload = function () {
    $("#LayoutProjects")
        .parent()
        .addClass("active");
};
