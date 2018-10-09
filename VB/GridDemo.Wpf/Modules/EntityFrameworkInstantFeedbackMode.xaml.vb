Namespace GridDemo
    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/InstantFeedbackModeViewModelBase.(cs)"), DevExpress.Xpf.DemoBase.CodeFile("Modules/EntityFrameworkServerMode/EntityFrameworkInstantFeedbackModeViewModel.(cs)"), DevExpress.Xpf.DemoBase.CodeFile("Modules/EntityFrameworkServerMode/OutlookDataContext.(cs)")>
    Partial Public Class EntityFrameworkInstantFeedbackMode
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            AddHandler ModuleUnloaded, Sub(s, e)
                grid.ItemsSource = Nothing
                instantFeedbackDataSource.Dispose()
            End Sub
        End Sub
    End Class
End Namespace
