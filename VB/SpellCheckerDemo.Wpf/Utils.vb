Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Xml.Serialization
Imports DevExpress.Utils
Imports DevExpress.Utils.Zip
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.DemoBase.Helpers.TextColorizer
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.Xpf.SpellChecker
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraSpellChecker

Namespace SpellCheckerDemo
    Public Class DemoUtils
        Public Shared ReadOnly PathToDemoData As String = "SpellCheckerDemo.Data"
        Public Shared ReadOnly PathToDictionaries As String = PathToDemoData & ".Dictionaries"

        Public Shared Function GetPathToResource(ByVal path As String, ByVal name As String) As String
            If DemoHelper.GetDemoLanguage(GetType(DemoUtils).Assembly) = CodeLanguage.CS Then
                Return String.Format("{0}.{1}", path, name)
            Else
                Return name
            End If
        End Function
        Public Shared Function GetDataStream(ByVal path As String, ByVal name As String) As Stream
            Dim fullPath As String = DemoUtils.GetPathToResource(path, name)
            If Not String.IsNullOrEmpty(fullPath) Then
                Return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(fullPath)
            End If
            Return Nothing
        End Function
    End Class

    Public NotInheritable Class SpellCheckerHelper

        Private Sub New()
        End Sub

        Public Shared Sub RegisterDefaultDictionaries(ByVal spellChecker As SpellChecker)
            spellChecker.Dictionaries.Add(CustomDictionary)
        End Sub
        Public Shared Sub RegisterHunspellDictionaries(ByVal spellChecker As SpellChecker)
            spellChecker.Dictionaries.Add(CreateHunspellDictionaries(New CultureInfo("en-US")))
            spellChecker.Dictionaries.Add(CreateHunspellDictionaries(New CultureInfo("de-DE")))
            spellChecker.Dictionaries.Add(CreateHunspellDictionaries(New CultureInfo("ru-RU")))
        End Sub
        Private Shared Function CreateHunspellDictionaries(ByVal culture As CultureInfo) As HunspellDictionary
            Dim parts() As String = culture.Name.Split("-"c)
            Dim result As New HunspellDictionary()
            Dim uriPath As String = String.Format("pack://application:,,,/SpellCheckerDemo;component//Data/Dictionaries/{0}/", parts(0))
            Dim dictionaryStream As Stream = Application.GetResourceStream(New Uri(String.Format("{0}{1}_{2}.dic", uriPath, parts(0), parts(1)))).Stream
            Dim grammarStream As Stream = Application.GetResourceStream(New Uri(String.Format("{0}{1}_{2}.aff", uriPath, parts(0), parts(1)))).Stream
            Try
                result.LoadFromStream(dictionaryStream, grammarStream)
            Catch
            Finally
                dictionaryStream.Close()
                grammarStream.Close()
            End Try
            result.Culture = culture
            Return result
        End Function
        Private Shared Function GetFileStream(ByVal files As InternalZipFileCollection, ByVal name As String) As Stream
            Dim stream As Stream = files.Find(Function(file As InternalZipFile) file.FileName.IndexOf(name) >= 0).FileDataStream
            Try
                Return CreateMemoryStream(stream)
            Finally
                stream.Close()
            End Try
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

        Public Shared ReadOnly Property DefaultDictionary() As ISpellCheckerDictionary
            Get
                Return CreateHunspellDictionaries(New CultureInfo("en-US"))
            End Get
        End Property
        Public Shared ReadOnly Property CustomDictionary() As ISpellCheckerDictionary
            Get
                Dim result As New SpellCheckerCustomDictionary()
                Dim dictionaryStream As Stream = DemoUtils.GetDataStream(DemoUtils.PathToDictionaries, "CustomEnglish.dic")
                Dim alphabetStream As Stream = DemoUtils.GetDataStream(DemoUtils.PathToDictionaries, "EnglishAlphabet.txt")
                Try
                    result.Load(dictionaryStream, alphabetStream)
                Catch
                Finally
                    dictionaryStream.Close()
                    alphabetStream.Close()
                End Try
                result.Culture = New CultureInfo("en-US")
                Return result

            End Get
        End Property
    End Class
    Public Class DocumentLoadHelper
        Public Shared Function Load(ByVal fileName As String) As Stream
            Dim path As String = DemoUtils.GetPathToResource(DemoUtils.PathToDemoData, fileName)
            Return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(path)
        End Function
    End Class

    Public Class Employees
        Implements System.ComponentModel.INotifyPropertyChanged

        Public Property EmployeeID() As Integer
        Public Property LastName() As String
        Public Property FirstName() As String
        Public Property Title() As String
        Public Property TitleOfCourtesy() As String
        Public Property BirthDate() As Date
        Public Property HireDate() As Date
        Public Property Address() As String
        Public Property City() As String
        Public Property Region() As String
        Public Property PostalCode() As String
        Public Property Country() As String
        Public Property HomePhone() As String
        Public Property Extension() As String
        Public Property Salary() As Double
        Public Property OnVacation() As Boolean
        Public Property Photo() As Byte()
        Public Property Notes() As String
        Public Property ReportsTo() As Integer

        #Region "INotifyPropertyChanged Members"
        Private onPropertyChanged As System.ComponentModel.PropertyChangedEventHandler
        Public Custom Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
            AddHandler(ByVal value As System.ComponentModel.PropertyChangedEventHandler)
                onPropertyChanged = DirectCast(System.Delegate.Combine(onPropertyChanged, value), System.ComponentModel.PropertyChangedEventHandler)
            End AddHandler
            RemoveHandler(ByVal value As System.ComponentModel.PropertyChangedEventHandler)
                onPropertyChanged = DirectCast(System.Delegate.Remove(onPropertyChanged, value), System.ComponentModel.PropertyChangedEventHandler)
            End RemoveHandler
            RaiseEvent(ByVal sender As System.Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
                If onPropertyChanged IsNot Nothing Then
                    For Each d As System.ComponentModel.PropertyChangedEventHandler In onPropertyChanged.GetInvocationList()
                        d.Invoke(sender, e)
                    Next d
                End If
            End RaiseEvent
        End Event
        #End Region
    End Class

    Public NotInheritable Class EmployeesData

        Private Sub New()
        End Sub

        Private Shared dataSource_Renamed As IList

        Public Shared ReadOnly Property DataSource() As IList
            Get
                If dataSource_Renamed Is Nothing Then
                    dataSource_Renamed = GetDataSource()
                    MakeMistakes(dataSource_Renamed)
                End If
                Return dataSource_Renamed
            End Get
        End Property
        Private Shared Function GetDataSource() As IList
            Dim s As New XmlSerializer(GetType(List(Of Employees)), New XmlRootAttribute("NewDataSet"))
            Dim stream As Stream = GetType(EmployeesData).Assembly.GetManifestResourceStream(DemoUtils.GetPathToResource(DemoUtils.PathToDemoData, "nwind.xml"))
            Return DirectCast(s.Deserialize(stream), IList)
        End Function
        Private Shared Sub MakeMistakes(ByVal dataSet As IList)
            For Each employee As Employees In dataSet
                Dim text As New StringBuilder(employee.Notes)
                Dim charSet As List(Of Char) = CreateCharSet(text)
                Dim random As New Random(Environment.TickCount)
                For i As Integer = text.Length - 1 To 0 Step -30
                    If Not Char.IsLetter(text(i)) Then
                        Continue For
                    End If
                    Dim ch As Char = GetRandomChar(charSet)
                    If Char.IsUpper(text(i)) Then
                        ch = Char.ToUpper(ch)
                    End If
                    If text(i) = ch Then
                        text.Remove(i, 1)
                    Else
                        text(i) = ch
                    End If
                Next i
                employee.Notes = text.ToString()
            Next employee
        End Sub
        Private Shared Function CreateCharSet(ByVal text As StringBuilder) As List(Of Char)
            Dim result As New List(Of Char)()
            Dim length As Integer = text.Length
            For i As Integer = 0 To length - 1
                Dim ch As Char = text(i)
                If Not Char.IsLetter(ch) Then
                    Continue For
                End If
                ch = Char.ToLower(ch)
                Dim index As Integer = result.BinarySearch(ch)
                If index < 0 Then
                    result.Insert((Not index), ch)
                End If
            Next i
            Return result
        End Function
        Private Shared Function GetRandomChar(ByVal charSet As List(Of Char)) As Char
            Dim random As New Random(Environment.TickCount)
            Dim index As Integer = random.Next(0, charSet.Count - 1)
            Return charSet(index)
        End Function
    End Class
End Namespace
