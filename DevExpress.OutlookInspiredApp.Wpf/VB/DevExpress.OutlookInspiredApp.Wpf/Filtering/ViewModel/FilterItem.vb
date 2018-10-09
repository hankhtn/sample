Imports System
Imports DevExpress.Data.Filtering
Imports DevExpress.Mvvm.POCO
Imports System.Linq

Namespace DevExpress.DevAV.ViewModels
    Public Class FilterItem

        Public Shared Function Create(ByVal entitiesCount As Integer, ByVal name As String, ByVal filterCriteria As CriteriaOperator, ByVal imageUri As String, ByVal filterTreeViewModel_Renamed As IFilterTreeViewModel) As FilterItem
            Return ViewModelSource.Create(Function() New FilterItem(entitiesCount, name, filterCriteria, imageUri, filterTreeViewModel_Renamed))
        End Function


        Protected Sub New(ByVal entitiesCount As Integer, ByVal name As String, ByVal filterCriteria As CriteriaOperator, ByVal imageUri As String, ByVal filterTreeViewModel_Renamed As IFilterTreeViewModel)
            Me.Name = name
            Me.FilterCriteria = filterCriteria
            Me.ImageUri = imageUri
            Me.FilterTreeViewModel = filterTreeViewModel_Renamed
            Update(entitiesCount)
        End Sub

        Public Overridable Property Name() As String

        Public Overridable Property FilterCriteria() As CriteriaOperator

        Public Overridable Property EntitiesCount() As Integer

        Public Overridable Property DisplayText() As String

        Public Overridable Property ImageUri() As String

        Private privateFilterTreeViewModel As IFilterTreeViewModel
        Public Property FilterTreeViewModel() As IFilterTreeViewModel
            Get
                Return privateFilterTreeViewModel
            End Get
            Protected Set(ByVal value As IFilterTreeViewModel)
                privateFilterTreeViewModel = value
            End Set
        End Property

        Public ReadOnly Property IsCustomFilter() As Boolean
            Get
                If Me.IsInDesignMode() Then
                    Return False
                End If
                Dim customCategory = FilterTreeViewModel.Categories.FirstOrDefault(Function(x) x.IsCustom)
                Return If(customCategory Is Nothing, False, customCategory.FilterItems.Contains(Me))
            End Get
        End Property

        Public Sub Update(ByVal entitiesCount As Integer)
            Me.EntitiesCount = entitiesCount
            DisplayText = String.Format("{0} ({1})", Name, entitiesCount)
        End Sub

        Public Function Clone() As FilterItem
            Return FilterItem.Create(EntitiesCount, Name, FilterCriteria, ImageUri, FilterTreeViewModel)
        End Function
        Public Function Clone(ByVal name As String, ByVal imageUri As String) As FilterItem
            Return FilterItem.Create(EntitiesCount, name, FilterCriteria, imageUri, FilterTreeViewModel)
        End Function

        Protected Overridable Sub OnNameChanged()
            Update(EntitiesCount)
        End Sub
    End Class
End Namespace
