Imports DevExpress.Utils
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Namespace RibbonDemo
    Public Class GlyphHelper
        Public Shared Function GetGlyph(ByVal ItemPath As String) As ImageSource
            Return New BitmapImage(AssemblyHelper.GetResourceUri(GetType(MVVMRibbon).Assembly, ItemPath))
        End Function
    End Class
End Namespace
