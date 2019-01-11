

var signInDiv = document.getElementById("SignInDiv");

function ShowSignInForm() {
    signInDiv.style.display = "block";
}

function CloseSignInForm() {
    signInDiv.style.display = "none";
}

window.onclick = function (event) {
    if (event.target == signInDiv) {
        signInDiv.style.display = "none";
    }
}

 

