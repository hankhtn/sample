Imports DevExpress.Xpf.DemoBase

Namespace EditorsDemo
    <CodeFile("ViewModels/RangeControlFilteringViewModel.(cs)")>
    Partial Public Class RangeControlFilteringModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            AddHandler ModuleUnloaded, Sub(s, e)
                grid.ItemsSource = Nothing
                WcfInstantFeedbackDataSource.Dispose()
            End Sub
        End Sub
    End Class
End Namespace
