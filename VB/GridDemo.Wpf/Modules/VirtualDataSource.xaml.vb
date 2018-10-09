Imports DevExpress.Data

Namespace GridDemo
    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/VirtualDataSourceViewModel.(cs)"), DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/VirtualDataSourceValuesProvider.(cs)")>
    Partial Public Class VirtualDataSource
        Inherits GridDemoModule

        Public Const ColumnsCount As Integer = 1000
        Public Const RowsCount As Integer = 100000000

        Public Sub New()
            InitializeComponent()
        End Sub

        Public ReadOnly Property ViewModel() As VirtualDataSourceViewModel
            Get
                Return CType(DataContext, VirtualDataSourceViewModel)
            End Get
        End Property

        Private Sub virtualDataSource_ValueNeeded(ByVal sender As Object, ByVal e As VirtualSourceValueNeededEventArgs)
            e.Value = ViewModel.GetValue(e.RowIndex, e.ColumnIndex)
        End Sub

        Private Sub virtualDataSource_ValuePushed(ByVal sender As Object, ByVal e As VirtualSourceValuePushedEventArgs)
            ViewModel.SetValue(e.RowIndex, e.ColumnIndex, e.Value)
        End Sub

        Private Sub virtualDataSource_PropertyNeeded(ByVal sender As Object, ByVal e As VirtualSourcePropertyNeededEventArgs)
            Dim [property] As VirtualDataSourceProperty = ViewModel.CreateProperty(e.Index)
            e.ProperyName = [property].Name
            e.PropertyType = [property].Type
        End Sub
    End Class
End Namespace
