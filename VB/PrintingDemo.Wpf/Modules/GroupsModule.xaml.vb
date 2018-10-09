Imports System.Windows
Imports System.Windows.Data
Imports System.Linq
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Printing
Imports DevExpress.XtraPrinting.DataNodes
Imports DevExpress.DemoData.Models

Namespace PrintingDemo
    Partial Public Class GroupsModule
        Inherits ModuleBase

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class GroupsViewModel
        Inherits ModuleViewModelBase


        Private keepTogether_Renamed As Boolean

        Private repeatHeaderEveryPage_Renamed As Boolean

        Private pageBreakAfter_Renamed As Boolean

        Private isKeepTogetherEnabled_Renamed As Boolean = True

        Private isRepeatHeaderEveryPageEnabled_Renamed As Boolean = True

        Private isPageBreakAfterEnabled_Renamed As Boolean = True
        Private context As NWindContext = NWindContext.Create()

        Public Property KeepTogether() As Boolean
            Get
                Return keepTogether_Renamed
            End Get
            Set(ByVal value As Boolean)
                If keepTogether_Renamed = value Then
                    Return
                End If
                keepTogether_Renamed = value
                RaisePropertyChanged("KeepTogether")
                CreateDocument()
            End Set
        End Property
        Public Property IsKeepTogetherEnabled() As Boolean
            Get
                Return isKeepTogetherEnabled_Renamed
            End Get
            Set(ByVal value As Boolean)
                If isKeepTogetherEnabled_Renamed = value Then
                    Return
                End If
                isKeepTogetherEnabled_Renamed = value
                RaisePropertyChanged("IsKeepTogetherEnabled")
            End Set
        End Property
        Public Property RepeatHeaderEveryPage() As Boolean
            Get
                Return repeatHeaderEveryPage_Renamed
            End Get
            Set(ByVal value As Boolean)
                If repeatHeaderEveryPage_Renamed = value Then
                    Return
                End If
                repeatHeaderEveryPage_Renamed = value
                RaisePropertyChanged("RepeatHeaderEveryPage")
                CreateDocument()
            End Set
        End Property
        Public Property IsRepeatHeaderEveryPageEnabled() As Boolean
            Get
                Return isRepeatHeaderEveryPageEnabled_Renamed
            End Get
            Set(ByVal value As Boolean)
                If isRepeatHeaderEveryPageEnabled_Renamed = value Then
                    Return
                End If
                isRepeatHeaderEveryPageEnabled_Renamed = value
                RaisePropertyChanged("IsRepeatHeaderEveryPageEnabled")
            End Set
        End Property
        Public Property PageBreakAfter() As Boolean
            Get
                Return pageBreakAfter_Renamed
            End Get
            Set(ByVal value As Boolean)
                If pageBreakAfter_Renamed = value Then
                    Return
                End If
                pageBreakAfter_Renamed = value
                RaisePropertyChanged("PageBreakAfter")
                CreateDocument()
            End Set
        End Property
        Public Property IsPageBreakAfterEnabled() As Boolean
            Get
                Return isPageBreakAfterEnabled_Renamed
            End Get
            Set(ByVal value As Boolean)
                If isPageBreakAfterEnabled_Renamed = value Then
                    Return
                End If
                isPageBreakAfterEnabled_Renamed = value
                RaisePropertyChanged("IsPageBreakAfterEnabled")
            End Set
        End Property
        Public Property GroupHeaderTemplate() As DataTemplate

        Protected Overrides Function CreateLink() As TemplatedLink

            Dim link_Renamed As New CollectionViewLink()
            link_Renamed.GroupInfos.Add(New GroupInfo(GroupHeaderTemplate))
            link_Renamed.ReportHeaderTemplate = ReportHeaderTemplate
            link_Renamed.DetailTemplate = DetailTemplate
            link_Renamed.PageFooterTemplate = PageFooterTemplate
            link_Renamed.CollectionView = CreateCollectionViewSource().View
            link_Renamed.DocumentName = "Products by Categories"
            Return link_Renamed
        End Function
        Protected Overrides Sub ProcessLink(ByVal link As TemplatedLink)
            IsRepeatHeaderEveryPageEnabled = Not(PageBreakAfter OrElse KeepTogether)
            IsPageBreakAfterEnabled = Not RepeatHeaderEveryPage
            IsKeepTogetherEnabled = Not RepeatHeaderEveryPage
            Dim collectionViewLink As CollectionViewLink = CType(link, CollectionViewLink)
            collectionViewLink.GroupInfos(0).Union = If(KeepTogether, GroupUnion.WholePage, GroupUnion.None)
            collectionViewLink.GroupInfos(0).RepeatHeaderEveryPage = RepeatHeaderEveryPage
            collectionViewLink.GroupInfos(0).PageBreakAfter = PageBreakAfter
        End Sub
        Private Class ProductForPrinting
            Public Property CategoryName() As String
            Public Property CategoryID() As Long?
            Public Property ProductName() As String
            Public Property QuantityPerUnit() As String
            Public Property UnitPrice() As Decimal?
        End Class
        Private Function CreateCollectionViewSource() As CollectionViewSource
            Dim source As CollectionViewSource = New CollectionViewSource With {.Source = context.Products.Join(context.Categories, Function(p) p.CategoryID, Function(c) c.CategoryID, Function(ps, cs) New ProductForPrinting With {.CategoryName = cs.CategoryName, .CategoryID = ps.CategoryID, .ProductName = ps.ProductName, .QuantityPerUnit = ps.QuantityPerUnit, .UnitPrice = ps.UnitPrice}).ToList()}
            source.View.GroupDescriptions.Add(New PropertyGroupDescription("CategoryName"))
            Return source
        End Function
    End Class
End Namespace
