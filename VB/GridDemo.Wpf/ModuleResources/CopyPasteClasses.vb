Imports DevExpress.Xpf.Grid
Imports System
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Animation

Namespace GridDemo
    Public Enum FocusedGrid
        First
        Second
        None
    End Enum
    Public Class RowsAnimationElement
        Inherits FrameworkContentElement

        Public Shared ReadOnly NewRowsProgressProperty As DependencyProperty
        Public Shared ReadOnly NewRowsColorProperty As DependencyProperty
        Shared Sub New()
            NewRowsProgressProperty = DependencyProperty.Register("NewRowsProgress", GetType(Double), GetType(RowsAnimationElement), New PropertyMetadata(1R))
            NewRowsColorProperty = DependencyProperty.Register("NewRowsColor", GetType(Color), GetType(RowsAnimationElement), New PropertyMetadata(Color.FromArgb(&H0, &HDE, &HF8, &HCB)))
        End Sub
        Public Property NewRowsProgress() As Double
            Get
                Return DirectCast(GetValue(NewRowsProgressProperty), Double)
            End Get
            Set(ByVal value As Double)
                SetValue(NewRowsProgressProperty, value)
            End Set
        End Property
        Public Property NewRowsColor() As Color
            Get
                Return DirectCast(GetValue(NewRowsColorProperty), Color)
            End Get
            Set(ByVal value As Color)
                SetValue(NewRowsColorProperty, value)
            End Set
        End Property
    End Class
    <Serializable>
    Public Class CopyPasteOutlookData
        Public Property UniqueID() As Integer
        Public Property OID() As Integer?
        Public Property [From]() As String
        Public Property Sent() As Date?
        Public Property HasAttachment() As Boolean?
        Public Property HoursActive() As Double?

        Public Shared Function ConvertOutlookDataToCopyPasteOutlookData(ByVal outlookDataObject As OutlookData, ByVal owner As CopyPasteOperations) As CopyPasteOutlookData
            owner.Counter += 1
            Return New CopyPasteOutlookData() With {.UniqueID = owner.Counter, .From = outlookDataObject.From, .HasAttachment = outlookDataObject.HasAttachment, .HoursActive = outlookDataObject.HoursActive, .OID = outlookDataObject.OID, .Sent = outlookDataObject.Sent}
        End Function
    End Class
    Friend Class PasteCompetedHelper
        Public Property Owner() As CopyPasteOperations
        Public Property ColorStoryboard() As Storyboard
        Public Sub ColorStoryboardCompleted(ByVal sender As Object, ByVal e As EventArgs)
            RemoveHandler ColorStoryboard.Completed, AddressOf ColorStoryboardCompleted
            Owner.RaisePasteCompetedEvent(New RoutedEventArgs(CopyPasteOperations.PasteCompetedEvent))
        End Sub
    End Class
    Friend Class PasteHelper
        Public Property View() As GridViewBase
        Public Property List() As BindingList(Of CopyPasteOutlookData)
        Public Property PositionNewRow() As Integer
        Public Property ObjectsForCopy() As Object()
        Public Property Owner() As CopyPasteOperations
        Public Property ColorStoryboard() As Storyboard

        Public Sub ColorStoryboardCompleted(ByVal sender As Object, ByVal e As EventArgs)
            RemoveHandler ColorStoryboard.Completed, AddressOf ColorStoryboardCompleted
            Dim posNewRow As Integer = PositionNewRow
            Owner.PasteRowsWithoutAnimation(posNewRow, View, List, ObjectsForCopy, Owner.MaxAnimationRows, ObjectsForCopy.Length)
            Owner.EndPasteForCanCommands()
        End Sub
    End Class
End Namespace
