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
Imports DevExpress.Xpf.DemoBase.DataClasses

Namespace EditorsDemo

    Partial Public Class TokenLookUpModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            DataContext = New TokenLookUpSource()
        End Sub
    End Class
    Public Class TokenLookUpSource
        Private viewModel As New CollectionViewViewModel()
        Public ReadOnly Property Source() As Object
            Get
                Return viewModel
            End Get
        End Property


        Private selectedEmployees_Renamed As List(Of Object)
        Public ReadOnly Property SelectedEmployees() As Object
            Get
                If selectedEmployees_Renamed Is Nothing Then
                    selectedEmployees_Renamed = CreateSelectedEmployees()
                End If
                Return selectedEmployees_Renamed
            End Get
        End Property

        Private selectedEmployees2_Renamed As List(Of Object)
        Public ReadOnly Property SelectedEmployees2() As Object
            Get
                If selectedEmployees2_Renamed Is Nothing Then
                    selectedEmployees2_Renamed = CreateSelectedEmployees2()
                End If
                Return selectedEmployees2_Renamed
            End Get
        End Property

        Private selectedEmployees3_Renamed As List(Of Object)
        Public ReadOnly Property SelectedEmployees3() As Object
            Get
                If selectedEmployees3_Renamed Is Nothing Then
                    selectedEmployees3_Renamed = CreateSelectedCountries3()
                End If
                Return selectedEmployees3_Renamed
            End Get
        End Property

        Private Function CreateSelectedCountries3() As List(Of Object)
            Return New List(Of Object)() From {viewModel.Employees(0), viewModel.Employees(1), viewModel.Employees(12), viewModel.Employees(5), viewModel.Employees(7), viewModel.Employees(3), viewModel.Employees(10), viewModel.Employees(15), viewModel.Employees(21), viewModel.Employees(25), viewModel.Employees(29), viewModel.Employees(30), viewModel.Employees(40), viewModel.Employees(22), viewModel.Employees(54), viewModel.Employees(20), viewModel.Employees(31), viewModel.Employees(37), viewModel.Employees(43), viewModel.Employees(49), viewModel.Employees(4), viewModel.Employees(6), viewModel.Employees(11), viewModel.Employees(33), viewModel.Employees(32), viewModel.Employees(19), viewModel.Employees(14), viewModel.Employees(23), viewModel.Employees(27), viewModel.Employees(38)}
        End Function

        Private Function CreateSelectedEmployees2() As List(Of Object)
            Return New List(Of Object)() From {viewModel.Employees(0), viewModel.Employees(1), viewModel.Employees(12), viewModel.Employees(5), viewModel.Employees(7), viewModel.Employees(3)}
        End Function
        Private Function CreateSelectedEmployees() As List(Of Object)
            Return New List(Of Object)() From {viewModel.Employees(7), viewModel.Employees(3), viewModel.Employees(10)}
        End Function
    End Class
End Namespace
