using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Mvvm.POCO;

namespace DialogsDemo {
    public abstract class BaseHeaderItemModel {
        public virtual bool IsVisible { get; set; }
        public ThemedWindowViewModel BaseModel { get { return this.GetParentViewModel<ThemedWindowViewModel>(); } }

        public string ResourceKey { get; set; }
    }
}