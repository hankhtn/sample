Imports System
Imports System.Collections.Generic
Imports DevExpress.Mvvm

Namespace GridDemo
    Public Class PLinqInstantFeedbackDemoViewModel
        Inherits BindableBase

        Private orderDataListSource As OrderDataListSource

        Private orderDataGenerator_Renamed As New OrderInfoDataGenerator(0)

        Private isGeneratingOrderData_Renamed As Boolean

        Private generateOrderDataProgressValue_Renamed As Double

        Private countItems_Renamed As CountItemCollection

        Private selectedCountItem_Renamed As CountItem
        Private isDesignTime As Boolean = True

        Private Sub orderDataGenerator_GenerateOrderDataStarted(ByVal sender As Object, ByVal e As EventArgs)
            IsGeneratingOrderData = True
        End Sub
        Private Sub orderDataGenerator_GenerateOrderDataCompleted(ByVal sender As Object, ByVal e As EventArgs)
            IsGeneratingOrderData = False
        End Sub
        Private Sub orderDataGenerator_GenerateOrderDataProgress(ByVal sender As Object, ByVal e As GenerateOrderDataProgressEventArgs)
            GenerateOrderDataProgressValue = e.Progress
        End Sub
        Public Sub New()
            AddHandler orderDataGenerator_Renamed.GenerateOrderDataStarted, AddressOf orderDataGenerator_GenerateOrderDataStarted
            AddHandler orderDataGenerator_Renamed.GenerateOrderDataCompleted, AddressOf orderDataGenerator_GenerateOrderDataCompleted
            AddHandler orderDataGenerator_Renamed.GenerateOrderDataProgress, AddressOf orderDataGenerator_GenerateOrderDataProgress
        End Sub
        Public Sub New(ByVal isDesignTime As Boolean)
            Me.New()
            Me.isDesignTime = isDesignTime
        End Sub

        Private Sub RecreateListSource()
            If (SelectedCountItem IsNot Nothing) AndAlso (Not isDesignTime) Then
                orderDataGenerator_Renamed.Count = SelectedCountItem.Count
            Else
                orderDataGenerator_Renamed.Count = 0
            End If
            ListSource = New OrderDataListSource(orderDataGenerator_Renamed)
        End Sub
        Public Property ListSource() As OrderDataListSource
            Get
                Return orderDataListSource
            End Get
            Set(ByVal value As OrderDataListSource)
                SetProperty(orderDataListSource, value, Function() ListSource)
            End Set
        End Property
        Public ReadOnly Property Categories() As List(Of CategoryData)
            Get
                Return orderDataGenerator_Renamed.GetCategories()
            End Get
        End Property
        Public Property IsGeneratingOrderData() As Boolean
            Get
                Return isGeneratingOrderData_Renamed
            End Get
            Set(ByVal value As Boolean)
                SetProperty(isGeneratingOrderData_Renamed, value, Function() IsGeneratingOrderData)
            End Set
        End Property
        Public Property GenerateOrderDataProgressValue() As Double
            Get
                Return generateOrderDataProgressValue_Renamed
            End Get
            Set(ByVal value As Double)
                SetProperty(generateOrderDataProgressValue_Renamed, value, Function() GenerateOrderDataProgressValue)
            End Set
        End Property
        Public Property CountItems() As CountItemCollection
            Get
                Return countItems_Renamed
            End Get
            Set(ByVal value As CountItemCollection)
                If SetProperty(countItems_Renamed, value, Function() CountItems) Then
                    If (CountItems IsNot Nothing) AndAlso (CountItems.Count > 0) Then
                        SelectedCountItem = CountItems(CountItems.Count \ 2)
                    Else
                        SelectedCountItem = Nothing
                    End If
                End If
            End Set
        End Property
        Public Property SelectedCountItem() As CountItem
            Get
                Return selectedCountItem_Renamed
            End Get
            Set(ByVal value As CountItem)
                SetProperty(selectedCountItem_Renamed, value, Function() SelectedCountItem, AddressOf RecreateListSource)
            End Set
        End Property
        Public Sub SetIsDesignTime(ByVal value As Boolean)
            isDesignTime = value
            RecreateListSource()
        End Sub
    End Class
End Namespace
