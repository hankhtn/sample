Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Interop
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Bars.Native
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase

Namespace EditorsDemo
    Friend Class PinnedWindowBehaviorNativeMethods
        <DllImport("user32.dll", EntryPoint := "GetWindowLong")>
        Shared Function GetWindowLong32(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer
        End Function

        <DllImport("user32.dll", EntryPoint := "GetWindowLongPtr")>
        Shared Function GetWindowLongPtr64(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As IntPtr
        End Function

        Public Shared Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Long
            If IntPtr.Size = 4 Then
                Return GetWindowLong32(hWnd, nIndex)
            End If
            Return GetWindowLongPtr64(hWnd, nIndex).ToInt64()
        End Function

        Public Shared Function SetWindowLongPtr(ByVal hWnd As HandleRef, ByVal nIndex As Integer, ByVal dwNewLong As Long) As IntPtr
            If IntPtr.Size = 8 Then
                Return SetWindowLongPtr64(hWnd, nIndex, New IntPtr(dwNewLong))
            End If
            Return New IntPtr(SetWindowLong32(hWnd, nIndex, CInt(dwNewLong)))
        End Function

        <DllImport("user32.dll", EntryPoint := "SetWindowLong")>
        Shared Function SetWindowLong32(ByVal hWnd As HandleRef, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
        End Function

        <DllImport("user32.dll", EntryPoint := "SetWindowLongPtr")>
        Shared Function SetWindowLongPtr64(ByVal hWnd As HandleRef, ByVal nIndex As Integer, ByVal dwNewLong As IntPtr) As IntPtr
        End Function

        <DllImport("user32.dll", SetLastError := True)>
        Public Shared Function SetParent(ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As IntPtr
        End Function
    End Class
End Namespace
