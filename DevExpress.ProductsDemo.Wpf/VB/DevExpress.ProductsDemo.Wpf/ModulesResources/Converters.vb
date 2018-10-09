Imports DevExpress.Data.Helpers
Imports DevExpress.Utils
Imports DevExpress.Xpf.Editors.Settings
Imports DevExpress.Xpf.Grid
Imports DevExpress.Mvvm
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Media.Imaging
Imports System.Windows.Markup
Imports System.Globalization

Namespace ProductsDemo.Modules
    Public Class EmptyPhotoConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value IsNot Nothing Then
                Return value
            End If
            Return New BitmapImage(New Uri("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Contacts/Unknown-user.png", UriKind.RelativeOrAbsolute))
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return value
        End Function
    End Class
    Public Class ViewToVisibilityConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing OrElse Not(TypeOf value Is ViewType) Then
                Return Visibility.Collapsed
            End If
            Dim viewType As ViewType = DirectCast(value, ViewType)
            Return If(viewType = ProductsDemo.Modules.ViewType.TableView, Visibility.Visible, Visibility.Collapsed)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
    Public Class ViewToMarginConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return If(TypeOf value Is TableView, New Thickness(1, 1, 4, 1), New Thickness(1))
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
    Public Class ObjectToOpacityConverter
        Implements IValueConverter

        Public Property Invert() As Boolean
        Public Property Opacity() As Double
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return If(value IsNot Nothing Xor Invert, Opacity, 0)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
    Public Class ColumnIconConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing OrElse Not(TypeOf value Is Integer) Then
                Return Nothing
            End If
            Dim intValue As Integer = DirectCast(value, Integer)
            Return New BitmapImage(New Uri(String.Format("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Status_{0}.png", intValue), UriKind.RelativeOrAbsolute))
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
    Public Class ColumnPriorityConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing OrElse Not(TypeOf value Is TaskPriority) Then
                Return Nothing
            End If
            Dim priority As TaskPriority = DirectCast(value, TaskPriority)
            If priority = TaskPriority.Medium Then
                Return Nothing
            End If
            Return New BitmapImage(New Uri(String.Format("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Priority_{0}.png", priority), UriKind.RelativeOrAbsolute))
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
    Public Class ColumnCategoryImageConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing OrElse Not(TypeOf value Is TaskCategory) Then
                Return Nothing
            End If
            Dim category As TaskCategory = DirectCast(value, TaskCategory)
            Return String.Format("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Category_{0}.png", category)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
    Public Class ColumnFlagStatusImageConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing OrElse Not(TypeOf value Is FlagStatus) Then
                Return Nothing
            End If
            Dim status As FlagStatus = DirectCast(value, FlagStatus)
            Return String.Format("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/{0}_Flag.png", status)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
    Public Class TaskStatusCompletedConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing OrElse Not(TypeOf value Is TaskStatus) Then
                Return False
            End If
            Dim status As TaskStatus = DirectCast(value, TaskStatus)
            Return status = TaskStatus.Completed
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
    Public Class BoolToDecorationsConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing OrElse Not(TypeOf value Is Boolean) Then
                Return Nothing
            End If
            Dim b As Boolean = DirectCast(value, Boolean)
            Return If((Not b), Nothing, TextDecorations.Strikethrough)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
    Public Class SplitStringConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return If(value Is Nothing, Nothing, SplitStringHelper.SplitPascalCaseString(value.ToString()))
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class

    Public Class FieldNameToTemplateConverter
        Inherits BindableBase
        Implements IValueConverter

        Private Shared cache As New Dictionary(Of String, Object)()


        Private targetTemplateName_Renamed As String = Nothing
        Public Property TargetTemplateName() As String
            Get
                Return targetTemplateName_Renamed
            End Get
            Set(ByVal value As String)
                targetTemplateName_Renamed = value
                RaisePropertiesChanged("TargetTemplateName")
            End Set
        End Property

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim column As GridColumnBase = TryCast(value, GridColumnBase)
            If column Is Nothing OrElse String.IsNullOrWhiteSpace(column.FieldName) Then
                Return Nothing
            End If
            Dim fullTemplateName As String = String.Format("{0}_{1}", column.FieldName, TargetTemplateName)
            If Not cache.ContainsKey(fullTemplateName) Then
                cache.Add(fullTemplateName, column.TryFindResource(fullTemplateName))
            End If
            Return cache(fullTemplateName)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
    Public Class PhoneFormatConverter
        Implements IValueConverter

        #Region "IValueConverter Members"

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return String.Empty
            End If
            Dim phoneNumber As String = DirectCast(value, String)
            If phoneNumber.Length = 0 Then
                Return phoneNumber
            End If
            Return String.Format("{0} {1}", phoneNumber.Substring(0, 5), phoneNumber.Substring(5))
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        #End Region
    End Class
    Public Class MaskToTextEditSettingsConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim mask As String = TryCast(value, String)
            If value Is Nothing Then
                Return Nothing
            End If
            Return New TextEditSettings With {.Mask = mask, .MaskUseAsDisplayFormat = True, .MaskType = DevExpress.Xpf.Editors.MaskType.RegEx}
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
    Public Class AddressDetailPanelConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim address As Address = TryCast(value, Address)
            If address Is Nothing Then
                Return Nothing
            End If
            Return address.GetAddressFullString(" | ")
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
    Public Class FocusedRowHandleToActiveRecordConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim view As GridViewBase = TryCast(parameter, GridViewBase)
            If value Is Nothing OrElse value.GetType() IsNot GetType(Integer) OrElse view Is Nothing Then
                Return -1
            End If
            Dim focusedRowHandle As Integer = DirectCast(value, Integer)
            Return view.Grid.GetListIndexByRowHandle(focusedRowHandle)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim view As GridViewBase = TryCast(parameter, GridViewBase)
            If value Is Nothing OrElse value.GetType() IsNot GetType(Integer) OrElse view Is Nothing Then
                Return -1
            End If
            Dim activeRecord As Integer = DirectCast(value, Integer)
            Return view.Grid.GetRowHandleByListIndex(activeRecord)
        End Function
    End Class
End Namespace
