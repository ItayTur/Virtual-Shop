var lbl = document.getElementById("divMsg");
var today = new Date();
var curHr = today.getHours();

if (curHr >= 6 && curHr < 12) {
    lbl.innerHTML = "Good Morning @Model.FirstName";
} else if (curHr >= 12 && curHr < 18) {
    lbl.innerHTML = "Good Afternoon @Model.FirstName" ;
} else if (curHr >= 18 || curHr < 6) {
    lbl.innerHTML = "Good Evening @Model.FirstName";
}