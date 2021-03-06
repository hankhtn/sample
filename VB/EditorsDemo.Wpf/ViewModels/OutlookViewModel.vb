Imports System
Imports System.Data
Imports DevExpress.Mvvm

Namespace EditorsDemo
    Public Class OutlookViewModel
        Inherits ViewModelBase

        Public Sub New()
            Initialize()
        End Sub

        Public Property FromDate() As Date?
            Get
                Return GetProperty(Function() FromDate)
            End Get
            Set(ByVal value? As Date)
                SetProperty(Function() FromDate, value)
                UpdateFilter()
            End Set
        End Property
        Public Property ToDate() As Date?
            Get
                Return GetProperty(Function() ToDate)
            End Get
            Set(ByVal value? As Date)
                SetProperty(Function() ToDate, value)
                UpdateFilter()
            End Set
        End Property
        Public Property FilterString() As String
            Get
                Return GetProperty(Function() FilterString)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() FilterString, value)
            End Set
        End Property
        Public Property Source() As DataTable
            Get
                Return GetProperty(Function() Source)
            End Get
            Private Set(ByVal value As DataTable)
                SetProperty(Function() Source, value)
            End Set
        End Property

        Private Sub Initialize()
            Source = OutlookDataGenerator.CreateOutlookDataTable(1000)
            Dim min As Date = Date.MaxValue, max As Date = Date.MinValue
            For Each row As DataRow In Source.Rows
                Dim [date] = DirectCast(row("Sent"), Date)
                If [date] < min Then
                    min = [date]
                End If
                If [date] > max Then
                    max = [date]
                End If
            Next row
            FromDate = min
            ToDate = max
        End Sub
        Private Sub UpdateFilter()
            If FromDate IsNot Nothing AndAlso ToDate IsNot Nothing AndAlso FromDate < ToDate Then
                FilterString = String.Format("([Sent] >= #{0}# AND [Sent] < #{1}#)", FromDate, ToDate)
            End If
        End Sub
    End Class
End Namespace
