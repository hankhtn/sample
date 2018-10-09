Imports DevExpress.Xpf.DemoBase.DataClasses
Imports System.Xml

Namespace GridDemo
    Partial Public Class BindingToXML
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            Dim document As New XmlDocument()
            document.Load(EmployeesWithPhotoData.GetDataStream())
            grid.ItemsSource = document.SelectNodes("/Employees/Employee")
        End Sub
    End Class
End Namespace
