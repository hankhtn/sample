Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows.Media.Media3D
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo
    <CodeFile("Modules/XyzChart/DataTriangulation.xaml"), CodeFile("Modules/XyzChart/DataTriangulation.xaml.(cs)")>
    Partial Public Class DataTriangulation
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class

    Public NotInheritable Class DataTriangulationModel

        Private Sub New()
        End Sub
        Private Shared _points As List(Of Point3D)

        Public Shared ReadOnly Property Points() As List(Of Point3D)
            Get
                If _points IsNot Nothing Then
                    Return _points
                Else
                    _points = CreatePoints()
                    Return _points
                End If
            End Get
        End Property

        Private Shared Function CreatePoints() As List(Of Point3D)

            Dim points_Renamed As New List(Of Point3D)()
            For j As Double = -25.0 To 24 Step 0.75
                For i As Double = -15.0 To 14 Step 0.75
                    Dim x As Double = 2 + i + Math.Sin(j / 4.0) * 2
                    Dim y As Double = 1 + j + Math.Cos(i / 4.0)
                    Dim z As Double = 5.5 * Math.Sin(i / 3.0) * Math.Sin(j / 3.0)
                    points_Renamed.Add(New Point3D(x, y, z))
                Next i
            Next j
            Return points_Renamed
        End Function
    End Class
End Namespace
