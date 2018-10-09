Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.UI
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.DemoBase.Helpers
Imports System
Imports System.Threading.Tasks
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace WindowsUIDemo
    Public Class NotificationsViewModel
        Public Overridable Property ShowImage() As Boolean
        Public Overridable Property Template() As NotificationTemplate
        Public Overridable ReadOnly Property NotificationService() As INotificationService
            Get
                Return Nothing
            End Get
        End Property
        Public Overridable ReadOnly Property LogService() As ILogService
            Get
                Return Nothing
            End Get
        End Property

        Private privateAppModelID As String
        Public Property AppModelID() As String
            Get
                Return privateAppModelID
            End Get
            Private Set(ByVal value As String)
                privateAppModelID = value
            End Set
        End Property
        Protected Sub New()
            ShowImage = True
            CustomNotificationWidth = 380
            CustomNotificationHeight = 100
            CustomNotificationBackground = Color.FromRgb(&Hf7, &H94, &H1e)
        End Sub

        Public Sub ShowCustom()
            Dim viewModel As CustomToastViewModel = ViewModelSource.Create(Function() New CustomToastViewModel With {.Width = CustomNotificationWidth, .Height = CustomNotificationHeight, .Text = CustomNotificationText, .Background = CustomNotificationBackgroundBrush})
            Dim notification As INotification = NotificationService.CreateCustomNotification(viewModel)
            Show(notification)
        End Sub

        Public Sub ShowPredefined()
            Dim image As ImageSource = If(ShowImage, New BitmapImage(New Uri("pack://application:,,,/WindowsUIDemo;component/Images/sun.png", UriKind.Absolute)), Nothing)
            Dim text1 As String = "Lorem ipsum dolor sit amet integer fringilla, dui eget ultrices cursus, justo tellus."
            Dim text2 As String = "In ornare ante magna, eget volutpat mi bibendum a. Nam ut ullamcorper libero. Pellentesque habitant."
            Dim text3 As String = "Quisque sapien odio, mollis tincidunt est id, fringilla euismod neque. Aenean adipiscing lorem dui, nec. "
            Dim notification As INotification = NotificationService.CreatePredefinedNotification(text1, text2, text3, image)
            Show(notification)
        End Sub

        Private Sub Show(ByVal notification As INotification)
            LogService.LogLine("Showing...")
            notification.ShowAsync().ContinueWith(AddressOf OnNotificationShown, TaskScheduler.FromCurrentSynchronizationContext())
        End Sub

        Private Sub OnNotificationShown(ByVal task As Task(Of NotificationResult))
            Try
                Select Case task.Result
                    Case NotificationResult.Activated
                        LogService.LogLine("Activated")
                    Case NotificationResult.TimedOut
                        LogService.LogLine("Timed out")
                    Case NotificationResult.UserCanceled
                        LogService.LogLine("Canceled by user")
                    Case NotificationResult.Dropped
                        LogService.LogLine("Dropped (the queue is full)")
                End Select
            Catch e As AggregateException
                LogService.LogLine("Error: " & e.InnerException.Message)
            End Try
        End Sub

        Protected Sub OnCustomNotificationBackgroundChanged()
            CustomNotificationBackgroundBrush = New SolidColorBrush With {.Color = CustomNotificationBackground}
        End Sub

        Public Overridable Property CustomNotificationText() As String
        Public Overridable Property CustomNotificationWidth() As Double
        Public Overridable Property CustomNotificationHeight() As Double
        Public Overridable Property CustomNotificationBackground() As Color
        Public Overridable Property CustomNotificationBackgroundBrush() As SolidColorBrush
    End Class

    <POCOViewModel>
    Public Class CustomToastViewModel
        Public Overridable Property Text() As String
        Public Overridable Property Height() As Double
        Public Overridable Property Width() As Double
        Public Overridable Property Background() As SolidColorBrush
    End Class
End Namespace
