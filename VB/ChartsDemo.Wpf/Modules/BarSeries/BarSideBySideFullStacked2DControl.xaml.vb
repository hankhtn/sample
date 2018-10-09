Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/BarSeries/BarSideBySideFullStacked2DControl.xaml"), CodeFile("Modules/BarSeries/BarSideBySideFullStacked2DControl.xaml.(cs)")>
    Partial Public Class BarSideBySideFullStacked2DControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
            chart.DataSource = AgeStructure.GetDataByAgeAndGender()
            GroupSeries()
        End Sub

        Private Sub ChartsDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
        End Sub
        Private Sub chbPercent_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            For Each series As BarFullStackedSeries2D In chart.Diagram.Series
                series.Label.TextPattern = "{VP:P0}"
            Next series
        End Sub
        Private Sub chbPercent_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            For Each series As BarFullStackedSeries2D In chart.Diagram.Series
                series.Label.TextPattern = "{V:0,,.}M"
            Next series
        End Sub
        Private Sub lbGroupBy_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            GroupSeries()
            chart.Animate()
        End Sub
        Private Sub GroupSeries()
            For Each series As BarSideBySideFullStackedSeries2D In chart.Diagram.Series
                Dim genderAge As GenderAgeInfo = CType(series.Tag, GenderAgeInfo)
                series.StackedGroup = If(lbGroupBy.SelectedIndex = 0, genderAge.Gender, genderAge.Age)
            Next series
        End Sub
    End Class
End Namespace
