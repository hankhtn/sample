Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports System
Imports System.Collections.ObjectModel
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media
Imports DevExpress.Mvvm

Namespace BarsDemo
    <POCOViewModel>
    Public Class BarButtonInfo
        Private action As Action
        Public Overridable Property Caption() As String
        Public Overridable Property LargeGlyph() As ImageSource
        Public Overridable Property SmallGlyph() As ImageSource
        <Command>
        Public Sub ExecuteAction()
            If action IsNot Nothing Then
                action()
            End If
        End Sub
        Public Sub New(ByVal action As Action)
            Caption = ""
            Me.action = action
        End Sub
    End Class

    Public Enum MyParentCommandType
        CommandCreation
        BarCreation
    End Enum

    Public Class ParentBarButtonInfo
        Inherits BarButtonInfo

        Public Sub New(ByVal viewModel As MVVMBarViewModel, ByVal type As MyParentCommandType)
            MyBase.New(Sub() Execute(type, viewModel))
        End Sub

        Private Shared Sub Execute(ByVal type As MyParentCommandType, ByVal viewModel As MVVMBarViewModel)
            Select Case type
                Case MyParentCommandType.CommandCreation
                    viewModel.Bars(0).Commands.Add(CreateNewBarButtonInfo(viewModel))
                Case MyParentCommandType.BarCreation
                    Dim model As BarViewModel = ViewModelSource.Create(Function() New BarViewModel() With {.Name = "New Bar"})
                    model.Commands.Add(CreateNewBarButtonInfo(viewModel))
                    viewModel.Bars.Add(model)
            End Select
        End Sub
        Private Shared Function CreateNewBarButtonInfo(ByVal viewModel As MVVMBarViewModel) As BarButtonInfo

            Dim caption_Renamed As String = "New Command"
            Dim action As Action = Sub() viewModel.MessageBoxService.Show(String.Format("Command ""{0}"" executed", caption_Renamed))
            Return ViewModelSource.Create(Function() New BarButtonInfo(action) With {.Caption = caption_Renamed, .LargeGlyph = DXImageHelper.GetDXImage("Wizard_32x32.png"), .SmallGlyph = DXImageHelper.GetDXImage("Wizard_16x16.png")})
        End Function
    End Class
    Public Class GroupBarButtonInfo
        Inherits BarButtonInfo

        Public Sub New()
            MyBase.New(Nothing)
            Commands = New ObservableCollection(Of BarButtonInfo)()
        End Sub
        Public Overridable Property Commands() As ObservableCollection(Of BarButtonInfo)
    End Class
End Namespace
