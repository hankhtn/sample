Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Reflection
Imports System.Windows.Media.Imaging
Imports System.Xml.Serialization
Imports DevExpress.Utils
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.Grid

Namespace TreeListDemo



    Partial Public Class DynamicNodeLoading
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
            Helper = New FileSystemHelper()
            InitDrives()
        End Sub

        Private Property Helper() As FileSystemDataProviderBase

        Public Sub InitDrives()
            treeList.BeginDataUpdate()
            Try
                Dim root() As String = Helper.GetLogicalDrives()

                For Each s As String In root
                    Dim node As New TreeListNode() With {.Content = New FileSystemItem(s, "Drive", "<Drive>", s), .Image = FileSystemImages.DiskImage}
                    view.Nodes.Add(node)
                    node.IsExpandButtonVisible = DefaultBoolean.True
                    node.Tag = False
                Next s
            Catch
            End Try
            treeList.EndDataUpdate()
        End Sub
        Private Sub InitFolder(ByVal treeListNode As TreeListNode)
            treeList.BeginDataUpdate()
            InitFolders(treeListNode)
            InitFiles(treeListNode)
            treeList.EndDataUpdate()
        End Sub

        Private Sub InitFiles(ByVal treeListNode As TreeListNode)
            Dim item As FileSystemItem = TryCast(treeListNode.Content, FileSystemItem)
            If item Is Nothing Then
                Return
            End If
            Dim node As TreeListNode
            Try
                Dim root() As String = Helper.GetFiles(item.FullName)
                For Each s As String In root
                    node = New TreeListNode() With {.Content = New FileSystemItem(Helper.GetFileName(s), "File", Helper.GetFileSize(s).ToString(), s), .Image = FileSystemImages.FileImage}
                    node.IsExpandButtonVisible = DefaultBoolean.False
                    treeListNode.Nodes.Add(node)
                Next s
            Catch
            End Try
        End Sub

        Private Sub InitFolders(ByVal treeListNode As TreeListNode)
            Dim item As FileSystemItem = TryCast(treeListNode.Content, FileSystemItem)
            If item Is Nothing Then
                Return
            End If

            Try
                Dim root() As String = Helper.GetDirectories(item.FullName)
                For Each s As String In root
                    Try
                        Dim node As New TreeListNode() With {.Content = New FileSystemItem(Helper.GetDirectoryName(s), "Folder", "<Folder>", s), .Image = FileSystemImages.ClosedFolderImage}
                        treeListNode.Nodes.Add(node)
                        node.IsExpandButtonVisible = If(HasFiles(s), DefaultBoolean.True, DefaultBoolean.False)
                    Catch
                    End Try
                Next s
            Catch
                treeListNode.IsExpandButtonVisible = DefaultBoolean.False
            End Try
        End Sub

        Private Function HasFiles(ByVal path As String) As Boolean
            Dim root() As String = Helper.GetFiles(path)
            If root.Length > 0 Then
                Return True
            End If
            root = Helper.GetDirectories(path)
            If root.Length > 0 Then
                Return True
            End If
            Return False
        End Function

        Private Sub treeListView_NodeExpanding(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.TreeList.TreeListNodeAllowEventArgs)
            Dim node As TreeListNode = e.Node
            If NodeIsFolder(node) Then
                node.Image = FileSystemImages.OpenedFolderImage
            End If
            If node.Tag Is Nothing OrElse CBool(node.Tag) = False Then
                InitFolder(node)
                node.Tag = True
            End If
        End Sub

        Private Sub view_NodeCollapsing(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.TreeList.TreeListNodeAllowEventArgs)
            Dim node As TreeListNode = e.Node
            If NodeIsFolder(node) Then
                node.Image = FileSystemImages.ClosedFolderImage
            End If
        End Sub

        Private Function NodeIsFolder(ByVal node As TreeListNode) As Boolean
            Return (TryCast(node.Content, FileSystemItem)).ItemType = "Folder"
        End Function
    End Class

    Public MustInherit Class FileSystemDataProviderBase
        Public MustOverride Function GetLogicalDrives() As String()
        Public MustOverride Function GetDirectories(ByVal path As String) As String()
        Public MustOverride Function GetFiles(ByVal path As String) As String()
        Public MustOverride Function GetDirectoryName(ByVal path As String) As String
        Public MustOverride Function GetFileName(ByVal path As String) As String
        Public MustOverride Function GetFileSize(ByVal path As String) As String
        Friend Function GetFileSize(ByVal size As Long) As String
            If size >= 1024 Then
                Return String.Format("{0:### ### ###} KB", size \ 1024)
            End If
            Return String.Format("{0} Bytes", size)
        End Function
    End Class

    Public Class FileSystemHelper
        Inherits FileSystemDataProviderBase

            Public Overrides Function GetLogicalDrives() As String()
                Return Directory.GetLogicalDrives()
            End Function

            Public Overrides Function GetDirectories(ByVal path As String) As String()
                Return Directory.GetDirectories(path)
            End Function

            Public Overrides Function GetFiles(ByVal path As String) As String()
                Return Directory.GetFiles(path)
            End Function

            Public Overrides Function GetDirectoryName(ByVal path As String) As String
                Return (New DirectoryInfo(path)).Name
            End Function

            Public Overrides Function GetFileName(ByVal path As String) As String
                Return (New FileInfo(path)).Name
            End Function

            Public Overrides Function GetFileSize(ByVal path As String) As String
                Dim size As Long = (New FileInfo(path)).Length
                Return GetFileSize(size)
            End Function
    End Class

    Public Class FileSystemItem
        Public Sub New(ByVal name As String, ByVal type As String, ByVal size As String, ByVal fullName As String)
            Me.Name = name
            Me.ItemType = type
            Me.Size = size
            Me.FullName = fullName
        End Sub
        Public Property Name() As String
        Public Property ItemType() As String
        Public Property Size() As String
        Public Property FullName() As String
    End Class
        Public Class FileSystemImages

        Private Shared fileImage_Renamed As BitmapImage
        Public Shared ReadOnly Property FileImage() As BitmapImage
            Get
                If fileImage_Renamed Is Nothing Then
                    fileImage_Renamed = LoadImage("File")
                End If
                Return fileImage_Renamed
            End Get
        End Property

        Private Shared diskImage_Renamed As BitmapImage
        Public Shared ReadOnly Property DiskImage() As BitmapImage
            Get
                If diskImage_Renamed Is Nothing Then
                    diskImage_Renamed = LoadImage("Local_Disk")
                End If
                Return diskImage_Renamed
            End Get
        End Property

        Private Shared closedFolderImage_Renamed As BitmapImage
        Public Shared ReadOnly Property ClosedFolderImage() As BitmapImage
            Get
                If closedFolderImage_Renamed Is Nothing Then
                    closedFolderImage_Renamed = LoadImage("Folder_Closed")
                End If
                Return closedFolderImage_Renamed
            End Get
        End Property

        Private Shared openedFolderImage_Renamed As BitmapImage
        Public Shared ReadOnly Property OpenedFolderImage() As BitmapImage
            Get
                If openedFolderImage_Renamed Is Nothing Then
                    openedFolderImage_Renamed = LoadImage("Folder_Opened")
                End If
                Return openedFolderImage_Renamed
            End Get
        End Property
        Private Shared Function LoadImage(ByVal imageName As String) As BitmapImage
            Return New BitmapImage(New Uri("/TreeListDemo;component/Images/" & imageName & ".png", UriKind.Relative))
        End Function
        End Class
End Namespace
