Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.POCO
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace PropertyGridDemo
    Public Enum FillOptionsType
        NoFill
        SolidFill
        PictureFill
    End Enum
    <MetadataType(GetType(Metadata.FillOptionsMetadata))>
    Public Class FillOptions
        Inherits BaseOptions

        Public Sub New()
        End Sub
        Public Sub New(ByVal root As SeriesOptions)
            MyBase.New(root)
        End Sub
        Public Property FillType() As FillOptionsType
        Public Overridable Property Result() As Brush
    End Class
    <MetadataType(GetType(Metadata.SolidFillOptionsMetadata))>
    Public Class SolidFillOptions
        Inherits FillOptions

        Public Sub New()
        End Sub
        Public Sub New(ByVal root As SeriesOptions)
            MyBase.New(root)
        End Sub
        Public Overridable Property Color() As Color
        Public Overridable Property Opacity() As Double
        Protected Sub OnColorChanged()
            Opacity = CDbl(Color.A) / 255
            UpdateBrush()
        End Sub
        Protected Sub OnOpacityChanged()
            Color = New Color() With {.A = CByte(Opacity * 255), .R = Color.R, .G = Color.G, .B = Color.B}
            UpdateBrush()
        End Sub
        Private Sub UpdateBrush()
            Result = New SolidColorBrush(Color)
        End Sub
    End Class
    <MetadataType(GetType(Metadata.PictureFillOptionsMetadata))>
    Public Class PictureFillOptions
        Inherits FillOptions

        Public Sub New()
            Me.New(Nothing)
        End Sub
        Public Sub New(ByVal root As SeriesOptions)
            MyBase.New(root)
        Opacity = 1
        End Sub
        Public Overridable Property Picture() As BitmapImage
        Public Overridable Property Opacity() As Double
        Protected Overridable ReadOnly Property OpenFileDialogService() As IOpenFileDialogService
            Get
                Return Nothing
            End Get
        End Property
        Public Sub SelectPicture()
            If OpenFileDialogService.ShowDialog() Then
                Picture = New BitmapImage(New Uri(OpenFileDialogService.GetFullFileName()))
            End If
        End Sub
        Public Function CanClearPicture() As Boolean
            Return Picture IsNot Nothing
        End Function
        Public Sub ClearPicture()
            Picture = Nothing
        End Sub
        Protected Sub OnPictureChanged()
            UpdateBrush()
        End Sub
        Protected Sub OnOpacityChanged()
            UpdateBrush()
        End Sub
        Private Sub UpdateBrush()
            Result = Picture.With(Function(x) New ImageBrush(x) With {.Opacity = Opacity})
        End Sub
    End Class
    Public Module FillOptionsExtensions
        <System.Runtime.CompilerServices.Extension> _
        Public Function CreateFillOptions(ByVal type As FillOptionsType, ByVal options As SeriesOptions) As FillOptions
            Select Case type
                Case FillOptionsType.SolidFill
                    Return ViewModelSource.Factory(Of SeriesOptions, SolidFillOptions)(Function(x) New SolidFillOptions(x))(options)
                Case FillOptionsType.PictureFill
                    Return ViewModelSource.Factory(Of SeriesOptions, PictureFillOptions)(Function(x) New PictureFillOptions(x))(options)
            End Select
            Return ViewModelSource.Factory(Of SeriesOptions, FillOptions)(Function(x) New FillOptions(x))(options)
        End Function
    End Module
End Namespace
