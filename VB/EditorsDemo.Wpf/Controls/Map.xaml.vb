Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Editors
Imports System.Globalization

Namespace EditorsDemo
    Partial Public Class Map
        Inherits Button

        Private Const totalDistance As Double = 684R
        Private Const totalTime As Double = 6R
        Public Shared ReadOnly MapDataProperty As DependencyProperty
        Shared Sub New()
            Dim ownerType As Type = GetType(Map)
            MapDataProperty = DependencyProperty.Register("MapData", GetType(MapData), ownerType, New PropertyMetadata(Nothing))
        End Sub
        Public Shared towns() As String = { "Afrene", "Hibesona", "Erarium", "Myralana", "Myrynana", "Minacius", "Lucacova", "Danyrova", "Tritrium"}
        Public Sub New()
            InitializeComponent()
            AddHandler Loaded, AddressOf Map_Loaded
        End Sub

        Private Sub Map_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateData(CType(GetTemplateChild("tb"), TrackBarEdit))
            Dim edit As TrackBarEdit = CType(GetTemplateChild("tb"), TrackBarEdit)
        End Sub
        Public Property MapData() As MapData
            Get
                Return DirectCast(GetValue(MapDataProperty), MapData)
            End Get
            Set(ByVal value As MapData)
                SetValue(MapDataProperty, value)
            End Set
        End Property
        Private Sub TrackBarEdit_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            UpdateData(DirectCast(sender, TrackBarEdit))
        End Sub
        Private Function GetStartIndex(ByVal edit As TrackBarEdit) As Integer
            Return CInt((CType(edit.EditValue, TrackBarEditRange).SelectionStart))
        End Function
        Private Function GetEndIndex(ByVal edit As TrackBarEdit) As Integer
            Return CInt((CType(edit.EditValue, TrackBarEditRange).SelectionEnd))
        End Function
        Private Sub UpdateData(ByVal edit As TrackBarEdit)
            Dim data As New MapData()
            data.StartTown = towns(GetStartIndex(edit))
            data.EndTown = towns(GetEndIndex(edit))
            data.DistanceFromAToStart = (CType(edit.EditValue, TrackBarEditRange).SelectionStart - edit.Minimum) / (edit.Maximum - edit.Minimum) * totalDistance
            data.DistanceFromAToEnd = (edit.Maximum - CType(edit.EditValue, TrackBarEditRange).SelectionStart) / (edit.Maximum - edit.Minimum) * totalDistance
            data.DistanceFromBToStart = (CType(edit.EditValue, TrackBarEditRange).SelectionEnd - edit.Minimum) / (edit.Maximum - edit.Minimum) * totalDistance
            data.DistanceFromBToEnd = (edit.Maximum - CType(edit.EditValue, TrackBarEditRange).SelectionEnd) / (edit.Maximum - edit.Minimum) * totalDistance
            data.TimeFromAToStart = (CType(edit.EditValue, TrackBarEditRange).SelectionStart - edit.Minimum) / (edit.Maximum - edit.Minimum) * totalTime
            data.TimeFromAToEnd = (edit.Maximum - CType(edit.EditValue, TrackBarEditRange).SelectionStart) / (edit.Maximum - edit.Minimum) * totalTime
            data.TimeFromBToStart = (CType(edit.EditValue, TrackBarEditRange).SelectionEnd - edit.Minimum) / (edit.Maximum - edit.Minimum) * totalTime
            data.TimeFromBToEnd = (edit.Maximum - CType(edit.EditValue, TrackBarEditRange).SelectionEnd) / (edit.Maximum - edit.Minimum) * totalTime
            data.DistanceBetween = (CType(edit.EditValue, TrackBarEditRange).SelectionEnd - CType(edit.EditValue, TrackBarEditRange).SelectionStart) / (edit.Maximum - edit.Minimum) * totalDistance
            data.TimeBetween = (CType(edit.EditValue, TrackBarEditRange).SelectionEnd - CType(edit.EditValue, TrackBarEditRange).SelectionStart) / (edit.Maximum - edit.Minimum) * totalTime
            MapData = data
        End Sub
    End Class
    Public Class MapData
        Public Property StartTown() As String
        Public Property EndTown() As String
        Public Property DistanceBetween() As Double
        Public Property DistanceFromAToEnd() As Double
        Public Property DistanceFromBToEnd() As Double
        Public Property DistanceFromAToStart() As Double
        Public Property DistanceFromBToStart() As Double
        Public Property TimeBetween() As Double
        Public Property TimeFromAToEnd() As Double
        Public Property TimeFromBToEnd() As Double
        Public Property TimeFromAToStart() As Double
        Public Property TimeFromBToStart() As Double
    End Class
    Public Class MapDataToTextConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim data As MapData = DirectCast(value, MapData)
            If data Is Nothing Then
                Return Nothing
            End If
            Dim text As String = Nothing
            If DirectCast(parameter, String) = "Start" Then
                text = data.StartTown.ToUpper() & ControlChars.Lf & Map.towns(0) & " - " & data.StartTown & ": " & data.TimeFromAToStart & " h, " & data.DistanceFromAToStart & " km" & ControlChars.Lf & data.StartTown & " - " & Map.towns(Map.towns.Length - 1) & ": " & data.TimeFromAToEnd & " h, " & data.DistanceFromAToEnd & " km"
            End If
            If DirectCast(parameter, String) = "End" Then
                text = data.EndTown.ToUpper() & ControlChars.Lf & Map.towns(0) & " - " & data.EndTown & ": " & data.TimeFromBToStart & " h, " & data.DistanceFromBToStart & " km" & ControlChars.Lf & data.EndTown & " - " & Map.towns(Map.towns.Length - 1) & ": " & data.TimeFromBToEnd & " h, " & data.DistanceFromBToEnd & " km"
            End If
            If DirectCast(parameter, String) = "Total" Then
                text = data.StartTown.ToUpper() & " - " & data.EndTown.ToUpper() & ControlChars.Lf & data.TimeBetween.ToString() & " h, " & data.DistanceBetween.ToString() & " km"
            End If
            Return text
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        #End Region
    End Class
End Namespace
