Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Threading
Imports System.Threading.Tasks
Imports ControlsDemo.Helpers
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace ControlsDemo.Wizard.WpfTour
    Public Class WizardViewModel
        Public Sub New()
            WelcomePageViewModel = CreateViewModel(Of WelcomePageViewModel)()
            PlayTunePageViewModel = CreateViewModel(Of PlayTunePageViewModel)()
            ReadEulaPageViewModel = CreateViewModel(Of ReadEulaPageViewModel)()
            ConfirmPageViewModel = CreateViewModel(Of ConfirmPageViewModel)()
            NotSoFastPageViewModel = CreateViewModel(Of NotSoFastPageViewModel)()
            TimeConsumePageViewModel = CreateViewModel(Of TimeConsumePageViewModel)()
            RobotPageViewModel = CreateViewModel(Of RobotPageViewModel)()
            CongratulationsPageViewModel = CreateViewModel(Of CongratulationsPageViewModel)()

            ConfirmPageViewModel.TimeConsumePage = TimeConsumePageViewModel
            ConfirmPageViewModel.NotSoFastPage = NotSoFastPageViewModel

            Items = New ObservableCollection(Of Object) From {WelcomePageViewModel, PlayTunePageViewModel, ReadEulaPageViewModel, ConfirmPageViewModel, NotSoFastPageViewModel, TimeConsumePageViewModel, RobotPageViewModel, CongratulationsPageViewModel}
        End Sub

        Private privateConfirmPageViewModel As ConfirmPageViewModel
        Public Property ConfirmPageViewModel() As ConfirmPageViewModel
            Get
                Return privateConfirmPageViewModel
            End Get
            Private Set(ByVal value As ConfirmPageViewModel)
                privateConfirmPageViewModel = value
            End Set
        End Property
        Private privateCongratulationsPageViewModel As CongratulationsPageViewModel
        Public Property CongratulationsPageViewModel() As CongratulationsPageViewModel
            Get
                Return privateCongratulationsPageViewModel
            End Get
            Private Set(ByVal value As CongratulationsPageViewModel)
                privateCongratulationsPageViewModel = value
            End Set
        End Property
        Private privateItems As ObservableCollection(Of Object)
        Public Property Items() As ObservableCollection(Of Object)
            Get
                Return privateItems
            End Get
            Private Set(ByVal value As ObservableCollection(Of Object))
                privateItems = value
            End Set
        End Property
        Private privateNotSoFastPageViewModel As NotSoFastPageViewModel
        Public Property NotSoFastPageViewModel() As NotSoFastPageViewModel
            Get
                Return privateNotSoFastPageViewModel
            End Get
            Private Set(ByVal value As NotSoFastPageViewModel)
                privateNotSoFastPageViewModel = value
            End Set
        End Property
        Private privatePlayTunePageViewModel As PlayTunePageViewModel
        Public Property PlayTunePageViewModel() As PlayTunePageViewModel
            Get
                Return privatePlayTunePageViewModel
            End Get
            Private Set(ByVal value As PlayTunePageViewModel)
                privatePlayTunePageViewModel = value
            End Set
        End Property
        Private privateRobotPageViewModel As RobotPageViewModel
        Public Property RobotPageViewModel() As RobotPageViewModel
            Get
                Return privateRobotPageViewModel
            End Get
            Private Set(ByVal value As RobotPageViewModel)
                privateRobotPageViewModel = value
            End Set
        End Property
        Private privateReadEulaPageViewModel As ReadEulaPageViewModel
        Public Property ReadEulaPageViewModel() As ReadEulaPageViewModel
            Get
                Return privateReadEulaPageViewModel
            End Get
            Private Set(ByVal value As ReadEulaPageViewModel)
                privateReadEulaPageViewModel = value
            End Set
        End Property
        Private privateTimeConsumePageViewModel As TimeConsumePageViewModel
        Public Property TimeConsumePageViewModel() As TimeConsumePageViewModel
            Get
                Return privateTimeConsumePageViewModel
            End Get
            Private Set(ByVal value As TimeConsumePageViewModel)
                privateTimeConsumePageViewModel = value
            End Set
        End Property
        Private privateWelcomePageViewModel As WelcomePageViewModel
        Public Property WelcomePageViewModel() As WelcomePageViewModel
            Get
                Return privateWelcomePageViewModel
            End Get
            Private Set(ByVal value As WelcomePageViewModel)
                privateWelcomePageViewModel = value
            End Set
        End Property
        Public Overridable Property SelectedItem() As WizardPageBase

        Public Shared Function Create() As WizardViewModel
            Return ViewModelSource.Create(Function() New WizardViewModel())
        End Function
        Public Sub Cancel(ByVal e As CancelEventArgs)
            If SelectedItem IsNot Nothing Then
                SelectedItem.Cancel(e)
            End If
        End Sub

        Private Function CreateViewModel(Of T As {WizardPageBase, New})() As T
            Dim viewModel = ViewModelSource.Create(Function() New T())
            viewModel.SetParentViewModel(Me)
            Return viewModel
        End Function
    End Class
    Public MustInherit Class WizardPageBase
        Protected Sub New()
            CanBack = True
            CanNext = CanBack
            CanCancel = CanNext
            CanFinish = False
            ShowCancel = True
            ShowBack = ShowCancel
            ShowNext = ShowBack
            ShowFinish = False
        End Sub

        Public Overridable Property CanBack() As Boolean
        Public Overridable Property CanCancel() As Boolean
        Public Overridable Property CanFinish() As Boolean
        Public Overridable Property CanNext() As Boolean
        Public Overridable Property Description() As String
        Public Overridable Property GoToPage() As Object
        Public Overridable ReadOnly Property Header() As String
            Get
                Return String.Empty
            End Get
        End Property
        Public Overridable Property ShowBack() As Boolean
        Public Overridable Property ShowCancel() As Boolean
        Public Overridable Property ShowFinish() As Boolean
        Public Overridable Property ShowNext() As Boolean

        Public Sub Cancel(ByVal e As CancelEventArgs)
            OnCancel(e)
        End Sub

        Protected Overridable Sub OnCancel(ByVal e As CancelEventArgs)
            If Me.GetService(Of IMessageBoxService)().ShowMessage("Do you want to exit the Wizard Control Feature Tour?", "Wizard Control", MessageButton.YesNo, MessageIcon.Question) = MessageResult.No Then
                e.Cancel = True
            End If
        End Sub
    End Class
    Public Class WelcomePageViewModel
        Inherits WizardPageBase

        Public Sub New()
            CanBack = False
            ShowBack = False
        End Sub

        Public Overrides ReadOnly Property Header() As String
            Get
                Return String.Empty
            End Get
        End Property
    End Class
    Public Class PlayTunePageViewModel
        Inherits WizardPageBase

        Public Sub New()
            Description = "To make this demo more entertaining, we would like to play a tune for you. Simple choose your favorite track."
        End Sub
        Public Overrides ReadOnly Property Header() As String
            Get
                Return "Step 2 - Play a tune"
            End Get
        End Property
        Public Overridable Property Song() As String

        Public Function CanPlay() As Boolean
            Return Not String.IsNullOrEmpty(Song)
        End Function
        Public Sub Play()
            Dim text As String = "Sorry, but we don't have that song in our library..." & Environment.NewLine
            text &= "But we are agree with you that ""{0}"" is an excellent choice."
            text = String.Format(text, Song)
            Me.GetService(Of IMessageBoxService)().ShowMessage(text, "Wizard Control", MessageButton.OK, MessageIcon.Information)
            Me.GetService(Of IWizardService)().GoForward()
            Messenger.Default.Send(New SongMessage() With {.Song = Song})
        End Sub
    End Class
    Public Class ReadEulaPageViewModel
        Inherits WizardPageBase
        Implements ISupportWizardNextCommand

        Private longTextTimer As Stopwatch

        Public Sub New()
            CanNext = False
            Description = "Before proceeding, we want you to read and understand the following text, which is very long."
        End Sub

        Public ReadOnly Property Eula() As String
            Get
                Return WizardDemoHelper.VeryLongText
            End Get
        End Property
        Public Overrides ReadOnly Property Header() As String
            Get
                Return "Step 3 - The Read Some Very Long Text step"
            End Get
        End Property
        Public Overridable Property IsAgreed() As Boolean
        Private ReadOnly Property ISupportWizardNextCommand_CanGoForward() As Boolean Implements ISupportWizardNextCommand.CanGoForward
            Get
                Return CanNext
            End Get
        End Property

        Public Sub StartTimer()
            longTextTimer = Stopwatch.StartNew()
        End Sub
        Protected Sub OnIsAgreedChanged()
            CanNext = IsAgreed
        End Sub
        Private Sub ISupportWizardNextCommand_OnGoForward(ByVal e As CancelEventArgs) Implements ISupportWizardNextCommand.OnGoForward
            Dim elapsed = CInt((longTextTimer.Elapsed.TotalSeconds))
            If elapsed < 60 Then
                Dim result = Me.GetService(Of IMessageBoxService)().ShowMessage(String.Format("Are you sure that {0} seconds was enough time for you to read all that text?", CInt((longTextTimer.Elapsed.TotalSeconds))), "Wizard Control", MessageButton.YesNo, MessageIcon.Question)
                If result = MessageResult.No Then
                    e.Cancel = True
                End If
            End If
        End Sub
    End Class
    Public Class ConfirmPageViewModel
        Inherits WizardPageBase

        Public Sub New()
            ShowNoSoFastPage = True
            GoToPage = If(ShowNoSoFastPage, NotSoFastPage, TimeConsumePage)
        End Sub

        Public Overrides ReadOnly Property Header() As String
            Get
                Return "Step 4 - Are You Tired Yet?"
            End Get
        End Property
        Public Overridable Property NotSoFastPage() As Object
        Public Overridable Property ShowNoSoFastPage() As Boolean
        Public Overridable Property TimeConsumePage() As Object

        Protected Sub OnShowNoSoFastPageChanged()
            GoToPage = If(ShowNoSoFastPage, NotSoFastPage, TimeConsumePage)
        End Sub
    End Class
    Public Class NotSoFastPageViewModel
        Inherits WizardPageBase

        Public Overrides ReadOnly Property Header() As String
            Get
                Return "Not so Fast!"
            End Get
        End Property
    End Class
    Public Class TimeConsumePageViewModel
        Inherits WizardPageBase

        Public Overrides ReadOnly Property Header() As String
            Get
                Return "Step 5 - Time-consuming Operation"
            End Get
        End Property
        Public Overridable Property IsCompleted() As Boolean
        Public ReadOnly Property MaximumProgress() As Integer
            Get
                Return 100
            End Get
        End Property
        Public ReadOnly Property MinimumProgress() As Integer
            Get
                Return 0
            End Get
        End Property
        Public Overridable Property Progress() As Integer

        Public Sub Clear()
            Progress = 0
            CanBack = False
            CanNext = CanBack
            IsCompleted = CanNext
        End Sub
        Public Function StartProcess() As Task
            Clear()
            Return Task.Factory.StartNew(AddressOf Process)
        End Function
        Private Sub Process()
            Thread.Sleep(TimeSpan.FromSeconds(1))
            Dim command = Me.GetAsyncCommand(Function(x) x.StartProcess())
            For i As Integer = MinimumProgress To MaximumProgress
                If command.IsCancellationRequested Then
                    Exit For
                End If
                Progress = i
                Thread.Sleep(TimeSpan.FromSeconds(0.02))
            Next i
            CanBack = True
            CanNext = CanBack
            IsCompleted = CanNext
        End Sub
    End Class
    Public Class RobotPageViewModel
        Inherits WizardPageBase

        Public Overrides ReadOnly Property Header() As String
            Get
                Return "Step 6 - Are You a Robot?"
            End Get
        End Property
        Public Overridable Property Capture() As String
        Protected Sub OnCaptureChanged()
            Messenger.Default.Send(New IsRobotMessage() With {.IsRobot = (Not Equals(Capture.ToLower(), "devexpress123"))})
        End Sub
    End Class
    Public Class CongratulationsPageViewModel
        Inherits WizardPageBase

        Public Sub New()
            CanFinish = True
            CanCancel = False
            ShowFinish = True
            ShowCancel = False
            ShowNext = ShowCancel
            IsRobot = True
            UpdateDescription()

            Messenger.Default.Register(Of IsRobotMessage)(Me, AddressOf OnIsRobotMessage)
            Messenger.Default.Register(Of SongMessage)(Me, AddressOf OnSongMessage)
        End Sub

        Public Overridable Property IsRobot() As Boolean
        Public Overridable Property Song() As String

        Protected Overrides Sub OnCancel(ByVal e As CancelEventArgs)
        End Sub
        Private Function GetArtistName(ByVal value As String) As String
            If String.IsNullOrEmpty(value) Then
                Return String.Empty
            End If
            Return value.Substring(0, value.IndexOf("-") - 1)
        End Function
        Private Sub OnIsRobotMessage(ByVal obj As IsRobotMessage)
            IsRobot = obj.IsRobot
            UpdateDescription()
        End Sub
        Private Sub OnSongMessage(ByVal obj As SongMessage)
            Song = obj.Song
            UpdateDescription()
        End Sub
        Private Sub UpdateDescription()
            If IsRobot Then
                Description = "We are very sorry, but no robots are allowed to continue this wizard. Please click Finish to exit."
            Else
                Dim artist As String = GetArtistName(Song)
                Description = If(String.IsNullOrEmpty(artist), "Thank you for completing this Wizard Control Feature Tour! We can now conclusively state that you are very patient, a quick reader and definitely not a robot.", String.Format("Thank you for completing this Wizard Control Feature Tour! We can now conclusively state that you are very patient, definitely not a robot, a quick reader, and a fan of {0} just like we are.", artist))
            End If
        End Sub
    End Class
    Public Class IsRobotMessage
        Public Property IsRobot() As Boolean
    End Class
    Public Class SongMessage
        Public Property Song() As String
    End Class
End Namespace
