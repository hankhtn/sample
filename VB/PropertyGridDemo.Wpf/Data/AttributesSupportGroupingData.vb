Imports System
Imports System.ComponentModel

Namespace PropertyGridDemo
    Public Class AttributesSupportGroupingData
        Private Const JobGroup As String = "Job"
        Private Const ContactGroup As String = "Contact"
        Private Const AddressGroup As String = "Address"
        Private Const PersonalGroup As String = "Personal"
        Private Const InfoGroup As String = "Info"
        <Category(InfoGroup), DisplayName("First name")>
        Public Property FirstName() As String
        <Category(InfoGroup), DisplayName("Last name")>
        Public Property LastName() As String

        <Category(AddressGroup)>
        Public Property AddressLine1() As String
        <Category(AddressGroup)>
        Public Property AddressLine2() As String

        <Category(PersonalGroup), DisplayName("Birth date")>
        Public Property BirthDate() As Date
        <Category(PersonalGroup)>
        Public Property Gender() As Gender

        <Category(ContactGroup)>
        Public Property Email() As String
        <Category(ContactGroup)>
        Public Property Phone() As String

        <Category(JobGroup)>
        Public Property Group() As String
        <Category(JobGroup), DisplayName("Hire date")>
        Public Property HireDate() As Date
        <Category(JobGroup)>
        Public Property Salary() As Decimal
        <Category(JobGroup)>
        Public Property Title() As String
    End Class
End Namespace
