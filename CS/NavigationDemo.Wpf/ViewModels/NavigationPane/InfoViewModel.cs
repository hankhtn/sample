using DevExpress.Mvvm;

namespace NavigationDemo {
    public class InfoViewModel : ISupportParameter {
        public virtual object Parameter { get; set; }

        #region ISupportParameter
        object ISupportParameter.Parameter {
            get { return Parameter; }
            set { Parameter = value; }
        }
        #endregion
    }
}
