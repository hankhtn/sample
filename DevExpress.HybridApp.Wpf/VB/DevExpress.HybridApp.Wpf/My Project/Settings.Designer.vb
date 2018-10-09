Namespace My


    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")>
    Friend NotInheritable Partial Class Settings
        Inherits System.Configuration.ApplicationSettingsBase

        Private Shared defaultInstance As Settings = (CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New Settings()), Settings))

        Public Shared ReadOnly Property [Default]() As Settings
            Get
                Return defaultInstance
            End Get
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>" & ControlChars.CrLf & "<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>All</Name>" & ControlChars.CrLf & "    <ImageUri>Resources/Employees/All.png</ImageUri>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Salaried</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,Salaried#</FilterCriteria>" & ControlChars.CrLf & "    <ImageUri>Resources/Employees/Salaried.png</ImageUri>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Commission</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,Commission#</FilterCriteria>" & ControlChars.CrLf & "    <ImageUri>Resources/Employees/Commission.png</ImageUri>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Contract</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,Contract#</FilterCriteria>" & ControlChars.CrLf & "    <ImageUri>Resources/Employees/Probation.png</ImageUri>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Terminated</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,Terminated#</FilterCriteria>" & ControlChars.CrLf & "    <ImageUri>Resources/Employees/Terminated.png</ImageUri>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>On Leave</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,OnLeave#</FilterCriteria>" & ControlChars.CrLf & "    <ImageUri>Resources/Employees/OnLeave.png</ImageUri>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "</ArrayOfFilterInfo>")>
        Public Property EmployeesStaticFilters() As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return (DirectCast(Me("EmployeesStaticFilters"), Global.DevExpress.DevAV.ViewModels.FilterInfoList))
            End Get
            Set(ByVal value As DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("EmployeesStaticFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>" & ControlChars.CrLf & "<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>All Customers</Name>" & ControlChars.CrLf & "    <FilterCriteria />" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>My Account</Name>" & ControlChars.CrLf & "    <FilterCriteria>[HomeOffice.State] = ##Enum#DevExpress.DevAV.StateEnum,CA#</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>John's Account</Name>" & ControlChars.CrLf & "    <FilterCriteria>[HomeOffice.State] = ##Enum#DevExpress.DevAV.StateEnum,WA#</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Top Stores</Name>" & ControlChars.CrLf & "    <FilterCriteria>[AnnualRevenue] &gt;= 90000000000.0m</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "</ArrayOfFilterInfo>")>
        Public Property CustomersCustomFilters() As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return (DirectCast(Me("CustomersCustomFilters"), Global.DevExpress.DevAV.ViewModels.FilterInfoList))
            End Get
            Set(ByVal value As DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("CustomersCustomFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>" & ControlChars.CrLf & "<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>HD Video player</Name>" & ControlChars.CrLf & "    <FilterCriteria>Contains([Name], 'HD') And Category = 'VideoPlayers'</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>50inch Plasma</Name>" & ControlChars.CrLf & "    <FilterCriteria>Contains([Name], '50') And Category = 'Televisions'</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>21inch Monitor</Name>" & ControlChars.CrLf & "    <FilterCriteria>Contains([Name], '21') And Category = 'Monitors'</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Remote Control</Name>" & ControlChars.CrLf & "    <FilterCriteria>Contains([Name], 'Remote') And Category = 'Automation'</FilterCriteria>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "</ArrayOfFilterInfo>")>
        Public Property ProductsCustomFilters() As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return (DirectCast(Me("ProductsCustomFilters"), Global.DevExpress.DevAV.ViewModels.FilterInfoList))
            End Get
            Set(ByVal value As DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("ProductsCustomFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>" & ControlChars.CrLf & "<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>All</Name>" & ControlChars.CrLf & "    <ImageUri>Resources/Products/All.png</ImageUri>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Video Players</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,VideoPlayers#</FilterCriteria>" & ControlChars.CrLf & "    <ImageUri>Resources/Products/VideoPlayers.png</ImageUri>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Automation</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,Automation#</FilterCriteria>" & ControlChars.CrLf & "    <ImageUri>Resources/Products/Automation.png</ImageUri>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Monitors</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,Monitors#</FilterCriteria>" & ControlChars.CrLf & "    <ImageUri>Resources/Products/Monitors.png</ImageUri>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Projectors</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,Projectors#</FilterCriteria>" & ControlChars.CrLf & "    <ImageUri>Resources/Products/Projectors.png</ImageUri>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "  <FilterInfo>" & ControlChars.CrLf & "    <Name>Televisions</Name>" & ControlChars.CrLf & "    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,Televisions#</FilterCriteria>" & ControlChars.CrLf & "    <ImageUri>Resources/Products/TVs.png</ImageUri>" & ControlChars.CrLf & "  </FilterInfo>" & ControlChars.CrLf & "</ArrayOfFilterInfo>")>
        Public Property ProductsStaticFilters() As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return (DirectCast(Me("ProductsStaticFilters"), Global.DevExpress.DevAV.ViewModels.FilterInfoList))
            End Get
            Set(ByVal value As DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("ProductsStaticFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?><ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">  <FilterInfo>    <Name>All Tasks</Name>    <FilterCriteria />    <ImageUri>Resources/Tasks/InProgress.png</ImageUri>  </FilterInfo>  <FilterInfo>    <Name>In Progress</Name>    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeTaskStatus,InProgress#</FilterCriteria>    <ImageUri>Resources/Tasks/InProgress.png</ImageUri>  </FilterInfo>  <FilterInfo>    <Name>Not Started</Name>    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeTaskStatus,NotStarted#</FilterCriteria>    <ImageUri>Resources/Tasks/NotStarted.png</ImageUri>  </FilterInfo>  <FilterInfo>    <Name>Deferred</Name>    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeTaskStatus,Deferred#</FilterCriteria>    <ImageUri>Resources/Tasks/Deferred.png</ImageUri>  </FilterInfo>  <FilterInfo>    <Name>Completed</Name>    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeTaskStatus,Completed#</FilterCriteria>    <ImageUri>Resources/Tasks/Completed.png</ImageUri>  </FilterInfo>  <FilterInfo>    <Name>High Priority</Name>    <FilterCriteria>[Priority] = ##Enum#DevExpress.DevAV.EmployeeTaskPriority,High#</FilterCriteria>    <ImageUri>Resources/Tasks/HighPriority.png</ImageUri>  </FilterInfo>  <FilterInfo>    <Name>Urgent</Name>    <FilterCriteria>[Priority] = ##Enum#DevExpress.DevAV.EmployeeTaskPriority,Urgent#</FilterCriteria>    <ImageUri>Resources/Tasks/Urgent.png</ImageUri>  </FilterInfo></ArrayOfFilterInfo>")>
        Public Property TasksStaticFilters() As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return (DirectCast(Me("TasksStaticFilters"), Global.DevExpress.DevAV.ViewModels.FilterInfoList))
            End Get
            Set(ByVal value As DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("TasksStaticFilters") = value
            End Set
        End Property
    End Class
End Namespace
