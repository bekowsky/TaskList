var goto = document.getElementById("BtnCreateProjects");

goto.onclick = function () {
    document.location.href = "http://localhost:50250/Home/CreateProjects";
}

$('#myModal').on('shown.bs.modal', function () {
    $('#myInput').trigger('focus')
})
window.onload = function () {
    $("#LayoutProjects")
        .parent()
        .addClass("active");
};
