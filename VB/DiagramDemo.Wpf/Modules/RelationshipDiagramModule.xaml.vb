Imports DevExpress.Diagram.Core
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Diagram
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Data
Imports System.Globalization
Imports DevExpress.Diagram.Demos
Imports DevExpress.Mvvm.POCO

Namespace DiagramDemo
    <CodeFile("ViewModels/RelationshipDiagramViewModel.(cs)"), CodeFile("Resources/RelationshipDiagramModuleResources.xaml")>
    Partial Public Class RelationshipDiagramModule
        Inherits DiagramDemoModule

        Private Const DefaultStrokeThickness As Double = 2
        Private Const SelectedStrokeThickness As Double = 4
        Private ReadOnly ViewModel As RelationshipDiagramViewModel
        Private onStartup As Boolean = True

        Public Sub New()
            ViewModel = ViewModelSource.Create(Function() New RelationshipDiagramViewModel(EmployeesData.FilteredEmployees.Take(9).ToArray()))
            DataContext = ViewModel
            InitializeComponent()
        End Sub

        Private Sub diagramControl_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
            For Each diagramConnector In diagramControl.Items.OfType(Of DiagramConnector)()
                Dim isSelectionRelatedConnector As Boolean = diagramControl.SelectedItems.Contains(CType(diagramConnector.BeginItem, DiagramItem)) OrElse diagramControl.SelectedItems.Contains(CType(diagramConnector.EndItem, DiagramItem))
                diagramConnector.StrokeThickness = If(isSelectionRelatedConnector, SelectedStrokeThickness, DefaultStrokeThickness)
            Next diagramConnector
            ViewModel.SelectedEmployee = If(diagramControl.PrimarySelection IsNot Nothing, (TryCast(diagramControl.PrimarySelection.DataContext, Employee)), Nothing)
        End Sub

        Private Sub dataBindingBehavior_GenerateConnector(ByVal sender As Object, ByVal e As DiagramGenerateConnectorEventArgs)
            Dim relationshipInfo = CType(e.DataObject, RelationshipInfo)
            e.Connector = e.CreateConnectorFromTemplate(relationshipInfo.Relationship.ToString())
        End Sub

        Private Sub diagramControl_ItemsChanged(ByVal sender As Object, ByVal e As DiagramItemsChangedEventArgs)
            If onStartup AndAlso TypeOf e.Item Is DiagramContainer Then
                onStartup = False
                diagramControl.SelectItem(CType(e.Item, DiagramContainer))
            End If
        End Sub

        Private Sub dataBindingBehavior_ItemsGenerated(ByVal sender As Object, ByVal e As DiagramItemsGeneratedEventArgs)
            diagramControl.FitToDrawing()
        End Sub
    End Class
End Namespace
