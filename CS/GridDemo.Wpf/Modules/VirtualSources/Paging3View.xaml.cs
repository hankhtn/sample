using DevExpress.Data.Filtering;
using DevExpress.Xpf.Data;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace GridDemo.VirtualSources {
    public partial class Paging3View : UserControl {
        public Paging3View() {
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
                #region sorting
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

                #region !
                IssueFilter filter = MakeIssueFilter(e.Filter);
                #endregion

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

            #region !
            source.GetUniqueValues += (o, e) => {
                if(e.PropertyName == "Priority") {
                    var values = Enum.GetValues(typeof(Priority)).Cast<object>().ToArray();
                    e.Result = System.Threading.Tasks.Task.Factory.StartNew(() => values);
                } else {
                    throw new InvalidOperationException();
                }
            };
            #endregion

            grid.ItemsSource = source;
        }

        #region !
        static IssueFilter MakeIssueFilter(CriteriaOperator filter) {
            return filter.Match(
                binary: (propertyName, value, type) => {
                    if(propertyName == "Votes" && type == BinaryOperatorType.GreaterOrEqual)
                        return new IssueFilter(minVotes: (int)value);

                    if(propertyName == "Priority" && type == BinaryOperatorType.Equal)
                        return new IssueFilter(priority: (Priority)value);

                    if(propertyName == "Created") {
                        if(type == BinaryOperatorType.GreaterOrEqual)
                            return new IssueFilter(createdFrom: (DateTime)value);
                        if(type == BinaryOperatorType.Less)
                            return new IssueFilter(createdTo: (DateTime)value);
                    }

                    throw new InvalidOperationException();
                },
                and: filters => {
                    return new IssueFilter(
                        createdFrom: filters.Select(x => x.CreatedFrom).SingleOrDefault(x => x != null),
                        createdTo: filters.Select(x => x.CreatedTo).SingleOrDefault(x => x != null),
                        minVotes: filters.Select(x => x.MinVotes).SingleOrDefault(x => x != null),
                        priority: filters.Select(x => x.Priority).SingleOrDefault(x => x != null)
                    );
                },
                @null: default(IssueFilter)
            );
        }
        #endregion
    }
}
