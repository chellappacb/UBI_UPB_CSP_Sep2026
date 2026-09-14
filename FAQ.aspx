<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FAQ.aspx.cs" Inherits="FAQ" %>

<%@ Register Src="footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="FSCS.ascx" TagName="FSCS" TagPrefix="uc3" %>
<%@ Register Src="~/HeaderInclude.ascx" TagName="headerincl" TagPrefix="id" %>
<%--<%@ Register Assembly="MacroWebControls" Namespace="MacroWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <id:headerincl ID="headerinclude" runat="server" />
    <!--  <id:headerincl ID="headerincl1" runat="server" />-->
    <link type="text/css" href="Website/Style.css" rel="Stylesheet" />
    <link href="website/css/bootstrap.min.css" rel="stylesheet" />

    <link type="text/css" href="Website/Responsive.css" rel="Stylesheet" />
    <link type="text/css" href="Website/custom-style.css" rel="Stylesheet" />

    <!-- Scroll Top -->
    <link href="website/scrolltop/scrolltopt.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="website/scrolltop/font-awesome.min.css" />
    <!-- Scroll Top -->
    <link href="faq/accordion.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="container">
        <div class="bg-white p-2">
            <div class="row">
                <div class="col-12 col-lg-7 align-self-center">
                    <img src="website/Images/logo.png" alt="Logo" class="logo" />
                </div>
                <div class="col-12 col-lg-5">
                    <div class="row">
                        <div class="col-12  pb-2">
                            <img src="images/sortcode.png" alt="sortcode" />&nbsp;Sort Code: 23-56-26
                        </div>
                        <div class="col-12  pb-2">
                            <img src="images/swift.png" alt="swift code" />
                            &nbsp;SWIFT Code: UBINGB2L
                        </div>

                        <div class="col-12  pb-2">
                            <img src="images/phone.png" alt="phone" />
                            &nbsp;&nbsp;+44 20 7332 4250 (Ext-1)
                        </div>

                        <div class="col-12  pb-2">
                            <img src="images/mail.png" alt="mail" />
                            &nbsp;premierbond@unionbankofindiauk.co.uk
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div class="bg-white">
            <div class="bg-light">
                <h2 class="faq-title p-2 pl-3">Frequent Questions & Answers</h2>
            </div>

            <div class="d-block pb-2">
                <div class="accordion-panel pt-2">
                    <div class="buttons-wrapper">
                        <i class="plus-icon"></i>
                        <div class="open-btn">
                            Expand all
                        </div>
                        <div class="close-btn hidden">
                            Collapse all
                        </div>
                    </div>

                    <dl class="accordion">
                        <dt>Who can open Union Premier Bond Account? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    Any Individuals of UK resident, who is 18 years of age or above and who has a Bank Account in his/her
                                    name in any Bank or Building Society in United Kingdom other than Union Bank of
                                    India (UK) Ltd.
                                </p>
                                <p>
                                    Business deposits are not applicable under Union Premier Bond.
                                </p>
                                <p>
                                    To open business deposits offline please click the link for downloading the forms:<a
                                        href="https://www.unionbankofindiauk.co.uk/forms" target="_blank">https://www.unionbankofindiauk.co.uk/forms</a>.
                                </p>
                            </div>
                        </dd>
                        <dt>What is the minimum amount I can invest in Union Premier Bond? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    There is a minimum requirement of £<span id="spMinAmt" runat="server"></span> to open a Union Premier Bond.
                                </p>
                            </div>
                        </dd>
                        <dt>What is the maximum amount that can be invested in Union Premier Bond? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    You can deposit a maximum amount of £<span id="spMaxAmt" runat="server"></span> per Union Premier Bond application.
                                </p>
                            </div>
                        </dd>
                        <dt>Can we open Joint Account?<i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    Yes, you can either apply individually or jointly.
                                </p>
                            </div>
                        </dd>
                        <dt>How many applicants can open an account jointly?<i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    There would be maximum of four persons in a joint account.
                                </p>
                            </div>
                        </dd>
                        <dt>In which currency I can open Union Premier Bond?<i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    You can open Union Premier Bond only in GBP <u>i.e. Sterling</u>
                                </p>
                            </div>
                        </dd>
                        <dt>Will my deposit be protected?<i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    Your eligible deposits with Union Bank Of India (UK) Ltd are protected up to the
                                    FSCS compensation limit by the Financial Services Compensation Scheme, the UK's
                                    deposit protection scheme. Most deposits are covered by the scheme. This limit is
                                    applied to the total of any deposits you have with Union Bank of India (UK) Ltd.
                                    Any deposits you hold above the FSCS compensation limit are unlikely to be covered,
                                    unless under specific circumstances, as determined by the FSCS.
                                </p>
                            </div>
                        </dd>
                        <dt>How do you verify my or Joint Account Holders’ details for KYC purpose?<i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    We are required to have adequate proof of your identity and address before opening
                                    your Union Premier Bond. We will verify your identity and address electronically
                                    with Credit Reference Agencies.
                                </p>
                                <p>
                                    Credit Reference Agencies will ask for your details such as your name, address,
                                    date of birth, Driving License or Passport details. These details will be submitted
                                    to the Credit Reference Agency to verify the information you have provided to us.
                                    Please do not send in your documents unless we request you to do so. However if
                                    the online verification fails, we may ask for documents (including your id and proof
                                    of address) in order to enable us to complete our due diligence procedures and open
                                    your bond account.
                                </p>
                            </div>
                        </dd>
                        <dt>What happen if my account does not pass due diligence stage? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    If your application is unsuccessful, you will be advised next step through Email
                                    or you may call us on 0207-332-4250 (Monday to Friday excl. Bank Holidays; 9.30am
                                    - 4.00pm) or email us on <a class="linkStyle" href="mailto:premierbond@unionbankofindiauk.co.uk">premierbond@unionbankofindiauk.co.uk </a>
                                </p>
                            </div>
                        </dd>
                        <dt>What if I do not have a UK driving licence or an up to date Passport? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    If you do not have a UK driving licence OR an up to date Passport, you will be unable
                                    to complete our online Union Premier Bond application form.
                                </p>
                                <p>
                                    In case of non-availability of any of those proofs, please contact our branch on
                                    0207-332-4250 or email at <a class="linkStyle" href="mailto:premierbond@unionbankofindiauk.co.uk">info@unionbankofindiauk.co.uk</a>
                                </p>
                            </div>
                        </dd>
                        <dt>What will happen if I cleared my application successfully and remit the funds but
                            your interest rate for deposit has changed meanwhile? <i class="plus-icon"></i>
                        </dt>
                        <dd>
                            <div class="content">
                                <p>
                                    The rate of interest would be the rate which will be prevailing at the date of receiving
                                    of cleared funds in our account.
                                </p>
                                <p>
                                    Note: - any fund received till 1 PM during working days would be processed on the
                                    same day, any fund received after cut off time would be processed on next working
                                    day.
                                </p>
                            </div>
                        </dd>
                        <dt>Upto what period do I have to fund my Union Premier Bond? <i class="plus-icon"></i>
                        </dt>
                        <dd>
                            <div class="content">
                                <p>
                                    Once your application is approved, You payment must reach us within 30 days from
                                    the date of approval. Otherwise your account will get disabled and you have to apply
                                    once again
                                </p>
                            </div>
                        </dd>
                        <dt>If my Union Premier Bond application is successful, when will I receive my Account
                            details?<i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    Once your Union Premier Bond application is successful and Bank is in receipt of
                                    cleared funds, Union Premier Bond Account number and details will be generated and
                                    e-mailed to you.
                                </p>
                                <p>
                                    Please Note : We will not be sending any certificate/confirmation through Post/Fax.
                                </p>
                            </div>
                        </dd>
                        <dt>If I remitted funds within 30 days from date of approval of my Union Premier Bond
                            Application, then what would be the effective date of my account? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    Date of Bond would be the date of funds received subjected to- any funds received
                                    till 1 PM during working days would be processed on the same day, any funds received
                                    after cut off time would be processed next working day.
                                </p>
                            </div>
                        </dd>
                        <dt>What happens after I have completed my Union Premier Bond Application?<i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    Once you've submitted Union Premier Bond application, you will be advised whether
                                    your application has been successful or not and you will receive a confirmation
                                    / notification email with unique reference number. (If in case of non-receipt of
                                    email, Please check your Spam/Junk folder as well). This will give you details on
                                    how to fund your Union Premier Bond Account and / or instructions on how to proceed.
                                </p>
                            </div>
                        </dd>
                        <dt>How can I transfer fund to create a Union Premier Bond?<i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content p-3">
                                <p>
                                    To Transfer funds you can use – FPS (Faster Payment Service)/CHAPS.
                                    <br />
                                    Any funds receipt before 1.00 PM (Monday to Friday excluding Holidays) will be processed
                                    same day. Funds receipt after 1.00 PM will not be processed until the next working
                                    day.
                                    <br />
                                    Please transfer your funds to the following account:
                                    <br />

                                    <table class="table  table-bordered">
                                        <tbody>
                                            <tr>
                                                <th scope="row">Union Bond Account</th>
                                                <td>28576002</td>
                                            </tr>
                                            <tr>
                                                <th scope="row">Sort code</th>
                                                <td>23-56-26</td>
                                            </tr>
                                            <tr>
                                                <th scope="row">Reference No</th>
                                                <td>Details of reference number that you would receive after the successful submission
                                                of your Application.</td>
                                            </tr>
                                            <tr>
                                                <th scope="row">Amount</th>
                                                <td>£ *******</td>
                                            </tr>
                                            <tr>
                                                <th scope="row">Name</th>
                                                <td>Union Premier Bond</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <!--  <table class="faq_tbl">
                                        <tr>
                                            <td class="tdwidth" style="font-weight: bold; border: 1px solid #000;">
                                                Union Bond Account
                                            </td>
                                            <td class="tdwidth" style="border: 1px solid #000;">
                                                28576002
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdwidth" style="font-weight: bold; border: 1px solid #000;">
                                                Sort code
                                            </td>
                                            <td class="tdwidth" style="border: 1px solid #000;">
                                                23-56-26
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdwidth" style="font-weight: bold; border: 1px solid #000;">
                                                Reference No
                                            </td>
                                            <td class="tdwidth" style="border: 1px solid #000;">
                                                Details of reference number that you would receive after the successful submission
                                                of your Application.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdwidth" style="font-weight: bold; border: 1px solid #000;">
                                                Amount
                                            </td>
                                            <td class="tdwidth" style="border: 1px solid #000;">
                                                £ *******
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdwidth" style="font-weight: bold; border: 1px solid #000;">
                                                Name
                                            </td>
                                            <td class="tdwidth" style="border: 1px solid #000;">
                                                Union Premier Bond
                                            </td>
                                        </tr>
                                    </table>-->
                                </p>
                            </div>
                        </dd>
                        <dt>Is any tax is deductible on interest earned on my deposit? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    No, Bank will not deduct any tax as per prevailing guidelines. Its customer’s or
                                    Account holder’s responsibility to pay tax to HMRC. For more information, please
                                    visit <a class="linkStyle" href="http://www.gov.uk/hmrc/savingsallowance" target="_blank">www.gov.uk/hmrc/savingsallowance</a>.
                                </p>
                            </div>
                        </dd>
                        <dt>Can I add new funds to my existing Union Premier Bond? <i class="plus-icon"></i>
                        </dt>
                        <dd>
                            <div class="content">
                                <p>
                                    No, new funds cannot be added to your existing Union Premier Bond. However you can
                                    open a new Union Premier Bond with your new funds.
                                </p>
                            </div>
                        </dd>
                        <dt>Can I redeem my Union Premier Bond before the original term? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    No, as per our Bank’s prevailing policy, we are not allowing early withdrawals.
                                </p>
                            </div>
                        </dd>
                        <dt>Are there any charges to open Union Premier Bond? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    There are no Bank charges associated with the Union Premier Bond.
                                </p>
                            </div>
                        </dd>
                        <dt>How many Union Premier Bond Accounts can I open? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    You can open any number of Union Premier Bond accounts.
                                </p>
                            </div>
                        </dd>
                        <dt>What are the periods for which I can open my Union Premier Bond? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    Union Premier Bond with interest payable on maturity is available for the following tenures: 
                                    <label id="lblTenureFAQ" runat="server"></label>
                                </p>
                            </div>
                        </dd>
                        <dt>Do all joint individuals need to give consent to open and close the Union Premier
                            Bond? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    No, it is Primary Applicant responsibility to take consent of all others joint applicants
                                    before opening or closing Union Premier Bond.
                                </p>
                            </div>
                        </dd>
                        <dt>Is it mandatory for me to open a Personal Current Account if I wish to open a Union
                            Premier Bond? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    No, it is not mandatory to open a separate current account with Union Bank of India
                                    (UK) Ltd. for Union Premier Bond.
                                </p>
                            </div>
                        </dd>
                        <dt>Can I send funds back into someone else's name? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    No, on maturity you can only transfer funds to your Nominated account i.e. to the
                                    Bank account whose details were provided at the time of applying for the Union Premier
                                    Bond
                                </p>
                            </div>
                        </dd>
                        <dt>When is interest paid? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    Interest will be paid only on maturity. Yearly interests will be getting accrued
                                    till maturity and no interest will be paid on these interest amounts as its simple
                                    interest paying account.
                                </p>
                            </div>
                        </dd>
                        <dt>What happens to my Union Premier Bond on maturity? <i class="plus-icon"></i>
                        </dt>
                        <dd>
                            <div class="content">
                                <p>
                                    We provide you with the following options at the time of opening of accounts:
                                </p>
                                <p>
                                    i) Automatic renewal for the Bond on maturity for the original period at the applicable
                                    interest rate (This feature ensures that you don't lose out even a single day's
                                    interest).
                                </p>
                                <p>
                                    ii) Automatic renewal of the deposit on maturity for a different period and at the
                                    applicable rate of interest on the date of maturity (However instructions to be
                                    given at the time of account opening or at least 7 working days before the maturity
                                    date) Payment of maturity proceeds would be transferred to same account from where
                                    they were originally received.
                                </p>
                                <p>
                                    Any change in the account details need to be informed to us separately.
                                </p>
                            </div>
                        </dd>
                        <dt>How do I request for renewal/payment/partial payment to the bank for my Union Premier
                            Bond? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    Bank will share a reminder link on your registered E-mail Id, 30 days before maturity.
                                    Open the link and enter your Union Premier Bond credentials to submit new or amend
                                    your existing request. Otherwise your bond will be getting renewed for the initial
                                    original term and the rate of interest would be prevailing interest rate on the
                                    date of maturity of Bond.
                                </p>
                            </div>
                        </dd>
                        <dt>How do I change my address, personal details? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    You can send email from your register email with us on <a class="linkStyle" href="mailto:premierbond@unionbankofindiauk.co.uk">premierbond@unionbankofindiauk.co.uk</a> to change the personal details including bank information and address. We also request you to please send supporting documents like recent Banks statements. Alternatively, you can also send written request addressing to Union Premier Bond section , Union Bank of India (UK) Ltd , 12 Arthur Street London EC4R 9AB. 
                                </p>
                            </div>
                        </dd>
                        <dt>Where do I login once I receive my Union Premier Bond banking credentials? <i
                            class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    You may directly visit (<a class="linkStyle" href="http://www.unionpremierbond.co.uk"
                                        target="_blank">www.unionpremierbond.co.uk</a>) or you can access this site
                                    from <a class="linkStyle" href="http://www.unionbankofindiauk.co.uk" target="_blank">www.unionbankofindiauk.co.uk</a>
                                </p>
                            </div>
                        </dd>
                        <dt>What if my Union Premier Bond is maturing on Holiday? <i class="plus-icon"></i>
                        </dt>
                        <dd>
                            <div class="content">
                                <p>
                                    If Bond is maturing on a holiday, the interest will be paid for the holiday period
                                    as well and will be processed on next working day. If the deposit is renewed for
                                    a further period, then the effective start date will be the original maturity date.
                                </p>
                            </div>
                        </dd>
                        <dt>What will happen if I forget my Union Premier Bond credentials? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    You can reset your password by clicking on Forget Password link (<a class="linkStyle"
                                        href="http://www.unionpremierbond.unionbankofindiauk.co.uk/ECODT/ForgotPassword.aspx"
                                        target="_blank">www.unionpremierbond.unionbankofindiauk.co.uk/ECODT/ForgotPassword.aspx</a>)
                                </p>
                                <p>
                                    Or
                                </p>
                                <p>
                                    You can drop a mail at <a class="linkStyle" href="mailto:premierbond@unionbankofindiauk.co.uk">premierbond@unionbankofindiauk.co.uk</a> from your registered email Id, we will
                                    send password to your Email Id.
                                </p>
                            </div>
                        </dd>
                        <dt>What will happen if I have Union Premier Bond Login credentials but I am not able
                            to login in my account? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    This may be due to one of the following reasons:
                                </p>
                                <ul class="ml-2 mb-2">
                                    <li>You have wrongly attempted to input your password(s) consecutively three times</li>
                                    <li>You internet browser is not Java enabled</li>
                                </ul>
                            </div>
                        </dd>
                        <dt>I receive a call from someone claiming to be from bank asking for my Union Premier
                            bond banking credentials? What should I do? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    Our Bank will never ask or send any email asking for Union Premier bond login details.
                                </p>
                            </div>
                        </dd>
                        <dt>How you can communicate or contact us for any clarifications? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    You can contact us on 020 7332 4250 or send us an e-mail at <a class="linkStyle"
                                        href="mailto:premierbond@unionbankofindiauk.co.uk">premierbond@unionbankofindiauk.co.uk</a>
                                </p>
                            </div>
                        </dd>
                        <dt>How do I obtain my Interest Certificate? <i class="plus-icon"></i></dt>
                        <dd>
                            <div class="content">
                                <p>
                                    We will issue an Interest Certificate after maturity and send it your registered
                                    Email ID.
                                </p>
                            </div>
                        </dd>
                    </dl>
                </div>
            </div>
            <uc3:FSCS ID="FSCS" runat="server" />
            <uc2:footer ID="footer1" runat="server" />
        </div>
    </div>
    <script src="javascript/jquery-3.7.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var bodyEl = $('body'),
                accordionDT = $('.accordion').find('dt'),
                accordionDD = accordionDT.next('dd'),
                parentHeight = accordionDD.height(),
                childHeight = accordionDD.children('.content').outerHeight(true),
                newHeight = parentHeight > 0 ? 0 : childHeight,
                accordionPanel = $('.accordion-panel'),
                buttonsWrapper = accordionPanel.find('.buttons-wrapper'),
                openBtn = accordionPanel.find('.open-btn'),
                closeBtn = accordionPanel.find('.close-btn');

            bodyEl.on('click', function (argument) {
                var totalItems = $('.accordion').children('dt').length;
                var totalItemsOpen = $('.accordion').children('dt.is-open').length;

                if (totalItems == totalItemsOpen) {
                    openBtn.addClass('hidden');
                    closeBtn.removeClass('hidden');
                    buttonsWrapper.addClass('is-open');
                } else {
                    openBtn.removeClass('hidden');
                    closeBtn.addClass('hidden');
                    buttonsWrapper.removeClass('is-open');
                }
            });

            function openAll() {

                openBtn.on('click', function (argument) {

                    accordionDD.each(function (argument) {
                        var eachNewHeight = $(this).children('.content').outerHeight(true);
                        $(this).css({
                            height: eachNewHeight
                        });
                    });
                    accordionDT.addClass('is-open');
                });
            }

            function closeAll() {

                closeBtn.on('click', function (argument) {
                    accordionDD.css({
                        height: 0
                    });
                    accordionDT.removeClass('is-open');
                });
            }

            function openCloseItem() {
                accordionDT.on('click', function () {

                    var el = $(this),
                        target = el.next('dd'),
                        parentHeight = target.height(),
                        childHeight = target.children('.content').outerHeight(true),
                        newHeight = parentHeight > 0 ? 0 : childHeight;

                    // animate to new height
                    target.css({
                        height: newHeight
                    });

                    // remove existing classes & add class to clicked target
                    if (!el.hasClass('is-open')) {
                        el.addClass('is-open');
                    }

                    // if we are on clicked target then remove the class
                    else {
                        el.removeClass('is-open');
                    }
                });
            }

            openAll();
            closeAll();
            openCloseItem();
        });
    </script>
    <script type="text/javascript" src="JS/CSP/OnClickHandlers.js"></script>
    <script type="text/javascript" src="JS/CSP/Home.js" nonce="ihYxAijSER-YFSUxCDJbag"></script>
</body>
</html>
