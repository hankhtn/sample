Imports System.Windows

Namespace MVVMDemo.Behaviors
    Public Class KeyToCommandViewModel
        Private privatePersons As PersonInfo()
        Public Property Persons() As PersonInfo()
            Get
                Return privatePersons
            End Get
            Private Set(ByVal value As PersonInfo())
                privatePersons = value
            End Set
        End Property

        Protected Sub New()
            Persons = PersonInfo.Persons
        End Sub

        Public Sub ShowPersonDetail(ByVal person As PersonInfo)
            If person IsNot Nothing Then
                MessageBox.Show(person.FullName, "Detail info")
            End If
        End Sub
    End Class
End Namespace
