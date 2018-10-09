Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Input
Imports DevExpress.Xpf
Imports DevExpress.Mvvm
Imports DevExpress.Utils

Namespace ProductsDemo
    Public Class InfoViewModel
        Inherits BindableBase


        Private aboutInfo_Renamed As AboutInfo

        Private showHelpCommand_Renamed As ICommand

        Private showGettingStartedCommand_Renamed As ICommand

        Private showContactUsCommand_Renamed As ICommand

        Public ReadOnly Property AboutInfo() As AboutInfo
            Get
                If aboutInfo_Renamed Is Nothing Then
                    aboutInfo_Renamed = New AboutInfo()
                    Dim data As DevExpress.Internal.UserData = DevExpress.Utils.About.Utility.GetInfoEx()
                    aboutInfo_Renamed.LicenseState = If(data.IsExpired, LicenseState.TrialExpired, If(data.IsTrial, LicenseState.Trial, LicenseState.Licensed))
                    aboutInfo_Renamed.ProductName = "WPF Controls"
                    aboutInfo_Renamed.ProductPlatform = "Build Your Own Office"
                    aboutInfo_Renamed.RegistrationCode = WpfSerialNumberProvider.GetSerial()
                    aboutInfo_Renamed.Version = AssemblyInfo.Version
                End If
                Return aboutInfo_Renamed
            End Get
        End Property
        Public Sub ShowHelp()
            DevExpress.Xpf.Core.DocumentPresenter.OpenLink("http://help.devexpress.com")
        End Sub
        Public Sub ShowGettingStarted()
            DevExpress.Xpf.Core.DocumentPresenter.OpenLink("http://devexpress.com/support/")
        End Sub
        Public Sub ShowContactUs()
            DevExpress.Xpf.Core.DocumentPresenter.OpenLink("http://devexpress.com/Support/Center")
        End Sub
        Public ReadOnly Property ShowHelpCommand() As ICommand
            Get
                If showHelpCommand_Renamed Is Nothing Then
                    showHelpCommand_Renamed = New DelegateCommand(AddressOf ShowHelp, False)
                End If
                Return showHelpCommand_Renamed
            End Get
        End Property
        Public ReadOnly Property ShowGettingStartedCommand() As ICommand
            Get
                If showGettingStartedCommand_Renamed Is Nothing Then
                    showGettingStartedCommand_Renamed = New DelegateCommand(AddressOf ShowGettingStarted, False)
                End If
                Return showGettingStartedCommand_Renamed
            End Get
        End Property
        Public ReadOnly Property ShowContactUsCommand() As ICommand
            Get
                If showContactUsCommand_Renamed Is Nothing Then
                    showContactUsCommand_Renamed = New DelegateCommand(AddressOf ShowContactUs, False)
                End If
                Return showContactUsCommand_Renamed
            End Get
        End Property
    End Class
End Namespace
