using DevExpress.Xpf.DemoBase;
using GridDemo.VirtualSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridDemo {
    public class InfiniteSourceModule : GridShowcaseBrowserModule {
        const string Uri = "/Controls-and-Libraries/Data-Grid/Binding-to-Data/Binding-to-any-Data-Source/How-to-Bind-to-Virtual-Source/";
        internal const string Step2Uri = "120195" + Uri + "Step-2-Fetch-Data-and-Enable-Scrolling";
        internal const string Step3Uri = "120196" + Uri + "Step-3-Add-Sorting";
        internal const string Step4Uri = "120197" + Uri + "Step-4-Add-Filtering";
        internal const string Step5Uri = "120198" + Uri + "Step-5-Add-Summaries";
        internal const string Path = "Modules/VirtualSources";

        protected override ShowcaseInfo[] CreateShowcases() {
            
            return new ShowcaseInfo[] {
                LoadShowcase("Step 1: Scrolling only", Step2Uri, Path, new[] { typeof(InfiniteScrolling1View), typeof(IssueData), typeof(IssuesService) }, showCodeBehind: true),
                LoadShowcase("Step 2: Adding sorting", Step3Uri, Path, new[] { typeof(InfiniteScrolling2View), typeof(IssueSortOrder) }, showCodeBehind: true),
                LoadShowcase("Step 3: Adding filtering", Step4Uri, Path, new[] { typeof(InfiniteScrolling3View), typeof(IssueFilter) }, showCodeBehind: true),
                LoadShowcase("Step 4: Adding summaries", Step5Uri, Path, new[] { typeof(InfiniteScrolling4View), typeof(IssuesSummaries) }, showCodeBehind: true),
            };
        }
    }
}
