Imports System
Imports System.ComponentModel
Imports System.Windows
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Printing

Namespace PrintingDemo
    Public MustInherit Class ModuleViewModelBase
        Inherits ViewModelBase

        Private linkCore As TemplatedLink

        Public Property PageHeaderTemplate() As DataTemplate
        Public Property ReportHeaderTemplate() As DataTemplate
        Public Property DetailTemplate() As DataTemplate
        Public Property ReportFooterTemplate() As DataTemplate
        Public Property PageFooterTemplate() As DataTemplate

        Public Property Link() As TemplatedLink
            Get
                Return linkCore
            End Get
            Private Set(ByVal value As TemplatedLink)
                SetProperty(linkCore, value, Function() Link)
            End Set
        End Property

        Public Sub CreateDocument()
            If Link Is Nothing Then
                Link = CreateLink()
                AddHandler Link.PrintingSystem.PageSettingsChanged, AddressOf OnPageSettingsChanged
            End If
            ProcessLink(Link)
            Link.CreateDocument(True)
        End Sub

        Protected Overridable Sub ProcessLink(ByVal link As TemplatedLink)
        End Sub

        Public Sub ClearDocument()
            If Link IsNot Nothing Then
                Link.StopPageBuilding()
                Link.Dispose()
                linkCore = Nothing
            End If
        End Sub

        Protected Sub OnPageSettingsChanged(ByVal sender As Object, ByVal e As EventArgs)
            Link.PaperKind = Link.PrintingSystem.PageSettings.PaperKind
            Link.Margins = Link.PrintingSystem.PageSettings.Margins
            Link.Landscape = Link.PrintingSystem.PageSettings.Landscape
        End Sub

        Protected MustOverride Function CreateLink() As TemplatedLink
    End Class
End Namespace
