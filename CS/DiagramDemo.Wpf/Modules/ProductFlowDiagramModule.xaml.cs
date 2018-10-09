using DevExpress.Data.Filtering;
using DevExpress.Diagram.Core;
using DevExpress.Xpf.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using DevExpress.Diagram.Demos;

namespace DiagramDemo {
    public partial class ProductFlowDiagramModule : DiagramDemoModule {
        readonly DiagramItemStyleId[] styles = DiagramShapeStyleId.Styles.ToArray();
        readonly ProductFlowInfo info;
        public ProductFlowDiagramModule() {
            InitializeComponent();
            diagramControl.Commands.RegisterHotKeys(ClearHotKeys);
            info = OrderDataGenerator.GenerateProductFlowInfo();
            DataContext = info;
        }
        void ClearHotKeys(IHotKeysRegistrator registrator) {
            registrator.ClearHotKeys(DiagramCommandsBase.SaveFileAsCommand);
            registrator.ClearHotKeys(DiagramCommandsBase.SaveFileCommand);
        }

        void SelectionChanged(object sender, EventArgs e) {
            var selectedDiagramItem = this.diagramControl.PrimarySelection;
            gridControl.ClearGrouping();
            gridControl.FilterCriteria = null;

            if(selectedDiagramItem == null)
                return;

            var customer = selectedDiagramItem.DataContext as CustomerData;
            if(customer != null) {
                gridControl.FilterCriteria = new BinaryOperator("Customer.Name", customer.Name, BinaryOperatorType.Equal);
                gridControl.GroupBy("Category.Name");
                gridControl.ExpandAllGroups();
            }

            var category = selectedDiagramItem.DataContext as CategoryData;
            if(category != null) {
                gridControl.FilterCriteria = new BinaryOperator("Category.Name", category.Name, BinaryOperatorType.Equal);
                gridControl.GroupBy("Customer.Name");
                gridControl.ExpandAllGroups();
            }

            var connector = selectedDiagramItem.DataContext as ProductFlowData;
            if(connector != null) {
                var productFilter = new BinaryOperator("Category.Name", connector.Category.Name, BinaryOperatorType.Equal);
                var customerFilter = new BinaryOperator("Customer.Name", connector.Customer.Name, BinaryOperatorType.Equal);
                gridControl.FilterCriteria = new GroupOperator(GroupOperatorType.And, productFilter, customerFilter);
            }
        }

        private void dataBindingBehavior_GenerateItem(object sender, DiagramGenerateItemEventArgs e) {
            var templateName = (e.DataObject is CustomerData) ? "CustomerTemplate" : "CategoryTemplate";
            e.Item = e.CreateItemFromTemplate(templateName);

        }
        private void dataBindingBehavior_CustomLayoutItems(object sender, DiagramCustomLayoutItemsEventArgs e) {
            ArrangeItemsInLine<CategoryData>(e.Items, new Point(600, 50), new Size(150, 105), 20);
            ArrangeItemsInLine<CustomerData>(e.Items, new Point(50, 100), new Size(150, 105), 20);
            foreach(var item in e.Items) {
                var customer = item.DataContext as CustomerData;
                if(customer != null) {
                    item.ThemeStyleId = styles[Array.IndexOf(info.Customers, customer)];
                }
            }
            foreach(var connector in e.Connectors) {
                var connectorData = (ProductFlowData)connector.DataContext;
                connector.ThemeStyleId = styles[Array.IndexOf(info.Customers, connectorData.Customer)];
            }
            e.Handled = true;
        }
        void ArrangeItemsInLine<TDataContext>(IEnumerable<DiagramItem> items, Point startPosition, Size itemSize, int margin) {
            Point position = startPosition;
            foreach(var diagramItem in items.Where(x => x.DataContext is TDataContext)) {
                diagramItem.Position = position;
                position.Offset(0, itemSize.Height + margin);
            }
        }
        private void dataBindingBehavior_UpdateConnector(object sender, DiagramUpdateConnectorEventArgs e) {
            var connectorData = (ProductFlowData)e.DataObject;
            e.Connector.StrokeThickness = connectorData.Weight;
        }
        private void DiagramControl_ItemsChanged(object sender, DiagramItemsChangedEventArgs e) {
            if(diagramControl.Items.Count == 1)
                diagramControl.SelectItem(diagramControl.Items.First());
        }
    }
}
