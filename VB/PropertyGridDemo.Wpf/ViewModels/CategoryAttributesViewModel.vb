Imports System

Namespace PropertyGridDemo
    Public Class CategoryAttributesViewModel
        Public Overridable Property Data() As AttributesSupportGroupingData
        Public Sub New()
            Data = New AttributesSupportGroupingData With {.FirstName = "Anita", .LastName = "Benson", .Group = "Inventory Management", .Title = "Purchasing Manager", .HireDate = New Date(2002, 2, 2), .Salary = 65000D, .Phone = "7138638137", .Email = "Anita_Benson@example.com", .AddressLine1 = "9602 South Main", .AddressLine2 = "Seattle, 77025, USA", .Gender = Gender.Female, .BirthDate = New Date(1985, 3, 18)}
        End Sub
    End Class
End Namespace
