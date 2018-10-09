Imports System
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.DataAnnotations

Namespace PropertyGridDemo
    Public Class AttributesSupportDataAnnotationsData
        Private Const JobGroup As String = "Job"
        Private Const ContactGroup As String = "Contact"
        Private Const AddressGroup As String = "Address"
        Private Const Personal As String = "Personal"
        Private Const NameGroup As String = "Name"

        <Display(GroupName := NameGroup, Name := "First name")>
        Public Property FirstName() As String
        <Display(GroupName := NameGroup, Name := "Last name")>
        Public Property LastName() As String

        <Display(GroupName := JobGroup)>
        Public Property Group() As String
        <DisplayFormat(NullDisplayText := "Title not set"), Display(GroupName := JobGroup)>
        Public Property Title() As String
        <Display(GroupName := JobGroup, Name := "Hire date")>
        Public Property HireDate() As Date
        <Display(GroupName := JobGroup), DataType(DataType.Currency)>
        Public Property Salary() As Decimal

        <Display(GroupName := ContactGroup), DataType(DataType.PhoneNumber)>
        Public Property Phone() As String
        <Display(GroupName := ContactGroup), EmailAddress>
        Public Property Email() As String

        <Display(GroupName := Personal)>
        Public Property Gender() As Gender
        <Display(GroupName := Personal, Name := "Birth date"), DisplayFormat(DataFormatString := "m", ApplyFormatInEditMode := True)>
        Public Property BirthDate() As Date

        <Display(GroupName := AddressGroup, Name := "Address line 1")>
        Public Property AddressLine1() As String
        <Display(GroupName := AddressGroup, Name := "Address line 2")>
        Public Property AddressLine2() As String
    End Class
End Namespace
