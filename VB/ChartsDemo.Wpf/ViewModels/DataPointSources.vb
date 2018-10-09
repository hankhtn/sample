Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports System.Text

Namespace ChartsDemo
    Public NotInheritable Class DataPointSources

        Private Sub New()
        End Sub
        #Region "Fields"


        Private Shared dataSource_Renamed As List(Of List(Of DataPoint))

        Private Shared dataSource2_Renamed As List(Of List(Of DataPoint))

        Private Shared pieDataSource_Renamed As List(Of List(Of DataPoint))

        Private Shared nestedDonutDataSource_Renamed As List(Of List(Of DataPoint))

        Private Shared barDataSource_Renamed As List(Of List(Of DataPoint))

        Private Shared barDataSource2_Renamed As List(Of List(Of DataPoint))

        Private Shared scatterDataSource_Renamed As List(Of List(Of DataPoint))

        Private Shared funnelDataSource_Renamed As List(Of List(Of DataPoint))

        Private Shared polarDataSource_Renamed As List(Of List(Of PolarDataPoint))

        Private Shared polarRangeDataSource_Renamed As List(Of List(Of PolarDataPoint))

        Private Shared bubbleDataSource_Renamed As List(Of List(Of DataPoint))

        Private Shared rangeDataSource_Renamed As List(Of List(Of DataPoint))

        Private Shared rangeDataSource2_Renamed As List(Of List(Of DataPoint))

        Private Shared financialDataSource_Renamed As List(Of DataTable)

        #End Region
        #Region "Properties"

        Public Shared ReadOnly Property DataSource() As List(Of List(Of DataPoint))
            Get
                If dataSource_Renamed IsNot Nothing Then
                    Return dataSource_Renamed
                Else
                    dataSource_Renamed = CreateDataSource()
                    Return dataSource_Renamed
                End If
            End Get
        End Property
        Public Shared ReadOnly Property DataSource2() As List(Of List(Of DataPoint))
            Get
                If dataSource2_Renamed IsNot Nothing Then
                    Return dataSource2_Renamed
                Else
                    dataSource2_Renamed = DataPointSources.DataSource.Take(1).ToList()
                    Return dataSource2_Renamed
                End If
            End Get
        End Property
        Public Shared ReadOnly Property PieDataSource() As List(Of List(Of DataPoint))
            Get
                If pieDataSource_Renamed IsNot Nothing Then
                    Return pieDataSource_Renamed
                Else
                    pieDataSource_Renamed = CreatePieDataSource().ToList()
                    Return pieDataSource_Renamed
                End If
            End Get
        End Property
        Public Shared ReadOnly Property NestedDonutDataSource() As List(Of List(Of DataPoint))
            Get
                If nestedDonutDataSource_Renamed IsNot Nothing Then
                    Return nestedDonutDataSource_Renamed
                Else
                    nestedDonutDataSource_Renamed = CreatePieDataSource().Concat(CreatePieDataSource()).ToList()
                    Return nestedDonutDataSource_Renamed
                End If
            End Get
        End Property
        Public Shared ReadOnly Property BarDataSource() As List(Of List(Of DataPoint))
            Get
                If barDataSource_Renamed IsNot Nothing Then
                    Return barDataSource_Renamed
                Else
                    barDataSource_Renamed = CreateBarDataSource()
                    Return barDataSource_Renamed
                End If
            End Get
        End Property
        Public Shared ReadOnly Property BarDataSource2() As List(Of List(Of DataPoint))
            Get
                If barDataSource2_Renamed IsNot Nothing Then
                    Return barDataSource2_Renamed
                Else
                    barDataSource2_Renamed = BarDataSource.Take(3).ToList()
                    Return barDataSource2_Renamed
                End If
            End Get
        End Property
        Public Shared ReadOnly Property ScatterDataSource() As List(Of List(Of DataPoint))
            Get
                If scatterDataSource_Renamed IsNot Nothing Then
                    Return scatterDataSource_Renamed
                Else
                    scatterDataSource_Renamed = CreateScatterDataSource().ToList()
                    Return scatterDataSource_Renamed
                End If
            End Get
        End Property
        Public Shared ReadOnly Property FunnelDataSource() As List(Of List(Of DataPoint))
            Get
                If funnelDataSource_Renamed IsNot Nothing Then
                    Return funnelDataSource_Renamed
                Else
                    funnelDataSource_Renamed = CreateFunnelDataSource().ToList()
                    Return funnelDataSource_Renamed
                End If
            End Get
        End Property
        Public Shared ReadOnly Property PolarDataSource() As List(Of List(Of PolarDataPoint))
            Get
                If polarDataSource_Renamed IsNot Nothing Then
                    Return polarDataSource_Renamed
                Else
                    polarDataSource_Renamed = CreatePolarDataSource().ToList()
                    Return polarDataSource_Renamed
                End If
            End Get
        End Property
        Public Shared ReadOnly Property PolarRangeDataSource() As List(Of List(Of PolarDataPoint))
            Get
                If polarRangeDataSource_Renamed IsNot Nothing Then
                    Return polarRangeDataSource_Renamed
                Else
                    polarRangeDataSource_Renamed = CreatePolarRangeDataSource().ToList()
                    Return polarRangeDataSource_Renamed
                End If
            End Get
        End Property
        Public Shared ReadOnly Property BubbleDataSource() As List(Of List(Of DataPoint))
            Get
                If bubbleDataSource_Renamed IsNot Nothing Then
                    Return bubbleDataSource_Renamed
                Else
                    bubbleDataSource_Renamed = CreateBubbleDataSource().ToList()
                    Return bubbleDataSource_Renamed
                End If
            End Get
        End Property
        Public Shared ReadOnly Property RangeDataSource() As List(Of List(Of DataPoint))
            Get
                If rangeDataSource_Renamed IsNot Nothing Then
                    Return rangeDataSource_Renamed
                Else
                    rangeDataSource_Renamed = CreateRangeDataSource().ToList()
                    Return rangeDataSource_Renamed
                End If
            End Get
        End Property
        Public Shared ReadOnly Property RangeDataSource2() As List(Of List(Of DataPoint))
            Get
                If rangeDataSource2_Renamed IsNot Nothing Then
                    Return rangeDataSource2_Renamed
                Else
                    rangeDataSource2_Renamed = RangeDataSource.Take(1).ToList()
                    Return rangeDataSource2_Renamed
                End If
            End Get
        End Property
        Public Shared ReadOnly Property FinancialDataSource() As List(Of DataTable)
            Get
                If financialDataSource_Renamed IsNot Nothing Then
                    Return financialDataSource_Renamed
                Else
                    financialDataSource_Renamed = CreateFinancialDataSource().ToList()
                    Return financialDataSource_Renamed
                End If
            End Get
        End Property

        #End Region
        #Region "Methods"

        Private Shared Function CreateGroups(ByVal arguments() As String, ByVal groups As List(Of Double())) As List(Of List(Of DataPoint))
            Return groups.Select(Function(x) x.Zip(arguments, Function(value, argument) New DataPoint(argument:= argument, value:= value)).ToList()).ToList()
        End Function
        Private Shared Function CreateDataSource() As List(Of List(Of DataPoint))
            Dim args = { "A", "B", "C", "D", "E", "F" }
            Dim group0 = New Double() { 15, 13, 7, 5, 23, 21 }
            Dim group1 = New Double() { 8, 12, 4, 9, 15, 19 }
            Dim group2 = New Double() { 3, 10, 6, 6, 8, 10 }
            Return CreateGroups(args, New List(Of Double()) From {group0, group1, group2})
        End Function
        Private Shared Iterator Function CreatePieDataSource() As IEnumerable(Of List(Of DataPoint))

            Dim dataSource_Renamed = New List(Of DataPoint)()
            Dim random = New Random(0)
            For i As Integer = 0 To 15
                dataSource_Renamed.Add(New DataPoint("1", random.NextDouble() * 3 + 1))
            Next i
            Yield dataSource_Renamed
        End Function
        Private Shared Function CreateBarDataSource() As List(Of List(Of DataPoint))

            Dim dataSource_Renamed = New List(Of DataPoint)()
            Dim args = { "A", "B", "C", "D", "E", "F" }
            Dim group0 = New Double() { 1, 2, 5, -2, -2.1, -2.4 }
            Dim group1 = New Double() { 3, 10, 6, -3, -3.2, -3.8 }
            Dim group2 = New Double() { 8, 12, 7, -1.5, -1, -0.7 }
            Dim group3 = New Double() { 15, 13, 4, -1.3, -0.6, -4 }
            Return CreateGroups(args, New List(Of Double()) From {group0, group1, group2, group3})
        End Function
        Private Shared Iterator Function CreateScatterDataSource() As IEnumerable(Of List(Of DataPoint))
            Yield New List(Of DataPoint) From {
                New DataPoint("A", 15),
                New DataPoint("B", 11),
                New DataPoint("C", 7),
                New DataPoint("D", 9),
                New DataPoint("C", 23),
                New DataPoint("B", 21)
            }
        End Function
        Private Shared Iterator Function CreateFunnelDataSource() As IEnumerable(Of List(Of DataPoint))
            Yield New List(Of DataPoint) From {
                New DataPoint("Visited a Website", 9152),
                New DataPoint("Downloaded a Trial", 6870),
                New DataPoint("Contacted to Support", 5121),
                New DataPoint("Subscribed", 2224),
                New DataPoint("Renewed", 1670)
            }
        End Function
        Private Shared Iterator Function CreatePolarDataSource() As IEnumerable(Of List(Of PolarDataPoint))
            Dim random = New Random()
            Yield Enumerable.Range(0, 7).Select(Function(x) New PolarDataPoint(Math.Floor(random.NextDouble() * 360), Math.Floor(random.NextDouble() * 25))).ToList()
        End Function
        Private Shared Iterator Function CreatePolarRangeDataSource() As IEnumerable(Of List(Of PolarDataPoint))
            Dim random = New Random()
            Dim pointsCount = 6
            Yield Enumerable.Range(0, 7).Select(Function(x) New PolarDataPoint(Math.Floor(x / CDbl(pointsCount) * 360), Math.Floor(random.NextDouble() * 10 + 30), Math.Floor(random.NextDouble() * 10 + 10))).ToList()
        End Function
        Private Shared Iterator Function CreateBubbleDataSource() As IEnumerable(Of List(Of DataPoint))
            Yield New List(Of DataPoint) From {
                New DataPoint("A", value:= 10, weight:= 5.9),
                New DataPoint("B", value:= 5, weight:= 4.9),
                New DataPoint("C", value:= 2, weight:= 4.6),
                New DataPoint("D", value:= 5, weight:= 3),
                New DataPoint("E", value:= 2, weight:= 2.9),
                New DataPoint("F", value:= 4, weight:= 2.8),
                New DataPoint("G", value:= 2, weight:= 2.6),
                New DataPoint("H", value:= 3, weight:= 2.5),
                New DataPoint("I", value:= 4, weight:= 2.4)
            }
        End Function
        Private Shared Iterator Function CreateRangeDataSource() As IEnumerable(Of List(Of DataPoint))
            Yield New List(Of DataPoint) From {
                New DataPoint("A", 3, 13),
                New DataPoint("B", 5, 8),
                New DataPoint("C", 2, 9),
                New DataPoint("D", -2, -8),
                New DataPoint("E", -1, -6),
                New DataPoint("F", -3, -7)
            }
            Yield New List(Of DataPoint) From {
                New DataPoint("A", 5, 15),
                New DataPoint("B", 3, 11),
                New DataPoint("C", 6, 11),
                New DataPoint("D", -1, -9),
                New DataPoint("E", -3, -9),
                New DataPoint("F", -2, -6)
            }
        End Function
        Private Shared Iterator Function CreateFinancialDataSource() As IEnumerable(Of DataTable)
            Yield (New GoogleStockData()).ShortData
        End Function

        #End Region
    End Class
    Public Class DataPoint
        Public Property Argument() As String
        Public Property Value() As Double
        Public Property Value2() As Double
        Public Property Weight() As Double

        Public Sub New(ByVal argument As String, ByVal value As Double, Optional ByVal value2 As Double = 0, Optional ByVal weight As Double = 0)
            Me.Argument = argument
            Me.Value = value
            Me.Value2 = value2
            Me.Weight = weight
        End Sub
    End Class
    Public Class PolarDataPoint
        Public Property Argument() As Double
        Public Property Value() As Double
        Public Property Value2() As Double

        Public Sub New(ByVal argument As Double, ByVal value As Double, Optional ByVal value2 As Double = 0)
            Me.Argument = argument
            Me.Value = value
            Me.Value2 = value2
        End Sub
    End Class

End Namespace
