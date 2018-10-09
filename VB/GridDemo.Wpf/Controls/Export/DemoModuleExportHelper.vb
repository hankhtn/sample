Imports DevExpress.Export
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.Native
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.PdfViewer
Imports DevExpress.Xpf.Printing
Imports DevExpress.Xpf.Ribbon
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.Xpf.Spreadsheet
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Localization
Imports DevExpress.XtraPrinting.Native
Imports DevExpress.XtraPrinting.Native.ExportOptionsControllers
Imports Microsoft.Win32
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
'using IDialogService = DevExpress.Mvvm.IDialogService
Imports System.Windows.Threading

Namespace GridDemo
    Public NotInheritable Class DemoModuleWYSIWYGExportHelper

        Private Sub New()
        End Sub

        Public Shared Sub DoExport(ByVal view As DataViewBase, ByVal format As ExportFormat)
            Select Case format
                Case ExportFormat.Xls
                    Export(view, Function(link) New Action(Of Stream, XlsExportOptions)(AddressOf link.ExportToXls))
                Case ExportFormat.Xlsx
                    Export(view, Function(link) New Action(Of Stream, XlsxExportOptions)(AddressOf link.ExportToXlsx))
                Case ExportFormat.Pdf
                    Export(view, Function(link) New Action(Of Stream, PdfExportOptions)(AddressOf link.ExportToPdf))
                Case ExportFormat.Htm
                    Export(view, Function(link) New Action(Of Stream, HtmlExportOptions)(AddressOf link.ExportToHtml))
                Case ExportFormat.Mht
                    Export(view, Function(link) New Action(Of Stream, MhtExportOptions)(AddressOf link.ExportToMht))
                Case ExportFormat.Rtf
                    Export(view, Function(link) New Action(Of Stream, RtfExportOptions)(AddressOf link.ExportToRtf))
                Case ExportFormat.Txt
                    Export(view, Function(link) New Action(Of Stream, TextExportOptions)(AddressOf link.ExportToText))
                Case ExportFormat.Image
                    Export(view, Function(link) New Action(Of Stream, ImageExportOptions)(AddressOf link.ExportToImage))
                Case ExportFormat.Xps
                    Export(view, Function(link) New Action(Of Stream, XpsExportOptions)(AddressOf link.ExportToXps))
                Case ExportFormat.Docx
                    Export(view, Function(link) New Action(Of Stream, DocxExportOptions)(AddressOf link.ExportToDocx))
            End Select
        End Sub
        Private Shared Sub OnAfterBuildPages(ByVal sender As Object, ByVal e As EventArgs)
            DXSplashScreen.Close()
        End Sub
        Private Shared Sub UnsubscribeProgressEvents(ByVal link As PrintableControlLink, ByVal onExportProgress As EventHandler)
            RemoveHandler link.PrintingSystem.ProgressReflector.PositionChanged, onExportProgress
            RemoveHandler link.PrintingSystem.AfterBuildPages, AddressOf OnAfterBuildPages
        End Sub
        Private Shared Sub SubscribeProgressEvents(ByVal link As PrintableControlLink, ByVal onExportProgress As EventHandler)
            AddHandler link.PrintingSystem.ProgressReflector.PositionChanged, onExportProgress
            AddHandler link.PrintingSystem.AfterBuildPages, AddressOf OnAfterBuildPages
        End Sub
        Private Shared Sub Export(Of T As {ExportOptionsBase, New})(ByVal view As DataViewBase, ByVal getExportToStreamMethod As Func(Of PrintableControlLink, Action(Of Stream, T)))
            Dim link = New PrintableControlLink(view)
            Dim onExportProgress As EventHandler = Sub(o, e) ExportHelper.ExportProgress(New ProgressChangedEventArgs(link.PrintingSystem.ProgressReflector.Position, Nothing))
            Using stream As New MemoryStream()
                ExportHelper.ExportCore(getExportToStreamMethod(link), stream, Sub(options) SubscribeProgressEvents(link, onExportProgress), Sub(options) UnsubscribeProgressEvents(link, onExportProgress))
            End Using
        End Sub
    End Class
    Public NotInheritable Class DemoModuleExportHelper

        Private Sub New()
        End Sub

        Public Shared Sub ExportToXlsx(ByVal view As DataViewBase)
            Export(New Action(Of Stream, XlsxExportOptionsEx)(AddressOf view.ExportToXlsx))
        End Sub
        Public Shared Sub ExportToXls(ByVal view As DataViewBase)
            Export(New Action(Of Stream, XlsExportOptionsEx)(AddressOf view.ExportToXls))
        End Sub
        Public Shared Sub ExportToCsv(ByVal view As DataViewBase)
            Export(New Action(Of Stream, CsvExportOptionsEx)(AddressOf view.ExportToCsv))
        End Sub
        Private Shared Sub Export(Of T As {ExportOptionsBase, New})(ByVal exportToStreamMethod As Action(Of Stream, T))
            Dispatcher.CurrentDispatcher.BeginInvoke(New Action(Of Action(Of Stream, T))(AddressOf ExportCore), DispatcherPriority.ContextIdle, exportToStreamMethod)
        End Sub
        Private Shared Sub ExportCore(Of T As {ExportOptionsBase, New})(ByVal exportToStreamMethod As Action(Of Stream, T))
            Using stream As New MemoryStream()
                ExportHelper.ExportCore(exportToStreamMethod, stream, AddressOf SubscribeProgressEvents, AddressOf UnsubscribeProgressEvents)
            End Using
        End Sub
        Private Shared Sub UnsubscribeProgressEvents(Of T As ExportOptionsBase)(ByVal options As T)
            RemoveHandler DirectCast(options, IDataAwareExportOptions).ExportProgress, AddressOf ExportHelper.ExportProgress
        End Sub
        Private Shared Sub SubscribeProgressEvents(Of T As ExportOptionsBase)(ByVal options As T)
            AddHandler DirectCast(options, IDataAwareExportOptions).ExportProgress, AddressOf ExportHelper.ExportProgress
        End Sub
    End Class
    Friend NotInheritable Class ExportHelper

        Private Sub New()
        End Sub

        Private Const OpenInInternalViewer As String = "Open with DevExpress Control"
        Private Const OpenInExternalViewer As String = "Open with Default App"
        Private Const OpenExportedFile As String = "Open Exported File"
        Private Const OpenExportedFileDescription As String = "You can view the exported file in your system default" & ControlChars.Lf & "application or in a DevExpress WPF {0} control."

        Private Shared InternalViewerDisplayTexts As New Dictionary(Of ViewerType, String)() From {
            { ViewerType.Spreadsheet, "Spreadsheet" },
            { ViewerType.RichEdit, "RichEdit" },
            { ViewerType.PDFViewer, "PDF Viewer" }
        }

        Public Shared Sub ExportCore(Of T As {ExportOptionsBase, New})(ByVal exportToStream As Action(Of Stream, T), ByVal stream As Stream, ByVal subscribeProgress As Action(Of T), ByVal unsubscribeProgress As Action(Of T))
            If stream Is Nothing Then
                Return
            End If
            Try
                DXSplashScreen.Show(Of ExportWaitIndicator)()
                Dim options = New T()
                subscribeProgress(options)
                Try
                    exportToStream(stream, options)
                Finally
                    unsubscribeProgress(options)
                    If DXSplashScreen.IsActive Then
                        DXSplashScreen.Close()
                    End If
                End Try

                stream.Seek(0, SeekOrigin.Begin)

                Dim viewerType As ViewerType = GetInternalViewerType(options, IsLargeFile(stream))

                If viewerType = GridDemo.ViewerType.External Then
                    If ShouldOpenExportedFile() Then
                        OpenExternalViewer(stream, options)
                    End If
                    Return
                End If

                Select Case GetExportType(viewerType)
                    Case GridDemo.ViewerType.External
                        OpenExternalViewer(stream, options)
                        Return
                    Case GridDemo.ViewerType.None
                        Return
                    Case Else
                        OpenInternalViewer(stream, options, viewerType)
                        Return
                End Select
            Catch e As Exception
                DXMessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            End Try
        End Sub

        Private Shared Function GetExportType(ByVal viewerType As ViewerType) As ViewerType
            Dim dialogCommands As New List(Of UICommand)()
            Dim result As ViewerType = GridDemo.ViewerType.None
            dialogCommands.Add(New UICommand(0, OpenInInternalViewer, DelegateCommandFactory.Create(Sub() result = viewerType), True, False))
            dialogCommands.Add(New UICommand(1, OpenInExternalViewer, DelegateCommandFactory.Create(Sub() result = GridDemo.ViewerType.External), True, False))
            Dim d As New DXDialogWindow() With {.ResizeMode = ResizeMode.NoResize, .SizeToContent = SizeToContent.WidthAndHeight, .WindowStyle = WindowStyle.SingleBorderWindow, .ShowInTaskbar = False, .WindowStartupLocation = WindowStartupLocation.CenterOwner, .Owner = Application.Current.MainWindow, .Title = OpenExportedFile}
            d.CommandsSource = dialogCommands
            d.Content = New TextBlock() With {.Text = String.Format(OpenExportedFileDescription, InternalViewerDisplayTexts(viewerType)), .Margin = New Thickness(24, 15, 24, 16), .HorizontalAlignment = HorizontalAlignment.Center}
            d.ShowDialogWindow()
            Return result
        End Function

        Private Shared Function IsLargeFile(ByVal stream As Stream) As Boolean
            Return stream.Length > CLng(150000)
        End Function

        Private Shared Sub OpenExternalViewer(Of T As ExportOptionsBase)(ByVal stream As Stream, ByVal options As T)
            Dim fullFilePath As String = SaveToFile(stream, options)
            ProcessLaunchHelper.StartProcess(fullFilePath, False)
        End Sub

        Private Shared Function GetInternalViewerType(ByVal options As ExportOptionsBase, ByVal isLargeFile As Boolean) As ViewerType
            Select Case options.GetFormat()
                Case ExportFormat.Csv, ExportFormat.Xls, ExportFormat.Xlsx
                    Return If(isLargeFile, ViewerType.External, ViewerType.Spreadsheet)
                Case ExportFormat.Htm, ExportFormat.Mht, ExportFormat.Rtf
                    Return If(isLargeFile, ViewerType.External, ViewerType.RichEdit)
                Case ExportFormat.Pdf
                    Return ViewerType.PDFViewer
                Case Else
                    Return ViewerType.External
            End Select
        End Function

        Private Shared Sub OpenInternalViewer(Of T As ExportOptionsBase)(ByVal stream As Stream, ByVal options As T, ByVal vieweType As ViewerType)
            Select Case vieweType
                Case ViewerType.Spreadsheet
                    OpenInternalViewerWindow(AddressOf SpreadSheetLoadDocument, Function() New SpreadsheetControl(), stream, options)
                    Return
                Case ViewerType.PDFViewer
                    OpenInternalViewerWindow(AddressOf PdfViewerLoadDocument, Function() New PdfViewerControl(), stream, options)
                    Return
                Case ViewerType.RichEdit
                    OpenInternalViewerWindow(AddressOf RichEditLoadDocument, Function() New RichEditControl(), stream, options)
                    Return
            End Select
        End Sub
        Private Shared Sub OpenInternalViewerWindow(Of T1 As ExportOptionsBase, T2 As FrameworkElement)(ByVal loadDocumentAction As Action(Of T2, Stream, T1), ByVal createViewer As Func(Of T2), ByVal stream As Stream, ByVal options As T1)
            DXSplashScreen.Show(Of OpenViewerWaitIndicator)()
            Dim viewerWindow = New ThemedWindow() With {.Title = "Document", .Owner = Application.Current.MainWindow, .WindowStartupLocation = WindowStartupLocation.CenterOwner}
            Dim viewerControl As T2 = createViewer()
            viewerWindow.Content = viewerControl
            loadDocumentAction(viewerControl, stream, options)
            AddHandler viewerWindow.Loaded, AddressOf ViewerWindow_Loaded1
            viewerWindow.ShowDialog()
        End Sub

        Private Shared Function GetSpreadsheetDocumentFormat(ByVal options As ExportOptionsBase) As DevExpress.Spreadsheet.DocumentFormat
            Select Case options.GetFormat()
                Case ExportFormat.Csv
                    Return DevExpress.Spreadsheet.DocumentFormat.Csv
                Case ExportFormat.Xls
                    Return DevExpress.Spreadsheet.DocumentFormat.Xls
                Case ExportFormat.Xlsx
                    Return DevExpress.Spreadsheet.DocumentFormat.Xlsx
                Case Else
                    Return DevExpress.Spreadsheet.DocumentFormat.Undefined
            End Select
        End Function
        Private Shared Function GetRichEditDocumentFormat(ByVal options As ExportOptionsBase) As DevExpress.XtraRichEdit.DocumentFormat
            Select Case options.GetFormat()
                Case ExportFormat.Rtf
                    Return DevExpress.XtraRichEdit.DocumentFormat.Rtf
                Case ExportFormat.Htm
                    Return DevExpress.XtraRichEdit.DocumentFormat.Html
                Case ExportFormat.Mht
                    Return DevExpress.XtraRichEdit.DocumentFormat.Mht
                Case Else
                    Return DevExpress.XtraRichEdit.DocumentFormat.Undefined
            End Select
        End Function

        Private Shared Sub ViewerWindow_Loaded1(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim element As FrameworkElement = DirectCast(sender, FrameworkElement)
            RemoveHandler element.Loaded, AddressOf ViewerWindow_Loaded1
            If DXSplashScreen.IsActive Then
                DXSplashScreen.Close()
            End If
        End Sub

        Private Shared Sub RichEditLoadDocument(Of T As ExportOptionsBase)(ByVal richEditControl As RichEditControl, ByVal stream As Stream, ByVal options As T)
            richEditControl.CommandBarStyle = DevExpress.Xpf.RichEdit.CommandBarStyle.Ribbon
            richEditControl.LoadDocument(stream, GetRichEditDocumentFormat(options))
        End Sub
        Private Shared Sub SpreadSheetLoadDocument(Of T As ExportOptionsBase)(ByVal spreadSheetControl As SpreadsheetControl, ByVal stream As Stream, ByVal options As T)
            spreadSheetControl.CommandBarStyle = DevExpress.Xpf.Spreadsheet.CommandBarStyle.Ribbon
            spreadSheetControl.LoadDocument(stream, GetSpreadsheetDocumentFormat(options))
        End Sub
        Private Shared Sub PdfViewerLoadDocument(Of T As ExportOptionsBase)(ByVal pdfViewerControl As PdfViewerControl, ByVal stream As Stream, ByVal options As T)
            pdfViewerControl.DocumentSource = stream
        End Sub

        Private Shared Function SaveToFile(Of T As ExportOptionsBase)(ByVal stream As Stream, ByVal options As T) As String
            Dim tempFileName As String = Path.ChangeExtension(Path.GetTempFileName(), options.GetFileExtension())
            Using fileStream As New FileStream(tempFileName, FileMode.Create)
                stream.CopyTo(fileStream)
            End Using
            Return tempFileName
        End Function

        Public Shared Sub ExportProgress(ByVal ea As ProgressChangedEventArgs)
            If Not DXSplashScreen.IsActive Then
                Return
            End If
            DXSplashScreen.Progress(ea.ProgressPercentage)
        End Sub
        Public Shared Function GetFileName(ByVal options As ExportOptionsBase) As String
            Return GetFileName(ExportOptionsControllerBase.GetControllerByOptions(options))
        End Function
        Private Shared Function GetFileName(ByVal controller As ExportOptionsControllerBase) As String
            Dim dlg As SaveFileDialog = CreateSaveFileDialog(controller)
            If dlg.ShowDialog() = True AndAlso (Not String.IsNullOrEmpty(dlg.FileName)) Then
                Return FileHelper.SetValidExtension(dlg.FileName, controller.FileExtensions(0), controller.FileExtensions)
            Else
                Return String.Empty
            End If
        End Function
        Private Shared Function CreateSaveFileDialog(ByVal controller As ExportOptionsControllerBase) As SaveFileDialog
            Dim dlg As New SaveFileDialog()
            dlg.Title = PreviewLocalizer.GetString(PreviewStringId.SaveDlg_Title)
            dlg.ValidateNames = True
            dlg.FileName = PrintPreviewOptions.DefaultFileNameDefault
            dlg.Filter = controller.Filter
            Return dlg
        End Function
        Private Shared Function ShouldOpenExportedFile() As Boolean
            Return DXMessageBox.Show(PreviewLocalizer.GetString(PreviewStringId.Msg_OpenFileQuestion), PreviewLocalizer.GetString(PreviewStringId.Msg_OpenFileQuestionCaption), MessageBoxButton.YesNo, MessageBoxImage.Question) = MessageBoxResult.Yes
        End Function
    End Class
    Public Class PrintingIconImageSourceConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim rawIconName As String = TryCast(value, String)
            If rawIconName Is Nothing Then
                Return Nothing
            End If
            Dim iconName As String = Regex.Replace(rawIconName, "[^a-zA-Z]", String.Empty)
            Dim iconPath As String = "Images/BarItems/" & iconName & "_32x32.png"
            Return New PrintingResourceImageExtension() With {.ResourceName = iconPath}.ProvideValue(Nothing)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class InternalViewerViewModel
        Public Sub New(ByVal stream As Stream, ByVal options As ExportOptionsBase)
            Me.Stream = stream
            Me.Options = options
        End Sub

        Private privateStream As Stream
        Public Property Stream() As Stream
            Get
                Return privateStream
            End Get
            Private Set(ByVal value As Stream)
                privateStream = value
            End Set
        End Property
        Private privateOptions As ExportOptionsBase
        Public Property Options() As ExportOptionsBase
            Get
                Return privateOptions
            End Get
            Private Set(ByVal value As ExportOptionsBase)
                privateOptions = value
            End Set
        End Property

        Public Sub StopSplashScreen()
            If DXSplashScreen.IsActive Then
                DXSplashScreen.Close()
            End If
        End Sub
    End Class

    Public Enum ViewerType
        None
        External
        Spreadsheet
        RichEdit
        PDFViewer
    End Enum
End Namespace
