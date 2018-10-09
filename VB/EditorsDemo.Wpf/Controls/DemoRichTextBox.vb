Imports System
Imports System.Collections.ObjectModel
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Documents
Imports System.Collections.Generic

Namespace DemoUtils
    Public Class DemoRichControl
        Inherits System.Windows.Controls.RichTextBox

        Public Sub New()
            DefaultStyleKey = GetType(System.Windows.Controls.RichTextBox)
        End Sub
        Public Property TextIsBold() As Boolean
            Get
                Return IsTextBold()
            End Get
            Set(ByVal value As Boolean)
                ToggleTextFormatBold(value)
            End Set
        End Property
        Public Property TextIsItalic() As Boolean
            Get
                Return IsTextItalic()
            End Get
            Set(ByVal value As Boolean)
                ToggleTextFormatItalic(value)
            End Set
        End Property
        Public Property TextIsUnderline() As Boolean
            Get
                Return IsTextUnderline()
            End Get
            Set(ByVal value As Boolean)
                ToggleTextFormatUnderline(value)
            End Set
        End Property
        Public Property Text() As String
            Get
                Return Selection.Text
            End Get
            Set(ByVal value As String)
                Selection.Text = value
            End Set
        End Property
        Public Property TextFontFamily() As Object
            Get
                Dim value As Object = Selection.GetPropertyValue(Run.FontFamilyProperty)
                Return If(value Is DependencyProperty.UnsetValue, Nothing, value)
            End Get
            Set(ByVal value As Object)
                If value Is Nothing OrElse value Is TextFontFamily Then
                    Return
                End If
                Try
                    If TypeOf value Is String Then
                        Selection.ApplyPropertyValue(Run.FontFamilyProperty, New FontFamily(TryCast(value, String)))
                    Else
                        Selection.ApplyPropertyValue(Run.FontFamilyProperty, value)
                    End If
                Catch
                End Try
            End Set
        End Property
        Public Property TextFontSize() As Object
            Get
                Dim value As Object = Selection.GetPropertyValue(Run.FontSizeProperty)
                If value Is DependencyProperty.UnsetValue Then
                    Return Nothing
                End If
                Return value
            End Get
            Set(ByVal value As Object)
                If value Is Nothing OrElse value.Equals(TextFontSize) Then
                    Return
                End If

                Selection.ApplyPropertyValue(Run.FontSizeProperty, Convert.ToDouble(value))
            End Set
        End Property
        Public Property TextColor() As Color
            Set(ByVal value As Color)
                If Object.Equals(value, TextColor) Then
                    Return
                End If
                Selection.ApplyPropertyValue(Run.ForegroundProperty, New SolidColorBrush(value))
            End Set
            Get
                Dim brush As SolidColorBrush = TryCast(Selection.GetPropertyValue(Run.ForegroundProperty), SolidColorBrush)
                If brush Is Nothing Then
                    Return Colors.Black
                End If
                Return brush.Color
            End Get
        End Property
        Public Sub SetTextColor(ByVal value As Color)
            Selection.ApplyPropertyValue(Run.ForegroundProperty, New SolidColorBrush(value))
        End Sub
        Public Property TextBackgroundColor() As Color
            Set(ByVal value As Color)

                If value = TextBackgroundColor Then
                    Return
                End If
                SetTextBackgroundColor(value)
            End Set
            Get
                Dim brush As SolidColorBrush = TryCast(Selection.GetPropertyValue(Run.BackgroundProperty), SolidColorBrush)
                If brush Is Nothing Then
                    Return Colors.Black
                End If
                Return brush.Color
            End Get
        End Property
        Public Sub SetTextBackgroundColor(ByVal value As Color)
            Selection.ApplyPropertyValue(Run.BackgroundProperty, New SolidColorBrush(value))
        End Sub
        Public Function GetTextAlignment() As TextAlignment
            Dim value As Object = Selection.GetPropertyValue(System.Windows.Documents.Paragraph.TextAlignmentProperty)
            If Object.Equals(value, DependencyProperty.UnsetValue) Then
                Return TextAlignment.Left
            End If
            If Object.Equals(value, TextAlignment.Center) Then
                Return TextAlignment.Center
            ElseIf Object.Equals(value, TextAlignment.Right) Then
                Return TextAlignment.Right
            Else
                Return TextAlignment.Left
            End If
        End Function
        Public Sub ToggleTextAlignmentLeft()
            Selection.ApplyPropertyValue(System.Windows.Documents.Paragraph.TextAlignmentProperty, TextAlignment.Left)
        End Sub
        Public Sub ToggleTextAlignmentCenter()
            Selection.ApplyPropertyValue(System.Windows.Documents.Paragraph.TextAlignmentProperty, TextAlignment.Center)
        End Sub
        Public Sub ToggleTextAlignmentRight()
            Selection.ApplyPropertyValue(System.Windows.Documents.Paragraph.TextAlignmentProperty, TextAlignment.Right)
        End Sub
        Public Sub ToggleTextAlignmentJustify()
            Selection.ApplyPropertyValue(System.Windows.Documents.Paragraph.TextAlignmentProperty, TextAlignment.Justify)
        End Sub
        Public Sub Clear()
            TryCast(Document, FlowDocument).Blocks.Clear()
        End Sub
        Public Sub Print()
             Dim dialog As New System.Windows.Controls.PrintDialog()
             If Not dialog.ShowDialog().Equals(True) Then
                 Return
             End If
             dialog.PrintVisual(Me, String.Empty)
        End Sub

        Public ReadOnly Property IsEmpty() As Boolean
            Get
                For Each b As Block In Document.Blocks
                    If Not(TypeOf b Is Paragraph) Then
                        Return False
                    End If

                    For Each o As Object In CType(b, Paragraph).Inlines
                        If Not(TypeOf o Is Run) Then
                            Return False
                        End If
                        Dim r As Run = TryCast(o, Run)
                        If Not String.IsNullOrEmpty(r.Text) Then
                            Return False
                        End If
                    Next o
                Next b
            Return True
            End Get
        End Property
        Public ReadOnly Property IsSelectionEmpty() As Boolean
            Get
                Return Selection.IsEmpty
            End Get
        End Property
        Protected Function IsTextBold() As Boolean
            Dim value As Object = Selection.GetPropertyValue(TextElement.FontWeightProperty)
            Return If(value Is DependencyProperty.UnsetValue, False, (Object.Equals(value, FontWeights.Bold)))
        End Function
        Protected Function IsTextItalic() As Boolean
            Dim value As Object = Selection.GetPropertyValue(Run.FontStyleProperty)
            Return If(value Is DependencyProperty.UnsetValue, False, (Object.Equals(value, FontStyles.Italic)))
        End Function
        Protected Function IsTextUnderline() As Boolean
            Dim value As Object = Selection.GetPropertyValue(Inline.TextDecorationsProperty)
            Return If(value Is DependencyProperty.UnsetValue, False, value IsNot Nothing AndAlso System.Windows.TextDecorations.Underline.Equals(value))
        End Function
        Protected Sub ToggleTextFormatBold(ByVal bold As Boolean)
            If bold = IsTextBold() Then
                Return
            End If
            If Not bold Then
                Selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal)
            Else
                Selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Bold)
            End If
        End Sub
        Protected Sub ToggleTextFormatItalic(ByVal italic As Boolean)
            If italic = IsTextItalic() Then
                Return
            End If
            If Not italic Then
                Selection.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Normal)
            Else
                Selection.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Italic)
            End If
        End Sub
        Protected Sub ToggleTextFormatUnderline(ByVal underline As Boolean)
            If underline = IsTextUnderline() Then
                Return
            End If
            If Not underline Then
                Selection.ApplyPropertyValue(Run.TextDecorationsProperty, Nothing)
            Else
                Selection.ApplyPropertyValue(Run.TextDecorationsProperty, System.Windows.TextDecorations.Underline)
            End If
        End Sub
        Public Function GetUIElementUnderSelection(Of T As Class)(ByVal blocks As BlockCollection) As T
            For Each block As Block In blocks
                Dim ph As Paragraph = TryCast(block, Paragraph)
                If ph IsNot Nothing Then
                    For Each obj As Object In ph.Inlines
                        If TypeOf obj Is Run Then
                            Continue For
                        End If
                        Dim cont As InlineUIContainer = TryCast(obj, InlineUIContainer)
                        If cont IsNot Nothing AndAlso cont.ContentStart.CompareTo(Selection.Start) > 0 AndAlso cont.ContentStart.CompareTo(Selection.End) < 0 Then
                            If TypeOf cont.Child Is T Then
                                Return TryCast(cont.Child, T)
                            End If
                        End If
                    Next obj
                Else
                    Dim lst As List = TryCast(block, List)
                    If lst IsNot Nothing Then
                        For Each lstItem As ListItem In lst.ListItems
                            Dim retVal As T = GetUIElementUnderSelection(Of T)(lstItem.Blocks)
                            If retVal IsNot Nothing Then
                                Return retVal
                            End If
                        Next lstItem
                    End If
                End If
            Next block
            Return Nothing

        End Function
        Public Property ListMarkerStyle() As TextMarkerStyle
            Get
                Dim startParagraph As Paragraph = Selection.Start.Paragraph
                Dim endParagraph As Paragraph = Selection.End.Paragraph
                If startParagraph IsNot Nothing AndAlso endParagraph IsNot Nothing AndAlso (TypeOf startParagraph.Parent Is ListItem) AndAlso (TypeOf endParagraph.Parent Is ListItem) AndAlso Object.ReferenceEquals(CType(startParagraph.Parent, ListItem).List, CType(endParagraph.Parent, ListItem).List) Then
                    Return CType(startParagraph.Parent, ListItem).List.MarkerStyle
                End If
                Return TextMarkerStyle.None
            End Get
            Set(ByVal value As TextMarkerStyle)
                If value = ListMarkerStyle Then
                    Return
                End If
                Dim p As Paragraph = Selection.Start.Paragraph
                If p Is Nothing Then
                    Return
                End If
                If value = TextMarkerStyle.None Then
                    If TypeOf p.Parent Is ListItem Then
                        EditingCommands.ToggleBullets.Execute(Nothing, Me)
                        p = Selection.Start.Paragraph
                        If TypeOf p.Parent Is ListItem Then
                            EditingCommands.ToggleBullets.Execute(Nothing, Me)
                        End If
                    End If
                    Return
                End If
                If Not(TypeOf p.Parent Is ListItem) Then
                    EditingCommands.ToggleBullets.Execute(Nothing, Me)
                    p = Me.Selection.Start.Paragraph
                End If

                If p Is Nothing OrElse Not(TypeOf p.Parent Is ListItem) Then
                    Return
                End If
                CType(p.Parent, ListItem).List.MarkerStyle = value
            End Set
        End Property
        Public Function GetUIElementUnderSelection(Of T As Class)() As T
            Dim col As BlockCollection = Document.Blocks
            If Selection.Start.GetNextInsertionPosition(LogicalDirection.Forward) Is Nothing OrElse Selection.Start.GetNextInsertionPosition(LogicalDirection.Forward).CompareTo(Selection.End) <> 0 Then
                Return Nothing
            End If
            Return GetUIElementUnderSelection(Of T)(col)
        End Function
    End Class
End Namespace
