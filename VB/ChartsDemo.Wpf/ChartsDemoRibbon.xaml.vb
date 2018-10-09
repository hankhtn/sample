Imports System
Imports System.Globalization
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo
    Partial Public Class ChartsDemoRibbon
        Inherits UserControl

        Public Shared ReadOnly ChartProperty As DependencyProperty = DependencyProperty.Register("Chart", GetType(ChartControlBase), GetType(ChartsDemoRibbon), New PropertyMetadata(Nothing, AddressOf OnChartChanged))

        Private Shared Sub OnChartChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)

            Dim chart_Renamed As ChartControlBase = TryCast(e.NewValue, ChartControlBase)
            If chart_Renamed IsNot Nothing Then
                chart_Renamed.Palette = SavedPalette
            End If
        End Sub

        Public Property Chart() As ChartControlBase
            Get
                Return DirectCast(GetValue(ChartProperty), ChartControlBase)
            End Get
            Set(ByVal value As ChartControlBase)
                SetValue(ChartProperty, value)
            End Set
        End Property

        Private Shared Property SavedPalette() As Palette

        Shared Sub New()
            SavedPalette = New OfficePalette()
        End Sub

        Public Sub New()
            InitializeComponent()
            DataContext = New PalettesViewModel(Me)
        End Sub

        Private Class PaletteViewModel
            Inherits BindableBase

            Private ReadOnly owner As PalettesViewModel
            Private ReadOnly kind As PaletteKind

            Private isActualPalette_Renamed As Boolean

            Public ReadOnly Property Name() As String
                Get
                    Return kind.Name
                End Get
            End Property
            Public Property IsActualPalette() As Boolean
                Get
                    Return isActualPalette_Renamed
                End Get
                Set(ByVal value As Boolean)
                    If isActualPalette_Renamed <> value Then
                        SetProperty(isActualPalette_Renamed, value, Function() IsActualPalette)
                        If value Then
                            owner.SetActualPalette(Me)
                        End If
                    End If
                End Set
            End Property

            Public Sub New(ByVal kind As PaletteKind, ByVal owner As PalettesViewModel)
                Me.kind = kind
                Me.owner = owner
                Me.isActualPalette_Renamed = kind.Type Is SavedPalette.GetType()
            End Sub
            Public Function CreatePalette() As Palette
                Dim palette As Palette = DirectCast(Activator.CreateInstance(kind.Type), Palette)
                palette.ColorCycleLength = 10
                Return palette
            End Function
        End Class

        Private Class PalettesViewModel
            Inherits BindableBase


            Private palettes_Renamed() As PaletteViewModel
            Private owner As WeakReference

            Public ReadOnly Property Palettes() As PaletteViewModel()
                Get
                    Return palettes_Renamed
                End Get
            End Property

            Public Sub New(ByVal owner As ChartsDemoRibbon)
                Me.owner = New WeakReference(owner)
                palettes_Renamed = Palette.GetPredefinedKinds().Select(Function(x) New PaletteViewModel(x, Me)).ToArray()
            End Sub
            Public Sub SetActualPalette(ByVal viewModel As PaletteViewModel)
                For Each paletteModel As PaletteViewModel In palettes_Renamed
                    paletteModel.IsActualPalette = viewModel Is paletteModel
                Next paletteModel
                Dim ribbon As ChartsDemoRibbon = If(owner.IsAlive, TryCast(owner.Target, ChartsDemoRibbon), Nothing)
                If ribbon IsNot Nothing Then
                    SavedPalette = viewModel.CreatePalette()
                    ribbon.Chart.Palette = SavedPalette
                End If
            End Sub
        End Class
    End Class
End Namespace
