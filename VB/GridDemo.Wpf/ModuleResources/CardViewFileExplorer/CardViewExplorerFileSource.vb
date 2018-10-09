Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports GridDemo.ModuleResources
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO

Namespace GridDemo
    Public Class CardViewExplorerFileSource
        Private Shared ReadOnly factory As Func(Of String, TypeElement, SizeIcon, Integer, CardViewExplorerFileSource) = ViewModelSource.Factory(Function(fullPath As String, type As TypeElement, sizeType As SizeIcon, size As Integer) New CardViewExplorerFileSource(fullPath, type, sizeType, size))
        Public Shared Function Create(ByVal fullPath As String, ByVal type As TypeElement, ByVal sizeType As SizeIcon, ByVal size As Integer) As CardViewExplorerFileSource
            Return factory(fullPath, type, sizeType, size)
        End Function

        Private Shared folder() As Byte
        Private Shared cache As New Dictionary(Of String, Byte())()

        <DependsOnProperties("FileName")>
        Public ReadOnly Property FileNameFirst() As Char
            Get
                Return Char.ToUpper(FileName.Chars(0))
            End Get
        End Property
        Public Overridable Property FileName() As String
        Public Overridable Property Icon() As Byte()
        Public Overridable Property Size() As Integer
        Private privateFullPath As String
        Public Property FullPath() As String
            Get
                Return privateFullPath
            End Get
            Private Set(ByVal value As String)
                privateFullPath = value
            End Set
        End Property
        Private privateType As TypeElement
        Public Property Type() As TypeElement
            Get
                Return privateType
            End Get
            Private Set(ByVal value As TypeElement)
                privateType = value
            End Set
        End Property

        Public Enum TypeElement
            Folder
            File
            Drive
        End Enum

        Protected Sub New(ByVal fullPath As String, ByVal type As TypeElement, ByVal sizeType As SizeIcon, ByVal size As Integer)
            Me.Type = type
            Me.FullPath = fullPath
            SetIcon(sizeType, size)
        End Sub

        Public Sub Resize(ByVal sizeType As SizeIcon, ByVal sizeInt As Integer)
            SetIcon(sizeType, sizeInt)
        End Sub

        Private Sub SetIcon(ByVal sizeType As SizeIcon, ByVal sizeInt As Integer)
            Size = sizeInt

            Dim size_Renamed As New Size(sizeInt, sizeInt)
            Select Case Type
                Case TypeElement.Folder
                    FileName = Path.GetFileName(FullPath)
                    If folder Is Nothing Then
                        folder = CardViewExplorerIconManager.GetLargeIconByte(FullPath, False)
                    End If
                    Icon = folder
                Case TypeElement.File
                    FileName = Path.GetFileName(FullPath)
                    Dim ext As String = System.IO.Path.GetExtension(FullPath)
                    If ext = ".exe" OrElse ext = ".lnk" Then
                        Icon = CardViewExplorerIconManager.GetLargeIconByte(FullPath, True, sizeType)
                    ElseIf Not cache.ContainsKey(ext) Then
                            Dim bi() As Byte = CardViewExplorerIconManager.GetLargeIconByte(FullPath, True, sizeType)
                            cache.Add(ext, bi)
                            Icon = bi
                          Else
                            Icon = cache(ext)
                          End If
                Case TypeElement.Drive
                    FileName = FullPath
                    Icon = CardViewExplorerIconManager.GetLargeIconByte(FullPath, False)
                Case Else
            End Select
        End Sub

        Public Shared Sub ClearCache()
            cache.Clear()
            folder = Nothing
        End Sub
    End Class
End Namespace
