Imports DevExpress.Data.Filtering
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Windows.Data

Namespace GridDemo
    Public Class DoubleToCriteriaOperatorConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim op As BinaryOperator = TryCast(value, BinaryOperator)
            If Object.ReferenceEquals(op, Nothing) Then
                Return 0R
            End If
            Dim operandValue As OperandValue = TryCast(op.RightOperand, OperandValue)
            Return operandValue.Value
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return New BinaryOperator("Quantity", Math.Round(DirectCast(value, Double)), BinaryOperatorType.Greater)
        End Function
        #End Region
    End Class

    Public Class FilteringViewModel
        Inherits ViewModelBase

        Private _CurrentView As String = "Table View"
        Private _ColumnFilterPopupMode As ColumnFilterPopupMode = ColumnFilterPopupMode.Excel
        Private _IncrementalSearchClearDelay As Integer = 1000
        Private _IncrementalSearchMode As IncrementalSearchMode = IncrementalSearchMode.Disabled
        Private _AllowFilterEditor As Boolean = True
        Private _AllowColumnFiltering As Boolean = True

        Public Property CurrentView() As String
            Get
                Return _CurrentView
            End Get
            Set(ByVal value As String)
                SetProperty(_CurrentView, value, "CurrentView")
            End Set
        End Property
        Public Property ColumnFilterPopupMode() As ColumnFilterPopupMode
            Get
                Return _ColumnFilterPopupMode
            End Get
            Set(ByVal value As ColumnFilterPopupMode)
                SetProperty(_ColumnFilterPopupMode, value, "ColumnFilterPopupMode")
            End Set
        End Property
        Public Property IncrementalSearchClearDelay() As Integer
            Get
                Return _IncrementalSearchClearDelay
            End Get
            Set(ByVal value As Integer)
                SetProperty(_IncrementalSearchClearDelay, value, "IncrementalSearchClearDelay")
            End Set
        End Property
        Public Property IncrementalSearchMode() As IncrementalSearchMode
            Get
                Return _IncrementalSearchMode
            End Get
            Set(ByVal value As IncrementalSearchMode)
                SetProperty(_IncrementalSearchMode, value, "IncrementalSearchMode")
            End Set
        End Property
        Public Property AllowFilterEditor() As Boolean
            Get
                Return _AllowFilterEditor
            End Get
            Set(ByVal value As Boolean)
                SetProperty(_AllowFilterEditor, value, "AllowFilterEditor")
            End Set
        End Property
        Public Property AllowColumnFiltering() As Boolean
            Get
                Return _AllowColumnFiltering
            End Get
            Set(ByVal value As Boolean)
                SetProperty(_AllowColumnFiltering, value, "AllowColumnFiltering")
            End Set
        End Property

    End Class
End Namespace
