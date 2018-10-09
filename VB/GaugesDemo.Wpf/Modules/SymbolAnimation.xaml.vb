Imports System
Imports System.Windows
Imports System.Windows.Threading
Imports System.Windows.Media
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Gauges
Imports DevExpress.Utils
Imports DevExpress.Mvvm

Namespace GaugesDemo
    Partial Public Class SymbolAnimation
        Inherits GaugesDemoModule

        Private dataGenerator As New PlayerDataGenerator()

        Public Sub New()
            InitializeComponent()
            receiverGrid.DataContext = dataGenerator
        End Sub

        Private Sub SrcButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            dataGenerator.ChangeSource()
        End Sub
        Private Sub lbeAnimationDirection_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            Dim isRightToLeft As Boolean = (lbeAnimationDirection.SelectedIndex = 0)
            dataGenerator.ChangeText(isRightToLeft)
        End Sub

        Private Sub FirstButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            dataGenerator.SwitchFirstTrack()
        End Sub
        Private Sub LastButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            dataGenerator.SwitchLastTrack()
        End Sub
        Private Sub NextButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            dataGenerator.SwitchNextTrack()
        End Sub
        Private Sub PreviousButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            dataGenerator.SwitchPreviousTrack()
        End Sub
    End Class
    Public Class PlayerDataGenerator
        Inherits BindableBase

        Public Property PlayerText() As String
            Get
                Return GetProperty(Function() PlayerText)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() PlayerText, value)
            End Set
        End Property
        Public Property TimeText() As String
            Get
                Return GetProperty(Function() TimeText)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() TimeText, value)
            End Set
        End Property
        Public Property PlayerAnimation() As SymbolsAnimation
            Get
                Return GetProperty(Function() PlayerAnimation)
            End Get
            Set(ByVal value As SymbolsAnimation)
                SetProperty(Function() PlayerAnimation, value)
            End Set
        End Property

        Private Const animationRefreshTime As Integer = 200
        Private Const rightToLeftRadioText As String = "RADIO            NOW PLAYING        WAAF FM BOSTON        107.3 MHZ"
        Private Const leftToRightRadioText As String = "107.3 MHZ         WAAF FM BOSTON        NOW PLAYING        RADIO"
        Private Const rightToLeftCDSourceInfo As String = "CD          NOW PLAYING              "
        Private Const leftToRightCDSourceInfo As String = "               NOW PLAYING              CD"
        Private Const rightToLeftTrackInfo As String = "          AT 320 KBPS         MP3/WMA"
        Private Const leftToRightTrackInfo As String = "MP3/WMA          AT 320 KBPS         "

        Private Shared creepingAnimationLeftToRight As New CreepingLineAnimation() With {.Direction = CreepingLineDirection.LeftToRight, .RefreshTime = TimeSpan.FromMilliseconds(animationRefreshTime), .Enable = True, .Repeat = True}
        Private Shared creepingAnimationRightToLeft As New CreepingLineAnimation() With {.Direction = CreepingLineDirection.RightToLeft, .RefreshTime = TimeSpan.FromMilliseconds(animationRefreshTime), .Enable = True, .Repeat = True}
        Private Shared blinkingAnimation As New BlinkingAnimation() With {.RefreshTime = TimeSpan.FromMilliseconds(300), .Enable = True}
        Private Shared rightToLeftTracks() As String = { "THE DARK SIDE OF THE MOON       PINK FLOYD", "SMOKE ON THE WATER       DEEP PURPLE", "BLACK MOUNTAIN SIDE       LED ZEPPELIN", "TRANSILVANIA       IRON MAIDEN", "HARD ROAD       BLACK SABBATH" }
        Private Shared leftToRightTracks() As String = { "PINK FLOYD       THE DARK SIDE OF THE MOON", "DEEP PURPLE       SMOKE ON THE WATER", "LED ZEPPELIN       BLACK MOUNTAIN SIDE", "IRON MAIDEN       TRANSILVANIA", "BLACK SABBATH       HARD ROAD" }


        Private isRadioPlaying As Boolean = True
        Private isRightToLeft As Boolean = True
        Private currentTrack As Integer
        Private rightToLeftCDText As String = ""
        Private leftToRightCDText As String = ""
        Private timeTimer As New DispatcherTimer()
        Private blinkingTimer As New DispatcherTimer()

        Public Sub New()
            PlayerText = ""
            TimeText = ""
            PlayerAnimation = creepingAnimationRightToLeft
            PlayerText = rightToLeftRadioText
            AddHandler timeTimer.Tick, AddressOf OnTimedEvent
            AddHandler blinkingTimer.Tick, AddressOf OnBlinkingTimedEvent
            timeTimer.Interval = New TimeSpan(0, 0, 1)
            blinkingTimer.Interval = New TimeSpan(0, 0, 4)
            UpdateTime()
            timeTimer.Start()
        End Sub
        Private Sub OnTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            UpdateTime()
        End Sub
        Private Sub OnBlinkingTimedEvent(ByVal source As Object, ByVal e As EventArgs)
            blinkingTimer.Stop()
            ChangeText(isRightToLeft)
        End Sub
        Private Sub UpdateTime()
            TimeText = String.Format("{0:H:mm}", Date.Now)
        End Sub
        Public Sub ChangeSource()
            isRadioPlaying = Not isRadioPlaying
            If isRadioPlaying Then
                PlayerAnimation = If(isRightToLeft, creepingAnimationRightToLeft, creepingAnimationLeftToRight)
                PlayerText = If(isRightToLeft, rightToLeftRadioText, leftToRightRadioText)
            Else
                PlayerAnimation = blinkingAnimation
                PlayerText = "READING"
                blinkingTimer.Start()
            End If
        End Sub
        Public Sub ChangeText(ByVal isAnimationDirectionRightToLeft As Boolean)
            isRightToLeft = isAnimationDirectionRightToLeft
            If Not blinkingTimer.IsEnabled Then
                PlayerAnimation = If(isRightToLeft, creepingAnimationRightToLeft, creepingAnimationLeftToRight)
                If isRadioPlaying Then
                    PlayerText = If(isRightToLeft, rightToLeftRadioText, leftToRightRadioText)
                Else
                    rightToLeftCDText = rightToLeftCDSourceInfo & rightToLeftTracks(currentTrack) & rightToLeftTrackInfo
                    leftToRightCDText = leftToRightTrackInfo & leftToRightTracks(currentTrack) & leftToRightCDSourceInfo
                    PlayerText = If(isRightToLeft, rightToLeftCDText, leftToRightCDText)
                End If
            End If
        End Sub
        Public Sub SwitchNextTrack()
            If currentTrack < leftToRightTracks.Length - 1 AndAlso (Not isRadioPlaying) Then
                currentTrack += 1
                ChangeText(isRightToLeft)
            End If
        End Sub
        Public Sub SwitchPreviousTrack()
            If currentTrack > 0 AndAlso (Not isRadioPlaying) Then
                currentTrack -= 1
                ChangeText(isRightToLeft)
            End If
        End Sub
        Public Sub SwitchFirstTrack()
            If currentTrack <> 0 AndAlso (Not isRadioPlaying) Then
                currentTrack = 0
                ChangeText(isRightToLeft)
            End If
        End Sub
        Public Sub SwitchLastTrack()
            If currentTrack <> leftToRightTracks.Length - 1 AndAlso (Not isRadioPlaying) Then
                currentTrack = leftToRightTracks.Length - 1
                ChangeText(isRightToLeft)
            End If
        End Sub
    End Class
End Namespace
