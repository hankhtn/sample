Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data
Imports DevExpress.Xpf.Charts.RangeControlClient
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors

Namespace ChartsDemo
    <CodeFile("Modules/Interactivity/ChartClientForRangeControlModule.xaml"), CodeFile("Modules/Interactivity/ChartClientForRangeControlModule.xaml.(cs)"), CodeFile("ViewModels/ChartClientModel.(cs)")>
    Partial Public Class ChartClientForRangeControlModule
        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Enum ChartViewType
        Area
        Bar
        Line
    End Enum

    Public Class ChartViewTypeConverter
        Implements IValueConverter

        Private Function Parse(ByVal type As ChartViewType) As RangeControlClientView
            If type = ChartViewType.Area Then
                Return New RangeControlClientAreaView()
            End If
            If type = ChartViewType.Bar Then
                Return New RangeControlClientBarView()
            End If
            If type = ChartViewType.Line Then
                Return New RangeControlClientLineView()
            End If
            Return Nothing
        End Function
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is ChartViewType Then
                Return Parse(DirectCast(value, ChartViewType))
            End If
            Return Nothing
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
