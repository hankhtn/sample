Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows
Imports System.Windows.Interop
Imports System.Windows.Media.Imaging

Namespace EditorsDemo.ModuleResources
    Friend NotInheritable Class CardWinExplorerIconManager

        Private Sub New()
        End Sub


        #Region "CommonWinApi"

        <DllImport("shell32.dll", CharSet := CharSet.Auto)>
        Shared Function SHGetFileInfo(ByVal pszPath As String, ByVal dwFileAttributes As Integer, ByRef psfi As SHFILEINFO, ByVal cbfileInfo As UInteger, ByVal uFlags As SHGFI) As Integer
        End Function

        <DllImport("shell32.dll", EntryPoint := "#727")>
        Shared Function SHGetImageList(ByVal iImageList As Integer, ByRef riid As Guid, ByRef ppv As IImageList) As Integer
        End Function

        <DllImport("user32")>
        Shared Function DestroyIcon(ByVal hIcon As IntPtr) As Integer
        End Function

        Private Const MAX_PATH As Integer = 260
        Private Const MAX_TYPE As Integer = 80

        <StructLayout(LayoutKind.Sequential, CharSet := CharSet.Auto)>
        Public Structure SHFILEINFO
            Public Sub New(ByVal b As Boolean)
                hIcon = IntPtr.Zero
                iIcon = 0
                dwAttributes = 0
                szDisplayName = ""
                szTypeName = ""
            End Sub
            Public hIcon As IntPtr
            Public iIcon As Integer
            Public dwAttributes As UInteger
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst := MAX_PATH)>
            Public szDisplayName As String
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst := MAX_TYPE)>
            Public szTypeName As String
        End Structure

        <Flags>
        Public Enum SHGFI As Integer
            Icon = &H100
            DisplayName = &H200
            TypeName = &H400
            Attributes = &H800
            IconLocation = &H1000
            ExeType = &H2000
            SysIconIndex = &H4000
            LinkOverlay = &H8000
            Selected = &H10000
            Attr_Specified = &H20000
            LargeIcon = &H0
            SmallIcon = &H1
            OpenIcon = &H2
            ShellIconSize = &H4
            PIDL = &H8
            UseFileAttributes = &H10
            AddOverlays = &H20
            OverlayIndex = &H40
        End Enum

        #End Region

        Private Shared dpi As Integer = CInt((GetType(SystemParameters).GetProperty("DpiX", System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Static).GetValue(Nothing, Nothing)))
        Private Shared dpiCoef As Double = CDbl(dpi) / 76
        Private Shared dpiCoef2 As Double = CDbl(dpi) / 96

        Private Shared Function IconSource(ByVal ic As Icon, ByVal size As CardWinExplorerDataSource.SizeIcon, ByVal maybeMinIcon As Boolean) As BitmapSource
            Dim src As Bitmap = GetBitmap(ic, size, maybeMinIcon)
            Dim ic2 As BitmapSource = Imaging.CreateBitmapSourceFromHBitmap(src.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
            ic2.Freeze()
            Return ic2
        End Function

        Private Shared Function IconByte(ByVal ic As Icon, ByVal size As CardWinExplorerDataSource.SizeIcon, ByVal maybeMinIcon As Boolean) As Byte()
            Dim converter As New ImageConverter()
            Return DirectCast(converter.ConvertTo(GetBitmap(ic, size, maybeMinIcon), GetType(Byte())), Byte())
        End Function

        Private Shared Function GetBitmap(ByVal ic As Icon, ByVal size As CardWinExplorerDataSource.SizeIcon, ByVal maybeMinIcon As Boolean) As Bitmap
            Dim src As Bitmap = ic.ToBitmap()
            If maybeMinIcon Then
                Dim sizeReal As Integer = CalcRealSize(src)
                If sizeReal < 200 Then
                    src = src.Clone(New Rectangle(0, 0, CInt((sizeReal * dpiCoef2)), CInt((sizeReal * dpiCoef2))), src.PixelFormat)
                    Select Case size
                        Case CardWinExplorerDataSource.SizeIcon.ExtraLarge
                            src = MakeBorder(New System.Drawing.Size(CInt((256 * dpiCoef)), CInt((256 * dpiCoef))), src, CInt((sizeReal * dpiCoef2)))
                        Case CardWinExplorerDataSource.SizeIcon.Large
                            src = MakeBorder(New System.Drawing.Size(CInt((128 * dpiCoef)), CInt((128 * dpiCoef))), src, CInt((sizeReal * dpiCoef2)))
                        Case CardWinExplorerDataSource.SizeIcon.Medium
                            src = MakeBorder(New System.Drawing.Size(CInt((65 * dpiCoef)), CInt((65 * dpiCoef))), src, CInt((sizeReal * dpiCoef2)))
                    End Select
                End If
            End If
            Return src
        End Function

        Private Shared Function MakeBorder(ByVal itemSize As System.Drawing.Size, ByVal bmpSource As Bitmap, ByVal sizeReal As Integer) As Bitmap
            Using bmpSmall As Bitmap = bmpSource.Clone(New Rectangle(0, 0, sizeReal, sizeReal), bmpSource.PixelFormat)
                Dim result As New Bitmap(itemSize.Width, itemSize.Height)
                Using g As Graphics = Graphics.FromImage(result)
                    g.DrawRectangle(Pens.Transparent, 0, 0, itemSize.Width - 1, itemSize.Height - 1)
                    Dim pt As New System.Drawing.Point(result.Width \ 2 - bmpSmall.Width \ 2, result.Width \ 2 - bmpSmall.Width \ 2)
                    g.DrawImage(bmpSmall, pt)
                End Using
                Return result
            End Using
        End Function

        Private Shared Function CalcRealSize(ByVal bmp As Bitmap) As Integer
            Dim halfW As Integer = bmp.Width \ 2
            Dim halfH As Integer = bmp.Height \ 2
            Dim result As Integer = 0
            For i As Integer = bmp.Width - 1 To 1 Step -1
                If bmp.GetPixel(i, halfH).A <> 0 Then
                    result = i
                    Exit For
                End If
            Next i
            If result <> 0 Then
                For i As Integer = bmp.Height - 1 To halfH + 1 Step -1
                    If bmp.GetPixel(halfW, i).A <> 0 Then
                        result = Math.Max(i, result)
                        Exit For
                    End If
                Next i
            Else
                Dim count As Integer = Math.Min(bmp.Width, bmp.Height)
                For i As Integer = 48 To bmp.Width - 1
                    If bmp.GetPixel(i, i).A = 0 Then
                        Dim newI As Integer = i - 1
                        If bmp.GetPixel(newI, i).A <> 0 Then
                            Continue For
                        End If
                        If bmp.GetPixel(i, newI).A <> 0 Then
                            Continue For
                        End If
                        Return i
                    End If
                Next i
            End If
            Return If(result <> 0, result, Math.Max(bmp.Width, bmp.Height))
        End Function

        Private Shared info As New SHFILEINFO(True)
        Private Shared cbFileInfo As UInteger = CUInt(Marshal.SizeOf(info))
        Private Shared flags As SHGFI = SHGFI.Icon Or SHGFI.LargeIcon
        Private Shared guil As New Guid("192B9D83-50FC-457B-90A0-2B82A8B5DAE1")
        Private Shared flagListImage As Integer = &H1 Or &H20


        Private Shared Function GetFileIcon(ByVal path As String, ByRef hIcon As IntPtr) As Icon
            SHGetFileInfo(path, 256, info, cbFileInfo, flags)
            hIcon = GetJumboIcon(info.iIcon)
            Dim icon As Icon = System.Drawing.Icon.FromHandle(hIcon)
            Return icon
        End Function

        Private Shared Function GetJumboIcon(ByVal iImage As Integer) As IntPtr
            Dim spiml As IImageList = Nothing
            SHGetImageList(&H4, guil, spiml)
            Dim hIcon As IntPtr = IntPtr.Zero
            spiml.GetIcon(iImage, flagListImage, hIcon)
            Return hIcon
        End Function

        Public Shared Function GetLargeIcon(ByVal fileName As String, ByVal maybeSmall As Boolean, Optional ByVal sizeIcon As CardWinExplorerDataSource.SizeIcon = CardWinExplorerDataSource.SizeIcon.ExtraLarge) As BitmapSource
            Dim hIcon As IntPtr = Nothing
            Dim icon As Icon = GetFileIcon(fileName, hIcon)
            Dim bs As BitmapSource = IconSource(icon, sizeIcon, maybeSmall)
            icon.Dispose()
            DestroyIcon(hIcon)
            Return bs
        End Function

        Public Shared Function GetLargeIconByte(ByVal fileName As String, ByVal maybeSmall As Boolean, Optional ByVal sizeIcon As CardWinExplorerDataSource.SizeIcon = CardWinExplorerDataSource.SizeIcon.ExtraLarge) As Byte()
            Dim hIcon As IntPtr = Nothing
            Dim icon As Icon = GetFileIcon(fileName, hIcon)
            Dim byteIcon() As Byte = IconByte(icon, sizeIcon, maybeSmall)
            icon.Dispose()
            DestroyIcon(hIcon)
            Return byteIcon
        End Function
    End Class
End Namespace
