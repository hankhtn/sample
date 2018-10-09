Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media.Animation
Imports System.Windows.Threading

Namespace GridDemo
    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/CopyPasteTemplates.xaml"), DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/CopyPasteClasses.(cs)")>
    Partial Public Class CopyPasteOperations
        Inherits GridDemoModule

        Public Shared ReadOnly PasteCompetedEvent As RoutedEvent

        Friend Property Counter() As Integer
        Public Shared ReadOnly PastUnderFocusedRowProperty As DependencyProperty
        Public Shared ReadOnly FocusedGridProperty As DependencyProperty
        Shared Sub New()
            PastUnderFocusedRowProperty = DependencyProperty.Register("PastUnderFocusedRow", GetType(Boolean), GetType(CopyPasteOperations), New PropertyMetadata(True))
            FocusedGridProperty = DependencyProperty.Register("FocusedGrid", GetType(FocusedGrid), GetType(CopyPasteOperations), New PropertyMetadata(FocusedGrid.None, Sub(d, e) CType(d, CopyPasteOperations).UpdateAnimationTarget()))
            PasteCompetedEvent = EventManager.RegisterRoutedEvent("PasteCompeted", RoutingStrategy.Direct, GetType(RoutedEventHandler), GetType(CopyPasteOperations))
        End Sub
        Public Custom Event PasteCompeted As RoutedEventHandler
            AddHandler(ByVal value As RoutedEventHandler)
                [AddHandler](PasteCompetedEvent, value)
            End AddHandler
            RemoveHandler(ByVal value As RoutedEventHandler)
                [RemoveHandler](PasteCompetedEvent, value)
            End RemoveHandler
            RaiseEvent(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
            End RaiseEvent
        End Event
        Public Property PastUnderFocusedRow() As Boolean
            Get
                Return CBool(GetValue(PastUnderFocusedRowProperty))
            End Get
            Set(ByVal value As Boolean)
                SetValue(PastUnderFocusedRowProperty, value)
            End Set
        End Property
        Public Property FocusedGrid() As FocusedGrid
            Get
                Return CType(GetValue(FocusedGridProperty), FocusedGrid)
            End Get
            Set(ByVal value As FocusedGrid)
                SetValue(FocusedGridProperty, value)
            End Set
        End Property
        Private animationElements As New Dictionary(Of Integer, RowsAnimationElement)()
        Private firstList As New BindingList(Of CopyPasteOutlookData)()
        Private secondList As New BindingList(Of CopyPasteOutlookData)()
        Private GridDictionary As New Dictionary(Of FocusedGrid, GridControl)()
        Public Sub New()
            Counter = 0
            InitializeComponent()
            OptionsDataContext = Me
            DemoContentGrid.DataContext = Me
            Dim list As ArrayList = OutlookDataGenerator.CreateOutlookArrayList(10)
            Dim objectForCopying() As Object = list.ToArray()
            For i As Integer = 0 To objectForCopying.Length - 1
                firstList.Add(CopyPasteOutlookData.ConvertOutlookDataToCopyPasteOutlookData(DirectCast(objectForCopying(i), OutlookData), Me))
            Next i
            firstGrid.ItemsSource = firstList
            secondGrid.ItemsSource = secondList
            allowCopyingtoClipboardCheckEdit.IsChecked = True
            copyHeaderCheckEdit.IsChecked = True

            AddHandler firstGrid.PreviewMouseDown, AddressOf firstGrid_PreviewMouseDown
            AddHandler secondGrid.PreviewMouseDown, AddressOf secondGrid_PreviewMouseDown
            GridDictionary.Add(FocusedGrid.First, firstGrid)
            GridDictionary.Add(FocusedGrid.Second, secondGrid)
        End Sub
        Private Sub firstGrid_PreviewMouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            FocusedGrid = FocusedGrid.First
        End Sub
        Private Sub secondGrid_PreviewMouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            FocusedGrid = FocusedGrid.Second
        End Sub
        Private Delegate Sub Action(ByVal view As TableView, ByVal list As BindingList(Of CopyPasteOutlookData))
        Private Function CompareForDeleteList(ByVal x As Integer, ByVal y As Integer) As Integer
            If x > y Then
                Return -1
            ElseIf x < y Then
                Return 1
            End If
            Return 0
        End Function
        Protected Overrides Sub HidePopupContent()
            If secondGrid IsNot Nothing Then
                secondGrid.View.HideColumnChooser()
            End If
            MyBase.HidePopupContent()
        End Sub
        Private Sub CopyingRows(ByVal view As TableView, ByVal list As BindingList(Of CopyPasteOutlookData))
            If view IsNot Nothing Then
                If view.Grid.SelectedItems.Count <> 0 Then
                    view.Grid.CopySelectedItemsToClipboard()
                ElseIf view.FocusedRowHandle <> DataControlBase.InvalidRowHandle Then
                    view.Grid.CopyRowsToClipboard(New Integer() { view.FocusedRowHandle })
                End If
            Else
                textEdit.Copy()
            End If
        End Sub
        Private Sub RemoveRow(ByVal view As TableView, ByVal rowHandle As Integer)
            RemoveAnimationElement(view.Grid.GetListIndexByRowHandle(rowHandle))
            view.DeleteRow(rowHandle)
        End Sub
        Private Sub DeleteRows(ByVal view As TableView, ByVal list As BindingList(Of CopyPasteOutlookData))
            If view IsNot Nothing Then
                If view.Grid.SelectedItems.Count <> 0 Then
                    view.Grid.BeginDataUpdate()
                    Dim selectedList As New List(Of Integer)(view.Grid.GetSelectedRowHandles())
                    selectedList.Sort(AddressOf CompareForDeleteList)
                    For Each row As Integer In selectedList
                        RemoveRow(view, row)
                    Next row
                    view.Grid.EndDataUpdate()
                ElseIf view.FocusedRowHandle <> DataControlBase.InvalidRowHandle Then
                    RemoveRow(view, view.FocusedRowHandle)
                End If
            Else
                textEdit.Delete()
            End If
        End Sub
        Private Sub CutRows(ByVal view As TableView, ByVal list As BindingList(Of CopyPasteOutlookData))
            CopyingRows(view, Nothing)
            DeleteRows(view, Nothing)
        End Sub

        Private ReadOnly maxAnimationRows_Renamed As Integer = 30
        Friend ReadOnly Property MaxAnimationRows() As Integer
            Get
                Return Me.maxAnimationRows_Renamed
            End Get
        End Property
        Friend Sub PasteRowsWithoutAnimation(ByRef positionNewRow As Integer, ByVal view As GridViewBase, ByVal list As BindingList(Of CopyPasteOutlookData), ByVal objectsForCopy() As Object, ByVal start As Integer, ByVal [end] As Integer)
            view.Grid.BeginDataUpdate()
            PasteRowsWithAnimation(positionNewRow, view, list, objectsForCopy, start, [end])
            view.Grid.EndDataUpdate()
        End Sub
        Private Sub PasteRowsWithAnimation(ByRef positionNewRow As Integer, ByVal view As GridViewBase, ByVal list As BindingList(Of CopyPasteOutlookData), ByVal objectsForCopy() As Object, ByVal start As Integer, ByVal [end] As Integer)
            Dim insertToEndOfRows As Boolean = view.FocusedRowHandle = GridControl.InvalidRowHandle
            For i As Integer = start To [end] - 1
                Dim obj As CopyPasteOutlookData = (DirectCast(objectsForCopy(i), CopyPasteOutlookData))
                Counter += 1
                obj.UniqueID = Counter
                If i = maxAnimationRows_Renamed - 1 Then
                    pasteWithoutAnimation = New PasteHelper() With {.List = list, .ObjectsForCopy = objectsForCopy, .Owner = Me, .PositionNewRow = positionNewRow, .View = view}
                End If
                If PastUnderFocusedRow AndAlso (list.Count <> 0) AndAlso (Not insertToEndOfRows) Then
                    list.Insert(positionNewRow, obj)
                    positionsNewRowsList.Add(positionNewRow)
                    positionNewRow += 1
                Else
                    list.Add(obj)
                    positionsNewRowsList.Add(list.Count - 1)
                End If
            Next i
        End Sub
        Private positionsNewRowsList As New List(Of Integer)()
        Private isPasting As Boolean = False
        Private pasteWithoutAnimation As PasteHelper = Nothing
        Private animationTarget As FocusedGrid = FocusedGrid.None
        Private Sub UpdateAnimationTarget()
            If Not animation Then
                animationTarget = FocusedGrid
            End If
        End Sub
        Private Sub PasteRows(ByVal view As GridViewBase, ByVal list As BindingList(Of CopyPasteOutlookData))
            If view IsNot Nothing Then
                Dim arrayList As ArrayList = Nothing
                Dim dataObj As IDataObject = GetClipboardDataObject()
                Dim format As String = GetType(ArrayList).FullName
                If dataObj IsNot Nothing AndAlso dataObj.GetDataPresent(format) Then
                    positionsNewRowsList.Clear()
                    pasteWithoutAnimation = Nothing
                    animation = True
                    StartPasteForCanCommands()
                    arrayList = TryCast(dataObj.GetData(format), ArrayList)
                    Dim objectsForCopy() As Object = arrayList.ToArray()
                    Dim positionNewRow As Integer = If(view.FocusedRowHandle = DataControlBase.InvalidRowHandle, 0, view.Grid.GetListIndexByRowHandle(view.FocusedRowHandle) + 1)
                    If maxAnimationRows_Renamed > objectsForCopy.Length Then
                        PasteRowsWithAnimation(positionNewRow, view, list, objectsForCopy, 0, objectsForCopy.Length)
                    Else
                        PasteRowsWithAnimation(positionNewRow, view, list, objectsForCopy, 0, maxAnimationRows_Renamed)
                    End If
                    Dispatcher.BeginInvoke(New System.Action(AddressOf AnimationRowsOfPasted), DispatcherPriority.Background)
                End If
            Else
                textEdit.Paste()
            End If
        End Sub
        Private Sub StartPasteForCanCommands()
            Cursor = Cursors.Wait
            isPasting = True
            CommandManager.InvalidateRequerySuggested()
        End Sub
        Friend Sub EndPasteForCanCommands()
            Cursor = Cursors.Arrow
            isPasting = False
            CommandManager.InvalidateRequerySuggested()
        End Sub
        Private Sub AnimationRowsOfPasted()
            For i As Integer = 0 To positionsNewRowsList.Count - 1
                If i = (positionsNewRowsList.Count - 1) Then
                    StartAnimation(positionsNewRowsList(i), pasteWithoutAnimation)
                    If pasteWithoutAnimation Is Nothing Then
                        EndPasteForCanCommands()
                    End If
                Else
                    StartAnimation(positionsNewRowsList(i), Nothing)
                End If
            Next i
            animation = False
            UpdateAnimationTarget()
        End Sub
        Private Sub StartAnimation(ByVal positionNewRow As Integer, ByVal pasteWithoutAnimation As PasteHelper)
            Dim addingStoryboard As Storyboard = GetStoryboard("newRowStoryboard")
            Dim colorStoryboard As Storyboard = GetStoryboard("newRowColorStoryboard")
            If pasteWithoutAnimation IsNot Nothing Then
                pasteWithoutAnimation.ColorStoryboard = colorStoryboard
                AddHandler colorStoryboard.Completed, AddressOf pasteWithoutAnimation.ColorStoryboardCompleted
            End If
            If positionsNewRowsList(positionsNewRowsList.Count - 1) = positionNewRow Then
                Dim pasteHelper As New PasteCompetedHelper() With {.Owner = Me, .ColorStoryboard = colorStoryboard}
                AddHandler colorStoryboard.Completed, AddressOf pasteHelper.ColorStoryboardCompleted
            End If
            StartStoryboard(addingStoryboard, positionNewRow, RowsAnimationElement.NewRowsProgressProperty)
            StartStoryboard(colorStoryboard, positionNewRow, RowsAnimationElement.NewRowsColorProperty)
        End Sub
        Friend Sub RaisePasteCompetedEvent(ByVal e As RoutedEventArgs)
            [RaiseEvent](e)
        End Sub
        Private Sub StartStoryboard(ByVal storyboard As Storyboard, ByVal indexElement As Integer, ByVal [property] As DependencyProperty)
            System.Windows.Media.Animation.Storyboard.SetTargetProperty(storyboard, New PropertyPath([property].Name))
            storyboard.Begin(GetAnimationElement(indexElement), HandoffBehavior.SnapshotAndReplace)
        End Sub
        Private Function GetStoryboard(ByVal resourceKey As String) As Storyboard
            Return CType(FindResource(resourceKey), Storyboard).Clone()
        End Function
        Private Sub CommandExecute(ByVal actionForCommand As Action)
            If IsFocusedTextEdit() Then
                actionForCommand(Nothing, Nothing)
            ElseIf FocusedGrid = FocusedGrid.First Then
                actionForCommand(CType(firstGrid.View, TableView), firstList)
            ElseIf FocusedGrid = FocusedGrid.Second Then
                actionForCommand(CType(secondGrid.View, TableView), secondList)
            End If
        End Sub
        Private Function IsFocusedTextEdit() As Boolean
            Return If((textEdit IsNot Nothing) AndAlso (textEdit.IsKeyboardFocusWithin), True, False)
        End Function
        Private Function IsSelectRows(ByVal view As TableView) As Boolean
            Return If((view.Grid.SelectedItems.Count <> 0) OrElse (view.FocusedRowHandle <> DataControlBase.InvalidRowHandle), True, False)
        End Function
        Private Function CanExecuteOutputCommands() As Boolean
            If IsFocusedTextEdit() Then
                Return (textEdit.SelectionLength <> 0)
            ElseIf FocusedGrid <> FocusedGrid.None Then
                Return IsSelectRows(CType(GridDictionary(FocusedGrid).View, TableView))
            End If
            Return False
        End Function
        Private Function CanExecuteInputCommands() As Boolean
            If isPasting Then
                Return False
            End If
            If IsFocusedTextEdit() Then
                Dim dataObject As IDataObject = Clipboard.GetDataObject()
                Dim text As String = If(dataObject IsNot Nothing, DirectCast(dataObject, DataObject).GetText(), Nothing)
                If Not String.IsNullOrEmpty(text) Then
                    Return True
                End If
            End If
            If FocusedGrid = FocusedGrid.None Then
                Return False
            End If
            Dim dataObj As IDataObject = GetClipboardDataObject()
            Dim format As String = GetType(ArrayList).FullName
            If dataObj IsNot Nothing AndAlso dataObj.GetDataPresent(format) Then
                Return True
            End If
            Return False
        End Function
        Private Function CanCopyCommands() As Boolean
            If IsFocusedTextEdit() Then
                Return (textEdit.SelectionLength <> 0)
            ElseIf FocusedGrid <> FocusedGrid.None Then
                Return (If(GridDictionary(FocusedGrid).ClipboardCopyMode <> ClipboardCopyMode.None, True, False))
            End If
            Return False
        End Function
        Private Function GetClipboardDataObject() As IDataObject
            Try
                Return Clipboard.GetDataObject()
            Catch
                Return Nothing
            End Try
        End Function
        Private Sub CopyCommandBinding_Executed(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
            CommandExecute(AddressOf CopyingRows)
        End Sub
        Private Sub CutCommandBinding_Executed(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
            CommandExecute(AddressOf CutRows)
        End Sub
        Private Sub PasteCommandBinding_Executed(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
            CommandExecute(AddressOf PasteRows)
        End Sub
        Private Sub DeleteCommandBinding_Executed(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
            CommandExecute(AddressOf DeleteRows)
        End Sub
        Private Sub CopyCommandBinding_CanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
            e.CanExecute = CanExecuteOutputCommands()
            If e.CanExecute Then
                e.CanExecute = CanCopyCommands()
            End If
            e.Handled = True
        End Sub
        Private Sub CutCommandBinding_CanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
            e.CanExecute = CanExecuteOutputCommands()
            If e.CanExecute Then
                e.CanExecute = CanCopyCommands()
            End If
            e.Handled = True
        End Sub
        Private Sub PasteCommandBinding_CanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
            e.CanExecute = CanExecuteInputCommands()
            e.Handled = True
        End Sub
        Private Sub DeleteCommandBinding_CanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
            e.CanExecute = CanExecuteOutputCommands()
            e.Handled = True
        End Sub

        Private Sub Grid_CustomUnboundColumnData(ByVal sender As Object, ByVal e As GridColumnDataEventArgs)
            If e.Column IsNot Nothing AndAlso e.Column.FieldName = "AnimationElement" Then
                Dim index As Integer = GetIndexForAnimationElements(e.ListSourceRowIndex, e.Source.Equals(secondGrid))
                e.Value = GetAnimationElementCore(index)
            End If
        End Sub
        Private Function GetIndexForAnimationElements(ByVal index As Integer) As Integer
            Return GetIndexForAnimationElements(index, animationTarget = FocusedGrid.Second)
        End Function
        Private Function GetIndexForAnimationElements(ByVal index As Integer, ByVal isSecondGrid As Boolean) As Integer
            Return If(isSecondGrid, secondList(index).UniqueID, firstList(index).UniqueID)
        End Function
        Private animation As Boolean = False
        Private Function GetAnimationElement(ByVal index As Integer) As RowsAnimationElement
            Return GetAnimationElementCore(GetIndexForAnimationElements(index))
        End Function
        Private Function GetAnimationElementCore(ByVal index As Integer) As RowsAnimationElement
            Dim result As RowsAnimationElement = Nothing
            If Not animationElements.TryGetValue(index, result) Then
                result = New RowsAnimationElement()
                animationElements.Add(index, result)
                If animation Then
                    result.NewRowsProgress = 0
                End If
            End If
            Return result
        End Function
        Private Sub RemoveAnimationElement(ByVal index As Integer)
            index = GetIndexForAnimationElements(index)
            If animationElements.ContainsKey(index) Then
                animationElements.Remove(index)
            End If
        End Sub
        Private Sub allowCopyingtoClipboardCheckEdit_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SetPropertyToGrids(ClipboardCopyMode.Default)
        End Sub
        Private Sub allowCopyingtoClipboardCheckEdit_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SetPropertyToGrids(ClipboardCopyMode.None)
        End Sub
        Private Sub copyHeaderCheckEdit_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SetPropertyToGrids(ClipboardCopyMode.IncludeHeader)
        End Sub
        Private Sub copyHeaderCheckEdit_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SetPropertyToGrids(ClipboardCopyMode.ExcludeHeader)
        End Sub
        Private Sub SetPropertyToGrids(ByVal copyMode As ClipboardCopyMode)
            firstGrid.ClipboardCopyMode = copyMode
            secondGrid.ClipboardCopyMode = copyMode
        End Sub
    End Class
End Namespace
