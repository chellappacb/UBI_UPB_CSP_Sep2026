Imports System.Data
Partial Class RootAccess
    Inherits System.Web.UI.Page

    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click

        Dim strUserName As String = txtUserID.Text.Trim()
        Dim strPassWord As String = txtPassword.Text.Trim()

        If strUserName = "" Or strPassWord = "" Then
            label1.Text = "Please enter both user name and password"
            Exit Sub
        End If

        Dim dataPath As String = Server.MapPath("~/App_Data/UserInformation.xml")
        Dim dSet As New DataSet()
        dSet.ReadXml(dataPath)
        Dim rows As DataRow() = dSet.Tables(0).Select("UserName = '" + strUserName + "' AND Password = '" + strPassWord + "'")

        If rows.Length > 0 Then
            FormsAuthentication.RedirectFromLoginPage(strUserName, True)
        Else
            label1.Text = "Login details are not valid"
        End If


    End Sub
End Class
