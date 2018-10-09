Imports DevExpress.Data.Filtering
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.UI
Imports DevExpress.Utils
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.ConditionalFormatting
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Media

Namespace GridDemo
    Public MustInherit Class BaseValueConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public MustOverride Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
        Public Overridable Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        Public NotOverridable Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class
    Public Class MultiSelectModeConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return Not Object.Equals(DirectCast(value, MultiSelectMode), MultiSelectMode.None)
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return If(DirectCast(value, Boolean), MultiSelectMode.Row, MultiSelectMode.None)
        End Function
    End Class
    Public Class FocusedToColorConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Sub New()
        End Sub
        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If (DirectCast(parameter, String)) = System.Enum.GetName(GetType(FocusedGrid), DirectCast(value, FocusedGrid)) Then
                Return New SolidColorBrush(Color.FromArgb(50, 200, 0, 0))
            End If
            Return New SolidColorBrush(Color.FromArgb(0, 0, 0, 0))
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        #End Region
    End Class
    Public Class PastUnderFocusedRowToSelectedIndexConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Sub New()
        End Sub
        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return If(DirectCast(value, Boolean), 0, 1)
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return If(DirectCast(value, Integer) = 0, True, False)
        End Function
        #End Region
    End Class

    Public Class BooleanToDefaultBooleanConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return If(DirectCast(value, Boolean), DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
    Public Class ScrollingAnimationDurationToBooleanConverterExtension
        Inherits BaseValueConverter

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            Return System.Convert.ToDouble(value) > 0
        End Function
        Public Overrides Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            Return If(System.Convert.ToBoolean(value), 350, 0)
        End Function
    End Class
    Public Class ShowSearchPanelModeToTextConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim showSearchPanelModeq? As ShowSearchPanelMode = DirectCast(value, ShowSearchPanelMode?)
            If showSearchPanelModeq Is Nothing Then
                Return value
            End If
            Dim showSearchPanelMode As ShowSearchPanelMode = showSearchPanelModeq.Value
            Select Case showSearchPanelMode
                Case ShowSearchPanelMode.Default
                    Return "Default"
                Case ShowSearchPanelMode.Always
                    Return "Always"
                Case ShowSearchPanelMode.Never
                    Return "Never"
            End Select
            Return value
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
    Public Class FindModeToTextConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim findModeq? As FindMode = DirectCast(value, FindMode?)
            If findModeq Is Nothing Then
                Return value
            End If
            Dim findMode As FindMode = findModeq.Value
            Select Case findMode
                Case FindMode.Always
                    Return "Always"
                Case FindMode.FindClick
                    Return "Find on Click"
            End Select
            Return value
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class DefaultBooleanToNulltableConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return DirectCast(value, Integer) <> CInt((DefaultBoolean.False))
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            If Not(TypeOf value Is Boolean) Then
                Return DefaultBoolean.Default
            End If
            Return If(DirectCast(value, Boolean), DefaultBoolean.True, DefaultBoolean.False)
        End Function
    End Class


    Public Class SearchPanelModeConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return value
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return DirectCast(value, ListBoxEditItem).Content
        End Function
    End Class
    Public Class SearchPanelFindButtonEnabledConverter
        Implements IMultiValueConverter

        Public Function Convert(ByVal values() As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IMultiValueConverter.Convert
            If values Is Nothing OrElse values.Length <> 2 Then
                Return False
            End If
            Return (Not DirectCast(values(0), Boolean)) AndAlso DirectCast(values(1), Integer) = 1
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetTypes() As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
    Public Class BoolToScrollBarSearchAnnotationConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return If(DirectCast(value, Boolean), ScrollBarAnnotationMode.SearchResult, ScrollBarAnnotationMode.None)
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
    Public Class GroupNameToImageConverterExtension
        Inherits BaseValueConverter

        Public Shared images As New List(Of String)() From {"administration", "inventory", "manufacturing", "quality", "research", "sales"}
        Public Shared Function GetImagePathByGroupName(ByVal groupName As String) As String
            groupName = groupName.ToLower()
            For Each item As String In images
                If groupName.Contains(item) Then
                    Return "pack://application:,,,/GridDemo;component/Images/GroupName/" & item & ".png"
                End If
            Next item
            Return groupName
        End Function
        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            If value Is Nothing Then
                Return Nothing
            End If
            Return GetImagePathByGroupName(DirectCast(value, String))
        End Function
    End Class
    Public Class DemoHeaderImageExtension
        Inherits MarkupExtension

        Private ReadOnly imageName As String

        Public Sub New(ByVal imageName As String)
            Me.imageName = imageName
        End Sub

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return New System.Windows.Media.Imaging.BitmapImage(New Uri("pack://application:,,,/GridDemo;component/Images/HeaderImages/" & imageName.Replace(" ", String.Empty) & ".png"))
        End Function
    End Class
    Public Class ColumnHeaderTextConverter
        Implements IValueConverter

        Public Property ColumnName() As String
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return Nothing
            End If
            Return If(DirectCast(value, String).Replace(" ", "") = ColumnName, "Bold", "Normal")
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
    Public Class BirthdayImageVisibilityConverterExtension
        Inherits BaseValueConverter

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            If value Is Nothing OrElse Not(TypeOf value Is Date) Then
                Return Visibility.Collapsed
            End If
            Dim birthDate As Date = DirectCast(value, Date)
            Dim someDate As Date = Date.Now.AddMonths(3)
            Dim someMonth As Integer = If(someDate.Month < 3, someDate.Month + 12, someDate.Month)
            Dim birthMonth As Integer = If(birthDate.Month < 3, birthDate.Month + 12, birthDate.Month)
            Return If(birthMonth >= Date.Now.Month AndAlso birthMonth <= someMonth AndAlso (If(birthDate.Month = Date.Now.Month, birthDate.Day > Date.Now.Day, True)), Visibility.Visible, Visibility.Collapsed)
        End Function
    End Class

    Public Class CountToVisibilityConverter
        Implements IValueConverter

        Public Property Invert() As Boolean
        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return If((DirectCast(value, Integer) > 0) Xor Invert, Visibility.Visible, Visibility.Collapsed)
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        #End Region
    End Class
    Public Class IntToDoubleConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return Convert.ToDouble(value)
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Convert.ToInt32(value)
        End Function
        #End Region
    End Class
    Public Class PriorityToImageConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return New Image() With {.Source = DirectCast(value, EnumMemberInfo).Image}
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class
    Public Class EditFormShowModeConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return False
            End If
            If Not System.Enum.IsDefined(GetType(EditFormShowMode), value) Then
                Return False
            End If
            Dim mode As EditFormShowMode = DirectCast(value, EditFormShowMode)
            Return Not mode.Equals(EditFormShowMode.None)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class IncrementalSearchModeToBoolConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If Not(TypeOf value Is IncrementalSearchMode) Then
                Return False
            End If
            If (DirectCast(value, IncrementalSearchMode)) = IncrementalSearchMode.Enabled Then
                Return True
            End If
            Return False
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            If Not(TypeOf value Is Boolean) Then
                Return IncrementalSearchMode.Disabled
            End If
            If DirectCast(value, Boolean) Then
                Return IncrementalSearchMode.Enabled
            End If
            Return IncrementalSearchMode.Disabled
        End Function
    End Class
    Public Class Int32Extension
        Inherits MarkupExtension

        Private ReadOnly value As Integer
        Public Sub New(ByVal value As Integer)
            Me.value = value
        End Sub
        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return value
        End Function
    End Class
    Public Class TabItemEventArgsToDataConverter
        Inherits EventArgsConverterBase(Of TabControlTabRemovedEventArgs)

        Protected Overrides Function Convert(ByVal sender As Object, ByVal args As TabControlTabRemovedEventArgs) As Object
            Return CType(args.Item, DXTabItem).DataContext
        End Function
    End Class
    Public Class FormatTypeConverter
        Inherits BaseValueConverter

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            If Not(TypeOf value Is FormatType) Then
                Return Nothing
            End If
            Select Case DirectCast(value, FormatType)
                Case FormatType.DateTimeToday
                    Return "YellowFillWithDarkYellowText"
                Case FormatType.TopPercent
                    Return "GreenFillWithDarkGreenText"
                Case Else
                    Return "LightRedFillWithDarkRedText"
            End Select
        End Function
    End Class
    Public Class ConditionTypeConverter
        Inherits BaseValueConverter

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            If Not(TypeOf value Is ConditionType) Then
                Return ConditionRule.None
            End If
            Select Case DirectCast(value, ConditionType)
                Case ConditionType.Greater
                    Return ConditionRule.Greater
                Case Else
                    Return ConditionRule.Less
            End Select
        End Function
    End Class
End Namespace
