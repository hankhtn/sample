Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Core
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Media

Namespace PropertyGridDemo
    Public Enum BorderOptionsType
        Empty
        Solid
    End Enum
    Public Module BorderOptionsExtensions
        <System.Runtime.CompilerServices.Extension> _
        Public Function CreateBorderOptions(ByVal type As BorderOptionsType, ByVal options As SeriesOptions) As BorderOptions
            Select Case type
                Case BorderOptionsType.Solid
                    Return ViewModelSource.Factory(Of SeriesOptions, SolidBorderOptions)(Function(x) New SolidBorderOptions(x))(options)
            End Select
            Return ViewModelSource.Factory(Of SeriesOptions, BorderOptions)(Function(x) New BorderOptions(x))(options)
        End Function
    End Module
    <MetadataType(GetType(Metadata.BorderOptionsDataMetadata))>
    Public Class BorderOptionsData
        Public Sub New()
            Color = Colors.Transparent
            Opacity = 0R
            Thickness = 0R
            DashStyle = DashStyles.Solid
        End Sub
        Public Property Color() As Color
        Public Property Opacity() As Double
        Public Property Thickness() As Double
        Public Overridable Property DashStyle() As DashStyle
    End Class
    <MetadataType(GetType(Metadata.BorderOptionsMetadata))>
    Public Class BorderOptions
        Inherits BaseOptions

        Public Sub New()
        End Sub
        Public Property BorderType() As BorderOptionsType
        Public Sub New(ByVal root As SeriesOptions)
            MyBase.New(root)
        End Sub
        Public Overridable Property Data() As BorderOptionsData
    End Class
    <MetadataType(GetType(Metadata.SolidBorderOptionsMetadata))>
    Public Class SolidBorderOptions
        Inherits BorderOptions

        Public Sub New()
            Me.New(Nothing)
        End Sub
        Public Sub New(ByVal root As SeriesOptions)
            MyBase.New(root)
            Color = Colors.Black
            Opacity = 1R
            Thickness = 0R
            DashStyle = DashStyles.Solid

        End Sub
        Public Overridable Property Color() As Color
        Public Overridable Property Opacity() As Double
        Public Overridable Property Thickness() As Double
        Public Overridable Property DashStyle() As DashStyle
        Protected Sub OnColorChanged()
            Opacity = CDbl(Color.A) / 255
            UpdateData()
        End Sub
        Protected Sub OnOpacityChanged()
            Color = New Color() With {.A = CByte(Opacity * 255), .R = Color.R, .G = Color.G, .B = Color.B}
            UpdateData()
        End Sub
        Protected Sub OnThicknessChanged()
            UpdateData()
        End Sub
        Protected Sub OnDashStyleChanged()
            UpdateData()
        End Sub

        Protected Sub UpdateData()
            Data = New BorderOptionsData() With {.Color = Me.Color, .Opacity = Me.Opacity, .Thickness = Me.Thickness, .DashStyle = Me.DashStyle}
        End Sub
    End Class
End Namespace
