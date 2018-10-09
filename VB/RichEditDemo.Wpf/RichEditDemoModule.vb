Imports System.Linq
Imports System.Windows
Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.Xpf.Utils

Namespace RichEditDemo
    Public Class RichEditDemoModule
        Inherits DemoModule

        Private Shared ReadOnly RichEditControlPropertyKey As DependencyPropertyKey
        Public Shared ReadOnly RichEditControlProperty As DependencyProperty

        Shared Sub New()
            NWindContext.Preload()
            RichEditControlPropertyKey = DependencyPropertyManager.RegisterReadOnly("RichEditControl", GetType(RichEditControl), GetType(RichEditDemoModule), New FrameworkPropertyMetadata(Nothing))
            RichEditControlProperty = RichEditControlPropertyKey.DependencyProperty
        End Sub

        Public Property RichEditControl() As RichEditControl
            Get
                Return CType(GetValue(RichEditControlProperty), RichEditControl)
            End Get
            Private Set(ByVal value As RichEditControl)
                SetValue(RichEditControlPropertyKey, value)
            End Set
        End Property

        Private Sub OnRichEditControlLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SetFocus(RichEditControl)
        End Sub
        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            RichEditControl = If(TryCast(Content, RichEditControl), LayoutTreeHelper.GetVisualChildren(CType(Content, DependencyObject)).OfType(Of RichEditControl)().FirstOrDefault())
            If RichEditControl IsNot Nothing Then
                AddHandler RichEditControl.Loaded, AddressOf OnRichEditControlLoaded
                CType(New RichEditDemoExceptionsHandler(RichEditControl), RichEditDemoExceptionsHandler).Install()
                SetBehaviorOptions()
            End If
        End Sub
        Private Sub SetBehaviorOptions()
            RichEditControl.BehaviorOptions.FontSource = DevExpress.XtraRichEdit.RichEditBaseValueSource.Document
            RichEditControl.BehaviorOptions.ForeColorSource = DevExpress.XtraRichEdit.RichEditBaseValueSource.Document
        End Sub
        Protected Friend Overridable Sub SetFocus(ByVal control As RichEditControl)
            If control Is Nothing Then
                Return
            End If
            If control.KeyCodeConverter IsNot Nothing Then
                control.KeyCodeConverter.Focus()
            End If
        End Sub
        Protected Overrides Sub ShowPopupContent()
            MyBase.ShowPopupContent()
            If RichEditControl IsNot Nothing Then
                RichEditControl.ShowHoverMenu = True
            End If
        End Sub
        Protected Overrides Sub HidePopupContent()
            If RichEditControl IsNot Nothing Then
                RichEditControl.ShowHoverMenu = False
            End If
            MyBase.HidePopupContent()
        End Sub
    End Class
End Namespace
