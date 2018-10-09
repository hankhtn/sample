Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.DemoBase.DataClasses

Namespace EditorsDemo
    Partial Public Class TokenComboBoxModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            DataContext = New TokenComboBoxSource()
        End Sub
    End Class
    Public Class TokenComboBoxSource
        Private context As NWindContext = NWindContext.Create()
        Public ReadOnly Property Countries() As Object
            Get
                Return context.CountriesArray
            End Get
        End Property


        Private selectedCountries_Renamed As List(Of Object)
        Public ReadOnly Property SelectedCountries() As Object
            Get
                If selectedCountries_Renamed Is Nothing Then
                    selectedCountries_Renamed = CreateSelectedCountries()
                End If
                Return selectedCountries_Renamed
            End Get
        End Property

        Private selectedCountries2_Renamed As List(Of Object)
        Public ReadOnly Property SelectedCountries2() As Object
            Get
                If selectedCountries2_Renamed Is Nothing Then
                    selectedCountries2_Renamed = CreateSelectedCountries2()
                End If
                Return selectedCountries2_Renamed
            End Get
        End Property

        Private selectedCountries3_Renamed As List(Of Object)
        Public ReadOnly Property SelectedCountries3() As Object
            Get
                If selectedCountries3_Renamed Is Nothing Then
                    selectedCountries3_Renamed = CreateSelectedCountries3()
                End If
                Return selectedCountries3_Renamed
            End Get
        End Property

        Private Function CreateSelectedCountries3() As List(Of Object)
            Return New List(Of Object)() From {context.CountriesArray(0), context.CountriesArray(1), context.CountriesArray(12), context.CountriesArray(5), context.CountriesArray(7), context.CountriesArray(3), context.CountriesArray(10), context.CountriesArray(15), context.CountriesArray(21), context.CountriesArray(25), context.CountriesArray(29), context.CountriesArray(30), context.CountriesArray(90), context.CountriesArray(40), context.CountriesArray(22), context.CountriesArray(54), context.CountriesArray(20), context.CountriesArray(31), context.CountriesArray(37), context.CountriesArray(43), context.CountriesArray(49), context.CountriesArray(63), context.CountriesArray(4), context.CountriesArray(6), context.CountriesArray(60), context.CountriesArray(61), context.CountriesArray(65), context.CountriesArray(70), context.CountriesArray(74), context.CountriesArray(76), context.CountriesArray(71), context.CountriesArray(73)}
        End Function

        Private Function CreateSelectedCountries2() As List(Of Object)
            Return New List(Of Object)() From {context.CountriesArray(0), context.CountriesArray(1), context.CountriesArray(12), context.CountriesArray(5), context.CountriesArray(7), context.CountriesArray(3), context.CountriesArray(10), context.CountriesArray(15), context.CountriesArray(2), context.CountriesArray(8)}
        End Function
        Private Function CreateSelectedCountries() As List(Of Object)
            Return New List(Of Object)() From {context.CountriesArray(7), context.CountriesArray(3), context.CountriesArray(10)}
        End Function
    End Class
End Namespace
