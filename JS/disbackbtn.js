function noBack(){window.history.forward()}
noBack();
window.onload=noBack;
window.onpageshow=function(evt){if(evt.persisted)noBack()}


function clickIE() {
    var message = "Right click is disabled for security reasons!.";
    if (document.all)

    { alert(message); return false; }

}

function clickNS(e) {
    var message = "Right click is disabled for security reasons!.";
    if
(document.layers || (document.getElementById && !document.all)) {
        if (e.which == 2 || e.which == 3) { alert(message); return false; }
    }
}

if (document.layers)
    { document.captureEvents(Event.MOUSEDOWN); document.DEFANGED_Onmousedown = clickNS; }
else
    { document.DEFANGED_Onmouseup = clickNS; document.oncontextmenu = clickIE; }
document.oncontextmenu = function () { return false; };


