Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace GridDemo
    Public NotInheritable Class VirtualDataSourceValuesProvider

        Private Sub New()
        End Sub

        Public Const UniqueTypesCount As Integer = 5
        Public Const DateTimePropertyOffset As Integer = 2
        Public Const MinIntValue As Integer = 0
        Public Const MaxIntValue As Integer = 1000

        Private Shared Function GetRandom(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As Random
            Return New Random(rowIndex + columnIndex)
        End Function

        Private Shared Function GetEnumValue(ByVal value As Integer) As UnboundSourceEnum
            Return CType(value, UnboundSourceEnum)
        End Function

        Public Shared Function GetValue(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As Object
            Select Case columnIndex Mod UniqueTypesCount
                Case 0
                    Return GetRandom(rowIndex, columnIndex).Next(MinIntValue, MaxIntValue)
                Case 1
                    Return String.Format("String {0}", GetRandom(rowIndex, columnIndex).Next(0, 100))
                Case 2
                    Return Date.Now.AddDays(GetRandom(rowIndex, columnIndex).Next(-100, 1))
                Case 3
                    Return GetRandom(rowIndex, columnIndex).Next(0, 2) = 0
                Case 4
                    Return GetEnumValue(GetRandom(rowIndex, columnIndex).Next(0, 7))
                Case Else
                    Return Nothing
            End Select
        End Function
        Public Shared Function GetColumnType(ByVal columnIndex As Integer) As Type
            Select Case columnIndex Mod UniqueTypesCount
                Case 0
                    Return GetType(Integer)
                Case 1
                    Return GetType(String)
                Case 2
                    Return GetType(Date)
                Case 3
                    Return GetType(Boolean)
                Case 4
                    Return GetType(UnboundSourceEnum)
                Case Else
                    Return Nothing
            End Select
        End Function
        Public Shared Function GetColumnName(ByVal columnIndex As Integer) As String
            Select Case columnIndex Mod UniqueTypesCount
                Case 0
                    Return String.Format("Int ({0})", columnIndex)
                Case 1
                    Return String.Format("Text ({0})", columnIndex)
                Case 2
                    Return String.Format("DateTime ({0})", columnIndex)
                Case 3
                    Return String.Format("Boolean ({0})", columnIndex)
                Case 4
                    Return String.Format("Enum ({0})", columnIndex)
                Case Else
                    Return Nothing
            End Select
        End Function
    End Class

    Public Enum UnboundSourceEnum
        Value0
        Value1
        Value2
        Value3
        Value4
        Value5
        Value6
    End Enum
End Namespace
