Imports System
Imports System.Linq
Imports System.Windows
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo

    Public Class FieldAreaHelper
        Inherits DependencyObject

        Public Shared ReadOnly FixAreasProperty As DependencyProperty

        Shared Sub New()
            FixAreasProperty = DependencyProperty.RegisterAttached("FixAreas", GetType(Boolean), GetType(FieldAreaHelper), New PropertyMetadata(AddressOf OnFixAreasPropertyChanged))
        End Sub

        Public Shared Function GetFixAreas(ByVal element As DependencyObject) As Boolean
            If Not(TypeOf element Is PivotGridControl) Then
                Throw New ArgumentNullException("element")
            End If
            Return CBool(CType(element, PivotGridControl).GetValue(FixAreasProperty))
        End Function

        Public Shared Sub SetFixAreas(ByVal element As DependencyObject, ByVal value As Boolean)
            If Not(TypeOf element Is PivotGridControl) Then
                Throw New ArgumentNullException("element")
            End If
            element.SetValue(FixAreasProperty, value)
        End Sub

        Private Shared Sub OnFixAreasPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim pivot As PivotGridControl = TryCast(d, PivotGridControl)
            If pivot Is Nothing Then
                Return
            End If
            AddHandler pivot.FieldAreaChanging, AddressOf OnPivotFieldAreaChanging
        End Sub

        Private Shared Sub OnPivotFieldAreaChanging(ByVal sender As Object, ByVal e As PivotFieldAreaChangingEventArgs)
            Dim field As PivotGridField = e.Field
            If field Is Nothing OrElse Not(TypeOf field.Parent Is PivotGridControl) OrElse CType(field.Parent, PivotGridControl).OlapConnectionString IsNot Nothing OrElse field.UnboundType <> FieldUnboundColumnType.Bound Then
                Return
            End If
            If field.Area = FieldArea.DataArea Then
                If e.NewArea <> FieldArea.DataArea Then
                    e.Allow = False
                End If
            Else
                If e.NewArea = FieldArea.DataArea Then
                    e.Allow = False
                End If
            End If
        End Sub
    End Class
End Namespace
