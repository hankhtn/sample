Imports System
Imports System.Collections.ObjectModel
Imports System.Windows
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Printing
Imports ColumnLayout = DevExpress.XtraPrinting.ColumnLayout
Imports DevExpress.DemoData.Models
Imports System.Linq
Imports System.Collections.Generic

Namespace PrintingDemo
    Public Enum PageNumberLocation
        TopMargin
        BottomMargin
    End Enum

    Partial Public Class MultiColumnModule
        Inherits ModuleBase

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class MultiColumnViewModel
        Inherits ModuleViewModelBase


        Private Shared ReadOnly pageNumberAlignmentValues_Renamed As New ReadOnlyCollection(Of HorizontalAlignment)(New HorizontalAlignment() { HorizontalAlignment.Left, HorizontalAlignment.Center, HorizontalAlignment.Right })


        Private pageNumberKind_Renamed As PageNumberKind = PageNumberKind.NumberOfTotal

        Private pageNumberFormat_Renamed As String

        Private pageNumberLocation_Renamed As PageNumberLocation

        Private pageNumberAlignment_Renamed As HorizontalAlignment

        Private isAcrossThenDownChecked_Renamed As Boolean = True
        Private customers As List(Of Customer)
        Private context As NWindContext = NWindContext.Create()

        Public Property PageNumberTemplate() As DataTemplate
        Public Property PageNumberKind() As PageNumberKind
            Get
                Return pageNumberKind_Renamed
            End Get
            Set(ByVal value As PageNumberKind)
                If pageNumberKind_Renamed.Equals(value) Then
                    Return
                End If
                pageNumberKind_Renamed = value
                PageNumberFormat = GetDefaultFormatString()
                CreateDocument()
                RaisePropertyChanged("PageNumberKind")
            End Set
        End Property
        Public Property PageNumberFormat() As String
            Get
                Return pageNumberFormat_Renamed
            End Get
            Set(ByVal value As String)
                If pageNumberFormat_Renamed = value Then
                    Return
                End If
                pageNumberFormat_Renamed = value
                CreateDocument()
                RaisePropertyChanged("PageNumberFormat")
            End Set
        End Property
        Public Property PageNumberLocation() As PageNumberLocation
            Get
                Return pageNumberLocation_Renamed
            End Get
            Set(ByVal value As PageNumberLocation)
                If pageNumberLocation_Renamed = value Then
                    Return
                End If
                pageNumberLocation_Renamed = value
                CreateDocument()
                RaisePropertyChanged("PageNumberLocation")
            End Set
        End Property
        Public Property PageNumberAlignment() As HorizontalAlignment
            Get
                Return pageNumberAlignment_Renamed
            End Get
            Set(ByVal value As HorizontalAlignment)
                If Object.Equals(pageNumberAlignment_Renamed, value) Then
                    Return
                End If
                pageNumberAlignment_Renamed = value
                CreateDocument()
                RaisePropertyChanged("PageNumberAlignment")
            End Set
        End Property
        Public Property IsAcrossThenDownChecked() As Boolean
            Get
                Return isAcrossThenDownChecked_Renamed
            End Get
            Set(ByVal value As Boolean)
                If isAcrossThenDownChecked_Renamed = value Then
                    Return
                End If
                isAcrossThenDownChecked_Renamed = value
                CreateDocument()
                RaisePropertyChanged("IsAcrossThenDownChecked")
            End Set
        End Property
        Public Shared ReadOnly Property PageNumberLocationValues() As Array
            Get
                Return System.Enum.GetValues(GetType(PageNumberLocation))
            End Get
        End Property
        Public Shared ReadOnly Property PageNumberKindValues() As Array
            Get
                Return System.Enum.GetValues(GetType(PageNumberKind))
            End Get
        End Property
        Public Shared ReadOnly Property PageNumberAlignmentValues() As ReadOnlyCollection(Of HorizontalAlignment)
            Get
                Return pageNumberAlignmentValues_Renamed
            End Get
        End Property

        Public Sub New()
            pageNumberFormat_Renamed = GetDefaultFormatString()
            customers = context.Customers.ToList()
        End Sub

        Protected Overrides Function CreateLink() As TemplatedLink

            Dim link_Renamed As New SimpleLink()
            link_Renamed.ColumnWidth = 198
            link_Renamed.ReportHeaderTemplate = ReportHeaderTemplate
            link_Renamed.DetailTemplate = DetailTemplate
            link_Renamed.DetailCount = context.Customers.Count()
            link_Renamed.DocumentName = "Multi-Column"
            AddHandler link_Renamed.CreateDetail, AddressOf link_CreateDetail
            Return link_Renamed
        End Function
        Protected Overrides Sub ProcessLink(ByVal link As TemplatedLink)
            CType(link, SimpleLink).ColumnLayout = GetColumnLayout()
            ClearPageHeaderFooter()
            Dim template As DataTemplate = PageNumberTemplate
            Select Case PageNumberLocation
                Case PrintingDemo.PageNumberLocation.TopMargin
                    link.TopMarginTemplate = template
                    link.TopMarginData = GetPageNumberDataContext()
                Case PrintingDemo.PageNumberLocation.BottomMargin
                    link.BottomMarginTemplate = template
                    link.BottomMarginData = GetPageNumberDataContext()
            End Select
        End Sub
        Private Sub link_CreateDetail(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
            e.Data = customers(e.DetailIndex)
        End Sub
        Private Function GetColumnLayout() As ColumnLayout
            Return If(IsAcrossThenDownChecked, ColumnLayout.AcrossThenDown, ColumnLayout.DownThenAcross)
        End Function
        Private Function GetPageNumberDataContext() As Object
            Return New PageNumberDataContext(PageNumberKind, PageNumberFormat, PageNumberAlignment)
        End Function
        Private Sub ClearPageHeaderFooter()
            Link.TopMarginTemplate = Nothing
            Link.BottomMarginTemplate = Nothing
            Link.TopMarginData = Nothing
            Link.BottomMarginData = Nothing
        End Sub
        Private Function GetDefaultFormatString() As String
            Select Case PageNumberKind
                Case PageNumberKind.Number
                    Return "Page {0}"
                Case PageNumberKind.NumberOfTotal
                    Return "Page {0} of {1}"
                Case PageNumberKind.RomanHiNumber
                    Return "- {0} -"
                Case PageNumberKind.RomanLowNumber
                    Return "{0}"
            End Select
            Return String.Empty
        End Function
    End Class
End Namespace
