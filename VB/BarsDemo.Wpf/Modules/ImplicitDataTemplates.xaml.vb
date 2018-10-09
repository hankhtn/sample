Imports DevExpress.Xpf.DemoBase
Imports System
Imports System.Collections.Generic

Namespace BarsDemo

    <CodeFile("ViewModels/ImplicitDataTemplatesModel.(cs)")>
    Partial Public Class ImplicitDataTemplates
        Inherits BarsDemoModule

        Public Sub New()
            InitializeComponent()
            DataContext = New List(Of CommandModel) From {
                New GroupModel() With {
                    .Caption = "File", .Glyph = "Home_16x16.png", .Commands = New List(Of CommandModel)() From {
                        New CommandModel() With {.Caption = "New", .Glyph = "New_16x16.png"},
                        New CommandModel() With {.Caption = "Open", .Glyph = "Open_16x16.png"},
                        New CommandModel() With {.Caption = "Close", .Glyph = "Close_16x16.png"}
                    }
                },
                New CommandModel() With {.Caption = "Settings", .Glyph = "New_16x16.png"},
                New EditorModel() With {.Caption = "Search:", .Glyph="Find_16x16.png"},
                New LabelModel() With {.Content = Date.Now}
            }
        End Sub
    End Class
End Namespace
