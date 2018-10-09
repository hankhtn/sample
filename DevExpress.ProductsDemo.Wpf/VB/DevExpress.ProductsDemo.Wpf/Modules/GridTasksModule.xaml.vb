Imports DevExpress.Data
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Grid.Native
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Printing
Imports DevExpress.Xpf.Ribbon
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace ProductsDemo.Modules



    Partial Public Class GridTasksModule
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub TableView_CellValueChanging(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.CellValueChangedEventArgs)
            e.Source.PostEditor()
        End Sub
    End Class

    Public MustInherit Class GridControlBehaviorBase
        Inherits Behavior(Of GridControl)

        Protected ViewModel As GridViewModelBase

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            ViewModel = TryCast(AssociatedObject.DataContext, GridViewModelBase)
        End Sub
    End Class
    Public Class BarManagerFlagStatusService
        Inherits Behavior(Of BarManager)

        Private ReadOnly Property ViewModel() As GridTasksModuleViewModel
            Get
                Return CType(AssociatedObject.DataContext, GridTasksModuleViewModel)
            End Get
        End Property

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler ViewModel.SelectedItemChanged, AddressOf ViewModel_SelectedItemChanged
        End Sub

        Private Sub ViewModel_SelectedItemChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Core.ValueChangedEventArgs(Of Task))
            If e.OldValue IsNot Nothing Then
                RemoveHandler e.OldValue.PropertyChanged, AddressOf Task_PropertyChanged
            End If
            If e.NewValue IsNot Nothing Then
                AddHandler e.NewValue.PropertyChanged, AddressOf Task_PropertyChanged
            End If
            UpdateFlags()
        End Sub

        Private Sub Task_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
            If e.PropertyName = "FlagStatus" Then
                UpdateFlags()
            End If
        End Sub

        Private Sub UpdateFlags()
            For Each item As BarCheckItem In AssociatedObject.Items.Where(Function(it) it.Name.StartsWith("followUp_"))
                If ViewModel.SelectedItem Is Nothing Then
                    item.IsChecked = False
                    Continue For
                End If
                item.IsChecked = item.Name.Contains(ViewModel.SelectedItem.FlagStatus.ToString())
            Next item
        End Sub

        Protected Overrides Sub OnDetaching()
            AddHandler ViewModel.SelectedItemChanged, AddressOf ViewModel_SelectedItemChanged
            MyBase.OnDetaching()
        End Sub
    End Class
    Public Class GridControlColumnsUpdateLocker
        Inherits GridControlBehaviorBase

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler ViewModel.IsLoadingChanged, AddressOf viewModel_IsLoadingChanged
        End Sub

        Private Sub viewModel_IsLoadingChanged(ByVal sender As Object, ByVal e As IsLoadingEventArgs)
            If e.IsLoading Then
                AssociatedObject.Columns.BeginUpdate()
                AssociatedObject.SortInfo.BeginUpdate()
            Else
                AssociatedObject.SortInfo.EndUpdate()
                AssociatedObject.Columns.EndUpdate()
            End If
        End Sub
        Protected Overrides Sub OnDetaching()
            RemoveHandler ViewModel.IsLoadingChanged, AddressOf viewModel_IsLoadingChanged
            MyBase.OnDetaching()
        End Sub
    End Class
    Public Class GridControlPrint
        Inherits GridControlBehaviorBase

        Private printGridControl As GridControl

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            PrintingService.PrintableControlLink = GetPrintableControlLink()
            RemoveHandler ViewModel.Print, AddressOf ViewModel_Print
            AddHandler ViewModel.Print, AddressOf ViewModel_Print
        End Sub
        Protected Overrides Sub OnDetaching()
            PrintingService.PrintableControlLink = Nothing
            RemoveHandler ViewModel.Print, AddressOf ViewModel_Print
            MyBase.OnDetaching()
        End Sub
        Private Function GetPrintableControlLink() As PrintableControlLink
            Return New PrintableControlLink(GetPrintGridControl().View)
        End Function

        Private Sub ViewModel_Print(ByVal sender As Object, ByVal e As EventArgs)
            Dim link = GetPrintableControlLink()
            Dim preview = New DocumentPreviewControl() With {.DocumentSource = link}
            link.CreateDocument(True)
            Dim previewWindow = New Window() With {.Content = preview, .Title = "Print Preview"}
            previewWindow.ShowDialog()
        End Sub

        Private Function GetPrintGridControl() As GridControl
            If TypeOf AssociatedObject.View Is TableView Then
                Return AssociatedObject
            End If
            If printGridControl Is Nothing Then
                printGridControl = TryCast(AssociatedObject.TryFindResource("printGridControl"), GridControl)
                printGridControl.DataContext = AssociatedObject.DataContext
                printGridControl.Style = TryCast(AssociatedObject.TryFindResource("gridControlMVVMStyle"), Style)
                Interaction.GetBehaviors(printGridControl).Add(New GridControlColumnsUpdateLocker())
            End If
            Return printGridControl
        End Function
    End Class

    Public Class StatusBarSummaryUpdate
        Inherits GridControlBehaviorBase

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.CustomSummary, AddressOf AssociatedObject_CustomSummary
        End Sub

        Public Shared ReadOnly CountProperty As DependencyProperty = DependencyProperty.Register("Count", GetType(Integer), GetType(StatusBarSummaryUpdate), New PropertyMetadata(Nothing))

        Public Property Count() As Integer
            Get
                Return CInt((GetValue(CountProperty)))
            End Get
            Set(ByVal value As Integer)
                SetValue(CountProperty, value)
            End Set
        End Property

        Private Sub AssociatedObject_CustomSummary(ByVal sender As Object, ByVal e As CustomSummaryEventArgs)
            Select Case e.SummaryProcess
                Case CustomSummaryProcess.Start
                    Count = 0
                Case CustomSummaryProcess.Calculate
                Case CustomSummaryProcess.Finalize
                    Count = CInt((AssociatedObject.GetTotalSummaryValue(AssociatedObject.TotalSummary(0))))
            End Select
        End Sub
        Protected Overrides Sub OnDetaching()
            RemoveHandler AssociatedObject.CustomSummary, AddressOf AssociatedObject_CustomSummary
            MyBase.OnDetaching()
        End Sub
    End Class
End Namespace
