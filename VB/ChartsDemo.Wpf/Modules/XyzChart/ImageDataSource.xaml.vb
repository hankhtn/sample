Imports System
Imports System.ComponentModel
Imports System.Windows.Media
Imports DevExpress.DemoData.Utils
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Mvvm

Namespace ChartsDemo
    <CodeFile("Modules/XyzChart/ImageDataSource.xaml"), CodeFile("Modules/XyzChart/ImageDataSource.xaml.(cs)")>
    Partial Public Class ImageDataSource
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            ActualChart = chart
        End Sub
    End Class
    Public Class FillStyleItem
        Inherits BindableBase


        Private title_Renamed As String

        Private fillStyle_Renamed As FillStyleBase

        Public Property Title() As String
            Get
                Return title_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(title_Renamed, value, Function() Title)
            End Set
        End Property
        Public Property FillStyle() As FillStyleBase
            Get
                Return fillStyle_Renamed
            End Get
            Set(ByVal value As FillStyleBase)
                SetProperty(fillStyle_Renamed, value, Function() FillStyle)
            End Set
        End Property
    End Class
    Public Class HeightViewModel
        Inherits BindableBase

        Private privateMapValues As DoubleCollection
        Public Property MapValues() As DoubleCollection
            Get
                Return privateMapValues
            End Get
            Private Set(ByVal value As DoubleCollection)
                privateMapValues = value
            End Set
        End Property
        Private privateMapX As DoubleCollection
        Public Property MapX() As DoubleCollection
            Get
                Return privateMapX
            End Get
            Private Set(ByVal value As DoubleCollection)
                privateMapX = value
            End Set
        End Property
        Private privateMapY As DoubleCollection
        Public Property MapY() As DoubleCollection
            Get
                Return privateMapY
            End Get
            Private Set(ByVal value As DoubleCollection)
                privateMapY = value
            End Set
        End Property
        Public Shared ReadOnly Property HeightMapUri() As Uri
            Get
                Return AssemblyHelper.GetResourceUri(GetType(ImageDataSource).Assembly, "/Data/Heightmap.jpg")
            End Get
        End Property

        Public Sub New()
            GenerateMap(HeightMapUri)
        End Sub

        Private Sub GenerateMap(ByVal uri As Uri)
            Dim ImageData As New ImageData(uri)
            Dim pixels(,) As PixelColor = ImageData.GetPixels()

            Dim countX As Integer = pixels.GetLength(0)
            Dim countY As Integer = pixels.GetLength(1)

            Dim startX As Integer = 0
            Dim startY As Integer = 0
            Dim gridStep As Integer = 100
            Dim minY As Double = -300
            Dim maxY As Double = 3000


            Dim mapX_Renamed As New DoubleCollection(countX)

            Dim mapY_Renamed As New DoubleCollection(countY)
            Dim values As New DoubleCollection(countX * countY)
            Dim fullY As Boolean = False
            For i As Integer = 0 To countX - 1
                mapX_Renamed.Add(startX + i * gridStep)
                For j As Integer = 0 To countY - 1
                    If Not fullY Then
                        mapY_Renamed.Add(startY + j * gridStep)
                    End If
                    Dim value As Double = GetValue(pixels(i, j), minY, maxY)
                    values.Add(value)

                Next j
                fullY = True
            Next i
            MapY = mapY_Renamed
            MapX = mapX_Renamed
            MapValues = values
        End Sub

        Private Function GetValue(ByVal color As PixelColor, ByVal min As Double, ByVal max As Double) As Double
            Dim normalizedValue As Double = CDbl(color.Gray) / 255.0
            Return min + normalizedValue * (max - min)
        End Function
    End Class
End Namespace
