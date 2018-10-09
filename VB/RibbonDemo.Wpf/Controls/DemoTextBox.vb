Imports System
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes
Imports DevExpress.Utils
Imports System.Linq
Imports System.Windows.Data
Imports System.Collections.Generic
Imports DevExpress.Mvvm
Imports Microsoft.Win32
Imports System.IO
Imports DevExpress.Xpf.Core.Native

Namespace RibbonDemo
    Public Class DemoTextBox
        Inherits TextBox

        Private Const filterString As String = "TXT Files (*.TXT)|*.txt"

        #Region "static"
        Public Shared ReadOnly CaretPosProperty As DependencyProperty = DependencyProperty.Register("CaretPos", GetType(Point), GetType(DemoTextBox), New PropertyMetadata(New Point()))
        #End Region

        #Region "dep props"
        Public Property CaretPos() As Point
            Get
                Return DirectCast(GetValue(CaretPosProperty), Point)
            End Get
            Set(ByVal value As Point)
                SetValue(CaretPosProperty, value)
            End Set
        End Property
        #End Region

        #Region "commands"
        Public Property CutCommand() As ICommand
        Public Property CopyCommand() As ICommand
        Public Property PasteCommand() As ICommand
        Public Property SelectAllCommand() As ICommand
        Public Property ClearCommand() As ICommand
        Public Property UndoCommand() As ICommand
        Public Property RedoCommand() As ICommand
        Public Property OpenFromFileCommand() As ICommand
        Public Property SaveToFileCommand() As ICommand
        #End Region

        #Region "commands implementation"
        Private Sub OnCutCommandExecute()
            DXClipboard.SetText(SelectedText)
            SelectedText = String.Empty
        End Sub
        Private Sub OnCopyCommandExecute()
            DXClipboard.SetText(SelectedText)
        End Sub
        Private Sub OnPasteCommandExecute()
            SelectedText = DXClipboard.GetText()
            SelectionStart += SelectionLength
            SelectionLength = 0
        End Sub
        Private Sub OnSelectAllCommandExecute()
            SelectionStart = 0
            SelectionLength = Text.Count()
        End Sub
        Private Sub OnClearCommandExecute()
            SelectionStart = 0
            SelectionLength = Text.Count()
            SelectedText = String.Empty
        End Sub
        Private Function UndoCommandCanExecute() As Boolean
            Return UndoStack.Count <> 0
        End Function

        Private Sub OnUndoCommandExecute()
            If UndoStack.Count <> 0 Then
                RedoStack.Push(UndoStack.Pop())
                disableUndoRedoLogic = True
                Text = If(UndoStack.Count = 0, String.Empty, UndoStack.Peek())
                DirectCast(UndoCommand, DelegateCommand).RaiseCanExecuteChanged()
                DirectCast(RedoCommand, DelegateCommand).RaiseCanExecuteChanged()
            End If
        End Sub
        Private Function RedoCommandCanExecute() As Boolean
            Return RedoStack.Count <> 0
        End Function
        Private Sub OnRedoCommandExecute()
            disableUndoRedoLogic = True
            Text = RedoStack.Pop()
            UndoStack.Push(Text)
            DirectCast(UndoCommand, DelegateCommand).RaiseCanExecuteChanged()
            DirectCast(RedoCommand, DelegateCommand).RaiseCanExecuteChanged()
        End Sub
        Private Sub OnOpenFromFileCommandExecute()
            Dim dialog As New OpenFileDialog() With {.Filter = filterString, .Title = "Open file..."}
            If dialog.ShowDialog().Value = True Then
                Text = (New StreamReader(dialog.FileName)).ReadToEnd()
                UndoStack.Clear()
                RedoStack.Clear()
            End If
        End Sub
        Private Sub OnSaveToFileCommandExecute()
            Dim dlg As New SaveFileDialog() With {.Filter = filterString, .Title = "Save file..."}
            If dlg.ShowDialog() = True Then
                Using stream As Stream = dlg.OpenFile()
                    Dim writer As New StreamWriter(stream)
                    writer.Write(Text)
                    writer.Close()
                End Using
            End If
        End Sub
        #End Region
        Public Sub New()
            CutCommand = New DelegateCommand(AddressOf OnCutCommandExecute)
            CopyCommand = New DelegateCommand(AddressOf OnCopyCommandExecute)
            PasteCommand = New DelegateCommand(AddressOf OnPasteCommandExecute)
            SelectAllCommand = New DelegateCommand(AddressOf OnSelectAllCommandExecute)
            ClearCommand = New DelegateCommand(AddressOf OnClearCommandExecute)
            UndoCommand = New DelegateCommand(AddressOf OnUndoCommandExecute, AddressOf UndoCommandCanExecute)
            RedoCommand = New DelegateCommand(AddressOf OnRedoCommandExecute, AddressOf RedoCommandCanExecute)
            OpenFromFileCommand = New DelegateCommand(AddressOf OnOpenFromFileCommandExecute)
            SaveToFileCommand = New DelegateCommand(AddressOf OnSaveToFileCommandExecute)

            UndoStack = New Stack(Of String)()
            RedoStack = New Stack(Of String)()

            AcceptsReturn = True
            TextWrapping = System.Windows.TextWrapping.Wrap
            Me.VerticalScrollBarVisibility = ScrollBarVisibility.Auto
            AcceptsTab = True
            AddHandler SelectionChanged, AddressOf OnSelectionChanged
            AddHandler TextChanged, AddressOf OnTextChanged
        End Sub
        Private Property UndoStack() As Stack(Of String)
        Private Property RedoStack() As Stack(Of String)
        Private disableUndoRedoLogic As Boolean
        Public Sub Close()

            UndoStack.Clear()
            RedoStack.Clear()
        End Sub

        Private Overloads Sub OnTextChanged(ByVal sender As Object, ByVal e As TextChangedEventArgs)
            If disableUndoRedoLogic Then
                disableUndoRedoLogic = False
                Return
            End If
            RedoStack.Clear()
            UndoStack.Push(Text)
            DirectCast(RedoCommand, DelegateCommand).RaiseCanExecuteChanged()
            DirectCast(UndoCommand, DelegateCommand).RaiseCanExecuteChanged()
        End Sub
        Private Overloads Sub OnSelectionChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateCaretPositionInfo()
        End Sub
        Private selPos As Integer = 0
        Private index As Integer = 0
        Private lastLineBreakPos As Integer = 0
        Private Sub UpdateCaretPositionInfo()
            selPos = Me.SelectionStart
            index = 0
            lastLineBreakPos = -1
            Dim lineBreaksCount As Integer = Text.Count(AddressOf updateCaretPositionPredicate)
            CaretPos = New Point(CaretPos.Y, lineBreaksCount + 1)
            CaretPos = New Point(selPos - lastLineBreakPos, CaretPos.Y)
        End Sub
        Private Function updateCaretPositionPredicate(ByVal ch As Char) As Boolean
            index += 1
            If ch = ControlChars.Cr AndAlso index <= selPos Then
                lastLineBreakPos = index
                Return True
            End If
            Return False
        End Function
    End Class

End Namespace
