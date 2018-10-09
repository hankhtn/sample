using DevExpress.Xpf.Data;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace GridDemo.VirtualSources {
    public partial class Paging2View : UserControl {
        public Paging2View() {
            InitializeComponent();

            #region create and dispose
            var source = new PagedAsyncSource() {
                ElementType = typeof(IssueData),
                PageSize = 10,
                PageNavigationMode = PageNavigationMode.Arbitrary
            };
            Unloaded += (o, e) => {
                source.Dispose();
            }; 
            #endregion

            source.FetchPage += (o, e) => {
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
                var issuesTask = IssuesService.GetIssuesAsync(
                    page: e.Skip / e.Take,
                    pageSize: e.Take,
                    sortOrder: sortOrder,
                    filter: filter);

                e.Result = issuesTask.ContinueWith(t => {
                    return new FetchRowsResult(t.Result, hasMoreRows: t.Result.Length == e.Take);
                });
                #endregion
            };

            grid.ItemsSource = source;
        }
    }
}
