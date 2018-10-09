Imports System
Imports System.Linq
Imports System.Windows
Imports DevExpress.Diagram.Core
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo
    Public NotInheritable Class ConfirmationMessageExample

        Private Sub New()
        End Sub

        Public Shared Function Run() As FrameworkElement
            Dim diagram = New DiagramControl() With {.PageSize = New Size(400, 300)}
            AddHandler diagram.Loaded, Sub(s, e) diagram.FitToPage()
            AddHandler diagram.QueryItemsAction, Sub(sender, e)
                If e.Action = ItemsActionKind.Move Then
                    e.Allow = False
                End If
            End Sub
            AddHandler diagram.QueryConnectionPoints, Sub(sender, e)
                If Equals(e.OppositeItem, e.HoveredItem) Then
                    e.ItemConnectionBorderState = ConnectionElementState.Hidden
                    For Each p As ConnectionPoint In e.ItemConnectionPointStates
                        p.State = ConnectionElementState.Hidden
                    Next p
                End If
            End Sub
            AddHandler diagram.ItemsChanged, Sub(sender, e)
                If e.Action = ItemsChangedAction.Added AndAlso TypeOf e.Item Is DiagramConnector Then
                    CType(e.Item, DiagramConnector).CanChangeRoute = False
                    e.Item.StrokeThickness = 2
                    e.Item.StrokeId = DiagramThemeColorId.Black_3
                End If
            End Sub
            ' Code Example Start
            ' Each time the end-user tries to modify a connector, the ConnectionChanging event is raised.
            ' The following implementation invokes the confirmation dialog window prompting the user to confirm the action.

            AddHandler diagram.ConnectionChanging, Sub(sender, e)
                If DXMessageBox.Show(diagram, "Confirm the connection changing action.", "Confirmation", MessageBoxButton.OKCancel) <> MessageBoxResult.OK Then
                    e.Cancel = True
                End If
            End Sub

            Dim item1 As New DiagramShape() With {.Shape = BasicShapes.Parallelogram, .Position = New Point(150, 40), .Width = 120, .Height = 80}
            diagram.Items.Add(item1)
            Dim item2 As New DiagramShape() With {.Shape = BasicShapes.Ellipse, .Position = New Point(250, 200), .Width = 120, .Height = 80}
            diagram.Items.Add(item2)
            diagram.Items.Add(New DiagramShape() With {.Shape = BasicShapes.Hexagon, .Position = New Point(30, 200), .Width = 120, .Height = 80})
            Dim connector As New DiagramConnector() With {.BeginItem = item1, .EndItem = item2}
            diagram.Items.Add(connector)
            diagram.SelectItem(connector)
            ' Code Example End
            Return diagram
        End Function
    End Class
End Namespace
