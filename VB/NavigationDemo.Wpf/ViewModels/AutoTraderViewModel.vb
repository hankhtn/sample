Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.DemoData.Models.Vehicles
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Utils.Filtering

Namespace NavigationDemo
    Public Class AutoTraderViewModel
        Private privateVehicles As IEnumerable(Of Model)
        Public Property Vehicles() As IEnumerable(Of Model)
            Get
                Return privateVehicles
            End Get
            Private Set(ByVal value As IEnumerable(Of Model))
                privateVehicles = value
            End Set
        End Property
        Private privateTrademarks As IEnumerable(Of Trademark)
        Public Property Trademarks() As IEnumerable(Of Trademark)
            Get
                Return privateTrademarks
            End Get
            Private Set(ByVal value As IEnumerable(Of Trademark))
                privateTrademarks = value
            End Set
        End Property
        Private privateTransmissionTypes As IEnumerable(Of TransmissionType)
        Public Property TransmissionTypes() As IEnumerable(Of TransmissionType)
            Get
                Return privateTransmissionTypes
            End Get
            Private Set(ByVal value As IEnumerable(Of TransmissionType))
                privateTransmissionTypes = value
            End Set
        End Property
        Private privateBodyStyles As IEnumerable(Of BodyStyle)
        Public Property BodyStyles() As IEnumerable(Of BodyStyle)
            Get
                Return privateBodyStyles
            End Get
            Private Set(ByVal value As IEnumerable(Of BodyStyle))
                privateBodyStyles = value
            End Set
        End Property
        Private privateDoorTypes As Integer()
        Public Property DoorTypes() As Integer()
            Get
                Return privateDoorTypes
            End Get
            Private Set(ByVal value As Integer())
                privateDoorTypes = value
            End Set
        End Property
        Private privateMinPrice As Decimal
        Public Property MinPrice() As Decimal
            Get
                Return privateMinPrice
            End Get
            Private Set(ByVal value As Decimal)
                privateMinPrice = value
            End Set
        End Property
        Private privateMaxPrice As Decimal
        Public Property MaxPrice() As Decimal
            Get
                Return privateMaxPrice
            End Get
            Private Set(ByVal value As Decimal)
                privateMaxPrice = value
            End Set
        End Property
        Private privateMinMPGCity As Integer
        Public Property MinMPGCity() As Integer
            Get
                Return privateMinMPGCity
            End Get
            Private Set(ByVal value As Integer)
                privateMinMPGCity = value
            End Set
        End Property
        Private privateMaxMPGCity As Integer
        Public Property MaxMPGCity() As Integer
            Get
                Return privateMaxMPGCity
            End Get
            Private Set(ByVal value As Integer)
                privateMaxMPGCity = value
            End Set
        End Property
        Private privateMinMPGHighway As Integer
        Public Property MinMPGHighway() As Integer
            Get
                Return privateMinMPGHighway
            End Get
            Private Set(ByVal value As Integer)
                privateMinMPGHighway = value
            End Set
        End Property
        Private privateMaxMPGHighway As Integer
        Public Property MaxMPGHighway() As Integer
            Get
                Return privateMaxMPGHighway
            End Get
            Private Set(ByVal value As Integer)
                privateMaxMPGHighway = value
            End Set
        End Property
        Public Sub New()
            If Me.IsInDesignMode() Then
                Return
            End If
            Dim context = New VehiclesContext()
            Vehicles = context.Models.ToList()
            Trademarks = context.Trademarks.ToList()
            TransmissionTypes = context.TransmissionTypes.ToList()
            BodyStyles = context.BodyStyles.ToList()
            DoorTypes = New Integer() { 2, 3, 4 }
            CalculateProperties()
            context.Dispose()
        End Sub
        Private Sub CalculateProperties()
            MinPrice = Vehicles.Min(Function(x) x.Price)
            MaxPrice = Vehicles.Max(Function(x) x.Price)
            MinMPGCity = Vehicles.Min(Function(x) If(x.MPGCity, Integer.MaxValue))
            MaxMPGCity = Vehicles.Max(Function(x) If(x.MPGCity, Integer.MinValue))
            MinMPGHighway = Vehicles.Min(Function(x) If(x.MPGHighway, Integer.MaxValue))
            MaxMPGHighway = Vehicles.Max(Function(x) If(x.MPGHighway, Integer.MinValue))
        End Sub
    End Class

    Public Class VehiclesFilteringViewModel
        <FilterRange("MinPrice", "MaxPrice")>
        Public Property Price() As Decimal
        <FilterLookup("Trademarks", DisplayMember := "Name", ValueMember := "ID")>
        Public Property TrademarkID() As Long
        <FilterLookup("TransmissionTypes", DisplayMember := "Name", ValueMember := "ID")>
        Public Property TransmissionTypeID() As Long
        <FilterLookup("BodyStyles", DisplayMember := "Name", ValueMember := "ID")>
        Public Property BodyStyleID() As Long
        <FilterLookup("DoorTypes")>
        Public Property Doors() As Integer
        <FilterRange("MinMPGCity", "MaxMPGCity")>
        Public Property MPGCity() As Integer?
        <FilterRange("MinMPGHighway", "MaxMPGHighway")>
        Public Property MPGHighway() As Integer?
    End Class
End Namespace
