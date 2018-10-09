Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace DialogsDemo
    Public Class WindowTitleEditorHeaderItemModel
        Inherits BaseHeaderItemModel

        Public Overridable Property Title() As String

        Public Sub New()
            ResourceKey = "WindowTitleEditor"
        End Sub

        Protected Sub OnTitleChanged()
            BaseModel.Title = Title
        End Sub
    End Class
End Namespace
