Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.DevAV.Common.Utils
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.DevAV
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.DevAV.ViewModels
Imports System.IO
Imports DevExpress.Mvvm.DataAnnotations

Namespace DevExpress.DevAV.ViewModels
    Partial Public Class ProductViewModel
        Private Shared ZoomFactors() As Double = { 0.5, 0.6, 0.7, 0.8, 0.9, 1, 2, 3, 4, 5 }
        Private zoomFactorIndex As Integer = 5

        Protected Overrides Sub OnInitializeInRuntime()
            MyBase.OnInitializeInRuntime()
            ZoomFactor = 1
        End Sub
        Public Overridable Property PdfDocument() As Stream
        Public Overridable Property ZoomFactor() As Double
        Public Overridable Sub ZoomIn()
            If zoomFactorIndex <> ZoomFactors.Count() - 1 Then
                zoomFactorIndex += 1
            End If
            ZoomFactor = ZoomFactors(zoomFactorIndex)
        End Sub
        Public Overridable Sub ZoomOut()
            If zoomFactorIndex <> 0 Then
                zoomFactorIndex -= 1
            End If
            ZoomFactor = ZoomFactors(zoomFactorIndex)
        End Sub
        Protected Overrides Function CreateEntity() As Product
            Dim entity As Product = MyBase.CreateEntity()
            entity.ProductionStart = Date.Now
            entity.CurrentInventory = 1
            Return entity
        End Function
        Protected Overrides Sub OnEntityChanged()
            MyBase.OnEntityChanged()
            PdfDocument = If(Entity IsNot Nothing AndAlso Entity.Catalog IsNot Nothing AndAlso Entity.Catalog.Count <> 0, Entity.Catalog(0).PdfStream, Nothing)
            Xpf.DemoBase.Helpers.Logger.Log("HybridApp: View Product")
        End Sub
    End Class
End Namespace
