Imports System
Imports System.ComponentModel.DataAnnotations

Namespace LayoutControlDemo
    Public Class AdvancedGroupedLayoutData
        Private Const NameGroup As String = "<Name>"
        Private Const TabbedGroup As String = "{Tabs}"
        Private Const JobGroup As String = "Job"
        Private Const JobGroupPath As String = TabbedGroup & "/" & JobGroup
        Private Const ContactGroup As String = "Contact"
        Private Const ContactGroupPath As String = TabbedGroup & "/" & ContactGroup
        Private Const AddressGroup As String = "Address"
        Private Const AddressGroupPath As String = ContactGroupPath & "/" & AddressGroup
        Private Const PersonalGroup As String = "Personal-"

        <Display(GroupName := AddressGroupPath, ShortName := "")>
        Public Property AddressLine1() As String
        <Display(GroupName := AddressGroupPath, ShortName := "")>
        Public Property AddressLine2() As String
        <Display(GroupName := PersonalGroup, Name := "Birth date")>
        Public Property BirthDate() As Date
        <Display(GroupName := ContactGroupPath, Order := 21)>
        Public Property Email() As String
        <Display(GroupName := NameGroup, Name := "First name", Order := 0)>
        Public Property FirstName() As String
        <Display(GroupName := PersonalGroup, Order := 3)>
        Public Property Gender() As Gender
        <Display(GroupName := JobGroupPath, Order := 1)>
        Public Property Group() As String
        <Display(GroupName := JobGroupPath, Name := "Hire date")>
        Public Property HireDate() As Date
        <Display(GroupName := NameGroup, Name := "Last name")>
        Public Property LastName() As String
        <Display(GroupName := ContactGroupPath, Order := 2), DataType(DataType.PhoneNumber)>
        Public Property Phone() As String
        <Display(GroupName := JobGroupPath), DataType(DataType.Currency)>
        Public Property Salary() As Decimal
        <Display(GroupName := JobGroupPath, Order := 11)>
        Public Property Title() As String
    End Class
End Namespace
