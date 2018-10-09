Imports DevExpress.Data
Imports DevExpress.Utils
Imports DevExpress.Mvvm
Imports DevExpress.XtraGrid
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Forms
Imports DevExpress.Xpf.Grid

Namespace ProductsDemo.Modules
    Public Class GridColumnModel
        Inherits BindableBase

        Public Property Name() As String
        Public Property DisplayName() As String


        Private isGrouped_Renamed As Boolean
        Public Property IsGrouped() As Boolean
            Get
                Return isGrouped_Renamed
            End Get
            Set(ByVal value As Boolean)
                If isGrouped_Renamed = value Then
                    Return
                End If
                isGrouped_Renamed = value
                RaisePropertiesChanged("IsGrouped")
            End Set
        End Property


        Private width_Renamed As GridColumnWidth = Double.NaN
        Public Property Width() As GridColumnWidth
            Get
                Return width_Renamed
            End Get
            Set(ByVal value As GridColumnWidth)
                width_Renamed = value
                RaisePropertiesChanged("Width")
            End Set
        End Property
        Public Property HorizontalHeaderContentAlignment() As System.Windows.HorizontalAlignment

        Private allowFiltering_Renamed As DefaultBoolean = DefaultBoolean.True
        Public Property AllowFiltering() As DefaultBoolean
            Get
                Return allowFiltering_Renamed
            End Get
            Set(ByVal value As DefaultBoolean)
                allowFiltering_Renamed = value
            End Set
        End Property

        Private allowSorting_Renamed As DefaultBoolean = DefaultBoolean.True
        Public Property AllowSorting() As DefaultBoolean
            Get
                Return allowSorting_Renamed
            End Get
            Set(ByVal value As DefaultBoolean)
                allowSorting_Renamed = value
            End Set
        End Property
        Public Property AllowEditing() As DefaultBoolean
        Public Property GroupInterval() As ColumnGroupInterval
        Public Property FixedWidth() As Boolean
        Public Property Mask() As String


        Private sortOrder_Renamed As ColumnSortOrder
        Public Property SortOrder() As ColumnSortOrder
            Get
                Return sortOrder_Renamed
            End Get
            Set(ByVal value As ColumnSortOrder)
                sortOrder_Renamed = value
                RaisePropertiesChanged("SortOrder")
            End Set
        End Property


        Private index_Renamed As Integer
        Public Property Index() As Integer
            Get
                Return index_Renamed
            End Get
            Set(ByVal value As Integer)
                index_Renamed = value
                RaisePropertiesChanged("Index")
            End Set
        End Property


        Private isVisible_Renamed As Boolean = True
        Public Property IsVisible() As Boolean
            Get
                Return isVisible_Renamed
            End Get
            Set(ByVal value As Boolean)
                isVisible_Renamed = value
                RaisePropertiesChanged("IsVisible")
            End Set
        End Property
    End Class
End Namespace
