using DevExpress.Xpf.DemoBase;
using GridDemo.VirtualSources;

namespace GridDemo {
    public class PagingModule : GridShowcaseBrowserModule {
        protected override ShowcaseInfo[] CreateShowcases() {
            return new ShowcaseInfo[] {
                LoadShowcase("Step 1: Paging only", InfiniteSourceModule.Step2Uri, InfiniteSourceModule.Path, new[] { typeof(Paging1View), typeof(IssueData), typeof(IssuesService) }, showCodeBehind: true),
                LoadShowcase("Step 2: Adding sorting", InfiniteSourceModule.Step3Uri, InfiniteSourceModule.Path, new[] { typeof(Paging2View), typeof(IssueSortOrder) }, showCodeBehind: true),
                LoadShowcase("Step 3: Adding filtering", InfiniteSourceModule.Step4Uri, InfiniteSourceModule.Path, new[] { typeof(Paging3View), typeof(IssueFilter) }, showCodeBehind: true),
                LoadShowcase("Step 4: Adding summaries and total page count", InfiniteSourceModule.Step5Uri, InfiniteSourceModule.Path, new[] { typeof(Paging4View), typeof(IssuesSummaries) }, showCodeBehind: true),
            };
        }
    }
}
