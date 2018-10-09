Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Linq
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Media
Imports DevExpress.DevAV.Common
Imports DevExpress.DevAV.Common.Utils
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.Localization
Imports DevExpress.Mvvm.DataModel
Imports System.Windows.Threading

Namespace DevExpress.DevAV.ViewModels
    Partial Public Class EmployeeTaskViewModel
        Private StartTimeOfWorkDay As New Date(1, 1, 1, 9, 0, 0)
        Private EndTimeOfWorkDay As New Date(1, 1, 1, 19, 0, 0)

        Private ReadOnly Property PrintService() As IReportService
            Get
                Return Me.GetRequiredService(Of IReportService)("PrintService")
            End Get
        End Property
        Private ReadOnly Property ExportService() As IReportService
            Get
                Return Me.GetRequiredService(Of IReportService)("ExportService")
            End Get
        End Property
        Protected Overridable ReadOnly Property OpenFileDialogService() As IOpenFileDialogService
            Get
                Return Nothing
            End Get
        End Property
        Public Overridable Property FilesInfo() As ObservableCollection(Of AttachedFileInfo)
        Public Overridable Property EntityContextLookUpEmployees() As IEnumerable(Of Employee)
        Public Overridable Property AssignedEmployees() As IEnumerable(Of Employee)
        Public Overridable Property RtfTextDescription() As String
        Public Overridable Property TextDescription() As String

        Public Property SelectedFile() As AttachedFileInfo
        Private allowEntityChange As Boolean
        Private disableAttachedFilesReload As Boolean
        Private allowParameterChanged As Boolean

        Private linksViewModel_Renamed As LinksViewModel
        Private createdFilePaths As List(Of String)
        Public actualRtfTextDescription As String

        Protected Overrides Function CreateEntity() As EmployeeTask
            Dim entity As EmployeeTask = MyBase.CreateEntity()
            entity.StartDate = Date.Now
            entity.DueDate = Date.Now + New TimeSpan(48, 0, 0)
            entity.Category = Colors.Transparent.ToString()
            Dim id = EmployeeTaskDetailsCollectionViewModel.LastSelectedEmployeeId
            If id IsNot Nothing Then
                Dim employee = Me.UnitOfWork.Employees.SingleOrDefault(Function(x) x.Id = id.Value)
                If employee IsNot Nothing Then
                    entity.AssignedEmployees.Add(employee)
                End If
                EmployeeTaskDetailsCollectionViewModel.LastSelectedEmployeeId = Nothing
            End If
            Return entity
        End Function

        Protected Overrides Sub OnEntityChanged()
            MyBase.OnEntityChanged()
            allowEntityChange = False
            If Not disableAttachedFilesReload Then
                FilesInfo = New ObservableCollection(Of AttachedFileInfo)()
                If Entity IsNot Nothing AndAlso Entity.AttachedFiles IsNot Nothing Then
                    For Each file In Entity.AttachedFiles
                        FilesInfo.Add(FilesHelper.GetAttachedFileInfo(file.Name, "", file.Id))
                    Next file
                End If
            End If
            Me.RaisePropertyChanged(Function(vm) vm.ReminderTime)
            Me.RaisePropertyChanged(Function(vm) vm.Status)
            Me.RaisePropertyChanged(Function(vm) vm.Completion)
            EntityContextLookUpEmployees = Me.UnitOfWork.Employees.AsEnumerable()
            If Entity IsNot Nothing Then
                allowParameterChanged = False
                AssignedEmployees = Entity.AssignedEmployees
                RtfTextDescription = Entity.RtfTextDescription
                Dim rtfAction As Action = Sub()
                    actualRtfTextDescription = RtfTextDescription
                    allowParameterChanged = True
                End Sub
                Dispatcher.CurrentDispatcher.BeginInvoke(rtfAction)
                Xpf.DemoBase.Helpers.Logger.Log(String.Format("OutlookInspiredApp: Edit Task: {0}",If(String.IsNullOrEmpty(Entity.Subject), "<New>", Entity.Subject)))
            End If
            allowEntityChange = True
        End Sub
        Public Overridable Sub OnAssignedEmployeesChanged()
            If (Not allowParameterChanged) OrElse AssignedEmployeesAreEqual() Then
                Return
            End If
            If allowEntityChange AndAlso Entity IsNot Nothing Then
                Entity.AssignedEmployees = AssignedEmployees.ToList()
                SetCollectionChange()
            End If
        End Sub
        Public Overridable Sub OnRtfTextDescriptionChanged()
            If (Not allowParameterChanged) OrElse Entity Is Nothing OrElse (Not allowEntityChange) Then
                Return
            End If
            If actualRtfTextDescription <> RtfTextDescription Then
                Entity.RtfTextDescription = RtfTextDescription
                actualRtfTextDescription = RtfTextDescription
                Update()
            End If
        End Sub
        Public Overridable Sub OnTextDescriptionChanged()
            If Entity IsNot Nothing AndAlso TextDescription <> Entity.Description AndAlso allowParameterChanged Then
                Entity.Description = TextDescription
            End If
        End Sub
        Public Overridable Sub Unloaded()
            FilesHelper.DeleteTempFiles(createdFilePaths)
        End Sub
        Public Property ReminderTime() As Date?
            Get
                If Me.Entity Is Nothing OrElse Me.Entity.ReminderDateTime Is Nothing Then
                    Return Nothing
                End If
                Return Me.Entity.ReminderDateTime.Value
            End Get
            Set(ByVal value? As Date)
                If Me.Entity Is Nothing OrElse value Is Nothing OrElse Me.Entity.ReminderDateTime Is Nothing Then
                    Return
                End If
                Dim reminderDateTime As Date = CDate(Me.Entity.ReminderDateTime)
                Me.Entity.ReminderDateTime = New Date(reminderDateTime.Year, reminderDateTime.Month, reminderDateTime.Day, CDate(value).Hour, CDate(value).Minute, CDate(value).Second)
            End Set
        End Property

        Public Property Status() As EmployeeTaskStatus
            Get
                Return If(Me.Entity Is Nothing, EmployeeTaskStatus.NotStarted, Me.Entity.Status)
            End Get
            Set(ByVal value As EmployeeTaskStatus)
                If Me.Entity Is Nothing Then
                    Return
                End If
                Dim oldStatus As String = Me.Entity.Status.ToString()
                Me.Entity.Status = value
                If oldStatus = (EmployeeTaskStatus.Completed).ToString() AndAlso Me.Entity.Status <> EmployeeTaskStatus.Completed Then
                    Me.Entity.Completion = 50
                End If
                If Me.Entity.Status = EmployeeTaskStatus.Completed AndAlso Me.Entity.Completion <> 100 Then
                    Me.Entity.Completion = 100
                End If
                Me.RaisePropertyChanged(Function(vm) vm.Entity)
                Me.RaisePropertyChanged(Function(vm) vm.Entity.Status)
                Me.RaisePropertyChanged(Function(vm) vm.Entity.Completion)
            End Set
        End Property
        Public Property Completion() As Integer
            Get
                Return If(Me.Entity IsNot Nothing, Me.Entity.Completion, 0)
            End Get
            Set(ByVal value As Integer)
                If Me.Entity Is Nothing Then
                    Return
                End If
                Me.Entity.Completion = value
                If Me.Entity.Completion = 100 Then
                    Me.Entity.Status = EmployeeTaskStatus.Completed
                End If
                If Me.Entity.Completion <> 100 AndAlso Me.Entity.Status = EmployeeTaskStatus.Completed Then
                    Me.Entity.Status = EmployeeTaskStatus.InProgress
                End If
                Me.RaisePropertyChanged(Function(vm) vm.Entity.Status)
                Me.RaisePropertyChanged(Function(vm) vm.Entity)
            End Set
        End Property
        Public Overridable Sub FollowUp(ByVal name As String)
            Select Case name
                Case "Today"
                    SetFollowUpDateTime(Date.Now, Date.Now, True, Date.Now, EndTimeOfWorkDay.AddHours(-1))
                Case "Tomorrow"
                    SetFollowUpDateTime(Date.Now.AddDays(1), Date.Now.AddDays(1), True, NextWorkDay(Date.Now), StartTimeOfWorkDay)
                Case "ThisWeek"
                    SetFollowUpDateTime(GetThisWeekStartDate(), GetBorderWorkDay(False), True, GetThisWeekStartDate(), StartTimeOfWorkDay)
                Case "NextWeek"
                    SetFollowUpDateTime(GetBorderWorkDay(True, True), GetBorderWorkDay(False, True), True, GetBorderWorkDay(True, True), StartTimeOfWorkDay)
                Case "NoDate"
                    SetFollowUpDateTime(Nothing, Nothing, False, Nothing, Nothing)
                Case "Custom"
                    SetFollowUpDateTime(Date.Now, Date.Now, True, Date.Now, Date.Now)
            End Select
            Me.RaisePropertyChanged(Function(vm) vm.Entity)
        End Sub
        Public Overridable Sub MarkComplete()
            Status = EmployeeTaskStatus.Completed
            Me.RaisePropertyChanged(Function(vm) vm.Status)
            Me.RaisePropertyChanged(Function(vm) vm.Completion)
        End Sub
        Public Overridable Sub AttachFiles(ByVal e As DragEventArgs)
            Dim filesName() As String = { }
            If e IsNot Nothing AndAlso e.Data.GetDataPresent(DataFormats.FileDrop) Then
                filesName = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
            Else
                Dim dialogResult As Boolean = OpenFileDialogService.ShowDialog()
                If dialogResult Then
                    filesName = OpenFileDialogService.Files.Select(Function(x) Path.Combine(x.DirectoryName, x.Name)).ToArray()
                End If
            End If
            AttachedFilesCore(filesName)
        End Sub
        Public Overridable Sub PrintTaskDetail()
            ShowReport(ReportInfoFactory.TaskDetailReport(Entity), "TaskDetail")
        End Sub
        Private Sub AttachedFilesCore(ByVal names() As String)
            Dim fileLengthExceed As Boolean = False
            Dim info As FileInfo
            For Each name As String In names
                info = New FileInfo(name)
                If info.Length > FilesHelper.MaxAttachedFileSize * 1050578 Then
                    fileLengthExceed = True
                Else
                    FilesInfo.Add(FilesHelper.GetAttachedFileInfo(info.Name, info.DirectoryName))
                End If
            Next name
            If fileLengthExceed Then
                MessageBoxService.ShowMessage(String.Format("The size of one of the files exceeds {0} MB.", FilesHelper.MaxAttachedFileSize), "Error attaching files")
            End If
            SetCollectionChange()
            Update()
        End Sub

        Public Overridable Sub OpenFile()
            If SelectedFile.Id = -1 Then
                FilesHelper.OpenFileFromDisc(SelectedFile.FullPath)
                Return
            End If
            If createdFilePaths Is Nothing Then
                createdFilePaths = New List(Of String)()
            End If
            createdFilePaths.Add(FilesHelper.OpenFileFromDb(SelectedFile.Name, Entity.AttachedFiles.Single(Function(x) x.Id = SelectedFile.Id).Content))
        End Sub
        Public Overridable Sub DeleteFile()
            FilesInfo.Remove(SelectedFile)
            SetCollectionChange()
            Update()
        End Sub
        Public Overrides Sub Delete()
            If MessageBoxService.ShowMessage(String.Format(ScaffoldingLocalizer.GetString(ScaffoldingStringId.Confirmation_Delete), EntityDisplayName), GetConfirmationMessageTitle(), MessageButton.YesNo) <> MessageResult.Yes Then
                Return
            End If
            Try
                If Entity.AttachedFiles IsNot Nothing Then
                    Dim deletedFileKeys() As Long = Entity.AttachedFiles.Select(Function(x) x.Id).ToArray()
                    For Each key In deletedFileKeys
                        Dim file = UnitOfWork.AttachedFiles.Find(key)
                        UnitOfWork.AttachedFiles.Remove(file)
                    Next key
                End If
                OnBeforeEntityDeleted(PrimaryKey, Entity)
                Repository.Remove(Entity)
                UnitOfWork.SaveChanges()
                Dim primaryKeyForMessage As Long = PrimaryKey
                Dim entityForMessage As EmployeeTask = Entity
                Entity = Nothing
                OnEntityDeleted(primaryKeyForMessage, entityForMessage)
                Close()
            Catch e As DbException
                MessageBoxService.ShowMessage(e.ErrorMessage, e.ErrorCaption, MessageButton.OK, MessageIcon.Error)
            End Try
        End Sub
        Private Sub SetCollectionChange()
            Entity.AttachedCollectionsChanged = Not Entity.AttachedCollectionsChanged
        End Sub
        Protected Overrides Function SaveCore() As Boolean
            disableAttachedFilesReload = True
            Dim saveCoreResult As Boolean = MyBase.SaveCore()
            If Not saveCoreResult Then
                Return False
            End If
            Dim hasFilesOperations As Boolean = False
            disableAttachedFilesReload = False
            If Entity.AttachedFiles IsNot Nothing Then
                Dim deletedItems As New List(Of Long)()
                For Each file In Entity.AttachedFiles
                    Dim id As Long = file.Id
                    If FilesInfo.FirstOrDefault(Function(x) id = x.Id) Is Nothing Then
                        deletedItems.Add(file.Id)
                    End If
                Next file
                For Each item As Long In deletedItems
                    Dim file = UnitOfWork.AttachedFiles.Find(item)
                    UnitOfWork.AttachedFiles.Remove(file)
                    hasFilesOperations = True
                Next item
            End If
            For Each fileInfo In FilesInfo
                If fileInfo.Id = -1 Then
                    Try
                        Using stream As New FileStream(fileInfo.FullPath, FileMode.Open, FileAccess.Read)
                            Dim attachedFile As TaskAttachedFile = UnitOfWork.AttachedFiles.Create()
                            attachedFile.EmployeeTaskId = Entity.Id
                            attachedFile.Name = fileInfo.Name
                            attachedFile.Content = New Byte(CInt(stream.Length) - 1){}
                            stream.Read(attachedFile.Content, 0, CInt(stream.Length))
                            hasFilesOperations = True
                        End Using
                    Catch e1 As Exception
                        MessageBoxService.ShowMessage("Error attaching file!", "Error")
                        Return False
                    End Try
                End If
            Next fileInfo
            If hasFilesOperations Then
                UnitOfWork.SaveChanges()
            End If
            Return True
        End Function
        Private Function AssignedEmployeesAreEqual() As Boolean
            If Entity Is Nothing OrElse Entity.AssignedEmployees Is Nothing OrElse AssignedEmployees Is Nothing Then
                Return True
            End If
            Dim hasItems1 = (Entity.AssignedEmployees).Except(AssignedEmployees).Any()
            Dim hasItems2 = AssignedEmployees.Except(Entity.AssignedEmployees).Any()
            Return Not(hasItems1 OrElse hasItems2)
        End Function
        Public Overridable Function CanMarkComplete() As Boolean
            Return Me.Entity IsNot Nothing AndAlso Me.Entity.Status <> EmployeeTaskStatus.Completed
        End Function
        Public Overridable Function CanOpenFile() As Boolean
            Return SelectedFile IsNot Nothing
        End Function
        Public Overridable Function CanDeleteFile() As Boolean
            Return SelectedFile IsNot Nothing
        End Function
        Protected Overrides Function GetTitle() As String
            Return If(Entity.Subject, String.Empty)
        End Function
        Protected Overrides Function GetConfirmationMessageTitle() As String
            Dim baseTitle = MyBase.GetConfirmationMessageTitle()
            If String.IsNullOrEmpty(baseTitle) Then
                Return baseTitle
            End If
            Return If(baseTitle.Length <= 30, baseTitle, baseTitle.Substring(0, 30) & "...")
        End Function
        Public ReadOnly Property LinksViewModel() As LinksViewModel
            Get
                If linksViewModel_Renamed Is Nothing Then
                    linksViewModel_Renamed = DevExpress.DevAV.ViewModels.LinksViewModel.Create()
                End If
                Return linksViewModel_Renamed
            End Get
        End Property
        Private Sub SetFollowUpDateTime(ByVal startDate? As Date, ByVal dueDate? As Date, ByVal reminder As Boolean, ByVal reminderDateTime? As Date, ByVal reminderTime? As Date)
            Me.Entity.StartDate = startDate
            Me.Entity.DueDate = dueDate
            Me.Entity.Reminder = reminder
            Me.Entity.ReminderDateTime = reminderDateTime
            Me.ReminderTime = reminderTime
            Me.RaisePropertyChanged(Function(vm) vm.ReminderTime)
        End Sub
        Private Function NextWorkDay(ByVal [date] As Date) As Date
            Do
                [date] = [date].AddDays(1)
            Loop While IsHoliday([date])
            Return [date]
        End Function
        Private Function IsHoliday(ByVal dateTime As Date) As Boolean
            Return dateTime.DayOfWeek = DayOfWeek.Saturday OrElse dateTime.DayOfWeek = DayOfWeek.Sunday
        End Function
        Private Function GetBorderWorkDay(Optional ByVal firstWorkDay As Boolean = True, Optional ByVal nextWeek As Boolean = False) As Date
            Dim dateTime As Date = Date.Now
            Do While IsHoliday(dateTime)
                dateTime = dateTime.AddDays(1)
            Loop
            If nextWeek Then
                dateTime = dateTime.AddDays(7)
            End If
            Do While Not IsHoliday(dateTime)
                dateTime = dateTime.AddDays(If(firstWorkDay, -1, 1))
            Loop
            dateTime = dateTime.AddDays(If(firstWorkDay, 1, -1))
            Return dateTime
        End Function
        Private Function GetThisWeekStartDate() As Date
            Dim dateTime As Date = Date.Now
            Dim realCounter As Integer = 0
            Do While IsHoliday(dateTime)
                dateTime = dateTime.AddDays(1)
            Loop
            Do
                dateTime = dateTime.AddDays(1)
                If Not IsHoliday(dateTime) Then
                    realCounter += 1
                End If
            Loop While (Not IsHoliday(dateTime)) AndAlso realCounter < 2
            If realCounter < 2 Then
                dateTime = dateTime.AddDays(-1)
            End If
            Return dateTime
        End Function
        Private Sub ShowReport(ByVal reportInfo As IReportInfo, ByVal reportId As String)
            ExportService.ShowReport(reportInfo)
            PrintService.ShowReport(reportInfo)
            Xpf.DemoBase.Helpers.Logger.Log(String.Format("OutlookInspiredApp: Create Report : Task: {0}", reportId))
        End Sub
    End Class
End Namespace
