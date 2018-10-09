Imports DevExpress.DemoData.Models.Vehicles
Imports System.Collections.Generic
Imports System.Linq
Imports VehicleModel = DevExpress.DemoData.Models.Vehicles.Model

Namespace DevExpress.DemoData
    Public Class VehiclesData
        Private context As New VehiclesContext()
        Public ReadOnly Property Models() As IEnumerable(Of VehicleModel)
            Get
                Return context.Models.ToList()
            End Get
        End Property
    End Class
End Namespace
