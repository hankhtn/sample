Imports System
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Xml.Linq

Namespace TreeMapDemo
    Partial Public Class Hierarchical
        Inherits TreeMapDemoModule

        Public Sub New()
            InitializeComponent()
            DataContext = LoadDataFromXML()
        End Sub
        Private Function LoadDataFromXML() As List(Of CountryEnergyInfo)
            Dim document As XDocument = DataLoader.LoadXDocumentFromResources("/Data/EnergyStatistic.xml")
            Dim infos As New List(Of CountryEnergyInfo)()
            If document IsNot Nothing Then
                For Each element As XElement In document.Element("ArrayOfEnergyStatistic").Elements()
                    Dim energyInfo As New CountryEnergyInfo()
                    energyInfo.Country = element.Attribute("Country").Value
                    For Each energyElement As XElement In element.Elements()
                        Dim item As New EnergyStatisticItem()
                        item.TypeName = energyElement.Attribute("TypeName").Value
                        item.Value = Convert.ToDouble(energyElement.Attribute("Value").Value, CultureInfo.InvariantCulture)
                        energyInfo.EnergyStatistic.Add(item)
                    Next energyElement
                    infos.Add(energyInfo)
                Next element
            End If
            Return infos
        End Function
    End Class

    Public Class CountryEnergyInfo

        Private ReadOnly energyStatistic_Renamed As New List(Of EnergyStatisticItem)()
        Public ReadOnly Property EnergyStatistic() As List(Of EnergyStatisticItem)
            Get
                Return energyStatistic_Renamed
            End Get
        End Property
        Public Property Country() As String
    End Class

    Public Class EnergyStatisticItem
        Public Property Value() As Double
        Public Property TypeName() As String
    End Class
End Namespace
