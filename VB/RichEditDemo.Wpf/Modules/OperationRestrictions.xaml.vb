Imports DevExpress.Xpf.PropertyGrid

Namespace RichEditDemo
    Partial Public Class OperationRestrictions
        Inherits RichEditDemoModule

        Public Sub New()
            InitializeComponent()
            Me.propertyGridControl.SelectedObject = New RichEditOptionsProvider(Me.richEdit.Options)
        End Sub

        Private Sub PropertyGridControl_CellValueChanged(ByVal sender As Object, ByVal args As CellValueChangedEventArgs)
            If args.Row.FullPath.Contains("DocumentCapabilities") Then
                RichEditControl.LoadDocument(RichEditControl.DocumentSaveOptions.CurrentFileName)
            End If
        End Sub
    End Class
End Namespace
