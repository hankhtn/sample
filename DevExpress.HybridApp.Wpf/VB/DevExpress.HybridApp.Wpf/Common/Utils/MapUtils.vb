Namespace DevExpress.DevAV
    Public Class MapUtils
        Private Const key As String = DevExpress.Map.Native.DXBingKeyVerifier.BingKeyWpfHybridApp
        Public ReadOnly Property DevExpressBingKey() As String
            Get
                Return key
            End Get
        End Property
    End Class
End Namespace
