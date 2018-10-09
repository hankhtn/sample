Imports System.Windows
Imports System.Windows.Input

Namespace ProductsDemo
    Public Class PlaceInfoControl
        Inherits VisibleControl


        Private changePlaceCommand_Renamed As ChangePlaceCommand
        Public Shared ReadOnly PlaceInfoProperty As DependencyProperty = DependencyProperty.Register("PlaceInfo", GetType(PlaceInfo), GetType(PlaceInfoControl),New PropertyMetadata(Nothing))

        Public Property PlaceInfo() As PlaceInfo
            Get
                Return DirectCast(GetValue(PlaceInfoProperty), PlaceInfo)
            End Get
            Set(ByVal value As PlaceInfo)
                SetValue(PlaceInfoProperty, value)
            End Set
        End Property
        Public ReadOnly Property ChangePlaceCommand() As ChangePlaceCommand
            Get
                Return changePlaceCommand_Renamed
            End Get
        End Property

        Public Event ShowNextPlace As RoutedEventHandler
        Public Event ShowPrevPlace As RoutedEventHandler

        Public Sub New()
            changePlaceCommand_Renamed = New ChangePlaceCommand(Me)
            DefaultStyleKey = GetType(PlaceInfoControl)
        End Sub
        Public Sub OnShowNextPlace()
            RaiseEvent ShowNextPlace(Me, New RoutedEventArgs())
        End Sub
        Public Sub OnShowPreviousPlace()
            RaiseEvent ShowPrevPlace(Me, New RoutedEventArgs())
        End Sub
    End Class

    Public Class ChangePlaceCommand
        Implements ICommand


        Private ReadOnly placeInfoControl_Renamed As PlaceInfoControl


        Public Sub New(ByVal placeInfoControl_Renamed As PlaceInfoControl)
            Me.placeInfoControl_Renamed = placeInfoControl_Renamed
        End Sub

        Public Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
            If CanExecuteChangedEvent IsNot Nothing Then
                Return placeInfoControl_Renamed IsNot Nothing
            End If
            Return False
        End Function
        Public Event CanExecuteChanged As System.EventHandler Implements ICommand.CanExecuteChanged

        Public Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
            If Not(TypeOf parameter Is NavDirection) Then
                Return
            End If
            Dim navDirection As NavDirection = DirectCast(parameter, NavDirection)
            Select Case navDirection
                Case ProductsDemo.NavDirection.Next
                    placeInfoControl_Renamed.OnShowNextPlace()
                Case ProductsDemo.NavDirection.Previous
                    placeInfoControl_Renamed.OnShowPreviousPlace()
            End Select
        End Sub
    End Class

    Public Enum NavDirection
        [Next]
        Previous
    End Enum
End Namespace
