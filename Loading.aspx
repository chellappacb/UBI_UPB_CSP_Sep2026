<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Loading.aspx.cs" Inherits="Loading" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link href="website/css/bootstrap.min.css" rel="stylesheet" />
    <link type="text/css" href="Website/custom-style.css" rel="Stylesheet" />

    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title>Loading, Please Wait...</title>
    <script language="javascript" type="text/javascript">
        var ctr = 1;
        var ctrMax = 50; // how many is up to you-how long does your end page take?
        var intervalId;
        function Begin() {

            var query_string = {};
            var query = window.location.search.substring(1);
            var vars = query.split("&");
            for (var i = 0; i < vars.length; i++) {
                var pair = vars[i].split("=");

            }

            if (pair[1] == "Rpt") {
                window.location.href = "PriviewRpt.aspx";
            }
            else if (pair[1] == "view") {
                window.location.href = "ViewAuthDetails.aspx";
            }

            // but make it wait while we do our progress...
            intervalId = window.setInterval(function () { ctr = UpdateIndicator(ctr, ctrMax); }, 1000);

        }
        function End() {
            // once the interval is cleared, we yield to the result page (which has been running)
            window.clearInterval(intervalId);

        }

        function UpdateIndicator(curCtr, ctrMaxIterations) {
            curCtr += 1;
            if (curCtr <= ctrMaxIterations) {
                indicator.style.width = curCtr * 10 + "px";
                return curCtr;
            }
            else {
                indicator.style.width = 0;
                return 1;
            }
        }
    </script>
</head>
<body onload="Begin()" onunload="End()">
    <form id="form1" runat="server">
    <div class="m-auto">
        <center>
        <img src="images/loading.gif" alt="" width="192" height="58" />
   
        <h3 class="text-maroon">
            Preparing Report, please wait...</h3>
        </center>
    </div>

    <div id="indicator" >
            <div class="visible-hidden text-darkOrange"></div>
    </div>

   <%-- <table  border="0" cellpadding="0" cellspacing="0" align="center" style="width: 5px;
        height: auto">
        <tr>
            <td align="left" bgcolor="#CC3300" width="100%" visible="false" height="0">
            </td>
        </tr>
    </table>--%>
    </form>

    <script type="text/javascript" src="website/js/bootstrap.bundle.min.js"> </script>
    <script src="javascript/jquery-3.7.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="JS/CSP/OnClickHandlers.js"></script>
    <script type="text/javascript" src="JS/CSP/Home.js"></script>
</body>
</html>
