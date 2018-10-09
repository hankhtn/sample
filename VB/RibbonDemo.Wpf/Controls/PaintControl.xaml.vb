Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.Native
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Core.Native
Imports System.Linq
Imports System.Reflection
Imports System.IO
Imports Microsoft.Win32

Namespace RibbonDemo
    Partial Public Class PaintControl
        Inherits UserControl

        Private Const filterString As String = "JPEG Files (*.JPG)|*.jpg;*.jpeg"

        #Region "static        "
        Public Shared ReadOnly ShowAutomaticButtonProperty As DependencyProperty
        Public Shared ReadOnly ShowNoColorButtonProperty As DependencyProperty
        Public Shared ReadOnly ShowMoreColorsButtonProperty As DependencyProperty
        Public Shared ReadOnly ChipSizeProperty As DependencyProperty
        Public Shared ReadOnly ShowEditorsProperty As DependencyProperty
        Public Shared ReadOnly BrushSizeProperty As DependencyProperty
        Public Shared ReadOnly BrushOpacityProperty As DependencyProperty
        Public Shared ReadOnly TextFontFamilyProperty As DependencyProperty
        Public Shared ReadOnly TextFontSizeProperty As DependencyProperty
        Public Shared ReadOnly TextFontColorProperty As DependencyProperty
        Public Shared ReadOnly BackgroundTextColorProperty As DependencyProperty

        Public Shared ReadOnly BackgroundImageSourceProperty As DependencyProperty
        Public Shared ReadOnly BrushColorProperty As DependencyProperty
        Public Shared ReadOnly MaxBrushSizeProperty As DependencyProperty
        Public Shared ReadOnly MinBrushSizeProperty As DependencyProperty
        Public Shared ReadOnly MousePositionProperty As DependencyProperty
        Public Shared ReadOnly IsBrushToolSelectedProperty As DependencyProperty
        Public Shared ReadOnly IsTextToolSelectedProperty As DependencyProperty
        Public Shared ReadOnly IsTextEditingProperty As DependencyProperty

        Shared Sub New()
            ShowAutomaticButtonProperty = DependencyProperty.Register("ShowAutomaticButton", GetType(Boolean), GetType(PaintControl), New PropertyMetadata(True))
            ShowNoColorButtonProperty = DependencyProperty.Register("ShowNoColorButton", GetType(Boolean), GetType(PaintControl), New PropertyMetadata(False))
            ShowMoreColorsButtonProperty = DependencyProperty.Register("ShowMoreColorsButton", GetType(Boolean), GetType(PaintControl), New PropertyMetadata(False))
            ChipSizeProperty = DependencyProperty.Register("ChipSize", GetType(ChipSize), GetType(PaintControl), New PropertyMetadata(ChipSize.Default))
            BrushSizeProperty = DependencyProperty.Register("BrushSize", GetType(Double), GetType(PaintControl), New PropertyMetadata(8R))
            BrushOpacityProperty = DependencyProperty.Register("BrushOpacity", GetType(Double), GetType(PaintControl), New PropertyMetadata(1R))
            TextFontFamilyProperty = DependencyProperty.Register("TextFontFamily", GetType(FontFamily), GetType(PaintControl), New PropertyMetadata(Nothing))
            TextFontSizeProperty = DependencyProperty.Register("TextFontSize", GetType(Double), GetType(PaintControl), New PropertyMetadata(12R))
            TextFontColorProperty = DependencyProperty.Register("TextFontColor", GetType(Color), GetType(PaintControl), New PropertyMetadata(Colors.Black))
            BackgroundTextColorProperty = DependencyProperty.Register("BackgroundTextColor", GetType(Color), GetType(PaintControl), New PropertyMetadata(Colors.Transparent))

            BackgroundImageSourceProperty = DependencyProperty.Register("BackgroundImageSource", GetType(ImageSource), GetType(PaintControl), New PropertyMetadata(DirectCast(Nothing, BitmapImage), New PropertyChangedCallback(AddressOf OnBackgroundImagePropertyChanged)))
            BrushColorProperty = DependencyProperty.Register("BrushColor", GetType(Color), GetType(PaintControl), New PropertyMetadata(Colors.Red))
            MaxBrushSizeProperty = DependencyProperty.Register("MaxBrushSize", GetType(Double), GetType(PaintControl), New PropertyMetadata(24R))
            MinBrushSizeProperty = DependencyProperty.Register("MinBrushSize", GetType(Double), GetType(PaintControl), New PropertyMetadata(1R))
            MousePositionProperty = DependencyProperty.Register("MousePosition", GetType(Point), GetType(PaintControl), New PropertyMetadata(New Point(-1, -1)))
            IsBrushToolSelectedProperty = DependencyProperty.Register("IsBrushToolSelected", GetType(Boolean), GetType(PaintControl), New PropertyMetadata(True, New PropertyChangedCallback(AddressOf OnIsBrushToolSelectedPropertyChanged)))
            IsTextToolSelectedProperty = DependencyProperty.Register("IsTextToolSelected", GetType(Boolean), GetType(PaintControl), New PropertyMetadata(False, New PropertyChangedCallback(AddressOf OnIsTextToolSelectedPropertyChanged)))
            IsTextEditingProperty = DependencyProperty.Register("IsTextEditing", GetType(Boolean), GetType(PaintControl), New PropertyMetadata(False))
        End Sub
        Public Shared Sub OnBackgroundImagePropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, PaintControl).OnBackgroundImageChanged(e)
        End Sub
        Private Sub OnBackgroundImageChanged(ByVal e As DependencyPropertyChangedEventArgs)
            AddHandler backgroundImage.LayoutUpdated, AddressOf OnBackgroundImageLayoutUpdated
        End Sub

        Private Sub OnBackgroundImageLayoutUpdated(ByVal sender As Object, ByVal e As EventArgs)
            RemoveHandler backgroundImage.LayoutUpdated, AddressOf OnBackgroundImageLayoutUpdated
            UpdateCanvas()
        End Sub
        Private Shared Sub OnIsBrushToolSelectedPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, PaintControl).OnIsBrushToolSelectedChanged()
        End Sub
        Private Shared Sub OnIsTextToolSelectedPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, PaintControl).OnIsTextToolSelectedChanged()
        End Sub
        #End Region
        #Region "props"
        Public Property MousePosition() As Point
            Get
                Return DirectCast(GetValue(MousePositionProperty), Point)
            End Get
            Set(ByVal value As Point)
                SetValue(MousePositionProperty, value)
            End Set
        End Property
        Public Property ShowAutomaticButton() As Boolean
            Get
                Return DirectCast(GetValue(ShowAutomaticButtonProperty), Boolean)
            End Get
            Set(ByVal value As Boolean)
                SetValue(ShowAutomaticButtonProperty, value)
            End Set
        End Property
        Public Property ShowNoColorButton() As Boolean
            Get
                Return DirectCast(GetValue(ShowNoColorButtonProperty), Boolean)
            End Get
            Set(ByVal value As Boolean)
                SetValue(ShowNoColorButtonProperty, value)
            End Set
        End Property
        Public Property ShowMoreColorsButton() As Boolean
            Get
                Return DirectCast(GetValue(ShowMoreColorsButtonProperty), Boolean)
            End Get
            Set(ByVal value As Boolean)
                SetValue(ShowMoreColorsButtonProperty, value)
            End Set
        End Property
        Public Property BrushSize() As Double
            Get
                Return DirectCast(GetValue(BrushSizeProperty), Double)
            End Get
            Set(ByVal value As Double)
                SetValue(BrushSizeProperty, value)
            End Set
        End Property
        Public Property ChipSize() As ChipSize
            Get
                Return DirectCast(GetValue(ChipSizeProperty), ChipSize)
            End Get
            Set(ByVal value As ChipSize)
                SetValue(ChipSizeProperty, value)
            End Set
        End Property
        Public Property MinBrushSize() As Double
            Get
                Return DirectCast(GetValue(MinBrushSizeProperty), Double)
            End Get
            Set(ByVal value As Double)
                SetValue(MinBrushSizeProperty, value)
            End Set
        End Property
        Public Property MaxBrushSize() As Double
            Get
                Return DirectCast(GetValue(MaxBrushSizeProperty), Double)
            End Get
            Set(ByVal value As Double)
                SetValue(MaxBrushSizeProperty, value)
            End Set
        End Property
        Public Property BrushColor() As Color
            Get
                Return DirectCast(GetValue(BrushColorProperty), Color)
            End Get
            Set(ByVal value As Color)
                SetValue(BrushColorProperty, value)
            End Set
        End Property

        Public Property TextFontColor() As Color
            Get
                Return DirectCast(GetValue(TextFontColorProperty), Color)
            End Get
            Set(ByVal value As Color)
                SetValue(TextFontColorProperty, value)
            End Set
        End Property
        Public Property BackgroundTextColor() As Color
            Get
                Return DirectCast(GetValue(BackgroundTextColorProperty), Color)
            End Get
            Set(ByVal value As Color)
                SetValue(BackgroundTextColorProperty, value)
            End Set
        End Property
        Public Property BackgroundImageSource() As ImageSource
            Get
                Return DirectCast(GetValue(BackgroundImageSourceProperty), ImageSource)
            End Get
            Set(ByVal value As ImageSource)
                SetValue(BackgroundImageSourceProperty, value)
            End Set
        End Property
        Public Property BrushOpacity() As Double
            Get
                Return DirectCast(GetValue(BrushOpacityProperty), Double)
            End Get
            Set(ByVal value As Double)
                SetValue(BrushOpacityProperty, value)
            End Set
        End Property
        Public Property TextFontSize() As Double
            Get
                Return DirectCast(GetValue(TextFontSizeProperty), Double)
            End Get
            Set(ByVal value As Double)
                SetValue(TextFontSizeProperty, value)
            End Set
        End Property
        Public Property TextFontFamily() As FontFamily
            Get
                Return DirectCast(GetValue(TextFontFamilyProperty), FontFamily)
            End Get
            Set(ByVal value As FontFamily)
                SetValue(TextFontFamilyProperty, value)
            End Set
        End Property
        Public Property IsBrushToolSelected() As Boolean
            Get
                Return DirectCast(GetValue(IsBrushToolSelectedProperty), Boolean)
            End Get
            Set(ByVal value As Boolean)
                SetValue(IsBrushToolSelectedProperty, value)
            End Set
        End Property
        Public Property IsTextToolSelected() As Boolean
            Get
                Return DirectCast(GetValue(IsTextToolSelectedProperty), Boolean)
            End Get
            Set(ByVal value As Boolean)
                SetValue(IsTextToolSelectedProperty, value)
            End Set
        End Property
        Public Property IsTextEditing() As Boolean
            Get
                Return DirectCast(GetValue(IsTextEditingProperty), Boolean)
            End Get
            Set(ByVal value As Boolean)
                SetValue(IsTextEditingProperty, value)
            End Set
        End Property

        #End Region
        #Region "commands"
        Public Property UndoCommand() As ICommand
        Public Property RedoCommand() As ICommand
        Public Property ClearCommand() As ICommand
        Public Property SaveCommand() As ICommand
        Public Property OpenCommand() As ICommand
        #End Region        

        Public Sub New()
            InitializeComponent()
            AddHandler Loaded, AddressOf OnLoaded
            AddHandler Unloaded, AddressOf OnUnloaded
            URStack = New Stack(Of UIElement)()
            UndoCommand = New DelegateCommand(AddressOf UndoCommandExecute, AddressOf CanUndoCommandExecute)
            RedoCommand = New DelegateCommand(AddressOf RedoCommandExecute, AddressOf CanRedoCommandExecute)
            ClearCommand = New DelegateCommand(AddressOf ClearCommandExecute, AddressOf CanClearCommandExecute)
            SaveCommand = New DelegateCommand(AddressOf SaveCommandExecute)
            OpenCommand = New DelegateCommand(AddressOf OpenCommandExecute)
            TextFontFamily = textEdit.FontFamily
            UpdateCursor()
        End Sub

        #Region "commands implementation"
        Protected Sub SaveCommandExecute()
            CompleteTextEditing(True)
            Dim dlg As New SaveFileDialog() With {.Filter = filterString, .Title = "Save file..."}
            If dlg.ShowDialog() = True Then
                Using stream As Stream = dlg.OpenFile()
                    Dim encoder As New JpegBitmapEncoder()
                    Dim bmp As New RenderTargetBitmap(CInt((canvas.ActualWidth)), CInt((canvas.ActualHeight)), 1 \ 96, 1 \ 96, PixelFormats.Pbgra32)
                    bmp.Render(canvas)
                    encoder.Frames.Add(BitmapFrame.Create(bmp))
                    encoder.Save(stream)
                End Using
            End If
        End Sub
        Protected Sub OpenCommandExecute()
            CompleteTextEditing(True)
            Dim dialog As New OpenFileDialog() With {.Filter = filterString, .Title = "Open file..."}
            If dialog.ShowDialog().Value = True Then
                ClearCommandExecute()
                BackgroundImageSource = DevExpress.Xpf.Core.Native.ImageHelper.CreateImageFromStream(New FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
            End If
        End Sub
        Protected Sub ClearCommandExecute()
            CompleteTextEditing(True)
            Do While canvas.Children.Count > 2
                canvas.Children.RemoveAt(2)
            Loop
            URStack.Clear()
            RaiseCommandsCanExecuteChanged()
            SetCurrentValue(BackgroundImageSourceProperty, Nothing)
        End Sub
        Protected Function CanClearCommandExecute() As Boolean
            Return URStack.Count <> 0 OrElse canvas.Children.Count > 2
        End Function
        Protected Sub UndoCommandExecute()
            URStack.Push(TryCast(canvas.Children(canvas.Children.Count - 1), UIElement))
            canvas.Children.RemoveAt(canvas.Children.Count - 1)
            RaiseCommandsCanExecuteChanged()
        End Sub
        Protected Function CanUndoCommandExecute() As Boolean
            Return canvas.Children.Count > 2
        End Function
        Protected Sub RedoCommandExecute()
            canvas.Children.Add(URStack.Pop())
            RaiseCommandsCanExecuteChanged()
        End Sub
        Protected Function CanRedoCommandExecute() As Boolean
            Return URStack.Count <> 0
        End Function
        Private Sub RaiseCommandsCanExecuteChanged()
            DirectCast(RedoCommand, DelegateCommand).RaiseCanExecuteChanged()
            DirectCast(UndoCommand, DelegateCommand).RaiseCanExecuteChanged()
            DirectCast(ClearCommand, DelegateCommand).RaiseCanExecuteChanged()
        End Sub
        #End Region



        Private Sub OnUnloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RemoveHandler SizeChanged, AddressOf OnSizeChanged
        End Sub

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            AddHandler SizeChanged, AddressOf OnSizeChanged
            UpdateCanvas()
        End Sub
        Private Sub OnSizeChanged(ByVal sender As Object, ByVal e As SizeChangedEventArgs)
            UpdateCanvas()
        End Sub

        Private URStack As Stack(Of UIElement)
        Private demoCenterBottomPanelHeightCoerceValue As Integer = 80
        Private Sub UpdateCanvas()
            Dim left As Double = (canvas.ActualWidth - backgroundImage.ActualWidth) / 2
            Canvas.SetLeft(backgroundImage,If(left < 0, 0, left))
            Dim top As Double = (canvas.ActualHeight - demoCenterBottomPanelHeightCoerceValue - backgroundImage.ActualHeight) / 2
            Canvas.SetTop(backgroundImage,If(top < 0, 0, top))
        End Sub
        Private Sub OnIsBrushToolSelectedChanged()
            If IsBrushToolSelected Then
                IsTextToolSelected = False
                UpdateCursor()
            End If
        End Sub
        Private Sub OnIsTextToolSelectedChanged()
            If IsTextToolSelected Then
                IsBrushToolSelected = False
                UpdateCursor()
            End If
        End Sub

        #Region "layout"
        Private isLeftButtonPressed As Boolean
        Private paintLayer As Canvas
        Private currentPoint As Point
        Private lastPoint As Point
        Private Sub DrawLine(ByVal fromPoint As Point, ByVal toPoint As Point)
            Dim line As New Line() With {.StrokeStartLineCap = PenLineCap.Round, .StrokeEndLineCap = PenLineCap.Round, .StrokeThickness = BrushSize, .Stroke = New SolidColorBrush(BrushColor)}
            line.X1 = toPoint.X
            line.Y1 = toPoint.Y
            line.X2 = fromPoint.X
            line.Y2 = fromPoint.Y
            paintLayer.Children.Add(line)
        End Sub
        Private Sub FocusTextEdit()
            textEdit.Focus()
        End Sub
        Private Sub CompleteTextEditing(ByVal cancel As Boolean)
            IsTextEditing = False
            UpdateCursor()
            If cancel OrElse String.IsNullOrEmpty(textEdit.Text) Then
                Return
            End If

            Dim textBlock As New TextBlock() With {.Foreground = New SolidColorBrush(TextFontColor), .FontSize = TextFontSize, .Text = textEdit.Text}
            Canvas.SetLeft(textBlock, Canvas.GetLeft(textEdit) + 2)
            Canvas.SetTop(textBlock, Canvas.GetTop(textEdit))
            If TextFontFamily IsNot Nothing Then
                textBlock.FontFamily = TextFontFamily
            End If
            canvas.Children.Add(textBlock)
            URStack.Clear()
        End Sub
        Private Sub OnCanvasMouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            isLeftButtonPressed = True
            If IsBrushToolSelected Then
                paintLayer = New Canvas() With {.Height = canvas.ActualHeight, .Width = canvas.ActualWidth, .FlowDirection = FlowDirection.LeftToRight, .Visibility = Visibility.Visible}
                IsTextEditing = False
                canvas.Children.Add(paintLayer)
                paintLayer.Opacity = BrushOpacity
                currentPoint = e.GetPosition(canvas)
                DrawLine(currentPoint, currentPoint)
                lastPoint = currentPoint
                canvas.CaptureMouse()
            Else
                If IsTextEditing Then
                    CompleteTextEditing(False)
                Else
                    textEdit.Text = ""
                    IsTextEditing = True
                    UpdateCursor()
                    Dim currentPoint As Point = e.GetPosition(canvas)
                    Canvas.SetLeft(textEdit, currentPoint.X - 4)
                    Canvas.SetTop(textEdit, currentPoint.Y - 8)
                    Dispatcher.BeginInvoke(New Action(AddressOf FocusTextEdit))
                End If
            End If
        End Sub
        Private Sub UpdateCursor()
            If IsBrushToolSelected Then
                Cursor = Cursors.Pen
            Else
                If IsTextEditing Then
                    Cursor = Cursors.Arrow
                Else
                    Cursor = Cursors.IBeam
                End If
            End If
        End Sub
        Private Sub OnCanvasMouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
            If IsBrushToolSelected AndAlso isLeftButtonPressed Then
                currentPoint = e.GetPosition(paintLayer)
                DrawLine(currentPoint, lastPoint)
                lastPoint = currentPoint
            End If
            MousePosition = e.GetPosition(canvas)
        End Sub
        Private Sub OnCanvasMouseUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            isLeftButtonPressed = False
            If IsBrushToolSelected Then
                canvas.ReleaseMouseCapture()
                Dim path As New System.Windows.Shapes.Path()
                path.StrokeThickness = BrushSize
                path.Stroke = New SolidColorBrush(BrushColor)
                path.StrokeStartLineCap = PenLineCap.Round
                path.StrokeEndLineCap = PenLineCap.Round
                path.Opacity = BrushOpacity
                Dim g As New PathGeometry()
                If paintLayer IsNot Nothing Then
                    For Each l As Line In paintLayer.Children
                        If l Is paintLayer.Children(0) Then
                            g.Figures.Add(New PathFigure() With {.StartPoint = New Point(l.X1, l.Y1)})
                            g.Figures(0).Segments.Add(New LineSegment() With {.Point = New Point(l.X2, l.Y2)})
                        Else
                            g.Figures(0).Segments.Add(New LineSegment() With {.Point = New Point(l.X1, l.Y1)})
                            g.Figures(0).Segments.Add(New LineSegment() With {.Point = New Point(l.X2, l.Y2)})
                        End If
                    Next l
                End If
                path.StrokeLineJoin = PenLineJoin.Round
                path.Data = g
                canvas.Children.Remove(paintLayer)
                canvas.Children.Add(path)
            Else

            End If
            URStack.Clear()
            RaiseCommandsCanExecuteChanged()
        End Sub
        Private Sub OnCanvasKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
            If IsTextToolSelected Then
                If e.Key = Key.Escape Then
                    CompleteTextEditing(True)
                End If
            End If
        End Sub
        #End Region
    End Class
End Namespace
