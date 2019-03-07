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

function autocomplete(inp, arr)
{
    /*the autocomplete function takes two arguments,
    the text field element and an array of possible autocompleted values:*/
    var currentFocus;
    /*execute a function when someone writes in the text field:*/
    inp.addEventListener("input", function (e)
    {
        var a, b, i, val = this.value;
        document.getElementById("on-campus-resources-countries").classList.add("search-active");
        /*close any already open lists of autocompleted values*/
        closeAllLists();
        if (!val) { return false; }
        currentFocus = -1;
        /*create a DIV element that will contain the items (values):*/
        a = document.createElement("DIV");
        a.setAttribute("id", this.id + "on-campus-resources-countries-list");
        a.setAttribute("class", "on-campus-resources-countries-items");
        /*append the DIV element as a child of the autocomplete container:*/
        this.parentNode.appendChild(a);
        /*for each item in the array...*/
        for (i = 0; i < arr.length; i++)
        {
            /*check if the item starts with the same letters as the text field value:*/
            if (arr[i].substr(0, val.length).toUpperCase() == val.toUpperCase())
            {
                /*create a DIV element for each matching element:*/
                b = document.createElement("DIV");
                /*make the matching letters bold:*/
                b.innerHTML = "<strong>" + arr[i].substr(0, val.length) + "</strong>";
                b.innerHTML += arr[i].substr(val.length);
                /*insert a input field that will hold the current array item's value:*/
                b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                /*execute a function when someone clicks on the item value (DIV element):*/
                b.addEventListener("click", function (e)
                {
                    /*insert the value for the autocomplete text field:*/
                    inp.value = this.getElementsByTagName("input")[0].value;
                    /*close the list of autocompleted values,
                    (or any other open lists of autocompleted values:*/
                    closeAllLists();
                });
                a.appendChild(b);
            }
        }
    });
    /*execute a function presses a key on the keyboard:*/
    inp.addEventListener("keydown", function (e)
    {
        var x = document.getElementById(this.id + "on-campus-resources-countries-list");
        if (x) x = x.getElementsByTagName("div");
        if (e.keyCode == 40)
        {
            /*If the arrow DOWN key is pressed,
            increase the currentFocus variable:*/
            currentFocus++;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 38)
        { //up
            /*If the arrow UP key is pressed,
            decrease the currentFocus variable:*/
            currentFocus--;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 13)
        {
            /*If the ENTER key is pressed, prevent the form from being submitted,*/
            e.preventDefault();
            if (currentFocus > -1)
            {
                /*and simulate a click on the "active" item:*/
                if (x) x[currentFocus].click();

                if (prevCoun !== document.getElementById("country-input").value)
                {
                    GetUniversities();
                    prevCoun = document.getElementById("country-input").value;
                }

                document.getElementById("country-input").blur();
            }
        }
    });
    function addActive(x)
    {
        /*a function to classify an item as "active":*/
        if (!x) return false;
        /*start by removing the "active" class on all items:*/
        removeActive(x);
        if (currentFocus >= x.length) currentFocus = 0;
        if (currentFocus < 0) currentFocus = (x.length - 1);
        /*add class "autocomplete-active":*/
        x[currentFocus].classList.add("on-campus-resources-countries-active");
    }
    function removeActive(x)
    {
        /*a function to remove the "active" class from all autocomplete items:*/
        for (var i = 0; i < x.length; i++)
        {
            x[i].classList.remove("on-campus-resources-countries-active");
        }
    }
    function closeAllLists(elmnt)
    {
        /*close all autocomplete lists in the document,
        except the one passed as an argument:*/
        var x = document.getElementsByClassName("on-campus-resources-countries-items");
        for (var i = 0; i < x.length; i++)
        {
            if (elmnt != x[i] && elmnt != inp)
            {
                x[i].parentNode.removeChild(x[i]);
            }
        }
    }
    /*execute a function when someone clicks in the document:*/
    document.addEventListener("click", function (e)
    {
        closeAllLists(e.target);
    });
}

function GetUniversities()
{
    let coun = document.getElementById("country-input").value;
    let discip = document.getElementById("info-heading-p").innerHTML;

    var xmlhttp = new XMLHttpRequest();

    xmlhttp.onreadystatechange = function ()
    {
        if (xmlhttp.readyState == XMLHttpRequest.DONE)
        {   
            if (xmlhttp.status == 200)
            {
                console.log(xmlhttp.responseText);
            }
            else if (xmlhttp.status == 400)
            {
                console.log('There was an error 400');
            }
            else
            {
                console.log(xmlhttp.status);
            }
        }
    };

    xmlhttp.open("POST", "/Explore/GetUniversities", true);
    xmlhttp.setRequestHeader("Content-Type", "application/json");
    xmlhttp.send(JSON.stringify(
    {
            Country: coun,
            Discipline: discip
    }));
}

function Init()
{
    prevCoun = document.getElementById("country-input").value;
}


document.getElementById("add-on-campus-resource").addEventListener("click", function ()
{
    let coun = "Pakistan";
    let unis = ["University of Engineering and Technology, Lahore", "University of Engineering and Technology, Taxila"];
    let discip = "Electrical engineering"; //should be just the current selected discipline
                                           // no option will be provided for choosing discipline here

    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function () 
    {
        if (xmlhttp.readyState == XMLHttpRequest.DONE)
        {
            if (xmlhttp.status == 200)
            {
                console.log(xmlhttp.responseText);
            }
            else if (xmlhttp.status == 400)
            {
                console.log('There was an error 400');
            }
            else
            {
                console.log(xmlhttp.status);
            }
        }
    };

    xmlhttp.open("POST", "/Explore/InsertUniversities", true);
    xmlhttp.setRequestHeader("Content-Type", "application/json");
    xmlhttp.send(JSON.stringify(
        {
            "Country": coun,
            "Universities": unis,
            "Discipline": discip
        }
        ))
});

var countries = ["Afghanistan", "Albania", "Algeria", "American Samoa", "Andorra", "Angola", "Anguilla", "Antarctica", "Antigua and Barbuda", "Argentina", "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Bouvet Island", "Brazil", "British Indian Ocean Territory", "Brunei Darussalam", "Bulgaria", "Burkina Faso", "Burundi", "Cambodia", "Cameroon", "Canada", "Cape Verde", "Cayman Islands", "Central African Republic", "Chad", "Chile", "China", "Christmas Island", "Cocos (Keeling) Islands", "Colombia", "Comoros", "Congo", "Cook Islands", "Costa Rica", "Croatia (Hrvatska)", "Cuba", "Cyprus", "Czech Republic", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "East Timor", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia", "Falkland Islands (Malvinas)", "Faroe Islands", "Fiji", "Finland", "France", "France, Metropolitan", "French Guiana", "French Polynesia", "French Southern Territories", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Gibraltar", "Greece", "Greenland", "Grenada", "Guadeloupe", "Guam", "Guatemala", "Guernsey", "Guinea", "Guinea-Bissau", "Guyana", "Haiti", "Heard and Mc Donald Islands", "Honduras", "Hong Kong", "Hungary", "Iceland", "India", "Indonesia", "Iran (Islamic Republic of)", "Iraq", "Ireland", "Isle of Man", "Israel", "Italy", "Ivory Coast", "Jamaica", "Japan", "Jersey", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Korea, Democratic People's Republic of", "Korea, Republic of", "Kosovo", "Kuwait", "Kyrgyzstan", "Lao People's Democratic Republic", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libyan Arab Jamahiriya", "Liechtenstein", "Lithuania", "Luxembourg", "Macau", "Macedonia", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Martinique", "Mauritania", "Mauritius", "Mayotte", "Mexico", "Micronesia, Federated States of", "Moldova, Republic of", "Monaco", "Mongolia", "Montenegro", "Montserrat", "Morocco", "Mozambique", "Myanmar", "Namibia", "Nauru", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia", "New Zealand", "Nicaragua", "Niger", "Nigeria", "Niue", "Norfolk Island", "Northern Mariana Islands", "Norway", "Oman", "Pakistan", "Palau", "Palestine", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Pitcairn", "Poland", "Portugal", "Puerto Rico", "Qatar", "Reunion", "Romania", "Russian Federation", "Rwanda", "Saint Kitts and Nevis", "Saint Lucia", "Saint Vincent and the Grenadines", "Samoa", "San Marino", "Sao Tome and Principe", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "South Africa", "South Georgia South Sandwich Islands", "South Sudan", "Spain", "Sri Lanka", "St. Helena", "St. Pierre and Miquelon", "Sudan", "Suriname", "Svalbard and Jan Mayen Islands", "Swaziland", "Sweden", "Switzerland", "Syrian Arab Republic", "Taiwan", "Tajikistan", "Tanzania, United Republic of", "Thailand", "Togo", "Tokelau", "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey", "Turkmenistan", "Turks and Caicos Islands", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "United States", "United States minor outlying islands", "Uruguay", "Uzbekistan", "Vanuatu", "Vatican City State", "Venezuela", "Vietnam", "Virgin Islands (British)", "Virgin Islands (U.S.)", "Wallis and Futuna Islands", "Western Sahara", "Yemen", "Zaire", "Zambia", "Zimbabwe"];

autocomplete(document.getElementById("country-input"), countries);

AddEventIsIcon();
AddEventChildren();
AddEventParent();
window.onload = Init;