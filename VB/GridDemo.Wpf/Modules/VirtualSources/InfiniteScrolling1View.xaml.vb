Imports DevExpress.Xpf.Data
Imports System.Windows.Controls

Namespace GridDemo.VirtualSources
    Partial Public Class InfiniteScrolling1View
        Inherits UserControl

        Public Sub New()
            InitializeComponent()

'            #Region "!"
            Dim source = New InfiniteAsyncSource() With {.ElementType = GetType(IssueData)}
            AddHandler Unloaded, Sub(o, e) source.Dispose()

            AddHandler source.FetchRows, Sub(o, e)
                Dim sortOrder As IssueSortOrder = IssueSortOrder.Default
                Dim filter As IssueFilter = Nothing

                Const pageSize As Integer = 30
                Dim issuesTask = IssuesService.GetIssuesAsync(page:= e.Skip / pageSize, pageSize:= pageSize, sortOrder:= sortOrder, filter:= filter)

                e.Result = issuesTask.ContinueWith(Function(t) New FetchRowsResult(t.Result, hasMoreRows:= t.Result.Length = pageSize))
            End Sub

            grid.ItemsSource = source
'            #End Region
        End Sub
    End Class
End Namespace
