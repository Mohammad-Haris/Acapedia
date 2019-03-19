var prevSelect;
var prevCoun;

function AddEventIsIcon()
{
    let toggler = document.getElementsByClassName("is-icon");
    for (let i = 0; i < toggler.length; i++)
    {
        toggler[i].addEventListener("click", async function ()
        {
            let child = this.parentElement.querySelector(".children");

            if (child.classList.contains("active"))
            {
                child.classList.add("animate-none");
                await sleep(280);
                child.classList.remove("active");
            }
            else
            {
                child.classList.remove("animate-none");
                child.classList.add("active");
            }

            if (this.innerHTML === "+")
            {
                this.innerHTML = "-";
            }

            else
            {
                this.innerHTML = "+";
            }
        });
    }
}

function sleep(ms)
{
    return new Promise(resolve => setTimeout(resolve, ms));
}

function AddEventChildren()
{
    var noChildren = document.getElementsByClassName("no-child");

    for (let i = 0; i < noChildren.length; i++)
    {
        noChildren[i].addEventListener("click", function ()
        {
            if (prevSelect != null)
            {
                prevSelect.classList.remove("current-selection");
            }

            this.classList.add("current-selection");
            prevSelect = this;
            document.getElementById("info-heading-p").innerHTML = this.innerHTML;

            var request = new XMLHttpRequest();

            request.open('GET', "https://en.wikipedia.org/api/rest_v1/page/summary/" + this.innerHTML, true);

            request.onload = function ()
            {
                let data = JSON.parse(this.response);

                if (request.status >= 200 && request.status < 400)
                {
                    document.getElementById("info-detail-p").innerHTML = data.extract_html;

                    if (typeof data.thumbnail === "undefined")
                    {
                        document.getElementById("info-detail-image").src = "";
                    }

                    else
                    {
                        document.getElementById("info-detail-image").src = data.thumbnail.source;
                    }
                }
                else
                {
                    document.getElementById("info-detail-p").innerHTML = "We had some trouble retrieving data from Wikipedia :(";
                    document.getElementById("info-detail-image").src = "";
                }
            }

            request.send();
            GetParents_Child(this);
        });
    }
}

document.getElementById("collapse-button").addEventListener("click", function ()
{
    let children = document.querySelectorAll("ul.children");
    for (let i = 0; i < children.length; i++)
    {
        if (children[i].classList.contains("active"))
        {
            children[i].classList.remove("active");
        }
    }

    let plus = document.querySelectorAll("span.is-icon");
    for (let i = 0; i < plus.length; i++)
    {
        plus[i].innerHTML = "+";
    }
});

document.getElementById("expand-button").addEventListener("click", function ()
{
    let children = document.querySelectorAll("ul.children");
    let i;
    for (i = 0; i < children.length; i++)
    {
        children[i].classList.remove("animate-none");
    }

    for (i = 0; i < children.length; i++)
    {
        if (!children[i].classList.contains("active"))
        {
            children[i].classList.add("active");
        }
    }

    let plus = document.querySelectorAll("span.is-icon");
    for (i = 0; i < plus.length; i++)
    {
        plus[i].innerHTML = "-";
    }
});

if (document.getElementById("login-user-info"))
{
    document.getElementById("login-user-info").addEventListener("click", function ()
    {
        let path = document.location.pathname.split("/");
        path.shift();

        if (typeof (Storage) !== "undefined")
        {
            sessionStorage.setItem('First', path[0]);
            sessionStorage.setItem('Second', path[1]);
        }
    });
}

function AddEventParent()
{
    var parents = document.getElementsByClassName("is-parent");

    for (let i = 0; i < parents.length; i++)
    {
        parents[i].addEventListener("click", function ()
        {
            if (prevSelect != null)
            {
                prevSelect.classList.remove("current-selection");
            }

            this.classList.add("current-selection");
            prevSelect = this;
            document.getElementById("info-heading-p").innerHTML = this.innerHTML;
            var request = new XMLHttpRequest();

            request.open('GET', "https://en.wikipedia.org/api/rest_v1/page/summary/" + this.innerHTML, true);

            request.onload = function ()
            {
                let data = JSON.parse(this.response);

                if (request.status >= 200 && request.status < 400)
                {
                    document.getElementById("info-detail-p").innerHTML = data.extract_html;

                    if (typeof data.thumbnail === "undefined")
                    {
                        document.getElementById("info-detail-image").src = "";
                    }

                    else
                    {
                        document.getElementById("info-detail-image").src = data.thumbnail.source;
                    }
                }
                else
                {
                    document.getElementById("info-detail-p").innerHTML = "We had some trouble retrieving data from Wikipedia :(";
                    document.getElementById("info-detail-image").src = "";
                }
            }

            request.send();

            GetParents_Parents(this);
        });
    }
}

function GetParents_Parents(selection)
{
    let map = [];
    map.push(selection.innerHTML);
    let topli = document.querySelector("ul").children;
    let top = [];
    for (let i = 0; i < topli.length; i++)
    {
        if (topli[i].querySelector("span.is-parent"))
        {
            top.push(topli[i].querySelector("span.is-parent").innerHTML);
        }

        else
        {
            top.push(topli[i].querySelector("span#u-top-level").innerHTML);
        }
    }

    let name = selection.parentElement.parentElement.parentElement.querySelector("span.is-parent").innerHTML;

    if (top.indexOf(selection.innerHTML) !== -1)
    {
        document.getElementById("road-map-text").innerHTML = map.join("  -->  ");
    }

    else
    {
        while (top.indexOf(name) === -1)
        {
            map.unshift(name);
            selection = selection.parentElement.parentElement.parentElement.querySelector("span.is-parent");
            name = selection.parentElement.parentElement.parentElement.querySelector("span.is-parent").innerHTML;
        }

        map.unshift(name);
        document.getElementById("road-map-text").innerHTML = map.join("  -->  ");
    }

}

function GetParents_Child(selection)
{
    let map = [];
    map.push(selection.innerHTML);
    let topli = document.querySelector("ul").children;
    let top = [];
    for (let i = 0; i < topli.length; i++)
    {
        if (topli[i].querySelector("span.is-parent"))
        {
            top.push(topli[i].querySelector("span.is-parent").innerHTML);
        }

        else
        {
            top.push(topli[i].querySelector("span#u-top-level").innerHTML);
        }
    }
    let name = selection.parentElement.parentElement.querySelector("span.is-parent").innerHTML;

    if (top.indexOf(name) !== -1)
    {
        if (map.indexOf(name) == -1)
        {
            map.unshift(name);
        }

        document.getElementById("road-map-text").innerHTML = map.join("  -->  ");
    }

    else
    {
        while (top.indexOf(name) === -1)
        {
            map.unshift(name);
            selection = selection.parentElement.parentElement.parentElement.querySelector("span.is-parent");
            name = selection.parentElement.parentElement.parentElement.querySelector("span.is-parent").innerHTML;
        }

        map.unshift(name);
        document.getElementById("road-map-text").innerHTML = map.join("  -->  ");
    }

}

if (document.getElementById("u-top-level"))
{
    document.getElementById("u-top-level").addEventListener("click", function ()
    {
        if (prevSelect != null)
        {
            prevSelect.classList.remove("current-selection");
        }

        this.classList.add("current-selection");
        prevSelect = this;
        document.getElementById("info-heading-p").innerHTML = this.innerHTML;

        var request = new XMLHttpRequest();

        request.open('GET', "https://en.wikipedia.org/api/rest_v1/page/summary/" + this.innerHTML, true);

        request.onload = function ()
        {
            let data = JSON.parse(this.response);

            if (request.status >= 200 && request.status < 400)
            {
                document.getElementById("info-detail-p").innerHTML = data.extract_html;

                if (typeof data.thumbnail == "undefined")
                {
                    document.getElementById("info-detail-image").src = "";
                }

                else
                {
                    document.getElementById("info-detail-image").src = data.thumbnail.source;
                }
            }
            else
            {
                document.getElementById("info-detail-p").innerHTML = "We had some trouble retrieving data from Wikipedia :(";
                document.getElementById("info-detail-image").src = "";
            }
        }

        request.send();

        document.getElementById("road-map-text").innerHTML = "Archeology";
    });
}

if (document.getElementById("signed-in-user"))
{
    document.getElementById("signed-in-user").addEventListener("click",
        function ()
        {
            document.querySelector(".profile-menu-wrapper").classList.toggle("profile-dropdown-active");
        });
}



AddEventIsIcon();
AddEventChildren();
AddEventParent();