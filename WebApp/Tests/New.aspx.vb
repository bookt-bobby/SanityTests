Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports System
Imports System.Linq
Imports System.Web
Imports System.Web.UI

Partial Public Class NewTest
    Inherits Page
    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        If Not Page.IsPostBack Then
            Suite.Items.Insert(0, "")
        End If
    End Sub

    Protected Sub Suite_DataBound(sender As Object, e As EventArgs) Handles Suite.DataBound
        If Not String.IsNullOrEmpty(Request.QueryString("sid")) AndAlso IsNumeric(Request.QueryString("sid")) Then
            Dim db As New DataClassesDataContext
            Dim id As Integer = Request.QueryString("sid")
            Dim su = (From s In db.Suites Where s.SortOrder = id Select s).FirstOrDefault
            If su IsNot Nothing Then
                Suite.SelectedValue = su.Id.ToString()
            End If
        End If
    End Sub
    Protected Sub CreateTest_Click(sender As Object, e As EventArgs)
        Dim db As New DataClassesDataContext
        Dim t As New Test
        t.PostData = PostData.Text
        t.Property = [Property].Text
        t.Test = TestName.Text
        t.Method = Method.SelectedValue
        t.Value = Value.Text
        t.IsGlobal = [Global].Checked
        t.CreatedBy = Page.User.Identity.GetUserName()
        t.CompanyID = New Guid(Profile.CompanyId)
        t.ContentType = ContentType.SelectedValue
        t.Description = Description.Text
        t.Id = Guid.NewGuid()
        t.Url = Url.Text
        t.ReturnParamName = ReturnParamName.Text
        t.ReturnParamPath = ReturnParamPath.Text
        t.PauseFirst = PauseFirst.Checked
        db.Tests.InsertOnSubmit(t)
        Try
            db.SubmitChanges()
            SuccessMessage.Visible = True
        Catch ex As Exception
            ErrorMessage.Text = ex.Message
            Exit Sub
        End Try
        If Not String.IsNullOrEmpty(Suite.SelectedValue) Then
            Dim newSuitTest As New SuitTest
            newSuitTest.SuiteId = New Guid(Suite.SelectedValue)
            newSuitTest.TestId = t.Id
            newSuitTest.Id = Guid.NewGuid()
            newSuitTest.Order = 0
            Integer.TryParse(Order.Text, newSuitTest.Order)
            db.SuitTests.InsertOnSubmit(newSuitTest)
            Try
                db.SubmitChanges()
                SuccessMessage.Visible = True
                If Not String.IsNullOrEmpty(Suite.SelectedValue) Then
                    Dim su = (From s In db.Suites Where s.Id.ToString() = Suite.SelectedValue Select s).FirstOrDefault
                    If su IsNot Nothing Then
                        SuccessMessage.Text = String.Format("Test successfully added to {1}.  Add a new test below," & _
                                                                               "<a href='/tests/#/run/{0}'>run</a> or <a href='/tests/#/edit/{0}'>edit tests </a> for {1},", _
                                                                               su.SortOrder, Suite.SelectedItem.Text)
                    End If

                End If
            Catch ex As Exception
                ErrorMessage.Text = ex.Message
                Exit Sub
            End Try
        End If
    End Sub
End Class
