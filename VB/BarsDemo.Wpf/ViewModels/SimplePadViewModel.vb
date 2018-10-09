Imports DevExpress.Mvvm
Imports System
Imports System.Collections.Generic
Namespace BarsDemo
    Public Class SimplePadViewModel
        #Region "properties"
        Private privateFontSizes As IEnumerable(Of Double?)
        Public Property FontSizes() As IEnumerable(Of Double?)
            Get
                Return privateFontSizes
            End Get
            Protected Set(ByVal value As IEnumerable(Of Double?))
                privateFontSizes = value
            End Set
        End Property
        #End Region
        #Region "services"
        Public Overridable ReadOnly Property ShowAboutDialogService() As IShowAboutDialogService
            Get
                Return Nothing
            End Get
        End Property
        #End Region
        #Region "commands"
        Public Sub ShowAboutDialog()
            ShowAboutDialogService.Show()
        End Sub
        Public Sub NavigateToHomePage()

            System.Diagnostics.Process.Start("http://www.devexpress.com")
        End Sub
        #End Region
        Public Sub New()
            FontSizes = New Double?() {Nothing, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 44, 48, 52, 56, 60, 64, 68, 72, 76, 80, 88, 96, 104, 112, 120, 128, 136, 144 }
        End Sub
    End Class
End Namespace
