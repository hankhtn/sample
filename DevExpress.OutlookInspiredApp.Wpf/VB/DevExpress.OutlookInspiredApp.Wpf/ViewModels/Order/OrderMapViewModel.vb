Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports DevExpress.DevAV
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Map
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map
Imports System.Windows
Imports System.IO
Imports DevExpress.DevAV.Reports

Namespace DevExpress.DevAV.ViewModels
    Public Class OrderMapViewModel
        Inherits NavigatorMapViewModel(Of CustomerStore)

        Public Shared Overloads Function Create(ByVal order As Order) As OrderMapViewModel
            Return ViewModelSource.Create(Function() New OrderMapViewModel(order))
        End Function
        Protected Sub New(ByVal order As Order)
            MyBase.New(order.Store, AddressHelper.DevAVHomeOffice.ToString(), New GeoPoint(AddressHelper.DevAVHomeOffice.Latitude, AddressHelper.DevAVHomeOffice.Longitude), order.Store.Address.ToString(), New GeoPoint(order.Store.Address.Latitude, order.Store.Address.Longitude), Nothing)
            Me.Order = order
        End Sub
        Private privateOrder As Order
        Public Property Order() As Order
            Get
                Return privateOrder
            End Get
            Protected Set(ByVal value As Order)
                privateOrder = value
            End Set
        End Property
        Public Overridable Property ShipmentText() As String
        Public Overridable Property PdfStream() As Stream

        Public Overrides Sub OnLoaded()
            MyBase.OnLoaded()
            CreateShippingDetailPdf()
        End Sub
        Public Overridable Sub OuUnloaded()
            PdfStream.Dispose()
        End Sub
        Private Sub CreateShippingDetailPdf()
            PdfStream = New MemoryStream()
            ReportFactory.ShippingDetail(Order).ExportToPdf(PdfStream)
            ShipmentText = GetShipmentText()
        End Sub
        Private Function GetShipmentText() As String
            Select Case Order.ShipmentStatus
                Case ShipmentStatus.Received
                    Return "Shipment Received"
                Case ShipmentStatus.Transit
                    Return "Shipment in Transit"
            End Select
            Return "Awaiting shipment"
        End Function
    End Class
End Namespace
