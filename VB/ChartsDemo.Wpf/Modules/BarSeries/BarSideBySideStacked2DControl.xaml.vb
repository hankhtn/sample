Imports System.Windows
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/BarSeries/BarSideBySideStacked2DControl.xaml"), CodeFile("Modules/BarSeries/BarSideBySideStacked2DControl.xaml.(cs)")>
    Partial Public Class BarSideBySideStacked2DControl
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
        Private Sub lbGroupBy_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            GroupSeries()
            chart.Animate()
        End Sub
        Private Sub GroupSeries()
            For Each series As BarSideBySideStackedSeries2D In chart.Diagram.Series
                Dim genderAge As GenderAgeInfo = CType(series.Tag, GenderAgeInfo)
                series.StackedGroup = If(lbGroupBy.SelectedIndex = 0, genderAge.Gender, genderAge.Age)
                If CStr(series.StackedGroup) = "65 years and older" Then
                    series.StackedGroup = "65+ years"
                End If
            Next series
        End Sub
    End Class
End Namespace
