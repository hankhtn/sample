Imports System
Imports System.Linq
Imports System.Windows
Imports DevExpress.Diagram.Core
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo
    Public NotInheritable Class ProhibitingActions2Example

        Private Sub New()
        End Sub

        Public Shared Function Run() As FrameworkElement
            Dim diagram = New DiagramControl() With {.PageSize = New Size(400, 300)}
            AddHandler diagram.Loaded, Sub(s, e) diagram.FitToPage()
            ' Code Example Start
            ' Each time the end-user tries to perform an action on a diagram item, the QueryItemsAction event is raised.
            ' The following implementation restricts certain actions based on the item's content.

            AddHandler diagram.QueryItemsAction, Sub(sender, e)
                Select Case e.Action
                Case ItemsActionKind.MoveCopy
                    GoTo CaseLabel1
                Case ItemsActionKind.Copy
                CaseLabel1:
                    If e.Items.OfType(Of DiagramShape)().Any(Function(x) x.Content = "Non-copyable") Then
                        e.Allow = False
                    End If
                Case ItemsActionKind.Delete
                    If e.Items.OfType(Of DiagramShape)().Any(Function(x) x.Content = "Non-deletable") Then
                        e.Allow = False
                    End If
                End Select
            End Sub
            AddHandler diagram.QueryItemEditAction, Sub(sender, e)
                If TypeOf e.Item Is DiagramShape AndAlso CType(e.Item, DiagramShape).Content = "Non-editable" Then
                    e.Allow = False
                End If
            End Sub

            diagram.Items.Add(New DiagramShape() With {.Shape = BasicShapes.Ellipse, .Content = "Non-copyable", .Position = New Point(22.5, 40), .Width = 120, .Height = 50})
            diagram.Items.Add(New DiagramShape() With {.Shape = BasicShapes.Rectangle, .Content = "Non-deletable", .Position = New Point(142.5, 120), .Width = 120, .Height = 50})
            diagram.Items.Add(New DiagramShape() With {.Shape = BasicShapes.Parallelogram, .Content = "Non-editable", .Position = New Point(262.5, 200), .Width = 120, .Height = 50})
            ' Code Example End
            Return diagram
        End Function
    End Class
End Namespace
