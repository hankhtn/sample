Imports DevExpress.Mvvm
Imports System
Imports System.Collections.Generic
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace LayoutControlDemo
    Public Class PageDashboardViewModel
        Public ReadOnly Property Agents() As List(Of Agent)
            Get
                Return LayoutControlDemo.Agents.DataSource
            End Get
        End Property
        Public ReadOnly Property Listings() As List(Of Listing)
            Get
                Return LayoutControlDemo.Listings.DataSource
            End Get
        End Property
    End Class
    Public Class Agent
        Public Property FirstName() As String
        Public Property LastName() As String
        Public Property Phone() As String
        Public Property Photo() As String
        Public ReadOnly Property PhotoSource() As ImageSource
            Get
                Return If(String.IsNullOrEmpty(Photo), Nothing, New BitmapImage(New Uri(LayoutControlDemoModule.UriPrefix & Photo, UriKind.Relative)))
            End Get
        End Property
    End Class
    Public NotInheritable Class Agents

        Private Sub New()
        End Sub

        Public Shared ReadOnly DataSource As New List(Of Agent)() From {
            New Agent With {.FirstName = "Anthony", .LastName = "Peterson", .Phone = "(555) 444-0782", .Photo = "/Images/Agents/1.jpg"},
            New Agent With {.FirstName = "Cindy", .LastName = "Haneline", .Phone = "(555) 638-9820", .Photo = "/Images/Agents/2.jpg"},
            New Agent With {.FirstName = "Albert", .LastName = "Walker", .Phone = "(555) 232-2303", .Photo = "/Images/Agents/3.jpg"},
            New Agent With {.FirstName = "Rachel", .LastName = "Scott", .Phone = "(555) 249-1614", .Photo = "/Images/Agents/4.jpg"},
            New Agent With {.FirstName = "Vernon", .LastName = "Rounds", .Phone = "(555) 682-5403", .Photo = "/Images/Agents/5.jpg"},
            New Agent With {.FirstName = "Andrew", .LastName = "Carter", .Phone = "(555) 578-3946", .Photo = "/Images/Agents/6.jpg"}
        }
    End Class
End Namespace
