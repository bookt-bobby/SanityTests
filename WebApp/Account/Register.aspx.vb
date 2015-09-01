Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports System
Imports System.Linq
Imports System.Web
Imports System.Web.UI

Public Partial Class Account_Register
    Inherits Page
    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        If Not Roles.IsUserInRole("Admin") Then
            ddlCompany.Visible = False
            lblCompany.Visible = True
            Dim db As New DataClassesDataContext
            Dim ch As New ContextHelper
            lblCompany.Text = (From c In db.Companies Where c.Id.ToString() = CompanyId Select c.Company).FirstOrDefault
        End If


    End Sub
    Public ReadOnly Property CompanyId As String
        Get
            If ddlCompany.Visible Then Return ddlCompany.SelectedValue
            If Not String.IsNullOrEmpty(Request.QueryString("cid")) Then
                Dim gid As Guid
                If Guid.TryParse(Request.QueryString("cid"), gid) Then
                    Return gid.ToString()
                Else
                    Throw New Exception("Could not parse guid.")
                End If
            End If
            Throw New Exception("No company specified")
        End Get
    End Property


    Protected Sub CreateUser_Click(sender As Object, e As EventArgs)
        Dim manager = New UserManager()
        Dim user = New ApplicationUser() With {.UserName = UserName.Text}
        Dim result = manager.Create(user, Password.Text)
        If result.Succeeded Then
            IdentityHelper.SignIn(manager, user, isPersistent:=False)
            Dim p = Profile.GetProfile(UserName.Text)
            If ddlCompany.Visible = True Then
                p.CompanyId = ddlCompany.SelectedValue()
            Else
                p.CompanyId = CompanyId
            End If

            p.Save()
            IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)
        Else
            ErrorMessage.Text = result.Errors.FirstOrDefault()
        End If
    End Sub
End Class
