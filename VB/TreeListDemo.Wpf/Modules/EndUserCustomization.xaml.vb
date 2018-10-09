Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.Xpf.Grid
Imports System.Windows.Data
Imports DevExpress.Xpf.Editors
Imports System.Xml.Serialization
Imports System.Collections
Imports System.Windows.Media.Imaging
Imports System.Windows.Media
Imports DevExpress.Xpf.DemoBase.DataClasses

Namespace TreeListDemo



    Partial Public Class EndUserCustomization
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub lbSummary_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If lbSummary.SelectedIndex = 0 Then
                TreeListControl.View.ShowTotalSummary = True
                TreeListControl.View.ShowFixedTotalSummary = False
            Else
                TreeListControl.View.ShowTotalSummary = False
                TreeListControl.View.ShowFixedTotalSummary = True
            End If
        End Sub
    End Class
    Public Class EmployeeCategoryImageSelector
        Inherits TreeListNodeImageSelector
        Implements IValueConverter

        Shared Sub New()
            ImageCache = New Dictionary(Of String, System.Windows.Media.ImageSource)()
        End Sub
        Private Shared ImageCache As Dictionary(Of String, System.Windows.Media.ImageSource)
        Public Overrides Function [Select](ByVal rowData As DevExpress.Xpf.Grid.TreeList.TreeListRowData) As System.Windows.Media.ImageSource
            Dim empl As Employee = (TryCast(rowData.Row, Employee))
            If empl Is Nothing Then
                Return Nothing
            End If
            Return GetImageByGroupName(empl.GroupName)
        End Function

        Public Shared Function GetImageByGroupName(ByVal groupName As String) As ImageSource
            If groupName Is Nothing Then
                Return Nothing
            End If
            If ImageCache.ContainsKey(groupName) Then
                Return ImageCache(groupName)
            End If
            Dim image As System.Windows.Media.ImageSource = New BitmapImage(New Uri(GetImagePathByGroupName(groupName), UriKind.Relative))
            ImageCache.Add(groupName, image)
            Return image
        End Function

        Public Shared images As New List(Of String)() From {"administration", "inventory", "manufacturing", "quality", "research", "sales"}
        Public Shared Function GetImagePathByGroupName(ByVal groupName As String) As String
            groupName = groupName.ToLower()
            For Each item As String In images
                If groupName.Contains(item) Then
                    Return "/TreeListDemo;component/Images/Categories/" & item & ".png"
                End If
            Next item
            Return groupName
        End Function

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return GetImagePathByGroupName(DirectCast(value, String))
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
    #Region "Converters"
    Public Class NavigationStyleToIsEnabledConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return True
            End If
            Return (DirectCast(value, GridViewNavigationStyle)) <> GridViewNavigationStyle.Row
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
    Public Class HeaderToImageConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return Nothing
            End If
            Return "/TreeListDemo;component/Images/HeaderIcons/" & DirectCast(value, String).Replace(" ", String.Empty) & ".png"
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    #End Region
End Namespace
