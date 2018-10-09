Imports System
Imports System.Globalization
Imports System.IO
Imports DevExpress.Utils.Zip
Imports DevExpress.XtraSpellChecker

Namespace RichEditDemo
    Public Class SpellCheckerHelper
        Public Shared Function CreateHunspellDictionary(ByVal culture As CultureInfo) As HunspellDictionary
            Dim parts() As String = culture.Name.Split("-"c)
            Dim result As New HunspellDictionary()
            Dim zipFileStream As Stream = GetFileStream(String.Format("{0}_{1}.zip", parts(0), parts(1)))
            Dim files As InternalZipFileCollection = InternalZipArchive.Open(zipFileStream)
            Dim dictionaryStream As Stream = GetFileStream(files, ".dic")
            Dim grammarStream As Stream = GetFileStream(files, ".aff")
            Try
                result.LoadFromStream(dictionaryStream, grammarStream)
            Catch
            Finally
                dictionaryStream.Dispose()
                grammarStream.Dispose()
                zipFileStream.Dispose()
                DisposeZipFileStreams(files)
            End Try
            result.Culture = culture
            Return result
        End Function
        Private Shared Sub DisposeZipFileStreams(ByVal files As InternalZipFileCollection)
            For Each file As InternalZipFile In files
                file.FileDataStream.Dispose()
            Next file
        End Sub
        Private Shared Function GetFileStream(ByVal files As InternalZipFileCollection, ByVal name As String) As Stream
            Dim stream As Stream = files.Find(Function(file As InternalZipFile) file.FileName.IndexOf(name) >= 0).FileDataStream
            Try
                Return CreateMemoryStream(stream)
            Finally
                stream.Close()
            End Try
        End Function
        Private Shared Function GetFileStream(ByVal relativeUri As String) As Stream
            Return New FileStream(DemoUtils.GetRelativePath(relativeUri), FileMode.Open, FileAccess.Read, FileShare.Read)
        End Function
        Private Shared Function CreateMemoryStream(ByVal stream As Stream) As Stream
            Dim result As New MemoryStream()
            Do
                Dim readedByte As Integer = stream.ReadByte()
                If readedByte < 0 Then
                    Exit Do
                End If
                result.WriteByte(CByte(readedByte))
            Loop
            result.Flush()
            result.Seek(0, SeekOrigin.Begin)
            Return result
        End Function
    End Class
End Namespace
