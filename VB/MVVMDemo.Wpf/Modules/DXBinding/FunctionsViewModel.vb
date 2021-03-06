Imports DevExpress.Mvvm
Imports System.Linq
Imports System.Windows

Namespace MVVMDemo.DXBindingDemo
    Public Class FunctionsViewModel
        Public Class PersonData
            Inherits BindableBase

            Private privateFullName As String
            Public Property FullName() As String
                Get
                    Return privateFullName
                End Get
                Private Set(ByVal value As String)
                    privateFullName = value
                End Set
            End Property
            Private ReadOnly country As String

            Public Sub New(ByVal fullName As String, ByVal country As String)
                Me.FullName = fullName
                Me.country = country
            End Sub

            Public Function LoadAddress() As Address
                Return New Address(country)
            End Function
        End Class
        Public Class Address
            Inherits BindableBase

            Public Sub New(ByVal country As String)
                Me.Country = country
            End Sub

            Private privateCountry As String
            Public Property Country() As String
                Get
                    Return privateCountry
                End Get
                Private Set(ByVal value As String)
                    privateCountry = value
                End Set
            End Property
        End Class
        Public Overridable Property Person() As PersonData

        Private personIndex As Integer
        Private ReadOnly persons() As PersonData
        Public Sub [Next]()
            Person = persons(personIndex Mod 3)
            personIndex += 1
        End Sub

        Public Overridable Property UserName() As String
        Public Overridable Property AcceptTerms() As Boolean
        Public Sub Register()
            MessageBox.Show("Registered")
        End Sub

        Protected Sub New()
            Dim countries() As String = { "Norway", "Denmark", "Sweden" }
            persons = PersonInfo.Persons.Select(Function(person, index) New PersonData(person.FullName, countries(index))).ToArray()
            [Next]()
        End Sub
    End Class
End Namespace
