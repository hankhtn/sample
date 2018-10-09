Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.IO

Namespace TreeListDemo
    Public NotInheritable Class DataHelper
        Inherits FileSystemHelper

        Private Shared ReadOnly instanceCore As New DataHelper()
        Private Sub New()
        End Sub
        Public Shared ReadOnly Property Instance() As DataHelper
            Get
                Return instanceCore
            End Get
        End Property
        Public Function GetFileNumSize(ByVal path As String) As Long
            Return (New FileInfo(path)).Length
        End Function
        Public Function GetFolderSize(ByVal fullName As String) As Long
            Dim info As New DirectoryInfo(fullName)
            Return GetFolderSize(info)
        End Function
        Private Function GetFolderSize(ByVal d As DirectoryInfo) As Long
            Dim Size As Long = 0
            Dim fis() As FileInfo = { }
            Try
                fis = d.GetFiles()
            Catch
            End Try
            If fis.Length <> 0 Then
                For Each fi As FileInfo In fis
                    Size += fi.Length
                Next fi
            End If
            Dim dis() As DirectoryInfo = { }
            Try
                dis = d.GetDirectories()
            Catch
            End Try
            If dis.Length <> 0 Then
                For Each di As DirectoryInfo In dis
                    Size += GetFolderSize(di)
                Next di
            End If
            Return Size
        End Function
    End Class

    Public Class FileSystemItemSize
        Private Const kb As Integer = 1024
        Private Const mb As Integer = 1048576

        Public Const Folder As String = "<Folder>"
        Public Const Drive As String = "<Drive>"
        Public Const Calculating As String = "Calculating"

        Private privateDisplaySize As String
        Public Property DisplaySize() As String
            Get
                Return privateDisplaySize
            End Get
            Private Set(ByVal value As String)
                privateDisplaySize = value
            End Set
        End Property
        Private privateNumSize As Long
        Public Property NumSize() As Long
            Get
                Return privateNumSize
            End Get
            Private Set(ByVal value As Long)
                privateNumSize = value
            End Set
        End Property

        Public Event SizeChanged As EventHandler(Of ItemSizeChangedEventArgs)
        Private Sub OnSizeChanged()
            RaiseEvent SizeChanged(Me, New ItemSizeChangedEventArgs(Me))
        End Sub

        Public Sub Change(ByVal size As Long)
            NumSize = size
            DisplaySize = FileSizeToString(size)
            OnSizeChanged()
        End Sub
        Public Sub Change(ByVal displaySize As String)
            Me.DisplaySize = displaySize
            OnSizeChanged()
        End Sub

        Public Sub New(ByVal displaySize As String)
            Change(displaySize)
        End Sub
        Public Sub New(ByVal size As Long)
            Change(size)
        End Sub

        Public Function IsCalculated() As Boolean
            Return (DisplaySize <> FileSystemItemSize.Calculating AndAlso DisplaySize <> FileSystemItemSize.Drive AndAlso DisplaySize <> FileSystemItemSize.Folder)
        End Function

        Private Function FileSizeToString(ByVal size As Long) As String
            If size > mb Then
                Return String.Format("{0:### ### ###} MB", size \ mb)
            ElseIf size > kb Then
                Return String.Format("{0:### ### ###} KB", size \ kb)
            Else
                Return String.Format("{0} Bytes", size)
            End If
        End Function
        Public Overrides Function ToString() As String
            Return DisplaySize
        End Function
    End Class

    Public Class ItemSizeChangedEventArgs
        Inherits EventArgs

        Public Property Size() As FileSystemItemSize
        Public Sub New(ByVal itemSize As FileSystemItemSize)
            Size = itemSize
        End Sub
    End Class
End Namespace
