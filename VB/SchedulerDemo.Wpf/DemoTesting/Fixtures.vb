Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports DevExpress.Xpf.Scheduler
Imports DevExpress.XtraScheduler
Imports System.Windows
Imports DevExpress.XtraScheduler.Drawing
Imports DevExpress.Xpf.Core.Native

Namespace SchedulerDemo.Tests
    #Region "SchedulerCheckAllDemosFixture"
    Public Class SchedulerCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function CanRunModule(ByVal moduleType As Type) As Boolean
            Dim skipModuleTypes() As Type = { GetType(ImportFromOutlook), GetType(SynchronizeWithOutlook) }
            Return Not skipModuleTypes.Contains(moduleType)
        End Function
        Protected Overrides Function CheckMemoryLeaks(ByVal moduleTyle As Type) As Boolean
            Return True
        End Function
    End Class
    #End Region

    #Region "SchedulerDemoModuleAccessor"
    Public Class SchedulerDemoModuleAccessor
        Inherits DemoModulesAccessor(Of SchedulerDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
        Public ReadOnly Property SchedulerControl() As SchedulerControl
            Get
                Return DemoModule.SchedulerControl
            End Get
        End Property
        Public ReadOnly Property DayViewModule() As SchedulerDemo.DayView
            Get
                Return CType(DemoModule, SchedulerDemo.DayView)
            End Get
        End Property
        Public ReadOnly Property WorkWeekViewModule() As SchedulerDemo.WorkWeekView
            Get
                Return CType(DemoModule, SchedulerDemo.WorkWeekView)
            End Get
        End Property
        Public ReadOnly Property WeekViewModule() As SchedulerDemo.WeekView
            Get
                Return CType(DemoModule, SchedulerDemo.WeekView)
            End Get
        End Property
        Public ReadOnly Property MonthViewModule() As SchedulerDemo.MonthView
            Get
                Return CType(DemoModule, SchedulerDemo.MonthView)
            End Get
        End Property
        Public ReadOnly Property TimeLineViewModule() As SchedulerDemo.TimeLineView
            Get
                Return CType(DemoModule, SchedulerDemo.TimeLineView)
            End Get
        End Property
        Public ReadOnly Property AppointmentStylesModule() As SchedulerDemo.AppointmentStyles
            Get
                Return CType(DemoModule, SchedulerDemo.AppointmentStyles)
            End Get
        End Property
    End Class
    #End Region
    #Region "BaseSchedulerTestingFixture"
    Public MustInherit Class BaseSchedulerTestingFixture
        Inherits BaseDemoTestingFixture

        Private ReadOnly modulesAccessor As SchedulerDemoModuleAccessor
        Public Sub New()
            Me.modulesAccessor = New SchedulerDemoModuleAccessor(Me)
        End Sub
        Public ReadOnly Property SchedulerControl() As SchedulerControl
            Get
                Return Me.modulesAccessor.SchedulerControl
            End Get
        End Property
        Public ReadOnly Property DayViewModule() As SchedulerDemo.DayView
            Get
                Return modulesAccessor.DayViewModule
            End Get
        End Property
        Public ReadOnly Property WorkWeekViewModule() As SchedulerDemo.WorkWeekView
            Get
                Return modulesAccessor.WorkWeekViewModule
            End Get
        End Property
        Public ReadOnly Property WeekViewModule() As SchedulerDemo.WeekView
            Get
                Return modulesAccessor.WeekViewModule
            End Get
        End Property
        Public ReadOnly Property MonthViewModule() As SchedulerDemo.MonthView
            Get
                Return modulesAccessor.MonthViewModule
            End Get
        End Property
        Public ReadOnly Property TimeLineViewModule() As SchedulerDemo.TimeLineView
            Get
                Return modulesAccessor.TimeLineViewModule
            End Get
        End Property
        Public ReadOnly Property AppointmentStylesModule() As SchedulerDemo.AppointmentStyles
            Get
                Return modulesAccessor.AppointmentStylesModule
            End Get
        End Property
    End Class
    #End Region
    #Region "CheckDayViewModuleFixture"
    Public Class CheckDayViewModuleFixture
        Inherits BaseSchedulerTestingFixture

        Protected Overrides Sub CreateActions()
            MyBase.CreateActions()
            AddLoadModuleActions(GetType(DayView))
            Dispatch(AddressOf CheckDemo)
        End Sub
        Private Sub CheckDemo()

            Dim dayViewModule_Renamed As DayView = CType(DemoBaseTesting.CurrentDemoModule, DayView)
            Dim initialDayCount As Integer = 3
            Assert.AreEqual(SchedulerViewType.Day, dayViewModule_Renamed.scheduler.ActiveViewType)
            Assert.AreEqual(initialDayCount, dayViewModule_Renamed.scheduler.DayView.DayCount)

            For i As Integer = initialDayCount + 1 To 10
                dayViewModule_Renamed.spnDayCount.SpinUp()
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(False, dayViewModule_Renamed.spnDayCount.HasValidationError)
                Assert.AreEqual(i, dayViewModule_Renamed.scheduler.DayView.DayCount)
            Next i

            Assert.AreEqual(False, dayViewModule_Renamed.scheduler.DayView.AllDayAreaScrollBarVisible)
            Assert.AreEqual(False, dayViewModule_Renamed.chkShowAllDayAreaScrollBars.IsChecked)
            dayViewModule_Renamed.chkShowAllDayAreaScrollBars.IsChecked = True
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(True, dayViewModule_Renamed.scheduler.DayView.AllDayAreaScrollBarVisible)

            dayViewModule_Renamed.chkShowAllDayAreaScrollBars.IsChecked = False
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(False, dayViewModule_Renamed.scheduler.DayView.AllDayAreaScrollBarVisible)

            Assert.AreEqual(False, dayViewModule_Renamed.scheduler.DayView.ShowWorkTimeOnly)
            Assert.AreEqual(False, dayViewModule_Renamed.chkShowWorkTimeOnly.IsChecked)
            dayViewModule_Renamed.chkShowWorkTimeOnly.IsChecked = True
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(True, dayViewModule_Renamed.scheduler.DayView.ShowWorkTimeOnly)

            dayViewModule_Renamed.chkShowWorkTimeOnly.IsChecked = False
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(False, dayViewModule_Renamed.scheduler.DayView.ShowWorkTimeOnly)

            Assert.AreEqual(True, dayViewModule_Renamed.scheduler.DayView.ShowDayHeaders)
            Assert.AreEqual(True, dayViewModule_Renamed.chkShowDayHeaders.IsChecked)
            dayViewModule_Renamed.chkShowDayHeaders.IsChecked = False
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(False, dayViewModule_Renamed.scheduler.DayView.ShowDayHeaders)

            dayViewModule_Renamed.chkShowDayHeaders.IsChecked = True
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(True, dayViewModule_Renamed.scheduler.DayView.ShowDayHeaders)

            Assert.AreEqual(True, dayViewModule_Renamed.scheduler.DayView.ShowAllDayArea, "dayViewModule.scheduler.DayView.ShowAllDayArea must be true")
            Assert.AreEqual(True, dayViewModule_Renamed.chkShowAllDayArea.IsChecked, "dayViewModule.chkShowAllDayArea.IsChecked must be true")
            dayViewModule_Renamed.chkShowAllDayArea.IsChecked = False
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(False, dayViewModule_Renamed.scheduler.DayView.ShowAllDayArea, "dayViewModule.scheduler.WorkWeekView.ShowAllDayArea must be false")

            dayViewModule_Renamed.chkShowAllDayArea.IsChecked = True
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(True, dayViewModule_Renamed.scheduler.DayView.ShowAllDayArea, "dayViewModule.scheduler.WorkWeekView.ShowAllDayArea must be true")
        End Sub
    End Class
    #End Region
    #Region "CheckWorkWeekViewModuleFixture"
    Public Class CheckWorkWeekViewModuleFixture
        Inherits BaseSchedulerTestingFixture

        Protected Overrides Sub CreateActions()
            MyBase.CreateActions()
            AddLoadModuleActions(GetType(WorkWeekView))
            Dispatch(AddressOf CheckDemo)
        End Sub
        Private Sub CheckDemo()

            Dim workWeekViewModule_Renamed As WorkWeekView = CType(DemoBaseTesting.CurrentDemoModule, WorkWeekView)
            Dim weekDays As WeekDays = WeekDays.Monday Or WeekDays.Tuesday Or WeekDays.Wednesday Or WeekDays.Thursday Or WeekDays.Friday
            Assert.AreEqual(SchedulerViewType.WorkWeek, workWeekViewModule_Renamed.scheduler.ActiveViewType)
            Assert.AreEqual(weekDays, workWeekViewModule_Renamed.scheduler.WorkDays.GetWeekDays())

            workWeekViewModule_Renamed.chkMonday.IsChecked = False
            weekDays = weekDays And Not WeekDays.Monday
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(weekDays, workWeekViewModule_Renamed.scheduler.WorkDays.GetWeekDays())

            workWeekViewModule_Renamed.chkTuesday.IsChecked = False
            weekDays = weekDays And Not WeekDays.Tuesday
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(weekDays, workWeekViewModule_Renamed.scheduler.WorkDays.GetWeekDays())

            workWeekViewModule_Renamed.chkWednesday.IsChecked = False
            weekDays = weekDays And Not WeekDays.Wednesday
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(weekDays, workWeekViewModule_Renamed.scheduler.WorkDays.GetWeekDays())

            workWeekViewModule_Renamed.chkThursday.IsChecked = False
            weekDays = weekDays And Not WeekDays.Thursday
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(weekDays, workWeekViewModule_Renamed.scheduler.WorkDays.GetWeekDays())

            workWeekViewModule_Renamed.chkFriday.IsChecked = False
            weekDays = weekDays And Not WeekDays.Friday
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(weekDays, workWeekViewModule_Renamed.scheduler.WorkDays.GetWeekDays())

            workWeekViewModule_Renamed.chkSaturday.IsChecked = True
            weekDays = weekDays Or WeekDays.Saturday
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(weekDays, workWeekViewModule_Renamed.scheduler.WorkDays.GetWeekDays())

            workWeekViewModule_Renamed.chkSunday.IsChecked = True
            weekDays = weekDays Or WeekDays.Sunday
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(weekDays, workWeekViewModule_Renamed.scheduler.WorkDays.GetWeekDays())

            Assert.AreEqual(False, workWeekViewModule_Renamed.scheduler.WorkWeekView.AllDayAreaScrollBarVisible)
            Assert.AreEqual(False, workWeekViewModule_Renamed.chkShowAllDayAreaScrollBars.IsChecked)
            workWeekViewModule_Renamed.chkShowAllDayAreaScrollBars.IsChecked = True
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(True, workWeekViewModule_Renamed.scheduler.WorkWeekView.AllDayAreaScrollBarVisible)

            workWeekViewModule_Renamed.chkShowAllDayAreaScrollBars.IsChecked = False
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(False, workWeekViewModule_Renamed.scheduler.WorkWeekView.AllDayAreaScrollBarVisible)

            Assert.AreEqual(True, workWeekViewModule_Renamed.scheduler.WorkWeekView.ShowAllDayArea, "workWeekViewModule.scheduler.WorkWeekView.ShowAllDayArea must be true")
            Assert.AreEqual(True, workWeekViewModule_Renamed.chkShowAllDayArea.IsChecked, "workWeekView.chkShowAllDayArea.IsChecked must be true")
            workWeekViewModule_Renamed.chkShowAllDayArea.IsChecked = False
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(False, workWeekViewModule_Renamed.scheduler.WorkWeekView.ShowAllDayArea, "workWeekViewModule.scheduler.WorkWeekView.ShowAllDayArea must be false")

            workWeekViewModule_Renamed.chkShowAllDayArea.IsChecked = True
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(True, workWeekViewModule_Renamed.scheduler.WorkWeekView.ShowAllDayArea, "workWeekViewModule.scheduler.WorkWeekView.ShowAllDayArea must be true")
        End Sub
    End Class
    #End Region
    #Region "CheckWeekViewModuleFixture"
    Public Class CheckWeekViewModuleFixture
        Inherits BaseSchedulerTestingFixture

        Protected Overrides Sub CreateActions()
            MyBase.CreateActions()
            AddLoadModuleActions(GetType(WeekView))
            Dispatch(AddressOf CheckDemo)
        End Sub
        Private Sub CheckDemo()

            Dim weekViewModule_Renamed As WeekView = CType(DemoBaseTesting.CurrentDemoModule, WeekView)
            Assert.AreEqual(SchedulerViewType.Week, weekViewModule_Renamed.scheduler.ActiveViewType)
            Assert.AreEqual(DevExpress.XtraScheduler.FirstDayOfWeek.System, weekViewModule_Renamed.scheduler.OptionsView.FirstDayOfWeek)

            For i As Integer = 0 To 7
                weekViewModule_Renamed.cbFirstDayOfWeek.SelectedItem = CType(i, DevExpress.XtraScheduler.FirstDayOfWeek)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(False, weekViewModule_Renamed.cbFirstDayOfWeek.HasValidationError)
                Assert.AreEqual(CType(i, DevExpress.XtraScheduler.FirstDayOfWeek), weekViewModule_Renamed.scheduler.OptionsView.FirstDayOfWeek)
            Next i

            For i As Integer = 0 To 2
                weekViewModule_Renamed.cbTimeDisplayType.SelectedItem = CType(i, AppointmentTimeDisplayType)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(CType(i, AppointmentTimeDisplayType), weekViewModule_Renamed.scheduler.WeekView.AppointmentDisplayOptions.TimeDisplayType)
            Next i

            For i As Integer = 0 To 2
                weekViewModule_Renamed.cbTimeDisplayType.SelectedItem = CType(i, AppointmentTimeDisplayType)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(CType(i, AppointmentTimeDisplayType), weekViewModule_Renamed.scheduler.WeekView.AppointmentDisplayOptions.TimeDisplayType)
            Next i

            For i As Integer = 0 To 2
                weekViewModule_Renamed.cbStartTimeVisibility.SelectedItem = CType(i, AppointmentTimeVisibility)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(CType(i, AppointmentTimeVisibility), weekViewModule_Renamed.scheduler.WeekView.AppointmentDisplayOptions.StartTimeVisibility)
            Next i

            For i As Integer = 0 To 2
                weekViewModule_Renamed.cbEndTimeVisibility.SelectedItem = CType(i, AppointmentTimeVisibility)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(CType(i, AppointmentTimeVisibility), weekViewModule_Renamed.scheduler.WeekView.AppointmentDisplayOptions.EndTimeVisibility)
            Next i
        End Sub
    End Class
    #End Region
    #Region "CheckMonthViewModuleFixture"
    Public Class CheckMonthViewModuleFixture
        Inherits BaseSchedulerTestingFixture

        Protected Overrides Sub CreateActions()
            MyBase.CreateActions()
            AddLoadModuleActions(GetType(MonthView))
            Dispatch(AddressOf CheckDemo)
        End Sub
        Private Sub CheckDemo()

            Dim monthViewModule_Renamed As MonthView = CType(DemoBaseTesting.CurrentDemoModule, MonthView)
            Dim initialWeekCount As Integer = 5
            Assert.AreEqual(SchedulerViewType.Month, monthViewModule_Renamed.scheduler.ActiveViewType)
            Assert.AreEqual(initialWeekCount, monthViewModule_Renamed.scheduler.MonthView.WeekCount)

            For i As Integer = initialWeekCount + 1 To 10
                monthViewModule_Renamed.spnWeekCount.SpinUp()
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(False, monthViewModule_Renamed.spnWeekCount.HasValidationError)
                Assert.AreEqual(i, monthViewModule_Renamed.scheduler.MonthView.WeekCount)
            Next i

            Assert.AreEqual(DevExpress.XtraScheduler.FirstDayOfWeek.System, monthViewModule_Renamed.scheduler.OptionsView.FirstDayOfWeek)

            For i As Integer = 0 To 7
                monthViewModule_Renamed.cbFirstDayOfWeek.SelectedItem = CType(i, DevExpress.XtraScheduler.FirstDayOfWeek)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(False, monthViewModule_Renamed.cbFirstDayOfWeek.HasValidationError)
                Assert.AreEqual(CType(i, DevExpress.XtraScheduler.FirstDayOfWeek), monthViewModule_Renamed.scheduler.OptionsView.FirstDayOfWeek)
            Next i

            Assert.AreEqual(False, monthViewModule_Renamed.chCompressWeekend.IsChecked)
            Assert.AreEqual(False, monthViewModule_Renamed.scheduler.MonthView.CompressWeekend)
            monthViewModule_Renamed.chCompressWeekend.IsChecked = True
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(True, monthViewModule_Renamed.scheduler.MonthView.CompressWeekend)

            Assert.AreEqual(True, monthViewModule_Renamed.chCompressWeekend.IsChecked)
            Assert.AreEqual(True, monthViewModule_Renamed.scheduler.MonthView.CompressWeekend)
            monthViewModule_Renamed.chCompressWeekend.IsChecked = False
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(False, monthViewModule_Renamed.scheduler.MonthView.CompressWeekend)


            Assert.AreEqual(True, monthViewModule_Renamed.scheduler.MonthView.ShowWeekend)
            Assert.AreEqual(True, monthViewModule_Renamed.chShowWeekend.IsChecked)
            monthViewModule_Renamed.chShowWeekend.IsChecked = False
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(False, monthViewModule_Renamed.scheduler.MonthView.ShowWeekend)

            Assert.AreEqual(False, monthViewModule_Renamed.chShowRecurrence.IsChecked)
            Assert.AreEqual(False, monthViewModule_Renamed.scheduler.MonthView.AppointmentDisplayOptions.ShowRecurrence)
            monthViewModule_Renamed.chShowRecurrence.IsChecked = True
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(True, monthViewModule_Renamed.scheduler.MonthView.AppointmentDisplayOptions.ShowRecurrence)

            For i As Integer = 0 To 2
                monthViewModule_Renamed.cbStatusDisplayType.SelectedItem = CType(i, AppointmentStatusDisplayType)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(CType(i, AppointmentStatusDisplayType), monthViewModule_Renamed.scheduler.MonthView.AppointmentDisplayOptions.StatusDisplayType)
            Next i

            For i As Integer = 0 To 2
                monthViewModule_Renamed.cbTimeDisplayType.SelectedItem = CType(i, AppointmentTimeDisplayType)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(CType(i, AppointmentTimeDisplayType), monthViewModule_Renamed.scheduler.MonthView.AppointmentDisplayOptions.TimeDisplayType)
            Next i

            For i As Integer = 0 To 2
                monthViewModule_Renamed.cbStartTimeVisibility.SelectedItem = CType(i, AppointmentTimeVisibility)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(CType(i, AppointmentTimeVisibility), monthViewModule_Renamed.scheduler.MonthView.AppointmentDisplayOptions.StartTimeVisibility)
            Next i

            For i As Integer = 0 To 2
                monthViewModule_Renamed.cbEndTimeVisibility.SelectedItem = CType(i, AppointmentTimeVisibility)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(CType(i, AppointmentTimeVisibility), monthViewModule_Renamed.scheduler.MonthView.AppointmentDisplayOptions.EndTimeVisibility)
            Next i
        End Sub
    End Class
    #End Region
    #Region "CheckTimeLineViewModuleFixture"
    Public Class CheckTimeLineViewModuleFixture
        Inherits BaseSchedulerTestingFixture

        Protected Overrides Sub CreateActions()
            MyBase.CreateActions()
            AddLoadModuleActions(GetType(TimeLineView))
            Dispatch(AddressOf CheckDemo)
        End Sub
        Private Sub CheckDemo()

            Dim timeLineViewModule_Renamed As TimeLineView = CType(DemoBaseTesting.CurrentDemoModule, TimeLineView)
            Dim initialIntervalCount As Integer = 10
            Dim initialSelectionBarHeight As Integer = 0
            Dim scheduler As SchedulerControl = timeLineViewModule_Renamed.scheduler
            Assert.AreEqual(SchedulerViewType.Timeline, scheduler.ActiveViewType)
            Dim timelineView As DevExpress.Xpf.Scheduler.TimelineView = scheduler.TimelineView
            Assert.AreEqual(initialIntervalCount, timelineView.IntervalCount)
            Dim selectionBar As SchedulerSelectionBarOptions = timelineView.SelectionBar
            Assert.AreEqual(initialSelectionBarHeight, selectionBar.Height)

            For i As Integer = initialIntervalCount - 1 To 1 Step -1
                timeLineViewModule_Renamed.spnIntervalCount.SpinDown()
                If initialIntervalCount \ 2 - 1 < i AndAlso i < initialIntervalCount \ 2 + 1 Then
                    timeLineViewModule_Renamed.chkVisible.IsChecked = False
                Else
                    timeLineViewModule_Renamed.chkVisible.IsChecked = True
                End If
                UpdateLayoutAndDoEvents()
                If selectionBar.Visible Then
                    Assert.AreEqual(i, GetSelectionBarElementCount(scheduler))
                End If
                Assert.AreEqual(False, timeLineViewModule_Renamed.spnIntervalCount.HasValidationError)
                Assert.AreEqual(i, timelineView.IntervalCount)
            Next i

            For i As Integer = 0 To 2
                timeLineViewModule_Renamed.cbSnapToCellsMode.SelectedItem = CType(i, AppointmentSnapToCellsMode)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(CType(i, AppointmentSnapToCellsMode), timelineView.AppointmentDisplayOptions.SnapToCellsMode)
            Next i

            Assert.AreEqual(True, timeLineViewModule_Renamed.chkVisible.IsChecked)

            For i As Integer = 20 To 10
                timeLineViewModule_Renamed.spnSelectionBarHeight.SpinUp()
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(i, selectionBar.Height)
            Next i

            timeLineViewModule_Renamed.chkVisible.IsChecked = False
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(False, selectionBar.Visible)

            Assert.AreEqual(False, timeLineViewModule_Renamed.chYearScaleEnabled.IsChecked)
            Assert.AreEqual(False, timelineView.Scales(0).Enabled)
            timeLineViewModule_Renamed.chYearScaleEnabled.IsChecked = True
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(True, timelineView.Scales(0).Enabled)

            Assert.AreEqual(True, timeLineViewModule_Renamed.chYearScaleVisible.IsChecked)
            Assert.AreEqual(True, timelineView.Scales(0).Visible)
            timeLineViewModule_Renamed.chYearScaleVisible.IsChecked = False
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(False, timelineView.Scales(0).Visible)

            Assert.AreEqual(False, timeLineViewModule_Renamed.chQuarterScaleEnabled.IsChecked)
            Assert.AreEqual(False, timelineView.Scales(1).Enabled)
            timeLineViewModule_Renamed.chQuarterScaleEnabled.IsChecked = True
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(True, timelineView.Scales(1).Enabled)

            Assert.AreEqual(True, timeLineViewModule_Renamed.chQuarterScaleVisible.IsChecked)
            Assert.AreEqual(True, timelineView.Scales(1).Visible)
            timeLineViewModule_Renamed.chQuarterScaleVisible.IsChecked = False
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(False, timelineView.Scales(1).Visible)

            Assert.AreEqual(False, timeLineViewModule_Renamed.chMonthScaleEnabled.IsChecked)
            Assert.AreEqual(False, timelineView.Scales(2).Enabled)
            timeLineViewModule_Renamed.chMonthScaleEnabled.IsChecked = True
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(True, timelineView.Scales(2).Enabled)

            Assert.AreEqual(True, timeLineViewModule_Renamed.chMonthScaleVisible.IsChecked)
            Assert.AreEqual(True, timelineView.Scales(2).Visible)
            timeLineViewModule_Renamed.chMonthScaleVisible.IsChecked = False
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(False, timelineView.Scales(2).Visible)

            Assert.AreEqual(True, timeLineViewModule_Renamed.chWeekScaleEnabled.IsChecked)
            Assert.AreEqual(True, timelineView.Scales(3).Enabled)
            timeLineViewModule_Renamed.chWeekScaleEnabled.IsChecked = False
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(False, timelineView.Scales(3).Enabled)

            Assert.AreEqual(True, timeLineViewModule_Renamed.chWeekScaleVisible.IsChecked)
            Assert.AreEqual(True, timelineView.Scales(3).Visible)
            timeLineViewModule_Renamed.chWeekScaleVisible.IsChecked = False
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(False, timelineView.Scales(3).Visible)

            Assert.AreEqual(True, timeLineViewModule_Renamed.chDayScaleEnabled.IsChecked)
            Assert.AreEqual(True, timelineView.Scales(4).Enabled)
            timeLineViewModule_Renamed.chDayScaleEnabled.IsChecked = False
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(False, timelineView.Scales(4).Enabled)

            Assert.AreEqual(False, timeLineViewModule_Renamed.chHourScaleEnabled.IsChecked)
            Assert.AreEqual(False, timelineView.Scales(5).Enabled)
            timeLineViewModule_Renamed.chHourScaleEnabled.IsChecked = True
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(True, timelineView.Scales(5).Enabled)

            Assert.AreEqual(True, timeLineViewModule_Renamed.chHourScaleVisible.IsChecked)
            Assert.AreEqual(True, timelineView.Scales(5).Visible)
            timeLineViewModule_Renamed.chHourScaleVisible.IsChecked = False
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(False, timelineView.Scales(5).Visible)

            Assert.AreEqual(False, timeLineViewModule_Renamed.chMin15ScaleEnabled.IsChecked)
            Assert.AreEqual(False, timelineView.Scales(6).Enabled)
            timeLineViewModule_Renamed.chMin15ScaleEnabled.IsChecked = True
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(True, timelineView.Scales(6).Enabled)

            Assert.AreEqual(True, timeLineViewModule_Renamed.chMin15ScaleVisible.IsChecked)
            Assert.AreEqual(True, timelineView.Scales(6).Visible)
            timeLineViewModule_Renamed.chMin15ScaleVisible.IsChecked = False
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(False, timelineView.Scales(6).Visible)
        End Sub
        Private Function GetSelectionBarElementCount(ByVal scheduler As SchedulerControl) As Integer
            Dim result As New List(Of FrameworkElement)()
            Dim treeWalker As New VisualTreeEnumerator(scheduler)
            Do While treeWalker.MoveNext()
                Dim fe As FrameworkElement = TryCast(treeWalker.Current, FrameworkElement)
                If fe Is Nothing Then
                    Continue Do
                End If
                If IsRootSelectionBarCell(fe, result) Then
                    result.Add(fe)
                End If
            Loop
            Return result.Count
        End Function
        Private Function IsRootSelectionBarCell(ByVal element As FrameworkElement, ByVal roots As List(Of FrameworkElement)) As Boolean
            Dim hitTest As SchedulerHitTest = SchedulerControl.GetHitTestType(element)
            If hitTest.Equals(SchedulerHitTest.SelectionBarCell) Then
                For Each fe As FrameworkElement In roots
                    If LayoutHelper.IsChildElement(fe, element) Then
                        Return False
                    End If
                Next fe
                Return True
            End If
            Return False
        End Function
    End Class
    #End Region

    #Region "CheckGroupByResourceFixture"
    Public Class CheckGroupByResourceFixture
        Inherits BaseSchedulerTestingFixture

        Protected Overrides Sub CreateActions()
            MyBase.CreateActions()
            AddLoadModuleActions(GetType(GroupByResource))
            Dispatch(AddressOf CheckDemo)
        End Sub
        Private Sub CheckDemo()
            Dim [module] As GroupByResource = CType(DemoBaseTesting.CurrentDemoModule, GroupByResource)

            Assert.AreEqual([module].scheduler.ActiveViewType, SchedulerViewType.Day)
            Assert.AreEqual([module].scheduler.GroupType, SchedulerGroupType.Resource)

            [module].viewType.EditValue = SchedulerViewType.Week
            UpdateLayoutAndDoEvents()
            Assert.AreEqual([module].scheduler.ActiveViewType, SchedulerViewType.Week)
            Assert.AreEqual([module].scheduler.GroupType, SchedulerGroupType.Resource)

            [module].viewType.EditValue = SchedulerViewType.WorkWeek
            UpdateLayoutAndDoEvents()
            Assert.AreEqual([module].scheduler.ActiveViewType, SchedulerViewType.WorkWeek)
            Assert.AreEqual([module].scheduler.GroupType, SchedulerGroupType.Resource)

            [module].viewType.EditValue = SchedulerViewType.Month
            UpdateLayoutAndDoEvents()
            Assert.AreEqual([module].scheduler.ActiveViewType, SchedulerViewType.Month)
            Assert.AreEqual([module].scheduler.GroupType, SchedulerGroupType.Resource)

            [module].viewType.EditValue = SchedulerViewType.Timeline
            UpdateLayoutAndDoEvents()
            Assert.AreEqual([module].scheduler.ActiveViewType, SchedulerViewType.Timeline)
            Assert.AreEqual([module].scheduler.GroupType, SchedulerGroupType.Resource)
        End Sub
    End Class
    #End Region
    #Region "CheckGroupByDateModuleFixture"
    Public Class CheckGroupByDateModuleFixture
        Inherits BaseSchedulerTestingFixture

        Protected Overrides Sub CreateActions()
            MyBase.CreateActions()
            AddLoadModuleActions(GetType(GroupByDate))
            Dispatch(AddressOf CheckDemo)
        End Sub
        Private Sub CheckDemo()
            Dim [module] As GroupByDate = CType(DemoBaseTesting.CurrentDemoModule, GroupByDate)

            Assert.AreEqual([module].scheduler.ActiveViewType, SchedulerViewType.Day)
            Assert.AreEqual([module].scheduler.GroupType, SchedulerGroupType.Date)

            [module].viewType.EditValue = SchedulerViewType.Week
            UpdateLayoutAndDoEvents()
            Assert.AreEqual([module].scheduler.ActiveViewType, SchedulerViewType.Week)
            Assert.AreEqual([module].scheduler.GroupType, SchedulerGroupType.Date)

            [module].viewType.EditValue = SchedulerViewType.WorkWeek
            UpdateLayoutAndDoEvents()
            Assert.AreEqual([module].scheduler.ActiveViewType, SchedulerViewType.WorkWeek)
            Assert.AreEqual([module].scheduler.GroupType, SchedulerGroupType.Date)

            [module].viewType.EditValue = SchedulerViewType.Month
            UpdateLayoutAndDoEvents()
            Assert.AreEqual([module].scheduler.ActiveViewType, SchedulerViewType.Month)
            Assert.AreEqual([module].scheduler.GroupType, SchedulerGroupType.Date)

            [module].viewType.EditValue = SchedulerViewType.Timeline
            UpdateLayoutAndDoEvents()
            Assert.AreEqual([module].scheduler.ActiveViewType, SchedulerViewType.Timeline)
            Assert.AreEqual([module].scheduler.GroupType, SchedulerGroupType.Date)
        End Sub
    End Class
    #End Region
































































































End Namespace
