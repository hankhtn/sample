Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.IO

Namespace EditorsDemo.ModuleResources
    Public Class CardWinExplorerFileSource
        Implements INotifyPropertyChanged


        Private fileName_Renamed As String

        Private fullPath_Renamed As String

        Private type_Renamed As TypeElement

        Private icon_Renamed() As Byte

        Private size_Renamed As Integer

        Private Shared folder() As Byte
        Private Shared cache As New Dictionary(Of String, Byte())()

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Property FileName() As String
            Get
                Return fileName_Renamed
            End Get
            Set(ByVal value As String)
                fileName_Renamed = value
                If PropertyChangedEvent IsNot Nothing Then
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FileName"))
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FileNameFirst"))
                End If
            End Set
        End Property
        Public ReadOnly Property FileNameFirst() As Char
            Get
                Return Char.ToUpper(FileName.Chars(0))
            End Get
        End Property
        Public Property Icon() As Byte()
            Get
                Return icon_Renamed
            End Get
            Set(ByVal value As Byte())
                icon_Renamed = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Icon"))
            End Set
        End Property
        Public Property Size() As Integer
            Get
                Return size_Renamed
            End Get
            Set(ByVal value As Integer)
                If size_Renamed <> value Then
                    size_Renamed = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Size"))
                End If
            End Set
        End Property
        Public Property FullPath() As String
            Get
                Return fullPath_Renamed
            End Get
            Set(ByVal value As String)
                fullPath_Renamed = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FullPath"))
            End Set
        End Property
        Public Property Type() As TypeElement
            Get
                Return type_Renamed
            End Get
            Set(ByVal value As TypeElement)
                type_Renamed = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Type"))
            End Set
        End Property

        Public Enum TypeElement
            Folder
            File
            Drive
        End Enum

        Public Sub New(ByVal fullPath As String, ByVal type As TypeElement, ByVal sizeType As CardWinExplorerDataSource.SizeIcon, ByVal size As Integer, Optional ByVal setIcon As Boolean = True)
            Me.Type = type
            Me.FullPath = fullPath
            Me.SetIcon(type, fullPath, sizeType, size, setIcon)
        End Sub

        Public Sub Resize(ByVal sizeType As CardWinExplorerDataSource.SizeIcon, ByVal sizeInt As Integer)
            SetIcon(Type, FullPath, sizeType, sizeInt)
        End Sub

        Private Sub SetIcon(ByVal type As TypeElement, ByVal fullPath As String, ByVal sizeType As CardWinExplorerDataSource.SizeIcon, ByVal sizeInt As Integer, Optional ByVal setIcon As Boolean = True)
            Size = sizeInt

            Dim size_Renamed As New Size(sizeInt, sizeInt)
            Select Case type
                Case TypeElement.Folder
                    FileName = Path.GetFileName(fullPath)
                    If folder Is Nothing AndAlso setIcon Then
                        folder = CardWinExplorerIconManager.GetLargeIconByte(fullPath, False)
                    End If
                    Icon = folder
                Case TypeElement.File
                    FileName = Path.GetFileName(fullPath)
                    Dim ext As String = System.IO.Path.GetExtension(fullPath)
                    If setIcon Then
                        If ext = ".exe" OrElse ext = ".lnk" Then
                            Icon = CardWinExplorerIconManager.GetLargeIconByte(fullPath, True, sizeType)
                        ElseIf Not cache.ContainsKey(ext) Then
                            Dim bi() As Byte = CardWinExplorerIconManager.GetLargeIconByte(fullPath, True, sizeType)
                            cache.Add(ext, bi)
                            Icon = bi
                        Else
                            Icon = cache(ext)
                        End If
                    End If
                Case TypeElement.Drive
                    FileName = fullPath
                    If setIcon Then
                        Icon = CardWinExplorerIconManager.GetLargeIconByte(fullPath, False)
                    End If
                Case Else
            End Select
        End Sub

        Public Shared Sub ClearCache()
            cache.Clear()
            folder = Nothing
        End Sub
    End Class
End Namespace
