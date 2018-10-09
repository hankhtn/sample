Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace ChartsDemo
    Public Class NestedDonutViewModel
        Private Const AgeDataMember As String = "Age"
        Private Const GenderDataMember As String = "Sex"

        Public Overridable Property ArgumentDataMember() As String
        Public Overridable Property SeriesDataMember() As String
        Public Overridable Property DemoTitle() As String
        Public Overridable Property HintDataMember() As String
        Private privateDataSource As Object
        Public Property DataSource() As Object
            Get
                Return privateDataSource
            End Get
            Private Set(ByVal value As Object)
                privateDataSource = value
            End Set
        End Property

        Public Sub New()
            DataSource = AgeStructure.GetPopulationAgeStructure()
            ArgumentDataMember = AgeDataMember
        End Sub

        Protected Sub OnArgumentDataMemberChanged()
            HintDataMember = If(ArgumentDataMember = AgeDataMember, GenderDataMember, AgeDataMember)
            SeriesDataMember = "Country" & HintDataMember & "Key"
            DemoTitle = "Population: " & ArgumentDataMember & " Structure"
        End Sub
    End Class
End Namespace
