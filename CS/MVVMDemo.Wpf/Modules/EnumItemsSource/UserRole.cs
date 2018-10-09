using DevExpress.Mvvm.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace MVVMDemo.EnumItemsSource{
    public enum UserRole {
        [Image("pack://application:,,,/MVVMDemo;component/Modules/Behaviors/Images/Admin.png")]
        [Display(Name = "Admin", Description = "High level of access")]
        Administrator,

        [Image("pack://application:,,,/MVVMDemo;component/Modules/Behaviors/Images/Moderator.png")]
        [Display(Description = "Average level of access")]
        Moderator,

        [Image("pack://application:,,,/MVVMDemo;component/Modules/Behaviors/Images/User.png")]
        [Display(Description = "Low level of access")]
        User
    }
}
