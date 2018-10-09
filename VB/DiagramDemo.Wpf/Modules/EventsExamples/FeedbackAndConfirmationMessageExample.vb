Imports System
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Diagram.Core
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo
    Public NotInheritable Class FeedbackAndConfirmationMessageExample

        Private Sub New()
        End Sub

        Public Shared Function Run() As FrameworkElement
            Dim panel = New Grid()
            Dim diagram = New DiagramControl() With {.PageSize = New Size(400, 300)}
            panel.Children.Add(diagram)
            AddHandler diagram.Loaded, Sub(s, e) diagram.FitToPage()
            Dim label = New TextBlock() With {.Margin = New Thickness(30), .IsHitTestVisible = False, .FontSize = 24}
            panel.Children.Add(label)
            ' Code Example Start
            ' Each time the end-user tries to move a diagram item, the ItemsMoving event is raised.
            ' The following implementation displays a label as the end-user is moving an item and invokes the confirmation dialog window prompting the user to confirm the action.

            AddHandler diagram.ItemsMoving, Sub(sender, e)
                Select Case e.Stage
                Case DiagramActionStage.Start
                    label.Text = "Moving..."
                Case DiagramActionStage.Canceled
                CaseLabel1:
                    label.Text = ""
                Case DiagramActionStage.Finished
                    If DXMessageBox.Show(diagram, "Confirm the moving action.", "Confirmation", MessageBoxButton.OKCancel) <> MessageBoxResult.OK Then
                        e.Cancel = True
                    End If
                    GoTo CaseLabel1
                End Select
            End Sub

            Dim item1 As New DiagramShape() With {.Shape = BasicShapes.Ellipse, .Position = New Point(30, 50), .Width = 120, .Height = 80}
            Dim item2 As New DiagramShape() With {.Shape = BasicShapes.Hexagon, .Position = New Point(30, 150), .Width = 120, .Height = 80}
            diagram.Items.Add(item1)
            diagram.Items.Add(item2)
            diagram.SelectItems(item1, item2)
            ' Code Example End
            Return panel
        End Function
    End Class
End Namespace
