Imports DevExpress.Xpf.DemoBase.DataClasses
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace GridDemo
    Partial Public Class TreeListView
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
    Public Class EmployeeCategoryImageSelector
        Inherits TreeListNodeImageSelector

        Public Overrides Function [Select](ByVal rowData As DevExpress.Xpf.Grid.TreeList.TreeListRowData) As ImageSource
            Dim empl As Employee = (TryCast(rowData.Row, Employee))
            If empl Is Nothing OrElse String.IsNullOrEmpty(empl.GroupName) Then
                Return Nothing
            End If
            Dim path = GroupNameToImageConverterExtension.GetImagePathByGroupName(empl.GroupName)
            Return New BitmapImage(New Uri(path))
        End Function
    End Class
End Namespace
