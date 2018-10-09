Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Windows.Media.Imaging
Imports System.Windows.Media.Media3D
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors
Imports System.Linq
Imports DevExpress.Mvvm

Namespace ChartsDemo
    Public Class MathematicalFunctionsViewModel
        Private Const UnitFactor As Double = 15

        Private privateFunctionItemsData As List(Of Function3DItemData)
        Public Property FunctionItemsData() As List(Of Function3DItemData)
            Get
                Return privateFunctionItemsData
            End Get
            Private Set(ByVal value As List(Of Function3DItemData))
                privateFunctionItemsData = value
            End Set
        End Property
        Public Overridable Property [Function]() As Function3DItemData

        Public Sub New()
            FunctionItemsData = CreateFunctionItemsData()
            [Function] = FunctionItemsData(3)
        End Sub

        #Region "Static"

        Private Shared Function CreateFunctionItemsData() As List(Of Function3DItemData)
            Dim funcs = New Func(Of Double, Double, Point3D)() { AddressOf CalculateFirstValue, AddressOf CalculateSecondValue, AddressOf CalculateThirdValue, AddressOf CalculateFourthValue, AddressOf CalculateFifthValue }
            Dim images = Enumerable.Range(1, 5).Select(Function(x) Chart3DUtils.CreateFunctionImage(x))
            Return funcs.Zip(images, Function(func, image) New Function3DItemData With {.Image = image, .Points = CreatePoints(func)}).ToList()
        End Function

        Private Shared Function Sinc(ByVal x As Double) As Double
            Return If(x <> 0, Math.Sin(x), 1)
        End Function
        Private Shared Function CalculateFirstValue(ByVal x As Double, ByVal y As Double) As Point3D
            x *= UnitFactor
            y *= UnitFactor
            Dim value As Double = Sinc(Math.Sin(Math.Pow(Math.Pow(x, 6) + Math.Pow(y, 6), 1.0R / 6.0R))) * 5
            Return New Point3D(x, y, value)
        End Function
        Private Shared Function CalculateSecondValue(ByVal x As Double, ByVal y As Double) As Point3D
            x *= UnitFactor
            y *= UnitFactor
            Dim square As Double = Math.Sqrt(x * x + y * y)
            Dim value As Double = square * Sinc(square) * 0.2
            Return New Point3D(x, y, value)
        End Function
        Private Shared Function CalculateThirdValue(ByVal x As Double, ByVal y As Double) As Point3D
            x *= UnitFactor / 2
            y *= UnitFactor / 2
            Dim value As Double = Math.Sin(x) * Math.Cos(y) * 2
            Return New Point3D(x, y, value)
        End Function
        Private Shared Function CalculateFourthValue(ByVal x As Double, ByVal y As Double) As Point3D
            Dim theta As Double = Math.Atan2(y, x)
            Dim r As Double = x * x + y * y
            Dim z As Double = Math.Exp(-r) * Math.Sin(2 * Math.PI * Math.Sqrt(r)) * Math.Cos(3 * theta)
            Return New Point3D(x, y, z)
        End Function
        Private Shared Function CalculateFifthValue(ByVal x As Double, ByVal y As Double) As Point3D
            x *= 3
            y *= 3
            Dim z As Double = Math.Sin(x * y)
            Return New Point3D(x, y, z)
        End Function
        Private Shared Function CreatePoints(ByVal valueCalculator As Func(Of Double, Double, Point3D)) As List(Of Point3D)
            Dim points As New List(Of Point3D)()
            For x As Double = -1 To 0 Step 0.017
                For y As Double = -1 To 0 Step 0.017
                    points.Add(valueCalculator(x, y))
                Next y
            Next x
            Return points
        End Function

        #End Region
    End Class
    Public Class Function3DItemData
        Inherits BindableBase


        Private points_Renamed As List(Of Point3D)

        Private image_Renamed As BitmapImage

        Public Property Points() As List(Of Point3D)
            Get
                Return points_Renamed
            End Get
            Set(ByVal value As List(Of Point3D))
                SetProperty(points_Renamed, value, Function() Points)
            End Set
        End Property
        Public Property Image() As BitmapImage
            Get
                Return image_Renamed
            End Get
            Set(ByVal value As BitmapImage)
                SetProperty(image_Renamed, value, Function() Image)
            End Set
        End Property
    End Class
End Namespace
