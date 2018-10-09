Imports DevExpress.Mvvm
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Media.Imaging
Imports DevExpress.SalesDemo.Model

Namespace ProductsDemo
    Public MustInherit Class NavigationModule
        Inherits ViewModelBase

        Public Sub New()
            IsActive = True
        End Sub
        Public MustOverride ReadOnly Property Caption() As String
        Public MustOverride ReadOnly Property Description() As String
        Public MustOverride ReadOnly Property Glyph() As BitmapImage

        Private isActive_Renamed As Boolean = False
        Public Property IsActive() As Boolean
            Get
                Return isActive_Renamed
            End Get
            Set(ByVal value As Boolean)
                SetProperty(isActive_Renamed, value, "IsActive", AddressOf OnIsActiveChanged)
            End Set
        End Property
        Private privateDataProvider As IDataProvider
        Public Property DataProvider() As IDataProvider
            Get
                Return privateDataProvider
            End Get
            Private Set(ByVal value As IDataProvider)
                privateDataProvider = value
            End Set
        End Property

        Private isDataLoading_Renamed As Boolean = False
        Public Property IsDataLoading() As Boolean
            Get
                Return isDataLoading_Renamed
            End Get
            Private Set(ByVal value As Boolean)
                SetProperty(isDataLoading_Renamed, value, "IsDataLoading")
            End Set
        End Property

        Private isDataLoaded_Renamed As Boolean = False
        Public Property IsDataLoaded() As Boolean
            Get
                Return isDataLoaded_Renamed
            End Get
            Private Set(ByVal value As Boolean)
                SetProperty(isDataLoaded_Renamed, value, "IsDataLoaded")
            End Set
        End Property

        Protected MustOverride Sub SaveAndClearData()
        Protected MustOverride Sub RestoreData()
        Protected MustOverride Sub InitializeData()
        Protected Sub SaveAndClearPropertyValue(Of T)(ByRef storage As T, ByVal propName As String, Optional ByVal nullValue As T = Nothing)
            Dim storageValue As T = storage
            Dim resultValue As T = Nothing
            If PropertyCache Is Nothing Then
                PropertyCache = New Dictionary(Of String, Object)()
            End If
            If PropertyCache.ContainsKey(propName) Then
                PropertyCache.Remove(propName)
            End If
            PropertyCache.Add(propName, storageValue)
            resultValue = nullValue
            storage = resultValue
            RaisePropertyChanged(propName)
        End Sub
        Protected Sub SavePropertyValue(Of T)(ByVal storage As T, ByVal propName As String)
            If PropertyCache Is Nothing Then
                PropertyCache = New Dictionary(Of String, Object)()
            End If
            If PropertyCache.ContainsKey(propName) Then
                PropertyCache.Remove(propName)
            End If
            PropertyCache.Add(propName, storage)
        End Sub
        Protected Sub RestorePropertyValue(Of T)(<System.Runtime.InteropServices.Out()> ByRef storage As T, ByVal propName As String, ByVal doRaisePropertyChanged As Boolean)
            Dim resultValue As T = Nothing
            If PropertyCache IsNot Nothing AndAlso PropertyCache.ContainsKey(propName) Then
                resultValue = DirectCast(PropertyCache(propName), T)
                PropertyCache.Remove(propName)
            End If
            storage = resultValue
            If doRaisePropertyChanged Then
                RaisePropertyChanged(propName)
            End If
        End Sub
        Protected Overrides Sub OnInitializeInDesignMode()
            MyBase.OnInitializeInDesignMode()
            DataProvider = New SampleDataProvider()
            InitializeData()
        End Sub
        Protected Overrides Sub OnInitializeInRuntime()
            MyBase.OnInitializeInRuntime()
            DataProvider = DataSource.GetDataProvider()
        End Sub
        Private Sub OnIsActiveChanged()
            If ViewModelBase.IsInDesignMode Then
                Return
            End If
            If IsDataLoading Then
                Return
            End If
            If IsActive AndAlso (Not IsDataLoaded) Then
                InitializeInBackground()
                Return
            End If
            If IsActive = False Then
                SaveAndClearData()
            Else
                RestoreData()
            End If
        End Sub
        Private Sub InitializeInBackground()
            If IsDataLoading OrElse IsDataLoaded Then
                Return
            End If
            IsDataLoading = True
            Dim t = DoInBackground(AddressOf InitializeData)
            t.ContinueWith(Sub(x)
                IsDataLoading = False
                IsDataLoaded = True
            End Sub)
            t.Start()
        End Sub
        Private Function DoInBackground(ByVal action As Action) As Task
            Return New Task(action)
        End Function
        Private PropertyCache As Dictionary(Of String, Object)
    End Class
End Namespace
