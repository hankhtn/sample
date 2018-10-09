Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Effects
Imports System.Windows.Media.Imaging
Imports System.Windows.Media.Media3D
Imports System.Windows.Resources

Namespace ChartsDemo
    Public NotInheritable Class Chart3DUtils

        Private Sub New()
        End Sub

        Public Shared Function CreateSize3D(ByVal x As Double, ByVal y As Double, ByVal z As Double) As Size3D
            Return New Size3D(x, y, z)
        End Function
        Public Shared Function CreateFunctionImage(ByVal index As Integer) As BitmapImage
            Dim uriStr As String = String.Format("/ChartsDemo;component/Images/Functions/{0}.png", index)
            Return New BitmapImage(New Uri(uriStr, UriKind.RelativeOrAbsolute))
        End Function
    End Class

    Public Class DataGenerator
        Public Class ElevationData

            Private directionX_Renamed As Integer

            Private directionZ_Renamed As Integer

            Private directionY_Renamed As Integer

            Public Property Elevation() As Point3D
            Public ReadOnly Property DirectionX() As Integer
                Get
                    Return directionX_Renamed
                End Get
            End Property
            Public ReadOnly Property DirectionZ() As Integer
                Get
                    Return directionZ_Renamed
                End Get
            End Property
            Public ReadOnly Property DirectionY() As Integer
                Get
                    Return directionY_Renamed
                End Get
            End Property

            Public Sub New(ByVal elevation As Point3D)
                Me.Elevation = elevation
                Me.directionX_Renamed = 1
                Me.directionZ_Renamed = 1
                Me.directionY_Renamed = 1
            End Sub
            Public Sub UpdateDirection(ByVal sideLength As Integer)
                If Elevation.X < 0 OrElse Elevation.X >= sideLength Then
                    directionX_Renamed *= -1
                End If
                If Elevation.Z < 0 OrElse Elevation.Z >= sideLength Then
                    directionZ_Renamed *= -1
                End If
                If Elevation.Y < -sideLength \ 3 OrElse Elevation.Y > sideLength \ 3 Then
                    directionY_Renamed *= -1
                End If
            End Sub
        End Class

        Private Const OffsetFactor As Double = Math.PI * 3.0 / 2.0
        Private Const RadiusFactor As Double = 10.0 / Math.PI
        Private Const ElevationsCount As Integer = 25

        Private ReadOnly rnd As New Random()
        Private ReadOnly vertices() As ElevationData
        Private sensetivitiDirection As Integer = 1
        Private sensitivity As Double

        Public Property Size() As Integer

        Public Sub New()
            Me.vertices = New ElevationData(ElevationsCount - 1){}
        End Sub
        Private Sub NextElevations()
            Dim k As Double = CDbl(Size) / 66.0
            For i As Integer = 0 To ElevationsCount - 1
                vertices(i).UpdateDirection(Size)
                Dim dx As Double = k * vertices(i).DirectionX
                Dim dz As Double = k * vertices(i).DirectionZ
                Dim dy As Double = k * vertices(i).DirectionY
                Dim x As Double = vertices(i).Elevation.X + dx
                Dim z As Double = vertices(i).Elevation.Z + dz
                Dim y As Double = vertices(i).Elevation.Y + dy
                vertices(i).Elevation = New Point3D(x, y, z)
            Next i
        End Sub
        Private Sub NextSensetivity()
            If sensitivity < Size * 0.15 Then
                sensetivitiDirection = 1
            End If
            If sensitivity > Size * 0.6 Then
                sensetivitiDirection = -1
            End If
            sensitivity += sensetivitiDirection * 0.2
        End Sub
        Public Sub RecreateElevations()
            sensitivity = Size * 0.5
            For i As Integer = 0 To ElevationsCount - 1
                Dim elevation As New Point3D(rnd.Next(0, Size), rnd.Next(CInt(-Size \ 3), CInt(Size \ 3)), rnd.Next(0, Size))
                vertices(i) = New ElevationData(elevation)
            Next i
        End Sub
        Public Function GenerateArguments() As IEnumerable(Of Object)
            Dim arguments As New List(Of Object)()
            For i As Integer = 0 To Size - 1
                arguments.Add(i)
            Next i
            Return arguments
        End Function
        Public Function GenerateValues() As IEnumerable(Of Double)
            NextElevations()
            NextSensetivity()
            Dim values((Size * Size) - 1) As Double
            For j As Integer = 0 To vertices.Length - 1
                Dim dataX As Double = vertices(j).Elevation.X
                Dim dataZ As Double = vertices(j).Elevation.Z
                Dim dataY As Double = vertices(j).Elevation.Y
                Dim counter As Integer = 0
                For x As Integer = 0 To Size - 1
                    For z As Integer = 0 To Size - 1
                        Dim dx As Double = dataX - x
                        Dim dz As Double = dataZ - z
                        Dim distance As Double = Math.Sqrt(dx * dx + dz * dz)
                        If distance <= sensitivity Then
                            Dim percent As Double = 1.0 - distance / sensitivity
                            Dim coef As Double = dataY - values(counter)
                            Dim elevate As Double = coef * (Math.Sin(percent * RadiusFactor + OffsetFactor) + 1.0) / 2
                            values(counter) += elevate
                        End If
                        counter += 1
                    Next z
                Next x
            Next j
            Return values
        End Function
    End Class

    Public Class Point3DComparer
        Implements IEqualityComparer(Of Point3D)

        Private Const DefaultEps As Double = 10e-12
        Private eps As Double = DefaultEps

        Public Sub New()
        End Sub

        Public Sub New(ByVal eps As Double)
            Me.eps = eps
        End Sub

        Public Overloads Function Equals(ByVal p1 As Point3D, ByVal p2 As Point3D) As Boolean Implements IEqualityComparer(Of Point3D).Equals
            Return Math.Abs(p1.X - p2.X) < eps AndAlso Math.Abs(p1.Y - p2.Y) < eps AndAlso Math.Abs(p1.Z - p2.Z) < eps
        End Function
        Public Overloads Function GetHashCode(ByVal e1 As Point3D) As Integer Implements IEqualityComparer(Of Point3D).GetHashCode
            Return e1.GetHashCode()
        End Function
    End Class

    Public Class StarAxisLabelDataToStringConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim res As Double = 0
            If Double.TryParse(TryCast(value, String), NumberStyles.Float, CultureInfo.InvariantCulture, res) Then
                Return If(Math.Abs(res) > 1000, String.Format("{0}k", res * 0.001), res.ToString())
            End If
            Return value
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class

    Public Class StarEffect
        Inherits ShaderEffect

        Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(StarEffect), 0)
        Public Shared ReadOnly TimeProperty As DependencyProperty = DependencyProperty.Register("Time", GetType(Double), GetType(StarEffect), New UIPropertyMetadata((CDbl(0R)), PixelShaderConstantCallback(0)))
        Public Shared ReadOnly WaveSizeProperty As DependencyProperty = DependencyProperty.Register("WaveSize", GetType(Double), GetType(StarEffect), New UIPropertyMetadata((CDbl(196R)), PixelShaderConstantCallback(1)))
        Public Shared ReadOnly CenterPointProperty As DependencyProperty = DependencyProperty.Register("CenterPoint", GetType(Point), GetType(StarEffect), New UIPropertyMetadata(New Point(0.5R, 0.5R), PixelShaderConstantCallback(2)))
        Public Shared ReadOnly InnerRadiusProperty As DependencyProperty = DependencyProperty.Register("InnerRadius", GetType(Double), GetType(StarEffect), New UIPropertyMetadata((CDbl(0.2R)), PixelShaderConstantCallback(3)))
        Public Shared ReadOnly OuterRadiusProperty As DependencyProperty = DependencyProperty.Register("OuterRadius", GetType(Double), GetType(StarEffect), New UIPropertyMetadata((CDbl(0.4R)), PixelShaderConstantCallback(4)))
        Public Shared ReadOnly MagnificationAmountProperty As DependencyProperty = DependencyProperty.Register("MagnificationAmount", GetType(Double), GetType(StarEffect), New UIPropertyMetadata((CDbl(1R)), PixelShaderConstantCallback(5)))
        Public Shared ReadOnly AspectRatioProperty As DependencyProperty = DependencyProperty.Register("AspectRatio", GetType(Double), GetType(StarEffect), New UIPropertyMetadata((CDbl(1.5R)), PixelShaderConstantCallback(6)))
        Public Shared ReadOnly BlurAmountProperty As DependencyProperty = DependencyProperty.Register("BlurAmount", GetType(Double), GetType(StarEffect), New UIPropertyMetadata((CDbl(2R)), PixelShaderConstantCallback(7)))
        Public Shared ReadOnly MinValueProperty As DependencyProperty = DependencyProperty.Register("MinValue", GetType(Double), GetType(StarEffect), New UIPropertyMetadata((CDbl(0R)), PixelShaderConstantCallback(8)))
        Public Shared ReadOnly MaxValueProperty As DependencyProperty = DependencyProperty.Register("MaxValue", GetType(Double), GetType(StarEffect), New UIPropertyMetadata((CDbl(0.5R)), PixelShaderConstantCallback(9)))
        Public Sub New()

            Dim pixelShader_Renamed As New PixelShader()
            pixelShader_Renamed.UriSource = New Uri("/ChartsDemo;component/Data/Star.ps", UriKind.Relative)
            Me.PixelShader = pixelShader_Renamed

            Me.UpdateShaderValue(InputProperty)
            Me.UpdateShaderValue(TimeProperty)
            Me.UpdateShaderValue(WaveSizeProperty)
            Me.UpdateShaderValue(CenterPointProperty)
            Me.UpdateShaderValue(InnerRadiusProperty)
            Me.UpdateShaderValue(OuterRadiusProperty)
            Me.UpdateShaderValue(MagnificationAmountProperty)
            Me.UpdateShaderValue(AspectRatioProperty)
            Me.UpdateShaderValue(BlurAmountProperty)
            Me.UpdateShaderValue(MinValueProperty)
            Me.UpdateShaderValue(MaxValueProperty)
        End Sub
        Public Property Input() As Brush
            Get
                Return (DirectCast(Me.GetValue(InputProperty), Brush))
            End Get
            Set(ByVal value As Brush)
                Me.SetValue(InputProperty, value)
            End Set
        End Property
        Public Property Time() As Double
            Get
                Return (DirectCast(Me.GetValue(TimeProperty), Double))
            End Get
            Set(ByVal value As Double)
                Me.SetValue(TimeProperty, value)
            End Set
        End Property
        Public Property WaveSize() As Double
            Get
                Return (DirectCast(Me.GetValue(WaveSizeProperty), Double))
            End Get
            Set(ByVal value As Double)
                Me.SetValue(WaveSizeProperty, value)
            End Set
        End Property
        Public Property CenterPoint() As Point
            Get
                Return (DirectCast(Me.GetValue(CenterPointProperty), Point))
            End Get
            Set(ByVal value As Point)
                Me.SetValue(CenterPointProperty, value)
            End Set
        End Property
        Public Property InnerRadius() As Double
            Get
                Return (DirectCast(Me.GetValue(InnerRadiusProperty), Double))
            End Get
            Set(ByVal value As Double)
                Me.SetValue(InnerRadiusProperty, value)
            End Set
        End Property
        Public Property OuterRadius() As Double
            Get
                Return (DirectCast(Me.GetValue(OuterRadiusProperty), Double))
            End Get
            Set(ByVal value As Double)
                Me.SetValue(OuterRadiusProperty, value)
            End Set
        End Property
        Public Property MagnificationAmount() As Double
            Get
                Return (DirectCast(Me.GetValue(MagnificationAmountProperty), Double))
            End Get
            Set(ByVal value As Double)
                Me.SetValue(MagnificationAmountProperty, value)
            End Set
        End Property
        Public Property AspectRatio() As Double
            Get
                Return (DirectCast(Me.GetValue(AspectRatioProperty), Double))
            End Get
            Set(ByVal value As Double)
                Me.SetValue(AspectRatioProperty, value)
            End Set
        End Property
        Public Property BlurAmount() As Double
            Get
                Return (DirectCast(Me.GetValue(BlurAmountProperty), Double))
            End Get
            Set(ByVal value As Double)
                Me.SetValue(BlurAmountProperty, value)
            End Set
        End Property
        Public Property MinValue() As Double
            Get
                Return (DirectCast(Me.GetValue(MinValueProperty), Double))
            End Get
            Set(ByVal value As Double)
                Me.SetValue(MinValueProperty, value)
            End Set
        End Property
        Public Property MaxValue() As Double
            Get
                Return (DirectCast(Me.GetValue(MaxValueProperty), Double))
            End Get
            Set(ByVal value As Double)
                Me.SetValue(MaxValueProperty, value)
            End Set
        End Property
    End Class
End Namespace
