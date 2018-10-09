Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System.Diagnostics
Imports System.IO
Imports System.Windows

Namespace MVVMDemo.Services
    Public Class CustomServiceViewModel
        Public Sub Export(ByVal format As ExportFormat)
            Dim path = System.IO.Path.ChangeExtension(System.IO.Path.GetTempFileName(), format.ToString().ToLower())

            Dim service As IExportService = Me.GetRequiredService(Of IExportService)()
            Using file As FileStream = System.IO.File.Create(path)
                service.Export(file, format)
            End Using

            If MessageBox.Show("Do you want to open the file?" & ControlChars.CrLf & path, "Open file", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then
                Process.Start(path)
            Else
                System.IO.File.Delete(path)
            End If
        End Sub
    End Class
    Public Interface IExportService
        Sub Export(ByVal stream As Stream, ByVal format As ExportFormat)
    End Interface
    Public Enum ExportFormat
        Xlsx
        Pdf
    End Enum
End Namespace
