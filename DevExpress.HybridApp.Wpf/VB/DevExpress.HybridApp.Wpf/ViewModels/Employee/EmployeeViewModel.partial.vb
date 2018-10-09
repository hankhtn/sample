Imports System
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels
    Partial Public Class EmployeeViewModel

        Private contacts_Renamed As EmployeeContactsViewModel

        Private firstName As String
        Private lastName As String

        Public Overrides Sub OnLoaded()
            MyBase.OnLoaded()
            firstName = Entity.FirstName
            lastName = Entity.LastName
        End Sub

        Protected Overrides Function SaveCore() As Boolean
            If Entity.FirstName <> firstName OrElse Entity.LastName <> lastName Then
                Entity.FullName = Entity.FirstName & " " & Entity.LastName
            End If
            Return MyBase.SaveCore()
        End Function
        Public Sub ShowMailMerge()

            Dim mailMergeViewModel_Renamed = MailMergeViewModel(Of Employee, Object).Create(UnitOfWorkFactory, getRepositoryFunc, Me.Entity.Id)
            DocumentManagerService.CreateDocument("EmployeeMailMergeView", mailMergeViewModel_Renamed, Nothing, Me).Show()
        End Sub
        Public Sub ShowProfile()
            Xpf.DemoBase.Helpers.Logger.Log("HybridApp: Create Report : Employee: Profile")

            DocumentManagerService.CreateDocument("ReportPreview", ReportPreviewViewModel.Create(ReportInfoFactory.EmployeeProfile(Entity)), Nothing, Me).Show()
        End Sub
        Public Sub ShowMeeting()
            MessageBoxService.ShowMessage(String.Format("Schedule meeting with {0}?", Entity.FullName), "Meeting", MessageButton.YesNoCancel)
        End Sub
        Public ReadOnly Property Contacts() As EmployeeContactsViewModel
            Get
                If contacts_Renamed Is Nothing Then
                    contacts_Renamed = EmployeeContactsViewModel.Create().SetParentViewModel(Me)
                End If
                Return contacts_Renamed
            End Get
        End Property
        Protected Overrides Sub OnEntityChanged()
            MyBase.OnEntityChanged()
            Contacts.Entity = Entity
            If Entity IsNot Nothing Then
                Xpf.DemoBase.Helpers.Logger.Log(String.Format("HybridApp: Edit Employee: {0}",If(String.IsNullOrEmpty(Entity.FullName), "<New>", Entity.FullName)))
            End If
        End Sub
        Protected Overrides Function GetTitle() As String
            Return Entity.FullName
        End Function
        Private ReadOnly Property DocumentManagerService() As IDocumentManagerService
            Get
                Return Me.GetRequiredService(Of IDocumentManagerService)("FrameDocumentUIService")
            End Get
        End Property
    End Class
End Namespace
