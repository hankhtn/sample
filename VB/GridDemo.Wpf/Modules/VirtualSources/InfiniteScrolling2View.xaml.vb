Imports DevExpress.Xpf.Data
Imports System
Imports System.ComponentModel
Imports System.Linq
Imports System.Windows.Controls

Namespace GridDemo.VirtualSources
    Partial Public Class InfiniteScrolling2View
        Inherits UserControl

        Public Sub New()
            InitializeComponent()

'            #Region "create and dispose"
            Dim source = New InfiniteAsyncSource() With {.ElementType = GetType(IssueData)}
            AddHandler Unloaded, Sub(o, e) source.Dispose()
'            #End Region

            AddHandler source.FetchRows, Sub(o, e)
'                #Region "!"
                Dim sortOrder As IssueSortOrder = IssueSortOrder.Default
                If e.SortOrder.Length > 0 Then
                    Dim sort = e.SortOrder.Single()
                    If sort.PropertyName = "Created" Then
                        If sort.Direction <> ListSortDirection.Descending Then
                            Throw New InvalidOperationException()
                        End If
                        sortOrder = IssueSortOrder.CreatedDescending
                    End If
                    If sort.PropertyName = "Votes" Then
                        sortOrder = If(sort.Direction = ListSortDirection.Ascending, IssueSortOrder.VotesAscending, IssueSortOrder.VotesDescending)
                    End If
                End If
'                #End Region

                Dim filter As IssueFilter = Nothing

'                #Region "service request"
                Const pageSize As Integer = 30
                Dim issuesTask = IssuesService.GetIssuesAsync(page:= e.Skip / pageSize, pageSize:= pageSize, sortOrder:= sortOrder, filter:= filter)

                e.Result = issuesTask.ContinueWith(Function(t) New FetchRowsResult(t.Result, hasMoreRows:= t.Result.Length = pageSize))
'                #End Region
            End Sub

            grid.ItemsSource = source
        End Sub
    End Class
End Namespace
