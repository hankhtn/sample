Imports DevExpress.XtraRichEdit.API.Native
Imports System
Imports System.Linq
Imports System.Text

Namespace RichEditDemo
    Public Class StaticsticsVisitor
        Inherits DocumentVisitorBase

        Private ReadOnly buffer As StringBuilder
        Private ReadOnly includeTextboxes As Boolean

        Private noSpacesCharacterCount_Renamed As Integer

        Private withSpacesCharacterCount_Renamed As Integer

        Private wordCount_Renamed As Integer

        Private paragraphCount_Renamed As Integer

        Public Sub New(ByVal includeTextboxes As Boolean)
            Me.buffer = New StringBuilder()
            Me.includeTextboxes = includeTextboxes
        End Sub

        Public ReadOnly Property NoSpacesCharacterCount() As Integer
            Get
                Return noSpacesCharacterCount_Renamed
            End Get
        End Property
        Public ReadOnly Property WithSpacesCharacterCount() As Integer
            Get
                Return withSpacesCharacterCount_Renamed
            End Get
        End Property
        Public ReadOnly Property WordCount() As Integer
            Get
                Return wordCount_Renamed
            End Get
        End Property
        Public ReadOnly Property ParagraphCount() As Integer
            Get
                Return paragraphCount_Renamed
            End Get
        End Property

        Public Overrides Sub Visit(ByVal text As DocumentText)
            Me.buffer.Append(text.Text)
        End Sub
        Public Overrides Sub Visit(ByVal textBox As DocumentTextBox)
            If Not Me.includeTextboxes Then
                Return
            End If
            Dim [iterator] As DocumentIterator = textBox.GetIterator(True)
            Dim visitor As New StaticsticsVisitor(False)
            Do While [iterator].MoveNext()
                [iterator].Current.Accept(visitor)
            Loop
            Me.noSpacesCharacterCount_Renamed += visitor.NoSpacesCharacterCount
            Me.withSpacesCharacterCount_Renamed += visitor.WithSpacesCharacterCount
            Me.wordCount_Renamed += visitor.WordCount
            Me.paragraphCount_Renamed += visitor.ParagraphCount
        End Sub
        Public Overrides Sub Visit(ByVal paragraphEnd As DocumentParagraphEnd)
            FinishParagraph()
        End Sub
        Public Overrides Sub Visit(ByVal sectionEnd As DocumentSectionEnd)
            FinishParagraph()
        End Sub
        Private Sub FinishParagraph()
            Dim text As String = Me.buffer.ToString()
            Me.noSpacesCharacterCount_Renamed += text.Count(Function(c) (Not Char.IsWhiteSpace(c)))
            Me.withSpacesCharacterCount_Renamed += text.Length
            Me.wordCount_Renamed += text.Split(New Char() { " "c, "."c, "!"c, "?"c }, StringSplitOptions.RemoveEmptyEntries).Length
            If Not String.IsNullOrWhiteSpace(text) Then
                Me.paragraphCount_Renamed += 1
            End If
            Me.buffer.Length = 0
        End Sub
    End Class
End Namespace
