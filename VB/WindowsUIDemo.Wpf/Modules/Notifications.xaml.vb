Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.XtraRichEdit
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Windows
Imports System.Windows.Media

Namespace WindowsUIDemo
    <CodeFile("ViewModels/NotificationsViewModel.(cs)")>
    Partial Public Class Notifications
        Inherits WindowsUIDemoModule

        Public Sub New()
            InitializeComponent()
#If CLICKONCE Then
            useWin8Notifications.IsChecked = False
            useWin8Notifications.IsEnabled = False
#End If
        End Sub

        Public Shared ReadOnly Property ApplicationID() As String
            Get
                Return String.Format("Components_{0}_Demo_Center_{0}", AssemblyInfo.VersionShort.Replace(".", "_"))
            End Get
        End Property

        Private ReadOnly Property ViewModel() As NotificationsViewModel
            Get
                Return CType(DataContext, NotificationsViewModel)
            End Get
        End Property

        Private Function PatchBackground(ByVal stream As MemoryStream, ByVal color As Color) As MemoryStream
            Dim doc As String = Encoding.UTF8.GetString(stream.ToArray())
            Dim rx As New Regex("color=""#(.*?)""")
            Dim matches = rx.Matches(doc)
            If matches.Count > 0 Then
                Dim strColor As String = matches(0).Groups(1).ToString()
                doc = doc.Replace(strColor, color.ToString().Substring(3))
            End If
            Return New MemoryStream(Encoding.UTF8.GetBytes(doc))
        End Function
    End Class
End Namespace
