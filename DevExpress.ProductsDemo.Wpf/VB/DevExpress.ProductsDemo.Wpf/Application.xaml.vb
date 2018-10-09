Imports System
Imports System.Windows
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.PdfViewer
Imports DevExpress.Internal

Namespace ProductsDemo
    Partial Public Class App
        Inherits Application

        Public Sub New()
            ExceptionHelper.Initialize()
            DevExpress.Images.ImagesAssemblyLoader.Load()
            PdfViewerLocalizer.Active = New CustomPdfViewerLocalizer()
            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2013Name
        End Sub
        Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
            ServiceContainer.Default.RegisterService(New ApplicationJumpListService())
            MyBase.OnStartup(e)
        End Sub
    End Class
    Public Class CustomPdfViewerLocalizer
        Inherits PdfViewerLocalizer

        Public Overrides Function GetLocalizedString(ByVal id As PdfViewerStringId) As String
            Select Case id
                Case PdfViewerStringId.BarCaption
                    Return "PDF VIEWER"
                Case PdfViewerStringId.BarCommentCaption
                    Return "COMMENT"
                Case PdfViewerStringId.BarFormDataCaption
                    Return "FORM DATA"
                Case Else
                    Return MyBase.GetLocalizedString(id)
            End Select
        End Function
    End Class
End Namespace
#If CLICKONCE Then
Namespace DevExpress.Internal.DemoLauncher
    Public NotInheritable Class ClickOnceEntryPoint

        Private Sub New()
        End Sub

        Public Shared Function Run() As Tuple(Of Action, System.Threading.Tasks.Task(Of Window))
            Dim done = New System.Threading.Tasks.TaskCompletionSource(Of Window)()

            Dim run_Renamed As Action = Sub()
                Dim app = New ProductsDemo.App()
                app.InitializeComponent()
                GetType(Application).GetField("_startupUri", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic).SetValue(app, Nothing)
                AddHandler app.Startup, Sub(s, e)
                    Dim window = New ProductsDemo.MainWindow()
                    window.Show()
                    app.MainWindow = window
                    done.SetResult(window)
                End Sub
                app.Run()
            End Sub
            Return Tuple.Create(run_Renamed, done.Task)
        End Function
    End Class
End Namespace
#End If
