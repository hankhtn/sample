Imports DevExpress.Mvvm.DataAnnotations
Imports System
Imports System.Collections.Generic
Imports DevExpress.Data
Imports Point = System.Drawing.Point

Namespace GridDemo
    <POCOViewModel>
    Public Class VirtualDataSourceViewModel
        Private ReadOnly valuesCache As New Dictionary(Of Point, Object)()

        Public Sub New()
            FormatConditions = CreateFormatConditions()
        End Sub

        Private privateFormatConditions As FormatConditionModel()
        Public Property FormatConditions() As FormatConditionModel()
            Get
                Return privateFormatConditions
            End Get
            Private Set(ByVal value As FormatConditionModel())
                privateFormatConditions = value
            End Set
        End Property

        Private Function CreateFormatConditions() As FormatConditionModel()
            Dim uniqueColumnsCount As Integer = VirtualDataSource.ColumnsCount \ VirtualDataSourceValuesProvider.UniqueTypesCount
            Dim intDelta As Integer = VirtualDataSourceValuesProvider.MaxIntValue - VirtualDataSourceValuesProvider.MinIntValue
            Dim intGreaterValue As Integer = CInt((intDelta * 0.95))
            Dim intLessValue As Integer = CInt((intDelta * 0.05))
            Dim yesterday As Date = Date.Now.AddDays(-1)

            Dim formatConditions_Renamed((uniqueColumnsCount * 3) - 1) As FormatConditionModel
            For i As Integer = 0 To uniqueColumnsCount - 1
                Dim intColumnName As String = String.Format("Int ({0})", i * VirtualDataSourceValuesProvider.UniqueTypesCount)
                formatConditions_Renamed(i) = CreateFormatCondition(intColumnName, FormatType.TopPercent, ConditionType.Greater, intGreaterValue)
                formatConditions_Renamed(uniqueColumnsCount + i) = CreateFormatCondition(intColumnName, FormatType.BottomPercent, ConditionType.Less, intLessValue)
                Dim dateTimeColumnName As String = String.Format("DateTime ({0})", i * VirtualDataSourceValuesProvider.UniqueTypesCount + VirtualDataSourceValuesProvider.DateTimePropertyOffset)
                formatConditions_Renamed(uniqueColumnsCount * 2 + i) = CreateFormatCondition(dateTimeColumnName, FormatType.DateTimeToday, ConditionType.Greater, yesterday)
            Next i
            Return formatConditions_Renamed
        End Function

        Private Function CreateFormatCondition(ByVal fieldName As String, ByVal formatType As FormatType, ByVal conditionType As ConditionType, ByVal value As Object) As FormatConditionModel
            Dim formatCondition As New FormatConditionModel()
            formatCondition.FormatType = formatType
            formatCondition.PropertyName = fieldName
            formatCondition.ConditionType = conditionType
            formatCondition.Value = value
            Return formatCondition
        End Function

        Public Function GetValue(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As Object
            Dim value As Object = Nothing
            If valuesCache.TryGetValue(New Point(rowIndex, columnIndex), value) Then
                Return value
            End If
            Return VirtualDataSourceValuesProvider.GetValue(rowIndex, columnIndex)
        End Function
        Public Sub SetValue(ByVal rowIndex As Integer, ByVal columnIndex As Integer, ByVal value As Object)
            Dim key = New Point(rowIndex, columnIndex)
            If valuesCache.ContainsKey(key) Then
                valuesCache(key) = value
            Else
                valuesCache.Add(key, value)
            End If
        End Sub
        Public Function CreateProperty(ByVal index As Integer) As VirtualDataSourceProperty
            Return New VirtualDataSourceProperty(VirtualDataSourceValuesProvider.GetColumnName(index), VirtualDataSourceValuesProvider.GetColumnType(index))
        End Function
    End Class

    Public Class FormatConditionModel
        Public Property PropertyName() As String
        Public Property FormatType() As FormatType
        Public Property ConditionType() As ConditionType
        Public Property Value() As Object
    End Class
    Public Class VirtualDataSourceProperty
        Public Sub New(ByVal name As String, ByVal type As Type)
            Me.Name = name
            Me.Type = type
        End Sub
        Private privateName As String
        Public Property Name() As String
            Get
                Return privateName
            End Get
            Private Set(ByVal value As String)
                privateName = value
            End Set
        End Property
        Private privateType As Type
        Public Property Type() As Type
            Get
                Return privateType
            End Get
            Private Set(ByVal value As Type)
                privateType = value
            End Set
        End Property
    End Class

    Public Enum FormatType
        DateTimeToday
        TopPercent
        BottomPercent
    End Enum
    Public Enum ConditionType
        Greater
        Less
    End Enum
End Namespace
