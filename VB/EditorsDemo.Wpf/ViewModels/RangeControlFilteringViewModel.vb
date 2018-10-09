Imports DevExpress.Mvvm.DataAnnotations
Imports EditorsDemo.SCService
Imports System

Namespace EditorsDemo
    Public Class RangeControlFilteringViewModel
        Public Sub New()
            WcfSCService = New SCEntities(New Uri("http://demos.devexpress.com/Services/WcfLinqSC/WcfSCService.svc/"))
            StartDate = New Date(2007, 1, 1)
            EndDate = New Date(2009, 1, 1)
            SelectedStart = New Date(2008, 1, 1)
            SelectedEnd = New Date(2008, 7, 1)
        End Sub
        Public Overridable Property WcfSCService() As SCEntities
        Public Overridable Property StartDate() As Date
        Public Overridable Property EndDate() As Date
        <BindableProperty(True, OnPropertyChangedMethodName := "UpdateFilter")>
        Public Overridable Property SelectedStart() As Date
        <BindableProperty(True, OnPropertyChangedMethodName := "UpdateFilter")>
        Public Overridable Property SelectedEnd() As Date
        Public Overridable Property FilterString() As String

        Protected Sub UpdateFilter()
            FilterString = String.Format("([CreatedOn] >= #{0}# AND [CreatedOn] < #{1}#)", SelectedStart, SelectedEnd)
        End Sub
    End Class
End Namespace
