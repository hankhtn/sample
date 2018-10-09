Imports System
Imports System.Linq
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.DemoBase.DemosHelpers.Grid
Imports DevExpress.Xpf.Editors
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.Data.Helpers
Imports System.Windows.Data
Imports DevExpress.Mvvm.DataAnnotations

Namespace PropertyGridDemo

    Public Class DataEditorsViewModel

        Private data_Renamed As DynamicallyAssignDataEditorsData
        Public ReadOnly Property Data() As DynamicallyAssignDataEditorsData
            Get
                If data_Renamed Is Nothing Then
                    data_Renamed = New DynamicallyAssignDataEditorsData()
                End If
                Return data_Renamed
            End Get
        End Property
        Public ReadOnly Property Properties() As IEnumerable(Of PropertyDescriptor)
            Get
                Return TypeDescriptor.GetProperties(data_Renamed).Cast(Of PropertyDescriptor)()
            End Get
        End Property
        Public Shared ReadOnly PalleteSizesStatic() As String = { "4x4", "10x10", "16x16", "20x20", "25x25" }
        Public ReadOnly Property PalleteSizes() As String()
            Get
                Return PalleteSizesStatic
            End Get
        End Property
    End Class
    Public Class DynamicallyAssignDataEditorsTemplateSelector
        Inherits DataTemplateSelector

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim element As FrameworkElement = CType(container, FrameworkElement)
            Dim resource As DataTemplate = TryCast(element.TryFindResource(GetTemplateName(DirectCast(item, PropertyDescriptor))), DataTemplate)
            Return If(resource, MyBase.SelectTemplate(item, container))
        End Function
        Public Shared Function GetTemplateName(ByVal [property] As PropertyDescriptor) As String
            Dim displayAttribute = CType([property].Attributes(GetType(DisplayAttribute)), DisplayAttribute)
            Return If(displayAttribute.GetDescription(), displayAttribute.GetName())
        End Function
    End Class
    Public Class PropertyDescriptorToDisplayNameConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim [property] = DirectCast(value, PropertyDescriptor)
            Dim displayAttribute = CType([property].Attributes(GetType(DisplayAttribute)), DisplayAttribute)
            Return displayAttribute.GetName()
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
