Imports System.Collections.Generic
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.TreeMap
Imports System.Xml

Namespace TreeMapDemo
    Partial Public Class Grouping
        Inherits TreeMapDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    <POCOViewModel>
    Public Class GrouppingViewModel
        Inherits ViewModelBase


        Private Shared instance_Renamed As GrouppingViewModel

        Public Shared ReadOnly Property Instance() As GrouppingViewModel
            Get
                If instance_Renamed Is Nothing Then
                    instance_Renamed = ViewModelSource.Create(Function() New GrouppingViewModel())
                End If
                Return instance_Renamed
            End Get
        End Property

        Public Overridable Property BillionaresInfos() As XmlDocument
        Public Overridable Property ValueMembers() As List(Of String)
        Public Overridable Property GroupDefinitionSettings() As List(Of GroupDefinitionSettings)
        Public Overridable Property GroupDefinition() As GroupDefinitionSettings

        Protected Sub New()
            BillionaresInfos = DataLoader.LoadXmlDocumentFromResources("/Data/Billionares.xml")
            GroupDefinitionSettings = GetGroupDefinitionSettings()
            GroupDefinition = GroupDefinitionSettings(3)
        End Sub

        Private Function GetGroupDefinitionSettings() As List(Of GroupDefinitionSettings)
            Dim GroupByResidence As New GroupDefinitionCollection()
            GroupByResidence.Add(New TreeMapGroupDefinition() With {.GroupDataMember = "Residence"})
            Dim GroupByAgeCategory As New GroupDefinitionCollection()
            GroupByAgeCategory.Add(New TreeMapGroupDefinition() With {.GroupDataMember = "AgeCategory"})
            Dim GroupByResidenceAndAgeCategory As New GroupDefinitionCollection()
            GroupByResidenceAndAgeCategory.Add(New TreeMapGroupDefinition() With {.GroupDataMember = "Residence"})
            GroupByResidenceAndAgeCategory.Add(New TreeMapGroupDefinition() With {.GroupDataMember = "AgeCategory"})
            Return New List(Of GroupDefinitionSettings)() From {
                New GroupDefinitionSettings() With {.CollectionGroupDefinitionName = "Without Groupping", .ColorizeGroups = False, .CollectionGroupDefinition = New GroupDefinitionCollection()},
                New GroupDefinitionSettings() With {.CollectionGroupDefinitionName = "Group By Residence", .ColorizeGroups = True, .CollectionGroupDefinition = GroupByResidence},
                New GroupDefinitionSettings() With {.CollectionGroupDefinitionName = "Group By Age Category", .ColorizeGroups = True, .CollectionGroupDefinition = GroupByAgeCategory},
                New GroupDefinitionSettings() With {.CollectionGroupDefinitionName = "Group By Residence And Age Category", .ColorizeGroups = True, .CollectionGroupDefinition = GroupByResidenceAndAgeCategory}
            }
        End Function
    End Class

    Public Class GroupDefinitionSettings
        Public Property CollectionGroupDefinition() As GroupDefinitionCollection
        Public Property CollectionGroupDefinitionName() As String
        Public Property ColorizeGroups() As Boolean

        Public Overrides Function ToString() As String
            Return CollectionGroupDefinitionName
        End Function
    End Class
End Namespace
