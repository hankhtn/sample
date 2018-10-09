using DevExpress.Xpf.Data;
using System.Windows.Controls;

namespace GridDemo.VirtualSources {
    public partial class InfiniteScrolling1View : UserControl {
        public InfiniteScrolling1View() {
            InitializeComponent();

            #region !
            var source = new InfiniteAsyncSource() {
                ElementType = typeof(IssueData)
            };
            Unloaded += (o, e) => {
                source.Dispose();
            };

            source.FetchRows += (o, e) => {
                IssueSortOrder sortOrder = IssueSortOrder.Default;
                IssueFilter filter = null;

                const int pageSize = 30;
                var issuesTask = IssuesService.GetIssuesAsync(
                    page: e.Skip / pageSize,
                    pageSize: pageSize,
                    sortOrder: sortOrder,
                    filter: filter);

                e.Result = issuesTask.ContinueWith(t => {
                    return new FetchRowsResult(t.Result, hasMoreRows: t.Result.Length == pageSize);
                });
            };

            grid.ItemsSource = source; 
            #endregion
        }
    }
}
