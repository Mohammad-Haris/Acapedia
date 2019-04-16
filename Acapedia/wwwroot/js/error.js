var _Code = document.getElementById("error-status-code").value;
var _Description = document.getElementById("error-description").value;
var _Path = window.location;

var input = '`<p id="error-code">> <span>ERROR CODE</span>: "<i>HTTP '+ _Code + '</i>"</p>`^300\n' +
    '`<p id="error-url">&nbsp;&nbsp;<span>REQUESTED URL</span>: "<i>'+ _Path +'</i>"</p>` ^200\n' +
    '`<p id="error-descrip">&nbsp;&nbsp;<span>ERROR DESCRIPTION</span>: "<i>' + _Description + '</i>" [For more information click <a href="https://httpstatuses.com/' + _Code + '" target="_blank">here</a>]</p>` ^200\n' +
    '`<p id="error-cause">&nbsp;&nbsp;<span>ERROR POSSIBLY CAUSED BY</span>: "<i>resource not found, execute access forbidden, read/write access forbidden, ssl required, ip address rejected etc</i>"</p>` ^500\n' +
    '`<p id="some-pages">> <span>SOME PAGES ON THIS SERVER TO VISIT</span>: <a href="/">Home</a> ---  <a href="/Explore">Explore</a> ---  <a href="/">About</a></p>` ^300\n' +
    '`<p id="error-request">&nbsp;&nbsp;<span>FEATURE/PAGE REQUEST:</span> <a href="/">Contact</a></p>` ^300\n' +
    '`<p id="error-support">&nbsp;&nbsp;<span>CONTRIBUTE:</span> <a href="/">Contribute</a></p>` ^200\n'+ 
    '<span id="error-end">> HAVE A NICE DAY :)</span>' + '<p></p>' + '`<span>&nbsp;&nbsp;&nbsp;&nbsp;></span>`';

var _Type = new Typed('#error-type', 
{
    strings: [input],
    typeSpeed: 15,
    backSpeed: 0,
    showCursor: true,
    cursorChar: '_',
    fadeOut: true,
    loop: false 
});