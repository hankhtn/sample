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
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Core
Imports System.Collections
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports System.Data

Namespace EditorsDemo
    Partial Public Class SearchLookUpEditModule
        Inherits EditorsDemoModule

        Private dialogOwner As IDialogOwner
        Private control As Control

        Private ReadOnly Property ViewModel() As CollectionViewViewModel
            Get
                Return CType(Resources("viewModel"), CollectionViewViewModel)
            End Get
        End Property
        Private ReadOnly Property NewItemRowID() As Integer
            Get
                Return Employees.Count + 1
            End Get
        End Property
        Private ReadOnly Property Employees() As IList
            Get
                Return ViewModel.Employees
            End Get
        End Property
        Private Property IsRecordValid() As Boolean

        Public Sub New()
            InitializeComponent()
            Me.options.FocusedEditor = searchLookUpEdit
            AddHandler searchLookUpEdit.ProcessNewValue, AddressOf searchLookUpEdit_ProcessNewValue
            AddHandler Unloaded, AddressOf SearchLookUpEditModule_Unloaded
        End Sub

        Private Sub SearchLookUpEditModule_Unloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If dialogOwner Is Nothing Then
                Return
            End If
            dialogOwner.CloseDialog(False)
        End Sub

        Private Sub searchLookUpEdit_ProcessNewValue(ByVal sender As DependencyObject, ByVal e As DevExpress.Xpf.Editors.ProcessNewValueEventArgs)
            If control IsNot Nothing Then
                Return
            End If
            control = New ContentControl With {.Template = CType(Resources("addNewRecordTemplate"), ControlTemplate)}
            Dim row As New Employee()
            row.LastName = e.DisplayText
            row.Id = NewItemRowID
            row.BirthDate = Date.Now.AddYears(-21).Date

            control.DataContext = row
            Dim owner As FrameworkElement = TryCast(sender, FrameworkElement)
            Dim closeHandler As DialogClosedDelegate
            closeHandler = Sub(close? As Boolean)
                If close IsNot Nothing AndAlso CBool(close) Then
                    Employees.Add(control.DataContext)
                End If
                control = Nothing
            End Sub
            dialogOwner = FloatingContainer.ShowDialogContent(control, owner, Size.Empty, New FloatingContainerParameters() With {.Title = "Add New Record", .AllowSizing = False, .ClosedDelegate = closeHandler, .ContainerFocusable = False, .ShowModal = False})
            e.PostponedValidation = True
            e.Handled = True
            AddHandler DirectCast(dialogOwner, FloatingContainer).Hiding, AddressOf SearchLookUpEditModule_Closing
        End Sub
        Private Sub SearchLookUpEditModule_Closing(ByVal sender As Object, ByVal e As CancelRoutedEventArgs)
            Dim result? As Boolean = DirectCast(sender, FloatingContainer).DialogResult
            If result Is Nothing OrElse (Not CBool(result)) Then
                Return
            End If
            e.Cancel = Not GetRecordValid()
        End Sub
        Private Sub CloseAddNewRecordHandler(ByVal close? As Boolean)
            If close IsNot Nothing AndAlso CBool(close) Then
                Employees.Add(control.DataContext)
            End If
            control = Nothing
        End Sub
        Private Sub Validate(ByVal sender As Object, ByVal e As ValidationEventArgs)
            If TypeOf e.Value Is String AndAlso String.IsNullOrEmpty(TryCast(e.Value, String)) OrElse e.Value Is Nothing Then
                e.IsValid = False
                e.ErrorContent = "Please, input the field"
            End If
        End Sub
        Private Function GetRecordValid() As Boolean
            If control Is Nothing Then
                Return False
            End If
            Dim employee As Employee = TryCast(control.DataContext, Employee)
            If employee Is Nothing Then
                Return False
            End If
            Return (Not Object.Equals(employee.BirthDate, Nothing)) AndAlso (Not String.IsNullOrEmpty(employee.FirstName)) AndAlso (Not String.IsNullOrEmpty(employee.LastName)) AndAlso Not String.IsNullOrEmpty(employee.JobTitle)
        End Function
    End Class
End Namespace
