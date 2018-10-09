Imports System
Imports System.Data.Objects
Imports System.Data.Objects.DataClasses
Imports System.Data.EntityClient
Imports System.ComponentModel
Imports System.Xml.Serialization
Imports System.Runtime.Serialization

<Assembly: EdmSchemaAttribute()>

Namespace GridDemo.Controls
    #Region "Contexts"




    Partial Public Class DXGridServerModeDBEntities
        Inherits ObjectContext

        #Region "Constructors"




        Public Sub New()
            MyBase.New("name=DXGridServerModeDBEntities", "DXGridServerModeDBEntities")
            Me.ContextOptions.LazyLoadingEnabled = True
            OnContextCreated()
        End Sub




        Public Sub New(ByVal connectionString As String)
            MyBase.New(connectionString, "DXGridServerModeDBEntities")
            Me.ContextOptions.LazyLoadingEnabled = True
            OnContextCreated()
        End Sub




        Public Sub New(ByVal connection As EntityConnection)
            MyBase.New(connection, "DXGridServerModeDBEntities")
            Me.ContextOptions.LazyLoadingEnabled = True
            OnContextCreated()
        End Sub

        #End Region

        #Region "Partial Methods"

        Partial Private Sub OnContextCreated()
        End Sub

        #End Region

        #Region "ObjectSet Properties"




        Public ReadOnly Property EFServerModeTestClasses() As ObjectSet(Of EFServerModeTestClass)
            Get
                If (_EFServerModeTestClasses Is Nothing) Then
                    _EFServerModeTestClasses = MyBase.CreateObjectSet(Of EFServerModeTestClass)("EFServerModeTestClasses")
                End If
                Return _EFServerModeTestClasses
            End Get
        End Property
        Private _EFServerModeTestClasses As ObjectSet(Of EFServerModeTestClass)

        #End Region
        #Region "AddTo Methods"





        Public Sub AddToEFServerModeTestClasses(ByVal eFServerModeTestClass_Renamed As EFServerModeTestClass)
            MyBase.AddObject("EFServerModeTestClasses", eFServerModeTestClass_Renamed)
        End Sub

        #End Region
    End Class


    #End Region

    #Region "Entities"




    <EdmEntityTypeAttribute(NamespaceName := "DXGridServerModeDBModel", Name := "EFServerModeTestClass"), Serializable(), DataContractAttribute(IsReference := True)>
    Partial Public Class EFServerModeTestClass
        Inherits EntityObject

        #Region "Factory Method"





        Public Shared Function CreateEFServerModeTestClass(ByVal oID As Global.System.Int32) As EFServerModeTestClass

            Dim eFServerModeTestClass_Renamed As New EFServerModeTestClass()
            eFServerModeTestClass_Renamed.OID = oID
            Return eFServerModeTestClass_Renamed
        End Function

        #End Region
        #Region "Primitive Properties"




        <EdmScalarPropertyAttribute(EntityKeyProperty := True, IsNullable := False), DataMemberAttribute()>
        Public Property OID() As Global.System.Int32
            Get
                Return _OID
            End Get
            Set(ByVal value As System.Int32)
                If _OID <> value Then
                    OnOIDChanging(value)
                    ReportPropertyChanging("OID")
                    _OID = StructuralObject.SetValidValue(value)
                    ReportPropertyChanged("OID")
                    OnOIDChanged()
                End If
            End Set
        End Property
        Private _OID As Global.System.Int32
        Partial Private Sub OnOIDChanging(ByVal value As Global.System.Int32)
        End Sub
        Partial Private Sub OnOIDChanged()
        End Sub




        <EdmScalarPropertyAttribute(EntityKeyProperty := False, IsNullable := True), DataMemberAttribute()>
        Public Property Subject() As Global.System.String
            Get
                Return _Subject
            End Get
            Set(ByVal value As System.String)
                OnSubjectChanging(value)
                ReportPropertyChanging("Subject")
                _Subject = StructuralObject.SetValidValue(value, True)
                ReportPropertyChanged("Subject")
                OnSubjectChanged()
            End Set
        End Property
        Private _Subject As Global.System.String
        Partial Private Sub OnSubjectChanging(ByVal value As Global.System.String)
        End Sub
        Partial Private Sub OnSubjectChanged()
        End Sub




        <EdmScalarPropertyAttribute(EntityKeyProperty := False, IsNullable := True), DataMemberAttribute()>
        Public Property [From]() As Global.System.String
            Get
                Return _From
            End Get
            Set(ByVal value As System.String)
                OnFromChanging(value)
                ReportPropertyChanging("From")
                _From = StructuralObject.SetValidValue(value, True)
                ReportPropertyChanged("From")
                OnFromChanged()
            End Set
        End Property
        Private _From As Global.System.String
        Partial Private Sub OnFromChanging(ByVal value As Global.System.String)
        End Sub
        Partial Private Sub OnFromChanged()
        End Sub




        <EdmScalarPropertyAttribute(EntityKeyProperty := False, IsNullable := True), DataMemberAttribute()>
        Public Property Sent() As Global.System.DateTime?
            Get
                Return _Sent
            End Get
            Set(ByVal value? As Date)
                OnSentChanging(value)
                ReportPropertyChanging("Sent")
                _Sent = StructuralObject.SetValidValue(value)
                ReportPropertyChanged("Sent")
                OnSentChanged()
            End Set
        End Property
        Private _Sent? As Global.System.DateTime
        Partial Private Sub OnSentChanging(ByVal value? As Global.System.DateTime)
        End Sub
        Partial Private Sub OnSentChanged()
        End Sub




        <EdmScalarPropertyAttribute(EntityKeyProperty := False, IsNullable := True), DataMemberAttribute()>
        Public Property Size() As Global.System.Int64?
            Get
                Return _Size
            End Get
            Set(ByVal value? As System.Int64)
                OnSizeChanging(value)
                ReportPropertyChanging("Size")
                _Size = StructuralObject.SetValidValue(value)
                ReportPropertyChanged("Size")
                OnSizeChanged()
            End Set
        End Property
        Private _Size? As Global.System.Int64
        Partial Private Sub OnSizeChanging(ByVal value? As Global.System.Int64)
        End Sub
        Partial Private Sub OnSizeChanged()
        End Sub




        <EdmScalarPropertyAttribute(EntityKeyProperty := False, IsNullable := True), DataMemberAttribute()>
        Public Property HasAttachment() As Global.System.Boolean?
            Get
                Return _HasAttachment
            End Get
            Set(ByVal value? As System.Boolean)
                OnHasAttachmentChanging(value)
                ReportPropertyChanging("HasAttachment")
                _HasAttachment = StructuralObject.SetValidValue(value)
                ReportPropertyChanged("HasAttachment")
                OnHasAttachmentChanged()
            End Set
        End Property
        Private _HasAttachment? As Global.System.Boolean
        Partial Private Sub OnHasAttachmentChanging(ByVal value? As Global.System.Boolean)
        End Sub
        Partial Private Sub OnHasAttachmentChanged()
        End Sub

        #End Region

    End Class

    #End Region

End Namespace
