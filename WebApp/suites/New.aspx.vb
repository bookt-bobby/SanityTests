Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports System
Imports System.Linq
Imports System.Web
Imports System.Web.UI

Partial Public Class Suites_Default
    Inherits Page
    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        If Not Page.IsPostBack Then
            
        End If
    End Sub
    Protected Sub CreateSuite_Click(sender As Object, e As EventArgs)
        Dim db As New DataClassesDataContext
        Dim s As New Suite
        s.TestSuite = SuiteName.Text
        s.Description = Description.Text
        s.CreatedBy = Page.User.Identity.GetUserName()
        s.Id = Guid.NewGuid()
        s.CompanyId = New Guid(Profile.CompanyId)
        s.TestInOrder = Sequential.Checked
        db.Suites.InsertOnSubmit(s)
        Try
            db.SubmitChanges()
            SuccessMessage.Text = String.Format("Suite successfully added. Now <a href='/tests/new/?sid={0}'>add some tests</a> ", s.SortOrder)
            SuccessMessage.Visible = True
        Catch ex As Exception
            ErrorMessage.Text = ex.Message
            Exit Sub
        End Try
    End Sub
End Class
