Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.DemoBase
Imports System.Collections
Imports DevExpress.Xpf.Core
Imports DevExpress.Utils
Imports System.Data
Imports DevExpress.Xpf.Editors
Imports DevExpress.DemoData
Imports System.Collections.Generic
Imports DevExpress.DemoData.Models
Imports EditorsDemo
Imports DevExpress.Data.Filtering

Namespace CommonDemo



    <CodeFile("ModuleResources/LookUpEditTemplates.xaml"), CodeFile("ModuleResources/LookUpEditWithMultipleSelectionTemplates.xaml"), CodeFile("ViewModels/LookUpEditorDemoViewModel.cs")>
    Partial Public Class LookUpEdit
        Inherits CommonDemoModule

        Public Sub New()
            Resources.MergedDictionaries.Add(New ResourceDictionary() With {.Source = New Uri(String.Format("/{0};component/Themes/{1}", AssemblyHelper.GetPartialName(GetType(LookUpEdit).Assembly), GenericXamlName), UriKind.Relative)})
            InitializeComponent()
            OptionsDataContext = lookUpEdit
            Initialize()
        End Sub

        Private Property ViewModel() As LookUpEditorDemoViewModel
        Private ReadOnly Property GenericXamlName() As String
            Get
                Return "Common.xaml"
            End Get
        End Property

        Private Sub Initialize()
            ComboBoxEdit.SetupComboBoxEnumItemSource(Of FindMode, FindMode)(cbFindModeComboBox)
            ComboBoxEdit.SetupComboBoxEnumItemSource(Of FilterCondition, FilterCondition)(cbFilterConditionComboBox)
            ViewModel = New LookUpEditorDemoViewModel()
            DataContext = ViewModel
        End Sub

        Private control As Control
        Private Sub LookUpEditProcessNewValue(ByVal sender As DependencyObject, ByVal e As DevExpress.Xpf.Editors.ProcessNewValueEventArgs)
            If Not CBool(ceProcessNewValue.IsChecked) Then
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
                ViewModel.ProductsSource.Add(DirectCast(control.DataContext, Product))
            End If
            control = Nothing
        End Sub
    End Class
End Namespace
