Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Navigation
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace DevExpress.DevAV
    Public Class FilterItemClickBehavior
        Inherits Behavior(Of TileBarItem)

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler Me.AssociatedObject.Click, AddressOf AssociatedObject_Click
        End Sub
        Private Sub AssociatedObject_Click(ByVal sender As Object, ByVal e As EventArgs)

            Dim filterItem_Renamed = TryCast(AssociatedObject.DataContext, FilterItem)
            If filterItem_Renamed Is Nothing OrElse filterItem_Renamed.Name Is Nothing Then
                Return
            End If
            Xpf.DemoBase.Helpers.Logger.Log(String.Format("OutlookInspiredApp: Change Filter: {0}", filterItem_Renamed.Name))
        End Sub
        Protected Overrides Sub OnDetaching()
            RemoveHandler Me.AssociatedObject.Click, AddressOf AssociatedObject_Click
            MyBase.OnDetaching()
        End Sub
    End Class
End Namespace
