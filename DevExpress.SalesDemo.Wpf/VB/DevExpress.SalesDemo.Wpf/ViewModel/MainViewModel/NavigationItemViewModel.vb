Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.SalesDemo.Wpf.Helpers
Imports System
Imports System.Windows.Media.Imaging
Imports DevExpress.Mvvm.ModuleInjection

Namespace DevExpress.SalesDemo.Wpf.ViewModel
    Public Class NavigationItemViewModel
        Public Shared Function Create(ByVal moduleType As String, ByVal caption As String, ByVal description As String, ByVal glyph As BitmapImage) As NavigationItemViewModel
            Dim res = ViewModelSource.Create(Function() New NavigationItemViewModel())
            res.moduleType = moduleType
            res.Caption = caption
            res.Description = description
            res.Glyph = glyph
            Return res
        End Function
        Protected Sub New()
            If Me.IsInDesignMode() Then
                InitializeInDesignMode()
            End If
            AddHandler ModuleManager.DefaultManager.GetEvents(Me).Navigated, Sub(s, e) IsActive = True
            AddHandler ModuleManager.DefaultManager.GetEvents(Me).NavigatedAway, Sub(s, e) IsActive = False
        End Sub
        Private Sub InitializeInDesignMode()
            Caption = "Sales"
            Description = "Revenue" & Environment.NewLine & "Snapshots"
            Glyph = ResourceImageHelper.GetResourceImage("Sales.png")
        End Sub

        Private moduleType As String
        Public Overridable Property Caption() As String
        Public Overridable Property Description() As String
        Public Overridable Property Glyph() As BitmapImage
        Public Overridable Property IsActive() As Boolean

        Public Sub Activate()
            If IsActive Then
                Return
            End If
            ModuleManager.DefaultManager.Navigate(Regions.Navigation, moduleType)
        End Sub
        Protected Sub OnIsActiveChanged()
            If IsActive Then
                ModuleManager.DefaultManager.Navigate(Regions.Main, moduleType)
            End If
        End Sub
    End Class
End Namespace
