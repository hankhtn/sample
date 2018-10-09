Imports DevExpress.Internal
Imports System.IO

Namespace DialogsDemo.Helpers
    Public NotInheritable Class DialogsDemoHelper

        Private Sub New()
        End Sub

        Public Shared Function GetDialogsDataPath(ByVal relativePath As String) As String
            Return Path.GetFullPath(DataDirectoryHelper.GetFile("Dialogs\" & relativePath, DataDirectoryHelper.DataFolderName))
        End Function
        Public Shared Function GetPhotosPath(ByVal relativePath As String) As String
            Return Path.GetFullPath(DataDirectoryHelper.GetFile("Photos\" & relativePath, DataDirectoryHelper.DataFolderName))
        End Function
        Public Shared Function GetDataStream(ByVal dataFileName As String) As Stream
            Dim path As String = GetDialogsDataPath(dataFileName)
            Return File.OpenRead(path)
        End Function
    End Class
End Namespace
