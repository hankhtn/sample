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

Namespace DevExpress.DevAV.ViewModels
    Partial Public Class EvaluationViewModel
        Protected Overrides Function CreateEntity() As Evaluation
            Dim entity As Evaluation = MyBase.CreateEntity()
            entity.CreatedOn = Date.Now
            Return entity
        End Function
    End Class
End Namespace
