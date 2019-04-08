if (document.getElementById("signed-in-user"))
{
    document.getElementById("signed-in-user").addEventListener("click", function ()
    {
        document.querySelector(".profile-menu-wrapper").classList.toggle("profile-dropdown-active");
    });
}