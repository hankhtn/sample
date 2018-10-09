Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.Xpf.Core

Namespace LayoutControlDemo
    Partial Public Class pageRealEstate
        Inherits LayoutControlDemoModule

        Public Sub New()
            InitializeComponent()
            ActiveListingIndex = 0
        End Sub

        Public Property ActiveListingIndex() As Integer
            Get
                Return Listings.DataSource.IndexOf(CType(LayoutRoot.DataContext, Listing))
            End Get
            Set(ByVal value As Integer)
                value = Math.Min(Math.Max(0, value), Listings.DataSource.Count - 1)
                If ActiveListingIndex <> value Then
                    LayoutRoot.DataContext = Listings.DataSource(value)
                End If
            End Set
        End Property

        Protected Overrides Sub Clear()
            MyBase.Clear()
        End Sub

        Private Sub NextButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ActiveListingIndex += 1
        End Sub
        Private Sub PrevButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ActiveListingIndex -= 1
        End Sub
    End Class

    Public Class Listing
        Public Property Address() As String
        Public Property Price() As Integer
        Public Property Image() As String
        Public ReadOnly Property ImageSource() As ImageSource
            Get
                Return If(String.IsNullOrEmpty(Image), Nothing, New BitmapImage(New Uri(Image, UriKind.Relative)))
            End Get
        End Property
        Public Property Description() As String
        Public Property Bedrooms() As Integer
        Public Property Baths() As Double
        Public Property Built() As Integer
        Public Property SqFootage() As Integer
        Public ReadOnly Property PricePerSqFoot() As Integer
            Get
                Return CInt(Price \ SqFootage)
            End Get
        End Property
        Public Property LotSize() As Integer
        Public Property ListDate() As Date
        Public Property MLS() As String

        Public Property Kitchen() As String
        Public Property DiningRoom() As String
        Public Property Bedroom() As String
        Public Property LivingRoom() As Boolean
        Public Property FamilyRoom() As Boolean
        Public Property FloorCoverings() As String
        Public Property Laundry() As Boolean
        Public Property Fireplace() As String

        Public Property Stories() As Integer
        Public Property RoofType() As String
        Public Property Parking() As String
        Public Property Yard() As String
        Public Property PoolType() As String
        Public Property Spa() As String

        Public Property Amenities() As String
        Public Property HOAFee() As Integer
        Public Property HOAFeeIncludes() As String
    End Class

    Public NotInheritable Class Listings

        Private Sub New()
        End Sub

        Public Shared ReadOnly DataSource As New List(Of Listing)() From {
            New Listing With {.Address = "6127 Kona Crest Ave, Pasadena, CA 91203", .Price = 345999, .Image = LayoutControlDemoModule.UriPrefix & "/Images/RealEstate/1.jpg", .Description = "This wonderful condo in the Playhouse district of Pasadena features over 1,560 sq. ft. of spacious & luxurious " & "living with 3 bdrm, 2 bath, secured entry, brand new carpet, recessed lighting throughout, window blinds, " & "walk-in mirrored closets, 2 parking stalls, guest parking, front & back private patio. Minutes from paseo shopping center. " & "This is an ideal condo in a great neighborhood. Come take a look.", .Bedrooms = 3, .Baths = 2, .Built = 1997, .SqFootage = 1567, .ListDate = New Date(2009, 12, 25), .MLS = "C09678945", .Kitchen = "BUILT-IN RANGE/OVEN, GAS RANGE/OVEN, DISHWASHER, MICROWAVE", .DiningRoom = "DINING ELL", .Bedroom = "MASTER BEDROOM, WALK-IN CLOSET", .LivingRoom = True, .FamilyRoom = True, .FloorCoverings = "WALL TO WALL CARPET", .Laundry = True, .Stories = 1, .RoofType = "CONCRETE TILE", .Parking = "2 PARKING SPACES", .Yard = "BALCONY", .Spa = "COMMUNITY SAUNA", .Amenities = "ASSOCIATION BARBECUE", .HOAFee = 250},
            New Listing With {.Address = "108 Brand Blvd, Seattle, WA 98052", .Price = 589000, .Image = LayoutControlDemoModule.UriPrefix & "/Images/RealEstate/2.jpg", .Description = "Gorgeous gated Seattle beauty with the best upgrades!! Open kitchen/family room with granite counters, " & "tile backsplash. Wood floors, tile in baths - all very neutral. Plantation shutters throughout, large master with fireplace, " & "marble countertops in master bath & deck off master with views. Private yard with built-in bbq. Nice!", .Bedrooms = 4, .Baths = 3.75, .Built = 2007, .SqFootage = 2590, .LotSize = 7812, .ListDate = New Date(2008, 1, 30), .MLS = "W67340912", .Kitchen = "BUILT-IN BARBEQUE, BUILT-IN GAS RANGE, DISHWASHER, GARBAGE DISPOSAL, GAS OVEN, MICROWAVE", .DiningRoom = "EATING AREA", .Bedroom = "MASTER BEDROOM", .LivingRoom = True, .FloorCoverings = "CARPET - PARTIAL, CERAMIC TILE, WOOD", .Laundry = True, .Fireplace = "GAS, IN LIVING ROOM, IN MASTER BEDROOM", .Stories = 2, .RoofType = "CONCRETE TILE", .Parking = "DIRECT GARAGE ACCESS, ATTACHED GARAGE", .Yard = "BALCONY, CONCRETE SLAB PATIO; YARD ENCLOSED WITH : BLOCK WALL", .PoolType = "ASSOCIATION POOL", .Spa = "ASSOCIATION SPA", .Amenities = "GATED COMMUNITY", .HOAFee = 170, .HOAFeeIncludes = "CLUB HOUSE RECREATION FACILITIES"},
            New Listing With {.Address = "777 Las Vegas Blvd, Las Vegas, NV 89120", .Price = 777333, .Image = LayoutControlDemoModule.UriPrefix & "/Images/RealEstate/3.jpg", .Description = "Loft condominium. New construction. 2-story open floorplan with spiral staircase, high ceilings, " & "large windows and 2 balconies (5 units left). Unit feature fine italian kitchens with caesar stone counter tops, " & "limited stainless steel appliances, custom designed bathroom sink/cabinets with tile floor and bathtub surround. " & "2 side by side parking spaces. Security system, video intercom system, keyfob entry to building, HD DirecTV and cable pre-wired.", .Bedrooms = 2, .Baths = 2.5, .Built = 2009, .SqFootage = 3100, .ListDate = New Date(2009, 4, 5), .MLS = "N55200987", .Kitchen = "DISHWASHER, GARBAGE DISPOSAL, MICROWAVE, RANGE/OVEN", .DiningRoom = "BREAKFAST AREA", .LivingRoom = True, .FloorCoverings = "HARDWOOD, TILE", .Laundry = False, .Fireplace = "GAS, IN DINING ROOM", .Stories = 2, .Parking = "COMMUNITY GARAGE, SIDE BY SIDE, SUBTERRANEAN", .Yard = "OPEN PATIO", .Spa = "ROOFTOP SPA", .Amenities = "ALARM SYSTEM, CABLE, INTERCOM, NETWORK WIRE, SATELLITE, GATED SECURITY", .HOAFee = 314}
        }
    End Class

    Public Class ListingPositionToBoolConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim listing = TryCast(value, Listing)
            Return parameter.Equals("IsNotFirst") AndAlso Listings.DataSource.IndexOf(listing) > 0 OrElse parameter.Equals("IsNotLast") AndAlso Listings.DataSource.IndexOf(listing) < Listings.DataSource.Count - 1
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
