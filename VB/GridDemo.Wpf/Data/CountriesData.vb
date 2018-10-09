Imports DevExpress.Xpf.DemoBase.Helpers
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Xml.Serialization

Namespace GridDemo
    <XmlRoot("Countries")>
    Public Class CountriesData
        Inherits List(Of Country)


        Private Shared dataSource_Renamed As List(Of Country) = Nothing
        Public Shared ReadOnly Property DataSource() As List(Of Country)
            Get
                If dataSource_Renamed IsNot Nothing Then
                    Return dataSource_Renamed
                End If
                Dim s As New XmlSerializer(GetType(CountriesData))
                Dim assembly As System.Reflection.Assembly = GetType(CountriesData).Assembly
                dataSource_Renamed = DirectCast(s.Deserialize(assembly.GetManifestResourceStream(DemoHelper.GetPath("GridDemo.Data.", assembly) & "Countries.xml")), List(Of Country))
                Return dataSource_Renamed
            End Get
        End Property
    End Class

    Public Class Country
        Public ReadOnly Property ActualNWindName() As String
            Get
                Return If(NWindName, Name)
            End Get
        End Property
        Public ReadOnly Property ActualName() As String
            Get
                Return If(Name, NWindName)
            End Get
        End Property
        Public Property Name() As String
        Public Property NWindName() As String
        Public Property Flag() As Byte()
    End Class
End Namespace
