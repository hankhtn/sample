Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports System
Imports System.Collections.ObjectModel
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media

Namespace RibbonDemo
    <POCOViewModel>
    Public Class PageGroupModel
        Implements ICommand

        Public Sub New()
            Commands = New ObservableCollection(Of CommandModel)()
        End Sub
        Public Overridable Property Name() As String
        Public Property Commands() As ObservableCollection(Of CommandModel)
        Public Overridable Property Glyph() As ImageSource
        Protected Friend Sub OnGlyphChanged()
            Glyph.Freeze()
        End Sub
        Public Sub Clear()
            Commands.Clear()
        End Sub
        #Region "ICommand"
        Private b As Boolean = False
        Private Event _canExecuteChanged As EventHandler
        Private Function ICommand_CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
            If b = True Then
                _canExecuteChangedEvent.Invoke(Me, New EventArgs())
            End If
            Return True
        End Function
        Private Custom Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
            AddHandler(ByVal value As EventHandler)
                AddHandler _canExecuteChanged, value
            End AddHandler
            RemoveHandler(ByVal value As EventHandler)
                RemoveHandler _canExecuteChanged, value
            End RemoveHandler
            RaiseEvent(ByVal sender As System.Object, ByVal e As System.EventArgs)
                RaiseEvent _canExecuteChanged(sender, e)
            End RaiseEvent
        End Event
        Private Sub ICommand_Execute(ByVal parameter As Object) Implements ICommand.Execute
            MessageBox.Show(Name & "'s command executed")
        End Sub
        #End Region
    End Class
    <POCOViewModel>
    Public Class CommandModel
        Implements ICommand

        Private action As Action

        Public Sub New()
            Me.New(Nothing)
        End Sub
        Public Sub New(ByVal action As Action)
            Me.action = If(action IsNot Nothing, action, New Action(AddressOf ShowMessageBox))
            Caption = ""
        End Sub
        Public Overridable Property Caption() As String
        Public Overridable Property LargeGlyph() As ImageSource
        Public Overridable Property SmallGlyph() As ImageSource

        Public Sub ShowMessageBox()
            MessageBox.Show(String.Format("Command ""{0}"" executed", Me.Caption))
        End Sub
        Protected Friend Sub OnSmallGlyphChanged()
            OnGlyphChanged(SmallGlyph)
        End Sub
        Protected Friend Sub OnLargeGlyphChanged()
            OnGlyphChanged(LargeGlyph)
        End Sub
        Protected Friend Sub OnGlyphChanged(ByVal e As ImageSource)
            e.Freeze()
        End Sub
        #Region "ICommand"
        Private Function ICommand_CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
            Return Me.CanExecute(parameter)
        End Function
        Private Custom Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
            AddHandler(ByVal value As EventHandler)
                AddHandler _canExecuteChanged, value
            End AddHandler
            RemoveHandler(ByVal value As EventHandler)
                RemoveHandler _canExecuteChanged, value
            End RemoveHandler
            RaiseEvent(ByVal sender As System.Object, ByVal e As System.EventArgs)
                RaiseEvent _canExecuteChanged(sender, e)
            End RaiseEvent
        End Event
        Private Sub ICommand_Execute(ByVal parameter As Object) Implements ICommand.Execute
            Me.Execute(parameter)
        End Sub
        #End Region
        Private b As Boolean = False
        Public Overridable Function CanExecute(ByVal parameter As Object) As Boolean
            If b = True Then
                _canExecuteChangedEvent.Invoke(Me, New EventArgs())
            End If
            Return True
        End Function
        Private Event _canExecuteChanged As EventHandler
        Public Overridable Sub Execute(ByVal parameter As Object)
            action()
        End Sub
    End Class

    Public Enum MyParentCommandType
        CommandCreation
        GroupCreation
        PageCreation
    End Enum
    <POCOViewModel>
    Public Class MyParentCommand
        Inherits CommandModel

        Private viewModel As MVVMRibbonViewModel
        Private type As MyParentCommandType
        Public Sub New(ByVal viewModel As MVVMRibbonViewModel, ByVal type As MyParentCommandType)
            Me.viewModel = viewModel
            Me.type = type
        End Sub
        Public Overrides Sub Execute(ByVal parameter As Object)
            Select Case type
                Case MyParentCommandType.CommandCreation
                    CommandCreation()
                Case MyParentCommandType.GroupCreation
                    PageGroupCreation()
                Case MyParentCommandType.PageCreation
                    PageCreation()
            End Select
            viewModel.RibbonMergeingService.Remerge()
        End Sub
        Private Sub PageCreation()
            viewModel.Categories(0).Pages.Add(ViewModelSource.Create(Function() New PageModel() With {.Name = "New Page" & IndexCreator.GetIndex()}))
        End Sub
        Private Sub PageGroupCreation()
            viewModel.Categories(0).Pages(0).Groups.Add(ViewModelSource.Create(Function() New PageGroupModel() With {.Name = "New Group", .Glyph = GlyphHelper.GetGlyph("/Images/Icons/NewViaWizard_32x32.png")}))
        End Sub
        Private Sub CommandCreation()
            Dim newCommand As CommandModel = ViewModelSource.Create(Function() New CommandModel() With {.Caption = "New Command", .LargeGlyph = GlyphHelper.GetGlyph("/Images/Icons/NewViaWizard_32x32.png"), .SmallGlyph = GlyphHelper.GetGlyph("/Images/Icons/NewViaWizard_16x16.png")})
            viewModel.Categories(0).Pages(0).Groups(0).Commands.Add(newCommand)
        End Sub
    End Class

    Public Class MyGroupCommand
        Inherits CommandModel

        Public Property Commands() As ObservableCollection(Of CommandModel)
        Public Sub New()
            MyBase.New(AddressOf emptyFunc)
            Commands = New ObservableCollection(Of CommandModel)()
        End Sub
        Public Shared Sub emptyFunc()
        End Sub
    End Class

    Public NotInheritable Class IndexCreator

        Private Sub New()
        End Sub

        Private Shared Value As Integer = 0
        Public Shared Function GetIndex() As String
            Value += 1
            Return Value.ToString()
        End Function
        Public Shared Sub Refresh()
            Value = 0
        End Sub
    End Class
End Namespace
