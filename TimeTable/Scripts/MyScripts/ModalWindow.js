document.addEventListener('DOMContentLoaded', function () {
    var modal = document.getElementById("modalWindow");
    var btn = document.getElementById("modalBtn");
    var span = document.getElementsByClassName("close")[0];
    console.log(btn);
    console.log(span);

    span.onclick = function () {
        modal.style.display = "none";
    }

    btn.onclick = function () {
        modal.style.display = "block";
    }


    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
});