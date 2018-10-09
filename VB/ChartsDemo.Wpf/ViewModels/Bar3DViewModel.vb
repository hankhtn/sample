Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Media.Media3D

Namespace ChartsDemo
    Public Class Bar3DViewModel
        Private Const pointsCount As Integer = 300
        Private Const maxValue As Integer = 200
        Public Overridable Property Source() As List(Of Point3D)

        Public Sub New()
            GenerateData()
        End Sub

        Public Sub GenerateData()
            Source = GenerateRandomData()
        End Sub

        Private Shared Function GenerateRandomData() As List(Of Point3D)
            Dim rand = New Random()
            Dim data = New List(Of Point3D)()
            For i As Integer = 0 To pointsCount - 1
                Dim point As Point3D
                Do
                    Dim x As Double = rand.NextDouble() * maxValue * 2
                    Dim y As Double = rand.NextDouble() * maxValue * 2
                    Dim z As Double = rand.NextDouble() * maxValue
                    point = New Point3D(x, y, z)
                Loop While data.Contains(point, New Point3DComparer(1.0))
                data.Add(point)
            Next i
            Return data
        End Function
    End Class
End Namespace
