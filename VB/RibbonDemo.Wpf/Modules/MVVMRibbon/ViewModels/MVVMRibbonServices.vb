Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Ribbon
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace RibbonDemo
    Public Interface IRibbonMergeingService
        Sub Remerge()
    End Interface
    Public Class RibbonMergeingService
        Inherits ServiceBaseGeneric(Of RibbonControl)
        Implements IRibbonMergeingService

        Public Sub Remerge() Implements IRibbonMergeingService.Remerge
            DirectCast(If(AssociatedObject.ActualMergedParent, AssociatedObject), IRibbonControl).ReMerge()
        End Sub
    End Class
End Namespace
