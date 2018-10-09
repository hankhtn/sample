Imports DevExpress.Data.Filtering
Imports DevExpress.Xpf.Editors
Imports System

Namespace GridDemo
    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/FilteringTemplates.xaml"), DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/FilteringClasses.(cs)")>
    Partial Public Class Filtering
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()

            grid.FilterCriteria = New OperandProperty("City") = "Bergamo"
            grid.FilterCriteria = New OperandProperty("UnboundOrderDate") >= New Date(Date.Today.Year, 1, 1)
            AddHandler viewsListBox.EditValueChanging, AddressOf ViewsListBox_EditValueChanging
        End Sub

        Private Sub ViewsListBox_EditValueChanging(ByVal sender As Object, ByVal e As EditValueChangingEventArgs)
            grid.View.IncrementalSearchEnd()
        End Sub
    End Class
End Namespace
