Imports DevExpress.Xpf.DemoBase
Imports GridDemo.VirtualSources

Namespace GridDemo
    Public Class PagingModule
        Inherits GridShowcaseBrowserModule

        Protected Overrides Function CreateShowcases() As ShowcaseInfo()
            Return New ShowcaseInfo() { LoadShowcase("Step 1: Paging only", InfiniteSourceModule.Step2Uri, InfiniteSourceModule.Path, { GetType(Paging1View), GetType(IssueData), GetType(IssuesService) }, showCodeBehind:= True), LoadShowcase("Step 2: Adding sorting", InfiniteSourceModule.Step3Uri, InfiniteSourceModule.Path, { GetType(Paging2View), GetType(IssueSortOrder) }, showCodeBehind:= True), LoadShowcase("Step 3: Adding filtering", InfiniteSourceModule.Step4Uri, InfiniteSourceModule.Path, { GetType(Paging3View), GetType(IssueFilter) }, showCodeBehind:= True), LoadShowcase("Step 4: Adding summaries and total page count", InfiniteSourceModule.Step5Uri, InfiniteSourceModule.Path, { GetType(Paging4View), GetType(IssuesSummaries) }, showCodeBehind:= True)}
        End Function
    End Class
End Namespace
