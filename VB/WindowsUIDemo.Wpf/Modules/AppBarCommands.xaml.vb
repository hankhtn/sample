Imports System
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media.Imaging
Imports System.Windows.Media.Effects
Imports System.Collections
Imports System.Windows.Controls
Imports DevExpress.Xpf.Core.Native
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm
Imports System.Collections.Generic
Imports System.Windows.Markup
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace WindowsUIDemo
    Partial Public Class AppBarCommands
        Inherits WindowsUIDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
    <POCOViewModel>
    Public Class PhotoCollection
        Public Overridable Property Photos() As List(Of Photo)
        Public Overridable Property SelectedItem() As Photo
        Public Sub New()
            Photos = New List(Of Photo)()
            AddPhoto("Mercedes-Benz", "/WindowsUIDemo;component/Images/Photos/Cars/0.jpg")
            AddPhoto("Mercedes-Benz", "/WindowsUIDemo;component/Images/Photos/Cars/1.jpg")
            AddPhoto("BMW", "/WindowsUIDemo;component/Images/Photos/Cars/2.jpg")
            AddPhoto("Rolls-Royce", "/WindowsUIDemo;component/Images/Photos/Cars/3.jpg")
            AddPhoto("Jaguar", "/WindowsUIDemo;component/Images/Photos/Cars/4.jpg")
            AddPhoto("Lexus", "/WindowsUIDemo;component/Images/Photos/Cars/5.jpg")
            AddPhoto("Lexus", "/WindowsUIDemo;component/Images/Photos/Cars/6.jpg")
            AddPhoto("Ford", "/WindowsUIDemo;component/Images/Photos/Cars/7.jpg")
            AddPhoto("Dodge", "/WindowsUIDemo;component/Images/Photos/Cars/8.jpg")
            AddPhoto("GMC", "/WindowsUIDemo;component/Images/Photos/Cars/9.jpg")
            AddPhoto("Nissan", "/WindowsUIDemo;component/Images/Photos/Cars/10.jpg")
        End Sub
        Private Sub AddPhoto(ByVal caption As String, ByVal uri As String)
            Dim image = New BitmapImage()
            image.BeginInit()
            image.UriSource = New Uri(uri, UriKind.Relative)
            image.EndInit()
            Photos.Add(ViewModelSource.Create(Function() New Photo With {.Caption = caption, .SizeInfo = "700x467", .Source = image, .ViewSize = New Size(150, 100)}))
        End Sub
        <Command>
        Public Sub RotateClockwise()
            If SelectedItem IsNot Nothing Then
                Select Case SelectedItem.Rotation
                    Case Rotation.Rotate0
                        SelectedItem.Rotation = Rotation.Rotate90
                    Case Rotation.Rotate90
                        SelectedItem.Rotation = Rotation.Rotate180
                    Case Rotation.Rotate180
                        SelectedItem.Rotation = Rotation.Rotate270
                    Case Rotation.Rotate270
                        SelectedItem.Rotation = Rotation.Rotate0
                End Select
            End If
        End Sub
        <Command>
        Public Sub RotateCounterclockwise()
            If SelectedItem IsNot Nothing Then
                Select Case SelectedItem.Rotation
                    Case Rotation.Rotate0
                        SelectedItem.Rotation = Rotation.Rotate270
                    Case Rotation.Rotate90
                        SelectedItem.Rotation = Rotation.Rotate0
                    Case Rotation.Rotate180
                        SelectedItem.Rotation = Rotation.Rotate90
                    Case Rotation.Rotate270
                        SelectedItem.Rotation = Rotation.Rotate180
                End Select
            End If
        End Sub
        <Command>
        Public Sub Rotate180()
            If SelectedItem IsNot Nothing Then
                Select Case SelectedItem.Rotation
                    Case Rotation.Rotate0
                        SelectedItem.Rotation = Rotation.Rotate180
                    Case Rotation.Rotate90
                        SelectedItem.Rotation = Rotation.Rotate270
                    Case Rotation.Rotate180
                        SelectedItem.Rotation = Rotation.Rotate0
                    Case Rotation.Rotate270
                        SelectedItem.Rotation = Rotation.Rotate90
                End Select
            End If
        End Sub
        <Command>
        Public Sub RotateReset()
            If SelectedItem IsNot Nothing Then
                SelectedItem.Rotation = Rotation.Rotate0
            End If
        End Sub
        <Command>
        Public Sub ZoomIn()
            If SelectedItem IsNot Nothing Then
                SelectedItem.Scale += 0.1
            End If
        End Sub
        <Command>
        Public Sub ZoomOut()
            If SelectedItem IsNot Nothing Then
                SelectedItem.Scale = Math.Max(0.1, SelectedItem.Scale - 0.1)
            End If
        End Sub
        <Command>
        Public Sub ResetScale()
            If SelectedItem IsNot Nothing Then
                SelectedItem.Scale = 1
            End If
        End Sub
        <Command>
        Public Sub Print()
            If SelectedItem Is Nothing Then
                Return
            End If
            Dim printDialog As New PrintDialog()
            If printDialog.ShowDialog() = True Then
                printDialog.PrintVisual(New Image() With {.Source = SelectedItem.Source}, SelectedItem.Caption)
            End If
        End Sub
        <Command>
        Public Sub Flip()
            If SelectedItem IsNot Nothing Then
                SelectedItem.Flip = Not SelectedItem.Flip
            End If
        End Sub
    End Class
    <POCOViewModel>
    Public Class Photo
        Public Property Source() As ImageSource
        Public Property Caption() As String
        Public Property SizeInfo() As String
        Public Property ViewSize() As Size
        Public Overridable Property Rotation() As Rotation
        Public Overridable Property Scale() As Double
        Public Overridable Property Flip() As Boolean
        Public Sub New()
            Scale = 1.1
            ViewSize = New Size(Double.NaN, Double.NaN)
        End Sub
    End Class
End Namespace
