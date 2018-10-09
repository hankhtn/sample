'#pragma warning disable 1591

Namespace Modules.DataBinding





    <Global.System.Serializable(), Global.System.ComponentModel.DesignerCategoryAttribute("code"), Global.System.ComponentModel.ToolboxItem(True), Global.System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema"), Global.System.Xml.Serialization.XmlRootAttribute("nwindOrders"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")>
    Partial Public Class nwindOrders
        Inherits System.Data.DataSet

        Private tableOrderDetails As OrderDetailsDataTable

        Private tableOrders As OrdersDataTable

        Private tableProducts As ProductsDataTable

        Private _schemaSerializationMode As Global.System.Data.SchemaSerializationMode = Global.System.Data.SchemaSerializationMode.IncludeSchema

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Sub New()
            Me.BeginInit()
            Me.InitClass()
            Dim schemaChangedHandler As New Global.System.ComponentModel.CollectionChangeEventHandler(AddressOf Me.SchemaChanged)
            AddHandler MyBase.Tables.CollectionChanged, schemaChangedHandler
            AddHandler MyBase.Relations.CollectionChanged, schemaChangedHandler
            Me.EndInit()
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Sub New(ByVal info As Global.System.Runtime.Serialization.SerializationInfo, ByVal context As Global.System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context, False)
            If (Me.IsBinarySerialized(info, context) = True) Then
                Me.InitVars(False)
                Dim schemaChangedHandler1 As New Global.System.ComponentModel.CollectionChangeEventHandler(AddressOf Me.SchemaChanged)
                AddHandler Me.Tables.CollectionChanged, schemaChangedHandler1
                AddHandler Me.Relations.CollectionChanged, schemaChangedHandler1
                Return
            End If
            Dim strSchema As String = (DirectCast(info.GetValue("XmlSchema", GetType(String)), String))
            If (Me.DetermineSchemaSerializationMode(info, context) = Global.System.Data.SchemaSerializationMode.IncludeSchema) Then
                Dim ds As New Global.System.Data.DataSet()
                ds.ReadXmlSchema(New Global.System.Xml.XmlTextReader(New Global.System.IO.StringReader(strSchema)))
                If (ds.Tables("OrderDetails") IsNot Nothing) Then
                    MyBase.Tables.Add(New OrderDetailsDataTable(ds.Tables("OrderDetails")))
                End If
                If (ds.Tables("Orders") IsNot Nothing) Then
                    MyBase.Tables.Add(New OrdersDataTable(ds.Tables("Orders")))
                End If
                If (ds.Tables("Products") IsNot Nothing) Then
                    MyBase.Tables.Add(New ProductsDataTable(ds.Tables("Products")))
                End If
                Me.DataSetName = ds.DataSetName
                Me.Prefix = ds.Prefix
                Me.Namespace = ds.Namespace
                Me.Locale = ds.Locale
                Me.CaseSensitive = ds.CaseSensitive
                Me.EnforceConstraints = ds.EnforceConstraints
                Me.Merge(ds, False, Global.System.Data.MissingSchemaAction.Add)
                Me.InitVars()
            Else
                Me.ReadXmlSchema(New Global.System.Xml.XmlTextReader(New Global.System.IO.StringReader(strSchema)))
            End If
            Me.GetSerializationData(info, context)
            Dim schemaChangedHandler As New Global.System.ComponentModel.CollectionChangeEventHandler(AddressOf Me.SchemaChanged)
            AddHandler MyBase.Tables.CollectionChanged, schemaChangedHandler
            AddHandler Me.Relations.CollectionChanged, schemaChangedHandler
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Browsable(False), Global.System.ComponentModel.DesignerSerializationVisibility(Global.System.ComponentModel.DesignerSerializationVisibility.Content)>
        Public ReadOnly Property OrderDetails() As OrderDetailsDataTable
            Get
                Return Me.tableOrderDetails
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Browsable(False), Global.System.ComponentModel.DesignerSerializationVisibility(Global.System.ComponentModel.DesignerSerializationVisibility.Content)>
        Public ReadOnly Property Orders() As OrdersDataTable
            Get
                Return Me.tableOrders
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Browsable(False), Global.System.ComponentModel.DesignerSerializationVisibility(Global.System.ComponentModel.DesignerSerializationVisibility.Content)>
        Public ReadOnly Property Products() As ProductsDataTable
            Get
                Return Me.tableProducts
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.BrowsableAttribute(True), Global.System.ComponentModel.DesignerSerializationVisibilityAttribute(Global.System.ComponentModel.DesignerSerializationVisibility.Visible)>
        Public Overrides Property SchemaSerializationMode() As Global.System.Data.SchemaSerializationMode
            Get
                Return Me._schemaSerializationMode
            End Get
            Set(ByVal value As System.Data.SchemaSerializationMode)
                Me._schemaSerializationMode = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.DesignerSerializationVisibilityAttribute(Global.System.ComponentModel.DesignerSerializationVisibility.Hidden)>
        Public Shadows ReadOnly Property Tables() As Global.System.Data.DataTableCollection
            Get
                Return MyBase.Tables
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.DesignerSerializationVisibilityAttribute(Global.System.ComponentModel.DesignerSerializationVisibility.Hidden)>
        Public Shadows ReadOnly Property Relations() As Global.System.Data.DataRelationCollection
            Get
                Return MyBase.Relations
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Overrides Sub InitializeDerivedDataSet()
            Me.BeginInit()
            Me.InitClass()
            Me.EndInit()
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Overrides Function Clone() As Global.System.Data.DataSet
            Dim cln As nwindOrders = (DirectCast(MyBase.Clone(), nwindOrders))
            cln.InitVars()
            cln.SchemaSerializationMode = Me.SchemaSerializationMode
            Return cln
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Overrides Function ShouldSerializeTables() As Boolean
            Return False
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Overrides Function ShouldSerializeRelations() As Boolean
            Return False
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Overrides Sub ReadXmlSerializable(ByVal reader As Global.System.Xml.XmlReader)
            If (Me.DetermineSchemaSerializationMode(reader) = Global.System.Data.SchemaSerializationMode.IncludeSchema) Then
                Me.Reset()
                Dim ds As New Global.System.Data.DataSet()
                ds.ReadXml(reader)
                If (ds.Tables("OrderDetails") IsNot Nothing) Then
                    MyBase.Tables.Add(New OrderDetailsDataTable(ds.Tables("OrderDetails")))
                End If
                If (ds.Tables("Orders") IsNot Nothing) Then
                    MyBase.Tables.Add(New OrdersDataTable(ds.Tables("Orders")))
                End If
                If (ds.Tables("Products") IsNot Nothing) Then
                    MyBase.Tables.Add(New ProductsDataTable(ds.Tables("Products")))
                End If
                Me.DataSetName = ds.DataSetName
                Me.Prefix = ds.Prefix
                Me.Namespace = ds.Namespace
                Me.Locale = ds.Locale
                Me.CaseSensitive = ds.CaseSensitive
                Me.EnforceConstraints = ds.EnforceConstraints
                Me.Merge(ds, False, Global.System.Data.MissingSchemaAction.Add)
                Me.InitVars()
            Else
                Me.ReadXml(reader)
                Me.InitVars()
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Overrides Function GetSchemaSerializable() As Global.System.Xml.Schema.XmlSchema
            Dim stream As New Global.System.IO.MemoryStream()
            Me.WriteXmlSchema(New Global.System.Xml.XmlTextWriter(stream, Nothing))
            stream.Position = 0
            Return Global.System.Xml.Schema.XmlSchema.Read(New Global.System.Xml.XmlTextReader(stream), Nothing)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Friend Sub InitVars()
            Me.InitVars(True)
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Friend Sub InitVars(ByVal initTable As Boolean)
            Me.tableOrderDetails = (CType(MyBase.Tables("OrderDetails"), OrderDetailsDataTable))
            If (initTable = True) Then
                If (Me.tableOrderDetails IsNot Nothing) Then
                    Me.tableOrderDetails.InitVars()
                End If
            End If
            Me.tableOrders = (CType(MyBase.Tables("Orders"), OrdersDataTable))
            If (initTable = True) Then
                If (Me.tableOrders IsNot Nothing) Then
                    Me.tableOrders.InitVars()
                End If
            End If
            Me.tableProducts = (CType(MyBase.Tables("Products"), ProductsDataTable))
            If (initTable = True) Then
                If (Me.tableProducts IsNot Nothing) Then
                    Me.tableProducts.InitVars()
                End If
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitClass()
            Me.DataSetName = "nwindOrders"
            Me.Prefix = ""
            Me.Namespace = "http://tempuri.org/nwindOrders.xsd"
            Me.EnforceConstraints = True
            Me.SchemaSerializationMode = Global.System.Data.SchemaSerializationMode.IncludeSchema
            Me.tableOrderDetails = New OrderDetailsDataTable()
            MyBase.Tables.Add(Me.tableOrderDetails)
            Me.tableOrders = New OrdersDataTable()
            MyBase.Tables.Add(Me.tableOrders)
            Me.tableProducts = New ProductsDataTable()
            MyBase.Tables.Add(Me.tableProducts)
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Function ShouldSerializeOrderDetails() As Boolean
            Return False
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Function ShouldSerializeOrders() As Boolean
            Return False
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Function ShouldSerializeProducts() As Boolean
            Return False
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub SchemaChanged(ByVal sender As Object, ByVal e As Global.System.ComponentModel.CollectionChangeEventArgs)
            If (e.Action = Global.System.ComponentModel.CollectionChangeAction.Remove) Then
                Me.InitVars()
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Shared Function GetTypedDataSetSchema(ByVal xs As Global.System.Xml.Schema.XmlSchemaSet) As Global.System.Xml.Schema.XmlSchemaComplexType
            Dim ds As New nwindOrders()
            Dim type As New Global.System.Xml.Schema.XmlSchemaComplexType()
            Dim sequence As New Global.System.Xml.Schema.XmlSchemaSequence()
            Dim any As New Global.System.Xml.Schema.XmlSchemaAny()
            any.Namespace = ds.Namespace
            sequence.Items.Add(any)
            type.Particle = sequence
            Dim dsSchema As Global.System.Xml.Schema.XmlSchema = ds.GetSchemaSerializable()
            If xs.Contains(dsSchema.TargetNamespace) Then
                Dim s1 As New Global.System.IO.MemoryStream()
                Dim s2 As New Global.System.IO.MemoryStream()
                Try
                    Dim schema As Global.System.Xml.Schema.XmlSchema = Nothing
                    dsSchema.Write(s1)
                    Dim schemas As System.Collections.IEnumerator = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator()
                    Do While schemas.MoveNext()
                        schema = (DirectCast(schemas.Current, Global.System.Xml.Schema.XmlSchema))
                        s2.SetLength(0)
                        schema.Write(s2)
                        If (s1.Length = s2.Length) Then
                            s1.Position = 0
                            s2.Position = 0
                            Do While ((s1.Position <> s1.Length) AndAlso (s1.ReadByte() = s2.ReadByte()))

                            Loop
                            If (s1.Position = s1.Length) Then
                                Return type
                            End If
                        End If
                    Loop
                Finally
                    If (s1 IsNot Nothing) Then
                        s1.Close()
                    End If
                    If (s2 IsNot Nothing) Then
                        s2.Close()
                    End If
                End Try
            End If
            xs.Add(dsSchema)
            Return type
        End Function

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Delegate Sub OrderDetailsRowChangeEventHandler(ByVal sender As Object, ByVal e As OrderDetailsRowChangeEvent)

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Delegate Sub OrdersRowChangeEventHandler(ByVal sender As Object, ByVal e As OrdersRowChangeEvent)

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Delegate Sub ProductsRowChangeEventHandler(ByVal sender As Object, ByVal e As ProductsRowChangeEvent)




        <Global.System.Serializable(), Global.System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")>
        Partial Public Class OrderDetailsDataTable
            Inherits System.Data.TypedTableBase(Of OrderDetailsRow)

            Private columnOrderID As Global.System.Data.DataColumn

            Private columnQuantity As Global.System.Data.DataColumn

            Private columnUnitPrice As Global.System.Data.DataColumn

            Private columnDiscount As Global.System.Data.DataColumn

            Private columnProductName As Global.System.Data.DataColumn

            Private columnSupplier As Global.System.Data.DataColumn

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub New()
                Me.TableName = "OrderDetails"
                Me.BeginInit()
                Me.InitClass()
                Me.EndInit()
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub New(ByVal table As Global.System.Data.DataTable)
                Me.TableName = table.TableName
                If (table.CaseSensitive <> table.DataSet.CaseSensitive) Then
                    Me.CaseSensitive = table.CaseSensitive
                End If
                If (table.Locale.ToString() <> table.DataSet.Locale.ToString()) Then
                    Me.Locale = table.Locale
                End If
                If (table.Namespace <> table.DataSet.Namespace) Then
                    Me.Namespace = table.Namespace
                End If
                Me.Prefix = table.Prefix
                Me.MinimumCapacity = table.MinimumCapacity
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Sub New(ByVal info As Global.System.Runtime.Serialization.SerializationInfo, ByVal context As Global.System.Runtime.Serialization.StreamingContext)
                MyBase.New(info, context)
                Me.InitVars()
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property OrderIDColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnOrderID
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property QuantityColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnQuantity
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property UnitPriceColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnUnitPrice
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property DiscountColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnDiscount
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ProductNameColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnProductName
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property SupplierColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnSupplier
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Browsable(False)>
            Public ReadOnly Property Count() As Integer
                Get
                    Return Me.Rows.Count
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Default Public ReadOnly Property Item(ByVal index As Integer) As OrderDetailsRow
                Get
                    Return (DirectCast(Me.Rows(index), OrderDetailsRow))
                End Get
            End Property

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event OrderDetailsRowChanging As OrderDetailsRowChangeEventHandler

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event OrderDetailsRowChanged As OrderDetailsRowChangeEventHandler

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event OrderDetailsRowDeleting As OrderDetailsRowChangeEventHandler

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event OrderDetailsRowDeleted As OrderDetailsRowChangeEventHandler

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub AddOrderDetailsRow(ByVal row As OrderDetailsRow)
                Me.Rows.Add(row)
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function AddOrderDetailsRow(ByVal OrderID As Integer, ByVal Quantity As Short, ByVal UnitPrice As Decimal, ByVal Discount As Single, ByVal ProductName As String, ByVal Supplier As String) As OrderDetailsRow
                Dim rowOrderDetailsRow As OrderDetailsRow = (DirectCast(Me.NewRow(), OrderDetailsRow))
                Dim columnValuesArray() As Object = { OrderID, Quantity, UnitPrice, Discount, ProductName, Supplier}
                rowOrderDetailsRow.ItemArray = columnValuesArray
                Me.Rows.Add(rowOrderDetailsRow)
                Return rowOrderDetailsRow
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Overrides Function Clone() As Global.System.Data.DataTable
                Dim cln As OrderDetailsDataTable = (DirectCast(MyBase.Clone(), OrderDetailsDataTable))
                cln.InitVars()
                Return cln
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Function CreateInstance() As Global.System.Data.DataTable
                Return New OrderDetailsDataTable()
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub InitVars()
                Me.columnOrderID = MyBase.Columns("OrderID")
                Me.columnProductName = MyBase.Columns("ProductName")
                Me.columnSupplier = MyBase.Columns("Supplier")
                Me.columnUnitPrice = MyBase.Columns("UnitPrice")
                Me.columnQuantity = MyBase.Columns("Quantity")
                Me.columnDiscount = MyBase.Columns("Discount")
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Private Sub InitClass()
                Me.columnOrderID = New Global.System.Data.DataColumn("OrderID", GetType(Integer), Nothing, Global.System.Data.MappingType.Element)
                Me.columnOrderID.ReadOnly = True
                MyBase.Columns.Add(Me.columnOrderID)
                Me.columnProductName = New Global.System.Data.DataColumn("ProductName", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                Me.columnProductName.ReadOnly = True
                MyBase.Columns.Add(Me.columnProductName)
                Me.columnSupplier = New Global.System.Data.DataColumn("Supplier", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnSupplier)
                Me.columnProductName.MaxLength = 40
                Me.columnSupplier.ReadOnly = True
                Me.columnSupplier.MaxLength = 255
                Me.columnUnitPrice = New Global.System.Data.DataColumn("UnitPrice", GetType(Decimal), Nothing, Global.System.Data.MappingType.Element)
                Me.columnUnitPrice.ReadOnly = True
                MyBase.Columns.Add(Me.columnUnitPrice)
                Me.columnQuantity = New Global.System.Data.DataColumn("Quantity", GetType(Short), Nothing, Global.System.Data.MappingType.Element)
                Me.columnQuantity.ReadOnly = True
                MyBase.Columns.Add(Me.columnQuantity)
                Me.columnDiscount = New Global.System.Data.DataColumn("Discount", GetType(Single), Nothing, Global.System.Data.MappingType.Element)
                Me.columnDiscount.ReadOnly = True
                MyBase.Columns.Add(Me.columnDiscount)
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function NewOrderDetailsRow() As OrderDetailsRow
                Return (DirectCast(Me.NewRow(), OrderDetailsRow))
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Function NewRowFromBuilder(ByVal builder As Global.System.Data.DataRowBuilder) As Global.System.Data.DataRow
                Return New OrderDetailsRow(builder)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Function GetRowType() As Global.System.Type
                Return GetType(OrderDetailsRow)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowChanged(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowChanged(e)
                RaiseEvent OrderDetailsRowChanged(Me, New OrderDetailsRowChangeEvent((DirectCast(e.Row, OrderDetailsRow)), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowChanging(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowChanging(e)
                RaiseEvent OrderDetailsRowChanging(Me, New OrderDetailsRowChangeEvent((DirectCast(e.Row, OrderDetailsRow)), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowDeleted(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowDeleted(e)
                RaiseEvent OrderDetailsRowDeleted(Me, New OrderDetailsRowChangeEvent((DirectCast(e.Row, OrderDetailsRow)), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowDeleting(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowDeleting(e)
                RaiseEvent OrderDetailsRowDeleting(Me, New OrderDetailsRowChangeEvent((DirectCast(e.Row, OrderDetailsRow)), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub RemoveOrderDetailsRow(ByVal row As OrderDetailsRow)
                Me.Rows.Remove(row)
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Shared Function GetTypedTableSchema(ByVal xs As Global.System.Xml.Schema.XmlSchemaSet) As Global.System.Xml.Schema.XmlSchemaComplexType
                Dim type As New Global.System.Xml.Schema.XmlSchemaComplexType()
                Dim sequence As New Global.System.Xml.Schema.XmlSchemaSequence()
                Dim ds As New nwindOrders()
                Dim any1 As New Global.System.Xml.Schema.XmlSchemaAny()
                any1.Namespace = "http://www.w3.org/2001/XMLSchema"
                any1.MinOccurs = New Decimal(0)
                any1.MaxOccurs = Decimal.MaxValue
                any1.ProcessContents = Global.System.Xml.Schema.XmlSchemaContentProcessing.Lax
                sequence.Items.Add(any1)
                Dim any2 As New Global.System.Xml.Schema.XmlSchemaAny()
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1"
                any2.MinOccurs = New Decimal(1)
                any2.ProcessContents = Global.System.Xml.Schema.XmlSchemaContentProcessing.Lax
                sequence.Items.Add(any2)
                Dim attribute1 As New Global.System.Xml.Schema.XmlSchemaAttribute()
                attribute1.Name = "namespace"
                attribute1.FixedValue = ds.Namespace
                type.Attributes.Add(attribute1)
                Dim attribute2 As New Global.System.Xml.Schema.XmlSchemaAttribute()
                attribute2.Name = "tableTypeName"
                attribute2.FixedValue = "OrderDetailsDataTable"
                type.Attributes.Add(attribute2)
                type.Particle = sequence
                Dim dsSchema As Global.System.Xml.Schema.XmlSchema = ds.GetSchemaSerializable()
                If xs.Contains(dsSchema.TargetNamespace) Then
                    Dim s1 As New Global.System.IO.MemoryStream()
                    Dim s2 As New Global.System.IO.MemoryStream()
                    Try
                        Dim schema As Global.System.Xml.Schema.XmlSchema = Nothing
                        dsSchema.Write(s1)
                        Dim schemas As System.Collections.IEnumerator = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator()
                        Do While schemas.MoveNext()
                            schema = (DirectCast(schemas.Current, Global.System.Xml.Schema.XmlSchema))
                            s2.SetLength(0)
                            schema.Write(s2)
                            If (s1.Length = s2.Length) Then
                                s1.Position = 0
                                s2.Position = 0
                                Do While ((s1.Position <> s1.Length) AndAlso (s1.ReadByte() = s2.ReadByte()))

                                Loop
                                If (s1.Position = s1.Length) Then
                                    Return type
                                End If
                            End If
                        Loop
                    Finally
                        If (s1 IsNot Nothing) Then
                            s1.Close()
                        End If
                        If (s2 IsNot Nothing) Then
                            s2.Close()
                        End If
                    End Try
                End If
                xs.Add(dsSchema)
                Return type
            End Function
        End Class




        <Global.System.Serializable(), Global.System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")>
        Partial Public Class OrdersDataTable
            Inherits System.Data.TypedTableBase(Of OrdersRow)

            Private columnOrderID As Global.System.Data.DataColumn

            Private columnCustomerID As Global.System.Data.DataColumn

            Private columnEmployeeID As Global.System.Data.DataColumn

            Private columnOrderDate As Global.System.Data.DataColumn

            Private columnRequiredDate As Global.System.Data.DataColumn

            Private columnShippedDate As Global.System.Data.DataColumn

            Private columnShipVia As Global.System.Data.DataColumn

            Private columnFreight As Global.System.Data.DataColumn

            Private columnShipName As Global.System.Data.DataColumn

            Private columnShipAddress As Global.System.Data.DataColumn

            Private columnShipCity As Global.System.Data.DataColumn

            Private columnShipRegion As Global.System.Data.DataColumn

            Private columnShipPostalCode As Global.System.Data.DataColumn

            Private columnShipCountry As Global.System.Data.DataColumn

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub New()
                Me.TableName = "Orders"
                Me.BeginInit()
                Me.InitClass()
                Me.EndInit()
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub New(ByVal table As Global.System.Data.DataTable)
                Me.TableName = table.TableName
                If (table.CaseSensitive <> table.DataSet.CaseSensitive) Then
                    Me.CaseSensitive = table.CaseSensitive
                End If
                If (table.Locale.ToString() <> table.DataSet.Locale.ToString()) Then
                    Me.Locale = table.Locale
                End If
                If (table.Namespace <> table.DataSet.Namespace) Then
                    Me.Namespace = table.Namespace
                End If
                Me.Prefix = table.Prefix
                Me.MinimumCapacity = table.MinimumCapacity
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Sub New(ByVal info As Global.System.Runtime.Serialization.SerializationInfo, ByVal context As Global.System.Runtime.Serialization.StreamingContext)
                MyBase.New(info, context)
                Me.InitVars()
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property OrderIDColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnOrderID
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property CustomerIDColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnCustomerID
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property EmployeeIDColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnEmployeeID
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property OrderDateColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnOrderDate
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property RequiredDateColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnRequiredDate
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ShippedDateColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnShippedDate
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ShipViaColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnShipVia
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property FreightColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnFreight
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ShipNameColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnShipName
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ShipAddressColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnShipAddress
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ShipCityColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnShipCity
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ShipRegionColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnShipRegion
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ShipPostalCodeColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnShipPostalCode
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ShipCountryColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnShipCountry
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Browsable(False)>
            Public ReadOnly Property Count() As Integer
                Get
                    Return Me.Rows.Count
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Default Public ReadOnly Property Item(ByVal index As Integer) As OrdersRow
                Get
                    Return (DirectCast(Me.Rows(index), OrdersRow))
                End Get
            End Property

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event OrdersRowChanging As OrdersRowChangeEventHandler

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event OrdersRowChanged As OrdersRowChangeEventHandler

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event OrdersRowDeleting As OrdersRowChangeEventHandler

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event OrdersRowDeleted As OrdersRowChangeEventHandler

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub AddOrdersRow(ByVal row As OrdersRow)
                Me.Rows.Add(row)
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function AddOrdersRow(ByVal CustomerID As String, ByVal EmployeeID As Integer, ByVal OrderDate As Date, ByVal RequiredDate As Date, ByVal ShippedDate As Date, ByVal ShipVia As Integer, ByVal Freight As Decimal, ByVal ShipName As String, ByVal ShipAddress As String, ByVal ShipCity As String, ByVal ShipRegion As String, ByVal ShipPostalCode As String, ByVal ShipCountry As String) As OrdersRow
                Dim rowOrdersRow As OrdersRow = (DirectCast(Me.NewRow(), OrdersRow))
                Dim columnValuesArray() As Object = { Nothing, CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry}
                rowOrdersRow.ItemArray = columnValuesArray
                Me.Rows.Add(rowOrdersRow)
                Return rowOrdersRow
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function FindByOrderID(ByVal OrderID As Integer) As OrdersRow
                Return (DirectCast(Me.Rows.Find(New Object() { OrderID}), OrdersRow))
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Overrides Function Clone() As Global.System.Data.DataTable
                Dim cln As OrdersDataTable = (DirectCast(MyBase.Clone(), OrdersDataTable))
                cln.InitVars()
                Return cln
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Function CreateInstance() As Global.System.Data.DataTable
                Return New OrdersDataTable()
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub InitVars()
                Me.columnOrderID = MyBase.Columns("OrderID")
                Me.columnCustomerID = MyBase.Columns("CustomerID")
                Me.columnEmployeeID = MyBase.Columns("EmployeeID")
                Me.columnOrderDate = MyBase.Columns("OrderDate")
                Me.columnRequiredDate = MyBase.Columns("RequiredDate")
                Me.columnShippedDate = MyBase.Columns("ShippedDate")
                Me.columnShipVia = MyBase.Columns("ShipVia")
                Me.columnFreight = MyBase.Columns("Freight")
                Me.columnShipName = MyBase.Columns("ShipName")
                Me.columnShipAddress = MyBase.Columns("ShipAddress")
                Me.columnShipCity = MyBase.Columns("ShipCity")
                Me.columnShipRegion = MyBase.Columns("ShipRegion")
                Me.columnShipPostalCode = MyBase.Columns("ShipPostalCode")
                Me.columnShipCountry = MyBase.Columns("ShipCountry")
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Private Sub InitClass()
                Me.columnOrderID = New Global.System.Data.DataColumn("OrderID", GetType(Integer), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnOrderID)
                Me.columnCustomerID = New Global.System.Data.DataColumn("CustomerID", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnCustomerID)
                Me.columnEmployeeID = New Global.System.Data.DataColumn("EmployeeID", GetType(Integer), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnEmployeeID)
                Me.columnOrderDate = New Global.System.Data.DataColumn("OrderDate", GetType(Date), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnOrderDate)
                Me.columnRequiredDate = New Global.System.Data.DataColumn("RequiredDate", GetType(Date), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnRequiredDate)
                Me.columnShippedDate = New Global.System.Data.DataColumn("ShippedDate", GetType(Date), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnShippedDate)
                Me.columnShipVia = New Global.System.Data.DataColumn("ShipVia", GetType(Integer), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnShipVia)
                Me.columnFreight = New Global.System.Data.DataColumn("Freight", GetType(Decimal), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnFreight)
                Me.columnShipName = New Global.System.Data.DataColumn("ShipName", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnShipName)
                Me.columnShipAddress = New Global.System.Data.DataColumn("ShipAddress", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnShipAddress)
                Me.columnShipCity = New Global.System.Data.DataColumn("ShipCity", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnShipCity)
                Me.columnShipRegion = New Global.System.Data.DataColumn("ShipRegion", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnShipRegion)
                Me.columnShipPostalCode = New Global.System.Data.DataColumn("ShipPostalCode", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnShipPostalCode)
                Me.columnShipCountry = New Global.System.Data.DataColumn("ShipCountry", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnShipCountry)
                Me.Constraints.Add(New Global.System.Data.UniqueConstraint("Constraint1", New Global.System.Data.DataColumn() { Me.columnOrderID}, True))
                Me.columnOrderID.AutoIncrement = True
                Me.columnOrderID.AutoIncrementSeed = -1
                Me.columnOrderID.AutoIncrementStep = -1
                Me.columnOrderID.AllowDBNull = False
                Me.columnOrderID.Unique = True
                Me.columnCustomerID.MaxLength = 5
                Me.columnShipName.MaxLength = 40
                Me.columnShipAddress.MaxLength = 60
                Me.columnShipCity.MaxLength = 15
                Me.columnShipRegion.MaxLength = 15
                Me.columnShipPostalCode.MaxLength = 10
                Me.columnShipCountry.MaxLength = 15
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function NewOrdersRow() As OrdersRow
                Return (DirectCast(Me.NewRow(), OrdersRow))
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Function NewRowFromBuilder(ByVal builder As Global.System.Data.DataRowBuilder) As Global.System.Data.DataRow
                Return New OrdersRow(builder)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Function GetRowType() As Global.System.Type
                Return GetType(OrdersRow)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowChanged(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowChanged(e)
                RaiseEvent OrdersRowChanged(Me, New OrdersRowChangeEvent((DirectCast(e.Row, OrdersRow)), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowChanging(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowChanging(e)
                RaiseEvent OrdersRowChanging(Me, New OrdersRowChangeEvent((DirectCast(e.Row, OrdersRow)), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowDeleted(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowDeleted(e)
                RaiseEvent OrdersRowDeleted(Me, New OrdersRowChangeEvent((DirectCast(e.Row, OrdersRow)), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowDeleting(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowDeleting(e)
                RaiseEvent OrdersRowDeleting(Me, New OrdersRowChangeEvent((DirectCast(e.Row, OrdersRow)), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub RemoveOrdersRow(ByVal row As OrdersRow)
                Me.Rows.Remove(row)
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Shared Function GetTypedTableSchema(ByVal xs As Global.System.Xml.Schema.XmlSchemaSet) As Global.System.Xml.Schema.XmlSchemaComplexType
                Dim type As New Global.System.Xml.Schema.XmlSchemaComplexType()
                Dim sequence As New Global.System.Xml.Schema.XmlSchemaSequence()
                Dim ds As New nwindOrders()
                Dim any1 As New Global.System.Xml.Schema.XmlSchemaAny()
                any1.Namespace = "http://www.w3.org/2001/XMLSchema"
                any1.MinOccurs = New Decimal(0)
                any1.MaxOccurs = Decimal.MaxValue
                any1.ProcessContents = Global.System.Xml.Schema.XmlSchemaContentProcessing.Lax
                sequence.Items.Add(any1)
                Dim any2 As New Global.System.Xml.Schema.XmlSchemaAny()
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1"
                any2.MinOccurs = New Decimal(1)
                any2.ProcessContents = Global.System.Xml.Schema.XmlSchemaContentProcessing.Lax
                sequence.Items.Add(any2)
                Dim attribute1 As New Global.System.Xml.Schema.XmlSchemaAttribute()
                attribute1.Name = "namespace"
                attribute1.FixedValue = ds.Namespace
                type.Attributes.Add(attribute1)
                Dim attribute2 As New Global.System.Xml.Schema.XmlSchemaAttribute()
                attribute2.Name = "tableTypeName"
                attribute2.FixedValue = "OrdersDataTable"
                type.Attributes.Add(attribute2)
                type.Particle = sequence
                Dim dsSchema As Global.System.Xml.Schema.XmlSchema = ds.GetSchemaSerializable()
                If xs.Contains(dsSchema.TargetNamespace) Then
                    Dim s1 As New Global.System.IO.MemoryStream()
                    Dim s2 As New Global.System.IO.MemoryStream()
                    Try
                        Dim schema As Global.System.Xml.Schema.XmlSchema = Nothing
                        dsSchema.Write(s1)
                        Dim schemas As System.Collections.IEnumerator = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator()
                        Do While schemas.MoveNext()
                            schema = (DirectCast(schemas.Current, Global.System.Xml.Schema.XmlSchema))
                            s2.SetLength(0)
                            schema.Write(s2)
                            If (s1.Length = s2.Length) Then
                                s1.Position = 0
                                s2.Position = 0
                                Do While ((s1.Position <> s1.Length) AndAlso (s1.ReadByte() = s2.ReadByte()))

                                Loop
                                If (s1.Position = s1.Length) Then
                                    Return type
                                End If
                            End If
                        Loop
                    Finally
                        If (s1 IsNot Nothing) Then
                            s1.Close()
                        End If
                        If (s2 IsNot Nothing) Then
                            s2.Close()
                        End If
                    End Try
                End If
                xs.Add(dsSchema)
                Return type
            End Function
        End Class




        <Global.System.Serializable(), Global.System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")>
        Partial Public Class ProductsDataTable
            Inherits System.Data.TypedTableBase(Of ProductsRow)

            Private columnProductID As Global.System.Data.DataColumn

            Private columnProductName As Global.System.Data.DataColumn

            Private columnSupplierID As Global.System.Data.DataColumn

            Private columnCategoryID As Global.System.Data.DataColumn

            Private columnQuantityPerUnit As Global.System.Data.DataColumn

            Private columnUnitPrice As Global.System.Data.DataColumn

            Private columnUnitsInStock As Global.System.Data.DataColumn

            Private columnUnitsOnOrder As Global.System.Data.DataColumn

            Private columnReorderLevel As Global.System.Data.DataColumn

            Private columnDiscontinued As Global.System.Data.DataColumn

            Private columnEAN13 As Global.System.Data.DataColumn

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub New()
                Me.TableName = "Products"
                Me.BeginInit()
                Me.InitClass()
                Me.EndInit()
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub New(ByVal table As Global.System.Data.DataTable)
                Me.TableName = table.TableName
                If (table.CaseSensitive <> table.DataSet.CaseSensitive) Then
                    Me.CaseSensitive = table.CaseSensitive
                End If
                If (table.Locale.ToString() <> table.DataSet.Locale.ToString()) Then
                    Me.Locale = table.Locale
                End If
                If (table.Namespace <> table.DataSet.Namespace) Then
                    Me.Namespace = table.Namespace
                End If
                Me.Prefix = table.Prefix
                Me.MinimumCapacity = table.MinimumCapacity
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Sub New(ByVal info As Global.System.Runtime.Serialization.SerializationInfo, ByVal context As Global.System.Runtime.Serialization.StreamingContext)
                MyBase.New(info, context)
                Me.InitVars()
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ProductIDColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnProductID
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ProductNameColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnProductName
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property SupplierIDColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnSupplierID
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property CategoryIDColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnCategoryID
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property QuantityPerUnitColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnQuantityPerUnit
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property UnitPriceColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnUnitPrice
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property UnitsInStockColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnUnitsInStock
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property UnitsOnOrderColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnUnitsOnOrder
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ReorderLevelColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnReorderLevel
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property DiscontinuedColumn() As Global.System.Data.DataColumn
                Get
                    Return Me.columnDiscontinued
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property EAN13Column() As Global.System.Data.DataColumn
                Get
                    Return Me.columnEAN13
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Browsable(False)>
            Public ReadOnly Property Count() As Integer
                Get
                    Return Me.Rows.Count
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Default Public ReadOnly Property Item(ByVal index As Integer) As ProductsRow
                Get
                    Return (DirectCast(Me.Rows(index), ProductsRow))
                End Get
            End Property

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event ProductsRowChanging As ProductsRowChangeEventHandler

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event ProductsRowChanged As ProductsRowChangeEventHandler

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event ProductsRowDeleting As ProductsRowChangeEventHandler

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event ProductsRowDeleted As ProductsRowChangeEventHandler

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub AddProductsRow(ByVal row As ProductsRow)
                Me.Rows.Add(row)
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function AddProductsRow(ByVal ProductName As String, ByVal SupplierID As Integer, ByVal CategoryID As Integer, ByVal QuantityPerUnit As String, ByVal UnitPrice As Decimal, ByVal UnitsInStock As Short, ByVal UnitsOnOrder As Short, ByVal ReorderLevel As Short, ByVal Discontinued As Boolean, ByVal EAN13 As String) As ProductsRow
                Dim rowProductsRow As ProductsRow = (DirectCast(Me.NewRow(), ProductsRow))
                Dim columnValuesArray() As Object = { Nothing, ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued, EAN13}
                rowProductsRow.ItemArray = columnValuesArray
                Me.Rows.Add(rowProductsRow)
                Return rowProductsRow
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function FindByProductID(ByVal ProductID As Integer) As ProductsRow
                Return (DirectCast(Me.Rows.Find(New Object() { ProductID}), ProductsRow))
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Overrides Function Clone() As Global.System.Data.DataTable
                Dim cln As ProductsDataTable = (DirectCast(MyBase.Clone(), ProductsDataTable))
                cln.InitVars()
                Return cln
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Function CreateInstance() As Global.System.Data.DataTable
                Return New ProductsDataTable()
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub InitVars()
                Me.columnProductID = MyBase.Columns("ProductID")
                Me.columnProductName = MyBase.Columns("ProductName")
                Me.columnSupplierID = MyBase.Columns("SupplierID")
                Me.columnCategoryID = MyBase.Columns("CategoryID")
                Me.columnQuantityPerUnit = MyBase.Columns("QuantityPerUnit")
                Me.columnUnitPrice = MyBase.Columns("UnitPrice")
                Me.columnUnitsInStock = MyBase.Columns("UnitsInStock")
                Me.columnUnitsOnOrder = MyBase.Columns("UnitsOnOrder")
                Me.columnReorderLevel = MyBase.Columns("ReorderLevel")
                Me.columnDiscontinued = MyBase.Columns("Discontinued")
                Me.columnEAN13 = MyBase.Columns("EAN13")
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Private Sub InitClass()
                Me.columnProductID = New Global.System.Data.DataColumn("ProductID", GetType(Integer), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnProductID)
                Me.columnProductName = New Global.System.Data.DataColumn("ProductName", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnProductName)
                Me.columnSupplierID = New Global.System.Data.DataColumn("SupplierID", GetType(Integer), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnSupplierID)
                Me.columnCategoryID = New Global.System.Data.DataColumn("CategoryID", GetType(Integer), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnCategoryID)
                Me.columnQuantityPerUnit = New Global.System.Data.DataColumn("QuantityPerUnit", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnQuantityPerUnit)
                Me.columnUnitPrice = New Global.System.Data.DataColumn("UnitPrice", GetType(Decimal), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnUnitPrice)
                Me.columnUnitsInStock = New Global.System.Data.DataColumn("UnitsInStock", GetType(Short), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnUnitsInStock)
                Me.columnUnitsOnOrder = New Global.System.Data.DataColumn("UnitsOnOrder", GetType(Short), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnUnitsOnOrder)
                Me.columnReorderLevel = New Global.System.Data.DataColumn("ReorderLevel", GetType(Short), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnReorderLevel)
                Me.columnDiscontinued = New Global.System.Data.DataColumn("Discontinued", GetType(Boolean), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnDiscontinued)
                Me.columnEAN13 = New Global.System.Data.DataColumn("EAN13", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                MyBase.Columns.Add(Me.columnEAN13)
                Me.Constraints.Add(New Global.System.Data.UniqueConstraint("Constraint1", New Global.System.Data.DataColumn() { Me.columnProductID}, True))
                Me.columnProductID.AutoIncrement = True
                Me.columnProductID.AutoIncrementSeed = -1
                Me.columnProductID.AutoIncrementStep = -1
                Me.columnProductID.AllowDBNull = False
                Me.columnProductID.Unique = True
                Me.columnProductName.MaxLength = 40
                Me.columnQuantityPerUnit.MaxLength = 20
                Me.columnEAN13.MaxLength = 12
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function NewProductsRow() As ProductsRow
                Return (DirectCast(Me.NewRow(), ProductsRow))
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Function NewRowFromBuilder(ByVal builder As Global.System.Data.DataRowBuilder) As Global.System.Data.DataRow
                Return New ProductsRow(builder)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Function GetRowType() As Global.System.Type
                Return GetType(ProductsRow)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowChanged(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowChanged(e)
                RaiseEvent ProductsRowChanged(Me, New ProductsRowChangeEvent((DirectCast(e.Row, ProductsRow)), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowChanging(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowChanging(e)
                RaiseEvent ProductsRowChanging(Me, New ProductsRowChangeEvent((DirectCast(e.Row, ProductsRow)), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowDeleted(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowDeleted(e)
                RaiseEvent ProductsRowDeleted(Me, New ProductsRowChangeEvent((DirectCast(e.Row, ProductsRow)), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowDeleting(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowDeleting(e)
                RaiseEvent ProductsRowDeleting(Me, New ProductsRowChangeEvent((DirectCast(e.Row, ProductsRow)), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub RemoveProductsRow(ByVal row As ProductsRow)
                Me.Rows.Remove(row)
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Shared Function GetTypedTableSchema(ByVal xs As Global.System.Xml.Schema.XmlSchemaSet) As Global.System.Xml.Schema.XmlSchemaComplexType
                Dim type As New Global.System.Xml.Schema.XmlSchemaComplexType()
                Dim sequence As New Global.System.Xml.Schema.XmlSchemaSequence()
                Dim ds As New nwindOrders()
                Dim any1 As New Global.System.Xml.Schema.XmlSchemaAny()
                any1.Namespace = "http://www.w3.org/2001/XMLSchema"
                any1.MinOccurs = New Decimal(0)
                any1.MaxOccurs = Decimal.MaxValue
                any1.ProcessContents = Global.System.Xml.Schema.XmlSchemaContentProcessing.Lax
                sequence.Items.Add(any1)
                Dim any2 As New Global.System.Xml.Schema.XmlSchemaAny()
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1"
                any2.MinOccurs = New Decimal(1)
                any2.ProcessContents = Global.System.Xml.Schema.XmlSchemaContentProcessing.Lax
                sequence.Items.Add(any2)
                Dim attribute1 As New Global.System.Xml.Schema.XmlSchemaAttribute()
                attribute1.Name = "namespace"
                attribute1.FixedValue = ds.Namespace
                type.Attributes.Add(attribute1)
                Dim attribute2 As New Global.System.Xml.Schema.XmlSchemaAttribute()
                attribute2.Name = "tableTypeName"
                attribute2.FixedValue = "ProductsDataTable"
                type.Attributes.Add(attribute2)
                type.Particle = sequence
                Dim dsSchema As Global.System.Xml.Schema.XmlSchema = ds.GetSchemaSerializable()
                If xs.Contains(dsSchema.TargetNamespace) Then
                    Dim s1 As New Global.System.IO.MemoryStream()
                    Dim s2 As New Global.System.IO.MemoryStream()
                    Try
                        Dim schema As Global.System.Xml.Schema.XmlSchema = Nothing
                        dsSchema.Write(s1)
                        Dim schemas As System.Collections.IEnumerator = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator()
                        Do While schemas.MoveNext()
                            schema = (DirectCast(schemas.Current, Global.System.Xml.Schema.XmlSchema))
                            s2.SetLength(0)
                            schema.Write(s2)
                            If (s1.Length = s2.Length) Then
                                s1.Position = 0
                                s2.Position = 0
                                Do While ((s1.Position <> s1.Length) AndAlso (s1.ReadByte() = s2.ReadByte()))

                                Loop
                                If (s1.Position = s1.Length) Then
                                    Return type
                                End If
                            End If
                        Loop
                    Finally
                        If (s1 IsNot Nothing) Then
                            s1.Close()
                        End If
                        If (s2 IsNot Nothing) Then
                            s2.Close()
                        End If
                    End Try
                End If
                xs.Add(dsSchema)
                Return type
            End Function
        End Class




        Partial Public Class OrderDetailsRow
            Inherits System.Data.DataRow

            Private tableOrderDetails As OrderDetailsDataTable

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub New(ByVal rb As Global.System.Data.DataRowBuilder)
                MyBase.New(rb)
                Me.tableOrderDetails = (CType(Me.Table, OrderDetailsDataTable))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property OrderID() As Integer
                Get
                    Try
                        Return (DirectCast(Me(Me.tableOrderDetails.OrderIDColumn), Integer))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'OrderID' in table 'OrderDetails' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As Integer)
                    Me(Me.tableOrderDetails.OrderIDColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property Quantity() As Short
                Get
                    Try
                        Return (DirectCast(Me(Me.tableOrderDetails.QuantityColumn), Short))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'Quantity' in table 'OrderDetails' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As Short)
                    Me(Me.tableOrderDetails.QuantityColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property UnitPrice() As Decimal
                Get
                    Try
                        Return (DirectCast(Me(Me.tableOrderDetails.UnitPriceColumn), Decimal))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'UnitPrice' in table 'OrderDetails' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As Decimal)
                    Me(Me.tableOrderDetails.UnitPriceColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property Discount() As Single
                Get
                    Try
                        Return (DirectCast(Me(Me.tableOrderDetails.DiscountColumn), Single))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'Discount' in table 'OrderDetails' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As Single)
                    Me(Me.tableOrderDetails.DiscountColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ProductName() As String
                Get
                    Try
                        Return (DirectCast(Me(Me.tableOrderDetails.ProductNameColumn), String))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ProductName' in table 'OrderDetails' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As String)
                    Me(Me.tableOrderDetails.ProductNameColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property Supplier() As String
                Get
                    Try
                        Return (DirectCast(Me(Me.tableOrderDetails.SupplierColumn), String))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'Supplier' in table 'OrderDetails' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As String)
                    Me(Me.tableOrderDetails.SupplierColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsOrderIDNull() As Boolean
                Return Me.IsNull(Me.tableOrderDetails.OrderIDColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetOrderIDNull()
                Me(Me.tableOrderDetails.OrderIDColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsQuantityNull() As Boolean
                Return Me.IsNull(Me.tableOrderDetails.QuantityColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetQuantityNull()
                Me(Me.tableOrderDetails.QuantityColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsUnitPriceNull() As Boolean
                Return Me.IsNull(Me.tableOrderDetails.UnitPriceColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetUnitPriceNull()
                Me(Me.tableOrderDetails.UnitPriceColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsDiscountNull() As Boolean
                Return Me.IsNull(Me.tableOrderDetails.DiscountColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetDiscountNull()
                Me(Me.tableOrderDetails.DiscountColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsProductNameNull() As Boolean
                Return Me.IsNull(Me.tableOrderDetails.ProductNameColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetProductNameNull()
                Me(Me.tableOrderDetails.ProductNameColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsSupplierNull() As Boolean
                Return Me.IsNull(Me.tableOrderDetails.SupplierColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetSupplierNull()
                Me(Me.tableOrderDetails.SupplierColumn) = Global.System.Convert.DBNull
            End Sub
        End Class




        Partial Public Class OrdersRow
            Inherits System.Data.DataRow

            Private tableOrders As OrdersDataTable

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub New(ByVal rb As Global.System.Data.DataRowBuilder)
                MyBase.New(rb)
                Me.tableOrders = (CType(Me.Table, OrdersDataTable))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property OrderID() As Integer
                Get
                    Return (DirectCast(Me(Me.tableOrders.OrderIDColumn), Integer))
                End Get
                Set(ByVal value As Integer)
                    Me(Me.tableOrders.OrderIDColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property CustomerID() As String
                Get
                    Try
                        Return (DirectCast(Me(Me.tableOrders.CustomerIDColumn), String))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'CustomerID' in table 'Orders' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As String)
                    Me(Me.tableOrders.CustomerIDColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property EmployeeID() As Integer
                Get
                    Try
                        Return (DirectCast(Me(Me.tableOrders.EmployeeIDColumn), Integer))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'EmployeeID' in table 'Orders' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As Integer)
                    Me(Me.tableOrders.EmployeeIDColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property OrderDate() As Date
                Get
                    Try
                        Return (DirectCast(Me(Me.tableOrders.OrderDateColumn), Global.System.DateTime))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'OrderDate' in table 'Orders' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As Date)
                    Me(Me.tableOrders.OrderDateColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property RequiredDate() As Date
                Get
                    Try
                        Return (DirectCast(Me(Me.tableOrders.RequiredDateColumn), Global.System.DateTime))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'RequiredDate' in table 'Orders' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As Date)
                    Me(Me.tableOrders.RequiredDateColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ShippedDate() As Date
                Get
                    Try
                        Return (DirectCast(Me(Me.tableOrders.ShippedDateColumn), Global.System.DateTime))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ShippedDate' in table 'Orders' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As Date)
                    Me(Me.tableOrders.ShippedDateColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ShipVia() As Integer
                Get
                    Try
                        Return (DirectCast(Me(Me.tableOrders.ShipViaColumn), Integer))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ShipVia' in table 'Orders' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As Integer)
                    Me(Me.tableOrders.ShipViaColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property Freight() As Decimal
                Get
                    Try
                        Return (DirectCast(Me(Me.tableOrders.FreightColumn), Decimal))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'Freight' in table 'Orders' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As Decimal)
                    Me(Me.tableOrders.FreightColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ShipName() As String
                Get
                    Try
                        Return (DirectCast(Me(Me.tableOrders.ShipNameColumn), String))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ShipName' in table 'Orders' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As String)
                    Me(Me.tableOrders.ShipNameColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ShipAddress() As String
                Get
                    Try
                        Return (DirectCast(Me(Me.tableOrders.ShipAddressColumn), String))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ShipAddress' in table 'Orders' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As String)
                    Me(Me.tableOrders.ShipAddressColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ShipCity() As String
                Get
                    Try
                        Return (DirectCast(Me(Me.tableOrders.ShipCityColumn), String))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ShipCity' in table 'Orders' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As String)
                    Me(Me.tableOrders.ShipCityColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ShipRegion() As String
                Get
                    Try
                        Return (DirectCast(Me(Me.tableOrders.ShipRegionColumn), String))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ShipRegion' in table 'Orders' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As String)
                    Me(Me.tableOrders.ShipRegionColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ShipPostalCode() As String
                Get
                    Try
                        Return (DirectCast(Me(Me.tableOrders.ShipPostalCodeColumn), String))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ShipPostalCode' in table 'Orders' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As String)
                    Me(Me.tableOrders.ShipPostalCodeColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ShipCountry() As String
                Get
                    Try
                        Return (DirectCast(Me(Me.tableOrders.ShipCountryColumn), String))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ShipCountry' in table 'Orders' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As String)
                    Me(Me.tableOrders.ShipCountryColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsCustomerIDNull() As Boolean
                Return Me.IsNull(Me.tableOrders.CustomerIDColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetCustomerIDNull()
                Me(Me.tableOrders.CustomerIDColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsEmployeeIDNull() As Boolean
                Return Me.IsNull(Me.tableOrders.EmployeeIDColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetEmployeeIDNull()
                Me(Me.tableOrders.EmployeeIDColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsOrderDateNull() As Boolean
                Return Me.IsNull(Me.tableOrders.OrderDateColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetOrderDateNull()
                Me(Me.tableOrders.OrderDateColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsRequiredDateNull() As Boolean
                Return Me.IsNull(Me.tableOrders.RequiredDateColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetRequiredDateNull()
                Me(Me.tableOrders.RequiredDateColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsShippedDateNull() As Boolean
                Return Me.IsNull(Me.tableOrders.ShippedDateColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetShippedDateNull()
                Me(Me.tableOrders.ShippedDateColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsShipViaNull() As Boolean
                Return Me.IsNull(Me.tableOrders.ShipViaColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetShipViaNull()
                Me(Me.tableOrders.ShipViaColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsFreightNull() As Boolean
                Return Me.IsNull(Me.tableOrders.FreightColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetFreightNull()
                Me(Me.tableOrders.FreightColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsShipNameNull() As Boolean
                Return Me.IsNull(Me.tableOrders.ShipNameColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetShipNameNull()
                Me(Me.tableOrders.ShipNameColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsShipAddressNull() As Boolean
                Return Me.IsNull(Me.tableOrders.ShipAddressColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetShipAddressNull()
                Me(Me.tableOrders.ShipAddressColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsShipCityNull() As Boolean
                Return Me.IsNull(Me.tableOrders.ShipCityColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetShipCityNull()
                Me(Me.tableOrders.ShipCityColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsShipRegionNull() As Boolean
                Return Me.IsNull(Me.tableOrders.ShipRegionColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetShipRegionNull()
                Me(Me.tableOrders.ShipRegionColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsShipPostalCodeNull() As Boolean
                Return Me.IsNull(Me.tableOrders.ShipPostalCodeColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetShipPostalCodeNull()
                Me(Me.tableOrders.ShipPostalCodeColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsShipCountryNull() As Boolean
                Return Me.IsNull(Me.tableOrders.ShipCountryColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetShipCountryNull()
                Me(Me.tableOrders.ShipCountryColumn) = Global.System.Convert.DBNull
            End Sub
        End Class




        Partial Public Class ProductsRow
            Inherits System.Data.DataRow

            Private tableProducts As ProductsDataTable

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub New(ByVal rb As Global.System.Data.DataRowBuilder)
                MyBase.New(rb)
                Me.tableProducts = (CType(Me.Table, ProductsDataTable))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ProductID() As Integer
                Get
                    Return (DirectCast(Me(Me.tableProducts.ProductIDColumn), Integer))
                End Get
                Set(ByVal value As Integer)
                    Me(Me.tableProducts.ProductIDColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ProductName() As String
                Get
                    Try
                        Return (DirectCast(Me(Me.tableProducts.ProductNameColumn), String))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ProductName' in table 'Products' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As String)
                    Me(Me.tableProducts.ProductNameColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property SupplierID() As Integer
                Get
                    Try
                        Return (DirectCast(Me(Me.tableProducts.SupplierIDColumn), Integer))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'SupplierID' in table 'Products' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As Integer)
                    Me(Me.tableProducts.SupplierIDColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property CategoryID() As Integer
                Get
                    Try
                        Return (DirectCast(Me(Me.tableProducts.CategoryIDColumn), Integer))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'CategoryID' in table 'Products' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As Integer)
                    Me(Me.tableProducts.CategoryIDColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property QuantityPerUnit() As String
                Get
                    Try
                        Return (DirectCast(Me(Me.tableProducts.QuantityPerUnitColumn), String))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'QuantityPerUnit' in table 'Products' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As String)
                    Me(Me.tableProducts.QuantityPerUnitColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property UnitPrice() As Decimal
                Get
                    Try
                        Return (DirectCast(Me(Me.tableProducts.UnitPriceColumn), Decimal))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'UnitPrice' in table 'Products' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As Decimal)
                    Me(Me.tableProducts.UnitPriceColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property UnitsInStock() As Short
                Get
                    Try
                        Return (DirectCast(Me(Me.tableProducts.UnitsInStockColumn), Short))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'UnitsInStock' in table 'Products' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As Short)
                    Me(Me.tableProducts.UnitsInStockColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property UnitsOnOrder() As Short
                Get
                    Try
                        Return (DirectCast(Me(Me.tableProducts.UnitsOnOrderColumn), Short))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'UnitsOnOrder' in table 'Products' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As Short)
                    Me(Me.tableProducts.UnitsOnOrderColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ReorderLevel() As Short
                Get
                    Try
                        Return (DirectCast(Me(Me.tableProducts.ReorderLevelColumn), Short))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ReorderLevel' in table 'Products' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As Short)
                    Me(Me.tableProducts.ReorderLevelColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property Discontinued() As Boolean
                Get
                    Try
                        Return (DirectCast(Me(Me.tableProducts.DiscontinuedColumn), Boolean))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'Discontinued' in table 'Products' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As Boolean)
                    Me(Me.tableProducts.DiscontinuedColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property EAN13() As String
                Get
                    Try
                        Return (DirectCast(Me(Me.tableProducts.EAN13Column), String))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'EAN13' in table 'Products' is DBNull.", e)
                    End Try
                End Get
                Set(ByVal value As String)
                    Me(Me.tableProducts.EAN13Column) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsProductNameNull() As Boolean
                Return Me.IsNull(Me.tableProducts.ProductNameColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetProductNameNull()
                Me(Me.tableProducts.ProductNameColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsSupplierIDNull() As Boolean
                Return Me.IsNull(Me.tableProducts.SupplierIDColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetSupplierIDNull()
                Me(Me.tableProducts.SupplierIDColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsCategoryIDNull() As Boolean
                Return Me.IsNull(Me.tableProducts.CategoryIDColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetCategoryIDNull()
                Me(Me.tableProducts.CategoryIDColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsQuantityPerUnitNull() As Boolean
                Return Me.IsNull(Me.tableProducts.QuantityPerUnitColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetQuantityPerUnitNull()
                Me(Me.tableProducts.QuantityPerUnitColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsUnitPriceNull() As Boolean
                Return Me.IsNull(Me.tableProducts.UnitPriceColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetUnitPriceNull()
                Me(Me.tableProducts.UnitPriceColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsUnitsInStockNull() As Boolean
                Return Me.IsNull(Me.tableProducts.UnitsInStockColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetUnitsInStockNull()
                Me(Me.tableProducts.UnitsInStockColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsUnitsOnOrderNull() As Boolean
                Return Me.IsNull(Me.tableProducts.UnitsOnOrderColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetUnitsOnOrderNull()
                Me(Me.tableProducts.UnitsOnOrderColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsReorderLevelNull() As Boolean
                Return Me.IsNull(Me.tableProducts.ReorderLevelColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetReorderLevelNull()
                Me(Me.tableProducts.ReorderLevelColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsDiscontinuedNull() As Boolean
                Return Me.IsNull(Me.tableProducts.DiscontinuedColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetDiscontinuedNull()
                Me(Me.tableProducts.DiscontinuedColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsEAN13Null() As Boolean
                Return Me.IsNull(Me.tableProducts.EAN13Column)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetEAN13Null()
                Me(Me.tableProducts.EAN13Column) = Global.System.Convert.DBNull
            End Sub
        End Class




        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Class OrderDetailsRowChangeEvent
            Inherits System.EventArgs

            Private eventRow As OrderDetailsRow

            Private eventAction As Global.System.Data.DataRowAction

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub New(ByVal row As OrderDetailsRow, ByVal action As Global.System.Data.DataRowAction)
                Me.eventRow = row
                Me.eventAction = action
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property Row() As OrderDetailsRow
                Get
                    Return Me.eventRow
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property Action() As Global.System.Data.DataRowAction
                Get
                    Return Me.eventAction
                End Get
            End Property
        End Class




        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Class OrdersRowChangeEvent
            Inherits System.EventArgs

            Private eventRow As OrdersRow

            Private eventAction As Global.System.Data.DataRowAction

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub New(ByVal row As OrdersRow, ByVal action As Global.System.Data.DataRowAction)
                Me.eventRow = row
                Me.eventAction = action
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property Row() As OrdersRow
                Get
                    Return Me.eventRow
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property Action() As Global.System.Data.DataRowAction
                Get
                    Return Me.eventAction
                End Get
            End Property
        End Class




        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Class ProductsRowChangeEvent
            Inherits System.EventArgs

            Private eventRow As ProductsRow

            Private eventAction As Global.System.Data.DataRowAction

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub New(ByVal row As ProductsRow, ByVal action As Global.System.Data.DataRowAction)
                Me.eventRow = row
                Me.eventAction = action
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property Row() As ProductsRow
                Get
                    Return Me.eventRow
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property Action() As Global.System.Data.DataRowAction
                Get
                    Return Me.eventAction
                End Get
            End Property
        End Class
    End Class
End Namespace
Namespace Modules.DataBinding.nwindOrdersTableAdapters





    <Global.System.ComponentModel.DesignerCategoryAttribute("code"), Global.System.ComponentModel.ToolboxItem(True), Global.System.ComponentModel.DataObjectAttribute(True), Global.System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner" & ", Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
    Partial Public Class OrderDetailsTableAdapter
        Inherits System.ComponentModel.Component

        Private _adapter As Global.System.Data.OleDb.OleDbDataAdapter

        Private _connection As Global.System.Data.OleDb.OleDbConnection

        Private _transaction As Global.System.Data.OleDb.OleDbTransaction

        Private _commandCollection() As Global.System.Data.OleDb.OleDbCommand

        Private _clearBeforeFill As Boolean

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Sub New()
            Me.ClearBeforeFill = True
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Friend ReadOnly Property Adapter() As Global.System.Data.OleDb.OleDbDataAdapter
            Get
                If (Me._adapter Is Nothing) Then
                    Me.InitAdapter()
                End If
                Return Me._adapter
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Friend Property Connection() As Global.System.Data.OleDb.OleDbConnection
            Get
                If (Me._connection Is Nothing) Then
                    Me.InitConnection()
                End If
                Return Me._connection
            End Get
            Set(ByVal value As System.Data.OleDb.OleDbConnection)
                Me._connection = value
                If (Me.Adapter.InsertCommand IsNot Nothing) Then
                    Me.Adapter.InsertCommand.Connection = value
                End If
                If (Me.Adapter.DeleteCommand IsNot Nothing) Then
                    Me.Adapter.DeleteCommand.Connection = value
                End If
                If (Me.Adapter.UpdateCommand IsNot Nothing) Then
                    Me.Adapter.UpdateCommand.Connection = value
                End If
                Dim i As Integer = 0
                Do While (i < Me.CommandCollection.Length)
                    If (Me.CommandCollection(i) IsNot Nothing) Then
                        CType(Me.CommandCollection(i), Global.System.Data.OleDb.OleDbCommand).Connection = value
                    End If
                    i = (i + 1)
                Loop
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Friend Property Transaction() As Global.System.Data.OleDb.OleDbTransaction
            Get
                Return Me._transaction
            End Get
            Set(ByVal value As System.Data.OleDb.OleDbTransaction)
                Me._transaction = value
                Dim i As Integer = 0
                Do While (i < Me.CommandCollection.Length)
                    Me.CommandCollection(i).Transaction = Me._transaction
                    i = (i + 1)
                Loop
                If ((Me.Adapter IsNot Nothing) AndAlso (Me.Adapter.DeleteCommand IsNot Nothing)) Then
                    Me.Adapter.DeleteCommand.Transaction = Me._transaction
                End If
                If ((Me.Adapter IsNot Nothing) AndAlso (Me.Adapter.InsertCommand IsNot Nothing)) Then
                    Me.Adapter.InsertCommand.Transaction = Me._transaction
                End If
                If ((Me.Adapter IsNot Nothing) AndAlso (Me.Adapter.UpdateCommand IsNot Nothing)) Then
                    Me.Adapter.UpdateCommand.Transaction = Me._transaction
                End If
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected ReadOnly Property CommandCollection() As Global.System.Data.OleDb.OleDbCommand()
            Get
                If (Me._commandCollection Is Nothing) Then
                    Me.InitCommandCollection()
                End If
                Return Me._commandCollection
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Property ClearBeforeFill() As Boolean
            Get
                Return Me._clearBeforeFill
            End Get
            Set(ByVal value As Boolean)
                Me._clearBeforeFill = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitAdapter()
            Me._adapter = New Global.System.Data.OleDb.OleDbDataAdapter()
            Dim tableMapping As New Global.System.Data.Common.DataTableMapping()
            tableMapping.SourceTable = "Table"
            tableMapping.DataSetTable = "OrderDetails"
            tableMapping.ColumnMappings.Add("OrderID", "OrderID")
            tableMapping.ColumnMappings.Add("Quantity", "Quantity")
            tableMapping.ColumnMappings.Add("UnitPrice", "UnitPrice")
            tableMapping.ColumnMappings.Add("Discount", "Discount")
            tableMapping.ColumnMappings.Add("ProductName", "ProductName")
            tableMapping.ColumnMappings.Add("Supplier", "Supplier")
            Me._adapter.TableMappings.Add(tableMapping)
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitConnection()
            Me._connection = New Global.System.Data.OleDb.OleDbConnection()
            Me._connection.ConnectionString = My.Settings.Default.nwindConnectionString
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitCommandCollection()
            Me._commandCollection = New Global.System.Data.OleDb.OleDbCommand(0){}
            Me._commandCollection(0) = New Global.System.Data.OleDb.OleDbCommand()
            Me._commandCollection(0).Connection = Me.Connection
            Me._commandCollection(0).CommandText = "SELECT OrderID, Quantity, UnitPrice, Discount, ProductName, Supplier FROM OrderDe" & "tails"
            Me._commandCollection(0).CommandType = Global.System.Data.CommandType.Text
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Fill, True)>
        Public Overridable Function Fill(ByVal dataTable As nwindOrders.OrderDetailsDataTable) As Integer
            Me.Adapter.SelectCommand = Me.CommandCollection(0)
            If (Me.ClearBeforeFill = True) Then
                dataTable.Clear()
            End If
            Dim returnValue As Integer = Me.Adapter.Fill(dataTable)
            Return returnValue
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Select, True)>
        Public Overridable Function GetData() As nwindOrders.OrderDetailsDataTable
            Me.Adapter.SelectCommand = Me.CommandCollection(0)
            Dim dataTable As New nwindOrders.OrderDetailsDataTable()
            Me.Adapter.Fill(dataTable)
            Return dataTable
        End Function
    End Class




    <Global.System.ComponentModel.DesignerCategoryAttribute("code"), Global.System.ComponentModel.ToolboxItem(True), Global.System.ComponentModel.DataObjectAttribute(True), Global.System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner" & ", Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
    Partial Public Class OrdersTableAdapter
        Inherits System.ComponentModel.Component

        Private _adapter As Global.System.Data.OleDb.OleDbDataAdapter

        Private _connection As Global.System.Data.OleDb.OleDbConnection

        Private _transaction As Global.System.Data.OleDb.OleDbTransaction

        Private _commandCollection() As Global.System.Data.OleDb.OleDbCommand

        Private _clearBeforeFill As Boolean

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Sub New()
            Me.ClearBeforeFill = True
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Friend ReadOnly Property Adapter() As Global.System.Data.OleDb.OleDbDataAdapter
            Get
                If (Me._adapter Is Nothing) Then
                    Me.InitAdapter()
                End If
                Return Me._adapter
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Friend Property Connection() As Global.System.Data.OleDb.OleDbConnection
            Get
                If (Me._connection Is Nothing) Then
                    Me.InitConnection()
                End If
                Return Me._connection
            End Get
            Set(ByVal value As System.Data.OleDb.OleDbConnection)
                Me._connection = value
                If (Me.Adapter.InsertCommand IsNot Nothing) Then
                    Me.Adapter.InsertCommand.Connection = value
                End If
                If (Me.Adapter.DeleteCommand IsNot Nothing) Then
                    Me.Adapter.DeleteCommand.Connection = value
                End If
                If (Me.Adapter.UpdateCommand IsNot Nothing) Then
                    Me.Adapter.UpdateCommand.Connection = value
                End If
                Dim i As Integer = 0
                Do While (i < Me.CommandCollection.Length)
                    If (Me.CommandCollection(i) IsNot Nothing) Then
                        CType(Me.CommandCollection(i), Global.System.Data.OleDb.OleDbCommand).Connection = value
                    End If
                    i = (i + 1)
                Loop
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Friend Property Transaction() As Global.System.Data.OleDb.OleDbTransaction
            Get
                Return Me._transaction
            End Get
            Set(ByVal value As System.Data.OleDb.OleDbTransaction)
                Me._transaction = value
                Dim i As Integer = 0
                Do While (i < Me.CommandCollection.Length)
                    Me.CommandCollection(i).Transaction = Me._transaction
                    i = (i + 1)
                Loop
                If ((Me.Adapter IsNot Nothing) AndAlso (Me.Adapter.DeleteCommand IsNot Nothing)) Then
                    Me.Adapter.DeleteCommand.Transaction = Me._transaction
                End If
                If ((Me.Adapter IsNot Nothing) AndAlso (Me.Adapter.InsertCommand IsNot Nothing)) Then
                    Me.Adapter.InsertCommand.Transaction = Me._transaction
                End If
                If ((Me.Adapter IsNot Nothing) AndAlso (Me.Adapter.UpdateCommand IsNot Nothing)) Then
                    Me.Adapter.UpdateCommand.Transaction = Me._transaction
                End If
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected ReadOnly Property CommandCollection() As Global.System.Data.OleDb.OleDbCommand()
            Get
                If (Me._commandCollection Is Nothing) Then
                    Me.InitCommandCollection()
                End If
                Return Me._commandCollection
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Property ClearBeforeFill() As Boolean
            Get
                Return Me._clearBeforeFill
            End Get
            Set(ByVal value As Boolean)
                Me._clearBeforeFill = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitAdapter()
            Me._adapter = New Global.System.Data.OleDb.OleDbDataAdapter()
            Dim tableMapping As New Global.System.Data.Common.DataTableMapping()
            tableMapping.SourceTable = "Table"
            tableMapping.DataSetTable = "Orders"
            tableMapping.ColumnMappings.Add("OrderID", "OrderID")
            tableMapping.ColumnMappings.Add("CustomerID", "CustomerID")
            tableMapping.ColumnMappings.Add("EmployeeID", "EmployeeID")
            tableMapping.ColumnMappings.Add("OrderDate", "OrderDate")
            tableMapping.ColumnMappings.Add("RequiredDate", "RequiredDate")
            tableMapping.ColumnMappings.Add("ShippedDate", "ShippedDate")
            tableMapping.ColumnMappings.Add("ShipVia", "ShipVia")
            tableMapping.ColumnMappings.Add("Freight", "Freight")
            tableMapping.ColumnMappings.Add("ShipName", "ShipName")
            tableMapping.ColumnMappings.Add("ShipAddress", "ShipAddress")
            tableMapping.ColumnMappings.Add("ShipCity", "ShipCity")
            tableMapping.ColumnMappings.Add("ShipRegion", "ShipRegion")
            tableMapping.ColumnMappings.Add("ShipPostalCode", "ShipPostalCode")
            tableMapping.ColumnMappings.Add("ShipCountry", "ShipCountry")
            Me._adapter.TableMappings.Add(tableMapping)
            Me._adapter.DeleteCommand = New Global.System.Data.OleDb.OleDbCommand()
            Me._adapter.DeleteCommand.Connection = Me.Connection
            Me._adapter.DeleteCommand.CommandText = "DELETE FROM `Orders` WHERE ((`OrderID` = ?) AND ((? = 1 AND `CustomerID` IS NULL) OR (`CustomerID` = ?)) AND ((? = 1 AND `EmployeeID` IS NULL) OR (`EmployeeID` = ?)) AND ((? = 1 AND `OrderDate` IS NULL) OR (`OrderDate` = ?)) AND ((? = 1 AND `RequiredDate` IS NULL) OR (`RequiredDate` = ?)) AND ((? = 1 AND `ShippedDate` IS NULL) OR (`ShippedDate` = ?)) AND ((? = 1 AND `ShipVia` IS NULL) OR (`ShipVia` = ?)) AND ((? = 1 AND `Freight` IS NULL) OR (`Freight` = ?)) AND ((? = 1 AND `ShipName` IS NULL) OR (`ShipName` = ?)) AND ((? = 1 AND `ShipAddress` IS NULL) OR (`ShipAddress` = ?)) AND ((? = 1 AND `ShipCity` IS NULL) OR (`ShipCity` = ?)) AND ((? = 1 AND `ShipRegion` IS NULL) OR (`ShipRegion` = ?)) AND ((? = 1 AND `ShipPostalCode` IS NULL) OR (`ShipPostalCode` = ?)) AND ((? = 1 AND `ShipCountry` IS NULL) OR (`ShipCountry` = ?)))"
            Me._adapter.DeleteCommand.CommandType = Global.System.Data.CommandType.Text
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_OrderID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "OrderID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_CustomerID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "CustomerID", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_CustomerID", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "CustomerID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_EmployeeID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "EmployeeID", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_EmployeeID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "EmployeeID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_OrderDate", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "OrderDate", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_OrderDate", Global.System.Data.OleDb.OleDbType.Date, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "OrderDate", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_RequiredDate", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "RequiredDate", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_RequiredDate", Global.System.Data.OleDb.OleDbType.Date, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "RequiredDate", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShippedDate", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShippedDate", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShippedDate", Global.System.Data.OleDb.OleDbType.Date, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShippedDate", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipVia", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipVia", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipVia", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipVia", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_Freight", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "Freight", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_Freight", Global.System.Data.OleDb.OleDbType.Currency, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "Freight", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipName", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipName", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipName", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipName", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipAddress", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipAddress", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipAddress", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipAddress", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipCity", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipCity", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipCity", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipCity", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipRegion", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipRegion", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipRegion", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipRegion", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipPostalCode", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipPostalCode", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipPostalCode", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipPostalCode", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipCountry", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipCountry", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipCountry", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipCountry", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.InsertCommand = New Global.System.Data.OleDb.OleDbCommand()
            Me._adapter.InsertCommand.Connection = Me.Connection
            Me._adapter.InsertCommand.CommandText = "INSERT INTO `Orders` (`CustomerID`, `EmployeeID`, `OrderDate`, `RequiredDate`, `S" & "hippedDate`, `ShipVia`, `Freight`, `ShipName`, `ShipAddress`, `ShipCity`, `ShipR" & "egion`, `ShipPostalCode`, `ShipCountry`) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?" & ", ?, ?)"
            Me._adapter.InsertCommand.CommandType = Global.System.Data.CommandType.Text
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("CustomerID", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "CustomerID", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("EmployeeID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "EmployeeID", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("OrderDate", Global.System.Data.OleDb.OleDbType.Date, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "OrderDate", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("RequiredDate", Global.System.Data.OleDb.OleDbType.Date, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "RequiredDate", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShippedDate", Global.System.Data.OleDb.OleDbType.Date, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShippedDate", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipVia", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipVia", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Freight", Global.System.Data.OleDb.OleDbType.Currency, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "Freight", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipName", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipName", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipAddress", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipAddress", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipCity", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipCity", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipRegion", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipRegion", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipPostalCode", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipPostalCode", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipCountry", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipCountry", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand = New Global.System.Data.OleDb.OleDbCommand()
            Me._adapter.UpdateCommand.Connection = Me.Connection
            Me._adapter.UpdateCommand.CommandText = "UPDATE `Orders` SET `CustomerID` = ?, `EmployeeID` = ?, `OrderDate` = ?, `RequiredDate` = ?, `ShippedDate` = ?, `ShipVia` = ?, `Freight` = ?, `ShipName` = ?, `ShipAddress` = ?, `ShipCity` = ?, `ShipRegion` = ?, `ShipPostalCode` = ?, `ShipCountry` = ? WHERE ((`OrderID` = ?) AND ((? = 1 AND `CustomerID` IS NULL) OR (`CustomerID` = ?)) AND ((? = 1 AND `EmployeeID` IS NULL) OR (`EmployeeID` = ?)) AND ((? = 1 AND `OrderDate` IS NULL) OR (`OrderDate` = ?)) AND ((? = 1 AND `RequiredDate` IS NULL) OR (`RequiredDate` = ?)) AND ((? = 1 AND `ShippedDate` IS NULL) OR (`ShippedDate` = ?)) AND ((? = 1 AND `ShipVia` IS NULL) OR (`ShipVia` = ?)) AND ((? = 1 AND `Freight` IS NULL) OR (`Freight` = ?)) AND ((? = 1 AND `ShipName` IS NULL) OR (`ShipName` = ?)) AND ((? = 1 AND `ShipAddress` IS NULL) OR (`ShipAddress` = ?)) AND ((? = 1 AND `ShipCity` IS NULL) OR (`ShipCity` = ?)) AND ((? = 1 AND `ShipRegion` IS NULL) OR (`ShipRegion` = ?)) AND ((? = 1 AND `ShipPostalCode` IS NULL) OR (`ShipPostalCode` = ?)) AND ((? = 1 AND `ShipCountry` IS NULL) OR (`ShipCountry` = ?)))"
            Me._adapter.UpdateCommand.CommandType = Global.System.Data.CommandType.Text
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("CustomerID", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "CustomerID", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("EmployeeID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "EmployeeID", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("OrderDate", Global.System.Data.OleDb.OleDbType.Date, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "OrderDate", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("RequiredDate", Global.System.Data.OleDb.OleDbType.Date, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "RequiredDate", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShippedDate", Global.System.Data.OleDb.OleDbType.Date, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShippedDate", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipVia", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipVia", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Freight", Global.System.Data.OleDb.OleDbType.Currency, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "Freight", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipName", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipName", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipAddress", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipAddress", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipCity", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipCity", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipRegion", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipRegion", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipPostalCode", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipPostalCode", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipCountry", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipCountry", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_OrderID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "OrderID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_CustomerID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "CustomerID", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_CustomerID", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "CustomerID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_EmployeeID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "EmployeeID", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_EmployeeID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "EmployeeID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_OrderDate", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "OrderDate", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_OrderDate", Global.System.Data.OleDb.OleDbType.Date, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "OrderDate", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_RequiredDate", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "RequiredDate", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_RequiredDate", Global.System.Data.OleDb.OleDbType.Date, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "RequiredDate", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShippedDate", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShippedDate", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShippedDate", Global.System.Data.OleDb.OleDbType.Date, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShippedDate", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipVia", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipVia", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipVia", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipVia", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_Freight", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "Freight", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_Freight", Global.System.Data.OleDb.OleDbType.Currency, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "Freight", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipName", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipName", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipName", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipName", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipAddress", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipAddress", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipAddress", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipAddress", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipCity", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipCity", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipCity", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipCity", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipRegion", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipRegion", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipRegion", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipRegion", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipPostalCode", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipPostalCode", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipPostalCode", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipPostalCode", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipCountry", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipCountry", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipCountry", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ShipCountry", Global.System.Data.DataRowVersion.Original, False, Nothing))
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitConnection()
            Me._connection = New Global.System.Data.OleDb.OleDbConnection()
            Me._connection.ConnectionString = My.Settings.Default.nwindConnectionString
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitCommandCollection()
            Me._commandCollection = New Global.System.Data.OleDb.OleDbCommand(0){}
            Me._commandCollection(0) = New Global.System.Data.OleDb.OleDbCommand()
            Me._commandCollection(0).Connection = Me.Connection
            Me._commandCollection(0).CommandText = "SELECT OrderID, CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate, Shi" & "pVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, Ship" & "Country FROM Orders"
            Me._commandCollection(0).CommandType = Global.System.Data.CommandType.Text
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Fill, True)>
        Public Overridable Function Fill(ByVal dataTable As nwindOrders.OrdersDataTable) As Integer
            Me.Adapter.SelectCommand = Me.CommandCollection(0)
            If (Me.ClearBeforeFill = True) Then
                dataTable.Clear()
            End If
            Dim returnValue As Integer = Me.Adapter.Fill(dataTable)
            Return returnValue
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Select, True)>
        Public Overridable Function GetData() As nwindOrders.OrdersDataTable
            Me.Adapter.SelectCommand = Me.CommandCollection(0)
            Dim dataTable As New nwindOrders.OrdersDataTable()
            Me.Adapter.Fill(dataTable)
            Return dataTable
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        Public Overridable Function Update(ByVal dataTable As nwindOrders.OrdersDataTable) As Integer
            Return Me.Adapter.Update(dataTable)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        Public Overridable Function Update(ByVal dataSet As nwindOrders) As Integer
            Return Me.Adapter.Update(dataSet, "Orders")
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        Public Overridable Function Update(ByVal dataRow As Global.System.Data.DataRow) As Integer
            Return Me.Adapter.Update(New Global.System.Data.DataRow() { dataRow})
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        Public Overridable Function Update(ByVal dataRows() As Global.System.Data.DataRow) As Integer
            Return Me.Adapter.Update(dataRows)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Delete, True)>
        Public Overridable Function Delete(ByVal Original_OrderID As Integer, ByVal Original_CustomerID As String, ByVal Original_EmployeeID? As Integer, ByVal Original_OrderDate? As Global.System.DateTime, ByVal Original_RequiredDate? As Global.System.DateTime, ByVal Original_ShippedDate? As Global.System.DateTime, ByVal Original_ShipVia? As Integer, ByVal Original_Freight? As Decimal, ByVal Original_ShipName As String, ByVal Original_ShipAddress As String, ByVal Original_ShipCity As String, ByVal Original_ShipRegion As String, ByVal Original_ShipPostalCode As String, ByVal Original_ShipCountry As String) As Integer
            Me.Adapter.DeleteCommand.Parameters(0).Value = (CInt(Original_OrderID))
            If (Original_CustomerID Is Nothing) Then
                Me.Adapter.DeleteCommand.Parameters(1).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(2).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.DeleteCommand.Parameters(1).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(2).Value = (CStr(Original_CustomerID))
            End If
            If (Original_EmployeeID.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(3).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(4).Value = (CInt(Original_EmployeeID.Value))
            Else
                Me.Adapter.DeleteCommand.Parameters(3).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(4).Value = Global.System.DBNull.Value
            End If
            If (Original_OrderDate.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(5).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(6).Value = (CDate(Original_OrderDate.Value))
            Else
                Me.Adapter.DeleteCommand.Parameters(5).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(6).Value = Global.System.DBNull.Value
            End If
            If (Original_RequiredDate.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(7).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(8).Value = (CDate(Original_RequiredDate.Value))
            Else
                Me.Adapter.DeleteCommand.Parameters(7).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(8).Value = Global.System.DBNull.Value
            End If
            If (Original_ShippedDate.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(9).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(10).Value = (CDate(Original_ShippedDate.Value))
            Else
                Me.Adapter.DeleteCommand.Parameters(9).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(10).Value = Global.System.DBNull.Value
            End If
            If (Original_ShipVia.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(11).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(12).Value = (CInt(Original_ShipVia.Value))
            Else
                Me.Adapter.DeleteCommand.Parameters(11).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(12).Value = Global.System.DBNull.Value
            End If
            If (Original_Freight.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(13).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(14).Value = (CDec(Original_Freight.Value))
            Else
                Me.Adapter.DeleteCommand.Parameters(13).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(14).Value = Global.System.DBNull.Value
            End If
            If (Original_ShipName Is Nothing) Then
                Me.Adapter.DeleteCommand.Parameters(15).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(16).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.DeleteCommand.Parameters(15).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(16).Value = (CStr(Original_ShipName))
            End If
            If (Original_ShipAddress Is Nothing) Then
                Me.Adapter.DeleteCommand.Parameters(17).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(18).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.DeleteCommand.Parameters(17).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(18).Value = (CStr(Original_ShipAddress))
            End If
            If (Original_ShipCity Is Nothing) Then
                Me.Adapter.DeleteCommand.Parameters(19).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(20).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.DeleteCommand.Parameters(19).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(20).Value = (CStr(Original_ShipCity))
            End If
            If (Original_ShipRegion Is Nothing) Then
                Me.Adapter.DeleteCommand.Parameters(21).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(22).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.DeleteCommand.Parameters(21).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(22).Value = (CStr(Original_ShipRegion))
            End If
            If (Original_ShipPostalCode Is Nothing) Then
                Me.Adapter.DeleteCommand.Parameters(23).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(24).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.DeleteCommand.Parameters(23).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(24).Value = (CStr(Original_ShipPostalCode))
            End If
            If (Original_ShipCountry Is Nothing) Then
                Me.Adapter.DeleteCommand.Parameters(25).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(26).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.DeleteCommand.Parameters(25).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(26).Value = (CStr(Original_ShipCountry))
            End If
            Dim previousConnectionState As Global.System.Data.ConnectionState = Me.Adapter.DeleteCommand.Connection.State
            If (CInt(Me.Adapter.DeleteCommand.Connection.State And Global.System.Data.ConnectionState.Open) <> CInt(Global.System.Data.ConnectionState.Open)) Then
                Me.Adapter.DeleteCommand.Connection.Open()
            End If
            Try
                Dim returnValue As Integer = Me.Adapter.DeleteCommand.ExecuteNonQuery()
                Return returnValue
            Finally
                If (previousConnectionState = Global.System.Data.ConnectionState.Closed) Then
                    Me.Adapter.DeleteCommand.Connection.Close()
                End If
            End Try
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Insert, True)>
        Public Overridable Function Insert(ByVal CustomerID As String, ByVal EmployeeID? As Integer, ByVal OrderDate? As Global.System.DateTime, ByVal RequiredDate? As Global.System.DateTime, ByVal ShippedDate? As Global.System.DateTime, ByVal ShipVia? As Integer, ByVal Freight? As Decimal, ByVal ShipName As String, ByVal ShipAddress As String, ByVal ShipCity As String, ByVal ShipRegion As String, ByVal ShipPostalCode As String, ByVal ShipCountry As String) As Integer
            If (CustomerID Is Nothing) Then
                Me.Adapter.InsertCommand.Parameters(0).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(0).Value = (CStr(CustomerID))
            End If
            If (EmployeeID.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(1).Value = (CInt(EmployeeID.Value))
            Else
                Me.Adapter.InsertCommand.Parameters(1).Value = Global.System.DBNull.Value
            End If
            If (OrderDate.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(2).Value = (CDate(OrderDate.Value))
            Else
                Me.Adapter.InsertCommand.Parameters(2).Value = Global.System.DBNull.Value
            End If
            If (RequiredDate.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(3).Value = (CDate(RequiredDate.Value))
            Else
                Me.Adapter.InsertCommand.Parameters(3).Value = Global.System.DBNull.Value
            End If
            If (ShippedDate.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(4).Value = (CDate(ShippedDate.Value))
            Else
                Me.Adapter.InsertCommand.Parameters(4).Value = Global.System.DBNull.Value
            End If
            If (ShipVia.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(5).Value = (CInt(ShipVia.Value))
            Else
                Me.Adapter.InsertCommand.Parameters(5).Value = Global.System.DBNull.Value
            End If
            If (Freight.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(6).Value = (CDec(Freight.Value))
            Else
                Me.Adapter.InsertCommand.Parameters(6).Value = Global.System.DBNull.Value
            End If
            If (ShipName Is Nothing) Then
                Me.Adapter.InsertCommand.Parameters(7).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(7).Value = (CStr(ShipName))
            End If
            If (ShipAddress Is Nothing) Then
                Me.Adapter.InsertCommand.Parameters(8).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(8).Value = (CStr(ShipAddress))
            End If
            If (ShipCity Is Nothing) Then
                Me.Adapter.InsertCommand.Parameters(9).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(9).Value = (CStr(ShipCity))
            End If
            If (ShipRegion Is Nothing) Then
                Me.Adapter.InsertCommand.Parameters(10).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(10).Value = (CStr(ShipRegion))
            End If
            If (ShipPostalCode Is Nothing) Then
                Me.Adapter.InsertCommand.Parameters(11).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(11).Value = (CStr(ShipPostalCode))
            End If
            If (ShipCountry Is Nothing) Then
                Me.Adapter.InsertCommand.Parameters(12).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(12).Value = (CStr(ShipCountry))
            End If
            Dim previousConnectionState As Global.System.Data.ConnectionState = Me.Adapter.InsertCommand.Connection.State
            If (CInt(Me.Adapter.InsertCommand.Connection.State And Global.System.Data.ConnectionState.Open) <> CInt(Global.System.Data.ConnectionState.Open)) Then
                Me.Adapter.InsertCommand.Connection.Open()
            End If
            Try
                Dim returnValue As Integer = Me.Adapter.InsertCommand.ExecuteNonQuery()
                Return returnValue
            Finally
                If (previousConnectionState = Global.System.Data.ConnectionState.Closed) Then
                    Me.Adapter.InsertCommand.Connection.Close()
                End If
            End Try
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Update, True)>
        Public Overridable Function Update(ByVal CustomerID As String, ByVal EmployeeID? As Integer, ByVal OrderDate? As Global.System.DateTime, ByVal RequiredDate? As Global.System.DateTime, ByVal ShippedDate? As Global.System.DateTime, ByVal ShipVia? As Integer, ByVal Freight? As Decimal, ByVal ShipName As String, ByVal ShipAddress As String, ByVal ShipCity As String, ByVal ShipRegion As String, ByVal ShipPostalCode As String, ByVal ShipCountry As String, ByVal Original_OrderID As Integer, ByVal Original_CustomerID As String, ByVal Original_EmployeeID? As Integer, ByVal Original_OrderDate? As Global.System.DateTime, ByVal Original_RequiredDate? As Global.System.DateTime, ByVal Original_ShippedDate? As Global.System.DateTime, ByVal Original_ShipVia? As Integer, ByVal Original_Freight? As Decimal, ByVal Original_ShipName As String, ByVal Original_ShipAddress As String, ByVal Original_ShipCity As String, ByVal Original_ShipRegion As String, ByVal Original_ShipPostalCode As String, ByVal Original_ShipCountry As String) As Integer
            If (CustomerID Is Nothing) Then
                Me.Adapter.UpdateCommand.Parameters(0).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(0).Value = (CStr(CustomerID))
            End If
            If (EmployeeID.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(1).Value = (CInt(EmployeeID.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(1).Value = Global.System.DBNull.Value
            End If
            If (OrderDate.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(2).Value = (CDate(OrderDate.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(2).Value = Global.System.DBNull.Value
            End If
            If (RequiredDate.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(3).Value = (CDate(RequiredDate.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(3).Value = Global.System.DBNull.Value
            End If
            If (ShippedDate.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(4).Value = (CDate(ShippedDate.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(4).Value = Global.System.DBNull.Value
            End If
            If (ShipVia.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(5).Value = (CInt(ShipVia.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(5).Value = Global.System.DBNull.Value
            End If
            If (Freight.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(6).Value = (CDec(Freight.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(6).Value = Global.System.DBNull.Value
            End If
            If (ShipName Is Nothing) Then
                Me.Adapter.UpdateCommand.Parameters(7).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(7).Value = (CStr(ShipName))
            End If
            If (ShipAddress Is Nothing) Then
                Me.Adapter.UpdateCommand.Parameters(8).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(8).Value = (CStr(ShipAddress))
            End If
            If (ShipCity Is Nothing) Then
                Me.Adapter.UpdateCommand.Parameters(9).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(9).Value = (CStr(ShipCity))
            End If
            If (ShipRegion Is Nothing) Then
                Me.Adapter.UpdateCommand.Parameters(10).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(10).Value = (CStr(ShipRegion))
            End If
            If (ShipPostalCode Is Nothing) Then
                Me.Adapter.UpdateCommand.Parameters(11).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(11).Value = (CStr(ShipPostalCode))
            End If
            If (ShipCountry Is Nothing) Then
                Me.Adapter.UpdateCommand.Parameters(12).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(12).Value = (CStr(ShipCountry))
            End If
            Me.Adapter.UpdateCommand.Parameters(13).Value = (CInt(Original_OrderID))
            If (Original_CustomerID Is Nothing) Then
                Me.Adapter.UpdateCommand.Parameters(14).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(15).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(14).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(15).Value = (CStr(Original_CustomerID))
            End If
            If (Original_EmployeeID.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(16).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(17).Value = (CInt(Original_EmployeeID.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(16).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(17).Value = Global.System.DBNull.Value
            End If
            If (Original_OrderDate.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(18).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(19).Value = (CDate(Original_OrderDate.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(18).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(19).Value = Global.System.DBNull.Value
            End If
            If (Original_RequiredDate.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(20).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(21).Value = (CDate(Original_RequiredDate.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(20).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(21).Value = Global.System.DBNull.Value
            End If
            If (Original_ShippedDate.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(22).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(23).Value = (CDate(Original_ShippedDate.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(22).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(23).Value = Global.System.DBNull.Value
            End If
            If (Original_ShipVia.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(24).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(25).Value = (CInt(Original_ShipVia.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(24).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(25).Value = Global.System.DBNull.Value
            End If
            If (Original_Freight.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(26).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(27).Value = (CDec(Original_Freight.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(26).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(27).Value = Global.System.DBNull.Value
            End If
            If (Original_ShipName Is Nothing) Then
                Me.Adapter.UpdateCommand.Parameters(28).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(29).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(28).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(29).Value = (CStr(Original_ShipName))
            End If
            If (Original_ShipAddress Is Nothing) Then
                Me.Adapter.UpdateCommand.Parameters(30).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(31).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(30).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(31).Value = (CStr(Original_ShipAddress))
            End If
            If (Original_ShipCity Is Nothing) Then
                Me.Adapter.UpdateCommand.Parameters(32).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(33).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(32).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(33).Value = (CStr(Original_ShipCity))
            End If
            If (Original_ShipRegion Is Nothing) Then
                Me.Adapter.UpdateCommand.Parameters(34).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(35).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(34).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(35).Value = (CStr(Original_ShipRegion))
            End If
            If (Original_ShipPostalCode Is Nothing) Then
                Me.Adapter.UpdateCommand.Parameters(36).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(37).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(36).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(37).Value = (CStr(Original_ShipPostalCode))
            End If
            If (Original_ShipCountry Is Nothing) Then
                Me.Adapter.UpdateCommand.Parameters(38).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(39).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(38).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(39).Value = (CStr(Original_ShipCountry))
            End If
            Dim previousConnectionState As Global.System.Data.ConnectionState = Me.Adapter.UpdateCommand.Connection.State
            If (CInt(Me.Adapter.UpdateCommand.Connection.State And Global.System.Data.ConnectionState.Open) <> CInt(Global.System.Data.ConnectionState.Open)) Then
                Me.Adapter.UpdateCommand.Connection.Open()
            End If
            Try
                Dim returnValue As Integer = Me.Adapter.UpdateCommand.ExecuteNonQuery()
                Return returnValue
            Finally
                If (previousConnectionState = Global.System.Data.ConnectionState.Closed) Then
                    Me.Adapter.UpdateCommand.Connection.Close()
                End If
            End Try
        End Function
    End Class




    <Global.System.ComponentModel.DesignerCategoryAttribute("code"), Global.System.ComponentModel.ToolboxItem(True), Global.System.ComponentModel.DataObjectAttribute(True), Global.System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner" & ", Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
    Partial Public Class ProductsTableAdapter
        Inherits System.ComponentModel.Component

        Private _adapter As Global.System.Data.OleDb.OleDbDataAdapter

        Private _connection As Global.System.Data.OleDb.OleDbConnection

        Private _transaction As Global.System.Data.OleDb.OleDbTransaction

        Private _commandCollection() As Global.System.Data.OleDb.OleDbCommand

        Private _clearBeforeFill As Boolean

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Sub New()
            Me.ClearBeforeFill = True
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Friend ReadOnly Property Adapter() As Global.System.Data.OleDb.OleDbDataAdapter
            Get
                If (Me._adapter Is Nothing) Then
                    Me.InitAdapter()
                End If
                Return Me._adapter
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Friend Property Connection() As Global.System.Data.OleDb.OleDbConnection
            Get
                If (Me._connection Is Nothing) Then
                    Me.InitConnection()
                End If
                Return Me._connection
            End Get
            Set(ByVal value As System.Data.OleDb.OleDbConnection)
                Me._connection = value
                If (Me.Adapter.InsertCommand IsNot Nothing) Then
                    Me.Adapter.InsertCommand.Connection = value
                End If
                If (Me.Adapter.DeleteCommand IsNot Nothing) Then
                    Me.Adapter.DeleteCommand.Connection = value
                End If
                If (Me.Adapter.UpdateCommand IsNot Nothing) Then
                    Me.Adapter.UpdateCommand.Connection = value
                End If
                Dim i As Integer = 0
                Do While (i < Me.CommandCollection.Length)
                    If (Me.CommandCollection(i) IsNot Nothing) Then
                        CType(Me.CommandCollection(i), Global.System.Data.OleDb.OleDbCommand).Connection = value
                    End If
                    i = (i + 1)
                Loop
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Friend Property Transaction() As Global.System.Data.OleDb.OleDbTransaction
            Get
                Return Me._transaction
            End Get
            Set(ByVal value As System.Data.OleDb.OleDbTransaction)
                Me._transaction = value
                Dim i As Integer = 0
                Do While (i < Me.CommandCollection.Length)
                    Me.CommandCollection(i).Transaction = Me._transaction
                    i = (i + 1)
                Loop
                If ((Me.Adapter IsNot Nothing) AndAlso (Me.Adapter.DeleteCommand IsNot Nothing)) Then
                    Me.Adapter.DeleteCommand.Transaction = Me._transaction
                End If
                If ((Me.Adapter IsNot Nothing) AndAlso (Me.Adapter.InsertCommand IsNot Nothing)) Then
                    Me.Adapter.InsertCommand.Transaction = Me._transaction
                End If
                If ((Me.Adapter IsNot Nothing) AndAlso (Me.Adapter.UpdateCommand IsNot Nothing)) Then
                    Me.Adapter.UpdateCommand.Transaction = Me._transaction
                End If
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected ReadOnly Property CommandCollection() As Global.System.Data.OleDb.OleDbCommand()
            Get
                If (Me._commandCollection Is Nothing) Then
                    Me.InitCommandCollection()
                End If
                Return Me._commandCollection
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Property ClearBeforeFill() As Boolean
            Get
                Return Me._clearBeforeFill
            End Get
            Set(ByVal value As Boolean)
                Me._clearBeforeFill = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitAdapter()
            Me._adapter = New Global.System.Data.OleDb.OleDbDataAdapter()
            Dim tableMapping As New Global.System.Data.Common.DataTableMapping()
            tableMapping.SourceTable = "Table"
            tableMapping.DataSetTable = "Products"
            tableMapping.ColumnMappings.Add("ProductID", "ProductID")
            tableMapping.ColumnMappings.Add("ProductName", "ProductName")
            tableMapping.ColumnMappings.Add("SupplierID", "SupplierID")
            tableMapping.ColumnMappings.Add("CategoryID", "CategoryID")
            tableMapping.ColumnMappings.Add("QuantityPerUnit", "QuantityPerUnit")
            tableMapping.ColumnMappings.Add("UnitPrice", "UnitPrice")
            tableMapping.ColumnMappings.Add("UnitsInStock", "UnitsInStock")
            tableMapping.ColumnMappings.Add("UnitsOnOrder", "UnitsOnOrder")
            tableMapping.ColumnMappings.Add("ReorderLevel", "ReorderLevel")
            tableMapping.ColumnMappings.Add("Discontinued", "Discontinued")
            tableMapping.ColumnMappings.Add("EAN13", "EAN13")
            Me._adapter.TableMappings.Add(tableMapping)
            Me._adapter.DeleteCommand = New Global.System.Data.OleDb.OleDbCommand()
            Me._adapter.DeleteCommand.Connection = Me.Connection
            Me._adapter.DeleteCommand.CommandText = "DELETE FROM `Products` WHERE ((`ProductID` = ?) AND ((? = 1 AND `ProductName` IS NULL) OR (`ProductName` = ?)) AND ((? = 1 AND `SupplierID` IS NULL) OR (`SupplierID` = ?)) AND ((? = 1 AND `CategoryID` IS NULL) OR (`CategoryID` = ?)) AND ((? = 1 AND `QuantityPerUnit` IS NULL) OR (`QuantityPerUnit` = ?)) AND ((? = 1 AND `UnitPrice` IS NULL) OR (`UnitPrice` = ?)) AND ((? = 1 AND `UnitsInStock` IS NULL) OR (`UnitsInStock` = ?)) AND ((? = 1 AND `UnitsOnOrder` IS NULL) OR (`UnitsOnOrder` = ?)) AND ((? = 1 AND `ReorderLevel` IS NULL) OR (`ReorderLevel` = ?)) AND ((? = 1 AND `Discontinued` IS NULL) OR (`Discontinued` = ?)) AND ((? = 1 AND `EAN13` IS NULL) OR (`EAN13` = ?)))"
            Me._adapter.DeleteCommand.CommandType = Global.System.Data.CommandType.Text
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ProductID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ProductID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ProductName", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ProductName", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ProductName", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ProductName", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_SupplierID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "SupplierID", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_SupplierID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "SupplierID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_CategoryID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "CategoryID", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_CategoryID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "CategoryID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_QuantityPerUnit", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "QuantityPerUnit", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_QuantityPerUnit", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "QuantityPerUnit", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_UnitPrice", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "UnitPrice", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_UnitPrice", Global.System.Data.OleDb.OleDbType.Currency, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "UnitPrice", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_UnitsInStock", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "UnitsInStock", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_UnitsInStock", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "UnitsInStock", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_UnitsOnOrder", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "UnitsOnOrder", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_UnitsOnOrder", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "UnitsOnOrder", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ReorderLevel", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ReorderLevel", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ReorderLevel", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ReorderLevel", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_Discontinued", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "Discontinued", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_Discontinued", Global.System.Data.OleDb.OleDbType.Boolean, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "Discontinued", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_EAN13", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "EAN13", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_EAN13", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "EAN13", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.InsertCommand = New Global.System.Data.OleDb.OleDbCommand()
            Me._adapter.InsertCommand.Connection = Me.Connection
            Me._adapter.InsertCommand.CommandText = "INSERT INTO `Products` (`ProductName`, `SupplierID`, `CategoryID`, `QuantityPerUn" & "it`, `UnitPrice`, `UnitsInStock`, `UnitsOnOrder`, `ReorderLevel`, `Discontinued`" & ", `EAN13`) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
            Me._adapter.InsertCommand.CommandType = Global.System.Data.CommandType.Text
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ProductName", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ProductName", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("SupplierID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "SupplierID", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("CategoryID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "CategoryID", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("QuantityPerUnit", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "QuantityPerUnit", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("UnitPrice", Global.System.Data.OleDb.OleDbType.Currency, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "UnitPrice", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("UnitsInStock", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "UnitsInStock", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("UnitsOnOrder", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "UnitsOnOrder", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ReorderLevel", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ReorderLevel", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Discontinued", Global.System.Data.OleDb.OleDbType.Boolean, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "Discontinued", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("EAN13", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "EAN13", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand = New Global.System.Data.OleDb.OleDbCommand()
            Me._adapter.UpdateCommand.Connection = Me.Connection
            Me._adapter.UpdateCommand.CommandText = "UPDATE `Products` SET `ProductName` = ?, `SupplierID` = ?, `CategoryID` = ?, `QuantityPerUnit` = ?, `UnitPrice` = ?, `UnitsInStock` = ?, `UnitsOnOrder` = ?, `ReorderLevel` = ?, `Discontinued` = ?, `EAN13` = ? WHERE ((`ProductID` = ?) AND ((? = 1 AND `ProductName` IS NULL) OR (`ProductName` = ?)) AND ((? = 1 AND `SupplierID` IS NULL) OR (`SupplierID` = ?)) AND ((? = 1 AND `CategoryID` IS NULL) OR (`CategoryID` = ?)) AND ((? = 1 AND `QuantityPerUnit` IS NULL) OR (`QuantityPerUnit` = ?)) AND ((? = 1 AND `UnitPrice` IS NULL) OR (`UnitPrice` = ?)) AND ((? = 1 AND `UnitsInStock` IS NULL) OR (`UnitsInStock` = ?)) AND ((? = 1 AND `UnitsOnOrder` IS NULL) OR (`UnitsOnOrder` = ?)) AND ((? = 1 AND `ReorderLevel` IS NULL) OR (`ReorderLevel` = ?)) AND ((? = 1 AND `Discontinued` IS NULL) OR (`Discontinued` = ?)) AND ((? = 1 AND `EAN13` IS NULL) OR (`EAN13` = ?)))"
            Me._adapter.UpdateCommand.CommandType = Global.System.Data.CommandType.Text
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ProductName", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ProductName", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("SupplierID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "SupplierID", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("CategoryID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "CategoryID", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("QuantityPerUnit", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "QuantityPerUnit", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("UnitPrice", Global.System.Data.OleDb.OleDbType.Currency, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "UnitPrice", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("UnitsInStock", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "UnitsInStock", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("UnitsOnOrder", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "UnitsOnOrder", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ReorderLevel", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ReorderLevel", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Discontinued", Global.System.Data.OleDb.OleDbType.Boolean, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "Discontinued", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("EAN13", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "EAN13", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ProductID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ProductID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ProductName", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ProductName", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ProductName", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ProductName", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_SupplierID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "SupplierID", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_SupplierID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "SupplierID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_CategoryID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "CategoryID", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_CategoryID", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "CategoryID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_QuantityPerUnit", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "QuantityPerUnit", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_QuantityPerUnit", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "QuantityPerUnit", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_UnitPrice", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "UnitPrice", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_UnitPrice", Global.System.Data.OleDb.OleDbType.Currency, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "UnitPrice", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_UnitsInStock", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "UnitsInStock", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_UnitsInStock", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "UnitsInStock", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_UnitsOnOrder", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "UnitsOnOrder", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_UnitsOnOrder", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "UnitsOnOrder", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ReorderLevel", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ReorderLevel", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ReorderLevel", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "ReorderLevel", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_Discontinued", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "Discontinued", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_Discontinued", Global.System.Data.OleDb.OleDbType.Boolean, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "Discontinued", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_EAN13", Global.System.Data.OleDb.OleDbType.Integer, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "EAN13", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_EAN13", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte(0)), (CByte(0)), "EAN13", Global.System.Data.DataRowVersion.Original, False, Nothing))
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitConnection()
            Me._connection = New Global.System.Data.OleDb.OleDbConnection()
            Me._connection.ConnectionString = My.Settings.Default.nwindConnectionString
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitCommandCollection()
            Me._commandCollection = New Global.System.Data.OleDb.OleDbCommand(0){}
            Me._commandCollection(0) = New Global.System.Data.OleDb.OleDbCommand()
            Me._commandCollection(0).Connection = Me.Connection
            Me._commandCollection(0).CommandText = "SELECT ProductID, ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice" & ", UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued, EAN13 FROM Products"
            Me._commandCollection(0).CommandType = Global.System.Data.CommandType.Text
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Fill, True)>
        Public Overridable Function Fill(ByVal dataTable As nwindOrders.ProductsDataTable) As Integer
            Me.Adapter.SelectCommand = Me.CommandCollection(0)
            If (Me.ClearBeforeFill = True) Then
                dataTable.Clear()
            End If
            Dim returnValue As Integer = Me.Adapter.Fill(dataTable)
            Return returnValue
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Select, True)>
        Public Overridable Function GetData() As nwindOrders.ProductsDataTable
            Me.Adapter.SelectCommand = Me.CommandCollection(0)
            Dim dataTable As New nwindOrders.ProductsDataTable()
            Me.Adapter.Fill(dataTable)
            Return dataTable
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        Public Overridable Function Update(ByVal dataTable As nwindOrders.ProductsDataTable) As Integer
            Return Me.Adapter.Update(dataTable)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        Public Overridable Function Update(ByVal dataSet As nwindOrders) As Integer
            Return Me.Adapter.Update(dataSet, "Products")
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        Public Overridable Function Update(ByVal dataRow As Global.System.Data.DataRow) As Integer
            Return Me.Adapter.Update(New Global.System.Data.DataRow() { dataRow})
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        Public Overridable Function Update(ByVal dataRows() As Global.System.Data.DataRow) As Integer
            Return Me.Adapter.Update(dataRows)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Delete, True)>
        Public Overridable Function Delete(ByVal Original_ProductID As Integer, ByVal Original_ProductName As String, ByVal Original_SupplierID? As Integer, ByVal Original_CategoryID? As Integer, ByVal Original_QuantityPerUnit As String, ByVal Original_UnitPrice? As Decimal, ByVal Original_UnitsInStock? As Short, ByVal Original_UnitsOnOrder? As Short, ByVal Original_ReorderLevel? As Short, ByVal Original_Discontinued As Boolean, ByVal Original_EAN13 As String) As Integer
            Me.Adapter.DeleteCommand.Parameters(0).Value = (CInt(Original_ProductID))
            If (Original_ProductName Is Nothing) Then
                Throw New Global.System.ArgumentNullException("Original_ProductName")
            Else
                Me.Adapter.DeleteCommand.Parameters(1).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(2).Value = (CStr(Original_ProductName))
            End If
            If (Original_SupplierID.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(3).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(4).Value = (CInt(Original_SupplierID.Value))
            Else
                Me.Adapter.DeleteCommand.Parameters(3).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(4).Value = Global.System.DBNull.Value
            End If
            If (Original_CategoryID.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(5).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(6).Value = (CInt(Original_CategoryID.Value))
            Else
                Me.Adapter.DeleteCommand.Parameters(5).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(6).Value = Global.System.DBNull.Value
            End If
            If (Original_QuantityPerUnit Is Nothing) Then
                Me.Adapter.DeleteCommand.Parameters(7).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(8).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.DeleteCommand.Parameters(7).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(8).Value = (CStr(Original_QuantityPerUnit))
            End If
            If (Original_UnitPrice.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(9).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(10).Value = (CDec(Original_UnitPrice.Value))
            Else
                Me.Adapter.DeleteCommand.Parameters(9).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(10).Value = Global.System.DBNull.Value
            End If
            If (Original_UnitsInStock.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(11).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(12).Value = (CShort(Original_UnitsInStock.Value))
            Else
                Me.Adapter.DeleteCommand.Parameters(11).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(12).Value = Global.System.DBNull.Value
            End If
            If (Original_UnitsOnOrder.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(13).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(14).Value = (CShort(Original_UnitsOnOrder.Value))
            Else
                Me.Adapter.DeleteCommand.Parameters(13).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(14).Value = Global.System.DBNull.Value
            End If
            If (Original_ReorderLevel.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(15).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(16).Value = (CShort(Original_ReorderLevel.Value))
            Else
                Me.Adapter.DeleteCommand.Parameters(15).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(16).Value = Global.System.DBNull.Value
            End If
            Me.Adapter.DeleteCommand.Parameters(17).Value = (DirectCast(0, Object))
            Me.Adapter.DeleteCommand.Parameters(18).Value = (CBool(Original_Discontinued))
            If (Original_EAN13 Is Nothing) Then
                Me.Adapter.DeleteCommand.Parameters(19).Value = (DirectCast(1, Object))
                Me.Adapter.DeleteCommand.Parameters(20).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.DeleteCommand.Parameters(19).Value = (DirectCast(0, Object))
                Me.Adapter.DeleteCommand.Parameters(20).Value = (CStr(Original_EAN13))
            End If
            Dim previousConnectionState As Global.System.Data.ConnectionState = Me.Adapter.DeleteCommand.Connection.State
            If (CInt(Me.Adapter.DeleteCommand.Connection.State And Global.System.Data.ConnectionState.Open) <> CInt(Global.System.Data.ConnectionState.Open)) Then
                Me.Adapter.DeleteCommand.Connection.Open()
            End If
            Try
                Dim returnValue As Integer = Me.Adapter.DeleteCommand.ExecuteNonQuery()
                Return returnValue
            Finally
                If (previousConnectionState = Global.System.Data.ConnectionState.Closed) Then
                    Me.Adapter.DeleteCommand.Connection.Close()
                End If
            End Try
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Insert, True)>
        Public Overridable Function Insert(ByVal ProductName As String, ByVal SupplierID? As Integer, ByVal CategoryID? As Integer, ByVal QuantityPerUnit As String, ByVal UnitPrice? As Decimal, ByVal UnitsInStock? As Short, ByVal UnitsOnOrder? As Short, ByVal ReorderLevel? As Short, ByVal Discontinued As Boolean, ByVal EAN13 As String) As Integer
            If (ProductName Is Nothing) Then
                Throw New Global.System.ArgumentNullException("ProductName")
            Else
                Me.Adapter.InsertCommand.Parameters(0).Value = (CStr(ProductName))
            End If
            If (SupplierID.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(1).Value = (CInt(SupplierID.Value))
            Else
                Me.Adapter.InsertCommand.Parameters(1).Value = Global.System.DBNull.Value
            End If
            If (CategoryID.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(2).Value = (CInt(CategoryID.Value))
            Else
                Me.Adapter.InsertCommand.Parameters(2).Value = Global.System.DBNull.Value
            End If
            If (QuantityPerUnit Is Nothing) Then
                Me.Adapter.InsertCommand.Parameters(3).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(3).Value = (CStr(QuantityPerUnit))
            End If
            If (UnitPrice.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(4).Value = (CDec(UnitPrice.Value))
            Else
                Me.Adapter.InsertCommand.Parameters(4).Value = Global.System.DBNull.Value
            End If
            If (UnitsInStock.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(5).Value = (CShort(UnitsInStock.Value))
            Else
                Me.Adapter.InsertCommand.Parameters(5).Value = Global.System.DBNull.Value
            End If
            If (UnitsOnOrder.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(6).Value = (CShort(UnitsOnOrder.Value))
            Else
                Me.Adapter.InsertCommand.Parameters(6).Value = Global.System.DBNull.Value
            End If
            If (ReorderLevel.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(7).Value = (CShort(ReorderLevel.Value))
            Else
                Me.Adapter.InsertCommand.Parameters(7).Value = Global.System.DBNull.Value
            End If
            Me.Adapter.InsertCommand.Parameters(8).Value = (CBool(Discontinued))
            If (EAN13 Is Nothing) Then
                Me.Adapter.InsertCommand.Parameters(9).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(9).Value = (CStr(EAN13))
            End If
            Dim previousConnectionState As Global.System.Data.ConnectionState = Me.Adapter.InsertCommand.Connection.State
            If (CInt(Me.Adapter.InsertCommand.Connection.State And Global.System.Data.ConnectionState.Open) <> CInt(Global.System.Data.ConnectionState.Open)) Then
                Me.Adapter.InsertCommand.Connection.Open()
            End If
            Try
                Dim returnValue As Integer = Me.Adapter.InsertCommand.ExecuteNonQuery()
                Return returnValue
            Finally
                If (previousConnectionState = Global.System.Data.ConnectionState.Closed) Then
                    Me.Adapter.InsertCommand.Connection.Close()
                End If
            End Try
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Update, True)>
        Public Overridable Function Update(ByVal ProductName As String, ByVal SupplierID? As Integer, ByVal CategoryID? As Integer, ByVal QuantityPerUnit As String, ByVal UnitPrice? As Decimal, ByVal UnitsInStock? As Short, ByVal UnitsOnOrder? As Short, ByVal ReorderLevel? As Short, ByVal Discontinued As Boolean, ByVal EAN13 As String, ByVal Original_ProductID As Integer, ByVal Original_ProductName As String, ByVal Original_SupplierID? As Integer, ByVal Original_CategoryID? As Integer, ByVal Original_QuantityPerUnit As String, ByVal Original_UnitPrice? As Decimal, ByVal Original_UnitsInStock? As Short, ByVal Original_UnitsOnOrder? As Short, ByVal Original_ReorderLevel? As Short, ByVal Original_Discontinued As Boolean, ByVal Original_EAN13 As String) As Integer
            If (ProductName Is Nothing) Then
                Throw New Global.System.ArgumentNullException("ProductName")
            Else
                Me.Adapter.UpdateCommand.Parameters(0).Value = (CStr(ProductName))
            End If
            If (SupplierID.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(1).Value = (CInt(SupplierID.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(1).Value = Global.System.DBNull.Value
            End If
            If (CategoryID.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(2).Value = (CInt(CategoryID.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(2).Value = Global.System.DBNull.Value
            End If
            If (QuantityPerUnit Is Nothing) Then
                Me.Adapter.UpdateCommand.Parameters(3).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(3).Value = (CStr(QuantityPerUnit))
            End If
            If (UnitPrice.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(4).Value = (CDec(UnitPrice.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(4).Value = Global.System.DBNull.Value
            End If
            If (UnitsInStock.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(5).Value = (CShort(UnitsInStock.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(5).Value = Global.System.DBNull.Value
            End If
            If (UnitsOnOrder.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(6).Value = (CShort(UnitsOnOrder.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(6).Value = Global.System.DBNull.Value
            End If
            If (ReorderLevel.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(7).Value = (CShort(ReorderLevel.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(7).Value = Global.System.DBNull.Value
            End If
            Me.Adapter.UpdateCommand.Parameters(8).Value = (CBool(Discontinued))
            If (EAN13 Is Nothing) Then
                Me.Adapter.UpdateCommand.Parameters(9).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(9).Value = (CStr(EAN13))
            End If
            Me.Adapter.UpdateCommand.Parameters(10).Value = (CInt(Original_ProductID))
            If (Original_ProductName Is Nothing) Then
                Throw New Global.System.ArgumentNullException("Original_ProductName")
            Else
                Me.Adapter.UpdateCommand.Parameters(11).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(12).Value = (CStr(Original_ProductName))
            End If
            If (Original_SupplierID.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(13).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(14).Value = (CInt(Original_SupplierID.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(13).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(14).Value = Global.System.DBNull.Value
            End If
            If (Original_CategoryID.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(15).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(16).Value = (CInt(Original_CategoryID.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(15).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(16).Value = Global.System.DBNull.Value
            End If
            If (Original_QuantityPerUnit Is Nothing) Then
                Me.Adapter.UpdateCommand.Parameters(17).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(18).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(17).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(18).Value = (CStr(Original_QuantityPerUnit))
            End If
            If (Original_UnitPrice.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(19).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(20).Value = (CDec(Original_UnitPrice.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(19).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(20).Value = Global.System.DBNull.Value
            End If
            If (Original_UnitsInStock.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(21).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(22).Value = (CShort(Original_UnitsInStock.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(21).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(22).Value = Global.System.DBNull.Value
            End If
            If (Original_UnitsOnOrder.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(23).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(24).Value = (CShort(Original_UnitsOnOrder.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(23).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(24).Value = Global.System.DBNull.Value
            End If
            If (Original_ReorderLevel.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(25).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(26).Value = (CShort(Original_ReorderLevel.Value))
            Else
                Me.Adapter.UpdateCommand.Parameters(25).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(26).Value = Global.System.DBNull.Value
            End If
            Me.Adapter.UpdateCommand.Parameters(27).Value = (DirectCast(0, Object))
            Me.Adapter.UpdateCommand.Parameters(28).Value = (CBool(Original_Discontinued))
            If (Original_EAN13 Is Nothing) Then
                Me.Adapter.UpdateCommand.Parameters(29).Value = (DirectCast(1, Object))
                Me.Adapter.UpdateCommand.Parameters(30).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(29).Value = (DirectCast(0, Object))
                Me.Adapter.UpdateCommand.Parameters(30).Value = (CStr(Original_EAN13))
            End If
            Dim previousConnectionState As Global.System.Data.ConnectionState = Me.Adapter.UpdateCommand.Connection.State
            If (CInt(Me.Adapter.UpdateCommand.Connection.State And Global.System.Data.ConnectionState.Open) <> CInt(Global.System.Data.ConnectionState.Open)) Then
                Me.Adapter.UpdateCommand.Connection.Open()
            End If
            Try
                Dim returnValue As Integer = Me.Adapter.UpdateCommand.ExecuteNonQuery()
                Return returnValue
            Finally
                If (previousConnectionState = Global.System.Data.ConnectionState.Closed) Then
                    Me.Adapter.UpdateCommand.Connection.Close()
                End If
            End Try
        End Function
    End Class




    <Global.System.ComponentModel.DesignerCategoryAttribute("code"), Global.System.ComponentModel.ToolboxItem(True), Global.System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerDesigner, Microsoft.VSD" & "esigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapterManager")>
    Partial Public Class TableAdapterManager
        Inherits System.ComponentModel.Component

        Private _updateOrder As UpdateOrderOption

        Private _ordersTableAdapter As OrdersTableAdapter

        Private _productsTableAdapter As ProductsTableAdapter

        Private _backupDataSetBeforeUpdate As Boolean

        Private _connection As Global.System.Data.IDbConnection

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Property UpdateOrder() As UpdateOrderOption
            Get
                Return Me._updateOrder
            End Get
            Set(ByVal value As UpdateOrderOption)
                Me._updateOrder = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.EditorAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerPropertyEditor, Microso" & "ft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3" & "a", "System.Drawing.Design.UITypeEditor")>
        Public Property OrdersTableAdapter() As OrdersTableAdapter
            Get
                Return Me._ordersTableAdapter
            End Get
            Set(ByVal value As OrdersTableAdapter)
                Me._ordersTableAdapter = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.EditorAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerPropertyEditor, Microso" & "ft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3" & "a", "System.Drawing.Design.UITypeEditor")>
        Public Property ProductsTableAdapter() As ProductsTableAdapter
            Get
                Return Me._productsTableAdapter
            End Get
            Set(ByVal value As ProductsTableAdapter)
                Me._productsTableAdapter = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Property BackupDataSetBeforeUpdate() As Boolean
            Get
                Return Me._backupDataSetBeforeUpdate
            End Get
            Set(ByVal value As Boolean)
                Me._backupDataSetBeforeUpdate = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Browsable(False)>
        Public Property Connection() As Global.System.Data.IDbConnection
            Get
                If (Me._connection IsNot Nothing) Then
                    Return Me._connection
                End If
                If ((Me._ordersTableAdapter IsNot Nothing) AndAlso (Me._ordersTableAdapter.Connection IsNot Nothing)) Then
                    Return Me._ordersTableAdapter.Connection
                End If
                If ((Me._productsTableAdapter IsNot Nothing) AndAlso (Me._productsTableAdapter.Connection IsNot Nothing)) Then
                    Return Me._productsTableAdapter.Connection
                End If
                Return Nothing
            End Get
            Set(ByVal value As System.Data.IDbConnection)
                Me._connection = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), Global.System.ComponentModel.Browsable(False)>
        Public ReadOnly Property TableAdapterInstanceCount() As Integer
            Get
                Dim count As Integer = 0
                If (Me._ordersTableAdapter IsNot Nothing) Then
                    count = (count + 1)
                End If
                If (Me._productsTableAdapter IsNot Nothing) Then
                    count = (count + 1)
                End If
                Return count
            End Get
        End Property




        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Function UpdateUpdatedRows(ByVal dataSet As nwindOrders, ByVal allChangedRows As Global.System.Collections.Generic.List(Of Global.System.Data.DataRow), ByVal allAddedRows As Global.System.Collections.Generic.List(Of Global.System.Data.DataRow)) As Integer
            Dim result As Integer = 0
            If (Me._ordersTableAdapter IsNot Nothing) Then
                Dim updatedRows() As Global.System.Data.DataRow = dataSet.Orders.Select(Nothing, Nothing, Global.System.Data.DataViewRowState.ModifiedCurrent)
                updatedRows = Me.GetRealUpdatedRows(updatedRows, allAddedRows)
                If ((updatedRows IsNot Nothing) AndAlso (0 < updatedRows.Length)) Then
                    result = (result + Me._ordersTableAdapter.Update(updatedRows))
                    allChangedRows.AddRange(updatedRows)
                End If
            End If
            If (Me._productsTableAdapter IsNot Nothing) Then
                Dim updatedRows() As Global.System.Data.DataRow = dataSet.Products.Select(Nothing, Nothing, Global.System.Data.DataViewRowState.ModifiedCurrent)
                updatedRows = Me.GetRealUpdatedRows(updatedRows, allAddedRows)
                If ((updatedRows IsNot Nothing) AndAlso (0 < updatedRows.Length)) Then
                    result = (result + Me._productsTableAdapter.Update(updatedRows))
                    allChangedRows.AddRange(updatedRows)
                End If
            End If
            Return result
        End Function




        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Function UpdateInsertedRows(ByVal dataSet As nwindOrders, ByVal allAddedRows As Global.System.Collections.Generic.List(Of Global.System.Data.DataRow)) As Integer
            Dim result As Integer = 0
            If (Me._ordersTableAdapter IsNot Nothing) Then
                Dim addedRows() As Global.System.Data.DataRow = dataSet.Orders.Select(Nothing, Nothing, Global.System.Data.DataViewRowState.Added)
                If ((addedRows IsNot Nothing) AndAlso (0 < addedRows.Length)) Then
                    result = (result + Me._ordersTableAdapter.Update(addedRows))
                    allAddedRows.AddRange(addedRows)
                End If
            End If
            If (Me._productsTableAdapter IsNot Nothing) Then
                Dim addedRows() As Global.System.Data.DataRow = dataSet.Products.Select(Nothing, Nothing, Global.System.Data.DataViewRowState.Added)
                If ((addedRows IsNot Nothing) AndAlso (0 < addedRows.Length)) Then
                    result = (result + Me._productsTableAdapter.Update(addedRows))
                    allAddedRows.AddRange(addedRows)
                End If
            End If
            Return result
        End Function




        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Function UpdateDeletedRows(ByVal dataSet As nwindOrders, ByVal allChangedRows As Global.System.Collections.Generic.List(Of Global.System.Data.DataRow)) As Integer
            Dim result As Integer = 0
            If (Me._productsTableAdapter IsNot Nothing) Then
                Dim deletedRows() As Global.System.Data.DataRow = dataSet.Products.Select(Nothing, Nothing, Global.System.Data.DataViewRowState.Deleted)
                If ((deletedRows IsNot Nothing) AndAlso (0 < deletedRows.Length)) Then
                    result = (result + Me._productsTableAdapter.Update(deletedRows))
                    allChangedRows.AddRange(deletedRows)
                End If
            End If
            If (Me._ordersTableAdapter IsNot Nothing) Then
                Dim deletedRows() As Global.System.Data.DataRow = dataSet.Orders.Select(Nothing, Nothing, Global.System.Data.DataViewRowState.Deleted)
                If ((deletedRows IsNot Nothing) AndAlso (0 < deletedRows.Length)) Then
                    result = (result + Me._ordersTableAdapter.Update(deletedRows))
                    allChangedRows.AddRange(deletedRows)
                End If
            End If
            Return result
        End Function




        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Function GetRealUpdatedRows(ByVal updatedRows() As Global.System.Data.DataRow, ByVal allAddedRows As Global.System.Collections.Generic.List(Of Global.System.Data.DataRow)) As Global.System.Data.DataRow()
            If ((updatedRows Is Nothing) OrElse (updatedRows.Length < 1)) Then
                Return updatedRows
            End If
            If ((allAddedRows Is Nothing) OrElse (allAddedRows.Count < 1)) Then
                Return updatedRows
            End If
            Dim realUpdatedRows As New Global.System.Collections.Generic.List(Of Global.System.Data.DataRow)()
            Dim i As Integer = 0
            Do While (i < updatedRows.Length)
                Dim row As Global.System.Data.DataRow = updatedRows(i)
                If (allAddedRows.Contains(row) = False) Then
                    realUpdatedRows.Add(row)
                End If
                i = (i + 1)
            Loop
            Return realUpdatedRows.ToArray()
        End Function




        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Overridable Function UpdateAll(ByVal dataSet As nwindOrders) As Integer
            If (dataSet Is Nothing) Then
                Throw New Global.System.ArgumentNullException("dataSet")
            End If
            If (dataSet.HasChanges() = False) Then
                Return 0
            End If
            If ((Me._ordersTableAdapter IsNot Nothing) AndAlso (Me.MatchTableAdapterConnection(Me._ordersTableAdapter.Connection) = False)) Then
                Throw New Global.System.ArgumentException("All TableAdapters managed by a TableAdapterManager must use the same connection s" & "tring.")
            End If
            If ((Me._productsTableAdapter IsNot Nothing) AndAlso (Me.MatchTableAdapterConnection(Me._productsTableAdapter.Connection) = False)) Then
                Throw New Global.System.ArgumentException("All TableAdapters managed by a TableAdapterManager must use the same connection s" & "tring.")
            End If
            Dim workConnection As Global.System.Data.IDbConnection = Me.Connection
            If (workConnection Is Nothing) Then
                Throw New Global.System.ApplicationException("TableAdapterManager contains no connection information. Set each TableAdapterMana" & "ger TableAdapter property to a valid TableAdapter instance.")
            End If
            Dim workConnOpened As Boolean = False
            If (CInt(workConnection.State And Global.System.Data.ConnectionState.Broken) = CInt(Global.System.Data.ConnectionState.Broken)) Then
                workConnection.Close()
            End If
            If (workConnection.State = Global.System.Data.ConnectionState.Closed) Then
                workConnection.Open()
                workConnOpened = True
            End If
            Dim workTransaction As Global.System.Data.IDbTransaction = workConnection.BeginTransaction()
            If (workTransaction Is Nothing) Then
                Throw New Global.System.ApplicationException("The transaction cannot begin. The current data connection does not support transa" & "ctions or the current state is not allowing the transaction to begin.")
            End If
            Dim allChangedRows As New Global.System.Collections.Generic.List(Of Global.System.Data.DataRow)()
            Dim allAddedRows As New Global.System.Collections.Generic.List(Of Global.System.Data.DataRow)()
            Dim adaptersWithAcceptChangesDuringUpdate As New Global.System.Collections.Generic.List(Of Global.System.Data.Common.DataAdapter)()
            Dim revertConnections As New Global.System.Collections.Generic.Dictionary(Of Object, Global.System.Data.IDbConnection)()
            Dim result As Integer = 0
            Dim backupDataSet As Global.System.Data.DataSet = Nothing
            If Me.BackupDataSetBeforeUpdate Then
                backupDataSet = New Global.System.Data.DataSet()
                backupDataSet.Merge(dataSet)
            End If
            Try


                If (Me._ordersTableAdapter IsNot Nothing) Then
                    revertConnections.Add(Me._ordersTableAdapter, Me._ordersTableAdapter.Connection)
                    Me._ordersTableAdapter.Connection = (DirectCast(workConnection, Global.System.Data.OleDb.OleDbConnection))
                    Me._ordersTableAdapter.Transaction = (DirectCast(workTransaction, Global.System.Data.OleDb.OleDbTransaction))
                    If Me._ordersTableAdapter.Adapter.AcceptChangesDuringUpdate Then
                        Me._ordersTableAdapter.Adapter.AcceptChangesDuringUpdate = False
                        adaptersWithAcceptChangesDuringUpdate.Add(Me._ordersTableAdapter.Adapter)
                    End If
                End If
                If (Me._productsTableAdapter IsNot Nothing) Then
                    revertConnections.Add(Me._productsTableAdapter, Me._productsTableAdapter.Connection)
                    Me._productsTableAdapter.Connection = (DirectCast(workConnection, Global.System.Data.OleDb.OleDbConnection))
                    Me._productsTableAdapter.Transaction = (DirectCast(workTransaction, Global.System.Data.OleDb.OleDbTransaction))
                    If Me._productsTableAdapter.Adapter.AcceptChangesDuringUpdate Then
                        Me._productsTableAdapter.Adapter.AcceptChangesDuringUpdate = False
                        adaptersWithAcceptChangesDuringUpdate.Add(Me._productsTableAdapter.Adapter)
                    End If
                End If



                If (Me.UpdateOrder = UpdateOrderOption.UpdateInsertDelete) Then
                    result = (result + Me.UpdateUpdatedRows(dataSet, allChangedRows, allAddedRows))
                    result = (result + Me.UpdateInsertedRows(dataSet, allAddedRows))
                Else
                    result = (result + Me.UpdateInsertedRows(dataSet, allAddedRows))
                    result = (result + Me.UpdateUpdatedRows(dataSet, allChangedRows, allAddedRows))
                End If
                result = (result + Me.UpdateDeletedRows(dataSet, allChangedRows))



                workTransaction.Commit()
                If (0 < allAddedRows.Count) Then
                    Dim rows(allAddedRows.Count - 1) As Global.System.Data.DataRow
                    allAddedRows.CopyTo(rows)
                    Dim i As Integer = 0
                    Do While (i < rows.Length)
                        Dim row As Global.System.Data.DataRow = rows(i)
                        row.AcceptChanges()
                        i = (i + 1)
                    Loop
                End If
                If (0 < allChangedRows.Count) Then
                    Dim rows(allChangedRows.Count - 1) As Global.System.Data.DataRow
                    allChangedRows.CopyTo(rows)
                    Dim i As Integer = 0
                    Do While (i < rows.Length)
                        Dim row As Global.System.Data.DataRow = rows(i)
                        row.AcceptChanges()
                        i = (i + 1)
                    Loop
                End If
            Catch ex As Global.System.Exception
                workTransaction.Rollback()

                If Me.BackupDataSetBeforeUpdate Then
                    Global.System.Diagnostics.Debug.Assert((backupDataSet IsNot Nothing))
                    dataSet.Clear()
                    dataSet.Merge(backupDataSet)
                Else
                    If (0 < allAddedRows.Count) Then
                        Dim rows(allAddedRows.Count - 1) As Global.System.Data.DataRow
                        allAddedRows.CopyTo(rows)
                        Dim i As Integer = 0
                        Do While (i < rows.Length)
                            Dim row As Global.System.Data.DataRow = rows(i)
                            row.AcceptChanges()
                            row.SetAdded()
                            i = (i + 1)
                        Loop
                    End If
                End If
                Throw ex
            Finally
                If workConnOpened Then
                    workConnection.Close()
                End If
                If (Me._ordersTableAdapter IsNot Nothing) Then
                    Me._ordersTableAdapter.Connection = (DirectCast(revertConnections(Me._ordersTableAdapter), Global.System.Data.OleDb.OleDbConnection))
                    Me._ordersTableAdapter.Transaction = Nothing
                End If
                If (Me._productsTableAdapter IsNot Nothing) Then
                    Me._productsTableAdapter.Connection = (DirectCast(revertConnections(Me._productsTableAdapter), Global.System.Data.OleDb.OleDbConnection))
                    Me._productsTableAdapter.Transaction = Nothing
                End If
                If (0 < adaptersWithAcceptChangesDuringUpdate.Count) Then
                    Dim adapters(adaptersWithAcceptChangesDuringUpdate.Count - 1) As Global.System.Data.Common.DataAdapter
                    adaptersWithAcceptChangesDuringUpdate.CopyTo(adapters)
                    Dim i As Integer = 0
                    Do While (i < adapters.Length)
                        Dim adapter As Global.System.Data.Common.DataAdapter = adapters(i)
                        adapter.AcceptChangesDuringUpdate = True
                        i = (i + 1)
                    Loop
                End If
            End Try
            Return result
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Overridable Sub SortSelfReferenceRows(ByVal rows() As Global.System.Data.DataRow, ByVal relation As Global.System.Data.DataRelation, ByVal childFirst As Boolean)
            Global.System.Array.Sort(Of Global.System.Data.DataRow)(rows, New SelfReferenceComparer(relation, childFirst))
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Overridable Function MatchTableAdapterConnection(ByVal inputConnection As Global.System.Data.IDbConnection) As Boolean
            If (Me._connection IsNot Nothing) Then
                Return True
            End If
            If ((Me.Connection Is Nothing) OrElse (inputConnection Is Nothing)) Then
                Return True
            End If
            If String.Equals(Me.Connection.ConnectionString, inputConnection.ConnectionString, Global.System.StringComparison.Ordinal) Then
                Return True
            End If
            Return False
        End Function




        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Enum UpdateOrderOption

            InsertUpdateDelete = 0

            UpdateInsertDelete = 1
        End Enum




        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Class SelfReferenceComparer
            Inherits Object
            Implements System.Collections.Generic.IComparer(Of System.Data.DataRow)

            Private _relation As Global.System.Data.DataRelation

            Private _childFirst As Integer

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub New(ByVal relation As Global.System.Data.DataRelation, ByVal childFirst As Boolean)
                Me._relation = relation
                If childFirst Then
                    Me._childFirst = -1
                Else
                    Me._childFirst = 1
                End If
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Private Function GetRoot(ByVal row As Global.System.Data.DataRow, ByRef distance As Integer) As Global.System.Data.DataRow
                Global.System.Diagnostics.Debug.Assert((row IsNot Nothing))
                Dim root As Global.System.Data.DataRow = row
                distance = 0

                Dim traversedRows As Global.System.Collections.Generic.IDictionary(Of Global.System.Data.DataRow, Global.System.Data.DataRow) = New Global.System.Collections.Generic.Dictionary(Of Global.System.Data.DataRow, Global.System.Data.DataRow)()
                traversedRows(row) = row

                Dim parent As Global.System.Data.DataRow = row.GetParentRow(Me._relation, Global.System.Data.DataRowVersion.Default)
                Do While ((parent IsNot Nothing) AndAlso (traversedRows.ContainsKey(parent) = False))
                    distance = (distance + 1)
                    root = parent
                    traversedRows(parent) = parent
                    parent = parent.GetParentRow(Me._relation, Global.System.Data.DataRowVersion.Default)
                Loop

                If (distance = 0) Then
                    traversedRows.Clear()
                    traversedRows(row) = row
                    parent = row.GetParentRow(Me._relation, Global.System.Data.DataRowVersion.Original)
                    Do While ((parent IsNot Nothing) AndAlso (traversedRows.ContainsKey(parent) = False))
                        distance = (distance + 1)
                        root = parent
                        traversedRows(parent) = parent
                        parent = parent.GetParentRow(Me._relation, Global.System.Data.DataRowVersion.Original)
                    Loop
                End If

                Return root
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Private Function IComparerGeneric_Compare(ByVal row1 As Global.System.Data.DataRow, ByVal row2 As Global.System.Data.DataRow) As Integer Implements Global.System.Collections.Generic.IComparer(Of Global.System.Data.DataRow).Compare
                If Object.ReferenceEquals(row1, row2) Then
                    Return 0
                End If
                If (row1 Is Nothing) Then
                    Return -1
                End If
                If (row2 Is Nothing) Then
                    Return 1
                End If

                Dim distance1 As Integer = 0
                Dim root1 As Global.System.Data.DataRow = Me.GetRoot(row1, distance1)

                Dim distance2 As Integer = 0
                Dim root2 As Global.System.Data.DataRow = Me.GetRoot(row2, distance2)

                If Object.ReferenceEquals(root1, root2) Then
                    Return (Me._childFirst * distance1.CompareTo(distance2))
                End If

                Global.System.Diagnostics.Debug.Assert(((root1.Table IsNot Nothing) AndAlso (root2.Table IsNot Nothing)))
                If (root1.Table.Rows.IndexOf(root1) < root2.Table.Rows.IndexOf(root2)) Then
                    Return -1
                End If

                Return 1
            End Function
        End Class
    End Class
End Namespace

'#pragma warning restore 1591
