Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.ComponentModel.DataAnnotations

Namespace TreeListDemo
    Public Class DataAnnotationsElement4

        <Display(Name := "Id", AutoGenerateField := False), Editable(False)>
        Public Property ID() As Integer

        <Display(Name := "Parent Id", AutoGenerateField := False), Editable(False)>
        Public Property ParentID() As Integer

        <Display(Name := "Category", GroupName := "Product Details"), Editable(False)>
        Public Property ProductCategory() As CategoryData

        <Display(Name := "Name", GroupName := "Product Details"), Editable(False)>
        Public Property ProductName() As String

        <Display(Name := "Customer", GroupName := "Order Details/Main"), Editable(False)>
        Public Property CustomerName() As String

        <Display(Name := "Date", GroupName := "Order Details/Main")>
        Public Property OrderDate() As Date

        <Display(GroupName := "Order Details/Status")>
        Public Property Quantity() As Integer

        <Display(GroupName := "Order Details/Status"), DataType(DataType.Currency)>
        Public Property Price() As Decimal

        <Display(Name := "Is ready", GroupName := "Order Details/Status")>
        Public Property IsReady() As Boolean

        Public Overrides Function ToString() As String
            Return "Nested Bands Layout"
        End Function
    End Class
End Namespace
