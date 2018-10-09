Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.DemoBase
Imports System.Collections
Imports DevExpress.Xpf.Core
Imports DevExpress.Utils
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports DevExpress.Xpf.Editors
Imports System.Data


Namespace CommonDemo



    <CodeFile("ModuleResources/LookUpEditWithMultipleSelectionTemplates.xaml")>
    Partial Public Class LookUpEditWithMultipleSelection
        Inherits CommonDemoModule

        Public Sub New()
            InitializeComponent()
            lookUpEdit.ItemsSource = CarsData.DataSource
        End Sub
    End Class
End Namespace
