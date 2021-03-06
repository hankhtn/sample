Imports DevExpress.Mvvm.DataAnnotations
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Namespace LayoutControlDemo
    <MetadataType(GetType(AttributeSupportMetadata))>
    Public Class AttributeSupportData_FluentAPI
        Public Property ID() As Integer
        Public Property Age() As Integer
        Public Property Employer() As String
        Public Property FirstName() As String
        Public ReadOnly Property FullName() As String
            Get
                Return FirstName & " " & LastName
            End Get
        End Property
        Public Property Gender() As Gender
        Public Property LastName() As String
        Public Property SSN() As String
        Public Property Department() As String
    End Class
    Public NotInheritable Class AttributeSupportMetadata

        Private Sub New()
        End Sub

        Public Shared Sub BuildMetadata(ByVal builder As MetadataBuilder(Of AttributeSupportData_FluentAPI))
            builder.Property(Function(x) x.ID).NotAutoGenerated()
            builder.Property(Function(x) x.Age).DisplayFormatString("d2", applyDisplayFormatInEditMode:= True)
            builder.Property(Function(x) x.Employer).NotEditable()
            builder.Property(Function(x) x.FirstName).DisplayName("First name").Required()
            builder.Property(Function(x) x.FullName).DisplayName("Full name")
            builder.Property(Function(x) x.Gender).DisplayName("Sex")
            builder.Property(Function(x) x.LastName).DisplayName("Last name").Required()
            builder.Property(Function(x) x.SSN).ReadOnly()
            builder.Property(Function(x) x.Department).NullDisplayText("Department not set")

            builder.DataFormLayout().ContainsProperty(Function(x) x.FirstName).ContainsProperty(Function(x) x.LastName).ContainsProperty(Function(x) x.FullName).ContainsProperty(Function(x) x.Gender)
        End Sub
    End Class
End Namespace
