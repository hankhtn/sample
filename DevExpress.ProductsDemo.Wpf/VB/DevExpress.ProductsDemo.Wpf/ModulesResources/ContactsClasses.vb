Imports DevExpress.DevAV
Imports DevExpress.Mvvm
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace ProductsDemo.Modules
    Public Class Contact
        Inherits BindableBase
        Implements ICloneable


        Private name_Renamed As Name
        Public Property Name() As Name
            Get
                Return name_Renamed
            End Get
            Set(ByVal value As Name)
                If name_Renamed IsNot value Then
                    name_Renamed = value
                    RaisePropertyChanged("Name")
                End If
            End Set
        End Property
        Public ReadOnly Property FirstName() As String
            Get
                Return If(Me.Name IsNot Nothing, Me.Name.FirstName, Nothing)
            End Get
        End Property
        Public ReadOnly Property LastName() As String
            Get
                Return If(Me.Name IsNot Nothing, Me.Name.LastName, Nothing)
            End Get
        End Property

        Private photo_Renamed As ImageSource
        Public Property Photo() As ImageSource
            Get
                Return photo_Renamed
            End Get
            Set(ByVal value As ImageSource)
                If photo_Renamed IsNot value Then
                    photo_Renamed = value
                    RaisePropertyChanged("Photo")
                End If
            End Set
        End Property

        Private address_Renamed As Address
        Public Property Address() As Address
            Get
                Return address_Renamed
            End Get
            Set(ByVal value As Address)
                If address_Renamed IsNot value Then
                    address_Renamed = value
                    RaisePropertyChanged("Address")
                End If
            End Set
        End Property

        Private email_Renamed As String
        Public Property Email() As String
            Get
                Return email_Renamed
            End Get
            Set(ByVal value As String)
                If email_Renamed <> value Then
                    email_Renamed = value
                    RaisePropertyChanged("Email")
                End If
            End Set
        End Property

        Private phone_Renamed As String
        Public Property Phone() As String
            Get
                Return phone_Renamed
            End Get
            Set(ByVal value As String)
                If phone_Renamed <> value Then
                    phone_Renamed = value
                    RaisePropertyChanged("Phone")
                End If
            End Set
        End Property

        Private birthDate_Renamed As Date
        Public Property BirthDate() As Date
            Get
                Return birthDate_Renamed
            End Get
            Set(ByVal value As Date)
                If birthDate_Renamed <> value Then
                    birthDate_Renamed = value
                    RaisePropertyChanged("BirthDate")
                End If
            End Set
        End Property

        Private prefix_Renamed As PersonPrefix
        Public Property Prefix() As PersonPrefix
            Get
                Return prefix_Renamed
            End Get
            Set(ByVal value As PersonPrefix)
                If Not Object.Equals(prefix_Renamed, value) Then
                    prefix_Renamed = value
                    RaisePropertyChanged("Prefix")
                End If
            End Set
        End Property

        Private notes_Renamed As String
        Public Property Notes() As String
            Get
                Return notes_Renamed
            End Get
            Set(ByVal value As String)
                If notes_Renamed <> value Then
                    notes_Renamed = value
                    RaisePropertyChanged("Notes")
                End If
            End Set
        End Property

        Private activity_Renamed As List(Of Integer)
        Public Property Activity() As List(Of Integer)
            Get
                Return activity_Renamed
            End Get
            Set(ByVal value As List(Of Integer))
                If activity_Renamed IsNot value Then
                    activity_Renamed = value
                    RaisePropertiesChanged("Activity")
                End If
            End Set
        End Property

        Public Sub New()
            Name = New Name()
            Address = New Address()
        End Sub

        Public Sub Assign(ByVal contact As Contact)
            Me.Name.Assign(contact.Name)
            Me.Photo = contact.Photo
            Me.Address.Assign(contact.Address)
            Me.Email = contact.Email
            Me.Phone = contact.Phone
            Me.BirthDate = contact.BirthDate
            Me.Prefix = contact.Prefix
            Me.Notes = contact.Notes
            Me.Activity = contact.Activity
            RaisePropertiesChanged(String.Empty)
        End Sub

        Public Function Clone() As Object Implements ICloneable.Clone
            Return New Contact() With {.Address = DirectCast(Me.Address.Clone(), Address), .BirthDate = Me.BirthDate, .Email = Me.Email, .Prefix= Me.Prefix, .Name = DirectCast(Me.Name.Clone(), Name), .Photo = Me.Photo, .Phone = Me.Phone, .Notes = Me.Notes, .Activity = Me.Activity}
        End Function
    End Class

    Public Class Name
        Inherits BindableBase
        Implements ICloneable


        Private firstName_Renamed As String
        Public Property FirstName() As String
            Get
                Return firstName_Renamed
            End Get
            Set(ByVal value As String)
                If firstName_Renamed <> value Then
                    firstName_Renamed = value
                    RaisePropertyChanged("FirstName")
                    UpdateFullName()
                End If
            End Set
        End Property

        Private middleName_Renamed As String
        Public Property MiddleName() As String
            Get
                Return middleName_Renamed
            End Get
            Set(ByVal value As String)
                If middleName_Renamed <> value Then
                    middleName_Renamed = value
                    RaisePropertyChanged("MiddleName")
                    UpdateFullName()
                End If
            End Set
        End Property

        Private lastName_Renamed As String
        Public Property LastName() As String
            Get
                Return lastName_Renamed
            End Get
            Set(ByVal value As String)
                If lastName_Renamed <> value Then
                    lastName_Renamed = value
                    RaisePropertyChanged("LastName")
                    UpdateFullName()
                End If
            End Set
        End Property

        Private fullName_Renamed As String
        Public Property FullName() As String
            Get
                Return fullName_Renamed
            End Get
            Set(ByVal value As String)
                If fullName_Renamed <> value Then
                    fullName_Renamed = value
                    RaisePropertyChanged("FullName")
                End If
            End Set
        End Property

        Private title_Renamed As ContactTitle
        Public Property Title() As ContactTitle
            Get
                Return title_Renamed
            End Get
            Set(ByVal value As ContactTitle)
                If title_Renamed <> value Then
                    title_Renamed = value
                    RaisePropertyChanged("Title")
                    UpdateFullName()
                End If
            End Set
        End Property
        Private Sub UpdateFullName()
            FullName = String.Format("{0}{1}{2}{3}",If(Title = ContactTitle.None, String.Empty, GetFormatString(Title.ToString())), GetFormatString(FirstName), GetFormatString(MiddleName), GetFormatString(LastName))
        End Sub
        Private Function GetFormatString(ByVal name As String) As String
            If String.IsNullOrEmpty(name) Then
                Return String.Empty
            End If
            Return String.Format("{0} ", name)
        End Function
        Public Sub Assign(ByVal name As Name)
            Me.FirstName = name.FirstName
            Me.MiddleName = name.MiddleName
            Me.LastName = name.LastName
            Me.Title = name.Title
        End Sub
        Public Overrides Function ToString() As String
            Return FullName
        End Function
        Public Function Clone() As Object Implements ICloneable.Clone
            Return New Name() With {.FirstName = Me.FirstName, .MiddleName = Me.MiddleName, .LastName = Me.LastName, .FullName = Me.FullName, .Title = Me.Title}
        End Function
    End Class
    Public Class Address
        Inherits BindableBase
        Implements ICloneable

        Public Sub New()
        End Sub
        Friend Sub New(ByVal addressString As String)
            Try
                Dim lines() As String = addressString.Split(","c)
                Me.AddressLine = lines(0).Trim()
                Me.City = lines(1).Trim()
                Me.State = lines(2).Trim().Substring(0, 2)
                Dim temp As String = lines(2).Trim()
                Me.Zip = temp.Substring(3, temp.Length - 3)
            Catch
            End Try
        End Sub

        Private addressLine_Renamed As String
        Public Property AddressLine() As String
            Get
                Return addressLine_Renamed
            End Get
            Set(ByVal value As String)
                If addressLine_Renamed <> value Then
                    addressLine_Renamed = value
                    RaisePropertyChanged("AddressLine")
                End If
            End Set
        End Property

        Private state_Renamed As String
        Public Property State() As String
            Get
                Return state_Renamed
            End Get
            Set(ByVal value As String)
                If state_Renamed <> value Then
                    state_Renamed = value
                    RaisePropertyChanged("State")
                End If
            End Set
        End Property

        Private city_Renamed As String
        Public Property City() As String
            Get
                Return city_Renamed
            End Get
            Set(ByVal value As String)
                If city_Renamed <> value Then
                    city_Renamed = value
                    RaisePropertyChanged("City")
                End If
            End Set
        End Property

        Private zip_Renamed As String
        Public Property Zip() As String
            Get
                Return zip_Renamed
            End Get
            Set(ByVal value As String)
                If zip_Renamed <> value Then
                    zip_Renamed = value
                    RaisePropertyChanged("Zip")
                End If
            End Set
        End Property
        Public Overrides Function ToString() As String
            Return GetAddressFullString(Environment.NewLine)
        End Function
        Public Function GetAddressFullString(ByVal separator As String) As String
            Return String.Format("{0}{1}{2}{3}{4}", AddressLine, separator, GetFormatString(City), GetFormatString(State, False), GetFormatString(Zip, False))
        End Function
        Private Function GetFormatString(ByVal name As String, Optional ByVal addComma As Boolean = True) As String
            If String.IsNullOrEmpty(name) Then
                Return String.Empty
            End If
            Dim format As String = If(addComma, "{0}, ", "{0} ")
            Return String.Format(format, name)
        End Function
        Public Sub Assign(ByVal address As Address)
            Me.AddressLine = address.AddressLine
            Me.State = address.State
            Me.City = address.City
            Me.Zip = address.Zip
        End Sub

        Public Function Clone() As Object Implements ICloneable.Clone
            Return New Address() With {.AddressLine = Me.AddressLine, .City = Me.City, .State = Me.State, .Zip = Me.Zip}
        End Function
    End Class


    Public Enum ContactTitle
        None
        Dr
        Miss
        Mr
        Mrs
        Ms
        Prof
    End Enum
End Namespace
