Imports System
Imports System.Data.Common
Imports System.Data.Entity
Imports System.Data.SQLite
Imports System.IO
Imports DevExpress.DemoData
Imports DevExpress.Internal

Namespace DevExpress.DevAV
    Public Class DevAVDbContext
        Inherits DevAVDbBase

        Shared Sub New()
            Database.SetInitializer(Of DevAVDbContext)(Nothing)
        End Sub
    End Class
End Namespace
