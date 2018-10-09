Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks

Namespace GridDemo
    Public NotInheritable Class IssuesService

        Private Sub New()
        End Sub

        #Region "helpers"
        Shared Sub New()
            AllIssues = New Lazy(Of IssueData())(Function()
                Dim [date] = Date.Today
                Dim rnd = New Random(0)
                Return Enumerable.Range(0, 100000).Select(Function(i)
                    [date] = [date].AddSeconds(-rnd.Next(20 * 60))
                    Return New IssueData(subject:= OutlookDataGenerator.GetSubject(), user:= OutlookDataGenerator.GetFrom(), created:= [date], votes:= rnd.Next(100), priority:= OutlookDataGenerator.GetPriority())
                End Function).ToArray()
            End Function)
        End Sub
        Private Shared ReadOnly AllIssues As Lazy(Of IssueData())
        Private Class DefaultSortComparer
            Implements IComparer(Of IssueData)

            Private Function IComparerGeneric_Compare(ByVal x As IssueData, ByVal y As IssueData) As Integer Implements IComparer(Of IssueData).Compare
                If x.Created.Date <> y.Created.Date Then
                    Return Comparer(Of Date).Default.Compare(x.Created.Date, y.Created.Date)
                End If
                Return Comparer(Of Integer).Default.Compare(x.Votes, y.Votes)
            End Function
        End Class
        #End Region

        Public Shared Function GetIssuesAsync(ByVal page As Integer, ByVal pageSize As Integer, ByVal sortOrder As IssueSortOrder, ByVal filter As IssueFilter) As Task(Of IssueData())
            Return System.Threading.Tasks.Task.Factory.StartNew(Function()
                Thread.Sleep(300)
                Dim issues = SortIssues(sortOrder, AllIssues.Value)
                If filter IsNot Nothing Then
                    issues = FilterIssues(filter, issues)
                End If
                Return issues.Skip(page * pageSize).Take(pageSize).ToArray()
            End Function)
        End Function

        Public Shared Function GetSummariesAsync(ByVal filter As IssueFilter) As Task(Of IssuesSummaries)
            Return System.Threading.Tasks.Task.Factory.StartNew(Function()
                Thread.Sleep(300)
                Dim issues = DirectCast(AllIssues.Value, IEnumerable(Of IssueData))
                If filter IsNot Nothing Then
                    issues = FilterIssues(filter, issues)
                End If
                Dim lastCreated As Date? = If(issues.Any(), issues.Max(Function(x) x.Created), DirectCast(Nothing, Date?))
                Return New IssuesSummaries(count:= issues.Count(), lastCreated:= lastCreated)
            End Function)
        End Function

        #Region "filter"
        Private Shared Function FilterIssues(ByVal filter As IssueFilter, ByVal issues As IEnumerable(Of IssueData)) As IEnumerable(Of IssueData)
            If filter.CreatedFrom IsNot Nothing OrElse filter.CreatedTo IsNot Nothing Then
                If filter.CreatedFrom Is Nothing OrElse filter.CreatedTo Is Nothing Then
                    Throw New InvalidOperationException()
                End If
                issues = issues.Where(Function(x) x.Created >= filter.CreatedFrom.Value AndAlso x.Created < filter.CreatedTo)
            End If
            If filter.MinVotes IsNot Nothing Then
                issues = issues.Where(Function(x) x.Votes >= filter.MinVotes.Value)
            End If
            If filter.Priority IsNot Nothing Then
                issues = issues.Where(Function(x) x.Priority = filter.Priority)
            End If
            Return issues
        End Function
        #End Region

        #Region "sort"
        Private Shared Function SortIssues(ByVal sortOrder As IssueSortOrder, ByVal issues As IEnumerable(Of IssueData)) As IEnumerable(Of IssueData)
            Select Case sortOrder
            Case IssueSortOrder.Default
                Return issues.OrderByDescending(Function(x) x, New DefaultSortComparer()).ThenByDescending(Function(x) x.Created)
            Case IssueSortOrder.CreatedDescending
                Return issues.OrderByDescending(Function(x) x.Created)
            Case IssueSortOrder.VotesAscending
                Return issues.OrderBy(Function(x) x.Votes).ThenByDescending(Function(x) x.Created)
            Case IssueSortOrder.VotesDescending
                Return issues.OrderByDescending(Function(x) x.Votes).ThenByDescending(Function(x) x.Created)
            Case Else
                Throw New InvalidOperationException()
            End Select
        End Function
        #End Region
    End Class
End Namespace
