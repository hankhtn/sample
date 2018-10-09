Imports System.Collections.Generic
Imports System.IO
Imports System.Windows.Input
Imports System.Windows.Media
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports DevExpress.Xpf.LayoutControl

Namespace LayoutControlDemo
    Partial Public Class pageEmployees
        Inherits LayoutControlDemoModule

        Public Sub New()
            InitializeComponent()
            RenderOptions.SetBitmapScalingMode(lc, BitmapScalingMode.HighQuality)
        End Sub

        Private Sub GroupBox_MouseLeftButtonUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim groupBox = DirectCast(sender, GroupBox)
            groupBox.State = If(groupBox.State = GroupBoxState.Normal, GroupBoxState.Maximized, GroupBoxState.Normal)
        End Sub
    End Class

    Public Class EmployeeViewModel
        Public Sub New(ByVal employee As Employee)
            Model = employee
            ImageSource = ImageHelper.CreateImageFromStream(New MemoryStream(Model.ImageData))
        End Sub

        Public ReadOnly Property AddressLine2() As String
            Get
                Dim result As String = Model.City
                If Not String.IsNullOrEmpty(Model.StateProvinceName) Then
                    result &= ", " & Model.StateProvinceName
                End If
                If Not String.IsNullOrEmpty(Model.PostalCode) Then
                    result &= ", " & Model.PostalCode
                End If
                If Not String.IsNullOrEmpty(Model.CountryRegionName) Then
                    result &= ", " & Model.CountryRegionName
                End If
                Return result
            End Get
        End Property
        Private privateImageSource As ImageSource
        Public Property ImageSource() As ImageSource
            Get
                Return privateImageSource
            End Get
            Private Set(ByVal value As ImageSource)
                privateImageSource = value
            End Set
        End Property
        Private privateModel As Employee
        Public Property Model() As Employee
            Get
                Return privateModel
            End Get
            Private Set(ByVal value As Employee)
                privateModel = value
            End Set
        End Property
    End Class

    Public Class EmployeesViewModel
        Inherits List(Of EmployeeViewModel)

        Public Sub New()
            Me.New(EmployeesWithPhotoData.DataSource)
        End Sub
        Public Sub New(ByVal model As IEnumerable(Of Employee))
            For Each employee As Employee In model
                Add(New EmployeeViewModel(employee))
            Next employee

            Sort(AddressOf CompareByLastNameFirstName)
        End Sub

        Private Function CompareByLastNameFirstName(ByVal x As EmployeeViewModel, ByVal y As EmployeeViewModel) As Integer
            Dim value1 As String = x.Model.LastName + x.Model.FirstName
            Dim value2 As String = y.Model.LastName + y.Model.FirstName
            Return String.Compare(value1, value2)
        End Function
    End Class
End Namespace
