Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Shell
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Utils
Imports DevExpress.Xpf.Core.Native

Namespace ControlsDemo
    Public Class TaskbarServicesViewModel
        Implements IDisposable

        Public Sub New()
            overlayIcons_Renamed = New NamedIcon() {
                New NamedIcon() With {.Caption = "Moon", .Icon = ImageSourceHelper.GetImageSource(AssemblyHelper.GetResourceUri(GetType(TaskbarServices).Assembly, "Images/Moon.png"))},
                New NamedIcon() With {.Caption = "Sun", .Icon = ImageSourceHelper.GetImageSource(AssemblyHelper.GetResourceUri(GetType(TaskbarServices).Assembly, "Images/Sun.png"))}
            }
            buttonProperties_Renamed = New ObservableCollection(Of Boolean) From {True, True, True, False, True}
            AddHandler buttonProperties_Renamed.CollectionChanged, AddressOf ButtonPropertyChanged
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            TaskbarButtonService.Description = Nothing
            TaskbarButtonService.OverlayIcon = Nothing
            TaskbarButtonService.ProgressState = TaskbarItemProgressState.None
            TaskbarButtonService.ThumbButtonInfos.Clear()
            TaskbarButtonService.ThumbnailClipMarginCallback = Nothing
            TaskbarButtonService.ThumbnailClipMargin = New Thickness()
            ApplicationJumpListService.Items.Clear()
            ApplicationJumpListService.Apply()
        End Sub

        Private ReadOnly overlayIcons_Renamed As IEnumerable(Of NamedIcon)
        Public ReadOnly Property OverlayIcons() As IEnumerable(Of NamedIcon)
            Get
                Return overlayIcons_Renamed
            End Get
        End Property

        Private ReadOnly buttonProperties_Renamed As ObservableCollection(Of Boolean)
        Public ReadOnly Property ButtonProperties() As IEnumerable(Of Boolean)
            Get
                Return buttonProperties_Renamed
            End Get
        End Property

        Public Overridable Property ThumbnailClipMarginMultipliyer() As Double
        Public Overridable Property ThumbButtonsCreate() As Boolean

        Public Overridable Property ThumbnailClipMargin() As Thickness

        <Required>
        Protected Overridable ReadOnly Property TaskbarButtonService() As ITaskbarButtonService
            Get
                Return Nothing
            End Get
        End Property
        <Required>
        Protected Overridable ReadOnly Property ApplicationJumpListService() As IApplicationJumpListService
            Get
                Return Nothing
            End Get
        End Property
        <Required>
        Protected Overridable ReadOnly Property DialogService() As IDialogService
            Get
                Return Nothing
            End Get
        End Property
        <Required>
        Protected Overridable ReadOnly Property MessageBoxService() As IMessageBoxService
            Get
                Return Nothing
            End Get
        End Property

        Protected Overridable Sub OnThumbnailClipMarginMultipliyerChanged()
            TaskbarButtonService.UpdateThumbnailClipMargin()
        End Sub
        Protected Overridable Sub OnThumbButtonsCreateChanged()
            If ThumbButtonsCreate Then
                TaskbarButtonService.ThumbButtonInfos.Add(New TaskbarThumbButtonInfo With {.Description = "Zoom out", .ImageSource = ImageSourceHelper.GetImageSource(AssemblyHelper.GetResourceUri(GetType(TaskbarServices).Assembly, "/Images/TaskbarScreenshots/ZoomOut_32x32.png")), .Action = Function() DecreaseThumbnailClipMarginMultipliyer()})
                TaskbarButtonService.ThumbButtonInfos.Add(New TaskbarThumbButtonInfo With {.Description = "Zoom in", .ImageSource = ImageSourceHelper.GetImageSource(AssemblyHelper.GetResourceUri(GetType(TaskbarServices).Assembly, "/Images/TaskbarScreenshots/ZoomIn_32x32.png")), .Action = Function() IncreaseThumbnailClipMarginMultipliyer()})
                SetButtonsProperties()
            Else
                TaskbarButtonService.ThumbButtonInfos.Clear()
            End If
        End Sub
        Private Sub ButtonPropertyChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
            SetButtonsProperties()
        End Sub
        Private Sub SetButtonsProperties()
            For Each item As TaskbarThumbButtonInfo In TaskbarButtonService.ThumbButtonInfos
                item.IsEnabled = buttonProperties_Renamed(0)
                item.IsInteractive = buttonProperties_Renamed(1)
                item.IsBackgroundVisible = buttonProperties_Renamed(2)
                item.DismissWhenClicked = buttonProperties_Renamed(3)
                item.Visibility = If(buttonProperties_Renamed(4), Visibility.Visible, Visibility.Collapsed)
            Next item
        End Sub
        Private Function DecreaseThumbnailClipMarginMultipliyer() As Boolean
            ThumbnailClipMarginMultipliyer = If(ThumbnailClipMarginMultipliyer >= 10, (ThumbnailClipMarginMultipliyer - 10), 0)
            TaskbarButtonService.UpdateThumbnailClipMargin()
            Return True
        End Function
        Private Function IncreaseThumbnailClipMarginMultipliyer() As Boolean
            ThumbnailClipMarginMultipliyer = If(ThumbnailClipMarginMultipliyer <= 90, (ThumbnailClipMarginMultipliyer + 10), 100)
            TaskbarButtonService.UpdateThumbnailClipMargin()
            Return True
        End Function
        Private Function GetThumbnailClipMargin(ByVal size As Size) As Thickness
            ThumbnailClipMargin = New Thickness With {.Left = 3.0 * CDbl(ThumbnailClipMarginMultipliyer) * size.Width / (5.0 * 100.0), .Top = 2.0 * CDbl(ThumbnailClipMarginMultipliyer) * size.Height / (5.0 * 100.0), .Right = 2.0 * CDbl(ThumbnailClipMarginMultipliyer) * size.Width / (5.0 * 100.0), .Bottom = 3.0 * CDbl(ThumbnailClipMarginMultipliyer) * size.Height / (5.0 * 100.0)}
            Return ThumbnailClipMargin
        End Function
        Private Sub AddTask(ByVal task As NewJumpTaskWindowViewModel)
            Dim customCategory As String = If(String.IsNullOrEmpty(task.CustomCategory), Nothing, task.CustomCategory)
            ApplicationJumpListService.Items.AddOrReplace(customCategory, task.Title, task.Icon.Icon, task.Description, Function() MessageBoxService.Show(task.MessageText))
            Dim rejectedItems As IEnumerable(Of RejectedApplicationJumpItem) = ApplicationJumpListService.Apply()
            For Each rejectedItem In rejectedItems
                Dim rejectedTask = CType(rejectedItem.JumpItem, ApplicationJumpTaskInfo)
                MessageBoxService.Show(String.Format("Error: {0}", rejectedItem.Reason), rejectedTask.Title, MessageBoxButton.OK, MessageBoxImage.Error)
            Next rejectedItem
        End Sub
        Public Sub OnLoaded()
            ThumbButtonsCreate = True
            ThumbnailClipMarginMultipliyer = 20
            TaskbarButtonService.ThumbnailClipMarginCallback = AddressOf GetThumbnailClipMargin
        End Sub
        Public Sub OpenTaskWindow()
            Dim taskAddition As NewJumpTaskWindowViewModel = ViewModelSource.Create(Function() New NewJumpTaskWindowViewModel())
            Dim errorInfo As IDataErrorInfo = DirectCast(taskAddition, IDataErrorInfo)
            Dim okCommand As New UICommand() With {.Caption = "OK", .IsCancel = False, .IsDefault = True, .Command = New DelegateCommand(Of CancelEventArgs)(Sub(x)
                End Sub, Function(x) String.IsNullOrEmpty(errorInfo("Title")))}
            Dim cancelCommand As New UICommand() With {.Caption = "Cancel", .IsCancel = True, .IsDefault = False}
            If DialogService.ShowDialog(New List(Of UICommand)() From {okCommand, cancelCommand}, "Add Jump List Task", "NewJumpTaskWindow", taskAddition) Is okCommand Then
                AddTask(taskAddition)
            End If
        End Sub
    End Class
    Public Class NamedIcon
        Public Property Caption() As String
        Public Property Icon() As ImageSource
    End Class
End Namespace
