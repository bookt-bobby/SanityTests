Imports Microsoft.VisualBasic

Public Class ContextHelper
    Private m_Company As Company
    Public ReadOnly Property Company As Company
        Get
            If m_Company IsNot Nothing Then Return m_Company
            Dim db As New DataClassesDataContext
            If Not HttpContext.Current.User.Identity.IsAuthenticated Then Return Nothing
            Dim cid As String = HttpContext.Current.Profile.GetPropertyValue("CompanyId")
            If Not String.IsNullOrEmpty(cid) Then
                Dim c = (From co In db.Companies Where co.Id.ToString() = cid Select co).FirstOrDefault()
                m_Company = c
                Return m_Company
            End If
            Return Nothing
        End Get
    End Property
    Public ReadOnly Property CompanyName As String
        Get
            If Company IsNot Nothing Then Return Company.Company
            Return ""
        End Get
    End Property
End Class
