Imports DevExpress.Mvvm
Imports DevExpress.Utils
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Windows.Input

Namespace GridDemo
    Public Class GridSearchPanelViewModel
        Inherits BindableBase


        Private selectAllItemsCommand_Renamed As ICommand

        Private columns_Renamed As IList(Of ListColumn)

        Private view_Renamed As DataViewBase
        Public Property View() As DataViewBase
            Get
                Return Me.view_Renamed
            End Get
            Set(ByVal value As DataViewBase)
                SetProperty(view_Renamed, value, Function() View)
            End Set
        End Property
        Public Property Columns() As IList(Of ListColumn)
            Get
                Return Me.columns_Renamed
            End Get
            Set(ByVal value As IList(Of ListColumn))
                SetProperty(columns_Renamed, value, Function() Columns)
            End Set
        End Property
        Public Property SelectAllItemsCommand() As ICommand
            Get
                Return Me.selectAllItemsCommand_Renamed
            End Get
            Set(ByVal value As ICommand)
                SetProperty(selectAllItemsCommand_Renamed, value, Function() SelectAllItemsCommand)
            End Set
        End Property
        Private privateChangeAllowSearchPanelCommand As ICommand
        Public Property ChangeAllowSearchPanelCommand() As ICommand
            Get
                Return privateChangeAllowSearchPanelCommand
            End Get
            Private Set(ByVal value As ICommand)
                privateChangeAllowSearchPanelCommand = value
            End Set
        End Property
        Private privateChangeSearchPanelVisibilityCommand As ICommand
        Public Property ChangeSearchPanelVisibilityCommand() As ICommand
            Get
                Return privateChangeSearchPanelVisibilityCommand
            End Get
            Private Set(ByVal value As ICommand)
                privateChangeSearchPanelVisibilityCommand = value
            End Set
        End Property
        Private privatePopulateColumnsCommand As ICommand
        Public Property PopulateColumnsCommand() As ICommand
            Get
                Return privatePopulateColumnsCommand
            End Get
            Private Set(ByVal value As ICommand)
                privatePopulateColumnsCommand = value
            End Set
        End Property
        Public ReadOnly Property SelectAllAction() As ChangeSelectionAction
            Get
                Return ChangeSelectionAction.SelectAll
            End Get
        End Property
        Public Sub New()
            ChangeAllowSearchPanelCommand = New DelegateCommand(Of ObservableCollection(Of Object))(AddressOf UpdateAllowSearchPanel, AddressOf CanUpdateAllowSearchPanel)
            ChangeSearchPanelVisibilityCommand = New DelegateCommand(Of Boolean)(AddressOf ChangeSearchPanelVisibility)
            PopulateColumnsCommand = New DelegateCommand(Of GridColumnCollection)(AddressOf PopulateColumns)
            Columns = New List(Of ListColumn)()
        End Sub
        Private Function CanUpdateAllowSearchPanel(ByVal selection As ObservableCollection(Of Object)) As Boolean
            Return selection IsNot Nothing
        End Function
        Private Sub PopulateColumns(ByVal gridColumns As GridColumnCollection)
            Columns = ListColumn.CreateCollection(gridColumns)
        End Sub
        Private Sub ChangeSearchPanelVisibility(ByVal args As Boolean)
            If CBool(args) Then
                View.Commands.ShowSearchPanel.Execute(False)
            Else
                View.Commands.HideSearchPanel.Execute(Nothing)
            End If
        End Sub
        Private Sub UpdateAllowSearchPanel(ByVal selection As ObservableCollection(Of Object))

            For Each listColumn_Renamed In Columns
                listColumn_Renamed.Column.AllowSearchPanel = If(selection.Contains(listColumn_Renamed), DefaultBoolean.True, DefaultBoolean.False)
            Next listColumn_Renamed
        End Sub
    End Class
End Namespace
