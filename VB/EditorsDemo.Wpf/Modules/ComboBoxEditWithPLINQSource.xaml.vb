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
Imports DevExpress.Data.PLinq.Helpers
Imports GridDemo

Namespace EditorsDemo
    Partial Public Class ComboBoxEditWithPLINQSource
        Inherits EditorsDemoModule

        Public Sub New()
            PLinqServerModeCore.DefaultForceCaseInsensitiveForAnySource = True
            Dim viewModel As New PLinqInstantFeedbackDemoViewModel(False)
            viewModel.CountItems = New CountItemCollection() From {
                New CountItem() With {.Count = 100000, .DisplayName = "Small"},
                New CountItem() With {.Count=1000000, .DisplayName="Medium"},
                New CountItem() With {.Count=3000000, .DisplayName="Large"}
            }
            viewModel.SelectedCountItem = viewModel.CountItems(1)
            DataContext = viewModel
            InitializeComponent()
        End Sub
    End Class
End Namespace
