Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Threading
Imports DevExpress.Utils
Imports DevExpress.Xpf.Gauges
Imports DevExpress.Mvvm

Namespace GaugesDemo
    Public Enum PressureState
        Low
        Normal
        High
        Undefined
    End Enum

    Public Enum TemperatureState
        Low
        High
    End Enum

    Public Class PredefinedElementKindToCircularGaugeModel
        Implements IValueConverter

        #Region "IValueConverter implementation"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim gaugeModelKind As PredefinedElementKind = TryCast(value, PredefinedElementKind)
            If gaugeModelKind IsNot Nothing AndAlso gaugeModelKind.Type.BaseType Is GetType(CircularGaugeModel) Then
                Return Activator.CreateInstance(gaugeModelKind.Type)
            End If
            Return value
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class

    Public Class PredefinedElementKindToLinearGaugeModel
        Implements IValueConverter

        #Region "IValueConverter implementation"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim gaugeModelKind As PredefinedElementKind = TryCast(value, PredefinedElementKind)
            If gaugeModelKind IsNot Nothing AndAlso gaugeModelKind.Type.BaseType Is GetType(LinearGaugeModel) Then
                Return Activator.CreateInstance(gaugeModelKind.Type)
            End If
            Return value
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class

    Public Class PredefinedElementKindToDigitalGaugeModel
        Implements IValueConverter

        #Region "IValueConverter implementation"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim gaugeModelKind As PredefinedElementKind = TryCast(value, PredefinedElementKind)
            If gaugeModelKind IsNot Nothing AndAlso gaugeModelKind.Type.BaseType Is GetType(DigitalGaugeModel) Then
                Return Activator.CreateInstance(gaugeModelKind.Type)
            End If
            Return value
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class

    Public Class DemoValuesProvider
        Public ReadOnly Property PredefinedCircularGaugeModelKinds() As IEnumerable(Of PredefinedElementKind)
            Get
                Return CircularGaugeControl.PredefinedModels
            End Get
        End Property
        Public ReadOnly Property PredefinedLinearGaugeModelKinds() As IEnumerable(Of PredefinedElementKind)
            Get
                Return LinearGaugeControl.PredefinedModels
            End Get
        End Property
        Public ReadOnly Property PredefinedDigitalGaugeModelKinds() As IEnumerable(Of PredefinedElementKind)
            Get
                Return DigitalGaugeControl.PredefinedModels
            End Get
        End Property
    End Class

    Public Class StringToEasingFunctionConvert
        Implements IValueConverter

        #Region "IValueConverter implementation"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim functionEase As String = TryCast(value, String)
            Dim returnFunctionEase As IEasingFunction = Nothing
            If functionEase IsNot Nothing Then
                Select Case functionEase
                    Case "ElasticEase"
                        returnFunctionEase = New ElasticEase()
                        DirectCast(returnFunctionEase, ElasticEase).Springiness = 8
                        DirectCast(returnFunctionEase, ElasticEase).Oscillations = 10
                    Case "BounceEase"
                        returnFunctionEase = New BounceEase()
                        DirectCast(returnFunctionEase, BounceEase).Bounces = 8
                        DirectCast(returnFunctionEase, BounceEase).Bounciness = 2
                    Case "BackEase"
                        returnFunctionEase = New BackEase()
                        DirectCast(returnFunctionEase, BackEase).Amplitude = 1
                    Case "CircleEase"
                        returnFunctionEase = New CircleEase()
                    Case "CubicEase"
                        returnFunctionEase = New CubicEase()
                    Case "ExponentialEase"
                        returnFunctionEase = New ExponentialEase()
                        DirectCast(returnFunctionEase, ExponentialEase).Exponent = 5
                    Case "PowerEase"
                        returnFunctionEase = New PowerEase()
                        DirectCast(returnFunctionEase, PowerEase).Power = 5
                    Case "QuadraticEase"
                        returnFunctionEase = New QuadraticEase()
                    Case "QuarticEase"
                        returnFunctionEase = New QuarticEase()
                    Case "QuinticEase"
                        returnFunctionEase = New QuinticEase()
                    Case "SineEase"
                        returnFunctionEase = New SineEase()
                End Select
            End If
            Return returnFunctionEase
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class

    Public Class DoubleToTimeSpanConvert
        Implements IValueConverter

        #Region "IValueConvector implementation"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim doubleValue As Double = DirectCast(value, Double)
            Return New TimeSpan(0, 0, CInt((Math.Ceiling(doubleValue))))
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class

    Public Class BoolToDefaultBooleanConvert
        Implements IValueConverter

        #Region "IValueConvector implementation"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim booleanValue As Boolean = DirectCast(value, Boolean)
            Return If(booleanValue, DefaultBoolean.True, DefaultBoolean.False)
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class

    Public Class BoolToSymbolPresentationConverter
        Implements IValueConverter

        Private Shared redBrush As New SolidColorBrush(Color.FromArgb(230, 255, 56, 56))
        Private Shared transparentRedBrush As New SolidColorBrush(Color.FromArgb(25, 255, 56, 56))
        Private Shared greenBrush As New SolidColorBrush(Color.FromArgb(230, 27, 255, 20))
        Private Shared transparentGreenBrush As New SolidColorBrush(Color.FromArgb(25, 27, 255, 20))
        Private Shared yellowBrush As New SolidColorBrush(Color.FromArgb(230, 238, 255, 20))
        Private Shared transparentYellowBrush As New SolidColorBrush(Color.FromArgb(25, 238, 255, 20))
        Private Shared transparentBrush As New SolidColorBrush(Colors.Transparent)

        Private Shared redSegmentPresentation As New DefaultMatrix8x14Presentation() With {.FillActive = redBrush, .FillInactive = transparentBrush}
        Private Shared gangerRedSegmentPresentation As New DefaultMatrix8x14Presentation() With {.FillActive = redBrush, .FillInactive = transparentBrush}
        Private Shared yellowSegmentPresentation As New DefaultMatrix8x14Presentation() With {.FillActive = yellowBrush, .FillInactive = transparentBrush}
        Private Shared greenLeftSegmentPresentation As New DefaultMatrix8x14Presentation() With {.FillActive = greenBrush, .FillInactive = transparentBrush}
        Private Shared greenRightSegmentPresentation As New DefaultMatrix8x14Presentation() With {.FillActive = greenBrush, .FillInactive = transparentBrush}
        Private Shared gangerGreenSegmentPresentation As New DefaultMatrix8x14Presentation() With {.FillActive = transparentGreenBrush, .FillInactive = transparentBrush}
        Private Shared timerPresentation As New DefaultFourteenSegmentsPresentation() With {.FillActive = greenBrush, .FillInactive = transparentBrush}

        #Region "IValueConvector implementation"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If targetType.BaseType Is GetType(SymbolPresentation) Then
                Dim currentSegment As String = DirectCast(parameter, String)
                Dim isSegmentEnabled As Boolean = DirectCast(value, Boolean)

                Select Case currentSegment
                    Case "Red"
                            If isSegmentEnabled Then
                                redSegmentPresentation.FillActive = redBrush
                            Else
                                redSegmentPresentation.FillActive = transparentRedBrush
                            End If
                            Return redSegmentPresentation
                    Case "Yellow"
                            If isSegmentEnabled Then
                                yellowSegmentPresentation.FillActive = yellowBrush
                            Else
                                yellowSegmentPresentation.FillActive = transparentYellowBrush
                            End If
                            Return yellowSegmentPresentation
                    Case "GreenLeft"
                            If isSegmentEnabled Then
                                greenLeftSegmentPresentation.FillActive = greenBrush
                            Else
                                greenLeftSegmentPresentation.FillActive = transparentGreenBrush
                            End If
                            Return greenLeftSegmentPresentation
                    Case "GreenRight"
                            If isSegmentEnabled Then
                                greenRightSegmentPresentation.FillActive = greenBrush
                            Else
                                greenRightSegmentPresentation.FillActive = transparentGreenBrush
                            End If
                            Return greenRightSegmentPresentation
                    Case "GangerGreen"
                            If isSegmentEnabled Then
                                gangerGreenSegmentPresentation.FillActive = greenBrush
                            Else
                                gangerGreenSegmentPresentation.FillActive = transparentGreenBrush
                            End If
                            Return gangerGreenSegmentPresentation
                    Case "GangerRed"
                            If isSegmentEnabled Then
                                gangerRedSegmentPresentation.FillActive = redBrush
                            Else
                                gangerRedSegmentPresentation.FillActive = transparentRedBrush
                            End If
                            Return gangerRedSegmentPresentation
                    Case Else
                            If isSegmentEnabled Then
                                timerPresentation.FillActive = greenBrush
                            Else
                                timerPresentation.FillActive = redBrush
                            End If
                            Return timerPresentation
                End Select
            End If
            Return Nothing
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class

    Public Class BoolToVisibilityConverter
        Implements IValueConverter

        #Region "IValueConvector implementation"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If targetType Is GetType(Visibility) Then
                Dim isSegmentEnabled As Boolean = DirectCast(value, Boolean)
                If isSegmentEnabled Then
                    Return Visibility.Visible
                Else
                    Return Visibility.Collapsed
                End If
            End If
            Return Nothing
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
        #End Region
    End Class

    Public NotInheritable Class Utils

        Private Sub New()
        End Sub

        Public Shared Function ConvertArabicToRoman(ByVal arabic As Integer) As String
            Dim roman As String = ""
            Dim bigNumber As Boolean = False
            Dim numerationSystemRoman() As String = { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M" }
            Dim numerationSystemArabic() As Integer = { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000 }
            Do While arabic > 0
                bigNumber = True
                For i As Integer = 1 To numerationSystemRoman.Length - 1
                    If arabic < numerationSystemArabic(i) Then
                        roman &= numerationSystemRoman(i - 1)
                        arabic -= numerationSystemArabic(i - 1)
                        bigNumber = False
                        Exit For
                    End If
                Next i
                If bigNumber Then
                    roman &= numerationSystemRoman(numerationSystemRoman.Length - 1)
                    arabic -= numerationSystemArabic(numerationSystemRoman.Length - 1)
                End If
            Loop
            Return roman
        End Function
    End Class

    Public Class GaugeRandomDataGenerator
        Inherits BindableBase

        Public Property NeedleValue() As Double
            Get
                Return GetProperty(Function() NeedleValue)
            End Get
            Set(ByVal value As Double)
                SetProperty(Function() NeedleValue, value)
            End Set
        End Property
        Public Property RangeBarValue() As Double
            Get
                Return GetProperty(Function() RangeBarValue)
            End Get
            Set(ByVal value As Double)
                SetProperty(Function() RangeBarValue, value)
            End Set
        End Property
        Public Property MarkerValue() As Double
            Get
                Return GetProperty(Function() MarkerValue)
            End Get
            Set(ByVal value As Double)
                SetProperty(Function() MarkerValue, value)
            End Set
        End Property
        Public Property LevelBarValue() As Double
            Get
                Return GetProperty(Function() LevelBarValue)
            End Get
            Set(ByVal value As Double)
                SetProperty(Function() LevelBarValue, value)
            End Set
        End Property

        Private Const defaultUpdateInterval As Double = 1000

        Private ReadOnly minValue As Double
        Private ReadOnly maxValue As Double
        Private ReadOnly random As New Random()
        Private ReadOnly updateTimer As DispatcherTimer

        Private ReadOnly Property ValuesRnage() As Double
            Get
                Return maxValue - minValue
            End Get
        End Property

        Public Sub New(ByVal minValue As Double, ByVal maxValue As Double, ByVal updateInterval As Double)
            Me.minValue = minValue
            Me.maxValue = maxValue
            updateTimer = New DispatcherTimer()
            updateTimer.Interval = TimeSpan.FromMilliseconds(updateInterval)
            AddHandler Me.updateTimer.Tick, AddressOf OnTimerTick
        End Sub
        Public Sub New(ByVal minValue As Double, ByVal maxValue As Double)
            Me.New(minValue, maxValue, defaultUpdateInterval)
        End Sub
        Private Sub OnTimerTick(ByVal sender As Object, ByVal e As EventArgs)
            NeedleValue = minValue + ValuesRnage * random.NextDouble()
            RangeBarValue = minValue + ValuesRnage * random.NextDouble()
            MarkerValue = minValue + ValuesRnage * random.NextDouble()
            LevelBarValue = minValue + ValuesRnage * random.NextDouble()
        End Sub
        Public Sub Start()
            updateTimer.Start()
        End Sub
        Public Sub [Stop]()
            updateTimer.Stop()
        End Sub
    End Class
End Namespace
