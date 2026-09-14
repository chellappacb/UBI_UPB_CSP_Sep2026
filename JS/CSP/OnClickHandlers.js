// Named, external equivalents of every distinct inline event-handler attribute value found
// app-wide (onclick/OnClientClick, onload, onunload, onmouseover/onmouseout, onkeydown).
// Each function body is a byte-for-byte copy of the original inline attribute text - no
// behavior changes, including any dead/unreachable code that was already present (e.g. a
// statement after an unconditional `return` never ran before this migration either, and
// still won't). JS/CSP/Home.js matches each element's live inline-attribute value against
// this map at runtime and swaps it for a real property-assigned handler referencing one of
// these functions, so CSP no longer needs 'unsafe-inline' on script-src-attr at all.
var CspOnClickHandlers = {
    "AddRequestHandler()": function () {
        return AddRequestHandler();
    },
    "callCloseEvent();": function () {
        callCloseEvent();
    },
    "this.stop();": function () {
        this.stop();
    },
    "this.start();": function () {
        this.start();
    },
    "preventEnterKey(event)": function (event) {
        return preventEnterKey(event);
    },
    "Begin()": function () {
        return Begin();
    },
    "End()": function () {
        return End();
    },
    "return confirm('Your keyed in data will be lost. Do you really want to exit from the application ?');": function () {
        return confirm('Your keyed in data will be lost. Do you really want to exit from the application ?');
    },
    "return confirm('Are you sure you want to delete this applicant?');": function () {
        return confirm('Are you sure you want to delete this applicant?');
    },
    "return VerifyEmailAdd();javascript:show_progress();": function () {
        return VerifyEmailAdd(); javascript: show_progress();
    },
    "return validate('txtCurPostCode');": function () {
        return validate('txtCurPostCode');
    },
    "return validate('txtPrePostCode');": function () {
        return validate('txtPrePostCode');
    },
    "return BtnClick();javascript:show_progress();": function () {
        return BtnClick(); javascript: show_progress();
    },
    "return SaveAndRetnLater();javascript:show_progress();": function () {
        return SaveAndRetnLater(); javascript: show_progress();
    },
    "Base64()": function () {
        return Base64();
    },
    "HideModalPopup('modalBehaviorEmail');Base64();": function () {
        HideModalPopup('modalBehaviorEmail'); Base64();
    },
    "var b=submitBond();if(b) var b=Check();return b;": function () {
        var b = submitBond(); if (b) var b = Check(); return b;
    },
    "return updateBond();": function () {
        return updateBond();
    },
    "return Check()": function () {
        return Check();
    },
    "clearCaptchaText();": function () {
        clearCaptchaText();
    },
    "return submitRetExisApp();": function () {
        return submitRetExisApp();
    },

    // Found via CSP Report-Only 'sample' data, not the original static markup scan - these
    // are rendered by the MacroWebTextBox custom control (Bin/MacroWebControls.dll) as
    // onkeyup/onblur attributes, so a grep of .aspx source for OnClientClick=/onclick=
    // could never have found them.
    "Val(this,'AlphaNumEmail')": function () {
        Val(this, 'AlphaNumEmail');
    },
    "Val(this,'isAlphaNum')": function () {
        Val(this, 'isAlphaNum');
    },
    "Val(this,'isNumeric')": function () {
        Val(this, 'isNumeric');
    },
    "Val(this,'isDate')": function () {
        Val(this, 'isDate');
    },
    "Val(this,'None')": function () {
        Val(this, 'None');
    },
    "Val(this,'isAlpha')": function () {
        Val(this, 'isAlpha');
    },
    "Val(this,'isAmount')": function () {
        Val(this, 'isAmount');
    },
    "Validate(this)": function () {
        Validate(this);
    },
    "this.style.background ='White'": function () {
        this.style.background = 'White';
    },

    // Standard ASP.NET-emitted onkeypress for certain TextBox/AutoPostBack/TextMode
    // combinations. The Report-Only sample was truncated at 40 chars
    // ("if (WebForm_TextBoxKeyHandler(event) == …"), so this is the well-known standard
    // Framework snippet rather than something read byte-for-byte from this app's rendered
    // output - if it's not an exact match, the console.warn fallback in Home.js will surface
    // the real text on the next test pass so this can be corrected precisely.
    "if (WebForm_TextBoxKeyHandler(event) == false) return false;": function (event) {
        if (WebForm_TextBoxKeyHandler(event) == false) return false;
    }
};
