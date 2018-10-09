Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Resources

Namespace ChartsDemo
    Public NotInheritable Class StarsData

        Private Sub New()
        End Sub

        Private Shared starsData As List(Of StarData)
        Public Shared ReadOnly Property Data() As List(Of StarData)
            Get
                If starsData IsNot Nothing Then
                    Return starsData
                Else
                    starsData = GetStarsData()
                    Return starsData
                End If
            End Get
        End Property

        Private Shared Function GetStarsData() As List(Of StarData)
            Dim fileName As String = "starsdata.csv"
            Dim dataSource = New List(Of StarData)()
            Try
                Dim path As String = "/ChartsDemo;component/Data/" & fileName
                Dim info As StreamResourceInfo = Application.GetResourceStream(New Uri(path, UriKind.RelativeOrAbsolute))
                Using reader As New StreamReader(info.Stream)
                    Do While Not reader.EndOfStream
                        Dim line As String = reader.ReadLine()
                        Dim values = line.Split(";"c)

                        Dim data_Renamed As New StarData()
                        data_Renamed.HipID = Integer.Parse(values(0), CultureInfo.InvariantCulture)
                        data_Renamed.Spectr = values(1)
                        data_Renamed.CI = Double.Parse(values(2), CultureInfo.InvariantCulture)
                        data_Renamed.X = Double.Parse(values(3), CultureInfo.InvariantCulture)
                        data_Renamed.Y = Double.Parse(values(4), CultureInfo.InvariantCulture)
                        data_Renamed.Z = Double.Parse(values(5), CultureInfo.InvariantCulture)
                        data_Renamed.Lum = Double.Parse(values(6), CultureInfo.InvariantCulture)
                        dataSource.Add(data_Renamed)
                    Loop
                End Using
            Catch
                Throw New Exception("It's impossible to load " & fileName)
            End Try
            Return dataSource
        End Function
    End Class

    Public Structure StarData
        Public Property HipID() As Integer
        Public Property Spectr() As String
        Public Property Lum() As Double
        Public Property CI() As Double
        Public Property X() As Double
        Public Property Y() As Double
        Public Property Z() As Double
    End Structure
End Namespace
