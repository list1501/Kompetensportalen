var modal = document.getElementById("testModal");

var startButton = document.getElementById("btnStartTest");

var close = document.getElementsByClassName("close")[0];

startButton.onclick = function()
{
    modal.style.display = "block";
}

close.onclick = function()
{
    modal.style.display = "none";
}