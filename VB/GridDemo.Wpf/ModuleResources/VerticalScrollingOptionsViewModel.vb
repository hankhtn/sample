Imports DevExpress.Mvvm

Namespace GridDemo
    Public Class VerticalScrollingOptionsViewModel
        Inherits BindableBase


        Private selectedDefinition_Renamed As GridControlDefinition
        Public Property SelectedDefinition() As GridControlDefinition
            Get
                Return selectedDefinition_Renamed
            End Get
            Set(ByVal value As GridControlDefinition)
                SetProperty(selectedDefinition_Renamed, value, Function() SelectedDefinition)
            End Set
        End Property

        Private controlDefinitions_Renamed As GridControlDefinitionCollection
        Public Property ControlDefinitions() As GridControlDefinitionCollection
            Get
                Return controlDefinitions_Renamed
            End Get
            Set(ByVal value As GridControlDefinitionCollection)
                controlDefinitions_Renamed = value
                If ControlDefinitions IsNot Nothing AndAlso ControlDefinitions.Count > 0 Then
                    SelectedDefinition = ControlDefinitions(0)
                End If
            End Set
        End Property
    End Class
End Namespace
