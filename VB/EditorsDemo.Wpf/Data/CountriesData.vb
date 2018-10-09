Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Collections
Imports System.Xml.Serialization
Imports DevExpress.Xpf.DemoBase
Imports System.ComponentModel
Imports DevExpress.Xpf.DemoBase.Helpers
Imports System.Reflection

Namespace EditorsDemo
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
                Dim assembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
                dataSource_Renamed = DirectCast(s.Deserialize(assembly.GetManifestResourceStream(DemoHelper.GetPath("EditorsDemo.Data.", assembly) & "Countries.xml")), List(Of Country))
                Return dataSource_Renamed
            End Get
        End Property
    End Class

    Public Class Country
        Public ReadOnly Property ActualName() As String
            Get
                Return If(NWindName, Name)
            End Get
        End Property
        Public Property Name() As String
        Public Property NWindName() As String
        Public Property Flag() As Byte()
    End Class
End Namespace
