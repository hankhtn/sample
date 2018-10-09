Imports System
Imports System.IO
Imports System.Windows
Imports System.Windows.Threading
Imports DevExpress.DemoData.Helpers
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo
    Partial Public Class OLAPKPI
        Inherits PivotGridDemoModule


        Private Shared sampleFileName_Renamed As String

        Public Sub New()
            InitializeComponent()
            pivotGrid.OlapConnectionString = "Provider=msolap;Data Source=" & SampleFileName & ";Initial Catalog=Adventure Works;Cube Name=Adventure Works"
        End Sub

        Private Shared ReadOnly Property SampleFileName() As String
            Get
                If String.IsNullOrEmpty(sampleFileName_Renamed) Then
                    sampleFileName_Renamed = Path.GetFullPath(DataFilesHelper.FindFile("AdventureWorks.cub", DataFilesHelper.DataPath))
                    If File.Exists(sampleFileName_Renamed) Then
                        Try
                            File.SetAttributes(sampleFileName_Renamed, FileAttributes.Normal)
                        Catch
                        End Try
                    End If
                End If
                Return sampleFileName_Renamed
            End Get
        End Property
    End Class
End Namespace
