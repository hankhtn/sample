Imports DevExpress.Xpf.Core
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Imaging
Imports System.Windows.Shapes

Namespace ProductsDemo



    Partial Public Class ProgressWindow
        Inherits Window
        Implements ISplashScreen

        Private ReadOnly Property Storyboard() As Storyboard
            Get
                Return DirectCast(FindResource("storyboard"), Storyboard)
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub ISplashScreen_CloseSplashScreen() Implements ISplashScreen.CloseSplashScreen
            If Storyboard IsNot Nothing Then
                AddHandler Storyboard.Completed, AddressOf OnAnimationCompleted
                Storyboard.Begin()
            End If
        End Sub

        Private Sub ISplashScreen_Progress(ByVal value As Double) Implements ISplashScreen.Progress
        End Sub

        Private Sub ISplashScreen_SetProgressState(ByVal isIndeterminate As Boolean) Implements ISplashScreen.SetProgressState
        End Sub

        Private Sub OnAnimationCompleted(ByVal sender As Object, ByVal e As EventArgs)
            RemoveHandler Storyboard.Completed, AddressOf OnAnimationCompleted
            Me.Close()
        End Sub
    End Class
End Namespace
