

function customValidator(val, valType) {

    var password = new RegExp('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$'); //^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[@#%^&+=]).{8,}$
    var numeric = new RegExp('^\\d*$');
    var alph = new RegExp('^[a-zA-Z-/. ]+$');
    var alphNum = new RegExp('^[0-9a-zA-Z.£@#$%^&+=()*-_/: ]+$'); //new RegExp('^[0-9a-zA-Z-_/. ]+$');  
    var alphNumSpec = new RegExp('^[0-9a-zA-Z.£@#$%~^&+=()*-_/: ]+$');
    var vDate = new RegExp('^(((0[1-9]|[12]\\d|3[01])\\/(0[13578]|1[02])\\/((19|[2-9]\\d)\\d{2}))|((0[1-9]|[12]\\d|30)\\/(0[13456789]|1[012])\\/((19|[2-9]\\d)\\d{2}))|((0[1-9]|1\\d|2[0-8])\\/02\\/((19|[2-9]\\d)\\d{2}))|(29\\/02\\/((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$');
    //var email = new RegExp('^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$"');
    //var email = new RegExp('/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/');
    var email = new RegExp('^(([\\w-]+\\.)+[\\w-]+|([a-zA-Z]{1}|[\\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z]+[\\w-]+\\.)+[a-zA-Z]{2,4})$');
    var amount = new RegExp(/^(-)?\d+(\.\d*)?$/);
    var accNo = new RegExp('^[0-9a-zA-Z]+$');

    var lpassword = new RegExp('^(?=.*[a-zA-Z])(?=.*[0-9])(?=.*?[@#%^&+=]).{6,}$');
    var isAmountNew = new RegExp('^(?:\d{1,5}(?:\.\d{0,2})?|\.\d{1,2})$');

    if (valType == enumVal.pwd) {
        return password.test(val);
    } else if (valType == enumVal.numer) {
        return numeric.test(val);
    } else if (valType == enumVal.alph) {
        return alph.test(val);
    } else if (valType == enumVal.alphNum) {
        return alphNum.test(val);
    } else if (valType == enumVal.alphaNumSpec) {
        return alphNumSpec.test(val);
    } else if (valType == enumVal.vDate) {
        return vDate.test(val);
    } else if (valType == enumVal.eMail) {
        return email.test(val);
    } else if (valType == enumVal.amnt) {
        return amount.test(val);
    } else if (valType == enumVal.accNo) {
        return accNo.test(val);
    } else if (valType == enumVal.pass) {
        return lpassword.test(val);
    } else if (valType == enumVal.isAmountNew) {
        return isAmountNew.test(val);
    }



}

function lengthValidate(val, min, max) {
    var lengthValidation = new RegExp('(.|\n){' + min + ',' + max + '}');
    return lengthValidation.test(val);
}

function compare(valone, valtwo) {
    if (valone == valtwo) {
        return true;
    } else {
        return false;

    }

}

var enumVal = { pwd: 0, numer: 1, alph: 2, alphNum: 3, alphaNumSpec: 4, vDate: 5, eMail: 6, amnt: 7, accNo: 8, length: 9, pass: 10, isAmountNew: 11 };

