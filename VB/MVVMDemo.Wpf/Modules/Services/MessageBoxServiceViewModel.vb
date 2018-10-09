Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace MVVMDemo.Services
    Public Class MessageBoxServiceViewModel
        Public Sub ShowMessage(ByVal serviceName As String)
            Dim messageBoxService As IMessageBoxService = Me.GetRequiredService(Of IMessageBoxService)(serviceName)
            Result = messageBoxService.ShowMessage(Text, Caption, Buttons, Icon)
        End Sub

        #Region "properties"
        Public Property Text() As String
        Public Property Caption() As String
        Public Property Buttons() As MessageButton
        Public Property Icon() As MessageIcon

        Private privateResult? As MessageResult
        Public Overridable Property Result() As MessageResult?
            Get
                Return privateResult
            End Get
            Protected Set(ByVal value? As MessageResult)
                privateResult = value
            End Set
        End Property

        Protected Sub New()
            Text = "Message text"
            Caption = "Caption"
            Buttons = MessageButton.OKCancel
            Icon = MessageIcon.Information
        End Sub
        #End Region
    End Class
End Namespace
