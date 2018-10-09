Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports DevExpress.Utils
Imports DevExpress.Xpf.Carousel
Imports DevExpress.Xpf.DemoBase

Namespace CarouselDemo
    Public Class ResourcesTable
        Public Const ImagesPath As String = "Data/Images/"
        Public Const ImagesPrefix As String = "/CarouselDemo;component/"

        Shared Sub New()
            UriTable = GetImageNames(ImagesPath)
        End Sub

        Private Shared Function GetImageNames(ByVal path As String) As String()
            Dim asm As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
            Dim enumerator As IDictionaryEnumerator = AssemblyHelper.GetResourcesEnumerator(asm)
            Dim imageNames As New List(Of String)()
            Do While enumerator.MoveNext()
                Dim resourceName As String = DirectCast(enumerator.Key, String)
                If resourceName.StartsWith(path, StringComparison.OrdinalIgnoreCase) Then
                    If resourceName.Contains("logo-back.png") Then
                        Continue Do
                    End If
                    imageNames.Add(ImagesPrefix & resourceName)
                End If
            Loop
            imageNames.Sort()
            Return imageNames.ToArray()
        End Function
        Public Shared UriTable() As String

        Public Shared Function GetUri(ByVal name As String) As Uri
            Dim result As String = Array.Find(Of String)(ResourcesTable.UriTable, Function(s) s.EndsWith(name, StringComparison.OrdinalIgnoreCase))
            Return New Uri(result, UriKind.Relative)
        End Function
    End Class
    Public Class CarouselDemoModule
        Inherits DemoModule

        Shared Sub New()
            Dim ownerType As Type = GetType(CarouselDemoModule)
        End Sub
        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            CommandManager.InvalidateRequerySuggested()
        End Sub
        Protected Overridable Function CreateItems(ByVal path As String, ByVal it As ItemType) As List(Of FrameworkElement)
            Dim contentLoadHelper As New ContentLoadHelper()
            contentLoadHelper.Path = path
            Dim itemList = New List(Of FrameworkElement)(contentLoadHelper.LoadItems(it).ToArray())
            For i As Integer = 0 To itemList.Count - 1
                itemList(i).Name = "Item" & i.ToString()
                CType(itemList(i), Image).Stretch = System.Windows.Media.Stretch.Fill
            Next i
            Return itemList
        End Function
        Protected Overridable Sub AddItems(ByVal path As String, ByVal it As ItemType, ByVal carousel As CarouselPanel)
            Dim itemList = CreateItems(path, it)
            For Each item In itemList
                item.Name = "item" & carousel.Children.Count
                carousel.Children.Add(item)
            Next item
        End Sub
    End Class
End Namespace
