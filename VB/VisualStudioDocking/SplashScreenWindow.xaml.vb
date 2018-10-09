Imports System.Windows.Controls

Namespace VisualStudioDocking
    Partial Public Class SplashScreenWindow
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
            copyrightText.Text = AssemblyInfo.AssemblyCopyright & ControlChars.CrLf & "All rights reserverd"
        End Sub
    End Class
End Namespace
