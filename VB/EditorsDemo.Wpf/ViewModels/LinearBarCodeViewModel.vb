Imports DevExpress.XtraPrinting.BarCode.Native
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace EditorsDemo
    Public Class LinearBarCodeViewModel
        Public Sub New()
            Me.BarCodeSymbology = BarCodeSymbology.Code128
            Me.Text = "101"
            Me.BarCodeModule = 3
            Me.AutoModule = True
            Me.ShowText = True
        End Sub
        Public Overridable Property ShowText() As Boolean
        Public Overridable Property AutoModule() As Boolean
        Public Overridable Property Text() As String
        Public Overridable Property BarCodeModule() As Double
        Public Overridable Property BarCodeSymbology() As BarCodeSymbology
        Public ReadOnly Property BarCodeTypes() As IEnumerable(Of BarCodeSymbology)
            Get
                Return GetBarCodeTypes()
            End Get
        End Property
        Protected Overridable Function GetBarCodeTypes() As IEnumerable(Of BarCodeSymbology)
            Return System.Enum.GetValues(GetType(BarCodeSymbology)).Cast(Of BarCodeSymbology)().TakeWhile(Function(bcs) bcs <> BarCodeSymbology.QRCode AndAlso bcs <> BarCodeSymbology.DataMatrix AndAlso bcs <> BarCodeSymbology.DataMatrixGS1 AndAlso bcs <> BarCodeSymbology.PDF417)
        End Function
    End Class
    Public Class BarCode2DViewModel
        Inherits LinearBarCodeViewModel

        Public Sub New()
            Me.BarCodeSymbology = BarCodeSymbology.QRCode
        End Sub
        Protected Overrides Function GetBarCodeTypes() As IEnumerable(Of BarCodeSymbology)
            Return New BarCodeSymbology() { BarCodeSymbology.QRCode, BarCodeSymbology.DataMatrix, BarCodeSymbology.DataMatrixGS1, BarCodeSymbology.PDF417 }
        End Function
    End Class
End Namespace
