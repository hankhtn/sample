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

Namespace TreeListDemo



    Partial Public Class ConditionalFormatting
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class SalesDataViewModel
        Public Sub New()
            SalesData = SalesDataGenerator.CreateData()
        End Sub
        Private privateSalesData As IList(Of SalesData)
        Public Property SalesData() As IList(Of SalesData)
            Get
                Return privateSalesData
            End Get
            Private Set(ByVal value As IList(Of SalesData))
                privateSalesData = value
            End Set
        End Property
    End Class
End Namespace
