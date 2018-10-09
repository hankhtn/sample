Imports System.Windows.Input
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Grid

Namespace DevExpress.DevAV
    Public Class CollectionViewBehavior
        Inherits Behavior(Of GridControl)

        Private privateSortAscendingCommand As ICommand
        Public Property SortAscendingCommand() As ICommand
            Get
                Return privateSortAscendingCommand
            End Get
            Private Set(ByVal value As ICommand)
                privateSortAscendingCommand = value
            End Set
        End Property
        Private privateSortDescendingCommand As ICommand
        Public Property SortDescendingCommand() As ICommand
            Get
                Return privateSortDescendingCommand
            End Get
            Private Set(ByVal value As ICommand)
                privateSortDescendingCommand = value
            End Set
        End Property
        Public Sub New()
            SortAscendingCommand = New DelegateCommand(Of String)(Sub(x)
                If AssociatedObject.Columns(x) IsNot Nothing AndAlso AssociatedObject IsNot Nothing Then
                    AssociatedObject.SortBy(AssociatedObject.Columns(x), Data.ColumnSortOrder.Ascending)
                End If
            End Sub)
            SortDescendingCommand = New DelegateCommand(Of String)(Sub(x)
                If AssociatedObject.Columns(x) IsNot Nothing AndAlso AssociatedObject IsNot Nothing Then
                    AssociatedObject.SortBy(AssociatedObject.Columns(x), Data.ColumnSortOrder.Descending)
                End If
            End Sub)
        End Sub
        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
        End Sub
        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
        End Sub
    End Class
End Namespace
