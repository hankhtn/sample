Imports DevExpress.DemoData
Imports DevExpress.DemoData.Models
Imports DevExpress.Utils
Imports DevExpress.Xpf.Core
Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls


Namespace CommonDemo



    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/MultiColumnLookupEditorTemplates.xaml")>
    Partial Public Class StandaloneMultiColumnLookupEditor
        Inherits CommonDemoModule

        Private Property NWind() As NWindDataLoader
        Private ReadOnly Property Products() As IList(Of Product)
            Get
                Return DirectCast(lookUpEdit.DataContext, IList(Of Product))
            End Get
        End Property
        Public Sub New()
            InitializeComponent()
            OptionsDataContext = lookUpEdit
            NWind = TryCast(Resources("NWindDataLoader"), NWindDataLoader)
        End Sub
        Private control As Control
        Private Sub lookUpEdit_ProcessNewValue(ByVal sender As DependencyObject, ByVal e As DevExpress.Xpf.Editors.ProcessNewValueEventArgs)
            If Not CBool(chProcessNewValue.IsChecked) Then
                Return
            End If

            control = New ContentControl With {.Template = CType(Resources("addNewRecordTemplate"), ControlTemplate), .Tag = e}
            Dim row As New Product()
            row.ProductName = e.DisplayText
            control.DataContext = row
            Dim owner As FrameworkElement = TryCast(sender, FrameworkElement)
            Dim closeHandler As DialogClosedDelegate = AddressOf CloseAddNewRecordHandler

            FloatingContainer.ShowDialogContent(control, owner, Size.Empty, New FloatingContainerParameters() With {.Title = "Add New Record", .AllowSizing = False, .ClosedDelegate = closeHandler, .ContainerFocusable = False, .ShowModal = True})

            e.PostponedValidation = True
            e.Handled = True
        End Sub

        Private Sub CloseAddNewRecordHandler(ByVal close? As Boolean)
            If close.HasValue AndAlso close.Value Then
                Products.Add(DirectCast(control.DataContext, Product))
            End If
            control = Nothing
        End Sub
    End Class
End Namespace
