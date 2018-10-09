Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Imaging
Imports DevExpress.DemoData.Models
Imports DevExpress.DemoData.Models.Vehicles
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.LayoutControl

Namespace LayoutControlDemo
    <CodeFile("ViewModels/CarsViewModel.(cs)")>
    Partial Public Class pageCars
        Inherits LayoutControlDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class CarDataTemplateSelector
        Inherits DataTemplateSelector

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim control = CType(container, FlowLayoutControl)
            Return CType(control.Resources(item.GetType().Name & "DataTemplate"), DataTemplate)
        End Function
    End Class
End Namespace
