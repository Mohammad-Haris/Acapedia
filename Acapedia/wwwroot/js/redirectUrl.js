setTimeout(redirecturl, 1500);


function redirecturl()
{
    if (!sessionStorage.getItem("_path"))
    {
        location.replace("https://acapedia.azurewebsites.net");
    }

    else
    {
        let _ReplaceUrl = sessionStorage.getItem("_path");
        sessionStorage.clear();
        
        location.replace("https://acapedia.azurewebsites.net" + _ReplaceUrl);
    }
}