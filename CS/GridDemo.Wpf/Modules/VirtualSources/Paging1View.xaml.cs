using DevExpress.Xpf.Data;
using System.Windows.Controls;

namespace GridDemo.VirtualSources {
    public partial class Paging1View : UserControl {
        public Paging1View() {
            InitializeComponent();

            #region !
            var source = new PagedAsyncSource() {
                ElementType = typeof(IssueData),
                PageSize = 10,
                PageNavigationMode = PageNavigationMode.Arbitrary
            };
            Unloaded += (o, e) => {
                source.Dispose();
            };

            source.FetchPage += (o, e) => {
                IssueSortOrder sortOrder = IssueSortOrder.Default;
                IssueFilter filter = null;

                var issuesTask = IssuesService.GetIssuesAsync(
                    page: e.Skip / e.Take,
                    pageSize: e.Take,
                    sortOrder: sortOrder,
                    filter: filter);

                e.Result = issuesTask.ContinueWith(t => {
                    return new FetchRowsResult(t.Result, hasMoreRows: t.Result.Length == e.Take);
                });
            };

            grid.ItemsSource = source; 
            #endregion
        }
    }
}
