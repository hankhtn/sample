Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Charts

Namespace CommonChartsDemo
    Public Class AggregateTypeToAggregateFunctionConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim aType As EnumMemberInfo = TryCast(value, EnumMemberInfo)
            If aType Is Nothing Then
                Return AggregateFunction.None
            End If
            Return CType(Convert.ToInt32(aType.Id), AggregateFunction)
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class
End Namespace
