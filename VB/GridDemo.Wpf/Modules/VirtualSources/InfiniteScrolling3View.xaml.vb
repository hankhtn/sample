Imports DevExpress.Data.Filtering
Imports DevExpress.Xpf.Data
Imports System
Imports System.ComponentModel
Imports System.Linq
Imports System.Windows.Controls

Namespace GridDemo.VirtualSources
    Partial Public Class InfiniteScrolling3View
        Inherits UserControl

        Public Sub New()
            InitializeComponent()

'            #Region "create and dispose"
            Dim source = New InfiniteAsyncSource() With {.ElementType = GetType(IssueData)}
            AddHandler Unloaded, Sub(o, e) source.Dispose()
'            #End Region

            AddHandler source.FetchRows, Sub(o, e)
'                #Region "sorting"
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

'                #Region "!"
                Dim filter As IssueFilter = MakeIssueFilter(e.Filter)
'                #End Region

'                #Region "service request"
                Const pageSize As Integer = 30
                Dim issuesTask = IssuesService.GetIssuesAsync(page:= e.Skip / pageSize, pageSize:= pageSize, sortOrder:= sortOrder, filter:= filter)

                e.Result = issuesTask.ContinueWith(Function(t) New FetchRowsResult(t.Result, hasMoreRows:= t.Result.Length = pageSize))
'                #End Region
            End Sub

'            #Region "!"
            AddHandler source.GetUniqueValues, Sub(o, e)
                If e.PropertyName = "Priority" Then
                    Dim values = System.Enum.GetValues(GetType(Priority)).Cast(Of Object)().ToArray()
                    e.Result = System.Threading.Tasks.Task.Factory.StartNew(Function() values)
                Else
                    Throw New InvalidOperationException()
                End If
            End Sub
'            #End Region

            grid.ItemsSource = source
        End Sub

        #Region "!"
        Private Shared Function MakeIssueFilter(ByVal filter As CriteriaOperator) As IssueFilter
            Return filter.Match(binary:= Function(propertyName, value, type)
                If propertyName = "Votes" AndAlso type = BinaryOperatorType.GreaterOrEqual Then
                    Return New IssueFilter(minVotes:= CInt((value)))
                End If
                If propertyName = "Priority" AndAlso type = BinaryOperatorType.Equal Then
                    Return New IssueFilter(priority:= CType(value, Priority))
                End If
                If propertyName = "Created" Then
                    If type = BinaryOperatorType.GreaterOrEqual Then
                        Return New IssueFilter(createdFrom:= CDate(value))
                    End If
                    If type = BinaryOperatorType.Less Then
                        Return New IssueFilter(createdTo:= CDate(value))
                    End If
                End If
                Throw New InvalidOperationException()
            End Function, and:= Function(filters) New IssueFilter(createdFrom:= filters.Select(Function(x) x.CreatedFrom).SingleOrDefault(Function(x) x IsNot Nothing), createdTo:= filters.Select(Function(x) x.CreatedTo).SingleOrDefault(Function(x) x IsNot Nothing), minVotes:= filters.Select(Function(x) x.MinVotes).SingleOrDefault(Function(x) x IsNot Nothing), priority:= filters.Select(Function(x) x.Priority).SingleOrDefault(Function(x) x IsNot Nothing)), null:= Nothing)
        End Function
        #End Region
    End Class
End Namespace
