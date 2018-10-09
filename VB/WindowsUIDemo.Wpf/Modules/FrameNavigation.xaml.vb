Imports DevExpress.Xpf.DemoBase
Imports System.Windows.Data
Imports System.Windows.Media.Animation
Namespace WindowsUIDemo
    <CodeFile("Modules/Views/NavigatorView.xaml"), CodeFile("Modules/Views/DashboardView.xaml"), CodeFile("Modules/Views/LoanCalculatorView.xaml"), CodeFile("Modules/Views/MortgageRatesView.xaml"), CodeFile("Modules/Views/ResearchView.xaml"), CodeFile("Modules/Views/StatisticsView.xaml"), CodeFile("Modules/Views/SystemInformationView.xaml"), CodeFile("Modules/Views/UserManagementView.xaml"), CodeFile("Modules/Views/ZillowView.xaml")>
    Partial Public Class FrameNavigation
        Inherits WindowsUIDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class FrameAnimationSelector
        Inherits DevExpress.Xpf.WindowsUI.AnimationSelector

        Private _BackStoryboard As Storyboard
        Public Property ForwardStoryboard() As Storyboard
        Public Property BackStoryboard() As Storyboard
            Get
                Return _BackStoryboard
            End Get
            Set(ByVal value As Storyboard)
                _BackStoryboard = value
            End Set
        End Property
        Protected Overrides Function SelectStoryboard(ByVal animation As DevExpress.Xpf.WindowsUI.FrameAnimation) As Storyboard
            Return If(animation.Direction = DevExpress.Xpf.WindowsUI.AnimationDirection.Forward, ForwardStoryboard, BackStoryboard)
        End Function
    End Class
End Namespace
