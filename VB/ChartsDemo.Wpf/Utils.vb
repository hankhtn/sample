Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Resources
Imports System.Windows.Threading
Imports System.Xml.Linq
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Public NotInheritable Class DataLoader

        Private Sub New()
        End Sub

        Public Shared Function LoadXmlFromResources(ByVal fileName As String) As XDocument
            Try
                Return XDocument.Load(LoadFromResources(fileName))
            Catch
                Return Nothing
            End Try
        End Function
        Public Shared Function LoadFromResources(ByVal fileName As String) As Stream
            Try
                Dim uri As New Uri("/ChartsDemo;component" & fileName, UriKind.RelativeOrAbsolute)
                Return Application.GetResourceStream(uri).Stream
            Catch
                Return Nothing
            End Try
        End Function
    End Class


    Public Class FinancialPoint
        Public Shared Function Create(ByVal argument As Integer, ByVal lowValue As Double, ByVal highValue As Double, ByVal openValue As Double, ByVal closeValue As Double) As FinancialPoint
            Return New FinancialPoint With {.IntArgument = argument, .LowValue = lowValue, .HighValue = highValue, .OpenValue = openValue, .CloseValue = closeValue}
        End Function

        Public Property IntArgument() As Integer
        Public Property Argument() As String
        Public Property DateTimeArgument() As Date
        Public Property HighValue() As Double
        Public Property LowValue() As Double
        Public Property OpenValue() As Double
        Public Property CloseValue() As Double
    End Class

    Public Structure RangeDataPoint

        Private x_Renamed As Double

        Private y1_Renamed As Double

        Private y2_Renamed As Double

        Public ReadOnly Property X() As Double
            Get
                Return x_Renamed
            End Get
        End Property
        Public ReadOnly Property Y1() As Double
            Get
                Return y1_Renamed
            End Get
        End Property
        Public ReadOnly Property Y2() As Double
            Get
                Return y2_Renamed
            End Get
        End Property

        Public Sub New(ByVal x As Double, ByVal y1 As Double, ByVal y2 As Double)
            Me.x_Renamed = x
            Me.y1_Renamed = y1
            Me.y2_Renamed = y2
        End Sub
    End Structure


    Public NotInheritable Class PolarFunctionsPointGenerator

        Private Sub New()
        End Sub

        Public Shared Function CreateLemniskateData() As List(Of RangeDataPoint)
            Dim list As New List(Of RangeDataPoint)()
            For x As Double = 0 To 359 Step 5
                Dim xRadian As Double = DegreeToRadian(x)
                Dim cos As Double = Math.Cos(2 * xRadian)
                Dim y As Double = Math.Pow(Math.Abs(cos), 2)
                list.Add(New RangeDataPoint(x, y, 1))
            Next x
            Return list
        End Function

        Public Shared Function CreateCardioidData() As List(Of RangeDataPoint)
            Dim list As New List(Of RangeDataPoint)()
            Const a As Double = 0.5
            For x As Double = 0 To 359 Step 15
                Dim y As Double = 2 * a * Math.Cos(DegreeToRadian(x))
                list.Add(New RangeDataPoint(x, y, 1))
            Next x
            Return list
        End Function

        Public Shared Function CreateTaubinsHeartData() As List(Of RangeDataPoint)
            Dim list As New List(Of RangeDataPoint)()
            For x As Double = 0 To 359 Step 15
                Dim xRadian As Double = DegreeToRadian(x)
                Dim y As Double = 2 - 0.5 * Math.Sin(xRadian) + Math.Sin(xRadian) * Math.Sqrt(Math.Abs(Math.Cos(xRadian))) / (Math.Sin(xRadian) + 1.4)
                list.Add(New RangeDataPoint(x, y, 1))
            Next x
            Return list
        End Function

        Private Shared Function DegreeToRadian(ByVal degree As Double) As Double
            Return 2 * Math.PI / 360 * degree
        End Function
    End Class

    Public NotInheritable Class CartesianFunctionsPointGenerator

        Private Sub New()
        End Sub
        Private Const a As Integer = 10

        Public Shared Function CreateArchimedianSpiralPoints() As List(Of Point)
            Dim points = New List(Of Point)()
            For i As Integer = 0 To 719 Step 15
                Dim t As Double = CDbl(i) / 180 * Math.PI
                Dim x As Double = t * Math.Cos(t)
                Dim y As Double = t * Math.Sin(t)
                points.Add(New Point(x, y))
            Next i
            Return points
        End Function
        Public Shared Function CreateCardioidPoints() As List(Of Point)
            Dim points = New List(Of Point)()
            For i As Integer = 0 To 359 Step 10
                Dim t As Double = CDbl(i) / 180 * Math.PI
                Dim x As Double = a * (2 * Math.Cos(t) - Math.Cos(2 * t))
                Dim y As Double = a * (2 * Math.Sin(t) - Math.Sin(2 * t))
                points.Add(New Point(x, y))
            Next i
            Return points
        End Function
        Public Shared Function CreateCartesianFoliumPoints() As List(Of Point)
            Dim points = New List(Of Point)()
            For i As Integer = -30 To 124 Step 5
                Dim t As Double = Math.Tan(CDbl(i) / 180 * Math.PI)
                Dim x As Double = 3 * CDbl(a) * t / (t * t * t + 1)
                Dim y As Double = x * t
                points.Add(New Point(x, y))
            Next i
            Return points
        End Function
    End Class


    Public MustInherit Class ScatterFunctionCalculatorBase
        Private Const spiralIntervalsCount As Integer = 72
        Private Const roseIntervalsCount As Integer = 288
        Private Const foliumSegmentIntervalsCount As Integer = 30

        Private Const roseParameter As Double = 7.0R / 4.0R
        Private Const foliumDistanceLimit As Double = 3.0

        Protected MustOverride Function NormalizeAngle(ByVal angle As Double) As Double
        Protected MustOverride Function ToRadian(ByVal angle As Double) As Double
        Protected MustOverride Function FromDegrees(ByVal angle As Double) As Double

        Private Function FilterPointsByRange(ByVal points As List(Of Point), ByVal min As Double, ByVal max As Double) As List(Of Point)
            Dim resultPoints As New List(Of Point)()
            For Each point As Point In points
                Dim pointValue As Double = point.Y
                If pointValue <= max AndAlso pointValue >= min Then
                    resultPoints.Add(point)
                End If
            Next point
            Return resultPoints
        End Function
        Private Function CreatePolarFunctionPoints(ByVal minAngleDegree As Double, ByVal maxAngleDegree As Double, ByVal intervalsCount As Integer, ByVal [function] As Func(Of Double, Double)) As List(Of Point)
            Dim points = New List(Of Point)()
            Dim minAngle As Double = FromDegrees(minAngleDegree)
            Dim maxAngle As Double = FromDegrees(maxAngleDegree)
            Dim angleStep As Double = (maxAngle - minAngle) / CDbl(intervalsCount)
            For pointIndex As Integer = 0 To intervalsCount
                Dim angle As Double = minAngle + pointIndex * angleStep
                Dim angleRadians As Double = ToRadian(angle)
                Dim distance As Double = [function](angleRadians)
                Dim normalAngle As Double = NormalizeAngle(angle)
                points.Add(New Point(normalAngle, distance))
            Next pointIndex
            Return points
        End Function
        Private Function ArchimedeanSpiralFunction(ByVal angleRadians As Double) As Double
            Return angleRadians
        End Function
        Private Function PolarRoseFunction(ByVal angleRadians As Double) As Double
            Return Math.Max(0.0, Math.Sin(roseParameter * angleRadians))
        End Function
        Private Function PolarFoliumFunction(ByVal angleRadians As Double) As Double
            Dim sin As Double = Math.Sin(angleRadians)
            Dim cos As Double = Math.Cos(angleRadians)
            Return (3.0 * sin * cos) / (Math.Pow(sin, 3.0) + Math.Pow(cos, 3.0))
        End Function

        Public Function CreateArchimedeanSpiralData() As List(Of Point)
            Return CreatePolarFunctionPoints(0.0, 720.0, spiralIntervalsCount, AddressOf ArchimedeanSpiralFunction)
        End Function
        Public Function CreateRoseData() As List(Of Point)
            Return CreatePolarFunctionPoints(0.0, 1440.0, roseIntervalsCount, AddressOf PolarRoseFunction)
        End Function
        Public Function CreateFoliumData() As List(Of Point)
            Dim points1 = CreatePolarFunctionPoints(120.0, 180.0, foliumSegmentIntervalsCount, AddressOf PolarFoliumFunction)
            Dim points2 = CreatePolarFunctionPoints(0.0, 90.0, foliumSegmentIntervalsCount, AddressOf PolarFoliumFunction)
            Dim points3 = CreatePolarFunctionPoints(270.0, 330.0, foliumSegmentIntervalsCount, AddressOf PolarFoliumFunction)
            Return FilterPointsByRange(points1.Concat(points2).Concat(points3).ToList(), 0.0, foliumDistanceLimit)
        End Function
    End Class


    Public Class RadianScatterFunctionCalculator
        Inherits ScatterFunctionCalculatorBase

        Protected Overrides Function NormalizeAngle(ByVal angle As Double) As Double
            Return angle Mod (Math.PI * 2.0)
        End Function
        Protected Overrides Function ToRadian(ByVal angle As Double) As Double
            Return angle
        End Function
        Protected Overrides Function FromDegrees(ByVal angle As Double) As Double
            Return angle * Math.PI / 180.0
        End Function
    End Class


    Public Class DegreeScatterFunctionCalculator
        Inherits ScatterFunctionCalculatorBase

        Protected Overrides Function NormalizeAngle(ByVal angle As Double) As Double
            Return angle Mod 360
        End Function
        Protected Overrides Function ToRadian(ByVal angle As Double) As Double
            Return angle * Math.PI / 180.0
        End Function
        Protected Overrides Function FromDegrees(ByVal angle As Double) As Double
            Return angle
        End Function
    End Class


    Public Class Bar2DKindToTickmarksLengthConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim bar2DKind As Bar2DKind = TryCast(value, Bar2DKind)
            If bar2DKind IsNot Nothing Then
                Select Case bar2DKind.Name
                    Case "Glass Cylinder"
                        Return 18
                    Case "Quasi-3D Bar"
                        Return 9
                End Select
            End If
            Return 5
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class


    Public Class MarkerSizeToLabelIndentConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return (DirectCast(value, Double)) / 2
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class

    Public Class BoolToResolveOverlappingModeConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim booleanValue As Boolean = DirectCast(value, Boolean)
            If booleanValue = True Then
                Return ResolveOverlappingMode.Default
            Else
                Return ResolveOverlappingMode.None
            End If
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class


    Public NotInheritable Class PaletteSelectorHelper

        Private Sub New()
        End Sub

        Private Shared actualPalette_Renamed As Palette = New OfficePalette()

        Public Shared Property ActualPalette() As Palette
            Get
                Return actualPalette_Renamed
            End Get
            Set(ByVal value As Palette)
                actualPalette_Renamed = value
            End Set
        End Property
    End Class


    Public NotInheritable Class CsvReader

        Private Sub New()
        End Sub
        Private Const FileNamePrefix As String = "/ChartsDemo;component/Data/"

        Public Shared Function ReadFinancialData(ByVal fileName As String) As List(Of FinancialPoint)
            Dim longFileName As String = String.Empty
            Dim reader As StreamReader
            Dim dataSource = New List(Of FinancialPoint)()
            Try
                longFileName = FileNamePrefix & fileName
                Dim uri As New Uri(longFileName, UriKind.RelativeOrAbsolute)
                Dim info As StreamResourceInfo = Application.GetResourceStream(uri)
                reader = New StreamReader(info.Stream)
                Do While Not reader.EndOfStream
                    Dim line As String = reader.ReadLine()
                    Dim values = line.Split(","c)
                    Dim point = New FinancialPoint()
                    point.DateTimeArgument = Date.ParseExact(values(0), "yyyy.MM.dd", CultureInfo.InvariantCulture)
                    point.OpenValue = Double.Parse(values(1), CultureInfo.InvariantCulture)
                    point.HighValue = Double.Parse(values(2), CultureInfo.InvariantCulture)
                    point.LowValue = Double.Parse(values(3), CultureInfo.InvariantCulture)
                    point.CloseValue = Double.Parse(values(4), CultureInfo.InvariantCulture)
                    dataSource.Add(point)
                Loop
            Catch
                Throw New Exception("It's impossible to load " & fileName)
            End Try
            Return dataSource
        End Function
    End Class


    <StructLayout(LayoutKind.Sequential, Pack := 1)>
    Public Structure PixelColor
        Public Blue As Byte
        Public Green As Byte
        Public Red As Byte
        Public Alpha As Byte
        Public ReadOnly Property Gray() As Byte
            Get
                Return CByte((CInt(Blue) + CInt(Green) + CInt(Red)) \ 3)
            End Get
        End Property
    End Structure


    Public Class ImageData
        Private ReadOnly streamResourceInfo As StreamResourceInfo

        Public Sub New(ByVal uri As Uri)
            Me.streamResourceInfo = Application.GetResourceStream(uri)
        End Sub
        Private Function GetPixels(ByVal source As BitmapSource) As PixelColor(,)
            If source.Format <> PixelFormats.Bgra32 Then
                source = New FormatConvertedBitmap(source, PixelFormats.Bgra32, Nothing, 0)
            End If
            Dim result(source.PixelWidth - 1, source.PixelHeight - 1) As PixelColor
            Dim stride As Integer = CInt(source.PixelWidth) * (source.Format.BitsPerPixel \ 8)
            CopyPixels(source, result, stride, 0)
            Return result
        End Function
        Private Sub CopyPixels(ByVal source As BitmapSource, ByVal pixels(,) As PixelColor, ByVal stride As Integer, ByVal offset As Integer)
            Dim height = source.PixelHeight
            Dim width = source.PixelWidth
            Dim pixelBytes = New Byte((height * width * 4) - 1){}
            source.CopyPixels(pixelBytes, stride, 0)
            Dim y0 As Integer = offset \ width
            Dim x0 As Integer = offset - width * y0
            For y As Integer = 0 To height - 1
                For x As Integer = 0 To width - 1
                    pixels(x + x0, y + y0) = New PixelColor With {.Blue = pixelBytes((y * width + x) * 4 + 0), .Green = pixelBytes((y * width + x) * 4 + 1), .Red = pixelBytes((y * width + x) * 4 + 2), .Alpha = pixelBytes((y * width + x) * 4 + 3)}
                Next x
            Next y
        End Sub
        Private Sub DoEvents()
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, New Action(Sub()
            End Sub))
        End Sub
        Public Function GetPixels() As PixelColor(,)
            Dim pixels(-1,-1) As PixelColor
            Try
                Dim source As New BitmapImage()
                source.BeginInit()
                source.StreamSource = Me.streamResourceInfo.Stream
                source.EndInit()
                Do While source.IsDownloading
                    DoEvents()
                Loop
                pixels = GetPixels(source)
            Catch
            End Try
            Return pixels
        End Function
    End Class

    <System.Windows.Markup.ContentProperty("Content")>
    Public Class ValueSelectorItem
        Inherits DependencyObject

        Public Property Content() As Object
            Get
                Return GetValue(ContentProperty)
            End Get
            Set(ByVal value As Object)
                SetValue(ContentProperty, value)
            End Set
        End Property
        Public Shared ReadOnly ContentProperty As DependencyProperty = DependencyProperty.Register("Content", GetType(Object), GetType(ValueSelectorItem), New PropertyMetadata(Nothing))

        Public Property DisplayName() As String
            Get
                Return DirectCast(GetValue(DisplayNameProperty), String)
            End Get
            Set(ByVal value As String)
                SetValue(DisplayNameProperty, value)
            End Set
        End Property
        Public Shared ReadOnly DisplayNameProperty As DependencyProperty = DependencyProperty.Register("DisplayName", GetType(String), GetType(ValueSelectorItem), New PropertyMetadata(Nothing))
    End Class

End Namespace
