﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.18444
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.Linq
Imports System.Data.Linq.Mapping
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Reflection


<Global.System.Data.Linq.Mapping.DatabaseAttribute(Name:="rcbrown")>  _
Partial Public Class DataClassesDataContext
	Inherits System.Data.Linq.DataContext
	
	Private Shared mappingSource As System.Data.Linq.Mapping.MappingSource = New AttributeMappingSource()
	
  #Region "Extensibility Method Definitions"
  Partial Private Sub OnCreated()
  End Sub
  Partial Private Sub InsertSuitTest(instance As SuitTest)
    End Sub
  Partial Private Sub UpdateSuitTest(instance As SuitTest)
    End Sub
  Partial Private Sub DeleteSuitTest(instance As SuitTest)
    End Sub
  Partial Private Sub InsertSuite(instance As Suite)
    End Sub
  Partial Private Sub UpdateSuite(instance As Suite)
    End Sub
  Partial Private Sub DeleteSuite(instance As Suite)
    End Sub
  Partial Private Sub InsertCompany(instance As Company)
    End Sub
  Partial Private Sub UpdateCompany(instance As Company)
    End Sub
  Partial Private Sub DeleteCompany(instance As Company)
    End Sub
  Partial Private Sub InsertTest(instance As Test)
    End Sub
  Partial Private Sub UpdateTest(instance As Test)
    End Sub
  Partial Private Sub DeleteTest(instance As Test)
    End Sub
  #End Region
	
	Public Sub New()
		MyBase.New(Global.System.Configuration.ConfigurationManager.ConnectionStrings("rcbrownConnectionString").ConnectionString, mappingSource)
		OnCreated
	End Sub
	
	Public Sub New(ByVal connection As String)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public Sub New(ByVal connection As System.Data.IDbConnection)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public Sub New(ByVal connection As String, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public Sub New(ByVal connection As System.Data.IDbConnection, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public ReadOnly Property SuitTests() As System.Data.Linq.Table(Of SuitTest)
		Get
			Return Me.GetTable(Of SuitTest)
		End Get
	End Property
	
	Public ReadOnly Property Suites() As System.Data.Linq.Table(Of Suite)
		Get
			Return Me.GetTable(Of Suite)
		End Get
	End Property
	
	Public ReadOnly Property Companies() As System.Data.Linq.Table(Of Company)
		Get
			Return Me.GetTable(Of Company)
		End Get
	End Property
	
	Public ReadOnly Property Tests() As System.Data.Linq.Table(Of Test)
		Get
			Return Me.GetTable(Of Test)
		End Get
	End Property
End Class

<Global.System.Data.Linq.Mapping.TableAttribute(Name:="dbo.SuitTest")>  _
Partial Public Class SuitTest
	Implements System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	
	Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)
	
	Private _Id As System.Guid
	
	Private _TestId As System.Nullable(Of System.Guid)
	
	Private _SuiteId As System.Nullable(Of System.Guid)
	
	Private _Order As System.Nullable(Of Integer)
	
    #Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnValidate(action As System.Data.Linq.ChangeAction)
    End Sub
    Partial Private Sub OnCreated()
    End Sub
    Partial Private Sub OnIdChanging(value As System.Guid)
    End Sub
    Partial Private Sub OnIdChanged()
    End Sub
    Partial Private Sub OnTestIdChanging(value As System.Nullable(Of System.Guid))
    End Sub
    Partial Private Sub OnTestIdChanged()
    End Sub
    Partial Private Sub OnSuiteIdChanging(value As System.Nullable(Of System.Guid))
    End Sub
    Partial Private Sub OnSuiteIdChanged()
    End Sub
    Partial Private Sub OnOrderChanging(value As System.Nullable(Of Integer))
    End Sub
    Partial Private Sub OnOrderChanged()
    End Sub
    #End Region
	
	Public Sub New()
		MyBase.New
		OnCreated
	End Sub
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Id", DbType:="UniqueIdentifier NOT NULL", IsPrimaryKey:=true)>  _
	Public Property Id() As System.Guid
		Get
			Return Me._Id
		End Get
		Set
			If ((Me._Id = value)  _
						= false) Then
				Me.OnIdChanging(value)
				Me.SendPropertyChanging
				Me._Id = value
				Me.SendPropertyChanged("Id")
				Me.OnIdChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_TestId", DbType:="UniqueIdentifier")>  _
	Public Property TestId() As System.Nullable(Of System.Guid)
		Get
			Return Me._TestId
		End Get
		Set
			If (Me._TestId.Equals(value) = false) Then
				Me.OnTestIdChanging(value)
				Me.SendPropertyChanging
				Me._TestId = value
				Me.SendPropertyChanged("TestId")
				Me.OnTestIdChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_SuiteId", DbType:="UniqueIdentifier")>  _
	Public Property SuiteId() As System.Nullable(Of System.Guid)
		Get
			Return Me._SuiteId
		End Get
		Set
			If (Me._SuiteId.Equals(value) = false) Then
				Me.OnSuiteIdChanging(value)
				Me.SendPropertyChanging
				Me._SuiteId = value
				Me.SendPropertyChanged("SuiteId")
				Me.OnSuiteIdChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Order", DbType:="Int")>  _
	Public Property [Order]() As System.Nullable(Of Integer)
		Get
			Return Me._Order
		End Get
		Set
			If (Me._Order.Equals(value) = false) Then
				Me.OnOrderChanging(value)
				Me.SendPropertyChanging
				Me._Order = value
				Me.SendPropertyChanged("[Order]")
				Me.OnOrderChanged
			End If
		End Set
	End Property
	
	Public Event PropertyChanging As PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging
	
	Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
	
	Protected Overridable Sub SendPropertyChanging()
		If ((Me.PropertyChangingEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanging(Me, emptyChangingEventArgs)
		End If
	End Sub
	
	Protected Overridable Sub SendPropertyChanged(ByVal propertyName As [String])
		If ((Me.PropertyChangedEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End If
	End Sub
End Class

<Global.System.Data.Linq.Mapping.TableAttribute(Name:="dbo.Suite")>  _
Partial Public Class Suite
	Implements System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	
	Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)
	
	Private _Id As System.Guid
	
	Private _TestSuite As String
	
	Private _CreatedBy As String
	
	Private _AddedOn As System.Nullable(Of Date)
	
	Private _Description As String
	
	Private _CompanyId As System.Nullable(Of System.Guid)
	
	Private _SortOrder As Integer
	
	Private _SuiteId As System.Nullable(Of System.Guid)
	
	Private _TestInOrder As System.Nullable(Of Boolean)
	
    #Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnValidate(action As System.Data.Linq.ChangeAction)
    End Sub
    Partial Private Sub OnCreated()
    End Sub
    Partial Private Sub OnIdChanging(value As System.Guid)
    End Sub
    Partial Private Sub OnIdChanged()
    End Sub
    Partial Private Sub OnTestSuiteChanging(value As String)
    End Sub
    Partial Private Sub OnTestSuiteChanged()
    End Sub
    Partial Private Sub OnCreatedByChanging(value As String)
    End Sub
    Partial Private Sub OnCreatedByChanged()
    End Sub
    Partial Private Sub OnAddedOnChanging(value As System.Nullable(Of Date))
    End Sub
    Partial Private Sub OnAddedOnChanged()
    End Sub
    Partial Private Sub OnDescriptionChanging(value As String)
    End Sub
    Partial Private Sub OnDescriptionChanged()
    End Sub
    Partial Private Sub OnCompanyIdChanging(value As System.Nullable(Of System.Guid))
    End Sub
    Partial Private Sub OnCompanyIdChanged()
    End Sub
    Partial Private Sub OnSortOrderChanging(value As Integer)
    End Sub
    Partial Private Sub OnSortOrderChanged()
    End Sub
    Partial Private Sub OnSuiteIdChanging(value As System.Nullable(Of System.Guid))
    End Sub
    Partial Private Sub OnSuiteIdChanged()
    End Sub
    Partial Private Sub OnTestInOrderChanging(value As System.Nullable(Of Boolean))
    End Sub
    Partial Private Sub OnTestInOrderChanged()
    End Sub
    #End Region
	
	Public Sub New()
		MyBase.New
		OnCreated
	End Sub
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Id", DbType:="UniqueIdentifier NOT NULL", IsPrimaryKey:=true)>  _
	Public Property Id() As System.Guid
		Get
			Return Me._Id
		End Get
		Set
			If ((Me._Id = value)  _
						= false) Then
				Me.OnIdChanging(value)
				Me.SendPropertyChanging
				Me._Id = value
				Me.SendPropertyChanged("Id")
				Me.OnIdChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_TestSuite", DbType:="VarChar(50)")>  _
	Public Property TestSuite() As String
		Get
			Return Me._TestSuite
		End Get
		Set
			If (String.Equals(Me._TestSuite, value) = false) Then
				Me.OnTestSuiteChanging(value)
				Me.SendPropertyChanging
				Me._TestSuite = value
				Me.SendPropertyChanged("TestSuite")
				Me.OnTestSuiteChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_CreatedBy", DbType:="VarChar(50)")>  _
	Public Property CreatedBy() As String
		Get
			Return Me._CreatedBy
		End Get
		Set
			If (String.Equals(Me._CreatedBy, value) = false) Then
				Me.OnCreatedByChanging(value)
				Me.SendPropertyChanging
				Me._CreatedBy = value
				Me.SendPropertyChanged("CreatedBy")
				Me.OnCreatedByChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_AddedOn", DbType:="DateTime")>  _
	Public Property AddedOn() As System.Nullable(Of Date)
		Get
			Return Me._AddedOn
		End Get
		Set
			If (Me._AddedOn.Equals(value) = false) Then
				Me.OnAddedOnChanging(value)
				Me.SendPropertyChanging
				Me._AddedOn = value
				Me.SendPropertyChanged("AddedOn")
				Me.OnAddedOnChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Description", DbType:="NVarChar(400)")>  _
	Public Property Description() As String
		Get
			Return Me._Description
		End Get
		Set
			If (String.Equals(Me._Description, value) = false) Then
				Me.OnDescriptionChanging(value)
				Me.SendPropertyChanging
				Me._Description = value
				Me.SendPropertyChanged("Description")
				Me.OnDescriptionChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_CompanyId", DbType:="UniqueIdentifier")>  _
	Public Property CompanyId() As System.Nullable(Of System.Guid)
		Get
			Return Me._CompanyId
		End Get
		Set
			If (Me._CompanyId.Equals(value) = false) Then
				Me.OnCompanyIdChanging(value)
				Me.SendPropertyChanging
				Me._CompanyId = value
				Me.SendPropertyChanged("CompanyId")
				Me.OnCompanyIdChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_SortOrder", AutoSync:=AutoSync.Always, DbType:="Int NOT NULL IDENTITY", IsDbGenerated:=true)>  _
	Public Property SortOrder() As Integer
		Get
			Return Me._SortOrder
		End Get
		Set
			If ((Me._SortOrder = value)  _
						= false) Then
				Me.OnSortOrderChanging(value)
				Me.SendPropertyChanging
				Me._SortOrder = value
				Me.SendPropertyChanged("SortOrder")
				Me.OnSortOrderChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_SuiteId", DbType:="UniqueIdentifier")>  _
	Public Property SuiteId() As System.Nullable(Of System.Guid)
		Get
			Return Me._SuiteId
		End Get
		Set
			If (Me._SuiteId.Equals(value) = false) Then
				Me.OnSuiteIdChanging(value)
				Me.SendPropertyChanging
				Me._SuiteId = value
				Me.SendPropertyChanged("SuiteId")
				Me.OnSuiteIdChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_TestInOrder", DbType:="Bit")>  _
	Public Property TestInOrder() As System.Nullable(Of Boolean)
		Get
			Return Me._TestInOrder
		End Get
		Set
			If (Me._TestInOrder.Equals(value) = false) Then
				Me.OnTestInOrderChanging(value)
				Me.SendPropertyChanging
				Me._TestInOrder = value
				Me.SendPropertyChanged("TestInOrder")
				Me.OnTestInOrderChanged
			End If
		End Set
	End Property
	
	Public Event PropertyChanging As PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging
	
	Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
	
	Protected Overridable Sub SendPropertyChanging()
		If ((Me.PropertyChangingEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanging(Me, emptyChangingEventArgs)
		End If
	End Sub
	
	Protected Overridable Sub SendPropertyChanged(ByVal propertyName As [String])
		If ((Me.PropertyChangedEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End If
	End Sub
End Class

<Global.System.Data.Linq.Mapping.TableAttribute(Name:="dbo.Company")>  _
Partial Public Class Company
	Implements System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	
	Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)
	
	Private _Id As System.Guid
	
	Private _Company As String
	
	Private _PrimaryEmail As String
	
	Private _Status As System.Nullable(Of Integer)
	
	Private _BaseUrl As String
	
    #Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnValidate(action As System.Data.Linq.ChangeAction)
    End Sub
    Partial Private Sub OnCreated()
    End Sub
    Partial Private Sub OnIdChanging(value As System.Guid)
    End Sub
    Partial Private Sub OnIdChanged()
    End Sub
    Partial Private Sub OnCompanyChanging(value As String)
    End Sub
    Partial Private Sub OnCompanyChanged()
    End Sub
    Partial Private Sub OnPrimaryEmailChanging(value As String)
    End Sub
    Partial Private Sub OnPrimaryEmailChanged()
    End Sub
    Partial Private Sub OnStatusChanging(value As System.Nullable(Of Integer))
    End Sub
    Partial Private Sub OnStatusChanged()
    End Sub
    Partial Private Sub OnBaseUrlChanging(value As String)
    End Sub
    Partial Private Sub OnBaseUrlChanged()
    End Sub
    #End Region
	
	Public Sub New()
		MyBase.New
		OnCreated
	End Sub
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Id", DbType:="UniqueIdentifier NOT NULL", IsPrimaryKey:=true)>  _
	Public Property Id() As System.Guid
		Get
			Return Me._Id
		End Get
		Set
			If ((Me._Id = value)  _
						= false) Then
				Me.OnIdChanging(value)
				Me.SendPropertyChanging
				Me._Id = value
				Me.SendPropertyChanged("Id")
				Me.OnIdChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Company", DbType:="VarChar(100)")>  _
	Public Property Company() As String
		Get
			Return Me._Company
		End Get
		Set
			If (String.Equals(Me._Company, value) = false) Then
				Me.OnCompanyChanging(value)
				Me.SendPropertyChanging
				Me._Company = value
				Me.SendPropertyChanged("Company")
				Me.OnCompanyChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_PrimaryEmail", DbType:="VarChar(100)")>  _
	Public Property PrimaryEmail() As String
		Get
			Return Me._PrimaryEmail
		End Get
		Set
			If (String.Equals(Me._PrimaryEmail, value) = false) Then
				Me.OnPrimaryEmailChanging(value)
				Me.SendPropertyChanging
				Me._PrimaryEmail = value
				Me.SendPropertyChanged("PrimaryEmail")
				Me.OnPrimaryEmailChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Status", DbType:="Int")>  _
	Public Property Status() As System.Nullable(Of Integer)
		Get
			Return Me._Status
		End Get
		Set
			If (Me._Status.Equals(value) = false) Then
				Me.OnStatusChanging(value)
				Me.SendPropertyChanging
				Me._Status = value
				Me.SendPropertyChanged("Status")
				Me.OnStatusChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_BaseUrl", DbType:="VarChar(100)")>  _
	Public Property BaseUrl() As String
		Get
			Return Me._BaseUrl
		End Get
		Set
			If (String.Equals(Me._BaseUrl, value) = false) Then
				Me.OnBaseUrlChanging(value)
				Me.SendPropertyChanging
				Me._BaseUrl = value
				Me.SendPropertyChanged("BaseUrl")
				Me.OnBaseUrlChanged
			End If
		End Set
	End Property
	
	Public Event PropertyChanging As PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging
	
	Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
	
	Protected Overridable Sub SendPropertyChanging()
		If ((Me.PropertyChangingEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanging(Me, emptyChangingEventArgs)
		End If
	End Sub
	
	Protected Overridable Sub SendPropertyChanged(ByVal propertyName As [String])
		If ((Me.PropertyChangedEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End If
	End Sub
End Class

<Global.System.Data.Linq.Mapping.TableAttribute(Name:="dbo.Test")>  _
Partial Public Class Test
	Implements System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	
	Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)
	
	Private _Id As System.Guid
	
	Private _Test As String
	
	Private _ContentType As String
	
	Private _PostData As String
	
	Private _Property As String
	
	Private _Value As String
	
	Private _CreatedBy As String
	
	Private _IsGlobal As System.Nullable(Of Boolean)
	
	Private _Method As String
	
	Private _CompanyID As System.Nullable(Of System.Guid)
	
	Private _Url As String
	
	Private _Description As String
	
	Private _ActualValue As String
	
	Private _ErrorMessagePath As String
	
	Private _ReturnParamName As String
	
	Private _ReturnParamPath As String
	
	Private _PauseFirst As System.Nullable(Of Boolean)
	
    #Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnValidate(action As System.Data.Linq.ChangeAction)
    End Sub
    Partial Private Sub OnCreated()
    End Sub
    Partial Private Sub OnIdChanging(value As System.Guid)
    End Sub
    Partial Private Sub OnIdChanged()
    End Sub
    Partial Private Sub OnTestChanging(value As String)
    End Sub
    Partial Private Sub OnTestChanged()
    End Sub
    Partial Private Sub OnContentTypeChanging(value As String)
    End Sub
    Partial Private Sub OnContentTypeChanged()
    End Sub
    Partial Private Sub OnPostDataChanging(value As String)
    End Sub
    Partial Private Sub OnPostDataChanged()
    End Sub
    Partial Private Sub OnPropertyChanging(value As String)
    End Sub
    Partial Private Sub OnPropertyChanged()
    End Sub
    Partial Private Sub OnValueChanging(value As String)
    End Sub
    Partial Private Sub OnValueChanged()
    End Sub
    Partial Private Sub OnCreatedByChanging(value As String)
    End Sub
    Partial Private Sub OnCreatedByChanged()
    End Sub
    Partial Private Sub OnIsGlobalChanging(value As System.Nullable(Of Boolean))
    End Sub
    Partial Private Sub OnIsGlobalChanged()
    End Sub
    Partial Private Sub OnMethodChanging(value As String)
    End Sub
    Partial Private Sub OnMethodChanged()
    End Sub
    Partial Private Sub OnCompanyIDChanging(value As System.Nullable(Of System.Guid))
    End Sub
    Partial Private Sub OnCompanyIDChanged()
    End Sub
    Partial Private Sub OnUrlChanging(value As String)
    End Sub
    Partial Private Sub OnUrlChanged()
    End Sub
    Partial Private Sub OnDescriptionChanging(value As String)
    End Sub
    Partial Private Sub OnDescriptionChanged()
    End Sub
    Partial Private Sub OnActualValueChanging(value As String)
    End Sub
    Partial Private Sub OnActualValueChanged()
    End Sub
    Partial Private Sub OnErrorMessagePathChanging(value As String)
    End Sub
    Partial Private Sub OnErrorMessagePathChanged()
    End Sub
    Partial Private Sub OnReturnParamNameChanging(value As String)
    End Sub
    Partial Private Sub OnReturnParamNameChanged()
    End Sub
    Partial Private Sub OnReturnParamPathChanging(value As String)
    End Sub
    Partial Private Sub OnReturnParamPathChanged()
    End Sub
    Partial Private Sub OnPauseFirstChanging(value As System.Nullable(Of Boolean))
    End Sub
    Partial Private Sub OnPauseFirstChanged()
    End Sub
    #End Region
	
	Public Sub New()
		MyBase.New
		OnCreated
	End Sub
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Id", DbType:="UniqueIdentifier NOT NULL", IsPrimaryKey:=true)>  _
	Public Property Id() As System.Guid
		Get
			Return Me._Id
		End Get
		Set
			If ((Me._Id = value)  _
						= false) Then
				Me.OnIdChanging(value)
				Me.SendPropertyChanging
				Me._Id = value
				Me.SendPropertyChanged("Id")
				Me.OnIdChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Test", DbType:="VarChar(100)")>  _
	Public Property Test() As String
		Get
			Return Me._Test
		End Get
		Set
			If (String.Equals(Me._Test, value) = false) Then
				Me.OnTestChanging(value)
				Me.SendPropertyChanging
				Me._Test = value
				Me.SendPropertyChanged("Test")
				Me.OnTestChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ContentType", DbType:="VarChar(50)")>  _
	Public Property ContentType() As String
		Get
			Return Me._ContentType
		End Get
		Set
			If (String.Equals(Me._ContentType, value) = false) Then
				Me.OnContentTypeChanging(value)
				Me.SendPropertyChanging
				Me._ContentType = value
				Me.SendPropertyChanged("ContentType")
				Me.OnContentTypeChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_PostData", DbType:="Text", UpdateCheck:=UpdateCheck.Never)>  _
	Public Property PostData() As String
		Get
			Return Me._PostData
		End Get
		Set
			If (String.Equals(Me._PostData, value) = false) Then
				Me.OnPostDataChanging(value)
				Me.SendPropertyChanging
				Me._PostData = value
				Me.SendPropertyChanged("PostData")
				Me.OnPostDataChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Name:="Property", Storage:="_Property", DbType:="VarChar(400)")>  _
	Public Property [Property]() As String
		Get
			Return Me._Property
		End Get
		Set
			If (String.Equals(Me._Property, value) = false) Then
				Me.OnPropertyChanging(value)
				Me.SendPropertyChanging
				Me._Property = value
				Me.SendPropertyChanged("[Property]")
				Me.OnPropertyChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Value", DbType:="Text", UpdateCheck:=UpdateCheck.Never)>  _
	Public Property Value() As String
		Get
			Return Me._Value
		End Get
		Set
			If (String.Equals(Me._Value, value) = false) Then
				Me.OnValueChanging(value)
				Me.SendPropertyChanging
				Me._Value = value
				Me.SendPropertyChanged("Value")
				Me.OnValueChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_CreatedBy", DbType:="VarChar(50)")>  _
	Public Property CreatedBy() As String
		Get
			Return Me._CreatedBy
		End Get
		Set
			If (String.Equals(Me._CreatedBy, value) = false) Then
				Me.OnCreatedByChanging(value)
				Me.SendPropertyChanging
				Me._CreatedBy = value
				Me.SendPropertyChanged("CreatedBy")
				Me.OnCreatedByChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_IsGlobal", DbType:="Bit")>  _
	Public Property IsGlobal() As System.Nullable(Of Boolean)
		Get
			Return Me._IsGlobal
		End Get
		Set
			If (Me._IsGlobal.Equals(value) = false) Then
				Me.OnIsGlobalChanging(value)
				Me.SendPropertyChanging
				Me._IsGlobal = value
				Me.SendPropertyChanged("IsGlobal")
				Me.OnIsGlobalChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Method", DbType:="VarChar(50)")>  _
	Public Property Method() As String
		Get
			Return Me._Method
		End Get
		Set
			If (String.Equals(Me._Method, value) = false) Then
				Me.OnMethodChanging(value)
				Me.SendPropertyChanging
				Me._Method = value
				Me.SendPropertyChanged("Method")
				Me.OnMethodChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_CompanyID", DbType:="UniqueIdentifier")>  _
	Public Property CompanyID() As System.Nullable(Of System.Guid)
		Get
			Return Me._CompanyID
		End Get
		Set
			If (Me._CompanyID.Equals(value) = false) Then
				Me.OnCompanyIDChanging(value)
				Me.SendPropertyChanging
				Me._CompanyID = value
				Me.SendPropertyChanged("CompanyID")
				Me.OnCompanyIDChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Url", DbType:="VarChar(400)")>  _
	Public Property Url() As String
		Get
			Return Me._Url
		End Get
		Set
			If (String.Equals(Me._Url, value) = false) Then
				Me.OnUrlChanging(value)
				Me.SendPropertyChanging
				Me._Url = value
				Me.SendPropertyChanged("Url")
				Me.OnUrlChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Description", DbType:="VarChar(5000)")>  _
	Public Property Description() As String
		Get
			Return Me._Description
		End Get
		Set
			If (String.Equals(Me._Description, value) = false) Then
				Me.OnDescriptionChanging(value)
				Me.SendPropertyChanging
				Me._Description = value
				Me.SendPropertyChanged("Description")
				Me.OnDescriptionChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ActualValue", DbType:="Text", UpdateCheck:=UpdateCheck.Never)>  _
	Public Property ActualValue() As String
		Get
			Return Me._ActualValue
		End Get
		Set
			If (String.Equals(Me._ActualValue, value) = false) Then
				Me.OnActualValueChanging(value)
				Me.SendPropertyChanging
				Me._ActualValue = value
				Me.SendPropertyChanged("ActualValue")
				Me.OnActualValueChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ErrorMessagePath", DbType:="VarChar(400)")>  _
	Public Property ErrorMessagePath() As String
		Get
			Return Me._ErrorMessagePath
		End Get
		Set
			If (String.Equals(Me._ErrorMessagePath, value) = false) Then
				Me.OnErrorMessagePathChanging(value)
				Me.SendPropertyChanging
				Me._ErrorMessagePath = value
				Me.SendPropertyChanged("ErrorMessagePath")
				Me.OnErrorMessagePathChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ReturnParamName", DbType:="VarChar(100)")>  _
	Public Property ReturnParamName() As String
		Get
			Return Me._ReturnParamName
		End Get
		Set
			If (String.Equals(Me._ReturnParamName, value) = false) Then
				Me.OnReturnParamNameChanging(value)
				Me.SendPropertyChanging
				Me._ReturnParamName = value
				Me.SendPropertyChanged("ReturnParamName")
				Me.OnReturnParamNameChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ReturnParamPath", DbType:="VarChar(300)")>  _
	Public Property ReturnParamPath() As String
		Get
			Return Me._ReturnParamPath
		End Get
		Set
			If (String.Equals(Me._ReturnParamPath, value) = false) Then
				Me.OnReturnParamPathChanging(value)
				Me.SendPropertyChanging
				Me._ReturnParamPath = value
				Me.SendPropertyChanged("ReturnParamPath")
				Me.OnReturnParamPathChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_PauseFirst", DbType:="Bit")>  _
	Public Property PauseFirst() As System.Nullable(Of Boolean)
		Get
			Return Me._PauseFirst
		End Get
		Set
			If (Me._PauseFirst.Equals(value) = false) Then
				Me.OnPauseFirstChanging(value)
				Me.SendPropertyChanging
				Me._PauseFirst = value
				Me.SendPropertyChanged("PauseFirst")
				Me.OnPauseFirstChanged
			End If
		End Set
	End Property
	
	Public Event PropertyChanging As PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging
	
	Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
	
	Protected Overridable Sub SendPropertyChanging()
		If ((Me.PropertyChangingEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanging(Me, emptyChangingEventArgs)
		End If
	End Sub
	
	Protected Overridable Sub SendPropertyChanged(ByVal propertyName As [String])
		If ((Me.PropertyChangedEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End If
	End Sub
End Class