Imports System.Collections.Generic
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo
    Partial Public Class IntervalGrouping
        Inherits PivotGridDemoModule

        Public Class GroupIntervalItem
            Public Property GroupInterval() As FieldGroupInterval
            Public Property Caption() As String
        End Class

        Public Sub New()
            InitializeComponent()
        End Sub

        Public Shared ReadOnly Property FieldGroupIntervals() As IEnumerable(Of Object)
            Get
                Return {
                    New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateMonth, .Caption = "Month"},
                    New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateQuarter, .Caption = "Quarter"},
                    New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateYear, .Caption = "Year"},
                    New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateMonthYear, .Caption = "Month-Year"},
                    New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateQuarterYear, .Caption = "Quarter-Year"},
                    New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateDayOfWeek, .Caption = "Day Of Week"},
                    New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateWeekOfMonth, .Caption = "Week Of Month"}
                }
            End Get
        End Property

        Private Sub pivotGrid_FieldValueDisplayText(ByVal sender As Object, ByVal e As PivotFieldDisplayTextEventArgs)
            If Object.ReferenceEquals(e.Field, fieldOrderDate) AndAlso e.Field.GroupInterval = FieldGroupInterval.DateQuarter Then
                e.DisplayText = String.Format("Qtr {0}", e.Value)
                If e.ValueType = FieldValueType.Total Then
                    e.DisplayText &= " Total"
                End If
            End If
        End Sub
    End Class
End Namespace
