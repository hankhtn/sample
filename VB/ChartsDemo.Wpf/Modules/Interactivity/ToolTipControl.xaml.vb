Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports System
Imports System.Linq
Imports System.Windows.Media

Namespace ChartsDemo
    <CodeFile("Modules/Interactivity/ToolTipControl.xaml"), CodeFile("Modules/Interactivity/ToolTipControl.xaml.(cs)"), CodeFile("ViewModels/G7Data.(cs)")>
    Partial Public Class ToolTipControl
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub

        Private Sub ToolTipController_ToolTipOpening(ByVal sender As Object, ByVal e As ChartToolTipEventArgs)
            Dim colorIndex = e.Series.Points.IndexOf(e.SeriesPoint)
            Dim memberColor = e.ChartControl.Palette(colorIndex)
            e.Hint = New G7MemberTooltipData(CType(e.SeriesPoint.Tag, G7Member), New SolidColorBrush(memberColor))
        End Sub
    End Class
    Public Class G7MemberTooltipData
        Private privateMember As G7Member
        Public Property Member() As G7Member
            Get
                Return privateMember
            End Get
            Private Set(ByVal value As G7Member)
                privateMember = value
            End Set
        End Property
        Private privateMemberBrush As SolidColorBrush
        Public Property MemberBrush() As SolidColorBrush
            Get
                Return privateMemberBrush
            End Get
            Private Set(ByVal value As SolidColorBrush)
                privateMemberBrush = value
            End Set
        End Property

        Public Sub New(ByVal member As G7Member, ByVal brush As SolidColorBrush)
            Me.Member = member
            Me.MemberBrush = brush
        End Sub
    End Class
End Namespace
