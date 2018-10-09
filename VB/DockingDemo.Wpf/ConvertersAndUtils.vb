Imports DevExpress.Xpf.Docking
Imports DevExpress.Xpf.Docking.Base
Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace DockingDemo

    Public Class UniversalContainer(Of T)
        Public Property Name() As String
        Public Property DisplayName() As String
        Public Property Value() As T
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return If(TypeOf obj Is UniversalContainer(Of T), Object.Equals(Value, DirectCast(obj, UniversalContainer(Of T)).Value), False)
        End Function
        Public Overrides Function GetHashCode() As Integer
            Return Value.GetHashCode()
        End Function
    End Class
    Public Class AutoHideExpandModeContainer
        Inherits UniversalContainer(Of AutoHideExpandMode)

    End Class
    Public Class AutoHideModeContainer
        Inherits UniversalContainer(Of AutoHideMode)

    End Class
    Public Class TileLayoutContainer
        Inherits UniversalContainer(Of TileLayout)

    End Class


    Public Class UniversalContainerConverter(Of T)
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return If(TypeOf value Is T, GetContainer(DirectCast(value, T)), Nothing)
        End Function
        Protected Overridable Function GetContainer(ByVal value As T) As Object
            Return Nothing
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return DirectCast(value, UniversalContainer(Of T)).Value
        End Function
        #End Region
    End Class
    Public Class UniversalContainerConverter2(Of TValue, TContainer As UniversalContainer(Of TValue))
        Inherits UniversalContainerConverter(Of TValue)

        Protected Overrides Function GetContainer(ByVal value As TValue) As Object
            Dim container As TContainer = Activator.CreateInstance(Of TContainer)()
            container.Value = value
            Return container
        End Function
    End Class
    Public Class AutoHideExpandModeContainerConverter
        Inherits UniversalContainerConverter2(Of AutoHideExpandMode, AutoHideExpandModeContainer)

    End Class
    Public Class AutoHideModeContainerConverter
        Inherits UniversalContainerConverter2(Of AutoHideMode, AutoHideModeContainer)

    End Class
    Public Class TileLayoutContainerConverter
        Inherits UniversalContainerConverter(Of TileLayout)

    End Class
    Public Class SourceConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return value
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        #End Region
    End Class

    Public Enum TileLayout
        [Default]
        Layout3x2
        Layout3x3
        Layout4x2
    End Enum
    Friend NotInheritable Class TileLayoutExtension

        Private Sub New()
        End Sub

        Public Shared Function GetRowCount(ByVal layout As TileLayout) As Integer
            Return If(layout = TileLayout.Layout3x3, 3, 2)
        End Function
        Public Shared Function GetColumnsCount(ByVal layout As TileLayout) As Integer
            Return If(layout = TileLayout.Layout4x2, 4, 3)
        End Function
    End Class
    Friend NotInheritable Class TileImageHelper

        Private Sub New()
        End Sub

        Private Shared images As IDictionary(Of Integer, Image) = New Dictionary(Of Integer, Image)()
        Public Shared Function GetImage(ByVal index As Integer) As Image
            Dim result As Image = Nothing
            If Not images.TryGetValue(index, result) Then
                Dim uri As New Uri(String.Format("/Images/TileImages/{0:D2}.jpg", index), UriKind.Relative)
                result = New Image()
                result.Source = New System.Windows.Media.Imaging.BitmapImage(uri)
                images.Add(index, result)
            End If
            Return result
        End Function
    End Class
    Public NotInheritable Class FontSizes

        Private Sub New()
        End Sub

        Private Shared itemsCore() As Double = { 3.0, 4.0, 5.0, 6.0, 6.5, 7.0, 7.5, 8.0, 8.5, 9.0, 9.5, 10.0, 10.5, 11.0, 11.5, 12.0, 12.5, 13.0, 13.5, 14.0, 15.0, 16.0, 17.0, 18.0, 19.0, 20.0, 22.0, 24.0, 26.0, 28.0, 30.0, 32.0, 34.0, 36.0, 38.0, 40.0, 44.0, 48.0, 52.0, 56.0, 60.0, 64.0, 68.0, 72.0, 76.0, 80.0, 88.0, 96.0, 104.0, 112.0, 120.0, 128.0, 136.0, 144.0}
        Public Shared ReadOnly Property Items() As Double()
            Get
                Return itemsCore
            End Get
        End Property
    End Class
    Public NotInheritable Class FontFamilies

        Private Sub New()
        End Sub


        Private Shared fontNames_Renamed As List(Of String)
        Public Shared ReadOnly Property FontNames() As List(Of String)
            Get
                If fontNames_Renamed Is Nothing Then
                    fontNames_Renamed = GetSystemFontNames()
                End If
                Return fontNames_Renamed
            End Get
        End Property
        Private Shared Function GetSystemFontNames() As List(Of String)
            Dim systemFontNames As New List(Of String)()
            For Each fam As FontFamily In Fonts.SystemFontFamilies
                systemFontNames.Add(fam.Source)
            Next fam
            Return systemFontNames
        End Function
    End Class

    Public Class Employee
        Inherits DependencyObject

        Public Property TitleOfCourtesy() As String
        Public Property Title() As String
        Public Property FirstName() As String
        Public Property LastName() As String
        Public Property Notes() As String
        Public Property Address() As String
        Public Property Country() As String
        Public Property City() As String
        Public Property HomePhone() As String
        Public Property Region() As String
        Public Property PostalCode() As String
        Public Property BirthDate() As String
        Public Property HireDate() As String
        Public Property Extension() As String
        Public Property Photo() As ImageSource

        Public Shared Function CreateSampleData() As List(Of Employee)
            Dim employees As New List(Of Employee)()

            Dim employee_Renamed As New Employee()
            employee_Renamed.TitleOfCourtesy = "Dr."
            employee_Renamed.Title = "Sales Representative"
            employee_Renamed.FirstName = "Andrew"
            employee_Renamed.LastName = "Fuller"
            employee_Renamed.Notes = "Andrew received his BTS commercial in 1974 and a Ph.D. in international marketing from the University of Dallas in 1981.  He is fluent in French and Italian and reads German.  He joined the company as a sales representative, was promoted to sales manager in January 1992 and to vice president of sales in March 1993.  Andrew is a member of the Sales Management Roundtable, the Seattle Chamber of Commerce, and the Pacific Rim Importers Association."
            employee_Renamed.Address = "908 W. Capital Way"
            employee_Renamed.Country = "USA"
            employee_Renamed.City = "Tacoma"
            employee_Renamed.HomePhone = "(206) 555-9482"
            employee_Renamed.Region = "WA"
            employee_Renamed.PostalCode = "98401"
            employee_Renamed.Photo = New BitmapImage(New Uri("/DockingDemo;component/Images/LayoutItemsVisibility/person1.png", UriKind.RelativeOrAbsolute))
            employees.Add(employee_Renamed)

            employee_Renamed = New Employee()
            employee_Renamed.FirstName = "Janet"
            employee_Renamed.LastName = "Leverling"
            employee_Renamed.Notes = "Janet has a BS degree in chemistry from Boston College (1984).  She has also completed a certificate program in food retailing management.  Janet was hired as a sales associate in 1991 and promoted to sales representative in February 1992."
            employee_Renamed.City = "Kirkland"
            employee_Renamed.Photo = New BitmapImage(New Uri("/DockingDemo;component/Images/LayoutItemsVisibility/person2.png", UriKind.RelativeOrAbsolute))
            employees.Add(employee_Renamed)

            employee_Renamed = New Employee()
            employee_Renamed.TitleOfCourtesy = "Dr."
            employee_Renamed.LastName = "Evil"
            employee_Renamed.Photo = New BitmapImage(New Uri("/DockingDemo;component/Images/LayoutItemsVisibility/person3.png", UriKind.RelativeOrAbsolute))
            employees.Add(employee_Renamed)
            Return employees
        End Function
    End Class
    Public Class CommandsModel
        Private privateClose As ICommand
        Public Property Close() As ICommand
            Get
                Return privateClose
            End Get
            Private Set(ByVal value As ICommand)
                privateClose = value
            End Set
        End Property
        Private privateMDIStyle As ICommand
        Public Property MDIStyle() As ICommand
            Get
                Return privateMDIStyle
            End Get
            Private Set(ByVal value As ICommand)
                privateMDIStyle = value
            End Set
        End Property
        Private privateCascade As ICommand
        Public Property Cascade() As ICommand
            Get
                Return privateCascade
            End Get
            Private Set(ByVal value As ICommand)
                privateCascade = value
            End Set
        End Property
        Private privateTileHorizontal As ICommand
        Public Property TileHorizontal() As ICommand
            Get
                Return privateTileHorizontal
            End Get
            Private Set(ByVal value As ICommand)
                privateTileHorizontal = value
            End Set
        End Property
        Private privateTileVertical As ICommand
        Public Property TileVertical() As ICommand
            Get
                Return privateTileVertical
            End Get
            Private Set(ByVal value As ICommand)
                privateTileVertical = value
            End Set
        End Property
        Private privateArrangeIcons As ICommand
        Public Property ArrangeIcons() As ICommand
            Get
                Return privateArrangeIcons
            End Get
            Private Set(ByVal value As ICommand)
                privateArrangeIcons = value
            End Set
        End Property

        Public Sub New()
            Close = DockControllerCommand.Close
            MDIStyle = MDIControllerCommand.ChangeMDIStyle
            Cascade = MDIControllerCommand.Cascade
            TileHorizontal = MDIControllerCommand.TileHorizontal
            TileVertical = MDIControllerCommand.TileVertical
            ArrangeIcons = MDIControllerCommand.ArrangeIcons
        End Sub
        Public Property Target() As Object
    End Class
End Namespace
