var prevSelect;
var prevUniCall = new Array(2);
var prevOnCall;

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
        noChildren[i].addEventListener("click", ChildClick);
    }
}

document.getElementById("collapse-button").addEventListener("click", function ()
{
    let parent = document.querySelector(".active-discip-list");
    let children = parent.querySelectorAll("ul.children");
    for (let i = 0; i < children.length; i++)
    {
        if (children[i].classList.contains("active"))
        {
            children[i].classList.remove("active");
        }
    }

    let plus = parent.querySelectorAll("span.is-icon");
    for (let i = 0; i < plus.length; i++)
    {
        plus[i].innerHTML = "+";
    }
});

document.getElementById("expand-button").addEventListener("click", function ()
{
    let parent = document.querySelector(".active-discip-list");
    let children = parent.querySelectorAll("ul.children");
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

    let plus = parent.querySelectorAll("span.is-icon");
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
        parents[i].addEventListener("click", ParentClick);
    }
}

function GetParents_Parents(selection)
{    
    let map = [];
    map.push(selection.innerHTML);
    
    if (map[0] === "Archeology")
    {
        document.getElementById("road-map-text").innerHTML = "Social Sciences --> Archeology";
        return;
    }

    let parent = document.querySelector(".active-discip-list");
    let topli = parent.querySelector("ul").children;
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
        map.unshift(document.querySelector(".active-discip-heading").getElementsByTagName("p")[0].innerHTML);
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
        map.unshift(document.querySelector(".active-discip-heading").getElementsByTagName("p")[0].innerHTML);
        document.getElementById("road-map-text").innerHTML = map.join("  -->  ");
    }

}

function GetParents_Child(selection)
{
    let map = [];
    map.push(selection.innerHTML);
    let parent = document.querySelector(".active-discip-list");
    let topli = parent.querySelector("ul").children;
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

        map.unshift(document.querySelector(".active-discip-heading").getElementsByTagName("p")[0].innerHTML);
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
        map.unshift(document.querySelector(".active-discip-heading").getElementsByTagName("p")[0].innerHTML);
        document.getElementById("road-map-text").innerHTML = map.join("  -->  ");
    }

}

document.getElementById("u-top-level").addEventListener("click", ParentClick);

if (document.getElementById("signed-in-user"))
{
    document.getElementById("signed-in-user").addEventListener("click", function ()
        {
            document.querySelector(".profile-menu-wrapper").classList.toggle("profile-dropdown-active");
        });
}

function _SelectTab(selected) 
{
    selected.classList.add("tab-select");
    selected.querySelector(".arrow").classList.add("arrow-down");
    selected.querySelector(".arrow-inner").classList.add("arrow-down-inner");
}

document.querySelector(".basic-info-tab").addEventListener("click", function ()
{
    if (!this.classList.contains("tab-select"))
    {
        _SelectTab(this);
        this.querySelector(".tab-img-white").classList.remove("active-icon");
        this.querySelector(".tab-img-green").classList.add("active-icon");
        document.querySelector(".uni-tab").querySelector(".tab-img-white").classList.add("active-icon");
        document.querySelector(".uni-tab").querySelector(".tab-img-green").classList.remove("active-icon");
        document.querySelector(".online-tab").querySelector(".tab-img-white").classList.add("active-icon");
        document.querySelector(".online-tab").querySelector(".tab-img-green").classList.remove("active-icon");
        document.querySelector(".info-tab-content").classList.add("active");
        document.querySelector(".unis").classList.remove("active");
        document.querySelector(".online").classList.remove("active");
        InfoTab();
    }

    document.querySelector(".uni-tab").classList.remove("tab-select");
    document.querySelector(".uni-tab").querySelector(".arrow").classList.remove("arrow-down");
    document.querySelector(".uni-tab").querySelector(".arrow-inner").classList.remove("arrow-down-inner");

    document.querySelector(".online-tab").classList.remove("tab-select");
    document.querySelector(".online-tab").querySelector(".arrow").classList.remove("arrow-down");
    document.querySelector(".online-tab").querySelector(".arrow-inner").classList.remove("arrow-down-inner");
});

document.querySelector(".uni-tab").addEventListener("click", function ()
{
    if (!this.classList.contains("tab-select"))
    {
        _SelectTab(this);
        this.querySelector(".tab-img-white").classList.remove("active-icon");
        this.querySelector(".tab-img-green").classList.add("active-icon");
        document.querySelector(".basic-info-tab").querySelector(".tab-img-white").classList.add("active-icon");
        document.querySelector(".basic-info-tab").querySelector(".tab-img-green").classList.remove("active-icon");
        document.querySelector(".online-tab").querySelector(".tab-img-white").classList.add("active-icon");
        document.querySelector(".online-tab").querySelector(".tab-img-green").classList.remove("active-icon");
        document.querySelector(".unis").classList.add("active");
        document.querySelector(".info-tab-content").classList.remove("active");
        document.querySelector(".online").classList.remove("active");

        if (prevSelect)
        {
            let _CountrySelect = document.getElementById("uni-country");
            let _Country = _CountrySelect.options[_CountrySelect.selectedIndex].value;
            let _Discip = prevSelect.innerHTML;

            if (!prevUniCall || (prevUniCall[0] !== _Country || prevUniCall[1] !== _Discip))
            {
                prevUniCall[0] = _Country;
                prevUniCall[1] = _Discip;

                GetAndDisplayUniversities(_Country, _Discip);
            }
        }
    }

    document.querySelector(".basic-info-tab").classList.remove("tab-select");
    document.querySelector(".basic-info-tab").querySelector(".arrow").classList.remove("arrow-down");
    document.querySelector(".basic-info-tab").querySelector(".arrow-inner").classList.remove("arrow-down-inner");

    document.querySelector(".online-tab").classList.remove("tab-select");
    document.querySelector(".online-tab").querySelector(".arrow").classList.remove("arrow-down");
    document.querySelector(".online-tab").querySelector(".arrow-inner").classList.remove("arrow-down-inner");
});

document.querySelector(".online-tab").addEventListener("click", function ()
{
    if (!this.classList.contains("tab-select"))
    {
        _SelectTab(this);
        this.querySelector(".tab-img-white").classList.remove("active-icon");
        this.querySelector(".tab-img-green").classList.add("active-icon");
        document.querySelector(".basic-info-tab").querySelector(".tab-img-white").classList.add("active-icon");
        document.querySelector(".basic-info-tab").querySelector(".tab-img-green").classList.remove("active-icon");
        document.querySelector(".uni-tab").querySelector(".tab-img-white").classList.add("active-icon");
        document.querySelector(".uni-tab").querySelector(".tab-img-green").classList.remove("active-icon");
        document.querySelector(".online").classList.add("active");
        document.querySelector(".info-tab-content").classList.remove("active");
        document.querySelector(".unis").classList.remove("active");

        if (prevSelect)
        {
            let _Discip = prevSelect.innerHTML;

            if (!prevOnCall || prevOnCall !== _Discip)
            {
                prevOnCall = _Discip;

                GetAndDisplayOnline(_Discip);
            }
        }
    }

    document.querySelector(".basic-info-tab").classList.remove("tab-select");
    document.querySelector(".basic-info-tab").querySelector(".arrow").classList.remove("arrow-down");
    document.querySelector(".basic-info-tab").querySelector(".arrow-inner").classList.remove("arrow-down-inner");

    document.querySelector(".uni-tab").classList.remove("tab-select");
    document.querySelector(".uni-tab").querySelector(".arrow").classList.remove("arrow-down");
    document.querySelector(".uni-tab").querySelector(".arrow-inner").classList.remove("arrow-down-inner");
});

document.getElementById("uni-country").addEventListener("change", function ()
{
    if (prevSelect)
    {
        let _CountrySelect = document.getElementById("uni-country");
        let _Country = _CountrySelect.options[_CountrySelect.selectedIndex].value;
        let _Discip = prevSelect.innerHTML;

        console.log(_Country + " " + _Discip);

        GetAndDisplayUniversities(_Country, _Discip);
    }

    else
    {
        console.log("Select a discipline from the left");
    }
});

document.querySelectorAll(".humanities, .social-sciences, .natural-sciences, .formal-sciences, .prof-nd-app-sciences").forEach(function (elmt)
{
    elmt.addEventListener("click", function ()
    {
        if (document.querySelector(".active-discip-heading"))
        {
            document.querySelector(".active-discip-heading").classList.remove("active-discip-heading");
        }


        this.classList.add("active-discip-heading");

        if (document.querySelector(".active-discip-list"))
        {
            document.querySelector(".active-discip-list").classList.remove("active-discip-list");
        }

        document.querySelector("." + this.classList[0] + "-discips").classList.add("active-discip-list");

    });
});

function GetAndDisplayOnline(_Discipline)
{
    var request = new XMLHttpRequest();

    request.onreadystatechange = function ()
    {
        if (request.readyState == XMLHttpRequest.DONE)
        {
            if (request.status == 200)
            {
                DisplayLinksOnline(request.responseText);
            }

            else if (request.status == 400)
            {
                console.log("There was an error retrieving results!");
            }

            else 
            {
                console.log(request.status);
            }
        }
    };

    request.open("POST", "/Explore/GetOnline", true);

    request.setRequestHeader("Content-Type", "application/json");

    request.send(JSON.stringify([_Discipline]));
}

function GetAndDisplayUniversities(_Country, _Discipline)
{
    var request = new XMLHttpRequest();

    request.onreadystatechange = function ()
    {
        if (request.readyState == XMLHttpRequest.DONE)
        {
            if (request.status == 200)
            {
                DisplayLinks(request.responseText, _Country);
            }

            else if (request.status == 400)
            {
                console.log("There was an error retrieving results!");
            }

            else 
            {
                console.log(request.status);
            }
        }
    };

    request.open("POST", "/Explore/GetUniversities", true);

    request.setRequestHeader("Content-Type", "application/json");

    request.send(JSON.stringify([_Country, _Discipline]));
}

function DisplayLinksOnline(_LinkInfo)
{
    let LinkInfo = JSON.parse(_LinkInfo);
    let itr;
    let _OnlineDiv = document.querySelector(".online");
    let length = LinkInfo.length;

    document.querySelectorAll(".online-found, .online-links, .online-titles, .online-descrips").forEach(
        function (elmt) 
        {
            elmt.remove();
        });

    let found = document.createElement("p");
    let text = document.createTextNode("Found " + length + " results for " + prevSelect.innerHTML + " courses Online");
    found.appendChild(text);
    found.classList.add("online-found");
    _OnlineDiv.appendChild(found);

    for (itr = 0; itr < length; itr++)
    {
        let anchor = document.createElement("a");
        let node = document.createTextNode(LinkInfo[itr].Link);
        anchor.appendChild(node);
        anchor.setAttribute("href", LinkInfo[itr].Link);
        anchor.setAttribute("target", "_blank");
        anchor.classList.add("online-links");

        let title = document.createElement("a");
        node = document.createTextNode(LinkInfo[itr].Title);
        title.appendChild(node);
        title.setAttribute("href", LinkInfo[itr].Link);
        title.setAttribute("target", "_blank");
        title.classList.add("online-titles");

        let description = document.createElement("p");
        node = document.createTextNode(LinkInfo[itr].Description);
        description.appendChild(node);
        description.classList.add("online-descrips");

        _OnlineDiv.appendChild(title);
        _OnlineDiv.appendChild(anchor);
        _OnlineDiv.appendChild(description);
    }
}

function DisplayLinks(_LinkInfo, _Country)
{
    let LinkInfo = JSON.parse(_LinkInfo);
    let itr;
    let _UnisDiv = document.querySelector(".unis");
    let length = LinkInfo.length;

    document.querySelectorAll(".unis-found, .unis-links, .unis-titles, .unis-descrips").forEach(
        function (elmt) 
        {
            elmt.remove();
        });

    let found = document.createElement("p");
    let text = document.createTextNode("Found " + length + " results for " + prevSelect.innerHTML + " universities in " + _Country);
    found.appendChild(text);
    found.classList.add("unis-found");
    _UnisDiv.appendChild(found);

    for (itr = 0; itr < length; itr++)
    {
        let anchor = document.createElement("a");
        let node = document.createTextNode(LinkInfo[itr].Link);
        anchor.appendChild(node);
        anchor.setAttribute("href", LinkInfo[itr].Link);
        anchor.setAttribute("target", "_blank");
        anchor.classList.add("unis-links");

        let title = document.createElement("a");
        node = document.createTextNode(LinkInfo[itr].Title);
        title.appendChild(node);
        title.setAttribute("href", LinkInfo[itr].Link);
        title.setAttribute("target", "_blank");
        title.classList.add("unis-titles");

        let description = document.createElement("p");
        node = document.createTextNode(LinkInfo[itr].Description);
        description.appendChild(node);
        description.classList.add("unis-descrips");

        _UnisDiv.appendChild(title);
        _UnisDiv.appendChild(anchor);
        _UnisDiv.appendChild(description);
    }
}

function ParentClick()
{
    if (prevSelect != null)
    {
        prevSelect.classList.remove("current-selection");
    }

    this.classList.add("current-selection");
    prevSelect = this;

    switch (document.querySelector(".tab-select").getElementsByTagName("p")[0].innerHTML)
    {
        case "Info":
            InfoTab();
            break;

        case "On-Campus Resources":
            if (prevSelect)
            {
                let _CountrySelect = document.getElementById("uni-country");
                let _Country = _CountrySelect.options[_CountrySelect.selectedIndex].value;
                let _Discip = prevSelect.innerHTML;

                if (!prevUniCall || (prevUniCall[0] !== _Country || prevUniCall[1] !== _Discip))
                {
                    prevUniCall[0] = _Country;
                    prevUniCall[1] = _Discip;

                    console.log(_Country + " " + _Discip);

                    GetAndDisplayUniversities(_Country, _Discip);
                }
            }
            break;

        case "Online Resources":
            if (prevSelect)
            {
                let _Discip = prevSelect.innerHTML;

                if (!prevOnCall || prevOnCall !== _Discip)
                {
                    prevOnCall = _Discip;

                    GetAndDisplayOnline(_Discip);
                }
            }
            break;
    }

    GetParents_Parents(this);
}

function InfoTab()
{
    document.getElementById("info-heading-p").innerHTML = prevSelect.innerHTML;
    var request = new XMLHttpRequest();

    request.open('GET', "https://en.wikipedia.org/api/rest_v1/page/summary/" + prevSelect.innerHTML.split(" ").join("_"), true);

    request.onload = function ()
    {
        let data = JSON.parse(this.response);

        if (request.status >= 200 && request.status < 400)
        {
            document.getElementById("info-detail-p").innerHTML = data.extract_html;

            if (document.querySelector(".wiki-page-link"))
            {
                document.querySelector(".wiki-page-link").remove();
            }

            let elm = document.createElement("a");
            let node = document.createTextNode("View complete page on Wikipedia.org");
            elm.appendChild(node);
            elm.setAttribute("href", data.content_urls.desktop.page);
            elm.setAttribute("target", "_blank");
            elm.classList.add("wiki-page-link");
            document.getElementById("info-detail").appendChild(elm);

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
            if (document.querySelector(".wiki-page-link"))
            {
                document.querySelector(".wiki-page-link").remove();
            }
            document.getElementById("info-detail-p").innerHTML = "We had some trouble retrieving data from Wikipedia :(";
            document.getElementById("info-detail-image").src = "";
        }
    }

    request.send();
}

function ChildClick() 
{
    if (prevSelect != null)
    {
        prevSelect.classList.remove("current-selection");
    }

    this.classList.add("current-selection");
    prevSelect = this;

    switch (document.querySelector(".tab-select").getElementsByTagName("p")[0].innerHTML)
    {
        case "Info":
            InfoTab();
            break;

        case "On-Campus Resources":
            if (prevSelect)
            {
                let _CountrySelect = document.getElementById("uni-country");
                let _Country = _CountrySelect.options[_CountrySelect.selectedIndex].value;
                let _Discip = prevSelect.innerHTML;

                if (!prevUniCall || (prevUniCall[0] !== _Country || prevUniCall[1] !== _Discip))
                {
                    prevUniCall[0] = _Country;
                    prevUniCall[1] = _Discip;

                    console.log(_Country + " " + _Discip);

                    GetAndDisplayUniversities(_Country, _Discip);
                }
            }
            break;

        case "Online Resources":
            if (prevSelect)
            {
                let _Discip = prevSelect.innerHTML;

                if (!prevOnCall || prevOnCall !== _Discip)
                {
                    prevOnCall = _Discip;

                    GetAndDisplayOnline(_Discip);
                }
            }
            break;
    }

    GetParents_Child(this);
}

function Init()
{
    let select = document.querySelector(".humanities-discips").querySelector(".is-parent");
    select.classList.add("current-selection");
    prevSelect = select;
    InfoTab();
    GetParents_Parents(prevSelect);
}

window.onload = Init;
AddEventChildren();
AddEventParent();
AddEventIsIcon();