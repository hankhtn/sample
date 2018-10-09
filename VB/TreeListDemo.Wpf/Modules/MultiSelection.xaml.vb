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
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.Grid
Imports DevExpress.Data

Namespace TreeListDemo



    Partial Public Class MultiSelection
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
            treeList.SelectRange(0, 10)
        End Sub
        Private Sub view_CustomSummary(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.TreeList.TreeListCustomSummaryEventArgs)
            If view Is Nothing Then
                Return
            End If
            If e.SummaryProcess = CustomSummaryProcess.Start Then
                e.TotalValue = 0
            End If
            If e.SummaryProcess = CustomSummaryProcess.Calculate Then
                If view.IsRowSelected(e.Node.RowHandle) Then
                    e.TotalValue = CInt((e.TotalValue)) + 1
                End If
            End If
        End Sub
        Protected Overrides Sub Show()
            MyBase.Show()
            treeList.UpdateTotalSummary()
        End Sub
        Private Sub view_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.TreeList.TreeListSelectionChangedEventArgs)
            If treeList IsNot Nothing Then
                treeList.UpdateTotalSummary()
            End If
        End Sub
    End Class
    #Region "Converters"
    Public Class MultiSelectModeToBoolConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return Not Object.Equals(value, MultiSelectMode.None)
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return If(DirectCast(value, Boolean), MultiSelectMode.Row, MultiSelectMode.None)
        End Function
        #End Region
    End Class
    #End Region
End Namespace
