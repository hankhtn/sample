Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data
Imports DevExpress.Xpf.Charts.RangeControlClient

Namespace ChartsDemo
    Public Class ChartClientModel
        Private Const itemCount As Integer = 100
        Private privateNumericItemsSource As Object
        Public Property NumericItemsSource() As Object
            Get
                Return privateNumericItemsSource
            End Get
            Private Set(ByVal value As Object)
                privateNumericItemsSource = value
            End Set
        End Property
        Private privateDateTimeItemsSource As Object
        Public Property DateTimeItemsSource() As Object
            Get
                Return privateDateTimeItemsSource
            End Get
            Private Set(ByVal value As Object)
                privateDateTimeItemsSource = value
            End Set
        End Property
        Public Overridable Property MinimumGridSpacing() As Double
        Public Overridable Property MiddleGridSpacing() As Double
        Public Overridable Property MaximumGridSpacing() As Double
        Public Overridable Property GridSpacingVisibility() As Visibility
        Public Overridable Property GridAlignment() As DateTimeGridAlignment

        Public Sub New()
            NumericItemsSource = GenerateNumericData()
            DateTimeItemsSource = GenerateDateTimeData()
            GridAlignment = DateTimeGridAlignment.Auto
        End Sub

        Protected Sub OnGridAlignmentChanged()
            GridSpacingVisibility = If(GridAlignment.Equals(DateTimeGridAlignment.Auto), Visibility.Collapsed, Visibility.Visible)
            MinimumGridSpacing = GetMinimumGridSpacing(GridAlignment)
            MaximumGridSpacing = GetMaximumGridSpacing(GridAlignment)
            MiddleGridSpacing = (MinimumGridSpacing + MaximumGridSpacing) / 2
        End Sub
        #Region "Static"

        Private Shared Function GenerateNumericData() As Double()
            Dim data(itemCount - 1) As Double
            Dim random As New Random()
            Dim value As Double = 0
            For i As Integer = 0 To itemCount - 1
                value += (random.NextDouble() - 0.5)
                data(i) = Math.Abs(value)
            Next i
            Return data
        End Function
        Private Shared Function GenerateDateTimeData() As List(Of DateTimeItem)
            Dim data As New List(Of DateTimeItem)()
            Dim now As Date = Date.Now.Date
            Dim random As New Random()
            Dim value As Double = 0
            For i As Integer = 0 To itemCount - 1
                now = now.AddDays(1)
                value += (random.NextDouble() - 0.5)
                data.Add(New DateTimeItem() With {.Argument = now, .Value = Math.Abs(value + Math.Sin(i * 0.6))})
            Next i
            Return data
        End Function
        Private Shared Function GetMaximumGridSpacing(ByVal gridAlignment As DateTimeGridAlignment) As Double
            Select Case gridAlignment
                Case DateTimeGridAlignment.Day
                    Return 15
                Case DateTimeGridAlignment.Month
                    Return 3
                Case DateTimeGridAlignment.Week
                    Return 6
                Case Else
                    Return 5
            End Select
        End Function
        Private Shared Function GetMinimumGridSpacing(ByVal gridAlignment As DateTimeGridAlignment) As Double
            Select Case gridAlignment
                Case DateTimeGridAlignment.Day
                    Return 5
                Case DateTimeGridAlignment.Month
                    Return 1
                Case DateTimeGridAlignment.Week
                    Return 2
                Case Else
                    Return 1
            End Select
        End Function

        #End Region
    End Class
    Public Class DateTimeItem
        Public Property Argument() As Date
        Public Property Value() As Double
    End Class
End Namespace
