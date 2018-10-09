Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Editors
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
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace EditorsDemo
    Partial Public Class SVGImageModule
        Public Sub New()
            MetadataLocator.Default = MetadataLocator.Create().AddMetadata(Of EnumMetadataBuilder)()
            InitializeComponent()
        End Sub

        Private Sub cmbGlyphSizesEditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            editor.EditValue = 1
        End Sub
    End Class
    Public Class EnumMetadataBuilder
        Public Shared Sub BuildMetadata(ByVal builder As EnumMetadataBuilder(Of HorizontalAlignment))
            builder.Member(HorizontalAlignment.Center).ImageUri("pack://application:,,,/Images/Svg/CenterAlignment.svg").EndMember().Member(HorizontalAlignment.Left).ImageUri("pack://application:,,,/Images/Svg/LeftAlignment.svg").EndMember().Member(HorizontalAlignment.Right).ImageUri("pack://application:,,,/Images/Svg/RightAlignment.svg").EndMember().Member(HorizontalAlignment.Stretch).ImageUri("pack://application:,,,/Images/Svg/StretchAlignment.svg").EndMember()
        End Sub
    End Class
End Namespace
