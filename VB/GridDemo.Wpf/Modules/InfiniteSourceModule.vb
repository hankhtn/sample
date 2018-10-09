Imports DevExpress.Xpf.DemoBase
Imports GridDemo.VirtualSources
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace GridDemo
    Public Class InfiniteSourceModule
        Inherits GridShowcaseBrowserModule

        Private Const Uri As String = "/Controls-and-Libraries/Data-Grid/Binding-to-Data/Binding-to-any-Data-Source/How-to-Bind-to-Virtual-Source/"
        Friend Const Step2Uri As String = "120195" & Uri & "Step-2-Fetch-Data-and-Enable-Scrolling"
        Friend Const Step3Uri As String = "120196" & Uri & "Step-3-Add-Sorting"
        Friend Const Step4Uri As String = "120197" & Uri & "Step-4-Add-Filtering"
        Friend Const Step5Uri As String = "120198" & Uri & "Step-5-Add-Summaries"
        Friend Const Path As String = "Modules/VirtualSources"

        Protected Overrides Function CreateShowcases() As ShowcaseInfo()

            Return New ShowcaseInfo() { LoadShowcase("Step 1: Scrolling only", Step2Uri, Path, { GetType(InfiniteScrolling1View), GetType(IssueData), GetType(IssuesService) }, showCodeBehind:= True), LoadShowcase("Step 2: Adding sorting", Step3Uri, Path, { GetType(InfiniteScrolling2View), GetType(IssueSortOrder) }, showCodeBehind:= True), LoadShowcase("Step 3: Adding filtering", Step4Uri, Path, { GetType(InfiniteScrolling3View), GetType(IssueFilter) }, showCodeBehind:= True), LoadShowcase("Step 4: Adding summaries", Step5Uri, Path, { GetType(InfiniteScrolling4View), GetType(IssuesSummaries) }, showCodeBehind:= True)}
        End Function
    End Class
End Namespace
