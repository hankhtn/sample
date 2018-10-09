Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Threading
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Threading
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.Grid
Imports System.IO
Imports System.Collections
Imports DevExpress.Data
Imports System.Collections.Concurrent
Imports DevExpress.Mvvm
Imports DevExpress.Data.TreeList

Namespace TreeListDemo
    Partial Public Class NodeChecking
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
            SizeUpdater.Instance.WindowDispatcher = Me.Dispatcher
            AddHandler Loaded, Sub(sender, e)
                If view.Nodes.Count > 0 Then
                    view.Nodes(0).IsExpanded = True
                End If
            End Sub
            AddHandler Unloaded, Sub(sender, e) SizeUpdater.Instance.ClearTasks()
        End Sub
        Private Sub view_NodeChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.TreeList.TreeListNodeChangedEventArgs)
            If e.ChangeType = NodeChangeType.Add Then
                Dim item As FileSystemItemModelBase = TryCast(e.Node.Content, FileSystemItemModelBase)
                If item.ItemType = "File" Then
                    e.Node.IsExpandButtonVisible = DevExpress.Utils.DefaultBoolean.False
                End If
            End If
        End Sub
    End Class

    Public Class FileSystem
        Inherits BindableBase
        Implements IChildNodesSelector

        Private totalSizeCore As FileSystemItemSize
        Public Property TotalSize() As FileSystemItemSize
            Get
                Return totalSizeCore
            End Get
            Set(ByVal value As FileSystemItemSize)
                SetProperty(totalSizeCore, value, Function() TotalSize)
            End Set
        End Property
        Public Sub New()
            Source = New List(Of FileSystemItemModelBase)()
            InitializeSource(Source)
            TotalSize = New FileSystemItemSize(0)
            AddHandler SizeUpdater.Instance.TotalSize.SizeChanged, AddressOf TotalSize_SizeChanged
        End Sub

        Private Sub TotalSize_SizeChanged(ByVal sender As Object, ByVal e As ItemSizeChangedEventArgs)
            TotalSize = New FileSystemItemSize(e.Size.NumSize)
        End Sub
        Private privateSource As List(Of FileSystemItemModelBase)
        Public Property Source() As List(Of FileSystemItemModelBase)
            Get
                Return privateSource
            End Get
            Private Set(ByVal value As List(Of FileSystemItemModelBase))
                privateSource = value
            End Set
        End Property
        Protected Overridable Sub InitializeSource(ByVal source As IList(Of FileSystemItemModelBase))
            Dim driveNames() As String = DataHelper.Instance.GetLogicalDrives()
            For Each driveName As String In driveNames
                source.Add(New LogicalDriveSystemItemModel(driveName))
            Next driveName
        End Sub
        #Region "IChildNodesSelector Members"
        Private Function IChildNodesSelector_SelectChildren(ByVal item As Object) As IEnumerable Implements IChildNodesSelector.SelectChildren
            Dim fileSystemItem As IChildNodesSelector = TryCast(item, IChildNodesSelector)
            If fileSystemItem IsNot Nothing Then
                Return fileSystemItem.SelectChildren(item)
            End If
            Return Nothing
        End Function
        #End Region
    End Class

    Public MustInherit Class FileSystemItemModelBase
        Inherits BindableBase

        Private checkedCore? As Boolean
        Private sizeCore As FileSystemItemSize
        Private affectsTotalSizeCore As Boolean

        Public Sub New(ByVal name As String, ByVal type As String, ByVal size As FileSystemItemSize, ByVal fullName As String, Optional ByVal check? As Boolean = False)
            Me.Name = name
            Me.ItemType = type
            Me.FullName = fullName
            Me.Size = size
            AddHandler Me.Size.SizeChanged, AddressOf SizeChanged
            Checked = check
            UpdateAffectsTotalSize()
        End Sub
        Private Sub SizeChanged(ByVal sender As Object, ByVal e As ItemSizeChangedEventArgs)
            RaisePropertyChanged("Size")
            If Size.DisplaySize <> FileSystemItemSize.Calculating Then
                UpdateAffectsTotalSize()
            End If
        End Sub

        Public Property Name() As String
        Public Property ItemType() As String
        Public Property Size() As FileSystemItemSize
            Get
                Return sizeCore
            End Get
            Private Set(ByVal value As FileSystemItemSize)
                If SetProperty(sizeCore, value, "Size") Then
                    UpdateAffectsTotalSize()
                End If
            End Set
        End Property
        Public Property FullName() As String
        Public Property Checked() As Boolean?
            Get
                Return checkedCore
            End Get
            Set(ByVal value? As Boolean)
                If SetProperty(checkedCore, value, "Checked") Then
                    SizeUpdater.Instance.AddTask(Me)
                    UpdateAffectsTotalSize()
                End If
            End Set
        End Property
        Public Property AffectsTotalSize() As Boolean
            Get
                Return affectsTotalSizeCore
            End Get
            Private Set(ByVal value As Boolean)
                If SetProperty(affectsTotalSizeCore, value, "AffectsTotalSize") Then
                    If value Then
                        SizeUpdater.Instance.IncreaseTotalSize(Me.Size.NumSize)
                    Else
                        SizeUpdater.Instance.DecreaseTotalSize(Me.Size.NumSize)
                    End If
                End If
            End Set
        End Property
        Friend Sub UpdateAffectsTotalSize()

            Dim hasChildren_Renamed As Boolean = HasChildren()
            UpdateAffectsTotalSize(hasChildren_Renamed)
        End Sub
        Friend Sub UpdateAffectsTotalSize(ByVal hasChildren As Boolean)
            AffectsTotalSize = Checked.HasValue AndAlso Checked.Value AndAlso Size.IsCalculated() AndAlso Not hasChildren
        End Sub
        Public MustOverride Function HasChildren() As Boolean
    End Class

    Public Class FileSystemItemModel
        Inherits FileSystemItemModelBase
        Implements IChildNodesSelector

        Public Sub New(ByVal name As String, ByVal type As String, ByVal size As FileSystemItemSize, ByVal fullName As String, ByVal check? As Boolean)
            MyBase.New(name, type, size, fullName, check)
        End Sub
        Public Overrides Function HasChildren() As Boolean
            Return False
        End Function
        Private Function IChildNodesSelector_SelectChildren(ByVal item As Object) As IEnumerable Implements IChildNodesSelector.SelectChildren
            Return Nothing
        End Function
    End Class

    Public Class FolderSystemItemModel
        Inherits FileSystemItemModelBase
        Implements IChildNodesSelector

        Public Sub New(ByVal name As String, ByVal type As String, ByVal size As FileSystemItemSize, ByVal fullName As String, ByVal check? As Boolean)
            MyBase.New(name, type, size, fullName, check)
            Source = New List(Of FileSystemItemModelBase)()
        End Sub
        Private privateSource As List(Of FileSystemItemModelBase)
        Public Property Source() As List(Of FileSystemItemModelBase)
            Get
                Return privateSource
            End Get
            Private Set(ByVal value As List(Of FileSystemItemModelBase))
                privateSource = value
            End Set
        End Property
        Public Overrides Function HasChildren() As Boolean
            Return If(Source Is Nothing, False, Source.Count <> 0)
        End Function
        Private Function IChildNodesSelector_SelectChildren(ByVal item As Object) As IEnumerable Implements IChildNodesSelector.SelectChildren
            InitFolders(Source)
            InitFiles(Source)
            UpdateAffectsTotalSize(Source.Count <> 0)
            Return Source
        End Function
        Private Sub InitFolders(ByVal items As IList(Of FileSystemItemModelBase))
            Try
                Dim directoryNames() As String = DataHelper.Instance.GetDirectories(FullName)
                For Each directoryName As String In directoryNames
                    Try
                        items.Add(New FolderSystemItemModel(GetDirectoryName(directoryName), "Folder", New FileSystemItemSize(FileSystemItemSize.Folder), directoryName, Checked))
                    Catch
                    End Try
                Next directoryName
            Catch
            End Try
        End Sub
        Private Sub InitFiles(ByVal items As IList(Of FileSystemItemModelBase))
            Try
                Dim fileNames() As String = DataHelper.Instance.GetFiles(FullName)
                For Each fileName As String In fileNames
                    items.Add(New FileSystemItemModel(GetFileName(fileName), "File", GetFileSize(fileName), fileName, Checked))
                Next fileName
            Catch
            End Try
        End Sub
        Protected Function GetDirectoryName(ByVal path As String) As String
            Return DataHelper.Instance.GetDirectoryName(path)
        End Function
        Protected Function GetFileName(ByVal path As String) As String
            Return DataHelper.Instance.GetFileName(path)
        End Function
        Public Function GetFileSize(ByVal path As String) As FileSystemItemSize

            Dim size_Renamed As Long = DataHelper.Instance.GetFileNumSize(path)
            Return New FileSystemItemSize(size_Renamed)
        End Function
    End Class

    Public Class LogicalDriveSystemItemModel
        Inherits FolderSystemItemModel

        Public Sub New(ByVal driveName As String)
            MyBase.New(driveName, "Drive", New FileSystemItemSize(FileSystemItemSize.Drive), driveName, False)
        End Sub
    End Class

    Public NotInheritable Class SizeUpdater
        Private Shared ReadOnly instanceCore As New SizeUpdater()
        Private Sub New()
            TotalSize = New FileSystemItemSize(0)
        End Sub
        Public Shared ReadOnly Property Instance() As SizeUpdater
            Get
                Return instanceCore
            End Get
        End Property

        Private privateTotalSize As FileSystemItemSize
        Public Property TotalSize() As FileSystemItemSize
            Get
                Return privateTotalSize
            End Get
            Private Set(ByVal value As FileSystemItemSize)
                privateTotalSize = value
            End Set
        End Property
        Public Sub IncreaseTotalSize(ByVal itemSize As Long)
            TotalSize.Change(TotalSize.NumSize + itemSize)
        End Sub
        Public Sub DecreaseTotalSize(ByVal itemSize As Long)
            TotalSize.Change(TotalSize.NumSize - itemSize)
        End Sub

        Public Property WindowDispatcher() As Dispatcher
        Private calcQueue As New SizeCalculatingQueue()
        Friend Sub AddTask(ByVal item As FileSystemItemModelBase)
            If Not item.Checked.Equals(True) Then
                Return
            End If
            calcQueue.ProcessTask(item)
        End Sub
        Public Sub ClearTasks()
            calcQueue.ClearTasks()
            TotalSize = New FileSystemItemSize(0)
        End Sub
        Friend Sub RecursiveCalculator(ByVal item As FileSystemItemModelBase)
            RecursiveCalculatorHelper(item)
        End Sub
        Private Function RecursiveCalculatorHelper(ByVal item As FileSystemItemModelBase) As Long
            Dim resSize As Long = 0
            Dim op As DispatcherOperation
            Dim sizeCalculatingAction As Action(Of FileSystemItemModelBase) = Sub(i As FileSystemItemModelBase) i.Size.Change(FileSystemItemSize.Calculating)
            Dim sizeCalculatedAction As Action(Of FileSystemItemModelBase) = Sub(i As FileSystemItemModelBase) i.Size.Change(resSize)
            If item.ItemType = "File" Then
                Return item.Size.NumSize
            End If
            Dim folderItem As FolderSystemItemModel = TryCast(item, FolderSystemItemModel)
            If folderItem Is Nothing Then
                Return 0
            End If
            If item.Size.IsCalculated() Then
                Return item.Size.NumSize
            End If
            op = WindowDispatcher.BeginInvoke(DispatcherPriority.Normal, sizeCalculatingAction, item)
            If item.HasChildren() Then
                For Each child As FileSystemItemModelBase In folderItem.Source
                    resSize += RecursiveCalculatorHelper(child)
                Next child
            Else
                resSize = DataHelper.Instance.GetFolderSize(item.FullName)
            End If
            op = WindowDispatcher.BeginInvoke(DispatcherPriority.Normal, sizeCalculatedAction, item)
            Return resSize
        End Function
    End Class

    Public Class SizeCalculatingQueue
        Private waitHandle As EventWaitHandle
        Private calculator As Thread
        Private locker As Object
        Private items As Queue(Of FileSystemItemModelBase)

        Public Sub New()
            waitHandle = New AutoResetEvent(False)
            locker = New Object()
            items = New Queue(Of FileSystemItemModelBase)()
            CreateCalcThread()
        End Sub
        Public Sub ProcessTask(ByVal item As FileSystemItemModelBase)
            SyncLock locker
                items.Enqueue(item)
            End SyncLock
            waitHandle.Set()
        End Sub
        Public Sub ClearTasks()
            SyncLock locker
                items.Clear()
            End SyncLock
            If calculator.IsAlive Then
                calculator.Abort()
            End If
            CreateCalcThread()
        End Sub
        Private Sub CreateCalcThread()
            calculator = New Thread(AddressOf Calc)
            calculator.IsBackground = True
            calculator.Start()
        End Sub
        Private Sub Calc()
            Do
                Dim item As FileSystemItemModelBase = GetItemFromQueue()
                If item IsNot Nothing Then
                    SizeUpdater.Instance.RecursiveCalculator(item)
                Else
                    waitHandle.WaitOne()
                End If
            Loop
        End Sub
        Private Function GetItemFromQueue() As FileSystemItemModelBase
            Dim item As FileSystemItemModelBase = Nothing
            SyncLock locker
                If items.Count <> 0 Then
                    item = items.Dequeue()
                End If
            End SyncLock
            Return item
        End Function
    End Class

    Public Class WaitIndicatorVisibilityConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value.ToString() <> FileSystemItemSize.Calculating Then
                Return Visibility.Visible
            End If
            Return Visibility.Collapsed
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class FileSystemImageSelector
        Inherits TreeListNodeImageSelector

        Public Overrides Function [Select](ByVal rowData As DevExpress.Xpf.Grid.TreeList.TreeListRowData) As ImageSource
            Dim item As FileSystemItemModelBase = TryCast(rowData.Row, FileSystemItemModelBase)
            If item Is Nothing Then
                Return Nothing
            End If
            Return GetImageByFileItemType(item.ItemType, rowData.Node.IsExpanded, item.HasChildren())
        End Function
        Private Function GetImageByFileItemType(ByVal type As String, ByVal isExpanded As Boolean, ByVal hasChildren As Boolean) As BitmapImage
            Dim image As BitmapImage = Nothing
            Select Case type
                Case "File"
                    image = FileSystemImages.FileImage
                Case "Folder"
                    image = If(isExpanded AndAlso hasChildren, FileSystemImages.OpenedFolderImage, FileSystemImages.ClosedFolderImage)
                Case "Drive"
                    image = FileSystemImages.DiskImage
            End Select
            Return image
        End Function
    End Class

End Namespace
