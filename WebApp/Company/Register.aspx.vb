Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports System
Imports System.Linq
Imports System.Web
Imports System.Web.UI

Partial Public Class RegisterCompany
    Inherits Page
    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        If Not Roles.IsUserInRole("Admin") Then
            'ddlCompany.Visible = False
        End If

        If Not String.IsNullOrEmpty(Request.QueryString("cid")) Then
            Dim gid As Guid
            If Guid.TryParse(Request.QueryString("cid"), gid) Then
                'ddlCompany.SelectedValue = gid.ToString()
            Else
                Throw New Exception("Could not parse guid.")
            End If
        End If
    End Sub

    Protected Sub CreateUser_Click(sender As Object, e As EventArgs)
        Dim db As New DataClassesDataContext
        Dim errorFree As Boolean = True
        If Not Page.User.Identity.IsAuthenticated() Then
            Dim manager = New UserManager()
            Dim user = New ApplicationUser() With {.UserName = UserName.Text}
            Dim result = manager.Create(user, Password.Text)
            If result.Succeeded Then
                IdentityHelper.SignIn(manager, user, isPersistent:=False)
            Else
                errorFree = False
                ErrorMessage.Text = result.Errors.FirstOrDefault()
            End If
        End If
        Dim confl As Boolean = (From c In db.Companies Where c.Company = txtCompanyName.Text Select c).Count() > 0
        If confl Then
            ErrorMessage.Text = "Cannot create this company already exists with this name.  Please choose another"
        End If
        If errorFree Then
            'create company
            Dim id = Guid.NewGuid()
            Dim newComp As New Company
            newComp.Company = txtCompanyName.Text
            newComp.PrimaryEmail = txtEmail.Text
            newComp.Id = id
            db.Companies.InsertOnSubmit(newComp)
            Try
                db.SubmitChanges()
            Catch ex As Exception
                errorFree = False
                ErrorMessage.Text = ex.Message
            End Try

            If errorFree Then
                Dim p = Profile.GetProfile(UserName.Text)
                p.CompanyId = id.ToString()
                p.Save()
                IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)
            End If
        End If

    End Sub
End Class
