Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.Internal
Imports System.Collections.ObjectModel

Namespace NavigationDemo.Utils
    Public NotInheritable Class ImageHelper

        Private Sub New()
        End Sub
        #Region "PhotoStudio"

        Private Shared rnd As New Random()


        Private ReadOnly Shared photos_Renamed As ObservableCollection(Of PhotoInfo) = CreatePhotos()
        Public Shared ReadOnly Property Photos() As ObservableCollection(Of PhotoInfo)
            Get
                Return photos_Renamed
            End Get
        End Property

        Private Shared Function CreatePhotos() As ObservableCollection(Of PhotoInfo)
            Dim getPhotosPath As Func(Of String, String) = Function(photosFolderName) DataDirectoryHelper.GetFile("Photos\" & photosFolderName, DataDirectoryHelper.DataFolderName)
            Dim getDirectoryPhotos As Func(Of String, IEnumerable(Of PhotoInfo)) = Function(directoryPath) Directory.GetFiles(directoryPath, "*.jpg").Select(Function(x) New PhotoInfo(x, GetRating()))
            Return New ObservableCollection(Of PhotoInfo)(getDirectoryPhotos(getPhotosPath("Photos")).Concat(getDirectoryPhotos(getPhotosPath("AdditionalPhotos"))))
        End Function
        Private Shared Function GetRating() As Integer
            Return rnd.Next(5)
        End Function

        #End Region
        #Region "Employees"

        Private Shared ReadOnly EmployeeDepartmentImages As New List(Of String)() From {"administration", "inventory", "manufacturing", "quality", "research", "sales"}
        Public Shared Function GetEmployeeDepartmentImage(ByVal departmentName As String) As Object
            Dim imageName = EmployeeDepartmentImages.FirstOrDefault(Function(x) departmentName.ToLower().Contains(x))
            If String.IsNullOrEmpty(imageName) Then
                Return Nothing
            End If
            Return "/NavigationDemo;component/Images/Departments/" & imageName & ".png"
        End Function

        #End Region
    End Class
    Public Class PhotoInfo
        Public Sub New(ByVal uri As String, ByVal rating As Integer)
            Image = New BitmapImage(New Uri(uri))
            Dimension = String.Format("{0}x{1}", Image.Width, Image.Height)
            Size = String.Format("{0} KB", ((New FileInfo(uri)).Length \ 1024).ToString())
            Name = Path.GetFileName(uri)
            Me.Uri = uri
            Me.Rating = rating
        End Sub
        Private privateImage As ImageSource
        Public Property Image() As ImageSource
            Get
                Return privateImage
            End Get
            Private Set(ByVal value As ImageSource)
                privateImage = value
            End Set
        End Property
        Private privateUri As String
        Public Property Uri() As String
            Get
                Return privateUri
            End Get
            Private Set(ByVal value As String)
                privateUri = value
            End Set
        End Property
        Private privateDimension As String
        Public Property Dimension() As String
            Get
                Return privateDimension
            End Get
            Private Set(ByVal value As String)
                privateDimension = value
            End Set
        End Property
        Private privateName As String
        Public Property Name() As String
            Get
                Return privateName
            End Get
            Private Set(ByVal value As String)
                privateName = value
            End Set
        End Property
        Private privateSize As String
        Public Property Size() As String
            Get
                Return privateSize
            End Get
            Private Set(ByVal value As String)
                privateSize = value
            End Set
        End Property
        Public Overridable Property Rating() As Integer
    End Class
End Namespace
