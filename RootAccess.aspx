<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RootAccess.aspx.vb" Inherits="RootAccess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="MacroWebControls" Namespace="MacroWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome to Union Bank of India UK</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <link href="~/website/rootaccess.css" rel="stylesheet" runat="server" />
</head>
<body>
    <form id="form1" autocomplete="off" runat="server">
        <div class="login-container">
            <p class="text-danger">
                Scheduled Maintenance Notice: The Union Premier Bond platform will be temporarily unavailable on 15 April 2026, from 15:30 to 17:00 BST, due to a planned system maintenance activity. We apologise for any inconvenience caused and appreciate your patience.
            </p>
            <asp:Label ID="label1" runat="server" Text=""></asp:Label>
            <cc1:MacroWebTextBox ID="txtUserID" runat="server" CssClass="txtbox form-control"
                Validate="IsEmail" BlurBackground="White" TabIndex="1" placeholder="Username"></cc1:MacroWebTextBox>
            <cc1:MacroWebTextBox ID="txtPassword" runat="server" CssClass="txtbox form-control"
                TextMode="Password" BlurBackground="White" Validate="IsAlphaNumSpec" TabIndex="2" placeholder="Password"></cc1:MacroWebTextBox>
            <asp:Button ID="btnSubmit" runat="server" Text="Log In" CssClass="button" TabIndex="3" />
        </div>
    </form>
</body>
</html>
