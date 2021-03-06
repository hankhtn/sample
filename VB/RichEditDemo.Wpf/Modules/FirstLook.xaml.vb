Imports System
Imports System.Windows
Imports System.Windows.Threading
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Core
Imports DevExpress.XtraRichEdit.API.Layout
Imports DevExpress.XtraRichEdit.API.Native

Namespace RichEditDemo
    Partial Public Class FirstLook
        Inherits RichEditDemoModule

        Private Shared ReadOnly PageCountProperty As DependencyProperty = DependencyProperty.Register("PageCount", GetType(Integer), GetType(FirstLook), New PropertyMetadata(1))
        Private Shared ReadOnly ActivePageNumberProperty As DependencyProperty = DependencyProperty.Register("ActivePageNumber", GetType(Integer), GetType(FirstLook), New PropertyMetadata(1))
        Private Shared ReadOnly WordCountProperty As DependencyProperty = DependencyProperty.Register("WordCount", GetType(Integer), GetType(FirstLook), New PropertyMetadata(0))
        Private Shared ReadOnly ActiveViewZoomProperty As DependencyProperty = DependencyProperty.Register("ActiveViewZoom", GetType(Single), GetType(FirstLook), New PropertyMetadata(100F, AddressOf OnActiveViewZoomChanged))
        Private Shared Sub OnActiveViewZoomChanged(ByVal obj As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim firstLook As FirstLook = CType(obj, FirstLook)
            firstLook.RichEditControl.ActiveView.ZoomFactor = DirectCast(e.NewValue, Single) / 100
        End Sub

        Private docummentStatisticsTimer As DispatcherTimer
        Private includeTextBoxes As Boolean

        Public Sub New()
            InitializeComponent()
            AddHandler Me.richEdit.DocumentLayout.DocumentFormatted, AddressOf DocumentLayout_DocumentFormatted
            Me.docummentStatisticsTimer = New DispatcherTimer()
            AddHandler Me.docummentStatisticsTimer.Tick, AddressOf DocummentStatisticsTimer_Tick
            Me.docummentStatisticsTimer.Start()
        End Sub

        Public Property PageCount() As Integer
            Get
                Return CInt((GetValue(PageCountProperty)))
            End Get
            Set(ByVal value As Integer)
                SetValue(PageCountProperty, value)
            End Set
        End Property
        Public Property ActivePageNumber() As Integer
            Get
                Return CInt((GetValue(ActivePageNumberProperty)))
            End Get
            Set(ByVal value As Integer)
                SetValue(ActivePageNumberProperty, value)
            End Set
        End Property
        Public Property WordCount() As Integer
            Get
                Return CInt((GetValue(WordCountProperty)))
            End Get
            Set(ByVal value As Integer)
                SetValue(WordCountProperty, value)
            End Set
        End Property
        Public Property ActiveViewZoom() As Single
            Get
                Return CSng(GetValue(ActiveViewZoomProperty))
            End Get
            Set(ByVal value As Single)
                SetValue(ActiveViewZoomProperty, value)
            End Set
        End Property

        Private Sub DocumentLayout_DocumentFormatted(ByVal sender As Object, ByVal e As EventArgs)
            Me.richEdit.Dispatcher.BeginInvoke(New Action(Sub() PageCount = Me.richEdit.DocumentLayout.GetPageCount()))
        End Sub
        Private Sub RichEditControl_InvalidFormatException(ByVal sender As Object, ByVal e As DevExpress.XtraRichEdit.RichEditInvalidFormatExceptionEventArgs)
            DXMessageBox.Show(String.Format("Cannot open the file '{0}' because the file format or file extension is not valid." & ControlChars.Lf & "Verify that file has not been corrupted and that the file extension matches the format of the file.", RichEditControl.Options.DocumentSaveOptions.CurrentFileName), "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Sub
        Private Sub RichEditControl_DocumentClosing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
            If RichEditControl.Modified Then
                Dim currentFileName As String = RichEditControl.Options.DocumentSaveOptions.CurrentFileName
                Dim message As String = If((Not String.IsNullOrEmpty(currentFileName)), String.Format("Do you want to save the changes you made for '{0}'?", currentFileName), "Do you want to save the changes?")
                Dim result As MessageBoxResult = DXMessageBox.Show(message, "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning)
                If result = MessageBoxResult.Yes Then
                    RichEditControl.SaveDocument()
                End If
                e.Cancel = result = MessageBoxResult.Cancel
            End If
        End Sub
        Private Sub RichEditControl_VisiblePagesChanged(ByVal sender As Object, ByVal e As EventArgs)
            ActivePageNumber = RichEditControl.ActiveView.GetVisiblePageLayoutInfos()(0).PageIndex + 1
        End Sub
        Private Sub RichEditControl_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim element As RangedLayoutElement = richEdit.DocumentLayout.GetElement(Of RangedLayoutElement)(RichEditControl.Document.CaretPosition)
            If element IsNot Nothing Then
                ActivePageNumber = RichEditControl.DocumentLayout.GetPageIndex(element) + 1
            End If
        End Sub
        Private Sub RichEditControl_ContentChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.docummentStatisticsTimer.Start()
        End Sub
        Private Sub DocummentStatisticsTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
            Me.docummentStatisticsTimer.Stop()
            Dispatcher.BeginInvoke(New Action(Sub()
                Dim visitor As StaticsticsVisitor = CalculateDocumentStatistics(Me.includeTextBoxes)
                WordCount = visitor.WordCount
            End Sub))
        End Sub
        Private Sub DocumentStatisticsBarButtonItem_ItemClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Bars.ItemClickEventArgs)
            Dim viewModel = StatisticsViewModel.Create(Me.includeTextBoxes, Function(includeTextBoxes)
                Dim visitor As StaticsticsVisitor = CalculateDocumentStatistics(includeTextBoxes)
                Return New StatisticsData(visitor.NoSpacesCharacterCount, visitor.WithSpacesCharacterCount, visitor.WordCount, visitor.ParagraphCount)
            End Function)
            Dim dialog As IDialogService = dialogService
            dialog.ShowDialog(MessageButton.OK, "Document Statistics", viewModel)
            If Me.includeTextBoxes <> viewModel.IncludeTextboxes Then
                Me.includeTextBoxes = viewModel.IncludeTextboxes
                Dim visitor As StaticsticsVisitor = CalculateDocumentStatistics(Me.includeTextBoxes)
                WordCount = visitor.WordCount
            End If
        End Sub
        Private Function CalculateDocumentStatistics(ByVal includeTextBoxes As Boolean) As StaticsticsVisitor
            Dim [iterator] As New DocumentIterator(RichEditControl.Document, True)
            Dim visitor As New StaticsticsVisitor(includeTextBoxes)
            Do While [iterator].MoveNext()
                [iterator].Current.Accept(visitor)
            Loop
            Return visitor
        End Function
        Private Sub RichEditControl_ZoomChanged(ByVal sender As Object, ByVal e As EventArgs)
            ActiveViewZoom = RichEditControl.ActiveView.ZoomFactor * 100
        End Sub
    End Class
End Namespace
