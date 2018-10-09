Imports DevExpress.Data.Filtering
Imports DevExpress.Diagram.Demos
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Diagram
Imports System.Windows
Imports System.Windows.Controls
Imports ColumnDefinition = DevExpress.Diagram.Demos.ColumnDefinition

Namespace DiagramDemo
    <CodeFile("ViewModels/DatabaseDiagramViewModel.(cs)"), CodeFile("Data/DatabaseDiagramData.(cs)")>
    Partial Public Class DatabaseDiagram
        Inherits DiagramDemoModule

        Private evaluationOperator As TableRelationEvaluationOperator
        Public Sub New()
            evaluationOperator = New TableRelationEvaluationOperator()
            CriteriaOperator.RegisterCustomFunction(evaluationOperator)
            InitializeComponent()
        End Sub
        Protected Overrides Sub Finalize()
            CriteriaOperator.UnregisterCustomFunction(evaluationOperator)
        End Sub
    End Class

    Public Class DatabaseDiagramItemTemplateSelector
        Inherits DataTemplateSelector

        Public Property TableTemplate() As DataTemplate
        Public Property ColumnTemplate() As DataTemplate

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            If TypeOf item Is TableDefinition Then
                Return TableTemplate
            ElseIf TypeOf item Is ColumnDefinition Then
                Return ColumnTemplate
            End If
            Return MyBase.SelectTemplate(item, container)
        End Function
    End Class
End Namespace
