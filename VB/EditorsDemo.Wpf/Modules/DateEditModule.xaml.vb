Imports System.Collections.Generic
Imports DevExpress.Xpf.DemoBase

Namespace EditorsDemo
    <CodeFile("ViewModels/OutlookViewModel.(cs)")>
    Partial Public Class DateEditModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            InitSources()
            DataContext = New OutlookViewModel()
        End Sub
        Private Sub InitSources()
            Dim dateMasks As New List(Of MaskWrapper)()
            dateMasks.Add(New MaskWrapper() With {.MaskName = "Short Date", .MaskString = "d"})
            dateMasks.Add(New MaskWrapper() With {.MaskName = "Long Date", .MaskString = "D"})
            dateMasks.Add(New MaskWrapper() With {.MaskName = "Month & Day", .MaskString = "m"})
            dateMasks.Add(New MaskWrapper() With {.MaskName = "Year & Month", .MaskString = "y"})
            dateMask.ItemsSource = dateMasks
        End Sub
        Public Class MaskWrapper
            Public Property MaskName() As String
            Public Property MaskString() As String
        End Class
    End Class
End Namespace
