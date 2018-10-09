Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.Xpf.Grid
Imports System.Windows.Data
Imports DevExpress.Xpf.Editors
Imports System.Xml.Serialization
Imports System.Collections
Imports System.Windows.Media.Imaging
Imports System.Windows.Media
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpf.DemoBase.Helpers
Imports System.ComponentModel
Imports System.Windows.Input
Imports DevExpress.Mvvm

Namespace TreeListDemo
    Partial Public Class Filtering
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class FiltrationModuleViewModel
        Inherits BindableBase

        Public Sub New()
            InitData()
            ShowAutoFilterRow = True
            ExpandNodesOnFiltering = True
            ShowCriteriaInAutoFilterRow = True
            ColumnFilterPopupMode = DevExpress.Xpf.Grid.ColumnFilterPopupMode.Excel
        End Sub

        Private Sub InitData()
            Filters = New List(Of Filter)()
            Filters.Add(New Filter("All", ""))
            Filters.Add(New Filter("Administration", "Contains([JobTitle], 'Administrator')"))
            Filters.Add(New Filter("Older than 35", "[Age] > 35"))
            Filters.Add(New Filter("Male", "[Gender] = 'M'"))
            Filters.Add(New Filter("Female", "[Gender] = 'F'"))
            Filters.Add(New Filter("Upcoming Birthdays", "[BalloonVisibility] = 'True'"))
            SearchPanelModes = New List(Of ShowSearchPanelMode)()
            SearchPanelModes.Add(ShowSearchPanelMode.Always)
            SearchPanelModes.Add(ShowSearchPanelMode.Default)
            SearchPanelModes.Add(ShowSearchPanelMode.Never)
            FilterModes = New List(Of TreeListFilterMode)()
            FilterModes.Add(TreeListFilterMode.Smart)
            FilterModes.Add(TreeListFilterMode.Standard)
            FilterModes.Add(TreeListFilterMode.Extended)
        End Sub

        Public Property Filters() As List(Of Filter)
        Public Property SearchPanelModes() As List(Of ShowSearchPanelMode)
        Public Property FilterModes() As List(Of TreeListFilterMode)

        Private showAutoFilterRowCore As Boolean
        Public Property ShowAutoFilterRow() As Boolean
            Get
                Return showAutoFilterRowCore
            End Get
            Set(ByVal value As Boolean)
                SetProperty(showAutoFilterRowCore, value, Function() ShowAutoFilterRow)
            End Set
        End Property
        Private showCriteriaInAutoFilterRowCore As Boolean
        Public Property ShowCriteriaInAutoFilterRow() As Boolean
            Get
                Return showCriteriaInAutoFilterRowCore
            End Get
            Set(ByVal value As Boolean)
                SetProperty(showCriteriaInAutoFilterRowCore, value, Function() ShowCriteriaInAutoFilterRow)
            End Set
        End Property
        Private expandNodesOnFilteringCore As Boolean
        Public Property ExpandNodesOnFiltering() As Boolean
            Get
                Return expandNodesOnFilteringCore
            End Get
            Set(ByVal value As Boolean)
                SetProperty(expandNodesOnFilteringCore, value, Function() ExpandNodesOnFiltering)
            End Set
        End Property
        Private сolumnFilterPopupModeCore As ColumnFilterPopupMode
        Public Property ColumnFilterPopupMode() As ColumnFilterPopupMode
            Get
                Return сolumnFilterPopupModeCore
            End Get
            Set(ByVal value As ColumnFilterPopupMode)
                SetProperty(сolumnFilterPopupModeCore, value, "ColumnFilterPopupMode")
            End Set
        End Property
    End Class

    Public Class Filter
        Public Sub New(ByVal name As String, ByVal filterString As String)
            Me.Name = name
            Me.FilterString = filterString
        End Sub
        Private privateName As String
        Public Property Name() As String
            Get
                Return privateName
            End Get
            Private Set(ByVal value As String)
                privateName = value
            End Set
        End Property
        Private privateFilterString As String
        Public Property FilterString() As String
            Get
                Return privateFilterString
            End Get
            Private Set(ByVal value As String)
                privateFilterString = value
            End Set
        End Property
    End Class
End Namespace
