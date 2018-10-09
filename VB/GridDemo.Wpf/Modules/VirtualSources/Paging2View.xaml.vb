Imports DevExpress.Xpf.Data
Imports System
Imports System.ComponentModel
Imports System.Linq
Imports System.Windows.Controls

Namespace GridDemo.VirtualSources
    Partial Public Class Paging2View
        Inherits UserControl

        Public Sub New()
            InitializeComponent()

'            #Region "create and dispose"
            Dim source = New PagedAsyncSource() With {.ElementType = GetType(IssueData), .PageSize = 10, .PageNavigationMode = PageNavigationMode.Arbitrary}
            AddHandler Unloaded, Sub(o, e) source.Dispose()
'            #End Region

            AddHandler source.FetchPage, Sub(o, e)
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
                Dim issuesTask = IssuesService.GetIssuesAsync(page:= e.Skip / e.Take, pageSize:= e.Take, sortOrder:= sortOrder, filter:= filter)

                e.Result = issuesTask.ContinueWith(Function(t) New FetchRowsResult(t.Result, hasMoreRows:= t.Result.Length = e.Take))
'                #End Region
            End Sub

            grid.ItemsSource = source
        End Sub
    End Class
End Namespace
