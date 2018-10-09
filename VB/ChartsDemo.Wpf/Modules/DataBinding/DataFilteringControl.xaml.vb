Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Windows
Imports DevExpress.Utils.Filtering
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.LayoutControl

Namespace ChartsDemo
    <CodeFile("Modules/DataBinding/DataFilteringControl.xaml"), CodeFile("Modules/DataBinding/DataFilteringControl.xaml.(cs)")>
    Partial Public Class DataFilteringControl
        Inherits ChartsDemoModule

        Private Const SalesTickFrequency As Double = 0.42
        Private Const ChargesTickFrequency As Double = 0.08

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub

        Private Sub SetUpTrackBar(ByVal trackBar As TrackBarEdit, ByVal tickFrequency As Double)
            trackBar.SmallStep = tickFrequency
            trackBar.TickFrequency = tickFrequency
            trackBar.LargeStep = tickFrequency
        End Sub
        Private Sub DataLayoutControl_AutoGeneratingItem(ByVal sender As Object, ByVal e As DataLayoutControlAutoGeneratingItemEventArgs)
            Dim group As LayoutGroup = TryCast(e.Item.Content, LayoutGroup)
            If group IsNot Nothing Then
                BarManager.SetDXContextMenu(group, Nothing)
                Dim trackBar As TrackBarEdit = TryCast(group.Children(group.Children.Count - 1), TrackBarEdit)
                If trackBar IsNot Nothing Then
                    If e.PropertyName = "Sales" Then
                        SetUpTrackBar(trackBar, SalesTickFrequency)
                    ElseIf e.PropertyName = "Charges" Then
                        SetUpTrackBar(trackBar, ChargesTickFrequency)
                    End If
                End If
            End If
        End Sub
        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
            chart.AnimationMode = ChartAnimationMode.OnDataChanged
        End Sub
    End Class

    Public Class SalesFilteringViewModel
        <FilterRange("StartYear", "EndYear", EditorType := RangeUIEditorType.Range)>
        Public Property Year() As Integer
        <Display(Name := "Sales, millions of USD"), FilterRange("MinSales", "MaxSales")>
        Public Property Sales() As Decimal
        <FilterLookup("Companies")>
        Public Property Company() As String
        <Display(Name := "Charges, millions of USD"), FilterRange("MinCharges", "MaxCharges")>
        Public Property Charges() As Decimal
    End Class

    Public Class SalesDataViewModel
        Private privateCompanies As IEnumerable(Of String)
        Public Property Companies() As IEnumerable(Of String)
            Get
                Return privateCompanies
            End Get
            Private Set(ByVal value As IEnumerable(Of String))
                privateCompanies = value
            End Set
        End Property
        Private privateSales As List(Of DevAVDataItem)
        Public Property Sales() As List(Of DevAVDataItem)
            Get
                Return privateSales
            End Get
            Private Set(ByVal value As List(Of DevAVDataItem))
                privateSales = value
            End Set
        End Property
        Private privateMinSales As Decimal
        Public Property MinSales() As Decimal
            Get
                Return privateMinSales
            End Get
            Private Set(ByVal value As Decimal)
                privateMinSales = value
            End Set
        End Property
        Private privateMaxSales As Decimal
        Public Property MaxSales() As Decimal
            Get
                Return privateMaxSales
            End Get
            Private Set(ByVal value As Decimal)
                privateMaxSales = value
            End Set
        End Property
        Private privateMinCharges As Decimal
        Public Property MinCharges() As Decimal
            Get
                Return privateMinCharges
            End Get
            Private Set(ByVal value As Decimal)
                privateMinCharges = value
            End Set
        End Property
        Private privateMaxCharges As Decimal
        Public Property MaxCharges() As Decimal
            Get
                Return privateMaxCharges
            End Get
            Private Set(ByVal value As Decimal)
                privateMaxCharges = value
            End Set
        End Property
        Private privateStartYear As Integer
        Public Property StartYear() As Integer
            Get
                Return privateStartYear
            End Get
            Private Set(ByVal value As Integer)
                privateStartYear = value
            End Set
        End Property
        Private privateEndYear As Integer
        Public Property EndYear() As Integer
            Get
                Return privateEndYear
            End Get
            Private Set(ByVal value As Integer)
                privateEndYear = value
            End Set
        End Property

        Public Sub New()
            Me.Sales = DevAVBranchesSales.GetList()
            Dim years As IEnumerable(Of Integer) = Sales.Select(Function(x) x.Year).Distinct()
            Me.StartYear = years.Min()
            Me.EndYear = years.Max()
            Me.Companies = Sales.Select(Function(x) x.Company).Distinct()
            Me.MinSales = Sales.Min(Function(x) x.Sales)
            Me.MaxSales = Sales.Max(Function(x) x.Sales)
            Me.MinCharges = Sales.Min(Function(x) x.Charges)
            Me.MaxCharges = Sales.Max(Function(x) x.Charges)
        End Sub
    End Class
End Namespace
