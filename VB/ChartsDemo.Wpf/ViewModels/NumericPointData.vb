Imports System
Imports System.Collections.Generic

Namespace ChartsDemo
    Public NotInheritable Class NumericPointData

        Private Sub New()
        End Sub

        Private Const pointCount As Integer = 100000
        Private Shared _data As List(Of NumericPoint)
        Public Shared ReadOnly Property Data() As List(Of NumericPoint)
            Get
                If _data IsNot Nothing Then
                    Return _data
                Else
                    _data = GetData()
                    Return _data
                End If
            End Get
        End Property

        Private Shared Function GetData() As List(Of NumericPoint)
            Dim value As Double = 0
            Dim argument As Double = 1
            Dim random As New Random()
            Dim points = New List(Of NumericPoint)()
            For i As Integer = 0 To pointCount - 1
                points.Add(New NumericPoint(argument, value))
                value += (random.NextDouble() * 10.0 - 5.0)
                argument += 1
            Next i
            Return points
        End Function
    End Class
    Public Class NumericPoint
        Private privateArgument As Double
        Public Property Argument() As Double
            Get
                Return privateArgument
            End Get
            Private Set(ByVal value As Double)
                privateArgument = value
            End Set
        End Property
        Private privateValue As Double
        Public Property Value() As Double
            Get
                Return privateValue
            End Get
            Private Set(ByVal value As Double)
                privateValue = value
            End Set
        End Property
        Private privateWeight As Double
        Public Property Weight() As Double
            Get
                Return privateWeight
            End Get
            Private Set(ByVal value As Double)
                privateWeight = value
            End Set
        End Property

        Public Sub New(ByVal argument As Double, ByVal value As Double, Optional ByVal weight As Double = 0)
            Me.Argument = argument
            Me.Value = value
        End Sub
    End Class
End Namespace
