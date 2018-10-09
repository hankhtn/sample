Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace DialogsDemo
    Public Class CustomHeaderItemModel
        Inherits BaseHeaderItemModel

        Protected Overridable ReadOnly Property MessageBoxService() As IMessageBoxService
            Get
                Return Nothing
            End Get
        End Property
        Public Property Content() As String

        Public Sub Click()
            MessageBoxService.Show(String.Format("{0} command executed!", Content), "Demo", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK)
        End Sub
        Public Sub New(ByVal index As Integer)
            ResourceKey = "CustomHeaderItem"
            Content = "Item " & index
        End Sub
    End Class
End Namespace
