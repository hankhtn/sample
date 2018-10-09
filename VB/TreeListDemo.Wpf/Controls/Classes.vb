Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Xpf.Grid
Imports System.Windows.Data
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Grid.TreeList
Imports System.Windows.Media
Imports DevExpress.Xpf.Editors
Imports System.Collections.ObjectModel
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports DevExpress.Xpf.Utils
Imports DevExpress.Xpf.Core
Imports System.Collections


Namespace TreeListDemo
    Public Class NavigationStyleList
        Inherits List(Of GridViewNavigationStyle)

    End Class
    #Region "Converters"
    Public Class ShowTreeListLinesConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return Not Object.Equals(value, TreeListLineStyle.None)
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return If(DirectCast(value, Boolean), TreeListLineStyle.Solid, TreeListLineStyle.None)
        End Function
        #End Region
    End Class

    Public Class AlertVisibilityConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return If(DirectCast(value, Boolean), Visibility.Visible, Visibility.Collapsed)
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class PriorityTemplateSelector
        Implements IValueConverter

        Public Property HighTemplate() As DataTemplate
        Public Property NormalTemplate() As DataTemplate
        Public Property LowTemplate() As DataTemplate

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim rowData As TreeListRowData = TryCast(value, TreeListRowData)
            If rowData IsNot Nothing Then

                Dim task_Renamed As Task = TryCast(rowData.Row, Task)
                If task_Renamed IsNot Nothing Then
                    Select Case task_Renamed.Priority
                        Case Priority.Low
                            Return LowTemplate
                        Case Priority.Normal
                            Return NormalTemplate
                        Case Priority.High
                            Return HighTemplate
                    End Select
                End If
            End If
            Return Nothing
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class ObjectIsTaskConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return TypeOf value Is Task
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class SummaryIconVisibilityConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return Nothing
            End If
            If value.GetType() Is GetType(TaskObject) Then
                Return Visibility.Collapsed
            Else
                Return Visibility.Visible
            End If
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
    Public Class CountryToFlagImageConverter
        Inherits BytesToImageSourceConverter

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object
            For Each item As Country In CountriesData.DataSource
                If item.Name = DirectCast(value, String) Then
                    Return MyBase.Convert(item.Flag, targetType, parameter, culture)
                End If
            Next item
            Return Nothing
        End Function
    End Class
    #End Region

    Public NotInheritable Class DragDropSourceGenerator

        Private Sub New()
        End Sub

        Public Shared Function GetActiveEmployees() As IEnumerable(Of Employee)
            Return GetEmployees(False)
        End Function
        Public Shared Function GetNewEmployees() As IEnumerable(Of Employee)
            Return GetEmployees(True)
        End Function
        Private Shared Function GetEmployees(ByVal isNew As Boolean) As IEnumerable(Of Employee)
            Dim newIds As New HashSet(Of Integer)() From {106, 002, 176, 128, 278, 231, 198, 201, 272}
            Return EmployeesData.DataSource.Where(Function(x) (Not(newIds.Contains(x.Id) Xor isNew)))
        End Function
    End Class

    Public Class TaskContentControl
        Inherits ContentControl

        Public Shared ReadOnly ShowBorderProperty As DependencyProperty = DependencyPropertyManager.Register("ShowBorder", GetType(Boolean), GetType(TaskContentControl), New PropertyMetadata(False))
        Public Sub New()
            Me.SetDefaultStyleKey(GetType(TaskContentControl))
        End Sub
        Public Property ShowBorder() As Boolean
            Get
                Return DirectCast(GetValue(ShowBorderProperty), Boolean)
            End Get
            Set(ByVal value As Boolean)
                SetValue(ShowBorderProperty, value)
            End Set
        End Property
    End Class
    Public Class IEnumerableToFirstItemConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return value
            End If
            Dim enumerator As IEnumerator = DirectCast(value, IEnumerable).GetEnumerator()
            enumerator.MoveNext()
            Dim result As Object = enumerator.Current
            enumerator.Reset()
            Return result
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
