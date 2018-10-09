Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.Utils

Namespace ControlsDemo
    Partial Public Class BookCalendar
        Inherits ControlsDemoModule

        Public Sub New()
            InitializeComponent()
            InitCalendar()
            InitCities()
        End Sub

        Public Shared CurrentCoordinates As Coordinate
        Private Property Cities() As Dictionary(Of String, Coordinate)
        Private ReadOnly Property SelectedCity() As String
            Get
                Return CStr(city.SelectedItem)
            End Get
        End Property
        Private Property CurrentDayIndex() As Integer

        Private Sub InitCalendar()
            book.DataSource = GetCalendar()
            CurrentDayIndex = Date.Now.DayOfYear - 1
            book.PageIndex = CurrentDayIndex
        End Sub
        Private Sub InitCities()
            Cities = GetCities()
            FillCities()
        End Sub
        Private Function GetCalendar() As IList
            Dim result = New List(Of Date)()
            Dim [date] = Date.Now.AddDays(1 - Date.Now.DayOfYear)
            Do While [date].Year <= Date.Now.Year
                result.Add([date])
                [date] = [date].AddDays(1)
            Loop
            Return result
        End Function
        Private Function GetCities() As Dictionary(Of String, Coordinate)
            Dim result = New Dictionary(Of String, Coordinate)()
            result.Add("Beijing, China", New Coordinate(39.91, 116.39, 8.0))
            result.Add("Berlin, Germany", New Coordinate(52.50, 13.40, 1.0))
            result.Add("Cairo, Egypt", New Coordinate(30.06, 31.23, 2.0))
            result.Add("Johannesburg, South Africa", New Coordinate(-26.20, 28.05, 2.0))
            result.Add("Las Vegas, United States", New Coordinate(36.18, -115.14, -8.0))
            result.Add("London, United Kingdom", New Coordinate(51.51, 0.13, 0.0))
            result.Add("Los Angeles, United States", New Coordinate(34.05, -118.25, -8.0))
            result.Add("Moscow, Russia", New Coordinate(55.75, 37.62, 4.0))
            result.Add("New Delhi, India", New Coordinate(28.61, 77.21, 5.30))
            result.Add("New York, United States", New Coordinate(40.73, -74.0, -5.0))
            result.Add("Paris, France", New Coordinate(48.86, 2.35, 1.0))
            result.Add("Santiago, Chile", New Coordinate(-33.45, -70.67, -4.0))
            result.Add("Sydney, Australia", New Coordinate(-33.86, 151.21, 10.0))
            result.Add("Tokyo, Japan", New Coordinate(35.70, 137.72, 9.0))
            Return result
        End Function
        Private Sub FillCities()
            For Each name As String In Cities.Keys
                city.Items.Add(name)
            Next name
            city.SelectedIndex = 0
        End Sub
        Private Sub city_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            CurrentCoordinates = Cities(SelectedCity)
            book.DataSource = GetCalendar()
        End Sub
    End Class

    Public Class Coordinate
        Public Sub New(ByVal lat As Double, ByVal lon As Double, ByVal utcOffset As Double)
            Latitude = lat
            Longitude = lon
            Me.UtcOffset = utcOffset
        End Sub

        Private privateLongitude As Double
        Public Property Longitude() As Double
            Get
                Return privateLongitude
            End Get
            Private Set(ByVal value As Double)
                privateLongitude = value
            End Set
        End Property
        Private privateLatitude As Double
        Public Property Latitude() As Double
            Get
                Return privateLatitude
            End Get
            Private Set(ByVal value As Double)
                privateLatitude = value
            End Set
        End Property
        Private privateUtcOffset As Double
        Public Property UtcOffset() As Double
            Get
                Return privateUtcOffset
            End Get
            Private Set(ByVal value As Double)
                privateUtcOffset = value
            End Set
        End Property
    End Class

    Public Class TextConverter
        Implements IValueConverter

        Public Shared Holidays As Dictionary(Of String, String)

        Shared Sub New()
            Holidays = New Dictionary(Of String, String)()
            Holidays.Add("January 01", "New Year's Day, Kwanzaa")
            Holidays.Add("January 19", "Martin Luther King, Jr. Day")
            Holidays.Add("January 20", "Inauguration Day")
            Holidays.Add("February 01", "Super Bowl Sunday")
            Holidays.Add("February 02", "Groundhog Day")
            Holidays.Add("February 14", "Valentine's Day")
            Holidays.Add("February 16", "Presidents Day (officially George Washington's Birthday)")
            Holidays.Add("February 25", "Ash Wednesday")
            Holidays.Add("March 17", "St. Patrick's Day")
            Holidays.Add("March 20", "Vernal Equinox")
            Holidays.Add("April 01", "April Fools' Day")
            Holidays.Add("April 05", "Palm Sunday")
            Holidays.Add("April 09", "First day of Passover")
            Holidays.Add("April 10", "Good Friday")
            Holidays.Add("April 16", "Last Day of Passover")
            Holidays.Add("April 20", "Patriot's Day/Marathon Monday")
            Holidays.Add("April 22", "Earth Day")
            Holidays.Add("April 24", "Arbor Day")
            Holidays.Add("May 05", "Cinco De Mayo")
            Holidays.Add("May 10", "Mother's Day")
            Holidays.Add("May 25", "Memorial Day")
            Holidays.Add("May 31", "Pentecost Sunday")
            Holidays.Add("June 14", "Flag Day")
            Holidays.Add("June 21", "Father's Day, Summer Solstice")
            Holidays.Add("July 04", "Independence Day")
            Holidays.Add("July 24", "Pioneer Day")
            Holidays.Add("August 22", "First day of Ramadan")
            Holidays.Add("September 07", "Labor Day")
            Holidays.Add("September 13", "Grandparents Day")
            Holidays.Add("September 19", "Rosh Hashanah")
            Holidays.Add("September 20", "Last day of Ramadan")
            Holidays.Add("September 21", "Eid-al-Fitr/Day after the end of Ramadan")
            Holidays.Add("September 22", "Autumnal equinox")
            Holidays.Add("September 28", "Yom Kippur")
            Holidays.Add("October 03", "First day of Sukkot")
            Holidays.Add("October 09", "Leif Erikson Day, Last Day of Sukkot")
            Holidays.Add("October 10", "Simchat Torah")
            Holidays.Add("October 12", "Columbus Day")
            Holidays.Add("October 30", "Mischief Night")
            Holidays.Add("October 31", "Halloween")
            Holidays.Add("November 01", "All Saints Day")
            Holidays.Add("November 11", "Veterans Day")
            Holidays.Add("November " & GetThanksgiving().ToString(), "Thanksgiving")
            Holidays.Add("November " & (GetThanksgiving() + 1).ToString(), "Black Friday")
            Holidays.Add("December 07", "Pearl Harbor Remembrance Day")
            Holidays.Add("December 12", "First day of Hanukkah")
            Holidays.Add("December 19", "Last day of Hanukkah")
            Holidays.Add("December 21", "Winter Solstice")
            Holidays.Add("December 24", "Christmas Eve")
            Holidays.Add("December 25", "Christmas Day")
            Holidays.Add("December 26", "First day of Kwanzaa")
            Holidays.Add("December 27", "Kwanzaa")
            Holidays.Add("December 28", "Kwanzaa")
            Holidays.Add("December 29", "Kwanzaa")
            Holidays.Add("December 30", "Kwanzaa")
            Holidays.Add("December 31", "New Year's Eve, Kwanzaa")

        End Sub

        Public Shared Function GetThanksgiving() As Integer
            Dim dt As New Date(Date.Now.Year, 11, 1)
            Dim count As Integer = 0
            Do While count < 4
                If dt.DayOfWeek = DayOfWeek.Thursday Then
                    count += 1
                End If
                dt = dt.AddDays(1)
            Loop
            Return dt.Day - 1
        End Function
        Public Shared Function IsHoliday(ByVal d As Date) As Boolean
            Return Holidays.ContainsKey(GetKey(d))
        End Function
        Private Shared Function GetKey(ByVal d As Date) As String
            Return d.ToString("MMMM dd", CultureInfo.InvariantCulture.DateTimeFormat)
        End Function

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return String.Empty
            End If
            Dim d As Date = DirectCast(value, Date)
            If IsHoliday(d) Then
                Return Holidays(GetKey(d))
            End If
            Return " "
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        #End Region
    End Class

    Public Class MonthConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return String.Empty
            End If
            Return DirectCast(value, Date).ToString("MMMM", CultureInfo.InvariantCulture.DateTimeFormat)
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        #End Region
    End Class

    Public Class DayConverter
        Implements IValueConverter

        #Region "IValueConverter Members"

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return Nothing
            End If
            Dim [date] As Date = DirectCast(value, Date)
            If [date].Month = 4 AndAlso [date].Day = 12 Then
                Dim assembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
                Dim bi As New BitmapImage()
                Using stream As Stream = AssemblyHelper.GetResourceStream(assembly, "gagarin.jpg", False)
                    bi.BeginInit()
                    bi.StreamSource = stream
                    bi.EndInit()
                End Using
                Return New Image() With {.Source = bi, .Stretch = Stretch.Uniform, .Height = 170, .Margin = New Thickness(20)}
            End If
            Return [date].Day
        End Function

        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        #End Region
    End Class

    Public Class DOWConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return String.Empty
            End If
            Return DirectCast(value, Date).ToString("dddd", CultureInfo.InvariantCulture.DateTimeFormat)
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        #End Region
    End Class

    Public Class ForegroundConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return Nothing
            End If
            Dim dow As DayOfWeek = DirectCast(value, Date).DayOfWeek
            If dow = DayOfWeek.Sunday OrElse dow = DayOfWeek.Saturday OrElse TextConverter.IsHoliday(DirectCast(value, Date)) Then
                Return New SolidColorBrush(Color.FromArgb(&HFF, &HC0, &H1D, &H1D))
            End If
            Return New SolidColorBrush(Color.FromArgb(&HFF, &H65, &H67, &H85))
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        #End Region
    End Class

    Public Class SunRiseConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return String.Empty
            End If
            Dim utcOffset As Double = BookCalendar.CurrentCoordinates.UtcOffset
            Dim lat As Double = BookCalendar.CurrentCoordinates.Latitude
            Dim lon As Double = BookCalendar.CurrentCoordinates.Longitude
            Dim time? As Date = SunCalculatior.SunRise(DirectCast(value, Date), utcOffset, lat, lon)
            If time.HasValue Then
                Return String.Format("Sun rise: {0}", time.Value.ToString("t", CultureInfo.InvariantCulture))
            End If
            Return "There is no sun rise today"
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        #End Region
    End Class

    Public Class SunSetConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return String.Empty
            End If
            Dim utcOffset As Double = BookCalendar.CurrentCoordinates.UtcOffset
            Dim lat As Double = BookCalendar.CurrentCoordinates.Latitude
            Dim lot As Double = BookCalendar.CurrentCoordinates.Longitude
            Dim time? As Date = SunCalculatior.SunSet(DirectCast(value, Date), utcOffset, lat, lot)
            If time.HasValue Then
                Return String.Format("Sun set: {0}", time.Value.ToString("t", CultureInfo.InvariantCulture))
            End If
            Return "There is no sun set today"
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        #End Region
    End Class
End Namespace
