Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpf.Spreadsheet

Namespace SpreadsheetDemo
    Partial Public Class CellCustomization
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class CellTemplateSelector
        Inherits DataTemplateSelector

        Private customTemplateCells As New List(Of String)() From {"F13", "F22", "F32"}

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim data As CellData = TryCast(item, CellData)
            Return If(CanApplyCustomTemplate(data.Cell.GetReferenceA1()), GetCellTemplate(data.Cell, CType(container, SpreadsheetControl)), MyBase.SelectTemplate(item, container))
        End Function

        Private Function CanApplyCustomTemplate(ByVal cellPosition As String) As Boolean
            Return customTemplateCells.Contains(cellPosition)
        End Function

        Private Function GetCellTemplate(ByVal cell As Cell, ByVal control As SpreadsheetControl) As DataTemplate
            Dim value As Double = 0
            Double.TryParse(cell.DisplayText, NumberStyles.Float, control.Options.Culture, value)
            Dim templateName As String = If(Math.Sign(value) = -1, "IncorrectTemplate", "CorrectTemplate")
            Return TryCast(control.TryFindResource(templateName), DataTemplate)
        End Function
    End Class
End Namespace
