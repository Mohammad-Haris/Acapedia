
var _SignedInInfo = document.getElementById("signed-in-user-info").children;

document.getElementById("manage-username").innerHTML = _SignedInInfo[0].innerHTML;

document.getElementById("manage-email").innerHTML = _SignedInInfo[1].innerHTML.replace("\n@", "@");

document.getElementById("manage-avatar").src = document.getElementById("signed-in-user").src;