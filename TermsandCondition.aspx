<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TermsandCondition.aspx.cs"
    Inherits="TermsandCondition" %>

<%@ Register Src="footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="~/HeaderInclude.ascx" TagName="headerincl" TagPrefix="id" %>
<%@ Register Src="FSCS.ascx" TagName="FSCS" TagPrefix="uc3" %>
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
                <h2 class="faq-title p-2 pl-3">Terms and Conditions</h2>
            </div>



            <div class="p-5 tc_width">
                <div class="tc_title">
                    <u>Union Premier Bond Account Specific Terms and Conditions</u>
                </div>

                <div class="pt-4">
                    <p class="tc-pi">
                        The following terms and conditions pertain specifically to the Union Premier Bond account and are supplementary to the General Terms and Conditions. In the event of any inconsistency between an account-specific condition and a provision in the General Terms and Conditions, the account-specific condition will take precedence and prevail.  
                <br />
                        <br />
                        This ensures that the terms & conditions specified for the Union Premier Bond account supersede any conflicting general provisions. If there are any uncertainties or inquiries regarding the Union Premier Bond account, it is recommended to thoroughly refer to these specific terms or contact the Bank for clarification.
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Terms and conditions</span>
                    <p class="tc-p py-1">
                        Union Premier Bond account is exclusively available as an online fixed deposit account for individuals who are UK residents, 18 years old and above as on the date of application. Applicant (s) must be holding an account in his/her/their name(s) in any authorised UK Bank or Building Society other than with the Union Bank of India (UK) Limited.
                    </p>
                    <p class="tc-p py-1">
                        When you apply for Union Premier Bond account, you are consenting to specific Terms and Conditions associated with the Union Premier Bond account you are opening. Copies of the Terms and Conditions, along with any other documents which form a part of the contract between you and us, is provided at the time of submitting application and also attached in all our communications. Additionally, these terms and conditions are also available on our website and can be provided upon request.
                    </p>
                    <p class="tc-p py-1">
                        The Terms and Conditions contain important information that requires careful reading, as they explain our responsibilities to you and your obligations to us. It is imperative that you thoroughly review all the terms and conditions and keep them safe for future reference, as they constitute a binding legal contract between you and us.
                    </p>
                    <p class="tc-p py-1">
                        Moreover, these terms and conditions supplement our General Terms and Conditions. In the event of inconsistency between an account specific condition and a provision in the General Terms and Condition, the account Specific condition will take precedence and prevail.
                    </p>
                    <p class="tc-p py-1">
                        These Terms and Conditions include certain words and phrases printed in bold type, specifying their special meaning, which is explained here.
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Eligibility</span>
                    <p class="tc-p py-1">
                        Any UK resident (s), who is/are 18 years old or above at the time of application. Applicants must possess and maintain an account in his/her/their name(s) with any authorized UK Bank or Building Society, excluding the Union Bank of India (UK) Limited.
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Currency</span>
                    <p class="tc-p py-1">
                        Union Premier Bonds are opened in Sterling (GBP) currency only.
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Investment Amount</span>
                    <p class="tc-p py-1">
                        Investment Amount refers to the sum you have contributed to open your Union Premier Bond account. Your investment must be made only by Electronic Transfer from your Nominated Account (Nominated account means your account with any authorised UK Bank or Building Society, excluding Union Bank of India (UK) Ltd. It serves as the source for investing/depositing into the Union Premier Bond account and is also the designated account to which maturing funds will be transferred back).
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Maximum/Minimum deposit</span>
                    <p class="tc-p py-1">
                        The minimum deposit amount is <b>£<span id="spMinAmt" runat="server"></span></b>, and the maximum deposit amount is <b>£<span id="spMaxAmt" runat="server"></span></b>, inclusive of joint applicants. A maximum of four joint applicants is permitted per account.
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Term/Duration</span>
                    <p class="tc-p py-1">
                        The duration for which you commit to invest in your Union Premier Bond account. When applying for a Union Premier Bond, you may place your deposit for any of the following available tenure options:
                    </p>
                    <div id="lblTD" runat="server" class="txt-color"></div>
                    <p class="tc-p py-1">
                        The tenure of the deposit will be considered from the date of full funding of the UPB application.
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Rate of Interest</span>
                    <p class="tc-p py-1">
                        For the Union Premier Bond account, the certificate will specify the applicable interest rate for the term you have applied for. We commit to paying you the rate of interest quoted in the certificate.
                    <br />
                        <br />
                        For a new application submitted, we would apply the interest rate prevailing on the date of the application submitted.
                    <br />
                        <br />
                        In case of renewal of the existing bond, we would apply the interest rate prevailing on the date of maturity. 
                    <strong>An additional loyalty bonus of 0.10% is applicable at the time of renewal</strong>, provided the renewal tenure selected is one year or above. (For renewal tenure selected less than one year, no loyalty bonus shall be applicable)
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Interest Calculation</span>
                    <p class="tc-p py-1">
                        For the Union Premier Bond account, annualised simple interest is calculated, and the total amount, including interest, is paid upon the maturity of the bond. Interest calculation starts from the date of receipt of the committed fund in full. 
                    <br />
                        <br />
                        No interest shall be paid on funds received partly. If the committed/applied fund is not received within 30 days of the date of application, the fund will be returned to the designated account, without any interest for the interim period.
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Interest payment</span>
                    <p class="tc-p py-1">
                        For all available tenures, interest is payable solely upon maturity. No interim or periodic interest is paid at any stage prior to maturity.
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Opening a Union Premier Bond account</span>
                    <p class="tc-p py-1">
                        We are obligated by UK anti-money laundering regulations to verify the identity and address of all applicants before opening a Union Premier Bond account. To meet these legal obligations, we will conduct electronic identity checks using public databases and credit reference agencies. If electronic identity verification is unsuccessful, we will request documentary evidence such as:
                    <ol class="tc-p">
                        <li>Certified copy of your valid passport or UK photo driving licence</li>
                        <li>Recent 3 months utility bill or bank statement showing your name and residential address.</li>
                    </ol>
                    </p>
                    <p class="tc-p py-1">
                        As a part of our legal obligation, we may also need to ask you about the source of funds being deposited and your reasons for opening the account. Please understand that these questions are a necessary and integral part of the account opening process, designed to ensure compliance rather than an inconvenience.
                    <br />
                        <br />
                        If we are unable to fulfil the identity and verification checks mandated by the UK anti-money laundering regulations, we regret to inform you that we will be unable to proceed with the account opening.
                    <br />
                        <br />
                        The application will be automatically cancelled if the required documents are not provided to us within 30 days from the date of application submitted.
                    <br />
                        <br />
                        If you are applying to open a joint account, by accepting these Terms and Conditions, you are confirming that you have the necessary permission from the other applicants to add their name(s) on the Account.
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Funding your Bond Account</span>
                    <p class="tc-p py-1">
                        You can remit money to fund the account only by FPS (Faster Payment Service)/CHAPS (Clearing House Automated Payment System). While remitting money please note that:
                    <ol class="tc-p">
                        <li>The remittance must be made from the nominated bank account as detailing in your Bond Application. The Bank Account must be in your name. We will remit the proceeds on maturity to the nominated Bank Account.</li>
                        <li>We also permit the remittance from Joint Bank Account, but you must be one of the Account Holders in the fore mentioned Bank Account and in this instance, we may request a statement as a proof that the funds are from a joint account.                            </li>
                        <li>We will accept multiple payments against a single Bond provided correct reference numbers are mentioned on the transfer remarks.</li>
                        <li>We will accept fund by electronic transfer only, any other mode such as a cash deposit, cheques, Bankers Drafts or Postal Orders etc is not acceptable.</li>
                        <li>Funds received after 30 days from the date of application submitted will not be applied to the Customer’s Bond Account and shall be returned. Additionally, the Bond application account will get automatically cancelled if we do not receive cleared funds prior to 30 days from the date of application submitted and the data will be deleted on the next day as per GDPR regulations.</li>
                    </ol>
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Maturity Value</span>
                    <p class="tc-p py-1">
                        A sum payable to you upon the maturity of the Union Premier Bond account, which is the original investment along with accrued interest.
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Nominated Account</span>
                    <p class="tc-p py-1">
                        A Nominated Account is a bank account held in your name(s) with any authorised Bank or Building Society in the United Kingdom, excluding the Union Bank of India (UK) Limited. This account serves as the source for investing/depositing into the Union Premier Bond account and is also the designated account to which maturing funds will be transferred back.
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Payment on Maturity</span>
                    <p class="tc-p py-1">
                        We will pay the maturity proceeds electronically using FPS/ CHAPS.<br />
                        <br />
                        In the event of a maturity payment return/failure, if funds are returned with the remark 'beneficiary account closed,' we may request you to provide a different account in your name with any authorized UK bank or building society, other than Union Bank of India (UK) Limited. Additionally, we may require a recent 3-month Account Statement of the bank account as proof of possession of the account in your name (s).<br />
                        <br />
                        No premature withdrawal or partial withdrawal is permitted during the tenure of the Union Premier Bond.
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Certificate</span>
                    <p class="tc-p py-1">
                        Means the certificate we give or send to you, which states that you are the holder of the Union Premier Bond Account and confirms the Investment Amount, Term/Duration, Rate of Interest of Bond, Maturity date and Maturity value.<br />
                        <br />
                        The certificate shall be sent to the registered email ID. In case of Joint account, the certificate shall be sent to the primary mail ID. On request, we also provide certificate by post (To Primary account holder in case of joint account).
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Electronic Transfer</span>
                    <p class="tc-p py-1">
                        Means an electronic transfer made by FPS (Faster Payment System) / CHAPS (Clearing House Automated Payment System)
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Maturity Instructions</span>
                    <p class="tc-p py-1">
                        The Bank will send you an email on two occasion, 15 days, and 7 days prior to the maturity date, outlining the process of providing maturity instructions. If you face any difficulty in submitting the maturity instructions, we do accept maturity instruction from your registered mail ID. Please email us the maturity instructions on 
                    <a class="linkStyle" href="mailto:MaturityinstructionsUPB@unionbankofindiauk.co.uk">MaturityinstructionsUPB@unionbankofindiauk.co.uk</a><br />
                        <br />
                        The maturity instruction can also be directly provided via logging in the link mentioned on our bank’s website i.e.,  
                    <a class="linkStyle" href="https://www.unionpremierbond.unionbankofindiauk.co.uk" target="_blank">https://www.unionpremierbond.unionbankofindiauk.co.uk/ECODT/Login.aspx</a>
                        <br />
                        <br />
                        Please make a note on the following information pertaining to the maturity instructions:
                    <ol class="tc-p">
                        <li>You must provide the maturity instructions latest by 10:00 AM on the maturity date.</li>
                        <li>You can revise your maturity instruction at any time prior to 10:00 AM on the maturity date.</li>
                        <li>For all renewals, the interest rate will be applicable on the date of maturity hence we request you to please check the interest rate by browsing our website on the maturity date..</li>
                        <li>In the case you are unhappy with the product offering to your renewed Bond, you have 14 days cooling period from the date of Bond renewed where you may withdraw/modify the renewed amount/tenure.</li>
                        <li>Please note that, no interest shall be paid during the 14 days cooling period if the customer opts out for withdrawal.</li>
                    </ol>
                    </p>
                    <p class="tc-p py-1">
                        At the time of providing maturity instructions, we will provide you with three options:<br />
                        <ol class="tc-p">
                            <li>Renew the bond with the maturity amount (Amount invested plus interest) at the interest rate applicable on the maturity date.</li>
                            <li>Renew the bond with a partial amount on the interest rate applicable on the maturity date, and remit back the remaining amount to your nominated Bank Account.</li>
                            <li>Remit back the maturity amount (Amount invested plus interest) to your nominated Bank Account.</li>
                        </ol>
                    </p>
                    <p class="tc-p py-1">
                        If no maturity instructions are received, we will reinvest the maturity amount (Amount invested plus interest) for a similar tenure chosen at the time of submitting the original/ recent renewal applications with the interest rate applicable for the applicable tenure on the maturity date. 
                    <br />
                        <br />
                        If the deposit is maturing on a Bank holiday or on a weekend, the interest will be paid for the holiday period as well and will be payable on the next working day. If the deposit is renewed for a further period, then the effective start date will be the original maturity date.
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Power of Attorney</span>
                    <p class="tc-p py-1">
                        We do not accept any form of Power of Attorney to the union premier bond account as it operates online. 
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Deceased case</span>
                    <p class="tc-p py-1">
                        In deceased cases, we require a certified copy of the death certificate and grant of probate to release funds.
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Communication </span>
                    <p class="tc-p py-1">
                        We will communicate with you by email using the email address provided by you in the application form. If you have a joint account, we will be communicating with the primary account holder only.
                    <br />
                        <br />
                        We will send the online login credentials to the registered email address of the primary account holder only, even in case of joint accounts also.
                    <br />
                        <br />
                        It is Primary applicant’s responsibility to take consent of all other joint applicants before opening or closing the deposit.
                    </p>
                </div>

                <div class="pt-4">
                    <span class="tc-head">Contact Us</span>
                    <p class="tc-p">
                        Please send us an e-mail at
                    <br />
                        <a class="linkStyle" href="mailto:premierbond@unionbankofindiauk.co.uk">premierbond@unionbankofindiauk.co.uk</a> - For general queries
                    <br />
                        <a class="linkStyle" href="mailto:KYCUPB@unionbankofindiauk.co.uk">KYCUPB@unionbankofindiauk.co.uk</a> - For KYC related queries      
                    <br />
                        <a class="linkStyle" href="mailto:MaturityinstructionsUPB@unionbankofindiauk.co.uk">MaturityinstructionsUPB@unionbankofindiauk.co.uk</a> - For maturity releated queries.
                    </p>
                    <p class="tc-p">
                        You may also call us on +44 20 7332 4250
                    <br />
                        Any time between 9.30 AM to 4:00 PM on working days.
                    </p>
                    <p class="tc-p pt-4">
                        <b>About FSCS Coverage </b>(<a class="linkStyle" href="https://www.fscs.org.uk/"
                            target="_blank">https://www.fscs.org.uk/</a>)
                    </p>
                    <p class="tc-p">
                        We are covered by the Financial Services Compensation Scheme (FSCS), the UK’s deposit guarantee scheme. The FSCS can pay compensation to depositors if a bank is unable to meet its financial obligations. In respect of deposits, an eligible depositor is entitled to claim up to £120,000. For joint accounts, each account holder is treated as having a claim in respect of their share, so, for example, a joint account held by two eligible depositors, the maximum amount that could be claimed would be £120,000 each (making a total of £240,000). The £120,000 limit relates to the combined amount in all the eligible depositor's accounts with the bank, including their share of any joint account, and not to each separate account. For further information about the compensation provided by the FSCS (including the amounts covered and eligibility to claim), please call our branch, refer to the FSCS website 
                    <a class="linkStyle" href="https://www.fscs.org.uk/" target="_blank">https://www.fscs.org.uk/</a> or call the FSCS on 081006781100 or 020 7741 4100. 
                    Please note that only compensation-related queries should be directed to the FSCS.                        
                    </p>
                </div>

            </div>

            <uc3:FSCS ID="FSCS" runat="server" />
            <uc2:footer ID="footer1" runat="server" />
        </div>
    </div>
    <script type="text/javascript" src="JS/CSP/Home.js" nonce="ihYxAijSER-YFSUxCDJbag"></script>
</body>
</html>
