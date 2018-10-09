Imports System
Imports System.Windows
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports DevExpress.Xpf.DemoBase.Helpers.TextColorizer
Imports DevExpress.Xpf.Printing
Imports DevExpress.Xpf.DemoBase

Namespace PrintingDemo.Tests
    Public Class PrintingCheckZoomComboBoxFixture
        Inherits CheckAllDemosFixture





        Protected Overrides Function AllowSwitchToTheTheme(ByVal moduleType As Type, ByVal theme As Theme) As Boolean
            Return False
        End Function

        Protected Overrides Function AllowCheckCodeFile(ByVal moduleType As Type, ByVal fileLanguage As CodeLanguage) As Boolean
            Return False
        End Function





























        Private Function IsDocumentViewerControl(ByVal element As FrameworkElement) As Boolean
            Return TypeOf element Is DocumentPreviewControl
        End Function
    End Class
End Namespace
