Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataModel
Imports DevExpress.Xpf.Core

Namespace DevExpress.DevAV.ViewModels
    Public Class MailMergeViewModel(Of TEntity As Class, TLinks As Class)
        Implements IDocumentContent


        Public Shared Function Create(Of TUnitOfWork As IUnitOfWork, TPrimaryKey As Structure)(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TEntity, TPrimaryKey)), ByVal key? As TPrimaryKey, Optional ByVal selectedTemplateName As String = Nothing, Optional ByVal linksViewModel_Renamed As TLinks = Nothing) As MailMergeViewModel(Of TEntity, TLinks)
            Dim repository = getRepositoryFunc(unitOfWorkFactory.CreateUnitOfWork())

            Dim entities_Renamed = repository.ToArray()

            Dim selectedEntity_Renamed = If(key IsNot Nothing, repository.Find(key.Value), Nothing)
            Return ViewModelSource.Create(Function() New MailMergeViewModel(Of TEntity, TLinks)(entities_Renamed, selectedEntity_Renamed, selectedTemplateName, linksViewModel_Renamed))
        End Function


        Protected Sub New(ByVal entities As IEnumerable(Of TEntity), ByVal selectedEntity As TEntity, ByVal selectedTemplateName As String, ByVal linksViewModel_Renamed As TLinks)
            Templates = MailMergeTemplatesHelper.GetAllTemplates()
            SelectedTemplate = Templates.FirstOrDefault(Function(t) t.Name = selectedTemplateName)
            IsAdditionParametersVisible = SelectedTemplate Is Nothing
            SelectedTemplate = If(SelectedTemplate, Templates.FirstOrDefault())
            Me.LinksViewModel = linksViewModel_Renamed

            Me.Entities = entities
            Me.SelectedEntity = selectedEntity
            Xpf.DemoBase.Helpers.Logger.Log(String.Format("HybridApp: View Quick Letter: {0}", selectedTemplateName))
        End Sub
        Private locker As New Locker()
        Public Overridable Property SelectedEntity() As TEntity
        Public Overridable Property ActiveRecord() As Integer
        Public Overridable Property Entities() As IEnumerable(Of TEntity)

        Public Overridable Property Templates() As List(Of TemplateViewModel)
        Public Overridable Property SelectedTemplate() As TemplateViewModel
        Public Overridable Property IsAdditionParametersVisible() As Boolean
        Public Overridable Property LinksViewModel() As TLinks
        Public Sub Close()
            If DocumentOwner IsNot Nothing Then
                DocumentOwner.Close(Me)
            End If
        End Sub
        Private privateDocumentOwner As IDocumentOwner
        Protected Property DocumentOwner() As IDocumentOwner
            Get
                Return privateDocumentOwner
            End Get
            Private Set(ByVal value As IDocumentOwner)
                privateDocumentOwner = value
            End Set
        End Property

        Public Sub OnSelectedEntityChanged()
            Me.locker.DoLockedActionIfNotLocked(Sub()
                If Entities IsNot Nothing Then
                    Dim index = Entities.Cast(Of Object)().Select(Function(x, i) New With {Key .item = x, Key .index = i}).FirstOrDefault(Function(x) x.item Is SelectedEntity)
                    ActiveRecord = If(index IsNot Nothing, index.index, -1)
                Else
                    ActiveRecord = -1
                End If
            End Sub)
        End Sub
        Public Sub OnActiveRecordChanged()
            Me.locker.DoLockedActionIfNotLocked(Sub()
                If Entities IsNot Nothing Then
                    Dim obj = Entities.Cast(Of Object)().Select(Function(x, i) New With {Key .item = x, Key .index = i}).FirstOrDefault(Function(x) x.index = ActiveRecord)
                    SelectedEntity = If(obj IsNot Nothing, TryCast(obj.item, TEntity), Nothing)
                Else
                    SelectedEntity = Nothing
                End If
            End Sub)
        End Sub
        Public Sub OnSelectedTemplateChanged()

            Dim activeRecord_Renamed As Integer = ActiveRecord
            ActiveRecord = -1
            ActiveRecord = activeRecord_Renamed
        End Sub

        #Region "IDocumentContent"
        Private Sub IDocumentContent_OnClose(ByVal e As CancelEventArgs) Implements IDocumentContent.OnClose
        End Sub
        Private Sub IDocumentContent_OnDestroy() Implements IDocumentContent.OnDestroy
        End Sub
        Private Property IDocumentContent_DocumentOwner() As IDocumentOwner Implements IDocumentContent.DocumentOwner
            Get
                Return DocumentOwner
            End Get
            Set(ByVal value As IDocumentOwner)
                DocumentOwner = value
            End Set
        End Property
        Private ReadOnly Property IDocumentContent_Title() As Object Implements IDocumentContent.Title
            Get
                Return "DevAV - Mail Merge"
            End Get
        End Property
        #End Region
    End Class
End Namespace
