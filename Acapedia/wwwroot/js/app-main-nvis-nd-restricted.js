AppMain();

function AppMain()
{
    var prevSelect;
    var prevUniCall = new Array(2);
    var prevOnCall;
    var prevWikiCall;
    var last = new Date().getTime();

    function AddEventCEIcon()
    {
        let toggler = document.getElementsByClassName("is-icon");
        for (let i = 0; i < toggler.length; i++)
        {
            toggler[i].addEventListener("click", CEEvent);
        }
    }

    async function CEEvent()
    {
        let child = this.parentElement.querySelector(".children");

        if (child.classList.contains("active"))
        {
            child.classList.add("animate-none");
            await Sleep(280);
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
    }

    function Sleep(ms)
    {
        return new Promise(resolve => setTimeout(resolve, ms));
    }

    document.getElementById("collapse-button").addEventListener("click", CollapseAllClick);

    document.getElementById("expand-button").addEventListener("click", ExpandAllClick);

    function AddEventChildren()
    {
        var noChildren = document.getElementsByClassName("no-child");

        for (let i = 0; i < noChildren.length; i++)
        {
            noChildren[i].addEventListener("click", ChildClick);
        }
    }

    function AddEventParent()
    {
        let parents = document.getElementsByClassName("is-parent");

        for (let i = 0; i < parents.length; i++)
        {
            parents[i].addEventListener("click", ParentClick);
        }
    }

    document.querySelector(".basic-info-tab").addEventListener("click", BasicInfoTabClick);

    document.querySelector(".uni-tab").addEventListener("click", UniTabClick);

    document.querySelector(".online-tab").addEventListener("click", OnlineTabClick);

    document.getElementById("uni-country").addEventListener("change", CountryChange);

    function GetAndDisplayParents_Parents(selection)
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
            map.unshift(document.querySelector(".active-discip-heading").querySelector(".discip-heading-title").innerHTML);
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
            map.unshift(document.querySelector(".active-discip-heading").querySelector(".discip-heading-title").innerHTML);
        }

        DisplayParents(map);
    }

    function GetAndDisplayParents_Child(selection)
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

            map.unshift(document.querySelector(".active-discip-heading").querySelector(".discip-heading-title").innerHTML);
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
            map.unshift(document.querySelector(".active-discip-heading").querySelector(".discip-heading-title").innerHTML);
        }

        DisplayParents(map);
    }

    function DisplayParents(_parents)
    {
        document.getElementById("road-map-text").innerHTML = _parents.join("  -->  ");
    }

    document.getElementById("u-top-level").addEventListener("click", ParentClick);

    function _SelectTab(selected) 
    {
        selected.classList.add("tab-select");
        selected.querySelector(".arrow").classList.add("arrow-down");
        selected.querySelector(".arrow-inner").classList.add("arrow-down-inner");
        selected.querySelector(".tab-img-white").classList.remove("active-icon");
        selected.querySelector(".tab-img-green").classList.add("active-icon");
    }

    document.querySelectorAll(".humanities, .social-sciences, .natural-sciences, .formal-sciences, .prof-nd-app-sciences").forEach(TopDiscipHeadingClick);

    async function GetAndDisplayOnline(_Discipline)
    {
        var OnlineContent = document.querySelector(".online-content");
        var OnlineTabContent = document.querySelector(".online");

        OnlineContent.classList.add("remove-display");
        OnlineTabContent.querySelector(".lds-ring").classList.add("loading");

        await SendCall();

        var request = new XMLHttpRequest();

        request.onload = function ()
        {
            DisplayLinksOnline(this.response, request.status);

            OnlineTabContent.querySelector(".lds-ring").classList.remove("loading");
            OnlineContent.classList.remove("remove-display");
        };

        request.onerror = function ()
        {
            DisplayLinksOnline(null, 500);

            OnlineTabContent.querySelector(".lds-ring").classList.remove("loading");
            OnlineContent.classList.remove("remove-display");
        };

        request.open("POST", "/Explore/GetOnline", true);

        request.setRequestHeader("Content-Type", "application/json");

        request.setRequestHeader("RequestVerificationToken",
            document.getElementById('RequestVerificationToken').value);

        request.send(JSON.stringify([_Discipline]));
    }

    async function GetAndDisplayUniversities(_Country, _Discipline)
    {
        var UniContent = document.querySelector(".unis-content");
        var UniTabContent = document.querySelector(".unis");

        UniContent.classList.add("remove-display");
        UniTabContent.querySelector(".lds-ring").classList.add("loading");

        await SendCall();

        var request = new XMLHttpRequest();

        request.onload = function ()
        {
            DisplayLinksUniversities(this.response, _Country, request.status);

            UniTabContent.querySelector(".lds-ring").classList.remove("loading");
            UniContent.classList.remove("remove-display");
        };

        request.onerror = function ()
        {
            DisplayLinksUniversities(null, _Country, 500);

            UniTabContent.querySelector(".lds-ring").classList.remove("loading");
            UniContent.classList.remove("remove-display");
        };

        request.open("POST", "/Explore/GetUniversities", true);

        request.setRequestHeader("Content-Type", "application/json");

        request.setRequestHeader("RequestVerificationToken",
            document.getElementById('RequestVerificationToken').value);

        request.send(JSON.stringify([_Country, _Discipline]));
    }

    function DisplayLinksOnline(_LinkInfo, _RequestStatus)
    {
        RemovePreviousOnlineLinks();
        let _OnlineDiv = document.querySelector(".online-content");

        if (_RequestStatus == 200)
        {
            let LinkInfo = JSON.parse(_LinkInfo);
            let itr;
            let length = LinkInfo.length;
            let text = "Found " + length + " results for " + prevSelect.innerHTML + " courses Online";
            let fragment = document.createDocumentFragment();

            fragment.appendChild(CreateElement("p", text, "", "", "online-found"));

            for (itr = 0; itr < length; itr++)
            {
                fragment.appendChild(CreateElement("a", LinkInfo[itr].Title, LinkInfo[itr].Link, "_blank", "online-titles"));
                fragment.appendChild(CreateElement("a", LinkInfo[itr].Link, LinkInfo[itr].Link, "_blank", "online-links"));
                fragment.appendChild(CreateElement("p", LinkInfo[itr].Description, "", "", "online-descrips"));
            }

            _OnlineDiv.appendChild(fragment);
        }

        else 
        {
            _OnlineDiv.appendChild(CreateElement("p", "There was an error retrieving results from the database :(", "", "", "online-descrips"));
            _OnlineDiv.appendChild(CreateElement("p", "You might be sending requests too frequently.", "", "", "online-descrips"));
        }
    }

    function DisplayLinksUniversities(_LinkInfo, _Country, _RequestStatus)
    {
        RemovePreviousUniLinks();
        let _UnisDiv = document.querySelector(".unis-content");

        if (_RequestStatus == 200)
        {
            let LinkInfo = JSON.parse(_LinkInfo);
            let itr;
            let length = LinkInfo.length;
            let text = `Found ${length} results for ${prevSelect.innerHTML} universities in ${_Country}`;
            let fragment = document.createDocumentFragment();

            fragment.appendChild(CreateElement("p", text, "", "", "unis-found"));

            for (itr = 0; itr < length; itr++)
            {
                fragment.appendChild(CreateElement("a", LinkInfo[itr].Title, LinkInfo[itr].Link, "_blank", "unis-titles"));
                fragment.appendChild(CreateElement("a", LinkInfo[itr].Link, LinkInfo[itr].Link, "_blank", "unis-links"));
                fragment.appendChild(CreateElement("p", LinkInfo[itr].Description, "", "", "unis-descrips"));
            }

            _UnisDiv.append(fragment);
        }

        else
        {
            _UnisDiv.appendChild(CreateElement("p", "There was an error retrieving results from the database :(", "", "", "unis-descrips"));
            _UnisDiv.appendChild(CreateElement("p", "You might be sending requests too frequently.", "", "", "unis-descrips"));

        }
    }

    function ParentClick()
    {
        let _Changed = SelectDiscip(this);

        switch (document.querySelector(".tab-select").querySelector(".tab-name").innerHTML)
        {
            case "Info":
                GetAndDisplayWiki(_Changed);
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

        GetAndDisplayParents_Parents(this);
        HideRMenu();
    }

    async function GetAndDisplayWiki(_IsChanged)
    {
        if (_IsChanged)
        {
            prevWikiCall = prevSelect;

            var InfoContent = document.querySelector(".info-content");
            var InfoTabContent = document.querySelector(".info-tab-content");

            InfoContent.classList.add("remove-display");
            InfoTabContent.querySelector(".lds-ring").classList.add("loading");

            await SendCall();

            var request = new XMLHttpRequest();

            request.onload = function ()
            {
                DisplayWiki(this.response, request.status);

                InfoTabContent.querySelector(".lds-ring").classList.remove("loading");
                InfoContent.classList.remove("remove-display");
            };

            request.onerror = function ()
            {
                DisplayWiki(null, 500);

                InfoTabContent.querySelector(".lds-ring").classList.remove("loading");
                InfoContent.classList.remove("remove-display");
            };

            request.open('GET', "https://en.wikipedia.org/api/rest_v1/page/summary/" + prevSelect.innerHTML.split(" ").join("_"), true);

            request.setRequestHeader("Api-User-Agent", "contact.acapedia@gmail.com");

            request.send();
        }
    }

    function ChildClick() 
    {
        let _Changed = SelectDiscip(this);

        switch (document.querySelector(".tab-select").querySelector(".tab-name").innerHTML)
        {
            case "Info":
                GetAndDisplayWiki(_Changed);
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

        GetAndDisplayParents_Child(this);
        HideRMenu();
    }

    function DisplayWiki(_ResponseData, _Status)
    {
        document.getElementById("info-heading-p").innerHTML = prevSelect.innerHTML;

        if (_Status == 200)
        {
            RemovePreviousWikiInfo();

            let data = JSON.parse(_ResponseData);

            document.getElementById("info-detail-p").innerHTML = data.extract_html;

            document.getElementById("info-detail").appendChild(CreateElement("a", "View complete page on Wikipedia.org", data.content_urls.desktop.page, "_blank",
                "wiki-page-link"));

            if (!data.thumbnail)
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

    function CollapseAllClick()
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
    }

    function ExpandAllClick()
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
    }

    function BasicInfoTabClick()
    {
        if (!this.classList.contains("tab-select"))
        {
            _SelectTab(this);

            document.querySelector(".uni-tab").querySelector(".tab-img-white").classList.add("active-icon");
            document.querySelector(".uni-tab").querySelector(".tab-img-green").classList.remove("active-icon");

            document.querySelector(".online-tab").querySelector(".tab-img-white").classList.add("active-icon");
            document.querySelector(".online-tab").querySelector(".tab-img-green").classList.remove("active-icon");

            document.querySelector(".info-tab-content").classList.add("active");
            document.querySelector(".unis").classList.remove("active");
            document.querySelector(".online").classList.remove("active");

            GetAndDisplayWiki(prevSelect != prevWikiCall);
        }

        document.querySelector(".uni-tab").classList.remove("tab-select");
        document.querySelector(".uni-tab").querySelector(".arrow").classList.remove("arrow-down");
        document.querySelector(".uni-tab").querySelector(".arrow-inner").classList.remove("arrow-down-inner");

        document.querySelector(".online-tab").classList.remove("tab-select");
        document.querySelector(".online-tab").querySelector(".arrow").classList.remove("arrow-down");
        document.querySelector(".online-tab").querySelector(".arrow-inner").classList.remove("arrow-down-inner");
    }

    function UniTabClick()
    {
        if (!this.classList.contains("tab-select"))
        {
            _SelectTab(this);

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
    }

    function OnlineTabClick()
    {
        if (!this.classList.contains("tab-select"))
        {
            _SelectTab(this);

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
    }

    function CountryChange()
    {
        if (prevSelect)
        {
            let _CountrySelect = document.getElementById("uni-country");
            let _Country = _CountrySelect.options[_CountrySelect.selectedIndex].value;
            let _Discip = prevSelect.innerHTML;

            GetAndDisplayUniversities(_Country, _Discip);
        }
    }

    function TopDiscipHeadingClick(elmt)
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
    }

    function CreateElement(_type, _content, _href, _target, _class)
    {
        let elmt = document.createElement(_type);
        elmt.appendChild(document.createTextNode(_content));

        if (_href)
        {
            elmt.setAttribute("href", _href);
        }

        if (_target)
        {
            elmt.setAttribute("target", "_blank");
        }

        if (_class)
        {
            elmt.classList.add(_class);
        }

        return elmt;
    }

    function RemovePreviousOnlineLinks()
    {
        document.querySelectorAll(".online-found, .online-links, .online-titles, .online-descrips").forEach(
            function (elmt) 
            {
                elmt.remove();
            });
    }

    function RemovePreviousUniLinks() 
    {
        document.querySelectorAll(".unis-found, .unis-links, .unis-titles, .unis-descrips").forEach(
            function (elmt) 
            {
                elmt.remove();
            });
    }

    function RemovePreviousWikiInfo() 
    {
        if (document.querySelector(".wiki-page-link"))
        {
            document.querySelector(".wiki-page-link").remove();
        }
    }

    function SelectDiscip(_Current)
    {
        if (prevSelect)
        {
            prevSelect.classList.remove("current-selection");
        }

        _Current.classList.add("current-selection");
        prevSelect = _Current;

        return _Current != prevWikiCall;
    }

    function Init()
    {
        let select = document.querySelector(".humanities-discips").querySelector(".is-parent");
        select.classList.add("current-selection");
        prevSelect = select;
        GetAndDisplayWiki(true);
        GetAndDisplayParents_Parents(prevSelect);
    }

    async function SendCall()
    {
        let now = new Date().getTime();

        if ((Math.abs(last - now) / 1000) < 3)
        {
            await Sleep(1000);
        }

        last = new Date().getTime();
    }

    async function HideRMenu()
    {
        if (window.screen.width <= 1000 && !document.querySelector(".top-div").classList.contains("hide-menu"))
        {
            document.querySelector(".top-div").classList.add("slide-out-left");
            await Sleep(210);
            document.querySelector(".top-div").classList.add("hide-menu");
            document.querySelector(".top-div").classList.remove("slide-out-left");
        }
    }

    async function ToggleRMenu()
    {
        var menu = document.querySelector(".top-div").classList;

        if (menu.contains("hide-menu"))
        {
            document.querySelector(".top-div").classList.remove("hide-menu");
        }

        else
        {
            document.querySelector(".top-div").classList.add("slide-out-left");
            await Sleep(210);
            document.querySelector(".top-div").classList.add("hide-menu");
            document.querySelector(".top-div").classList.remove("slide-out-left");
        }
    }

    document.getElementById("hamburger").addEventListener("click", ToggleRMenu);

    window.onload = Init;
    AddEventChildren();
    AddEventParent();
    AddEventCEIcon();
}