Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.SalesDemo.Model
Imports System
Imports System.Collections.Generic
Imports System.Threading
Imports System.Threading.Tasks
Imports DevExpress.Mvvm.ModuleInjection

Namespace DevExpress.SalesDemo.Wpf.ViewModel
    Public NotInheritable Class Regions

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property Main() As String
            Get
                Return "MainRegion"
            End Get
        End Property
        Public Shared ReadOnly Property Navigation() As String
            Get
                Return "NavigationRegion"
            End Get
        End Property
    End Class
    Public NotInheritable Class Modules

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property Dashboard() As String
            Get
                Return "Dashboard"
            End Get
        End Property
        Public Shared ReadOnly Property Products() As String
            Get
                Return "Products"
            End Get
        End Property
        Public Shared ReadOnly Property Sectors() As String
            Get
                Return "Sectors"
            End Get
        End Property
        Public Shared ReadOnly Property Regions() As String
            Get
                Return "Regions"
            End Get
        End Property
        Public Shared ReadOnly Property Channels() As String
            Get
                Return "Channels"
            End Get
        End Property
    End Class
    Public MustInherit Class DataViewModel
        Protected Sub New()
            If Me.IsInDesignMode() Then
                DataProvider = New SampleDataProvider()
            Else
                DataProvider = DataSource.GetDataProvider()
            End If
        End Sub
        Public Sub RequestData(Of T)(ByVal actionId As String, ByVal requestDataFunction As Func(Of IDataProvider, T), ByVal callback As Action(Of T))
            If Me.IsInDesignMode() Then
                callback(requestDataFunction(DataProvider))
                Return
            End If
            If Not tasks.ContainsKey(actionId) Then
                tasks.Add(actionId, New CancellationTokenSource())
                Dim task = New Task(Of T)(Function() requestDataFunction(DataProvider), tasks(actionId).Token, TaskCreationOptions.LongRunning)
                task.ContinueWith(Sub(x) callback(x.Result), tasks(actionId).Token, TaskContinuationOptions.LongRunning, TaskScheduler.Current)
                task.Start()
                Return
            End If
            If tasks.ContainsKey(actionId) Then
                tasks(actionId).Cancel()
                tasks.Remove(actionId)
                RequestData(actionId, requestDataFunction, callback)
                Return
            End If
        End Sub
        Private DataProvider As IDataProvider
        Private tasks As New Dictionary(Of String, CancellationTokenSource)()
    End Class
    Public MustInherit Class PageViewModel
        Protected Sub New()
            AddHandler ModuleManager.DefaultManager.GetEvents(Me).Navigated, AddressOf OnNavigated
            If Me.IsInDesignMode() Then
                Initialize()
            End If
        End Sub
        Protected MustOverride Sub Initialize()
        Private Sub OnNavigated(ByVal sender As Object, ByVal e As NavigationEventArgs)
            If isInitialize Then
                Return
            End If
            Initialize()
            isInitialize = True
        End Sub
        Private isInitialize As Boolean = False
    End Class
End Namespace
