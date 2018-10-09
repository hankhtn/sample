Imports DevExpress.DemoData.Models
Imports System.Data.Entity

Namespace GridDemo
    Partial Public Class MasterDetailViewViaEntityFramework
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            Dim customers = (New NWindContext()).Customers
            customers.Load()
            grid.ItemsSource = customers.Local
        End Sub
    End Class
End Namespace
