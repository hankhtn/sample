Imports DevExpress.Data.Filtering
Imports DevExpress.Diagram.Core
Imports DevExpress.Xpf.Diagram
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Windows
Imports System.Windows.Data
Imports System.Globalization
Imports DevExpress.Diagram.Demos

Namespace DiagramDemo
    Partial Public Class ProductFlowDiagramModule
        Inherits DiagramDemoModule

        Private ReadOnly styles() As DiagramItemStyleId = DiagramShapeStyleId.Styles.ToArray()
        Private ReadOnly info As ProductFlowInfo
        Public Sub New()
            InitializeComponent()
            diagramControl.Commands.RegisterHotKeys(AddressOf ClearHotKeys)
            info = OrderDataGenerator.GenerateProductFlowInfo()
            DataContext = info
        End Sub
        Private Sub ClearHotKeys(ByVal registrator As IHotKeysRegistrator)
            registrator.ClearHotKeys(DiagramCommandsBase.SaveFileAsCommand)
            registrator.ClearHotKeys(DiagramCommandsBase.SaveFileCommand)
        End Sub

        Private Sub SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim selectedDiagramItem = Me.diagramControl.PrimarySelection
            gridControl.ClearGrouping()
            gridControl.FilterCriteria = Nothing

            If selectedDiagramItem Is Nothing Then
                Return
            End If

            Dim customer = TryCast(selectedDiagramItem.DataContext, CustomerData)
            If customer IsNot Nothing Then
                gridControl.FilterCriteria = New BinaryOperator("Customer.Name", customer.Name, BinaryOperatorType.Equal)
                gridControl.GroupBy("Category.Name")
                gridControl.ExpandAllGroups()
            End If

            Dim category = TryCast(selectedDiagramItem.DataContext, CategoryData)
            If category IsNot Nothing Then
                gridControl.FilterCriteria = New BinaryOperator("Category.Name", category.Name, BinaryOperatorType.Equal)
                gridControl.GroupBy("Customer.Name")
                gridControl.ExpandAllGroups()
            End If

            Dim connector = TryCast(selectedDiagramItem.DataContext, ProductFlowData)
            If connector IsNot Nothing Then
                Dim productFilter = New BinaryOperator("Category.Name", connector.Category.Name, BinaryOperatorType.Equal)
                Dim customerFilter = New BinaryOperator("Customer.Name", connector.Customer.Name, BinaryOperatorType.Equal)
                gridControl.FilterCriteria = New GroupOperator(GroupOperatorType.And, productFilter, customerFilter)
            End If
        End Sub

        Private Sub dataBindingBehavior_GenerateItem(ByVal sender As Object, ByVal e As DiagramGenerateItemEventArgs)
            Dim templateName = If(TypeOf e.DataObject Is CustomerData, "CustomerTemplate", "CategoryTemplate")
            e.Item = e.CreateItemFromTemplate(templateName)

        End Sub
        Private Sub dataBindingBehavior_CustomLayoutItems(ByVal sender As Object, ByVal e As DiagramCustomLayoutItemsEventArgs)
            ArrangeItemsInLine(Of CategoryData)(e.Items, New Point(600, 50), New Size(150, 105), 20)
            ArrangeItemsInLine(Of CustomerData)(e.Items, New Point(50, 100), New Size(150, 105), 20)
            For Each item In e.Items
                Dim customer = TryCast(item.DataContext, CustomerData)
                If customer IsNot Nothing Then
                    item.ThemeStyleId = styles(Array.IndexOf(info.Customers, customer))
                End If
            Next item
            For Each connector In e.Connectors
                Dim connectorData = CType(connector.DataContext, ProductFlowData)
                connector.ThemeStyleId = styles(Array.IndexOf(info.Customers, connectorData.Customer))
            Next connector
            e.Handled = True
        End Sub
        Private Sub ArrangeItemsInLine(Of TDataContext)(ByVal items As IEnumerable(Of DiagramItem), ByVal startPosition As Point, ByVal itemSize As Size, ByVal margin As Integer)
            Dim position As Point = startPosition
            For Each diagramItem In items.Where(Function(x) TypeOf x.DataContext Is TDataContext)
                diagramItem.Position = position
                position.Offset(0, itemSize.Height + margin)
            Next diagramItem
        End Sub
        Private Sub dataBindingBehavior_UpdateConnector(ByVal sender As Object, ByVal e As DiagramUpdateConnectorEventArgs)
            Dim connectorData = CType(e.DataObject, ProductFlowData)
            e.Connector.StrokeThickness = connectorData.Weight
        End Sub
        Private Sub DiagramControl_ItemsChanged(ByVal sender As Object, ByVal e As DiagramItemsChangedEventArgs)
            If diagramControl.Items.Count = 1 Then
                diagramControl.SelectItem(diagramControl.Items.First())
            End If
        End Sub
    End Class
End Namespace
