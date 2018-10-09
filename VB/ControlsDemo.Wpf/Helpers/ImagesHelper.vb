Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace CommonDemo.Helpers
    Public NotInheritable Class ImagesHelper
        Private Shared privateLasVegasPhotos As IEnumerable(Of ImageSource)
        Public Shared Property LasVegasPhotos() As IEnumerable(Of ImageSource)
            Get
                Return privateLasVegasPhotos
            End Get
            Private Set(ByVal value As IEnumerable(Of ImageSource))
                privateLasVegasPhotos = value
            End Set
        End Property

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property LasVegasPhoto1() As ImageSource
            Get
                Return LasVegasPhotos.ElementAt(0)
            End Get
        End Property
        Public Shared ReadOnly Property LasVegasPhoto2() As ImageSource
            Get
                Return LasVegasPhotos.ElementAt(1)
            End Get
        End Property
        Public Shared ReadOnly Property LasVegasPhoto3() As ImageSource
            Get
                Return LasVegasPhotos.ElementAt(2)
            End Get
        End Property
        Public Shared ReadOnly Property LasVegasPhoto4() As ImageSource
            Get
                Return LasVegasPhotos.ElementAt(3)
            End Get
        End Property
        Public Shared Function GetRandomLasVegasPhoto() As ImageSource
            Return LasVegasPhotos.ElementAt(rnd.Next(LasVegasPhotos.Count() - 1))
        End Function

        Private Shared privateNaturePhotos As IEnumerable(Of ImageSource)
        Public Shared Property NaturePhotos() As IEnumerable(Of ImageSource)
            Get
                Return privateNaturePhotos
            End Get
            Private Set(ByVal value As IEnumerable(Of ImageSource))
                privateNaturePhotos = value
            End Set
        End Property
        Public Shared ReadOnly Property NaturePhoto1() As ImageSource
            Get
                Return NaturePhotos.ElementAt(0)
            End Get
        End Property
        Public Shared ReadOnly Property NaturePhoto2() As ImageSource
            Get
                Return NaturePhotos.ElementAt(1)
            End Get
        End Property
        Public Shared ReadOnly Property NaturePhoto3() As ImageSource
            Get
                Return NaturePhotos.ElementAt(2)
            End Get
        End Property
        Public Shared ReadOnly Property NaturePhoto4() As ImageSource
            Get
                Return NaturePhotos.ElementAt(3)
            End Get
        End Property
        Public Shared ReadOnly Property NaturePhoto5() As ImageSource
            Get
                Return NaturePhotos.ElementAt(4)
            End Get
        End Property
        Public Shared ReadOnly Property NaturePhoto6() As ImageSource
            Get
                Return NaturePhotos.ElementAt(5)
            End Get
        End Property
        Public Shared ReadOnly Property NaturePhoto7() As ImageSource
            Get
                Return NaturePhotos.ElementAt(6)
            End Get
        End Property
        Public Shared ReadOnly Property NaturePhoto8() As ImageSource
            Get
                Return NaturePhotos.ElementAt(7)
            End Get
        End Property
        Public Shared ReadOnly Property NaturePhoto9() As ImageSource
            Get
                Return NaturePhotos.ElementAt(8)
            End Get
        End Property
        Public Shared ReadOnly Property NaturePhoto10() As ImageSource
            Get
                Return NaturePhotos.ElementAt(9)
            End Get
        End Property
        Public Shared Function GetRandomNaturePhoto() As ImageSource
            Return NaturePhotos.ElementAt(rnd.Next(NaturePhotos.Count() - 1))
        End Function

        Private Shared privateGroupIcon As ImageSource
        Public Shared Property GroupIcon() As ImageSource
            Get
                Return privateGroupIcon
            End Get
            Private Set(ByVal value As ImageSource)
                privateGroupIcon = value
            End Set
        End Property

        Public Shared Function GetWebIcon(ByVal icon As String) As ImageSource
            Return New BitmapImage(New Uri("/ControlsDemo;component/Images/TabControl/" & icon, UriKind.Relative))
        End Function

        Shared Sub New()
            Dim lasVegasUriPath As String = "/ControlsDemo;component/Images/Photos/Las Vegas/"
            Dim getLasVegasImage As Func(Of String, ImageSource) = Function(x) New BitmapImage(New Uri(lasVegasUriPath & x, UriKind.Relative))

            Dim lasVegasPhotos_Renamed = New List(Of ImageSource)()
            lasVegasPhotos_Renamed.Add(getLasVegasImage("DSC_4405.jpg"))
            lasVegasPhotos_Renamed.Add(getLasVegasImage("DSC_4498.jpg"))
            lasVegasPhotos_Renamed.Add(getLasVegasImage("DSC_4647.jpg"))
            lasVegasPhotos_Renamed.Add(getLasVegasImage("DSC_4721.jpg"))
            lasVegasPhotos_Renamed.Add(getLasVegasImage("IMG_1216.jpg"))
            lasVegasPhotos_Renamed.Add(getLasVegasImage("IMG_1280.jpg"))
            lasVegasPhotos_Renamed.Add(getLasVegasImage("IMG_1285.jpg"))
            lasVegasPhotos_Renamed.Add(getLasVegasImage("IMG_1321.jpg"))
            lasVegasPhotos_Renamed.Add(getLasVegasImage("IMG_1327.jpg"))
            lasVegasPhotos_Renamed.Add(getLasVegasImage("IMG_1345.jpg"))
            LasVegasPhotos = lasVegasPhotos_Renamed

            Dim natureUriPath As String = "/ControlsDemo;component/Images/Photos/Nature/"
            Dim getNatureImage As Func(Of String, ImageSource) = Function(x) New BitmapImage(New Uri(natureUriPath & x, UriKind.Relative))

            Dim naturePhotos_Renamed = New List(Of ImageSource)()
            naturePhotos_Renamed.Add(getNatureImage("01.JPG"))
            naturePhotos_Renamed.Add(getNatureImage("02.JPG"))
            naturePhotos_Renamed.Add(getNatureImage("03.JPG"))
            naturePhotos_Renamed.Add(getNatureImage("04.JPG"))
            naturePhotos_Renamed.Add(getNatureImage("05.JPG"))
            naturePhotos_Renamed.Add(getNatureImage("06.JPG"))
            naturePhotos_Renamed.Add(getNatureImage("07.JPG"))
            naturePhotos_Renamed.Add(getNatureImage("08.JPG"))
            naturePhotos_Renamed.Add(getNatureImage("09.JPG"))
            naturePhotos_Renamed.Add(getNatureImage("10.JPG"))
            NaturePhotos = naturePhotos_Renamed

            GroupIcon = New BitmapImage(New Uri("/ControlsDemo;component/Images/TabControl/GroupIcon.png", UriKind.Relative))

        End Sub
        Private Shared ReadOnly rnd As New Random()
    End Class
End Namespace
