Imports System
Imports System.Windows.Media
Imports System.Xml.Linq
Imports System.Collections.Generic
Imports DevExpress.Xpf.TreeMap
Imports System.Windows
Imports System.Linq

Namespace TreeMapDemo
    Partial Public Class Customizing
        Inherits TreeMapDemoModule

        Public Sub New()
            InitializeComponent()
            DataContext = LoadDataFromXML()
        End Sub
        Private Function LoadDataFromXML() As List(Of OlympicMedals)
            Dim document As XDocument = DataLoader.LoadXDocumentFromResources("/Data/OlympicMedals2014.xml")
            Dim infos As New List(Of OlympicMedals)()
            If document IsNot Nothing Then
                For Each element As XElement In document.Element("ArrayOfOlympicMedals").Elements()
                    Dim medal As New OlympicMedals()
                    medal.Country = element.Element("Country").Value
                    medal.Sport = element.Element("Sport").Value.TrimEnd()
                    medal.MedalType = element.Element("MedalType").Value
                    medal.Count = Convert.ToDouble(element.Element("Count").Value)
                    medal.Athletes = GetAthletes(element.Element("Athletes"))
                    infos.Add(medal)
                Next element
            End If
            Return infos
        End Function
        Private Function GetAthletes(ByVal enumerable As XElement) As List(Of String)
            Dim athletes As New List(Of String)()
            For Each item As XElement In enumerable.Elements()
                Dim athlete As String = String.Empty
                For Each el In item.Elements()
                    athlete &= String.Format(" {0},", el.Value)
                Next el
                athletes.Add(athlete.Substring(1, athlete.Length - 2))
            Next item
            Return athletes
        End Function
    End Class

    Public Class MedalsColorizer
        Inherits TreeMapColorizerBase

        Private Class MinMaxColors
            Public Property Min() As Color
            Public Property Max() As Color
        End Class

        Private goldColors As New MinMaxColors() With {.Min = Color.FromRgb(241, 136, 24), .Max = Color.FromRgb(252, 174, 43)}
        Private silverColors As New MinMaxColors() With {.Min = Color.FromRgb(134, 134, 134), .Max = Color.FromRgb(192, 192, 192)}
        Private bronzeColors As New MinMaxColors() With {.Min = Color.FromRgb(168, 90, 41), .Max = Color.FromRgb(226, 162, 119)}

        Private Function MixColors(ByVal colors As MinMaxColors, ByVal coeff As Double) As Color
            Return Color.FromRgb(CByte(colors.Max.R * (1 - coeff) + colors.Min.R * coeff), CByte(colors.Max.G * (1 - coeff) + colors.Min.G * coeff), CByte(colors.Max.B * (1 - coeff) + colors.Min.B * coeff))
        End Function

        Public Overrides Function GetItemColor(ByVal item As TreeMapItem, ByVal group As TreeMapItemGroupInfo) As Color?
            If group.GroupLevel = 1 Then
                Return Colors.White
            End If
            If group.GroupLevel = 2 Then
                Dim delta As Double = group.MaxValue - group.MinValue
                Dim coef As Double = 1 - (group.MaxValue - item.Value) / delta
                Select Case CType(item.Tag, OlympicMedals).MedalType
                    Case "Gold"
                        Return MixColors(goldColors, coef)
                    Case "Silver"
                        Return MixColors(silverColors, coef)
                    Case "Bronze"
                        Return MixColors(bronzeColors, coef)
                    Case Else
                End Select
            End If
            Return Nothing
        End Function
        Protected Overrides Function CreateObject() As TreeMapDependencyObject
            Return New MedalsColorizer()
        End Function
    End Class

    Public Class CustomTreeMapLayoutAlgorithm
        Inherits SquarifiedLayoutAlgorithm

        Private Function SortItemsByMedalType(ByVal items As IList(Of ITreeMapLayoutItem)) As IList(Of ITreeMapLayoutItem)
            Return items.OrderByDescending(AddressOf SelectKey).ToList()
        End Function
        Private Function SelectKey(ByVal item As ITreeMapLayoutItem) As Object
            Dim medals As IList(Of Object) = TryCast(DirectCast(item, TreeMapItem).Tag, IList(Of Object))
            If medals Is Nothing OrElse medals.Count = 0 Then
                Return Nothing
            End If
            Return DirectCast(medals(0), OlympicMedals).MedalType
        End Function

        Public Overrides Sub Calculate(ByVal items As IList(Of ITreeMapLayoutItem), ByVal size As Size, ByVal groupLevel As Integer)
            MyBase.Calculate(If(groupLevel = 1, SortItemsByMedalType(items), items), size, groupLevel)
        End Sub
    End Class

    Public Class OlympicMedals
        Public Property Country() As String
        Public Property Sport() As String
        Public Property MedalType() As String
        Public Property Count() As Double
        Public Property Athletes() As List(Of String)
    End Class
End Namespace
