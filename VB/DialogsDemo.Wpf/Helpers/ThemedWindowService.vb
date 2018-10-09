Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Core

Namespace DialogsDemo
    Public Interface IThemedWindowService
        Function Show(ByVal dataContext As Object) As Boolean
        Function Hide() As Boolean
        ReadOnly Property IsWindowOpened() As Boolean
    End Interface
    Public Class ThemedWindowService
        Inherits ServiceBase
        Implements IThemedWindowService

        Private themedWindow As ThemedWindow
        Public Property WindowStyle() As Style
        Public Sub New()
        End Sub

        Private Sub OnThemedWindowClosed(ByVal sender As Object, ByVal e As EventArgs)
            IsWindowOpened = False
            RemoveHandler themedWindow.Closed, AddressOf OnThemedWindowClosed
            themedWindow = Nothing
        End Sub

        Public Function Show(ByVal dataContext As Object) As Boolean Implements IThemedWindowService.Show
            If IsWindowOpened Then
                Return False
            End If
            IsWindowOpened = True
            themedWindow = New ThemedWindow()
            AddHandler themedWindow.Closed, AddressOf OnThemedWindowClosed
            themedWindow.DataContext = dataContext
            themedWindow.Style = WindowStyle
            themedWindow.Show()
            Return True
        End Function

        Public Function Hide() As Boolean Implements IThemedWindowService.Hide
            If Not IsWindowOpened Then
                Return False
            End If
            themedWindow.Close()
            Return True
        End Function

        Private ReadOnly Property IThemedWindowService_IsWindowOpened() As Boolean Implements IThemedWindowService.IsWindowOpened
            Get
                Return IsWindowOpened
            End Get
        End Property
        Private privateIsWindowOpened As Boolean
        Public Property IsWindowOpened() As Boolean
            Get
                Return privateIsWindowOpened
            End Get
            Private Set(ByVal value As Boolean)
                privateIsWindowOpened = value
            End Set
        End Property
    End Class
End Namespace
