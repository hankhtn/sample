Imports System
Imports System.IO
Imports System.Windows
Imports System.Diagnostics
Imports DevExpress.Xpf.Core

Imports System.Windows.Forms
Imports DevExpress.DemoData.Helpers
Imports System.Reflection


Namespace SpreadsheetDemo
    Public NotInheritable Class DocumentLoadHelper

        Private Sub New()
        End Sub

        Public Shared Function GetDocument(ByVal path As String) As Stream
            Dim assembly = System.Reflection.Assembly.GetExecutingAssembly()
            Return assembly.GetManifestResourceStream(path)
        End Function
    End Class
    #Region "DemoUtils"
    Public Class DemoUtils
        Public Shared Function GetRelativePath(ByVal name As String) As String
            name = "Data\" & name
            Dim path As String = DataFilesHelper.DataDirectory
            Dim s As String = "\"
            For i As Integer = 0 To 10
                If System.IO.File.Exists(path & s & name) Then
                    Return System.IO.Path.GetFullPath(path & s & name)
                Else
                    s &= "..\"
                End If
            Next i
            Return ""
        End Function

        Public Shared Function GetRelativeDirectoryPath(ByVal name As String) As String
            name = "Data\" & name
            Dim path As String = DataFilesHelper.DataDirectory
            Dim s As String = "\"
            For i As Integer = 0 To 10
                If System.IO.Directory.Exists(path & s & name) Then
                    Return (path & s & name)
                Else
                    s &= "..\"
                End If
            Next i
            Return ""
        End Function

        Public Shared Function GetDataStream(ByVal fileName As String) As Stream
            Dim path As String = DemoUtils.GetRelativePath(fileName)
            If Not String.IsNullOrEmpty(path) Then
                Dim fileAccess As FileAccess = If((File.GetAttributes(path) And FileAttributes.ReadOnly) <> 0, System.IO.FileAccess.Read, System.IO.FileAccess.ReadWrite)
                Return New FileStream(path, FileMode.Open, fileAccess)
            End If
            Return Nothing
        End Function

        Public Shared Sub SetDatabasePath()
            Const dbName As String = "nwind.mdb"
            Const pathToDbTag As String = "|pathToDb|"
            Dim path As String = GetRelativePath(dbName)
            If String.IsNullOrEmpty(path) Then
                Return
            End If
            Dim connectionString As String = TryCast(My.Settings.Default("nwindConnectionString"), String)
            If String.IsNullOrEmpty(connectionString) Then
                Return
            End If
            connectionString = connectionString.Replace(pathToDbTag, path)
            My.Settings.Default("nwindConnectionString") = connectionString
        End Sub
    End Class
    #End Region
    #Region "DialogHelper"
    Public Class DialogHelper
        Public Shared Function ShowQuestionDialog(ByVal message As String) As MessageBoxResult
            Return DXMessageBox.Show(message, System.Windows.Forms.Application.ProductName, MessageBoxButton.OKCancel, MessageBoxImage.Question)
        End Function
        Public Shared Sub ShowErrorDialog(ByVal message As String)
            DXMessageBox.Show(message, System.Windows.Forms.Application.ProductName, MessageBoxButton.OK, MessageBoxImage.Error)
        End Sub
    End Class
    #End Region
    #Region "FileOpenHelper"
    Public Class FileOpenHelper
        Public Shared Sub ShowFile(ByVal fileName As String)
            If Not File.Exists(fileName) Then
                Return
            End If

            If DialogHelper.ShowQuestionDialog("Do you want to open the resulting file?") = MessageBoxResult.OK Then
                Process.Start(fileName)
            End If
        End Sub
    End Class
    #End Region
    #Region "FileSaveHelper"
    Public Class FileSaveHelper
        Public Shared Function GetSaveFileName(ByVal filter As String) As String
            Dim sfDialog As New SaveFileDialog()
            sfDialog.Filter = filter
            sfDialog.FileName = "Book1"
            If sfDialog.ShowDialog() <> DialogResult.OK Then
                Return String.Empty
            End If
            Return sfDialog.FileName
        End Function
    End Class
    #End Region
End Namespace
