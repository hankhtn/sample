Imports System
Imports System.Linq
Imports System.Windows
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Diagram
Imports ResizeMode = DevExpress.Diagram.Core.ResizeMode

Namespace DiagramDemo
    Public NotInheritable Class MaxWidthExample

        Private Sub New()
        End Sub

        Public Shared Function Run() As FrameworkElement
            Dim diagram = New DiagramControl() With {.PageSize = New Size(400, 300), .EnableProportionalResizing = False}
            AddHandler diagram.Loaded, Sub(s, e) diagram.FitToPage()
            ' Code Example Start
            ' Each time the end-user tries to resize a diagram item, the ItemsResizing event is raised.
            ' The following implementation sets the maximum width based on the item's content.

            AddHandler diagram.ItemsResizing, Sub(sender, e)
                For Each c As ResizingItem In e.Items
                    Dim maxWidth As Double = 0R
                    If TypeOf c.Item Is DiagramShape Then
                        Double.TryParse(CType(c.Item, DiagramShape).Content, maxWidth)
                    End If
                    Dim widthOver As Double = c.NewSize.Width - maxWidth
                    If widthOver <= 0R Then
                        Continue For
                    End If

                    c.NewSize = New Size(maxWidth, c.NewSize.Height)
                    If e.Mode = ResizeMode.Left OrElse e.Mode = ResizeMode.TopLeft OrElse e.Mode = ResizeMode.BottomLeft Then
                        c.NewDiagramPosition = New Point(c.NewDiagramPosition.X + widthOver, c.NewDiagramPosition.Y)
                    End If
                Next c
            End Sub

            diagram.Items.Add(New DiagramShape() With {.Content = "300", .Position = New Point(60, 75), .Width = 280, .Height = 150, .CanRotate = False, .FontSize = 24})
            ' Code Example End
            Return diagram
        End Function
    End Class
End Namespace
