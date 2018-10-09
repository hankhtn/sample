Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Grid
Imports System.Windows
Imports System.Windows.Controls



Namespace GridDemo
    Public Class DemoColumnChooserControl
        Inherits Control

        Public Shared ReadOnly ViewProperty As DependencyProperty

        Shared Sub New()
            ViewProperty = DependencyProperty.Register("View", GetType(GridViewBase), GetType(DemoColumnChooserControl), New PropertyMetadata(Nothing))
        End Sub
        Public Sub New()
            DefaultStyleKey = GetType(DemoColumnChooserControl)
        End Sub

        Public Property View() As GridViewBase
            Get
                Return DirectCast(GetValue(ViewProperty), GridViewBase)
            End Get
            Set(ByVal value As GridViewBase)
                SetValue(ViewProperty, value)
            End Set
        End Property

        Private privateColunmChooserControl As ColumnChooserControl
        Friend Property ColunmChooserControl() As ColumnChooserControl
            Get
                Return privateColunmChooserControl
            End Get
            Private Set(ByVal value As ColumnChooserControl)
                privateColunmChooserControl = value
            End Set
        End Property

        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            ColunmChooserControl = CType(GetTemplateChild("PART_ColumnChooserControl"), ColumnChooserControl)
        End Sub
    End Class
    Public Class DemoColumnChooser
        Implements IColumnChooser, IColumnChooserFactory

        Private ReadOnly columnChooserControl As DemoColumnChooserControl
        Public Sub New(ByVal columnChooserControl As DemoColumnChooserControl)
            Me.columnChooserControl = columnChooserControl
        End Sub
        #Region "IColumnChooser Members"
        Private Sub IColumnChooser_Show() Implements IColumnChooser.Show
        End Sub
        Private Sub IColumnChooser_Hide() Implements IColumnChooser.Hide
        End Sub
        Private Sub IColumnChooser_ApplyState(ByVal state As IColumnChooserState) Implements IColumnChooser.ApplyState
        End Sub
        Private Sub IColumnChooser_SaveState(ByVal state As IColumnChooserState) Implements IColumnChooser.SaveState
        End Sub
        Private Sub IColumnChooser_Destroy() Implements IColumnChooser.Destroy
        End Sub
        Private ReadOnly Property IColumnChooser_TopContainer() As UIElement Implements IColumnChooser.TopContainer
            Get
                Return columnChooserControl.ColunmChooserControl
            End Get
        End Property
        #End Region

        #Region "IColumnChooserFactory Members"
        Private Function IColumnChooserFactory_Create(ByVal owner As Control) As IColumnChooser Implements IColumnChooserFactory.Create
            Return Me
        End Function
        #End Region
    End Class
End Namespace
