Imports System.ComponentModel
Imports System.Windows.Data
Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.DemoBase

Namespace PrintingDemo
    Public Class ModuleBase
        Inherits DemoModule

        Shared Sub New()
            NWindContext.Preload()
        End Sub
        Public Sub New()
            Interaction.GetBehaviors(Me).Add(CreateDispatchFocusBehavior())
            AddHandler Loaded, AddressOf ModuleBase_Loaded
        End Sub

        Private Sub ModuleBase_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
'            #Region "DEMO_REMOVE"
            If Not System.ComponentModel.DesignerProperties.GetIsInDesignMode(Me) Then
                Dim viewer = DevExpress.Xpf.Core.Native.LayoutHelper.FindElementByType(Of DevExpress.Xpf.Printing.DocumentPreviewControl)(Me)
                System.Diagnostics.Debug.Assert(viewer IsNot Nothing AndAlso viewer.Name = "viewer", "Can not find a DocumentPreviewControl with the name 'viewer'.")
            End If
'            #End Region

            If (Not DesignerProperties.GetIsInDesignMode(Me)) AndAlso ViewModel IsNot Nothing Then
                ViewModel.CreateDocument()
            End If
        End Sub

        Public ReadOnly Property ViewModel() As ModuleViewModelBase
            Get
                Return CType(DataContext, ModuleViewModelBase)
            End Get
        End Property

        Protected Overrides Sub Clear()
            MyBase.Clear()
            If ViewModel IsNot Nothing Then
                ViewModel.ClearDocument()
            End If
        End Sub

        Protected Overridable ReadOnly Property NeedChangeEditorsTheme() As Boolean
            Get
                Return False
            End Get
        End Property

        Private Shared Function CreateDispatchFocusBehavior() As DispatchFocusBehavior
            Dim behavior = New DispatchFocusBehavior()
            BindingOperations.SetBinding(behavior, DispatchFocusBehavior.ElementProperty, New Binding() With {.ElementName = "viewer"})
            Return behavior
        End Function
    End Class
End Namespace
