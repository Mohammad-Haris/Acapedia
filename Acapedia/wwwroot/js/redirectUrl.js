setTimeout(redirecturl, 1500);


function redirecturl()
{
    if (!sessionStorage.getItem("_path"))
    {
        location.replace("https://localhost:5001");
    }

    else
    {
        let _ReplaceUrl = sessionStorage.getItem("_path");
        sessionStorage.clear();
        
        location.replace("https://localhost:5001" + _ReplaceUrl);
    }
}