setTimeout(redirecturl, 2000);


function redirecturl()
{
    if (sessionStorage.getItem("First") === null || sessionStorage.getItem("Second") === null)
    {
        location.replace("https://localhost:5001");
    }

    else
    {
        let first = sessionStorage.getItem("First");
        let second = sessionStorage.getItem("Second");
        sessionStorage.clear();
        location.replace("https://localhost:5001/" + first + "/" + second);
    }
}