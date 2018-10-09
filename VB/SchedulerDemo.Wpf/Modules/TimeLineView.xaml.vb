Imports System
Imports System.Windows
Imports DevExpress.XtraScheduler

Namespace SchedulerDemo



    Partial Public Class TimeLineView
        Inherits SchedulerDemoModule

        Public Shared ReadOnly YearScaleProperty As DependencyProperty
        Public Shared ReadOnly QuarterScaleProperty As DependencyProperty
        Public Shared ReadOnly MonthScaleProperty As DependencyProperty
        Public Shared ReadOnly WeekScaleProperty As DependencyProperty
        Public Shared ReadOnly DayScaleProperty As DependencyProperty
        Public Shared ReadOnly HourScaleProperty As DependencyProperty
        Public Shared ReadOnly Min15ScaleProperty As DependencyProperty


        Shared Sub New()
            YearScaleProperty = DependencyProperty.Register("YearScale", GetType(TimeScale), GetType(TimeLineView), New PropertyMetadata(Nothing))
            QuarterScaleProperty = DependencyProperty.Register("QuarterScale", GetType(TimeScale), GetType(TimeLineView), New PropertyMetadata(Nothing))
            MonthScaleProperty = DependencyProperty.Register("MonthScale", GetType(TimeScale), GetType(TimeLineView), New PropertyMetadata(Nothing))
            WeekScaleProperty = DependencyProperty.Register("WeekScale", GetType(TimeScale), GetType(TimeLineView), New PropertyMetadata(Nothing))
            DayScaleProperty = DependencyProperty.Register("DayScale", GetType(TimeScale), GetType(TimeLineView), New PropertyMetadata(Nothing))
            HourScaleProperty = DependencyProperty.Register("HourScale", GetType(TimeScale), GetType(TimeLineView), New PropertyMetadata(Nothing))
            Min15ScaleProperty = DependencyProperty.Register("Min15Scale", GetType(TimeScale), GetType(TimeLineView), New PropertyMetadata(Nothing))

        End Sub
        Public Sub New()
            InitializeComponent()
            OptionsDataContext = Me
            FillTimeScaleList()
            InitializeScheduler()
            Me.scheduler.TimelineView.TimeIndicatorDisplayOptions.Visibility = TimeIndicatorVisibility.CurrentDate
        End Sub

        Public Property YearScale() As TimeScale
            Get
                Return CType(GetValue(YearScaleProperty), TimeScale)
            End Get
            Set(ByVal value As TimeScale)
                SetValue(YearScaleProperty, value)
            End Set
        End Property
        Public Property QuarterScale() As TimeScale
            Get
                Return CType(GetValue(QuarterScaleProperty), TimeScale)
            End Get
            Set(ByVal value As TimeScale)
                SetValue(QuarterScaleProperty, value)
            End Set
        End Property
        Public Property MonthScale() As TimeScale
            Get
                Return CType(GetValue(MonthScaleProperty), TimeScale)
            End Get
            Set(ByVal value As TimeScale)
                SetValue(MonthScaleProperty, value)
            End Set
        End Property
        Public Property WeekScale() As TimeScale
            Get
                Return CType(GetValue(WeekScaleProperty), TimeScale)
            End Get
            Set(ByVal value As TimeScale)
                SetValue(WeekScaleProperty, value)
            End Set
        End Property
        Public Property DayScale() As TimeScale
            Get
                Return CType(GetValue(DayScaleProperty), TimeScale)
            End Get
            Set(ByVal value As TimeScale)
                SetValue(DayScaleProperty, value)
            End Set
        End Property
        Public Property HourScale() As TimeScale
            Get
                Return CType(GetValue(HourScaleProperty), TimeScale)
            End Get
            Set(ByVal value As TimeScale)
                SetValue(HourScaleProperty, value)
            End Set
        End Property
        Public Property Min15Scale() As TimeScale
            Get
                Return CType(GetValue(Min15ScaleProperty), TimeScale)
            End Get
            Set(ByVal value As TimeScale)
                SetValue(Min15ScaleProperty, value)
            End Set
        End Property
        Public Property GroupType() As SchedulerGroupType
            Get
                Return SchedulerControl.GroupType
            End Get
            Set(ByVal value As SchedulerGroupType)
                SchedulerControl.GroupType = value
            End Set
        End Property

        Private Sub FillTimeScaleList()
            Dim scales As TimeScaleCollection = scheduler.TimelineView.Scales
            YearScale = scales(0)
            QuarterScale = scales(1)
            MonthScale = scales(2)
            WeekScale = scales(3)
            DayScale = scales(4)
            HourScale = scales(5)
            Min15Scale = scales(6)
        End Sub
    End Class
End Namespace
