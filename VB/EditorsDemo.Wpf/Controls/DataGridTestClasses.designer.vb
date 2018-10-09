Imports System
Imports System.ComponentModel
Imports System.Linq.Expressions
Imports System.Linq
Imports System.Reflection
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Linq.Mapping
Imports System.Data.Linq

Namespace GridDemo.Controls


    <System.Data.Linq.Mapping.DatabaseAttribute(Name := "DXGridServerModeDB")>
    Partial Public Class DataGridTestClassesDataContext
        Inherits System.Data.Linq.DataContext

        Private Shared mappingSource As System.Data.Linq.Mapping.MappingSource = New AttributeMappingSource()

        #Region "Extensibility Method Definitions"
        Partial Private Sub OnCreated()
        End Sub
        Partial Private Sub InsertWpfServerSideGridTest(ByVal instance As WpfServerSideGridTest)
        End Sub
        Partial Private Sub UpdateWpfServerSideGridTest(ByVal instance As WpfServerSideGridTest)
        End Sub
        Partial Private Sub DeleteWpfServerSideGridTest(ByVal instance As WpfServerSideGridTest)
        End Sub
        #End Region

        Public Sub New(ByVal connection As String)
            MyBase.New(connection, mappingSource)
            OnCreated()
        End Sub

        Public Sub New(ByVal connection As System.Data.IDbConnection)
            MyBase.New(connection, mappingSource)
            OnCreated()
        End Sub

        Public Sub New(ByVal connection As String, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
            MyBase.New(connection, mappingSource)
            OnCreated()
        End Sub

        Public Sub New(ByVal connection As System.Data.IDbConnection, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
            MyBase.New(connection, mappingSource)
            OnCreated()
        End Sub

        Public ReadOnly Property WpfServerSideGridTests() As System.Data.Linq.Table(Of WpfServerSideGridTest)
            Get
                Return Me.GetTable(Of WpfServerSideGridTest)()
            End Get
        End Property
    End Class

    <Table(Name := "dbo.WpfServerSideGridTest")>
    Partial Public Class WpfServerSideGridTest
        Implements INotifyPropertyChanging, INotifyPropertyChanged

        Private Shared emptyChangingEventArgs As New PropertyChangingEventArgs(String.Empty)

        Private _OID As Integer

        Private _Subject As String

        Private _From As String

        Private _Sent? As Date

        Private _Size? As Long

        Private _HasAttachment? As Boolean

        #Region "Extensibility Method Definitions"
        Partial Private Sub OnLoaded()
        End Sub
        Partial Private Sub OnValidate(ByVal action As System.Data.Linq.ChangeAction)
        End Sub
        Partial Private Sub OnCreated()
        End Sub
        Partial Private Sub OnOIDChanging(ByVal value As Integer)
        End Sub
        Partial Private Sub OnOIDChanged()
        End Sub
        Partial Private Sub OnSubjectChanging(ByVal value As String)
        End Sub
        Partial Private Sub OnSubjectChanged()
        End Sub
        Partial Private Sub OnFromChanging(ByVal value As String)
        End Sub
        Partial Private Sub OnFromChanged()
        End Sub
        Partial Private Sub OnSentChanging(ByVal value? As Date)
        End Sub
        Partial Private Sub OnSentChanged()
        End Sub
        Partial Private Sub OnSizeChanging(ByVal value? As Long)
        End Sub
        Partial Private Sub OnSizeChanged()
        End Sub
        Partial Private Sub OnHasAttachmentChanging(ByVal value? As Boolean)
        End Sub
        Partial Private Sub OnHasAttachmentChanged()
        End Sub
        #End Region

        Public Sub New()
            OnCreated()
        End Sub

        <Column(Storage := "_OID", AutoSync := AutoSync.OnInsert, DbType := "Int NOT NULL IDENTITY", IsPrimaryKey := True, IsDbGenerated := True)>
        Public Property OID() As Integer
            Get
                Return Me._OID
            End Get
            Set(ByVal value As Integer)
                If (Me._OID <> value) Then
                    Me.OnOIDChanging(value)
                    Me.SendPropertyChanging()
                    Me._OID = value
                    Me.SendPropertyChanged("OID")
                    Me.OnOIDChanged()
                End If
            End Set
        End Property

        <Column(Storage := "_Subject", DbType := "NVarChar(100)")>
        Public Property Subject() As String
            Get
                Return Me._Subject
            End Get
            Set(ByVal value As String)
                If (Me._Subject <> value) Then
                    Me.OnSubjectChanging(value)
                    Me.SendPropertyChanging()
                    Me._Subject = value
                    Me.SendPropertyChanged("Subject")
                    Me.OnSubjectChanged()
                End If
            End Set
        End Property

        <Column(Name := "[From]", Storage := "_From", DbType := "NVarChar(100)")>
        Public Property [From]() As String
            Get
                Return Me._From
            End Get
            Set(ByVal value As String)
                If (Me._From <> value) Then
                    Me.OnFromChanging(value)
                    Me.SendPropertyChanging()
                    Me._From = value
                    Me.SendPropertyChanged("From")
                    Me.OnFromChanged()
                End If
            End Set
        End Property

        <Column(Storage := "_Sent", DbType := "DateTime")>
        Public Property Sent() As Date?
            Get
                Return Me._Sent
            End Get
            Set(ByVal value? As Date)
                If (Not Me._Sent.Equals(value)) Then
                    Me.OnSentChanging(value)
                    Me.SendPropertyChanging()
                    Me._Sent = value
                    Me.SendPropertyChanged("Sent")
                    Me.OnSentChanged()
                End If
            End Set
        End Property

        <Column(Storage := "_Size", DbType := "BigInt")>
        Public Property Size() As Long?
            Get
                Return Me._Size
            End Get
            Set(ByVal value? As Long)
                If (Not Me._Size.Equals(value)) Then
                    Me.OnSizeChanging(value)
                    Me.SendPropertyChanging()
                    Me._Size = value
                    Me.SendPropertyChanged("Size")
                    Me.OnSizeChanged()
                End If
            End Set
        End Property

        <Column(Storage := "_HasAttachment", DbType := "Bit")>
        Public Property HasAttachment() As Boolean?
            Get
                Return Me._HasAttachment
            End Get
            Set(ByVal value? As Boolean)
                If (Not Me._HasAttachment.Equals(value)) Then
                    Me.OnHasAttachmentChanging(value)
                    Me.SendPropertyChanging()
                    Me._HasAttachment = value
                    Me.SendPropertyChanged("HasAttachment")
                    Me.OnHasAttachmentChanged()
                End If
            End Set
        End Property

        Public Event PropertyChanging As PropertyChangingEventHandler Implements INotifyPropertyChanging.PropertyChanging

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Overridable Sub SendPropertyChanging()
            RaiseEvent PropertyChanging(Me, emptyChangingEventArgs)
        End Sub

        Protected Overridable Sub SendPropertyChanged(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class
End Namespace
