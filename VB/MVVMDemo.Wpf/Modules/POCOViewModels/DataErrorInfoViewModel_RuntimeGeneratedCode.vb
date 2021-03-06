Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports System.ComponentModel

' This class demonstrates the code that will be generated by the POCO mechanism at runtime

Namespace MVVMDemo.POCOViewModels
    <POCOViewModel(ImplementIDataErrorInfo := True)>
    Public Class DataErrorInfoViewModel_RuntimeGeneratedCode
        Inherits DataErrorInfoViewModel
        Implements IDataErrorInfo, INotifyPropertyChanged

        Private ReadOnly Property IDataErrorInfo_Error() As String Implements IDataErrorInfo.Error
            Get
                Return String.Empty
            End Get
        End Property
        Public ReadOnly Property IDataErrorInfo_Item(ByVal columnName As String) As String Implements IDataErrorInfo.Item
            Get
                Return IDataErrorInfoHelper.GetErrorText(Me, columnName)
            End Get
        End Property

        #Region "properties and commands"
        Public Overrides Property UserName() As String
            Get
                Return MyBase.UserName
            End Get
            Set(ByVal value As String)
                If MyBase.UserName = value Then
                    Return
                End If
                MyBase.UserName = value
                RaisePropertyChanged("UserName")
            End Set
        End Property

        Private _RegisterCommand As DelegateCommand
        Public ReadOnly Property RegisterCommand() As DelegateCommand
            Get
                If _RegisterCommand IsNot Nothing Then
                    Return _RegisterCommand
                Else
                    _RegisterCommand = New DelegateCommand(AddressOf Register, AddressOf CanRegister)
                    Return _RegisterCommand
                End If
            End Get
        End Property
        #End Region

        #Region "INotifyPropertyChanged implementation"
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim handler = PropertyChangedEvent
            If handler IsNot Nothing Then
                handler(Me, New PropertyChangedEventArgs(propertyName))
            End If
        End Sub
        #End Region
    End Class
End Namespace
