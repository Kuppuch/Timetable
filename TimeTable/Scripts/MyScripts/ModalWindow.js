﻿document.addEventListener('DOMContentLoaded', function () {
    var modal = document.getElementById("modalWindow");
    var btn = document.getElementById("modalBtn");
    var span = document.getElementsByClassName("close")[0];
    console.log(btn);

    btn.onclick = function () {
        modal.style.display = "block";
    }
    span.onclick = function () {
        modal.style.display = "none";
    }

    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
});