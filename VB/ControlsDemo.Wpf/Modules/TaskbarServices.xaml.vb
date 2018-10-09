Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.DemoBase
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls

Namespace ControlsDemo
    <CodeFile("ViewModels/TaskbarServicesViewModel.(cs)"), CodeFile("Helpers/ImageNameConverter.(cs)")>
    Partial Public Class TaskbarServices
        Inherits DemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
        Protected Overrides Sub Clear()
            Try
                Dim disposableViewModel As IDisposable = TryCast(ViewHelper.GetViewModelFromView(Me), IDisposable)
                If disposableViewModel IsNot Nothing Then
                    disposableViewModel.Dispose()
                End If
            Finally
                MyBase.Clear()
            End Try
        End Sub
    End Class

    Public Class ReverseStackPanel
        Inherits Panel

        Protected Overrides Function MeasureOverride(ByVal availableSize As Size) As Size
            Dim childAvailableSize As New Size(availableSize.Width, Double.PositiveInfinity)
            Dim size As New Size()
            For Each child As UIElement In InternalChildren
                child.Measure(childAvailableSize)
                size.Width = Math.Max(size.Width, child.DesiredSize.Width)
                size.Height += child.DesiredSize.Height
            Next child
            Return size
        End Function
        Protected Overrides Function ArrangeOverride(ByVal finalSize As Size) As Size
            Dim y As Double = 0.0
            For Each child As UIElement In InternalChildren.Cast(Of UIElement)().Reverse()
                Dim childHeight As Double = child.DesiredSize.Height
                child.Arrange(New Rect(New Point(0.0, y), New Size(finalSize.Width, childHeight)))
                y += childHeight
            Next child
            Return finalSize
        End Function
    End Class
End Namespace
