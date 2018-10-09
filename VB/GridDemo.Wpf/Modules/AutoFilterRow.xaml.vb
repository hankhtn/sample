Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Editors.Settings

Namespace GridDemo



    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/AutoFilterRowTemplates.xaml"), DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/AutoFilterRowClasses.(cs)")>
    Partial Public Class AutoFilterRow
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            grid.ItemsSource = OutlookDataGenerator.CreateOutlookDataTable(1000)
            Dim settings As New ComboBoxEditSettings() With {.IsTextEditable = False}
            ComboBoxEdit.SetupComboBoxSettingsEnumItemSource(Of Priority)(settings)
            colPriority.EditSettings = settings
        End Sub
    End Class
End Namespace
