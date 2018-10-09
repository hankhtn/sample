Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data
Imports System.Linq
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Input

Namespace GridDemo



    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/HitTestTemplates.xaml"), DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/HitTestClasses.(cs)")>
    Partial Public Class HitTesting
        Inherits GridDemoModule

        Private ReadOnly Property TableView() As DevExpress.Xpf.Grid.TableView
            Get
                Return CType(grid.View, DevExpress.Xpf.Grid.TableView)
            End Get
        End Property
        Private hitInfoList As New ObservableCollection(Of HitTestInfo)()
        Private startPosition As Point
        Private floatingContainerIsOpenCount As Integer


        Public Property AllowShowHitInfo() As Boolean
            Get
                Return CBool(GetValue(AllowShowHitInfoProperty))
            End Get
            Set(ByVal value As Boolean)
                SetValue(AllowShowHitInfoProperty, value)
            End Set
        End Property

        Public Shared ReadOnly AllowShowHitInfoProperty As DependencyProperty = DependencyProperty.Register("AllowShowHitInfo", GetType(Boolean), GetType(HitTesting), New UIPropertyMetadata(True))


        Public Sub New()
            InitializeComponent()

            AddHandler viewsListBox.EditValueChanged, AddressOf viewsListBox_SelectionChanged

            FloatingContainer.AddFloatingContainerIsOpenChangedHandler(Me, AddressOf OnFloatingContainerIsOpenChanged)
            AddHandler grid.Loaded, AddressOf grid_Loaded

            hitIfoItemsControl.ItemsSource = hitInfoList

        End Sub
        Private Sub grid_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim mBinding As New MultiBinding()
            mBinding.Mode = BindingMode.OneWay
            Dim bIsMouseOver As New Binding() With {.Mode = BindingMode.OneWay, .ElementName = "grid", .Path = New PropertyPath("IsMouseOver", Nothing)}
            mBinding.Bindings.Add(bIsMouseOver)
            Dim bIsMouseCaptureWithin As New Binding() With {.Mode = BindingMode.OneWay, .ElementName = "grid", .Path = New PropertyPath("IsMouseCaptureWithin", Nothing), .Converter = New NegationConverterExtension()}
            mBinding.Bindings.Add(bIsMouseCaptureWithin)
            Dim bIsChecked As New Binding() With {.Mode = BindingMode.OneWay, .ElementName = "showHitInfoCheckEdit", .Path = New PropertyPath("IsChecked", Nothing)}
            mBinding.Bindings.Add(bIsChecked)
            Dim bAllowShowHitInfo As New Binding() With {.Mode = BindingMode.OneWay, .RelativeSource = New RelativeSource(RelativeSourceMode.FindAncestor, Me.GetType(), 1), .Path = New PropertyPath("AllowShowHitInfo", Nothing)}
            mBinding.Bindings.Add(bAllowShowHitInfo)
            mBinding.Converter = New AndConverter()
            hitInfoPopup.SetBinding(PopupBase.IsOpenProperty, mBinding)
        End Sub
        Private Sub viewsListBox_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            grid.View = CType(FindResource(If(viewsListBox.SelectedIndex = 0, "tableView", "cardView")), GridViewBase)
        End Sub

        Private Sub grid_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs)
            Dim location As Point = e.GetPosition(grid)
            Dim hOffset As Double = location.X - startPosition.X
            If FlowDirection = System.Windows.FlowDirection.RightToLeft Then
                hOffset = -hOffset
            End If

            hitInfoPopup.HorizontalOffset = hOffset
            hitInfoPopup.VerticalOffset = location.Y - startPosition.Y

            Dim info As GridViewHitInfoBase = GetHitInfo(e)

            hitInfoList.Clear()

            AddHitInfo("HitTest", TypeDescriptor.GetProperties(info)("HitTest").GetValue(info).ToString())

            AddHitInfo("Column",If(info.Column IsNot Nothing, TryCast(info.Column.HeaderCaption, String), "No column"))
            AddHitInfo("RowHandle", GetRowHandleDescription(info.RowHandle))
            AddHitInfo("CellValue",If(info.Column IsNot Nothing, grid.GetCellDisplayText(info.RowHandle, info.Column), Nothing))
            info.Accept(CreateDemoHitTestVisitor())
        End Sub
        Private Sub OnFloatingContainerIsOpenChanged(ByVal sender As Object, ByVal e As FloatingContainerEventArgs)
            If e.Container.IsOpen Then
                floatingContainerIsOpenCount += 1
            Else
                floatingContainerIsOpenCount -= 1
            End If
            AllowShowHitInfo = floatingContainerIsOpenCount = 0
        End Sub
        Private Function CreateDemoHitTestVisitor() As GridViewHitTestVisitorBase
            If TypeOf grid.View Is DevExpress.Xpf.Grid.TableView Then
                Return New DemoTableViewHitTestVisitor(Me)
            End If
            Return New DemoCardViewHitTestVisitor(Me)
        End Function
        Private Function GetHitInfo(ByVal e As RoutedEventArgs) As GridViewHitInfoBase
            If TypeOf grid.View Is DevExpress.Xpf.Grid.TableView Then
                Return CType(TableView.CalcHitInfo(TryCast(e.OriginalSource, DependencyObject)), GridViewHitInfoBase)
            End If
            Return CType(grid.View, DevExpress.Xpf.Grid.CardView).CalcHitInfo(TryCast(e.OriginalSource, DependencyObject))

        End Function
        Private Function GetRowHandleDescription(ByVal rowHanle As Integer) As String
            If rowHanle = GridControl.InvalidRowHandle Then
                Return "No row"
            End If
            If rowHanle = GridControl.NewItemRowHandle Then
                Return "New Item Row"
            End If
            If rowHanle = GridControl.AutoFilterRowHandle Then
                Return "Auto Filter Row"
            End If
            Return String.Format("{0} ({1})", rowHanle,If(grid.IsGroupRowHandle(rowHanle), "group row", "data row"))
        End Function
        Friend Sub AddHitInfo(ByVal name As String, ByVal text As String)
            hitInfoList.Add(New HitTestInfo(name, text))
        End Sub
        Friend Sub RemoveHitInfo(ByVal name As String)
            Dim infoToRemove As HitTestInfo = hitInfoList.Where(Function(info) info.Name = name).FirstOrDefault()
            If infoToRemove IsNot Nothing Then
                hitInfoList.Remove(infoToRemove)
            End If
        End Sub
        Friend Sub AddTotalSummaryInfo(ByVal column As ColumnBase)
            AddHitInfo("TotalSummary", column.TotalSummaryText)
        End Sub
        Friend Sub AddFixedTotalSummaryInfo(ByVal summaryText As String)
            RemoveHitInfo("CellValue")
            AddHitInfo("FixedTotalSummary", summaryText)
        End Sub
        Friend Sub AddGroupValueInfo(ByVal columnData As GridColumnData)
            AddHitInfo("GroupValue", String.Format("{0}: {1}", columnData.Column.FieldName, columnData.Value))
        End Sub
        Friend Sub AddGroupSummaryInfo(ByVal summaryData As GridGroupSummaryData)
            AddHitInfo("GroupSummary", summaryData.Text)
        End Sub
        Private Sub hitInfoPopup_Opened(ByVal sender As Object, ByVal e As EventArgs)
            startPosition = Mouse.GetPosition(grid)
        End Sub

    End Class
End Namespace
