Imports System
Imports System.Collections
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports DevExpress.Mvvm.UI
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Editors

Namespace DiagramDemo
    Public Interface IScrollToEndService
        Sub ScrollToEnd()
    End Interface
    Public Class ScrollToEndService
        Inherits ServiceBase
        Implements IScrollToEndService

        Public Shared ReadOnly ScrollViewerProperty As DependencyProperty = DependencyProperty.Register("ScrollViewer", GetType(ScrollViewer), GetType(ScrollToEndService), New PropertyMetadata(Nothing))
        Public Property ScrollViewer() As ScrollViewer
            Get
                Return CType(GetValue(ScrollViewerProperty), ScrollViewer)
            End Get
            Set(ByVal value As ScrollViewer)
                SetValue(ScrollViewerProperty, value)
            End Set
        End Property
        Private ReadOnly Property ActualScrollViewer() As ScrollViewer
            Get
                Return If(ScrollViewer, TryCast(AssociatedObject, ScrollViewer))
            End Get
        End Property

        Private Sub IScrollToEndService_ScrollToEnd() Implements IScrollToEndService.ScrollToEnd

            Dim scrollViewer_Renamed = ActualScrollViewer
            If scrollViewer_Renamed Is Nothing Then
                Return
            End If
            scrollViewer_Renamed.ScrollToEnd()
        End Sub
    End Class
End Namespace
