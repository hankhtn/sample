Imports DevExpress.Mvvm.POCO

Namespace DevExpress.SalesDemo.Wpf.ViewModel
    Public Class ProgressSplashScreenViewModel
        Public Shared Function Create(ByVal caption As String, ByVal copyright As String) As ProgressSplashScreenViewModel
            Return ViewModelSource.Create(Function() New ProgressSplashScreenViewModel(caption, copyright))
        End Function
        Protected Sub New(ByVal caption As String, ByVal copyright As String)
            Me.Caption = caption
            Me.Copyright = copyright
        End Sub

        Private privateCaption As String
        Public Property Caption() As String
            Get
                Return privateCaption
            End Get
            Private Set(ByVal value As String)
                privateCaption = value
            End Set
        End Property
        Private privateCopyright As String
        Public Property Copyright() As String
            Get
                Return privateCopyright
            End Get
            Private Set(ByVal value As String)
                privateCopyright = value
            End Set
        End Property
    End Class
End Namespace
