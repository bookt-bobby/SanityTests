Imports System.IO

Partial Class ws_Default
    Inherits System.Web.UI.Page

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        Dim res As New Wrapper
        Try
            Dim db As New DataClassesDataContext
            Dim method As String = Request("method")
            If String.IsNullOrEmpty(method) Then Throw New Exception("No method specified")
            db = New DataClassesDataContext()
            Select Case method
                Case "get"
                    res.result = GetList()
                Case "delete"
                    res.result = Delete()
                Case "save"
                    res.result = Save()
            End Select
            res.success = True
        Catch ex As Exception
            res.success = False
            res.result = ex.Message
        End Try

        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
        Response.Write(jss.Serialize(res))
        Response.End()
    End Sub

    Public Function GetList() As Object
        If String.IsNullOrEmpty(Request.QueryString("entity")) Then
            Throw New Exception("Entity no supplied")
        End If
        Select Case Request.QueryString("entity")
            Case "test"
                Return GetTests()
            Case "suite"
                Return GetSuites()
            Case "company"
                Dim db As New DataClassesDataContext()
                Dim c As Company = (From co In db.Companies Where co.Id = New Guid(Profile.CompanyId) Select co).FirstOrDefault()
                Return c
        End Select
        Throw New Exception("Entity not supported")
    End Function

    Public Function GetSuites() As List(Of TestSuite)
        Dim db As New DataClassesDataContext
        Dim res As New List(Of TestSuite)
        Dim compSuites = (From s In db.Suites Where s.CompanyId.ToString() = Profile.CompanyId.ToString() Select s).ToList()
        For Each c In compSuites
            res.Add(New TestSuite(c, db))
        Next
        Return res
    End Function


    Public Function GetTests() As List(Of TestCase)
        Dim db As New DataClassesDataContext
        Dim ids = Request("ids")
        Dim suiteBySortOrder As Integer = -1
        If Not String.IsNullOrEmpty(Request.QueryString("suiteSortOrder")) AndAlso IsNumeric(Request.QueryString("suiteSortOrder")) Then
            suiteBySortOrder = Request.QueryString("suiteSortOrder")
        End If
        Dim res As New List(Of TestCase)
        If String.IsNullOrEmpty(ids) Then
            If suiteBySortOrder > -1 Then
                Dim suitGuid = (From s In db.Suites Where s.SortOrder = suiteBySortOrder Select s.Id).FirstOrDefault()
                If suitGuid = Guid.Empty Then Throw New Exception("Could not find suite")
                Dim testIDs = (From st In db.SuitTests Where st.SuiteId = suitGuid Select st.TestId).ToList()
                Dim compTests = (From t In db.Tests Where t.CompanyID.ToString() = Profile.CompanyId.ToString() AndAlso testIDs.Contains(t.Id) Select t).ToList()
                For Each c In compTests
                    Dim newTC As New TestCase(c, db)
                    res.Add(newTC) 'by default add all company tests
                Next
            Else
                Dim compTests = (From t In db.Tests Where t.CompanyID.ToString() = Profile.CompanyId.ToString() Select t).ToList()
                For Each c In compTests
                    Dim newTC As New TestCase(c, db)
                    res.Add(newTC) 'by default add all company tests
                Next
            End If

        End If
        res = (From r In res Order By r.Order Ascending Select r).ToList()
        Return res
    End Function
    Public Class TestSuite
        Public Suite As Suite
        Public Tests As New List(Of Test)
        Public Sub New()

        End Sub
        Public Sub New(_su As Suite, db As DataClassesDataContext)
            Suite = _su
            Dim stests = (From su In db.SuitTests Where su.SuiteId = _su.Id Select su).ToList()
            stests = (From st In stests Select st Order By st.Order Ascending).ToList()
            If stests IsNot Nothing Then
                For Each st In stests
                    Dim tId As Guid = st.TestId
                    If Not Guid.Empty = tId Then
                        Dim currTest = (From t In db.Tests Where t.Id.ToString() = tId.ToString()).FirstOrDefault()
                        'If Suite IsNot Nothing Then Suite.AddedOn = Nothing
                        Tests.Add(currTest)
                    End If
                Next
                'Tests=(From t In Tests Order By t.)
                'Order = st.Order
            End If
        End Sub
    End Class
    Public Class TestCase
        Public Test As Test
        Public Suite As Suite
        Public Order As Integer
        Public Sub New()

        End Sub
        Public Sub New(t As Test, db As DataClassesDataContext)
            Test = t
            Dim st As SuitTest = (From su In db.SuitTests Where su.TestId = t.Id Select su).FirstOrDefault()
            If st IsNot Nothing Then
                Dim suId As Guid = st.SuiteId
                If Not Guid.Empty = suId Then
                    Suite = (From s In db.Suites Where s.Id.ToString() = suId.ToString()).FirstOrDefault()
                    If Suite IsNot Nothing Then Suite.AddedOn = Nothing
                End If
                Order = st.Order
            End If
        End Sub
    End Class

    Public Function DeleteTest() As Boolean
        Dim db As New DataClassesDataContext
        Dim theTest = GetTestFromQS()
        db.Tests.Attach(theTest)
        db.Tests.DeleteOnSubmit(theTest)
        db.SubmitChanges()
        Return True
    End Function

    Public Function DeleteSuite() As Boolean
        Dim db As New DataClassesDataContext
        Dim theSuite = GetSuiteFromQS()
        db.Suites.Attach(theSuite)
        db.Suites.DeleteOnSubmit(theSuite)
        db.SubmitChanges()
        Return True
    End Function

    Public Function Delete() As Boolean
        Select Case Request.QueryString("entity")
            Case "test"
                Return DeleteTest()
            Case "suite"
                Return DeleteSuite()
        End Select
        Throw New Exception("Entity not supported")
    End Function

    Public Function GetInputStream() As String
        Dim reader = New StreamReader(Request.InputStream)
        Dim text = reader.ReadToEnd()
        Return text
    End Function
    Public Function Save() As Object
        Select Case Request.QueryString("entity")
            Case "test"
                Return SaveTest()
            Case "suite"
                Return SaveSuite()
            Case "company"
                Return SaveCompany()
        End Select
        Throw New Exception("Entity Not Supported")
    End Function
    Public Function SaveTest() As TestCase
        Dim db As New DataClassesDataContext
        Dim d = Request.InputStream
        Dim tc As New TestCase()
        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim str As String = GetInputStream()
        tc = jss.Deserialize(Of TestCase)(str)
        Dim t As Test = tc.Test
        Dim id = t.Id.ToString()
        't = Nothing
        If t.Test = "TSE" Then
            Throw New Exception("Cannot save test like this")
        End If
        Dim res As New TestCase
        If Guid.Empty.ToString() = id Then
            t.Id = Guid.NewGuid()
            t.CompanyID = New Guid(Profile.CompanyId)
            t.CreatedBy = User.Identity.Name
            db.Tests.InsertOnSubmit(t)
            db.SubmitChanges()
            res.Test = t
        Else
            Dim updateTest = (From te In db.Tests Where te.Id.ToString() = id Select te).FirstOrDefault()
            updateTest.Property = t.Property
            updateTest.Value = t.Value
            updateTest.Method = t.Method
            updateTest.Description = t.Description
            updateTest.Test = t.Test
            updateTest.Url = t.Url
            updateTest.ContentType = t.ContentType
            updateTest.ActualValue = t.ActualValue
            updateTest.ErrorMessagePath = t.ErrorMessagePath
            updateTest.IsGlobal = t.IsGlobal
            updateTest.PostData = t.PostData
            updateTest.ReturnParamName = t.ReturnParamName
            updateTest.ReturnParamPath = t.ReturnParamPath
            updateTest.PauseFirst = t.PauseFirst
            res.Test = updateTest
            If tc.Suite IsNot Nothing AndAlso Not String.IsNullOrEmpty(tc.Suite.TestSuite) Then
                Dim theSuite = (From s In db.Suites Where s.TestSuite = tc.Suite.TestSuite AndAlso s.CompanyId.ToString() = Profile.CompanyId Select s).FirstOrDefault
                If theSuite Is Nothing Then
                    theSuite = New Suite
                    theSuite.Id = Guid.NewGuid
                    theSuite.CompanyId = New Guid(Profile.CompanyId)
                    theSuite.TestSuite = tc.Suite.TestSuite
                    theSuite.CreatedBy = Page.User.Identity.Name
                    theSuite.AddedOn = Now
                    db.Suites.InsertOnSubmit(theSuite)
                End If
                Dim testSuite = (From ts In db.SuitTests Where ts.SuiteId.ToString() = theSuite.Id.ToString() _
                                AndAlso ts.TestId.ToString() = updateTest.Id.ToString() Select ts).FirstOrDefault()
                If testSuite Is Nothing Then
                    testSuite = New SuitTest
                    testSuite.SuiteId = theSuite.Id
                    testSuite.TestId = updateTest.Id
                    testSuite.Id = Guid.NewGuid()
                    db.SuitTests.InsertOnSubmit(testSuite)
                End If
                testSuite.Order = tc.Order
                res.Suite = theSuite
                res.Order = tc.Order
            End If
            db.SubmitChanges()
        End If
        Return res
    End Function

    Public Function SaveCompany() As Company
        Dim db As New DataClassesDataContext
        Dim d = Request.InputStream
        Dim ts As New Company
        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim str As String = GetInputStream()
        ts = jss.Deserialize(Of Company)(str)
        Dim theCompany As Company
        If Guid.Empty.ToString() = ts.Id.ToString() Then
            theCompany = New Company()
            theCompany.Id = Guid.NewGuid
            db.Companies.InsertOnSubmit(theCompany)
        Else
            theCompany = (From c In db.Companies Where c.Id = ts.Id Select c).FirstOrDefault()
        End If
        theCompany.Company = ts.Company
        theCompany.PrimaryEmail = ts.PrimaryEmail
        theCompany.Status = ts.Status
        theCompany.BaseUrl = ts.BaseUrl
        db.SubmitChanges()
        Return theCompany
    End Function

    Public Function SaveSuite() As TestSuite
        Dim db As New DataClassesDataContext
        Dim d = Request.InputStream
        Dim ts As New TestSuite()
        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim str As String = GetInputStream()
        ts = jss.Deserialize(Of TestSuite)(str)
        Dim s = ts.Suite
        't = Nothing
        If s.TestSuite = "TSE" Then
            Throw New Exception("Cannot save test like this")
        End If
        If Guid.Empty.ToString() = ID Then
            s.Id = Guid.NewGuid()
            s.CompanyId = New Guid(Profile.CompanyId)
            s.CreatedBy = User.Identity.Name
            db.Suites.InsertOnSubmit(s)
            db.SubmitChanges()
            ts.Suite = s
        Else
            Dim updateSuite = (From su In db.Suites Where su.Id.ToString() = s.Id.ToString() Select su).FirstOrDefault()
            updateSuite.TestSuite = s.TestSuite
            updateSuite.Description = s.Description
            updateSuite.TestInOrder = s.TestInOrder
            db.SubmitChanges()
            ts.Suite = updateSuite
        End If
        Return ts
    End Function
    Private Function GetTestFromQS() As Test
        Dim db As New DataClassesDataContext
        Dim id = Request("id")
        If String.IsNullOrEmpty(id) Then Throw New Exception("No id specified to delete.")
        If Not Guid.TryParse(id, New Guid) Then Throw New Exception("Id must be guid")
        Dim theTest = (From t In db.Tests Where t.Id.ToString() = id Select t).FirstOrDefault()
        Return theTest
    End Function
    Private Function GetSuiteFromQS() As Suite
        Dim db As New DataClassesDataContext
        Dim id = Request("id")
        If String.IsNullOrEmpty(id) Then Throw New Exception("No id specified to delete.")
        If Not Guid.TryParse(id, New Guid) Then Throw New Exception("Id must be guid")
        Dim theSuite = (From s In db.Suites Where s.Id.ToString() = id Select s).FirstOrDefault()
        Return theSuite
    End Function
    Class Wrapper
        Public success As Boolean
        Public result As Object
    End Class
End Class
