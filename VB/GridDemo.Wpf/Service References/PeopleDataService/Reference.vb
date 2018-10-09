Namespace GridDemo.PeopleDataService




    Partial Public Class Entities
        Inherits System.Data.Services.Client.DataServiceContext



        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Sub New(ByVal serviceRoot As Global.System.Uri)
            MyBase.New(serviceRoot)
            Me.ResolveName = New Global.System.Func(Of Global.System.Type, String)(AddressOf Me.ResolveNameFromType)
            Me.ResolveType = New Global.System.Func(Of String, Global.System.Type)(AddressOf Me.ResolveTypeFromName)
            Me.OnContextCreated()
        End Sub
        Partial Private Sub OnContextCreated()
        End Sub





        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Protected Function ResolveTypeFromName(ByVal typeName As String) As Global.System.Type
            If typeName.StartsWith("PersonDataBase.PeopleManagementContextModel", Global.System.StringComparison.Ordinal) Then
                Return Me.GetType().Assembly.GetType(String.Concat("GridDemo.PeopleDataService", typeName.Substring(43)), False)
            End If
            Return Nothing
        End Function





        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Protected Function ResolveNameFromType(ByVal clientType As Global.System.Type) As String
            If clientType.Namespace.Equals("GridDemo.PeopleDataService", Global.System.StringComparison.Ordinal) Then
                Return String.Concat("PersonDataBase.PeopleManagementContextModel.", clientType.Name)
            End If
            Return Nothing
        End Function



        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public ReadOnly Property People() As Global.System.Data.Services.Client.DataServiceQuery(Of Person)
            Get
                If (Me._People Is Nothing) Then
                    Me._People = MyBase.CreateQuery(Of Person)("People")
                End If
                Return Me._People
            End Get
        End Property
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _People As Global.System.Data.Services.Client.DataServiceQuery(Of Person)



        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Sub AddToPeople(ByVal person As Person)
            MyBase.AddObject("People", person)
        End Sub
    End Class






    <Global.System.Data.Services.Common.EntitySetAttribute("People"), Global.System.Data.Services.Common.DataServiceKeyAttribute("PersonID")>
    Partial Public Class Person
        Implements System.ComponentModel.INotifyPropertyChanged




        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Shared Function CreatePerson(ByVal personID As String) As Person
            Dim person As New Person()
            person.PersonID = personID
            Return person
        End Function



        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property PersonID() As String
            Get
                Return Me._PersonID
            End Get
            Set(ByVal value As String)
                Me.OnPersonIDChanging(value)
                Me._PersonID = value
                Me.OnPersonIDChanged()
                Me.OnPropertyChanged("PersonID")
            End Set
        End Property
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _PersonID As String
        Partial Private Sub OnPersonIDChanging(ByVal value As String)
        End Sub
        Partial Private Sub OnPersonIDChanged()
        End Sub



        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property FullName() As String
            Get
                Return Me._FullName
            End Get
            Set(ByVal value As String)
                Me.OnFullNameChanging(value)
                Me._FullName = value
                Me.OnFullNameChanged()
                Me.OnPropertyChanged("FullName")
            End Set
        End Property
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _FullName As String
        Partial Private Sub OnFullNameChanging(ByVal value As String)
        End Sub
        Partial Private Sub OnFullNameChanged()
        End Sub



        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property Company() As String
            Get
                Return Me._Company
            End Get
            Set(ByVal value As String)
                Me.OnCompanyChanging(value)
                Me._Company = value
                Me.OnCompanyChanged()
                Me.OnPropertyChanged("Company")
            End Set
        End Property
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _Company As String
        Partial Private Sub OnCompanyChanging(ByVal value As String)
        End Sub
        Partial Private Sub OnCompanyChanged()
        End Sub



        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property JobTitle() As String
            Get
                Return Me._JobTitle
            End Get
            Set(ByVal value As String)
                Me.OnJobTitleChanging(value)
                Me._JobTitle = value
                Me.OnJobTitleChanged()
                Me.OnPropertyChanged("JobTitle")
            End Set
        End Property
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _JobTitle As String
        Partial Private Sub OnJobTitleChanging(ByVal value As String)
        End Sub
        Partial Private Sub OnJobTitleChanged()
        End Sub



        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property City() As String
            Get
                Return Me._City
            End Get
            Set(ByVal value As String)
                Me.OnCityChanging(value)
                Me._City = value
                Me.OnCityChanged()
                Me.OnPropertyChanged("City")
            End Set
        End Property
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _City As String
        Partial Private Sub OnCityChanging(ByVal value As String)
        End Sub
        Partial Private Sub OnCityChanged()
        End Sub



        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property Address() As String
            Get
                Return Me._Address
            End Get
            Set(ByVal value As String)
                Me.OnAddressChanging(value)
                Me._Address = value
                Me.OnAddressChanged()
                Me.OnPropertyChanged("Address")
            End Set
        End Property
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _Address As String
        Partial Private Sub OnAddressChanging(ByVal value As String)
        End Sub
        Partial Private Sub OnAddressChanged()
        End Sub



        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property Phone() As String
            Get
                Return Me._Phone
            End Get
            Set(ByVal value As String)
                Me.OnPhoneChanging(value)
                Me._Phone = value
                Me.OnPhoneChanged()
                Me.OnPropertyChanged("Phone")
            End Set
        End Property
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _Phone As String
        Partial Private Sub OnPhoneChanging(ByVal value As String)
        End Sub
        Partial Private Sub OnPhoneChanged()
        End Sub



        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Property Email() As String
            Get
                Return Me._Email
            End Get
            Set(ByVal value As String)
                Me.OnEmailChanging(value)
                Me._Email = value
                Me.OnEmailChanged()
                Me.OnPropertyChanged("Email")
            End Set
        End Property
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Private _Email As String
        Partial Private Sub OnEmailChanging(ByVal value As String)
        End Sub
        Partial Private Sub OnEmailChanged()
        End Sub
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Public Event PropertyChanged As Global.System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")>
        Protected Overridable Sub OnPropertyChanged(ByVal [property] As String)
            RaiseEvent PropertyChanged(Me, New Global.System.ComponentModel.PropertyChangedEventArgs([property]))
        End Sub
    End Class
End Namespace
