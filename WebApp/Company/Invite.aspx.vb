Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports System
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Net.Mail

Partial Public Class Account_InviteUser
    Inherits Page
    Private m_CompanyName As String
    Public ReadOnly Property CompanyName As String
        Get
            If Not String.IsNullOrEmpty(m_CompanyName) Then Return m_CompanyName
            Dim ch As New ContextHelper
            m_CompanyName = ch.CompanyName
            Return m_CompanyName
        End Get
    End Property

    Protected Overrides Sub OnLoad(e As EventArgs)

    End Sub

    Protected Sub InviteUser_Click(sender As Object, e As EventArgs)
        Dim ch As New ContextHelper
        Try
            Const SERVER As String = "relay-hosting.secureserver.net"

            Dim oMail As New System.Net.Mail.MailMessage
            oMail.To.Add(New MailAddress(txtEmail.Text, txtName.Text))
            oMail.IsBodyHtml = True
            oMail.Subject = "You've been invited Sanity Tests"
            oMail.Priority = System.Net.Mail.MailPriority.High   '// enumeration
            oMail.Body = String.Format("Hi, you've been invited to help manage tests for {0}.  Please follow  <a href='{2}/Account/Register?cid={1}'>this link to register</a>", ch.CompanyName, ch.Company.Id, HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority))
            Dim sc As New SmtpClient(SERVER)
            sc.Send(oMail)
            oMail = Nothing  '// free up resources

        Catch ex As Exception
            ErrorMessage.Text = String.Format("Sorry, there was a problem sending the email.  Problem was: {0}", ex.Message)
            ErrorMessage.Text &= String.Format("<br/>Please send the user you would like to invite this link: <code>" & Server.HtmlEncode("<a href='{2}/Account/Register?cid={1}'>register</a>") & "</code>", ch.CompanyName, ch.Company.Id, HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority))
        End Try
    End Sub
End Class
