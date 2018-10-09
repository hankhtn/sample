Imports System
Imports System.Windows
Imports System.Windows.Threading
Imports DevExpress.Utils
Imports DevExpress.Xpf.Gauges

Namespace GaugesDemo
    Partial Public Class CircularScales
        Inherits GaugesDemoModule

        Private timer As New DispatcherTimer()

        Public Sub New()
            InitializeComponent()
            UpdateTime()
            AddHandler timer.Tick, AddressOf OnTimedEvent
            timer.Interval = New TimeSpan(0, 0, 1)
            timer.Start()
            CreateCustomLabels(watchNewYorkScale)
            CreateCustomLabels(watchLondonScale)
            CreateCustomLabels(watchMoscowScale)
        End Sub
        Private Sub OnTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            UpdateTime()
        End Sub
        Private Sub UpdateTime()
            Dim nowUtc As Date = Date.UtcNow
            Dim newYorkTime As Date = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
            Dim londonTime As Date = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"))
            Dim moscowTime As Date = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time"))
            hourIndicatorNewYork.Value = newYorkTime.Hour Mod 12
            hourIndicatorLondon.Value = londonTime.Hour Mod 12
            hourIndicatorMoscow.Value = moscowTime.Hour Mod 12
            minuteIndicatorNewYork.Value = newYorkTime.Minute \ 60.0 * 12.0
            minuteIndicatorLondon.Value = londonTime.Minute \ 60.0 * 12.0
            minuteIndicatorMoscow.Value = moscowTime.Minute \ 60.0 * 12.0
            secondIndicatorNewYork.Value = newYorkTime.Second \ 60.0 * 12.0
            secondIndicatorLondon.Value = londonTime.Second \ 60.0 * 12.0
            secondIndicatorMoscow.Value = moscowTime.Second \ 60.0 * 12.0
        End Sub
        Private Sub UserCustomLabels_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ChangeVisibilityLabelsAndCustomLabels(watchNewYorkScale, userCustomLabelsCheckEdit.IsChecked.Value, (Not userCustomLabelsCheckEdit.IsChecked.Value))
            ChangeVisibilityLabelsAndCustomLabels(watchLondonScale, userCustomLabelsCheckEdit.IsChecked.Value, (Not userCustomLabelsCheckEdit.IsChecked.Value))
            ChangeVisibilityLabelsAndCustomLabels(watchMoscowScale, userCustomLabelsCheckEdit.IsChecked.Value, (Not userCustomLabelsCheckEdit.IsChecked.Value))
        End Sub
        Private Sub ShowLabels_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ChangeVisibilityLabelsAndCustomLabels(watchNewYorkScale, False, False)
            ChangeVisibilityLabelsAndCustomLabels(watchLondonScale, False, False)
            ChangeVisibilityLabelsAndCustomLabels(watchMoscowScale, False, False)
        End Sub
        Private Sub CreateCustomLabels(ByVal scale As ArcScale)
            For i As Integer = 1 To 12
                Dim label As New ScaleCustomLabel() With {.RenderTransformOrigin = New Point(0.5, 0.5)}
                label.Value = i
                label.Offset = scale.LabelOptions.Offset
                label.Content = Utils.ConvertArabicToRoman(i)
                label.Visible = False
                scale.CustomLabels.Add(label)
            Next i
        End Sub
        Private Sub ChangeVisibilityLabelsAndCustomLabels(ByVal scale As ArcScale, ByVal showCustomLabels As Boolean, ByVal showLabels As Boolean)
            For Each label As ScaleCustomLabel In scale.CustomLabels
                label.Visible = showCustomLabels
            Next label
            scale.ShowLabels = If(showLabels, DefaultBoolean.True, DefaultBoolean.False)
        End Sub
    End Class
End Namespace
