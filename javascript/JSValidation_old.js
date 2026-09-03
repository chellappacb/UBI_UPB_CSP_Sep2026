//function check() {
//    var isCheck = true;
//    //alert("hidfdf");
//    if (BtnClick() && checkradio()) {
//        //alert("SUCCESS");
//        isCheck = true;
//    }
//    //else if (checkradio()) {
//    //   alert("trueradio");
//    //    isCheck = true;
//    // }

//    else {
//        //alert("false1ccc");
//        isCheck = false;
//    }

//    if (isCheck == false) {

//        document.getElementById("errormsg").innerText = "Please fill mantatory fields";
//        $('#errormsg').addClass('lblbox error');
//    }
//    return isCheck;

//}

//function checkradio() {

//    //alert("radio");
//    var isValidrad = true;

//    var isgcheck = false;
//    var genradio = document.getElementsByName("rblGender");
//    for (var i = 0; i < genradio.length; i++) {
//        if (genradio[i].checked) {
//            //alert("gendertrue");
//            $('#rblGender').removeClass('txtbox error');
//            isgcheck = true;
//            break;
//        }
//    }
//    if (!isgcheck) {
//        //alert("genderfalse");
//        $('#rblGender').addClass('txtbox error');
//        isValidrad = false;
//        //$('#rblGender').focus();
//    }



//    var isidencheck = false;
//    var idenradio = document.getElementsByName("rblIdentity");
//    for (var i = 0; i < idenradio.length; i++) {

//        if (idenradio[i].checked) {
//            //alert("identitytrue");
//            $('#rblIdentity').removeClass('txtbox error');
//            isidencheck = true;
//            break;
//        }

//    }
//    if (!isidencheck) {
//        //alert("identityfalse");
//        $('#rblIdentity').addClass('txtbox error');
//        isValidrad = false;
//        //$('#rblIdentity').focus();
//    }



//    var passcheckradio = document.getElementsByName("rblIdentity");
//    for (var i = 0; i < passcheckradio.length; i++) {
//        if (passcheckradio[i].checked) {
//            var value = passcheckradio[i].value;
//        }
//    }
//    //alert(value);
//    if (value == "DRIVING LICENCE") {
//        //alert("DVLSUCCESS");
//        var isdltypecheck = false;
//        var dltyperadio = document.getElementsByName("rbldltype");
//        for (var i = 0; i < dltyperadio.length; i++) {

//            if (dltyperadio[i].checked) {
//                //alert("dltyptrue");
//                $('#rbldltype').removeClass('txtbox error');
//                isdltypecheck = true;
//                break;
//            }
//        }
//        if (!isdltypecheck) {
//            $('#rbldltype').addClass('txtbox error');
//            isValidrad = false;
//            //$('#rbldltype').focus();
//        }

//    }

//    else if (value == "PASSPORT") {
//        //alert("PASSPORT");
//        var isrblpasscheck = false;
//        var rblpassradio = document.getElementsByName("rblPassport");
//        for (var i = 0; i < rblpassradio.length; i++) {

//            if (rblpassradio[i].checked) {
//                //alert("passporttrue");
//                $('#rblPassport').removeClass('txtbox error');
//                isrblpasscheck = true;
//                break;
//            }
//        }
//        if (!isrblpasscheck) {
//            //alert("passportfalse");
//            $('#rblPassport').addClass('txtbox error');
//            isValidrad = false;
//            //$('#rblPassport').focus();
//        }

//    }


//    var isuspersoncheck = false;
//    var uspersonradio = document.getElementsByName("rblUSPerson");
//    for (var i = 0; i < uspersonradio.length; i++) {

//        if (uspersonradio[i].checked) {
//            //alert("uspersontrue");
//            $('#rblUSPerson').removeClass('txtbox error');
//            isuspersoncheck = true;
//            break;
//        }
//    }
//    if (!isuspersoncheck) {
//        //alert("uspersonfalse");
//        $('#rblUSPerson').addClass('txtbox error');
//        isValidrad = false;
//        //$('#rblUSPerson').focus();
//    }







//    //alert(isValidrad);
//    return isValidrad;
//}

function BtnClick() {
    var isValid = true;

    //alert("textbox");

    $('#ddlTittle,#txtFirstNm,#txtSurNm,#CalDOB,#ddlMaritalSts,#txtPlaceOfBirth,#txtMomName,#ddlCtznShp,#txtMobileNo,#txtEmailAddr,#txtDRLNo1,#txtDRLNo2,#txtDRLNo3,#txtDVLCPcode,#txtUKPassportL1,#txtUKPassportL2,#txtUKPassportL3,#txtUKPassport1,#txtUKPassport2,#txtUKPassport3,#txtUKPassport4,#txtUKPassport5,#txtUKPassport6,#txtUKPassport7,#txtIntPassportL1,#txtIntPassportL2,#txtIntPassportL3,#txtIntPassport1,#txtIntPassport2,#txtIntPassport3,#txtIntPassport4,#txtIntPassport5,#txtIntPassport6,#txtIntPassport7,#txtIntPassport8,#txtIntPassport9,#ddlPsPrtIsCntry,#ddlEmpType,#txtEmptypOth,#txtCurPostCode,#txtCurDrNo,#txtCurAddr3,#ddlCurCountry,#txtRsdnSnc,#txtPrePostCode,#txtPreDrNo,#txtPreAddr3,#txtPriJuri,#txtTin1,#txtReasonTin').each(function () {
        //alert("hi1");

        $('input[type="text"]').tooltip();
        $('input[type="password"]').tooltip();
        $('select ').tooltip();

        if ($(this).is("input")) {


            if ($(this).attr('name') == 'txtFirstNm') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'FirstName Cannot be blank');
                    //$(this).focus();
                    isValid = false;
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtSurNm') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'SurName Cannot be blank');
                    //$(this).focus();
                    isValid = false;
                }
                else if (!lengthValidate($.trim($(this).val()), 2, 30)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Surname should be miniumum 2 characters');
                    //$(this).focus();
                    isValid = false;
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }



            else if ($(this).attr('name') == 'txtPlaceOfBirth') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Place of birth cannot be blank');
                    //$(this).focus();
                    isValid = false;
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtMomName') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Mothers maiden name cannot be blank');
                    //$(this).focus();
                    isValid = false;
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtMobileNo') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('Utxtbox error');
                    $(this).attr('data-original-title', 'Mobile No. cannot be blank');
                    //$(this).focus();
                    isValid = false;
                }
                else if (!lengthValidate($.trim($(this).val()), 11, 11)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Mobile No should be 11 Numbers');
                    //$(this).focus();
                    isValid = false;
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtEmailAddr') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Email Address cannot be blank');
                    //$(this).focus();
                    isValid = false;
                }
                else if (!customValidator($.trim($(this).val()), enumVal.eMail)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Please enter valid email format');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }
            //Driving Licence
            else if ($(this).attr('name') == 'txtDRLNo1') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'DVLA No. 1 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'DVLA No. 1 allows only AlphaNumeric (a-z 0-9) characters');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 5, 5)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'DVLA No. 1 should be 5 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtDRLNo2') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'DVLA No. 2 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'DVLA No. 2 allows only AlphaNumeric (a-z 0-9) characters');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 6, 6)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'DVLA No. 2 should be 6 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtDRLNo3') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'DVLA No. 3 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'DVLA No. 3 allows only AlphaNumeric (a-z 0-9) characters');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 5, 5)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'DVLA No. 3 should be 5 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtDVLCPcode') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Driving license postcode cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }
            //            //Driving Licence


            //Passport
            else if ($(this).attr('name') == 'txtUKPassportL1') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', '1st line Passport No. field 1 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', '1st line Passport No. field 1 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 2, 2)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', '1st line Passport No. field 1 should be 2 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtUKPassportL2') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', '1st line Passport No. field 2 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', '1st line Passport No. field 2 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 3, 3)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', '1st line Passport No. field 2 should be 3 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtUKPassportL3') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', '1st line Passport No. field 3 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', '1st line Passport No. field 3 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 39, 39)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', '1st line Passport No. field 3 should be 39 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }




            else if ($(this).attr('name') == 'txtUKPassport1') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 1 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 1 accept only Alphanumeric!');
                    $(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 10, 10)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 1 should be 10 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtUKPassport2') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 2 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 2 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 3, 3)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 2 should be 3 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtUKPassport3') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 3 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 3 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 7, 7)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 3 should be 7 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtUKPassport4') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 4 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 4 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 1, 1)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 4 should be 1 character');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtUKPassport5') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 5 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 5 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 7, 7)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 5 should be 7 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtUKPassport6') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 6 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 6 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 14, 14)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 6 should be 14 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }


            else if ($(this).attr('name') == 'txtUKPassport7') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 7 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 7 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 2, 2)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 7 should be 2 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }







            //intpassport
            else if ($(this).attr('name') == 'txtIntPassportL1') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', '1st line Passport No. field 1 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', '1st line Passport No. field 1 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 2, 2)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', '1st line Passport No. field 1 should be 2 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtIntPassportL2') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', '1st line Passport No. field 2 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', '1st line Passport No. field 2 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 3, 3)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', '1st line Passport No. field 2 should be 3 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtIntPassportL3') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', '1st line Passport No. field 3 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', '1st line Passport No. field 3 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 39, 39)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', '1st line Passport No. field 3 should be 39 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }



            else if ($(this).attr('name') == 'txtIntPassport1') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 1 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 1 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 9, 9)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 1 should be 9 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtIntPassport2') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 2 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 2 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 1, 1)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 2 should be 1 character');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtIntPassport3') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 3 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 3 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 3, 3)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 3 should be 3 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }


            else if ($(this).attr('name') == 'txtIntPassport4') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 4 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 4 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 7, 7)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 4 should be 7 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }


            else if ($(this).attr('name') == 'txtIntPassport5') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 5 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 5 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 1, 1)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 5 should be 1 character');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }


            else if ($(this).attr('name') == 'txtIntPassport6') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 6 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 6 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 7, 7)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 6 should be 7 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }


            else if ($(this).attr('name') == 'txtIntPassport7') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 7 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 7 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 14, 14)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 7 should be 14 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }


            else if ($(this).attr('name') == 'txtIntPassport8') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 8 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 8 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 1, 1)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 8 should be 1 character');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtIntPassport9') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 9 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 9 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 1, 1)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Passport No. field 9 should be 1 character');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            //Employee Details
            else if ($(this).attr('name') == 'txtEmptypOth') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Details of Employment(if Other) cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            //Address
            else if ($(this).attr('name') == 'txtCurPostCode') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Current Address Postal Code cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtCurDrNo') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Address- Line 1  cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtCurAddr3') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Current Address City cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtRsdnSnc') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Residing Since date cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.vDate)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Invalid Residing Since, Date must be dd/MM/yyyy format.');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }


            //Previous Address
            else if ($(this).attr('name') == 'txtPrePostCode') {
                //alert("PAddress");
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Previous Address Postal Code cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtPreDrNo') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Previous Address- Line 1  cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtPreAddr3') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Previous Address City cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }
            //Previous Address



            else if (($("#txtPriJuri").val()) == '' && ($("#txtTin1").val()) == '' && ($("#txtReasonTin").val()) == '') {
                // alert("success us Primary Jurisdiction cannot be blank");

                if ($(this).attr('name') == 'txtPriJuri') {
                    if ($.trim($(this).val()) == '') {
                        //alert("Primtextboxtrue");
                        $(this).addClass('txtbox error');
                        $(this).attr('data-original-title', 'Primary Jurisdiction cannot be blank');
                        isValid = false;
                        //$(this).focus();
                    }
                    else {
                        //alert("Primtextboxerror");
                        $(this).removeClass('txtbox error');
                        $(this).addClass('txtbox');
                        $(this).attr('data-original-title', '');
                    }
                }

                else if ($(this).attr('name') == 'txtTin1') {
                    if ($.trim($(this).val()) == '') {
                        //alert("tintextboxtrue");
                        $(this).addClass('txtbox error');
                        $(this).attr('data-original-title', 'TIN cannot be blank');
                        isValid = false;
                        //$(this).focus();
                    }
                    else {
                        //alert("tintextboxtrue");
                        $(this).removeClass('txtbox error');
                        $(this).addClass('txtbox');
                        $(this).attr('data-original-title', '');
                    }
                }
            }

            else if (($("#txtPriJuri").val()) != '' && ($("#txtTin1").val()) == '') {
                //alert("tin error");
                if ($(this).attr('name') == 'txtTin1') {
                    if ($.trim($(this).val()) == '') {
                        //alert("tintextboxerror");
                        $('#txtTin1').addClass('txtbox error');
                        $('#txtPriJuri').removeClass('txtbox error');
                        $('#txtTin1').attr('data-original-title', 'TIN cannot be blank');
                        isValid = false;
                        //$(this).focus();
                    }
                    else {
                        //alert("tintextboxtrue");
                        $('#txtPriJuri').addClass('txtbox');
                        $('#txtPriJuri').attr('data-original-title', '');
                    }
                }
            }

            else if (($("#txtPriJuri").val()) == '' && ($("#txtTin1").val()) != '') {
                //alert("primary error");
                if ($(this).attr('name') == 'txtPriJuri') {
                    if ($.trim($(this).val()) == '') {
                        //alert("Primtextboxerror");
                        $('#txtPriJuri').addClass('txtbox error');
                        $('#txtTin1').removeClass('txtbox error');
                        $('#txtPriJuri').attr('data-original-title', 'Primary Jurisdiction cannot be blank');
                        isValid = false;
                        //$(this).focus();
                    }
                    else {
                        //alert("Primtextboxtrue");
                        $('#txtTin1').addClass('txtbox');
                        $('#txtTin1').attr('data-original-title', '');
                    }
                }
            }


            else {
                $(this).removeClass('txtbox error');
                $(this).addClass('txtbox');
                $(this).attr('data-original-title', '');
            }
        }




        else if ($(this).is("select")) {

            if ($(this).attr('name') == 'ddlTittle') {
                if ($(this)[0].selectedIndex == 0) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Please Select the Title');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'CalDOB') {
                if ($(this)[0].selectedIndex == 0) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Please Select DOB');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'ddlMaritalSts') {
                if ($(this)[0].selectedIndex == 0) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Please Select Marital Status');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'ddlCtznShp') {
                if ($(this)[0].selectedIndex == 0) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Please Select Citizenship');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'ddlPsPrtIsCntry') {
                if ($(this)[0].selectedIndex == 0) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Please Select Country of Issue');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'ddlEmpType') {
                if ($(this)[0].selectedIndex == 0) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Please Select the Employment detail');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'ddlCurCountry') {
                if ($(this)[0].selectedIndex == 0) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Current Address Country cannot be blank');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else {
                $(this).removeClass('txtbox error');
                $(this).addClass('txtbox');
            }
        }
    });



    //    if (isValidfname == true && isValidsname == true) {
    //        isValid = true;
    //    }
    //    else {
    //        isValid = false;
    //    }

    //alert(isValid);

    var isgcheck = false;
    var genradio = document.getElementsByName("rblGender");
    for (var i = 0; i < genradio.length; i++) {
        if (genradio[i].checked) {
            //alert("gendertrue");
            $('#rblGender').removeClass('txtbox error');
            isgcheck = true;
            break;
        }
    }
    if (!isgcheck) {
        //alert("genderfalse");
        $('#rblGender').addClass('txtbox error');

        isValid = false;
        //$('#rblGender').focus();
    }



    var isidencheck = false;
    var idenradio = document.getElementsByName("rblIdentity");
    for (var i = 0; i < idenradio.length; i++) {

        if (idenradio[i].checked) {
            //alert("identitytrue");
            $('#rblIdentity').removeClass('txtbox error');
            isidencheck = true;
            break;
        }

    }
    if (!isidencheck) {
        //alert("identityfalse");
        $('#rblIdentity').addClass('txtbox error');
        isValid = false;
        //$('#rblIdentity').focus();
    }



    var passcheckradio = document.getElementsByName("rblIdentity");
    for (var i = 0; i < passcheckradio.length; i++) {
        if (passcheckradio[i].checked) {
            var value = passcheckradio[i].value;
        }
    }
    //alert(value);
    if (value == "DRIVING LICENCE") {
        //alert("DVLSUCCESS");
        var isdltypecheck = false;
        var dltyperadio = document.getElementsByName("rbldltype");
        for (var i = 0; i < dltyperadio.length; i++) {

            if (dltyperadio[i].checked) {
                //alert("dltyptrue");
                $('#rbldltype').removeClass('txtbox error');
                isdltypecheck = true;
                break;
            }
        }
        if (!isdltypecheck) {
            //alert("dltypfalse");
            $('#rbldltype').addClass('txtbox error');
            isValid = false;
            //$('#rbldltype').focus();
        }

    }

    else if (value == "PASSPORT") {
        //alert("PASSPORT");
        var isrblpasscheck = false;
        var rblpassradio = document.getElementsByName("rblPassport");
        for (var i = 0; i < rblpassradio.length; i++) {

            if (rblpassradio[i].checked) {
                //alert("passporttrue");
                $('#rblPassport').removeClass('txtbox error');
                isrblpasscheck = true;
                break;
            }
        }
        if (!isrblpasscheck) {
            //alert("passportfalse");
            $('#rblPassport').addClass('txtbox error');
            isValid = false;
            //$('#rblPassport').focus();
        }

    }


    var isuspersoncheck = false;
    var uspersonradio = document.getElementsByName("rblUSPerson");
    for (var i = 0; i < uspersonradio.length; i++) {

        if (uspersonradio[i].checked) {
            //alert("uspersontrue");
            $('#rblUSPerson').removeClass('txtbox error');
            isuspersoncheck = true;
            break;
        }
    }
    if (!isuspersoncheck) {
        //alert("uspersonfalse");
        $('#rblUSPerson').addClass('txtbox error');
        isValid = false;
        //$('#rblUSPerson').focus();
    }



    if (isValid == false) {

        document.getElementById("errormsg").innerText = "Please fill mandatory fields";
        $('#errormsg').addClass('lblbox error');
    }

    return isValid;

}





function submitBond() {
    var isValidBond = true;

    //alert("bond");

    $('#txtAmt,#ddlPeriod,#txtOthBankName,#txtOthBankSC1,#txtOthBankSC2,#txtOthBankSC3,#txtOthBankAcNo').each(function () {
        //alert("bondtext");

        $('input[type="text"]').tooltip();
        $('input[type="password"]').tooltip();
        $('select ').tooltip();

        if ($(this).is("input")) {
            //alert("bondinput");

            if ($(this).attr('name') == 'txtAmt') {
                //alert("amt");

                if ($.trim($(this).val()) == '') {
                    //alert("case1");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Please enter Amount');
                    //$(this).focus();
                    isValidBond = false;
                }
                else if (!customValidator($.trim($(this).val()), enumVal.amnt)) {
                    //alert("case2");
                    isValidBond = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Invalid Amount');
                    //$(this).focus();
                }
                //                !lengthValidate($.trim($(this).val()), 1, 2)
                else if (!(($.trim($(this).val())) >= 1000 && ($.trim($(this).val())) <= 340000)) {
                    //alert("case3");

                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Amount should be between £1000 to £340,000');
                    isValidBond = false;
                    //$(this).focus();
                }
                else {
                    //alert("case4");
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtOthBankName') {
                //alert("bankname");
                if ($.trim($(this).val()) == '') {
                    //alert("bondinputerror");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Please enter Name of the other bank');
                    //$(this).focus();
                    isValidBond = false;
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtOthBankSC1') {
                //alert("sc1");
                if ($.trim($(this).val()) == '') {
                    //alert("bondinputerror");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Please enter Sort Code1');
                    //$(this).focus();
                    isValidBond = false;
                }
                else if (!customValidator($.trim($(this).val()), enumVal.numer)) {
                    isValidBond = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Sort Code1 accept only Numeric Digit');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 2, 2)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Sort Code1 should be two Numeric Digit');
                    isValidBond = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtOthBankSC2') {
                //alert("sc2");
                if ($.trim($(this).val()) == '') {
                    //alert("bondinputerror");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Please enter Sort Code2');
                    //$(this).focus();
                    isValidBond = false;
                }
                else if (!customValidator($.trim($(this).val()), enumVal.numer)) {
                    isValidBond = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Sort Code2 accept only Numeric Digit');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 2, 2)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Sort Code2 should be two Numeric Digit');
                    isValidBond = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtOthBankSC3') {
                //alert("sc3");
                if ($.trim($(this).val()) == '') {
                    //alert("bondinputerror");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Please enter Sort Code3');
                    //$(this).focus();
                    isValidBond = false;
                }
                else if (!customValidator($.trim($(this).val()), enumVal.numer)) {
                    isValidBond = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Sort Code3 accept only Numeric Digit');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 2, 2)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Sort Code3 should be two Numeric Digit');
                    isValidBond = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }


            else if ($(this).attr('name') == 'txtOthBankAcNo') {
                //alert("acno");
                if ($.trim($(this).val()) == '') {
                    //alert("bondinputerror");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Please enter other bank Account No');
                    //$(this).focus();
                    isValidBond = false;
                }
                else if (!customValidator($.trim($(this).val()), enumVal.numer)) {
                    isValidBond = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Other bank Account Number should be Numeric(0-9)');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 8, 8)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Other bank Account Number should be 8 characters');
                    isValidBond = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else {
                $(this).removeClass('txtbox error');
                $(this).addClass('txtbox');
                $(this).attr('data-original-title', '');
            }
        }




        else if ($(this).is("select")) {
            //alert("period");
            if ($(this).attr('name') == 'ddlPeriod') {
                if ($(this)[0].selectedIndex == 0) {
                    isValidBond = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Please Select Period');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }



            else {
                $(this).removeClass('txtbox error');
                $(this).addClass('txtbox');
            }
        }


    });


    //rblRepayInst
    var ischeckRepayInst = false;
    var rblpayInst = document.getElementsByName("rblRepayInst");
    for (var i = 0; i < rblpayInst.length; i++) {
        if (rblpayInst[i].checked) {
            //alert("bondradiotrue");
            $('#rblRepayInst').removeClass('txtbox error');
            $('#rblRepayInst').attr('data-original-title', 'Please select Repayment Instruction');
            ischeckRepayInst = true;
            break;
        }
    }
    if (!ischeckRepayInst) {
        //alert("bondradiofalse");
        $('#rblRepayInst').addClass('txtbox error');
        isValidBond = false;
        //$('#rblGender').focus();
    }


    //    var chkbox1 = false;
    //    var chkPassport = document.getElementById("chkTermsAndConditions");
    //    if (chkPassport.checked) {
    //        chkbox1 = true;
    //    }
    //    else {
    //        alert("checkbox1");
    //        alert("Please tick the terms & conditions for the further procedings");
    //        isValidBond = false;
    //    }

    //    var chkbox2 = false;
    //    var chkJoint = document.getElementById("chkIfJoint");
    //    if (chkJoint.checked) {
    //        chkbox2 = true;
    //    }
    //    else {
    //        alert("checkbox2");
    //        alert("Please tick Primary applicant accessibility control");
    //        isValidBond = false;
    //    }

    var chkbox1 = false;
    var isChecked = $("#chkTermsAndConditions").is(":checked");
    if (isChecked) {
        chkbox1 = true;
    } else {
        alert("Please tick the terms and conditions for further enrollment process!");
        //$('#chkTermsAndConditions').addClass('chkbox error');
        isValidBond = false;
    }

    var chkbox2 = false;
    var isCheckedJoint = $("#chkIfJoint").is(":checked");
    if (isCheckedJoint) {
        chkbox2 = true;
    } else {
        alert("Please tick Primary applicant accessibility control");
        //$('#chkTermsAndConditions').addClass('chkbox error');
        isValidBond = false;
    }
    //alert(isValidBond);
    if (isValidBond == false) {

        document.getElementById("errormsg").innerText = "Please fill mantatory fields";
        $('#errormsg').addClass('lblbox error');
    }
    return isValidBond;
}


function submitRetExisApp() {
    var isValidRetExistApp = true;

    //alert("Pass");

    $('#txtReferNo,#txtEmailId,#txtPassword,#txtkey').each(function () {

        $('input[type="text"]').tooltip();
        $('input[type="password"]').tooltip();
        $('select ').tooltip();

        if ($(this).is("input")) {

            if ($(this).attr('name') == 'txtReferNo') {

                if ($.trim($(this).val()) == '') {
                    //alert("case1");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Reference Number cannot be blank!');
                    isValidRetExistApp = false;
                }

                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }


            else if ($(this).attr('name') == 'txtEmailId') {

                if ($.trim($(this).val()) == '') {
                    //alert("case2");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Email Address cannot be blank');
                    isValidRetExistApp = false;
                }
                else if (!customValidator($.trim($(this).val()), enumVal.eMail)) {
                    isValidRetExistApp = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Invalid Email Id!');
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtPassword') {

                if ($.trim($(this).val()) == '') {
                    //alert("case3");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Password cannot be blank');
                    isValidRetExistApp = false;
                }

                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }


            else if ($(this).attr('name') == 'txtkey') {

                if ($.trim($(this).val()) == '') {
                    //alert("case3");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-original-title', 'Enter the characters as they are shown in the image above.');
                    isValidRetExistApp = false;
                }

                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-original-title', '');
                }
            }

            else {
                $(this).removeClass('txtbox error');
                $(this).addClass('txtbox');
                $(this).attr('data-original-title', '');
            }
        }



    });


    //alert(isValidRetExistApp);
    if (isValidRetExistApp == false) {

        document.getElementById("errormsg").innerText = "Please fill mantatory fields";
        $('#errormsg').addClass('lblbox error');
    }
    return isValidRetExistApp;
}