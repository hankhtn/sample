Imports DevExpress.Diagram.Core
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Shapes

Namespace DiagramDemo
    Partial Public Class FlowchartModule
        Inherits DiagramDemoModule

        Public Sub New()
            InitializeComponent()
            diagramControl.LoadDemoData("Flowchart.xml")
        End Sub
    End Class
End Namespace
