Imports MVVMDemo.DXCommandDemo
Imports DevExpress.Xpf.DemoBase

Namespace MVVMDemo
    Partial Public Class DXCommandModule
        Inherits MVVMDemoModule

        Protected Overrides Function CreateShowcases() As ShowcaseInfo()
            Const path As String = "Modules/DXCommand"
            Const uri As String = "115776/MVVM-Framework/Productivity/DXCommand"
            Return New ShowcaseInfo() { LoadShowcase("Binding command to methods", uri, path, { GetType(BindingCommandToMethodsView), GetType(BindingCommandToMethodsViewModel) }), LoadShowcase("Using values from other controls", uri, path, { GetType(ValuesFromControlsView), GetType(ValuesFromControlsViewModel) }), LoadShowcase("Using command parameter", uri, path, { GetType(CommandParameterView), GetType(CommandParameterViewModel) }), LoadShowcase("Call multiple methods", uri, path, { GetType(MultipleCallsView), GetType(MultipleCallsViewModel) })}
        End Function
    End Class
End Namespace
