function BtnClick() {
    var isValid = true;

    /* UPBv2 Aug-2021 */
    var isRSDateValid = true;
    //alert("textbox");

    //$('#ddlTittle,#txtFirstNm,#txtSurNm,#CalDOB,#txtPlaceOfBirth,#txtMomName,#ddlCtznShp,#txtMobileNo,#txtEmailAddr,#txtDRLNo1,#txtDRLNo2,#txtDRLNo3,#txtDVLCPcode,#txtUKPassportL1,#txtUKPassportL2,#txtUKPassportL3,#txtUKPassport1,#txtUKPassport2,#txtUKPassport3,#txtUKPassport4,#txtUKPassport5,#txtUKPassport6,#txtUKPassport7,#txtIntPassportL1,#txtIntPassportL2,#txtIntPassportL3,#txtIntPassport1,#txtIntPassport2,#txtIntPassport3,#txtIntPassport4,#txtIntPassport5,#txtIntPassport6,#txtIntPassport7,#txtIntPassport8,#txtIntPassport9,#ddlPsPrtIsCntry,#ddlEmpType,#txtEmptypOth,#txtCurPostCode,#txtCurDrNo,#txtCurAddr1,#txtCurAddr3,#ddlCurCountry,#txtRsdnSnc,#txtPrePostCode,#txtPreDrNo,#txtPreAddr3,#ddlPriJuri,#txtTin1,#txtReasonTin,#ddlSOF,#txtSOFOth').each(function () {
    $('#ddlTittle,#txtFirstNm,#txtSurNm,#CalDOB,#txtPlaceOfBirth,#txtMomName,#ddlCtznShp,#txtMobileNo,#txtEmailAddr,#txtDRLNo1,#txtDRLNo2,#txtDRLNo3,#txtDVLCPcode,#txtUKPassportL1,#txtUKPassportL2,#txtUKPassportL3,#txtUKPassport1,#txtUKPassport2,#txtUKPassport3,#txtUKPassport4,#txtUKPassport5,#txtUKPassport6,#txtUKPassport7,#txtIntPassportL1,#txtIntPassportL2,#txtIntPassportL3,#txtIntPassport1,#txtIntPassport2,#txtIntPassport3,#txtIntPassport4,#txtIntPassport5,#txtIntPassport6,#txtIntPassport7,#txtIntPassport8,#txtIntPassport9,#ddlPsPrtIsCntry,#ddlEmpType,#txtEmptypOth,#txtCurPostCode,#txtCurDrNo,#txtCurAddr1,#txtCurAddr2,#txtCurAddr3,#ddlCurCity,#ddlCurCountry,#txtRsdnSnc,#txtPrePostCode,#txtPreDrNo,#txtPreAddr2,#txtPreAddr3,#ddlPreCity,#ddlPreCountry,#ddlPriJuri,#txtTin1,#txtReasonTin,#ddlSOF,#txtSOFOth').each(function () {
        //alert("hi1");

        $('input[type="text"]').tooltip();
        $('input[type="password"]').tooltip();
        $('select ').tooltip();

        if ($(this).is("input")) {


            if ($(this).attr('name') == 'txtFirstNm') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'FirstName Cannot be blank');
                    //$(this).focus();
                    isValid = false;
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtSurNm') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'SurName Cannot be blank');
                    //$(this).focus();
                    isValid = false;
                }
                else if (!lengthValidate($.trim($(this).val()), 2, 30)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Surname should be miniumum 2 characters');
                    //$(this).focus();
                    isValid = false;
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }



            else if ($(this).attr('name') == 'txtPlaceOfBirth') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Place of birth cannot be blank');
                    //$(this).focus();
                    isValid = false;
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtMomName') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Mothers maiden name cannot be blank');
                    //$(this).focus();
                    isValid = false;
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtMobileNo') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('Utxtbox error');
                    $(this).attr('data-bs-original-title', 'Mobile No. cannot be blank');
                    //$(this).focus();
                    isValid = false;
                }
                else if (!lengthValidate($.trim($(this).val()), 10, 10)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Mobile No should be 10 Numbers');
                    //$(this).focus();
                    isValid = false;
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtEmailAddr') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Email Address cannot be blank');
                    //$(this).focus();
                    isValid = false;
                }
                else if (!customValidator($.trim($(this).val()), enumVal.eMail)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please enter valid email format');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }
            //Driving Licence
            else if ($(this).attr('name') == 'txtDRLNo1') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'DVLA No. 1 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'DVLA No. 1 allows only AlphaNumeric (a-z 0-9) characters');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 5, 5)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'DVLA No. 1 should be 5 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtDRLNo2') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'DVLA No. 2 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'DVLA No. 2 allows only AlphaNumeric (a-z 0-9) characters');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 6, 6)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'DVLA No. 2 should be 6 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtDRLNo3') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'DVLA No. 3 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'DVLA No. 3 allows only AlphaNumeric (a-z 0-9) characters');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 5, 5)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'DVLA No. 3 should be 5 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtDVLCPcode') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Driving license postcode cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }
            //            //Driving Licence


            //Passport
            else if ($(this).attr('name') == 'txtUKPassportL1') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', '1st line Passport No. field 1 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', '1st line Passport No. field 1 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 2, 2)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', '1st line Passport No. field 1 should be 2 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtUKPassportL2') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', '1st line Passport No. field 2 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', '1st line Passport No. field 2 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 3, 3)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', '1st line Passport No. field 2 should be 3 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtUKPassportL3') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', '1st line Passport No. field 3 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', '1st line Passport No. field 3 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 39, 39)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', '1st line Passport No. field 3 should be 39 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }




            else if ($(this).attr('name') == 'txtUKPassport1') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 1 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 1 accept only Alphanumeric!');
                    $(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 10, 10)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 1 should be 10 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtUKPassport2') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 2 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 2 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 3, 3)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 2 should be 3 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtUKPassport3') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 3 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 3 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 7, 7)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 3 should be 7 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtUKPassport4') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 4 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 4 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 1, 1)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 4 should be 1 character');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtUKPassport5') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 5 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 5 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 7, 7)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 5 should be 7 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtUKPassport6') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 6 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 6 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 14, 14)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 6 should be 14 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }


            else if ($(this).attr('name') == 'txtUKPassport7') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 7 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 7 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 2, 2)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 7 should be 2 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }







            //intpassport
            else if ($(this).attr('name') == 'txtIntPassportL1') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', '1st line Passport No. field 1 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', '1st line Passport No. field 1 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 2, 2)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', '1st line Passport No. field 1 should be 2 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtIntPassportL2') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', '1st line Passport No. field 2 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', '1st line Passport No. field 2 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 3, 3)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', '1st line Passport No. field 2 should be 3 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtIntPassportL3') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', '1st line Passport No. field 3 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', '1st line Passport No. field 3 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 39, 39)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', '1st line Passport No. field 3 should be 39 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }



            else if ($(this).attr('name') == 'txtIntPassport1') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 1 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 1 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 9, 9)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 1 should be 9 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtIntPassport2') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 2 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 2 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 1, 1)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 2 should be 1 character');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtIntPassport3') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 3 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 3 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 3, 3)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 3 should be 3 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }


            else if ($(this).attr('name') == 'txtIntPassport4') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 4 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 4 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 7, 7)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 4 should be 7 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }


            else if ($(this).attr('name') == 'txtIntPassport5') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 5 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 5 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 1, 1)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 5 should be 1 character');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }


            else if ($(this).attr('name') == 'txtIntPassport6') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 6 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 6 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 7, 7)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 6 should be 7 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }


            else if ($(this).attr('name') == 'txtIntPassport7') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 7 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 7 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 14, 14)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 7 should be 14 characters');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }


            else if ($(this).attr('name') == 'txtIntPassport8') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 8 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 8 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 1, 1)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 8 should be 1 character');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtIntPassport9') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 9 cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.alphNum)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 9 accept only Alphanumeric!');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 1, 1)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Passport No. field 9 should be 1 character');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            //Employee Details
            else if ($(this).attr('name') == 'txtEmptypOth') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Details of Employment(if Other) cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtSOFOth') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'SOF Other cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            //Address
            else if ($(this).attr('name') == 'txtCurPostCode') {

                var sCAPCodeValue = $(this).val();
                var sFCAPCodeValue = sCAPCodeValue.replace(/\s/g, "");  //Remove space from string
                var regex = /^.{5,7}$/; // Regular expression for minimum 5 and maximum 7 characters

                ////get 4th character from last in a string 
                //var fourthFromLast = sCAPCodeValue.charAt(sCAPCodeValue.length - 4);
                //console.log(fourthFromLast);
                ////End

                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Current Address Postal Code cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!regex.test(sFCAPCodeValue)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'The postcode you provided does not conform to a valid UK postcode format.');
                    isValid = false;
                    //$(this).focus();
                }
                //else if (fourthFromLast.indexOf(' ') === -1) {
                //    console.log("not space");
                //    $(this).addClass('txtbox error');
                //    $(this).attr('data-bs-original-title', 'The postcode you provided does not conform to a valid UK postcode format.');
                //    isValid = false;
                //}
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if (($(this).attr('name') == 'txtCurDrNo') || ($(this).attr('name') == 'txtCurAddr1')) {

                if ($.trim($("#txtCurDrNo").val()) == '' && $.trim($("#txtCurAddr1").val()) == '') {

                    //$(this).addClass('txtbox error');

                    $("#txtCurDrNo").addClass("txtbox error");
                    $("#txtCurAddr1").addClass("txtbox error");

                    $("#txtCurDrNo").attr('data-bs-original-title', 'Both the House/Flat No and Building Name fields cannot be left blank. Please fill in at least one of these fields');
                    $("#txtCurAddr1").attr('data-bs-original-title', 'Both the House/Flat No and Building Name fields cannot be left blank. Please fill in at least one of these fields');

                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $("#txtCurDrNo").removeClass('txtbox error');
                    $("#txtCurAddr1").removeClass('txtbox error');

                    $("#txtCurDrNo").addClass('txtbox');
                    $("#txtCurAddr1").addClass('txtbox');

                    $("#txtCurDrNo").attr('data-bs-original-title', '');
                    $("#txtCurAddr1").attr('data-bs-original-title', '');

                    //$(this).removeClass('txtbox error');
                    //$(this).addClass('txtbox');
                    //$(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtCurAddr2') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Current Address Street cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtCurAddr3') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Current Address City cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtRsdnSnc') {
                if ($.trim($(this).val()) == '') {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Residing Since date cannot be blank');
                    //$(this).focus();
                }
                else if (!customValidator($.trim($(this).val()), enumVal.vDate)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Invalid Residing Since, Date must be dd/MM/yyyy format.');
                    /* UPBv2 Aug-2021 */
                    isRSDateValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }


            //Previous Address
            else if ($(this).attr('name') == 'txtPrePostCode') {
                var sPAPCodeValue = $(this).val();
                var sFPAPCodeValue = sPAPCodeValue.replace(/\s/g, "");
                var regex = /^.{5,7}$/;

                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Previous Address Postal Code cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else if (!regex.test(sFPAPCodeValue)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'The postcode you provided does not conform to a valid UK postcode format.');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if (($(this).attr('name') == 'txtPreDrNo') || ($(this).attr('name') == 'txtPreAddr1')) {

                if ($.trim($("#txtPreDrNo").val()) == '' && $.trim($("#txtPreAddr1").val()) == '') {

                    //$(this).addClass('txtbox error');
                    //$(this).attr('data-bs-original-title', 'Previous Address- Line 1  cannot be blank');

                    $("#txtPreDrNo").addClass("txtbox error");
                    $("#txtPreAddr1").addClass("txtbox error");

                    $("#txtPreDrNo").attr('data-bs-original-title', 'Both the House/Flat No and Building Name fields cannot be left blank. Please fill in at least one of these fields');
                    $("#txtPreAddr1").attr('data-bs-original-title', 'Both the House/Flat No and Building Name fields cannot be left blank. Please fill in at least one of these fields');

                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $("#txtPreDrNo").removeClass('txtbox error');
                    $("#txtPreAddr1").removeClass('txtbox error');

                    $("#txtPreDrNo").addClass('txtbox');
                    $("#txtPreAddr1").addClass('txtbox');

                    $("#txtPreDrNo").attr('data-bs-original-title', '');
                    $("#txtPreAddr1").attr('data-bs-original-title', '');

                    //$(this).removeClass('txtbox error');
                    //$(this).addClass('txtbox');
                    //$(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtPreAddr2') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Previous Address Street cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            //Previous Address

            //            if ($(this).attr('name') == 'ddlTittle') {
            //                if ($(this)[0].selectedIndex == 0) {
            //                    isValid = false;
            //                    $(this).addClass('txtbox error');
            //                    $(this).attr('data-bs-original-title', 'Please Select the Title');
            //                    //$(this).focus();
            //                }
            //                else {
            //                    $(this).removeClass('txtbox error');
            //                    $(this).addClass('txtbox');
            //                    $(this).attr('data-bs-original-title', '');
            //                }
            //            }

            else if (($("#ddlPriJuri").val()) == '-1' && ($("#txtTin1").val()) == '' && ($("#txtReasonTin").val()) == '') {
                // alert("success us Primary Jurisdiction cannot be blank");



                if ($(this).attr('name') == 'txtReasonTin') {
                    if ($.trim($(this).val()) == '') {
                        //alert("Primtextboxtrue");
                        $(this).addClass('txtbox error');
                        $(this).attr('data-bs-original-title', 'Reason cannot be blank');
                        isValid = false;
                        //$(this).focus();
                    }
                    else {
                        //alert("Primtextboxerror");
                        //                        $('#ddlPriJuri').removeClass('txtbox error');
                        //                        $('#txtTin1').removeClass('txtbox error');
                        $('#txtReasonTin').addClass('txtbox');
                        $(this).attr('data-bs-original-title', '');
                    }
                }

                //                else if ($(this).attr('name') == 'txtTin1') {
                //                    if ($.trim($(this).val()) == '') {
                //                        //alert("tintextboxtrue");
                //                        $(this).addClass('txtbox error');
                //                        $(this).attr('data-bs-original-title', 'TIN cannot be blank');
                //                        isValid = false;
                //                        //$(this).focus();
                //                    }
                //                    else {
                //                        //alert("tintextboxtrue");
                //                        $(this).removeClass('txtbox error');
                //                        $(this).addClass('txtbox');
                //                        $(this).attr('data-bs-original-title', '');
                //                    }
                //                }
            }

            else if (($("#ddlPriJuri").val()) != '-1' && ($("#txtTin1").val()) == '') {
                //alert($("#ddlPriJuri").val());
                //alert("tin error");
                if ($(this).attr('name') == 'txtTin1') {
                    if ($.trim($(this).val()) == '') {
                        //alert("tintextboxerror");
                        $('#txtTin1').addClass('txtbox error');
                        $('#ddlPriJuri').removeClass('txtbox error');
                        $('#txtReasonTin').removeClass('txtbox error');
                        $('#txtTin1').attr('data-bs-original-title', 'TIN cannot be blank');
                        isValid = false;
                        //$(this).focus();
                    }
                    else {
                        //alert("tintextboxtrue");
                        $('#ddlPriJuri').addClass('txtbox');
                        $('#ddlPriJuri').attr('data-bs-original-title', '');
                    }
                }
            }

            else if (($("#ddlPriJuri").val()) == '-1' && ($("#txtTin1").val()) != '') {
                //alert("primary error");
                if (($("#ddlPriJuri").val()) == '-1') {
                    //alert("Primtextboxerror");
                    $('#ddlPriJuri').addClass('txtbox error');
                    $('#txtTin1').removeClass('txtbox error');
                    $('#txtReasonTin').removeClass('txtbox error');
                    $('#ddlPriJuri').attr('data-bs-original-title', 'Primary Jurisdiction cannot be blank');
                    isValid = false;
                    //$(this).focus();
                }
                else {
                    //alert("Primtextboxtrue");
                    $('#txtTin1').addClass('txtbox');
                    $('#txtTin1').attr('data-bs-original-title', '');
                }

            }

            //            else if ($("#rblPayTax:checked").val() == '-1') {
            //                alert("hi");
            //                alert($("[rblPayTax]:checked").val());
            //            }


            else {
                $(this).removeClass('txtbox error');
                $(this).addClass('txtbox');
                $(this).attr('data-bs-original-title', '');
            }
        }




        else if ($(this).is("select")) {

            if ($(this).attr('name') == 'ddlTittle') {
                if ($(this)[0].selectedIndex == 0) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please Select the Title');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'CalDOB') {
                if ($(this)[0].selectedIndex == 0) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please Select DOB');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            //commented on 2023
            //            else if ($(this).attr('name') == 'ddlMaritalSts') {
            //                if ($(this)[0].selectedIndex == 0) {
            //                    isValid = false;
            //                    $(this).addClass('txtbox error');
            //                    $(this).attr('data-bs-original-title', 'Please Select Marital Status');
            //                    //$(this).focus();
            //                }
            //                else {
            //                    $(this).removeClass('txtbox error');
            //                    $(this).addClass('txtbox');
            //                    $(this).attr('data-bs-original-title', '');
            //                }
            //            }

            else if ($(this).attr('name') == 'ddlCtznShp') {
                if ($(this)[0].selectedIndex == 0) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please Select Citizenship');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'ddlPsPrtIsCntry') {
                if ($(this)[0].selectedIndex == 0) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please Select Country of Issue');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'ddlEmpType') {
                if ($(this)[0].selectedIndex == 0) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please Select the Employment detail');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'ddlSOF') {
                //alert("test");
                if ($(this)[0].selectedIndex == 0) {
                    //alert("test");
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please Select the SOF detail');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'ddlCurCity') {
                if ($(this)[0].selectedIndex == 0) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please Select Current Address City');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'ddlCurCountry') {
                if ($(this)[0].selectedIndex == 0) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please Select Current Address Country');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'ddlPreCity') {
                if ($(this)[0].selectedIndex == 0) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please Select Previous Address City');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'ddlPreCountry') {
                if ($(this)[0].selectedIndex == 0) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please Select Previous Address Country');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
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






    var isptaxchk = false;
    var paytaxcheckradio = document.getElementsByName("rblPayTax");
    for (var i = 0; i < paytaxcheckradio.length; i++) {
        if (paytaxcheckradio[i].checked) {
            //var paytaxvalue = paytaxcheckradio[i].value;
            $('#rblPayTax').removeClass('txtbox error');
            isptaxchk = true;
            break;
        }
    }
    if (!isptaxchk) {
        //alert("uspersonfalse");
        $('#rblPayTax').addClass('txtbox error');
        isValid = false;
    }

    //20171109
    var isUSchk = false;
    var UScheckradio = document.getElementsByName("rblUSCitizen");
    for (var i = 0; i < UScheckradio.length; i++) {
        if (UScheckradio[i].checked) {
            //var paytaxvalue = paytaxcheckradio[i].value;
            $('#rblUSCitizen').removeClass('txtbox error');
            isUSchk = true;
            break;
        }
    }
    if (!isUSchk) {
        //alert("uspersonfalse");
        $('#rblUSCitizen').addClass('txtbox error');
        isValid = false;
    }


    var ispGreeCardchk = false;
    var Greencheckradio = document.getElementsByName("rblGreenCard");
    for (var i = 0; i < Greencheckradio.length; i++) {
        if (Greencheckradio[i].checked) {
            //var paytaxvalue = paytaxcheckradio[i].value;
            $('#rblGreenCard').removeClass('txtbox error');
            ispGreeCardchk = true;
            break;
        }
    }
    if (!ispGreeCardchk) {
        //alert("uspersonfalse");
        $('#rblGreenCard').addClass('txtbox error');
        isValid = false;
    }


    var ispRealEschk = false;
    var RealEscheckradio = document.getElementsByName("rblRealEst");
    for (var i = 0; i < RealEscheckradio.length; i++) {
        if (RealEscheckradio[i].checked) {
            //var paytaxvalue = paytaxcheckradio[i].value;
            $('#rblRealEst').removeClass('txtbox error');
            ispRealEschk = true;
            break;
        }
    }
    if (!ispRealEschk) {
        //alert("uspersonfalse");
        $('#rblRealEst').addClass('txtbox error');
        isValid = false;
    }

    var ispAssetchk = false;
    var Assetcheckradio = document.getElementsByName("rblassets");
    for (var i = 0; i < Assetcheckradio.length; i++) {
        if (Assetcheckradio[i].checked) {
            //var paytaxvalue = paytaxcheckradio[i].value;
            $('#rblassets').removeClass('txtbox error');
            ispAssetchk = true;
            break;
        }
    }
    if (!ispAssetchk) {
        //alert("uspersonfalse");
        $('#rblassets').addClass('txtbox error');
        isValid = false;
    }
    //20171109

    //    if (paytaxvalue == "No") {
    //        
    //        var isUSCiticheck = false;
    //        var USCitiradio = document.getElementsByName("rblUSCitizen");
    //        for (var i = 0; i < USCitiradio.length; i++) {

    //            if (USCitiradio[i].checked) {
    //                //alert("paytax true");
    //                $('#rblUSCitizen').removeClass('txtbox error');
    //                isUSCiticheck = true;
    //                break;
    //            }
    //        }
    //        if (!isUSCiticheck) {
    //            //alert("paytax error");
    //            $('#rblUSCitizen').addClass('txtbox error');
    //            isValid = false;
    //        }


    //        var isGreenCardcheck = false;
    //        var GreenCardradio = document.getElementsByName("rblGreenCard");
    //        for (var i = 0; i < GreenCardradio.length; i++) {

    //            if (GreenCardradio[i].checked) {
    //                //alert("paytax true");
    //                $('#rblGreenCard').removeClass('txtbox error');
    //                isGreenCardcheck = true;
    //                break;
    //            }
    //        }
    //        if (!isGreenCardcheck) {
    //            //alert("paytax error");
    //            $('#rblGreenCard').addClass('txtbox error');
    //            isValid = false;
    //        }


    //        var isRealEstcheck = false;
    //        var RealEstradio = document.getElementsByName("rblRealEst");
    //        for (var i = 0; i < RealEstradio.length; i++) {

    //            if (RealEstradio[i].checked) {
    //                //alert("paytax true");
    //                $('#rblRealEst').removeClass('txtbox error');
    //                isRealEstcheck = true;
    //                break;
    //            }
    //        }
    //        if (!isRealEstcheck) {
    //            //alert("paytax error");
    //            $('#rblRealEst').addClass('txtbox error');
    //            isValid = false;
    //        }

    //        var isassetscheck = false;
    //        var assetsradio = document.getElementsByName("rblassets");
    //        for (var i = 0; i < assetsradio.length; i++) {

    //            if (assetsradio[i].checked) {
    //                //alert("paytax true");
    //                $('#rblassets').removeClass('txtbox error');
    //                isassetscheck = true;
    //                break;
    //            }
    //        }
    //        if (!isassetscheck) {
    //            //alert("paytax error");
    //            $('#rblassets').addClass('txtbox error');
    //            isValid = false;
    //        }
    //    }




    /* UPBv2 Aug-2021 */
    //    if (isValid == false)  {
    //        document.getElementById("errormsg").innerText = "Please fill mandatory fields.";
    //        $('#errormsg').addClass('lblbox error');
    //    }

    if ((isValid == false) && (isRSDateValid == true)) {
        document.getElementById("errormsg").innerText = "Please enter a valid input for the highlighted fields";
        $('#errormsg').addClass('lblbox error');
    }
    else if ((isValid == false) && (isRSDateValid == false)) {
        document.getElementById("errormsg").innerText = "Please enter a valid input for the highlighted fields";
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

        const formatter = new Intl.NumberFormat('en-GB');

        if ($(this).is("input")) {
            //alert("bondinput");

            if ($(this).attr('name') == 'txtAmt') {
                //alert("amt");

                if ($.trim($(this).val()) == '') {
                    //alert("case1");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please enter Amount');
                    //$(this).focus();
                    isValidBond = false;
                }
                else if (!customValidator($.trim($(this).val()), enumVal.amnt)) {
                    //alert("case2");
                    isValidBond = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Invalid Amount');
                    //$(this).focus();
                }
                //!lengthValidate($.trim($(this).val()), 1, 2)
                else if (!(($.trim($(this).val())) >= sDepMinAmt && ($.trim($(this).val())) <= sDepMaxAmt)) {
                    //alert("case3");

                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please enter an amount between £' + formatter.format(sDepMinAmt) + ' and £' + formatter.format(sDepMaxAmt));
                    isValidBond = false;
                    //$(this).focus();
                }
                else {
                    //alert("case4");
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtOthBankName') {
                //alert("bankname");
                if ($.trim($(this).val()) == '') {
                    //alert("bondinputerror");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please enter Name of the other bank');
                    //$(this).focus();
                    isValidBond = false;
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtOthBankSC1') {
                //alert("sc1");
                if ($.trim($(this).val()) == '') {
                    //alert("bondinputerror");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please enter Sort Code1');
                    //$(this).focus();
                    isValidBond = false;
                }
                else if (!customValidator($.trim($(this).val()), enumVal.numer)) {
                    isValidBond = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Sort Code1 accept only Numeric Digit');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 2, 2)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Sort Code1 should be two Numeric Digit');
                    isValidBond = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtOthBankSC2') {
                //alert("sc2");
                if ($.trim($(this).val()) == '') {
                    //alert("bondinputerror");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please enter Sort Code2');
                    //$(this).focus();
                    isValidBond = false;
                }
                else if (!customValidator($.trim($(this).val()), enumVal.numer)) {
                    isValidBond = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Sort Code2 accept only Numeric Digit');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 2, 2)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Sort Code2 should be two Numeric Digit');
                    isValidBond = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtOthBankSC3') {
                //alert("sc3");
                if ($.trim($(this).val()) == '') {
                    //alert("bondinputerror");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please enter Sort Code3');
                    //$(this).focus();
                    isValidBond = false;
                }
                else if (!customValidator($.trim($(this).val()), enumVal.numer)) {
                    isValidBond = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Sort Code3 accept only Numeric Digit');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 2, 2)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Sort Code3 should be two Numeric Digit');
                    isValidBond = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }


            else if ($(this).attr('name') == 'txtOthBankAcNo') {
                //alert("acno");
                if ($.trim($(this).val()) == '') {
                    //alert("bondinputerror");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please enter other bank Account No');
                    //$(this).focus();
                    isValidBond = false;
                }
                else if (!customValidator($.trim($(this).val()), enumVal.numer)) {
                    isValidBond = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Other bank Account Number should be Numeric(0-9)');
                    //$(this).focus();
                }
                else if (!lengthValidate($.trim($(this).val()), 8, 8)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Other bank Account Number should be 8 characters');
                    isValidBond = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else {
                $(this).removeClass('txtbox error');
                $(this).addClass('txtbox');
                $(this).attr('data-bs-original-title', '');
            }
        }




        else if ($(this).is("select")) {
            //alert("period");
            if ($(this).attr('name') == 'ddlPeriod') {
                if ($(this)[0].selectedIndex == 0) {
                    isValidBond = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please Select Period');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
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
            $('#rblRepayInst').attr('data-bs-original-title', 'Please select Repayment Instruction');
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

    //    var chkbox1 = false;
    //    var isChecked = $("#chkTermsAndConditions").is(":checked");
    //    if (isChecked) {
    //        chkbox1 = true;
    //    } else {
    //        alert("Please tick the terms and conditions for further enrollment process!");
    //        //$('#chkTermsAndConditions').addClass('chkbox error');
    //        isValidBond = false;
    //    }

    //    var chkbox2 = false;
    //    var isCheckedJoint = $("#chkIfJoint").is(":checked");
    //    if (isCheckedJoint) {
    //        chkbox2 = true;
    //    } else {
    //        alert("Please tick Primary applicant accessibility control");
    //        //$('#chkTermsAndConditions').addClass('chkbox error');
    //        isValidBond = false;
    //    }
    //alert(isValidBond);
    if (isValidBond == false) {

        document.getElementById("errormsg").innerText = "Please enter a valid input for the highlighted fields.";
        $('#errormsg').addClass('lblbox error');
    }
    return isValidBond;
}


function submitRetExisApp() {
    var isValidRetExistApp = true;

    //alert("Pass");

    $('#txtReferNo,#txtCaptcha').each(function () {

        $('input[type="text"]').tooltip();
        $('input[type="password"]').tooltip();
        $('select ').tooltip();

        if ($(this).is("input")) {

            if ($(this).attr('name') == 'txtReferNo') {

                if ($.trim($(this).val()) == '') {
                    //alert("case1");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Reference Number cannot be blank.');
                    isValidRetExistApp = false;
                }

                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            } 

            else if ($(this).attr('name') == 'txtCaptcha') {

                if ($.trim($(this).val()) == '') {
                    //alert("case3");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Enter the characters as they are shown in the image below.');
                    isValidRetExistApp = false;
                }

                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else {
                $(this).removeClass('txtbox error');
                $(this).addClass('txtbox');
                $(this).attr('data-bs-original-title', '');
            }
        }
    });


    //alert(isValidRetExistApp);
    if (isValidRetExistApp == false) {
        $('#lblmsg').hide();
        document.getElementById("errormsg").innerText = "Please enter a valid input for the highlighted fields.";
        $('#errormsg').removeClass('d-none').addClass('d-block');
    }
    return isValidRetExistApp;
}





//20170810
function SubRetPopUp() {

    //alert("submit");
    var isValidRetPopUp = true;

    $('#txtEmailId,#txtPassword').each(function () {

        $('input[type="text"]').tooltip();
        $('input[type="password"]').tooltip();
        $('select ').tooltip();

        if ($(this).is("input")) {

            if ($(this).attr('name') == 'txtEmailId') {

                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Email Address cannot be blank');
                    isValidRetPopUp = false;
                }
                else if (!customValidator($.trim($(this).val()), enumVal.eMail)) {
                    isValidRetPopUp = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Invalid Email Id!');
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else if ($(this).attr('name') == 'txtPassword') {
                //alert("password");

                if ($.trim($(this).val()) == '') {
                    //alert("password empty");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Password cannot be blank');
                    isValidRetPopUp = false;
                }
                else if ($.trim($(this).val()).length < 8) {
                    //alert("length error");
                    isValidRetPopUp = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'The Password length should be 8 Characters.');
                }
                else if (!customValidator($.trim($(this).val()), enumVal.pwd)) {
                    //alert("password error");
                    isValidRetPopUp = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'The Password must contain alpha and numeric characters.');
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }


            else {
                $(this).removeClass('txtbox error');
                $(this).addClass('txtbox');
                $(this).attr('data-bs-original-title', '');
            }
        }



    });


    //alert(isValidRetPopUp);
    //    if (isValidRetPopUp == false) {

    //        document.getElementById("errormsg").innerText = "Please fill mantatory fields";
    //        $('#errormsg').addClass('lblbox error');
    //    }
    return isValidRetPopUp;

}


//function SaveAndRetnLater() {
//    var isValid = true;

//    $('#txtFirstNm,#txtSurNm,#txtEmailAddr').each(function () {

//        $('input[type="text"]').tooltip();
//        $('input[type="password"]').tooltip();
//        $('select ').tooltip();

//        if ($(this).is("input")) {

//            if ($(this).attr('name') == 'txtFirstNm') {
//                if ($.trim($(this).val()) == '') {
//                    $(this).addClass('txtbox error');
//                    $(this).attr('data-bs-original-title', 'First name cannot be blank!');
//                    isValid = false;
//                }

//                else {
//                    $(this).removeClass('txtbox error');
//                    $(this).addClass('txtbox');
//                    $(this).attr('data-bs-original-title', '');
//                }
//            }

//            else if ($(this).attr('name') == 'txtSurNm') {
//                if ($.trim($(this).val()) == '') {
//                    $(this).addClass('txtbox error');
//                    $(this).attr('data-bs-original-title', 'Surname cannot be blank');
//                    isValid = false;
//                }
//                else if ($.trim($(this).val()).length < 2) {
//                    $(this).addClass('txtbox error');
//                    $(this).attr('data-bs-original-title', 'Surname should have minimum 2 characters');
//                    isValid = false;
//                }
//                else {
//                    $(this).removeClass('txtbox error');
//                    $(this).addClass('txtbox');
//                    $(this).attr('data-bs-original-title', '');
//                }
//            }

//            else if ($(this).attr('name') == 'txtEmailAddr') {
//                if ($.trim($(this).val()) == '') {
//                    $(this).addClass('txtbox error');
//                    $(this).attr('data-bs-original-title', 'Email Address cannot be blank');
//                    isValid = false;
//                }
//                else if (!customValidator($.trim($(this).val()), enumVal.eMail)) {
//                    isValid = false;
//                    $(this).addClass('txtbox error');
//                    $(this).attr('data-bs-original-title', 'Invalid Email Id!');
//                }
//                else {
//                    $(this).removeClass('txtbox error');
//                    $(this).addClass('txtbox');
//                    $(this).attr('data-bs-original-title', '');
//                }
//            }

//            else {
//                $(this).removeClass('txtbox error');
//                $(this).addClass('txtbox');
//                $(this).attr('data-bs-original-title', '');
//            }
//        }



//    });

//    if (isValid == false) {
//        document.getElementById("errormsg").innerText = "Please enter a valid input for the highlighted fields.";
//        $('#errormsg').addClass('lblbox error');
//    }
//    return isValid;
//}

//New enhancements 2023
function updateBond() {
    var isValidBond = true;

    $('#txtAmt,#ddlPeriod').each(function () {

        $('input[type="text"]').tooltip();
        $('select ').tooltip();

        if ($(this).is("input")) {

            if ($(this).attr('name') == 'txtAmt') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please enter Amount');
                    //$(this).focus();
                    isValidBond = false;
                }
                else if (!customValidator($.trim($(this).val()), enumVal.amnt)) {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please enter a valid deposit amount');
                    isValidBond = false;
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }
            else {
                $(this).removeClass('txtbox error');
                $(this).addClass('txtbox');
                $(this).attr('data-bs-original-title', '');
            }
        }
        else if ($(this).is("select")) {
            if ($(this).attr('name') == 'ddlPeriod') {
                if ($(this)[0].selectedIndex == 0) {
                    isValidBond = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please Select Period');
                    //$(this).focus();
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }
            else {
                $(this).removeClass('txtbox error');
                $(this).addClass('txtbox');
            }
        }
    });

    return isValidBond;
}

//2024 Verify Email
function SaveAndRetnLater() {
    var isValid = true;

    $('#txtEmailAddr').each(function () {

        $('input[type="text"]').tooltip();
        $('input[type="password"]').tooltip();
        $('select ').tooltip();

        if ($(this).is("input")) {
            if ($(this).attr('name') == 'txtEmailAddr') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please verify your email address before saving the application for future retrieval.');
                    isValid = false;
                }
                else if (!customValidator($.trim($(this).val()), enumVal.eMail)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please verify your email address before saving the application for future retrieval.');
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else {
                $(this).removeClass('txtbox error');
                $(this).addClass('txtbox');
                $(this).attr('data-bs-original-title', '');
            }
        }
    });

    if (isValid == false) {
        document.getElementById("errormsg").innerText = "Please enter a valid input for the highlighted fields.";
        $('#errormsg').addClass('lblbox error');
    }
    return isValid;
}

function VerifyEmailAdd() {
    var isValid = true;

    $('#txtEmailAddr').each(function () {

        $('input[type="text"]').tooltip();
        $('input[type="password"]').tooltip();
        $('select ').tooltip();

        if ($(this).is("input")) {

            if ($(this).attr('name') == 'txtEmailAddr') {
                if ($.trim($(this).val()) == '') {
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please verify your email address before saving the application for future retrieval');
                    isValid = false;
                }
                else if (!customValidator($.trim($(this).val()), enumVal.eMail)) {
                    isValid = false;
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Invalid Email Id!');
                }
                else {
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }

            else {
                $(this).removeClass('txtbox error');
                $(this).addClass('txtbox');
                $(this).attr('data-bs-original-title', '');
            }
        }

    });

    if (isValid == false) {
        document.getElementById("errormsg").innerText = "Please enter a valid input for the highlighted fields.";
        $('#errormsg').addClass('lblbox error');
    }
    return isValid;
}

function OTPVerify() {
    var isValid = true;
    //console.log("1");
    $('#txtOTP').each(function () {
        //console.log("2");
        $('input[type="text"]').tooltip();
        $('input[type="password"]').tooltip();
        $('select ').tooltip();

        if ($(this).is("input")) {
            //console.log("3");
            if ($(this).attr('name') == 'txtOTP') {
                //console.log("4");
                if ($.trim($(this).val()) == '') {
                    //console.log("5");
                    $(this).addClass('txtbox error');
                    $(this).attr('data-bs-original-title', 'Please enter a valid OTP');
                    isValid = false;
                }

                else {
                    //console.log("6");
                    $(this).removeClass('txtbox error');
                    $(this).addClass('txtbox');
                    $(this).attr('data-bs-original-title', '');
                }
            }
            else {
                //console.log("7");
                $(this).removeClass('txtbox error');
                $(this).addClass('txtbox');
                $(this).attr('data-bs-original-title', '');
            }
            //console.log("8");
        }
        //console.log("9");
    });

    if (isValid == false) {
        //console.log("10");
        document.getElementById("errormsg").innerText = "Please enter a valid input for the highlighted fields.";
        $('#errormsg').addClass('lblbox error');
        //console.log("11");
    }
    return isValid;
}
//End