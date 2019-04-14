if (document.getElementById("signed-in-user"))
{
    document.getElementById("signed-in-user").addEventListener("click", function ()
    {
        document.querySelector(".profile-menu-wrapper").classList.toggle("profile-dropdown-active");
    });

    let _UserName = document.getElementById("signed-in-user-info").children[0].innerHTML;
    document.getElementById("signed-in-user-info").children[0].innerHTML = _TitleCase(_UserName);
}

function _TitleCase(str)
{
    var splitStr = str.toLowerCase().split(' ');
    for (var i = 0; i < splitStr.length; i++)
    {
        splitStr[i] = splitStr[i].charAt(0).toUpperCase() + splitStr[i].substring(1);
    }
    return splitStr.join(' ');
}

if (document.getElementById("login-user-info"))
{
    document.getElementById("login-user-info").addEventListener("click", function ()
    {
        let path = document.location.pathname;

        if (typeof (Storage) !== "undefined")
        {            
            sessionStorage.setItem("_path", path);            
        }
    });
}