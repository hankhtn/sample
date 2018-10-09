Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.Xpf.DemoBase.DataClasses

Namespace NavigationDemo
    Public Class CollapseModeViewModel

        Private privateFiltersData As ReadOnlyCollection(Of FilterGroup)
        Public Property FiltersData() As ReadOnlyCollection(Of FilterGroup)
            Get
                Return privateFiltersData
            End Get
            Private Set(ByVal value As ReadOnlyCollection(Of FilterGroup))
                privateFiltersData = value
            End Set
        End Property

        Public Sub New()
            FiltersData = New ReadOnlyCollection(Of FilterGroup)(CreateFilters())
        End Sub

        Private Function CreateFilters() As FilterGroup()
            Dim experienceItems = New FilterItem() {
                New FilterItem("> 20 years", "(DateDiffYear([HireDate], Today()) > 20)"),
                New FilterItem("> 15 and <= 20 years", "(DateDiffYear([HireDate], Today()) > 15 AND DateDiffYear([HireDate], Today()) <= 20)"),
                New FilterItem("> 10 and <= 15 years", "(DateDiffYear([HireDate], Today()) > 10 AND DateDiffYear([HireDate], Today()) <= 15)"),
                New FilterItem("<= 10 years", "(DateDiffYear([HireDate], Today()) <= 10)")
            }
            Dim regionItems = EmployeesWithPhotoData.DataSource.Select(Function(x) x.CountryRegionName).Distinct().Select(Function(x) New FilterItem(x, String.Format("([CountryRegionName] = '{0}')", x), False, GetFlag(x))).ToArray()
            regionItems.Take(4).ToList().ForEach(Sub(x) x.ShowInCollapsedMode = True)

            Return New FilterGroup() {
                New FilterGroup("Experience", experienceItems),
                New FilterGroup("Region", regionItems.ToArray())
            }
        End Function
        Private Shared Function GetFlag(ByVal country As String) As Byte()
            Dim countryInfo = CountriesData.DataSource.FirstOrDefault(Function(x) Object.Equals(x.ActualName, country))
            Return If(countryInfo Is Nothing, Nothing, countryInfo.Flag)
        End Function
    End Class

    Public Class FilterGroup
        Private privateName As String
        Public Property Name() As String
            Get
                Return privateName
            End Get
            Private Set(ByVal value As String)
                privateName = value
            End Set
        End Property
        Private privateFilterItems As List(Of FilterItem)
        Public Property FilterItems() As List(Of FilterItem)
            Get
                Return privateFilterItems
            End Get
            Private Set(ByVal value As List(Of FilterItem))
                privateFilterItems = value
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal filterItems() As FilterItem)
            Me.Name = name
            Me.FilterItems = filterItems.ToList()
        End Sub
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
    Public Class FilterItem
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
        Public Property ShowInCollapsedMode() As Boolean
        Private privateIcon As Object
        Public Property Icon() As Object
            Get
                Return privateIcon
            End Get
            Private Set(ByVal value As Object)
                privateIcon = value
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal filterString As String, Optional ByVal showInCollapsedMode As Boolean = False, Optional ByVal icon() As Byte = Nothing)
            Me.Name = name
            Me.FilterString = filterString
            Me.ShowInCollapsedMode = showInCollapsedMode
            Me.Icon = icon
        End Sub
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
End Namespace
