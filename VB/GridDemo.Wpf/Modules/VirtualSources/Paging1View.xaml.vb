Imports DevExpress.Xpf.Data
Imports System.Windows.Controls

Namespace GridDemo.VirtualSources
    Partial Public Class Paging1View
        Inherits UserControl

        Public Sub New()
            InitializeComponent()

'            #Region "!"
            Dim source = New PagedAsyncSource() With {.ElementType = GetType(IssueData), .PageSize = 10, .PageNavigationMode = PageNavigationMode.Arbitrary}
            AddHandler Unloaded, Sub(o, e) source.Dispose()

            AddHandler source.FetchPage, Sub(o, e)
                Dim sortOrder As IssueSortOrder = IssueSortOrder.Default
                Dim filter As IssueFilter = Nothing

                Dim issuesTask = IssuesService.GetIssuesAsync(page:= e.Skip / e.Take, pageSize:= e.Take, sortOrder:= sortOrder, filter:= filter)

                e.Result = issuesTask.ContinueWith(Function(t) New FetchRowsResult(t.Result, hasMoreRows:= t.Result.Length = e.Take))
            End Sub

            grid.ItemsSource = source
'            #End Region
        End Sub
    End Class
End Namespace
