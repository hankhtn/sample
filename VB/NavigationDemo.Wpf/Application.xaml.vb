Imports System
Imports System.Linq
Imports System.Windows
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase

Namespace NavigationDemo
    Partial Public Class App
        Inherits Application

        Shared Sub New()
            ApplicationThemeHelper.ApplicationThemeName = (If(DemoBaseControl.DefaultTheme, Theme.Office2016ColorfulSE)).Name
        End Sub
#If DEBUG Then
        Public ReadOnly Property IsDebug() As Boolean
            Get
                Return True
            End Get
        End Property
#End If
    End Class
End Namespace
