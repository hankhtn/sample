Imports System
Imports System.Windows.Controls
Imports System.Collections.Generic
Imports System.Windows.Media
Imports DevExpress.Xpf.Map
Imports System.ComponentModel

Namespace MapDemo
    Partial Public Class Colorizer
        Inherits MapDemoModule
        Implements INotifyPropertyChanged


        Private toolTipPattern_Renamed As String
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Public Property ToolTipPattern() As String
            Get
                Return toolTipPattern_Renamed
            End Get
            Set(ByVal value As String)
                If toolTipPattern_Renamed <> value Then
                    toolTipPattern_Renamed = value
                    NotifyPropertyChanged("ToolTipPattern")
                End If
            End Set
        End Property

        Private ReadOnly gdpColorizer As MapColorizer
        Private ReadOnly populationColorizer As MapColorizer
        Private ReadOnly politicalColorizer As MapColorizer

        Public Sub New()
            InitializeComponent()
            DataContext = Me
            gdpColorizer = TryCast(Resources("gdpColorizer"), MapColorizer)
            populationColorizer = TryCast(Resources("populationColorizer"), MapColorizer)
            politicalColorizer = TryCast(Resources("politicalColorizer"), MapColorizer)
            vectorLayer.Colorizer = gdpColorizer
            ToolTipPattern = "{NAME} : ${GDP_MD_EST:n2}M"
        End Sub

        Private Sub NotifyPropertyChanged(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
        Private Sub lbMapType_EditValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            If lbMapType.SelectedIndex = 0 Then
                vectorLayer.Colorizer = gdpColorizer
                ToolTipPattern = "{NAME} : ${GDP_MD_EST:n2}M"
            End If
            If lbMapType.SelectedIndex = 1 Then
                vectorLayer.Colorizer = populationColorizer
                ToolTipPattern = "{NAME} : {POP_EST:#,##0,,}M"
            End If
            If lbMapType.SelectedIndex = 2 Then
                vectorLayer.Colorizer = politicalColorizer
                ToolTipPattern = "{NAME}"
            End If
        End Sub
    End Class
End Namespace
