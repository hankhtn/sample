Imports DevExpress.Xpf.DemoBase
Imports MVVMDemo.DXDataTemplateSelector

Namespace MVVMDemo
    Partial Public Class DXDataTemplateSelectorModule
        Inherits MVVMDemoModule

        Protected Overrides Function CreateShowcases() As ShowcaseInfo()
            Const path As String = "Modules/DXDataTemplateSelector"
            Const uri As String = "119428/MVVM-Framework/DXBinding/DXDataTemplateSelector"
            Return New ShowcaseInfo() { LoadShowcase("Declarative DataTemplateSelector", uri, path, { GetType(DeclarativeDataTemplateSelectorView), GetType(UserRoleInfo) }), LoadShowcase("Using DXBinding", uri, path, { GetType(UsingDXBindingView), GetType(UserRoleInfo) })}
        End Function
    End Class
End Namespace
