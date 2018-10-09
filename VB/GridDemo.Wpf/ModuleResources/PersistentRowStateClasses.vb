Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Runtime.InteropServices
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input

Namespace GridDemo
    Public Class ResizeableDataRow
        Inherits Control
        Implements IResizeHelperOwner

        Private Shared ReadOnly random As New Random(1)
        Public Shared ReadOnly RowHeightProperty As DependencyProperty
        Shared Sub New()
            DefaultStyleKeyProperty.OverrideMetadata(GetType(ResizeableDataRow), New FrameworkPropertyMetadata(GetType(ResizeableDataRow)))
            RowHeightProperty = DependencyProperty.RegisterAttached("RowHeight", GetType(Double), GetType(ResizeableDataRow), New PropertyMetadata(0R))
            RowData.RowDataProperty.OverrideMetadata(GetType(ResizeableDataRow), New FrameworkPropertyMetadata(AddressOf OnScrollViewerVerticalOffsetChanged))
        End Sub
        Private Shared Sub OnScrollViewerVerticalOffsetChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, ResizeableDataRow).OnRowDataChanged(DirectCast(e.OldValue, RowData), DirectCast(e.NewValue, RowData))
        End Sub
        Public Shared Sub SetRowHeight(ByVal element As DependencyObject, ByVal value As Double)
            element.SetValue(RowHeightProperty, value)
        End Sub
        Public Shared Function GetRowHeight(ByVal element As DependencyObject) As Double
            Return DirectCast(element.GetValue(RowHeightProperty), Double)
        End Function
        Private resizeHelper As ResizeHelper
        Private ReadOnly Property RowData() As RowData
            Get
                Return DirectCast(DataContext, RowData)
            End Get
        End Property
        Private Property RowHeight() As Double
            Get
                Return GetRowHeight(RowData.RowState)
            End Get
            Set(ByVal value As Double)
                SetRowHeight(RowData.RowState, value)
            End Set
        End Property
        Public Sub New()
            resizeHelper = New ResizeHelper(Me)
        End Sub
        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            Dim resizer As Thumb = CType(GetTemplateChild("PART_Resizer"), Thumb)
            resizeHelper.Init(resizer)
            InitializeRowHeight()
        End Sub
        Private Sub OnRowDataChanged(ByVal oldValue As RowData, ByVal newValue As RowData)
            If oldValue IsNot Nothing Then
                RemoveHandler oldValue.ContentChanged, AddressOf RowData_ContentChanged
            End If
            If newValue IsNot Nothing Then
                AddHandler newValue.ContentChanged, AddressOf RowData_ContentChanged
                InitializeRowHeight()
            End If
        End Sub
        Private Sub RowData_ContentChanged(ByVal sender As Object, ByVal e As EventArgs)
            InitializeRowHeight()
        End Sub
        Private Sub InitializeRowHeight()
            If RowHeight = 0 Then
                RowHeight = 75 + 80 * random.NextDouble()
            End If
        End Sub
        #Region "IResizeHelperOwner Members"
        Private Property IResizeHelperOwner_ActualSize() As Double Implements IResizeHelperOwner.ActualSize
            Get
                Return RowHeight
            End Get
            Set(ByVal value As Double)
                RowHeight = value
            End Set
        End Property
        Private Sub IResizeHelperOwner_ChangeSize(ByVal delta As Double) Implements IResizeHelperOwner.ChangeSize
            RowHeight = Math.Min(300, Math.Max(20, RowHeight + delta))
        End Sub
        Private Sub IResizeHelperOwner_OnDoubleClick() Implements IResizeHelperOwner.OnDoubleClick
        End Sub
        Private Sub IResizeHelperOwner_SetIsResizing(ByVal isResizing As Boolean) Implements IResizeHelperOwner.SetIsResizing
        End Sub
        Private ReadOnly Property IResizeHelperOwner_SizeHelper() As SizeHelperBase Implements IResizeHelperOwner.SizeHelper
            Get
                Return VerticalSizeHelper.Instance
            End Get
        End Property
        #End Region
    End Class
    Public Class ResizeableCard
        Inherits Control

        Public Shared ReadOnly ScaleFactorProperty As DependencyProperty
        Shared Sub New()
            DefaultStyleKeyProperty.OverrideMetadata(GetType(ResizeableCard), New FrameworkPropertyMetadata(GetType(ResizeableCard)))
            ScaleFactorProperty = DependencyProperty.RegisterAttached("ScaleFactor", GetType(Double), GetType(ResizeableCard), New PropertyMetadata(1R))
        End Sub
        Public Shared Sub SetScaleFactor(ByVal element As DependencyObject, ByVal value As Double)
            element.SetValue(ScaleFactorProperty, value)

        End Sub
        Public Shared Function GetScaleFactor(ByVal element As DependencyObject) As Double
            Return DirectCast(element.GetValue(ScaleFactorProperty), Double)
        End Function
    End Class
    Public NotInheritable Class UnsafeNativeMethods

        Private Sub New()
        End Sub

        <DllImport("user32.dll", CharSet := CharSet.Auto, ExactSpelling := True)>
        Public Shared Function SetCursorPos(ByVal x As Integer, ByVal y As Integer) As Boolean
        End Function
    End Class
    Public Class CommandManagerAttachedBehavior
        Inherits Behavior(Of FrameworkElement)

        Public Shared ReadOnly CanExecuteHandlerCommandProperty As DependencyProperty
        Shared Sub New()
            CanExecuteHandlerCommandProperty = DependencyProperty.Register("CanExecuteHandlerCommand", GetType(ICommand), GetType(CommandManagerAttachedBehavior))
        End Sub
        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            CommandManager.AddCanExecuteHandler(AssociatedObject, AddressOf CanExecute)
        End Sub
        Protected Overrides Sub OnDetaching()
            CommandManager.RemoveCanExecuteHandler(AssociatedObject, AddressOf CanExecute)
            MyBase.OnDetaching()
        End Sub
        Private Sub CanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
            If CanExecuteHandlerCommand Is Nothing Then
                Return
            End If
            CanExecuteHandlerCommand.Execute(e)
        End Sub
        Public Property CanExecuteHandlerCommand() As ICommand
            Get
                Return DirectCast(GetValue(CanExecuteHandlerCommandProperty), ICommand)
            End Get
            Set(ByVal value As ICommand)
                SetValue(CanExecuteHandlerCommandProperty, value)
            End Set
        End Property
    End Class
End Namespace
