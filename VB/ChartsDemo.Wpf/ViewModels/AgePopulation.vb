Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Xml.Linq

Namespace ChartsDemo
    Public Class AgePopulation

        Private name_Renamed As String

        Private age_Renamed As String

        Private sex_Renamed As String

        Private population_Renamed As Double

        Public ReadOnly Property Name() As String
            Get
                Return name_Renamed
            End Get
        End Property
        Public ReadOnly Property Age() As String
            Get
                Return age_Renamed
            End Get
        End Property
        Public ReadOnly Property Sex() As String
            Get
                Return sex_Renamed
            End Get
        End Property
        Public ReadOnly Property SexAgeKey() As String
            Get
                Return sex_Renamed.ToString() & ": " & age_Renamed
            End Get
        End Property
        Public ReadOnly Property CountryAgeKey() As String
            Get
                Return name_Renamed & ": " & age_Renamed
            End Get
        End Property
        Public ReadOnly Property CountrySexKey() As String
            Get
                Return name_Renamed & ": " & sex_Renamed.ToString()

            End Get
        End Property
        Public ReadOnly Property Population() As Double
            Get
                Return population_Renamed
            End Get
        End Property

        Public Sub New(ByVal name As String, ByVal age As String, ByVal gender As String, ByVal population As Double)
            Me.name_Renamed = name
            Me.age_Renamed = age
            Me.sex_Renamed = gender
            Me.population_Renamed = population
        End Sub
    End Class

End Namespace
