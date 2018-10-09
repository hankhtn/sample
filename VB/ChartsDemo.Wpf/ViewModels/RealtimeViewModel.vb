Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.Linq
Imports System.Text
Imports System.Windows.Threading

Namespace ChartsDemo
    Public Class RealtimeViewModel
        Private Const UpdateInterval As Integer = 40


        Private ReadOnly dataSource_Renamed As New DataCollection()
        Private ReadOnly timer As New DispatcherTimer()
        Private ReadOnly random As New Random()
        Private value1 As Double = 10.0
        Private value2 As Double = -10.0
        Private inProcess As Boolean = True

        Public Property TimeInterval() As Integer
        Public Overridable Property IsTimerEnabled() As Boolean
        Public ReadOnly Property DataSource() As DataCollection
            Get
                Return dataSource_Renamed
            End Get
        End Property
        Public Overridable Property MinTime() As Date
        Public Overridable Property MaxTime() As Date

        Public Sub New()
            IsTimerEnabled = True
            TimeInterval = 10
            timer.Interval = TimeSpan.FromMilliseconds(UpdateInterval)
            AddHandler timer.Tick, Sub(_d, _e) OnTimerTick()
        End Sub

        Public Sub DisableProcess()
            inProcess = timer.IsEnabled
            IsTimerEnabled = False
        End Sub
        Public Sub RestoreProcess()
            IsTimerEnabled = inProcess
        End Sub
        Public Sub ToggleIsTimerEnabled()
            IsTimerEnabled = Not IsTimerEnabled
        End Sub

        Protected Sub OnIsTimerEnabledChanged()
            timer.IsEnabled = IsTimerEnabled
        End Sub
        Private Sub OnTimerTick()
            Dim argument As Date = Date.Now
            Dim minDate As Date = argument.AddSeconds(-TimeInterval)
            Dim itemsToInsert As IList(Of ProcessItem) = New List(Of ProcessItem)()
            For i As Integer = 0 To UpdateInterval - 1
                itemsToInsert.Add(New ProcessItem() With {.DateAndTime = argument, .Process1 = value1, .Process2 = value2})
                argument = argument.AddMilliseconds(1)
                value1 = CalculateNextValue(value1)
                value2 = CalculateNextValue(value2)
            Next i
            dataSource_Renamed.AddRange(itemsToInsert)
            dataSource_Renamed.RemoveRangeAt(0, dataSource_Renamed.TakeWhile(Function(item) item.DateAndTime < minDate).Count())

            MinTime = minDate
            MaxTime = argument
        End Sub
        Private Function CalculateNextValue(ByVal value As Double) As Double
            Return value + (random.NextDouble() * 10.0 - 5.0)
        End Function
    End Class

    Public Structure ProcessItem
        Public Property DateAndTime() As Date
        Public Property Process1() As Double
        Public Property Process2() As Double
    End Structure

    Public Class DataCollection
        Inherits ObservableCollection(Of ProcessItem)

        Public Sub AddRange(ByVal items As IList(Of ProcessItem))
            For Each item As ProcessItem In items
                Me.Items.Add(item)
            Next item
            OnCollectionChanged(New NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, DirectCast(items, IList), Me.Items.Count - items.Count))
        End Sub
        Public Sub RemoveRangeAt(ByVal startingIndex As Integer, ByVal count As Integer)
            Dim removedItems = New List(Of ProcessItem)(count)
            For i As Integer = 0 To count - 1
                removedItems.Add(Items(startingIndex))
                Items.RemoveAt(startingIndex)
            Next i
            If count > 0 Then
                OnCollectionChanged(New NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, DirectCast(removedItems, IList), startingIndex))
            End If
        End Sub
    End Class
End Namespace
