Imports System

Namespace GridDemo
    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/InstantFeedbackModeViewModelBase.(cs)"), DevExpress.Xpf.DemoBase.CodeFile("Modules/LookUpEditServerMode/LookUpInstantFeedbackModeViewModel.(cs)"), DevExpress.Xpf.DemoBase.CodeFile("Modules/LookUpEditServerMode/PersonsDataContext.(cs)")>
    Partial Public Class LookUpEditServerMode
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            AddHandler ModuleLoaded, Sub(o, e)
                Dispatcher.BeginInvoke(New Action(Sub() CType(DataContext, LookUpInstantFeedbackModeViewModel).OnLoaded()))
            End Sub
        End Sub
    End Class
End Namespace
