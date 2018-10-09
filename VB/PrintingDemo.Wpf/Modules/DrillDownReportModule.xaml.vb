Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Input
Imports DevExpress.Xpf.Printing
Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm

Namespace PrintingDemo
    Partial Public Class DrillDownReportModule
        Inherits ModuleBase

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class DrillDownReportModuleViewModel
        Inherits ModuleViewModelBase


        Private currentPageNumber_Renamed As Integer

        Private previewClickCommand_Renamed As ICommand
        Private tempPageNumber As Integer
        Private context As NWindContext = NWindContext.Create()
        Public Class CategoryWrapper
            Public Property IsExpanded() As Boolean
            Private privateCategory As Category
            Public Property Category() As Category
                Get
                    Return privateCategory
                End Get
                Private Set(ByVal value As Category)
                    privateCategory = value
                End Set
            End Property

            Public Sub New(ByVal isExpanded As Boolean, ByVal category As Category)
                Me.IsExpanded = isExpanded
                Me.Category = category
            End Sub
        End Class

        Public Property CurrentPageNumber() As Integer
            Get
                Return currentPageNumber_Renamed
            End Get
            Set(ByVal value As Integer)
                SetProperty(currentPageNumber_Renamed, value, Function() CurrentPageNumber)
            End Set
        End Property

        Private Property CategoryWrappers() As Dictionary(Of Long, CategoryWrapper)

        Public ReadOnly Property PreviewClickCommand() As ICommand
            Get
                If previewClickCommand_Renamed IsNot Nothing Then
                    Return previewClickCommand_Renamed
                Else
                    previewClickCommand_Renamed = New DelegateCommand(Of DocumentPreviewMouseEventArgs)(AddressOf OnPreviewMouseClick)
                    Return previewClickCommand_Renamed
                End If
            End Get
        End Property

        Public Sub New()
            FillCategoryWrappers()
        End Sub

        Protected Overrides Function CreateLink() As TemplatedLink

            Dim link_Renamed As New SimpleLink()
            link_Renamed.DetailTemplate = DetailTemplate
            link_Renamed.DocumentName = "Drill-Down"
            link_Renamed.DetailCount = CategoryWrappers.Count
            AddHandler link_Renamed.CreateDetail, AddressOf link_CreateDetail
            Return link_Renamed
        End Function

        Private Sub FillCategoryWrappers()
            CategoryWrappers = New Dictionary(Of Long, CategoryWrapper)()
            For Each category In context.Categories.ToList()
                CategoryWrappers.Add(category.CategoryID, New CategoryWrapper(False, category))
            Next category
        End Sub

        Private Sub link_CreateDetail(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
            e.Data = CategoryWrappers(CategoryWrappers.ElementAt(e.DetailIndex).Value.Category.CategoryID)
        End Sub

        Private Sub link_CreateDocumentFinished(ByVal sender As Object, ByVal e As EventArgs)
            RemoveHandler Link.CreateDocumentFinished, AddressOf link_CreateDocumentFinished
            CurrentPageNumber = tempPageNumber
        End Sub

        Private Sub OnPreviewMouseClick(ByVal args As DocumentPreviewMouseEventArgs)
            Dim categoryID As Integer = Nothing
            If Integer.TryParse(String.Format("{0}", args.ElementTag), categoryID) Then
                CategoryWrappers(categoryID).IsExpanded = Not CategoryWrappers(categoryID).IsExpanded
                tempPageNumber = CurrentPageNumber
                AddHandler Link.CreateDocumentFinished, AddressOf link_CreateDocumentFinished
                Link.CreateDocument(True)
            End If
        End Sub
    End Class
End Namespace
