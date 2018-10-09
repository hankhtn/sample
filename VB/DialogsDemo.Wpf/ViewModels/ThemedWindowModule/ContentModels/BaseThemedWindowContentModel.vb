Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Mvvm.POCO

Namespace DialogsDemo
    Public MustInherit Class BaseThemedWindowContentModel
        Public ReadOnly Property RootViewModel() As ThemedWindowViewModel
            Get
                Return Me.GetParentViewModel(Of ThemedWindowViewModel)()
            End Get
        End Property
    End Class
End Namespace
