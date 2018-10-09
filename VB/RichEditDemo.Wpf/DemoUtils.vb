Imports System.IO
Imports DevExpress.DemoData.Helpers

Namespace RichEditDemo
    Public Class DemoUtils
        Public Shared Function GetRelativePath(ByVal name As String) As String
            name = "Data\" & name
            Dim path As String = DataFilesHelper.DataDirectory
            Dim s As String = "\"
            For i As Integer = 0 To 10
                If File.Exists(path & s & name) Then
                    Return System.IO.Path.GetFullPath(path & s & name)
                Else
                    s &= "..\"
                End If
            Next i
            Return ""
        End Function
    End Class
End Namespace
