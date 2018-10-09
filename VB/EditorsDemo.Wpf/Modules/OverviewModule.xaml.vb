Imports System
Imports System.Collections.Generic
Imports DevExpress.Xpf.DemoBase

Namespace EditorsDemo
    <CodeFile("ViewModels/OverviewDemoViewModel.(cs)")>
    Partial Public Class OverviewModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            DataContext = New EmployeeViewModel()
        End Sub
    End Class
End Namespace
