Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations

Namespace EditorsDemo
    Friend NotInheritable Class EmployeeImageHelper

        Private Sub New()
        End Sub

        Public Const ImagesPath As String = "pack://application:,,,/EditorsDemo;component/Images/Overview/"
    End Class
    Public Enum PersonPrefix
        <Image(EmployeeImageHelper.ImagesPath & "Person/Dr.png")>
        Dr
        <Image(EmployeeImageHelper.ImagesPath & "Person/Mr.png")>
        Mr
        <Image(EmployeeImageHelper.ImagesPath & "Person/Ms.png")>
        Ms
        <Image(EmployeeImageHelper.ImagesPath & "Person/Miss.png")>
        Miss
        <Image(EmployeeImageHelper.ImagesPath & "Person/Mrs.png")>
        Mrs
    End Enum

    Public Enum EmployeeStatus
        Salaried
        Commission
        Terminated
        OnLeave
    End Enum
    Public Enum EmployeeDepartment
        Sales
        Support
        Shipping
        Engineering
        HumanResources
        Management
        IT
    End Enum
    Public Enum StateEnum
        CA
        AR
        AL
        AK
        AZ
        CO
        CT
        DE
        DC
        FL
        GA
        HI
        ID
        IL
        [IN]
        IA
        KS
        KY
        LA
        [ME]
        MD
        MA
        MI
        MN
        MS
        MO
        MT
        NE
        NV
        NH
        NJ
        NM
        NY
        NC
        OH
        OK
        [OR]
        PA
        RI
        SC
        SD
        TN
        TX
        UT
        VT
        VA
        WA
        WV
        WI
        WY
        ND
    End Enum

    Public Enum EmployeeTaskPriority
        <Image(EmployeeImageHelper.ImagesPath & "Priority/Low.png")>
        Low
        <Image(EmployeeImageHelper.ImagesPath & "Priority/Normal.png")>
        Normal
        <Image(EmployeeImageHelper.ImagesPath & "Priority/Medium.png")>
        High
        <Image(EmployeeImageHelper.ImagesPath & "Priority/High.png")>
        Urgent
    End Enum

    Public Class EmployeeTask
        Public Property Completion() As Integer
        Public Property Description() As String
        Public Property DueDate() As Date?
        Public Property Priority() As EmployeeTaskPriority
        Public Property StartDate() As Date?
        Public Property Subject() As String
    End Class
    Public Class Evaluation
        Public Property CreatedOn() As Date
        Public Property Manager() As String
        Public Property Subject() As String
    End Class
    Public Class Address
        Inherits ViewModelBase


        Private city_Renamed As String

        Private line_Renamed As String

        Private state_Renamed As StateEnum

        Private zipCode_Renamed As String
        Public Property City() As String
            Get
                Return city_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(city_Renamed, value, "City")
            End Set
        End Property
        Public Property Line() As String
            Get
                Return line_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(line_Renamed, value, "Line")
            End Set
        End Property
        Public Property State() As StateEnum
            Get
                Return state_Renamed
            End Get
            Set(ByVal value As StateEnum)
                SetProperty(state_Renamed, value, "State")
            End Set
        End Property
        Public Property ZipCode() As String
            Get
                Return zipCode_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(zipCode_Renamed, value, "ZipCode")
            End Set
        End Property
        Public Sub New()
            Initialize()
        End Sub
        Private Sub Initialize()
            Line = "6755 Newlin Ave"
            City = "Whittier"
            State = StateEnum.AK
            ZipCode = "90601"
        End Sub
    End Class

    Public Class EmployeeViewModel
        Inherits ViewModelBase
        Implements IDataErrorInfo


        Private address_Renamed As Address

        Private assignedTasks_Renamed As List(Of EmployeeTask)

        Private birthDate_Renamed? As Date

        Private department_Renamed As EmployeeDepartment

        Private email_Renamed As String

        Private evaluations_Renamed As List(Of Evaluation)

        Private firstName_Renamed As String

        Private hireDate_Renamed? As Date

        Private homePhone_Renamed As String

        Private lastName_Renamed As String

        Private mobilePhone_Renamed As String

        Private personalProfile_Renamed As String

        Private photo_Renamed As ImageSource

        Private prefix_Renamed As PersonPrefix

        Private probationReason_Renamed As String

        Private skype_Renamed As String

        Private status_Renamed As EmployeeStatus

        Private title_Renamed As String
        Public Property Address() As Address
            Get
                Return address_Renamed
            End Get
            Set(ByVal value As Address)
                SetProperty(address_Renamed, value, "Address")
            End Set
        End Property
        Public Property AssignedTasks() As List(Of EmployeeTask)
            Get
                Return assignedTasks_Renamed
            End Get
            Set(ByVal value As List(Of EmployeeTask))
                SetProperty(assignedTasks_Renamed, value, "AssignedTasks")
            End Set
        End Property
        Public Property BirthDate() As Date?
            Get
                Return birthDate_Renamed
            End Get
            Set(ByVal value? As Date)
                SetProperty(birthDate_Renamed, value, "BirthDate")
            End Set
        End Property
        Public Property Department() As EmployeeDepartment
            Get
                Return department_Renamed
            End Get
            Set(ByVal value As EmployeeDepartment)
                SetProperty(department_Renamed, value, "Department")
            End Set
        End Property
        Public Property Email() As String
            Get
                Return email_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(email_Renamed, value, "Email")
            End Set
        End Property
        Public Property Evaluations() As List(Of Evaluation)
            Get
                Return evaluations_Renamed
            End Get
            Set(ByVal value As List(Of Evaluation))
                SetProperty(evaluations_Renamed, value, "Evaluations")
            End Set
        End Property
        Public Property FirstName() As String
            Get
                Return firstName_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(firstName_Renamed, value, "FirstName", AddressOf RaiseFullNamePropertyChanged)
            End Set
        End Property
        Public ReadOnly Property FullName() As String
            Get
                Return String.Format("{0} {1}", FirstName, LastName)
            End Get
        End Property
        Public Property HireDate() As Date?
            Get
                Return hireDate_Renamed
            End Get
            Set(ByVal value? As Date)
                SetProperty(hireDate_Renamed, value, "HireDate")
            End Set
        End Property
        Public Property HomePhone() As String
            Get
                Return homePhone_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(homePhone_Renamed, value, "HomePhone")
            End Set
        End Property
        Public Property LastName() As String
            Get
                Return lastName_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(lastName_Renamed, value, "LastName", AddressOf RaiseFullNamePropertyChanged)
            End Set
        End Property
        Public Property MobilePhone() As String
            Get
                Return mobilePhone_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(mobilePhone_Renamed, value, "MobilePhone")
            End Set
        End Property
        Public Property PersonalProfile() As String
            Get
                Return personalProfile_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(personalProfile_Renamed, value, "PersonalProfile")
            End Set
        End Property
        Public Property Photo() As ImageSource
            Get
                Return photo_Renamed
            End Get
            Set(ByVal value As ImageSource)
                SetProperty(photo_Renamed, value, "Photo")
            End Set
        End Property
        Public Property Prefix() As PersonPrefix
            Get
                Return prefix_Renamed
            End Get
            Set(ByVal value As PersonPrefix)
                SetProperty(prefix_Renamed, value, "Prefix")
            End Set
        End Property
        Public Property ProbationReason() As String
            Get
                Return probationReason_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(probationReason_Renamed, value, "ProbationReason")
            End Set
        End Property
        Public Property Skype() As String
            Get
                Return skype_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(skype_Renamed, value, "Skype")
            End Set
        End Property
        Public Property Status() As EmployeeStatus
            Get
                Return status_Renamed
            End Get
            Set(ByVal value As EmployeeStatus)
                SetProperty(status_Renamed, value, "Status")
            End Set
        End Property
        Public Property Title() As String
            Get
                Return title_Renamed
            End Get
            Set(ByVal value As String)
                SetProperty(title_Renamed, value, "Title")
            End Set
        End Property
        Public Sub New()
            Initialize()
        End Sub
        Private Sub Initialize()
            Address = New Address()
            FirstName = "Leah"
            LastName = "Simpson"
            BirthDate = New Date(1983, 4, 19)
            Title = "Test Coordinator"
            Prefix = PersonPrefix.Mrs
            HomePhone = "(562) 555-7372"
            MobilePhone = "(562) 559-5830"
            Email = "leahs@dx-email.com"
            Skype = "leahs_DX_skype"
            Department = EmployeeDepartment.Engineering
            Status = EmployeeStatus.Salaried
            HireDate = New Date(2009, 2, 14)
            Photo = New BitmapImage(New Uri("pack://application:,,,/EditorsDemo;component/Images/Persons/w03.jpg"))
            GenerateAssignedTasks()
            GenerateEvaluations()
        End Sub
        Private Sub RaiseFullNamePropertyChanged()
            RaisePropertyChanged("FullName")
        End Sub
        Private Sub GenerateAssignedTasks()
            AssignedTasks = New List(Of EmployeeTask)()
            Dim employeeTask1 As New EmployeeTask()
            employeeTask1.Priority = EmployeeTaskPriority.Normal
            employeeTask1.DueDate = Date.Now + New TimeSpan(10, 10, 0, 0, 0)
            employeeTask1.Subject = String.Format("{0} QA Strategy Report", Date.Now.Year)
            employeeTask1.Description = String.Format("In final stages of the {0} R & D Report to Management.Need QA strategy report asap.Remember, {1} was a difficult year product quality-wise and we must step it up in {0}. Leah Simpson: Bart, my apologies about {1}.My report includes remedies to issues we encountered.", Date.Now.Year, Date.Now.Year - 1)
            employeeTask1.Completion = 100
            AssignedTasks.Add(employeeTask1)

            Dim employeeTask2 As New EmployeeTask()
            employeeTask2.Priority = EmployeeTaskPriority.Urgent
            employeeTask2.DueDate = Date.Now + New TimeSpan(40, 10, 0, 0, 0)
            employeeTask2.Subject = "Review Training Course for any Commissions"
            employeeTask2.Description = "Leah, consider this most important item on your agenda. I need this new training material reviewed so it can be submitted to management. Leah Simpson: I only found a few spelling mistakes."
            employeeTask2.Completion = 70
            AssignedTasks.Add(employeeTask2)


            Dim employeeTask3 As New EmployeeTask()
            employeeTask3.Priority = EmployeeTaskPriority.Low
            employeeTask3.DueDate = Date.Now + New TimeSpan(80, 10, 0, 0, 0)
            employeeTask3.Subject = "Review New Training Material"
            employeeTask3.Description = "Just getting ready to push out some new training material for our customers so they can better understand how our product line fits together.Can I get a review of the content so I can send it out to the printer ? Leah Simpson: Nat, I've reviewed everything and it looks really nice."
            employeeTask3.Completion = 55
            AssignedTasks.Add(employeeTask3)

            Dim employeeTask4 As New EmployeeTask()
            employeeTask4.Priority = EmployeeTaskPriority.High
            employeeTask4.DueDate = Date.Now + New TimeSpan(95, 10, 0, 0, 0)
            employeeTask4.Subject = "Test New Automation App"
            employeeTask4.Description = "We are in a rush to ship this and you need to put all your energy behind finding bugs.If you do find bugs, use standard reporting mechanisms. We'll fix what we can as soon as we can."
            employeeTask4.Completion = 40
            AssignedTasks.Add(employeeTask4)

            Dim employeeTask5 As New EmployeeTask()
            employeeTask5.Priority = EmployeeTaskPriority.Urgent
            employeeTask5.DueDate = Date.Now + New TimeSpan(30, 10, 0, 0, 0)
            employeeTask5.Subject = "Email Test Report on New Products"
            employeeTask5.Description = "Leah, we cannot fix our products until we get the test report from you.Please send everything you have by email to me so I can distribute it in the engineering dept. Leah Simpson: Still collecting these"
            employeeTask5.Completion = 15
            AssignedTasks.Add(employeeTask5)
        End Sub
        Private Sub GenerateEvaluations()
            Dim r As New Random()
            Evaluations = New List(Of Evaluation)()
            For i As Integer = HireDate.Value.Year To Date.Now.Year - 1
                Evaluations.Add(New Evaluation() With {.CreatedOn = New Date(i, r.Next(1, 12), r.Next(1, 25)), .Manager = "Bart Simpson", .Subject = String.Format("{0} Employee Review", i)})
            Next i
        End Sub
        Default Public ReadOnly Property Item(ByVal columnName As String) As String Implements IDataErrorInfo.Item
            Get
                If columnName = "FirstName" AndAlso String.IsNullOrEmpty(FirstName) Then
                    Return "The First Name field is required."
                End If
                If columnName = "LastName" AndAlso String.IsNullOrEmpty(LastName) Then
                    Return "The Last Name field is required."
                End If
                If columnName = "Title" AndAlso String.IsNullOrEmpty(Title) Then
                    Return "The Title field is required."
                End If
                If columnName = "MobilePhone" AndAlso String.IsNullOrEmpty(MobilePhone) Then
                    Return "The Mobile Phone field is required."
                End If
                If columnName = "Email" AndAlso String.IsNullOrEmpty(Email) Then
                    Return "The Email field is required."
                End If
                Return Nothing
            End Get
        End Property
        Public ReadOnly Property [Error]() As String Implements IDataErrorInfo.Error
            Get

                Dim error_Renamed As String = Me("FirstName") & Me("LastName") & Me("Title") & Me("MobilePhone") & Me("Email")
                If Not String.IsNullOrEmpty(error_Renamed) Then
                    Return "Please check inputted data."
                End If
                Return Nothing
            End Get
        End Property
    End Class
    Public Class OverviewDemoViewModel
        Inherits ViewModelBase

        Public Property Employee() As EmployeeViewModel
    End Class

    Public Class EnumToItemSourceProvider
        Inherits MarkupExtension

        Public Property EnumType() As Type
        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Dim result As New List(Of Object)()
            Dim values As Array = System.Enum.GetValues(EnumType)
            For Each value In values
                result.Add(New With {Key .Name = System.Enum.GetName(EnumType, value), Key .Value = value})
            Next value

            Return result
        End Function
    End Class
End Namespace
