using DevExpress.Xpf.Data;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace GridDemo.VirtualSources {
    public partial class InfiniteScrolling2View : UserControl {
        public InfiniteScrolling2View() {
            InitializeComponent();

            #region create and dispose
            var source = new InfiniteAsyncSource() {
                ElementType = typeof(IssueData)
            };
            Unloaded += (o, e) => {
                source.Dispose();
            }; 
            #endregion

            source.FetchRows += (o, e) => {
                #region !
                IssueSortOrder sortOrder = IssueSortOrder.Default;
                if(e.SortOrder.Length > 0) {
                    var sort = e.SortOrder.Single();
                    if(sort.PropertyName == "Created") {
                        if(sort.Direction != ListSortDirection.Descending)
                            throw new InvalidOperationException();
                        sortOrder = IssueSortOrder.CreatedDescending;
                    }
                    if(sort.PropertyName == "Votes") {
                        sortOrder = sort.Direction == ListSortDirection.Ascending
                            ? IssueSortOrder.VotesAscending
                            : IssueSortOrder.VotesDescending;
                    }
                }
                #endregion

                IssueFilter filter = null;

                #region service request
                const int pageSize = 30;
                var issuesTask = IssuesService.GetIssuesAsync(
                    page: e.Skip / pageSize,
                    pageSize: pageSize,
                    sortOrder: sortOrder,
                    filter: filter);

                e.Result = issuesTask.ContinueWith(t => {
                    return new FetchRowsResult(t.Result, hasMoreRows: t.Result.Length == pageSize);
                });
                #endregion
            };

            grid.ItemsSource = source;
        }
    }
}
