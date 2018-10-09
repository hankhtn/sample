Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Native

Namespace LayoutControlDemo
    Partial Public Class controlCarInfo
        Inherits UserControl

        Private _Owner As FrameworkElement

        Public Sub New()
            Me.New(False, False, False)
        End Sub
        Public Sub New(ByVal showBackground As Boolean, ByVal showBorder As Boolean, ByVal showDetails As Boolean)
            InitializeComponent()
            If Not showBackground Then
                LayoutRoot.Background = New SolidColorBrush(Colors.Transparent)
            End If
            If Not showBorder Then
                layoutBase.Padding = New Thickness(0)
            End If
            If Not showDetails Then
                For Each element As FrameworkElement In layoutBase.GetVisibleChildren()
                    If DirectCast(element.Tag, String) = "DetailInfo" Then
                        element.SetVisible(False)
                    End If
                Next element
            End If
        End Sub

        Public ReadOnly Property ContentOffset() As Point
            Get
                Return layoutBase.MapPoint(layoutBase.ContentBounds.Location(), Me)
            End Get
        End Property
        Public Property Owner() As FrameworkElement
            Get
                Return _Owner
            End Get
            Set(ByVal value As FrameworkElement)
                If Owner Is value Then
                    Return
                End If
                _Owner = value
                LayoutRoot.IsHitTestVisible = Owner Is Nothing
            End Set
        End Property

        Protected Overrides Function HitTestCore(ByVal hitTestParameters As PointHitTestParameters) As HitTestResult
            If Owner Is Nothing Then
                Return MyBase.HitTestCore(hitTestParameters)
            Else
                If Owner.IsInVisualTree() AndAlso Owner.Contains(Me.MapPoint(hitTestParameters.HitPoint, Nothing)) Then
                    Return New PointHitTestResult(Owner, TranslatePoint(hitTestParameters.HitPoint, Owner))
                Else
                    Return Nothing
                End If
            End If
        End Function
    End Class
End Namespace
