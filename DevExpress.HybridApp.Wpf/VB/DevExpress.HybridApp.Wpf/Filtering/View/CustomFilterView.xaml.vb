Imports System
Imports System.Windows.Controls
Imports DevExpress.Data.Filtering.Helpers
Imports DevExpress.Xpf.Editors.Filtering
Imports DevExpress.Xpf.Editors.Settings

Namespace DevExpress.DevAV.Views
    Partial Public Class CustomFilterView
        Inherits UserControl

        Private ReadOnly Property ViewModel() As CustomFilterViewModel
            Get
                Return DirectCast(DataContext, CustomFilterViewModel)
            End Get
        End Property
        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
    Public Class CustomFilterControlColumnsBehavior
        Inherits FilterControlColumnsBehavior

        Protected Overrides Function CreateFilterColumn(ByVal columnCaption As String, ByVal editSettings As BaseEditSettings, ByVal columnType As Type, ByVal fieldName As String) As FilterColumn
            Return New CustomFilterColumn With {.ColumnCaption = columnCaption, .EditSettings = editSettings, .ColumnType = columnType, .FieldName = fieldName}
        End Function
    End Class
    Public Class CustomFilterColumn
        Inherits FilterColumn

        Public Overrides Function IsValidClause(ByVal clause As ClauseType) As Boolean
            Return MyBase.IsValidClause(clause) AndAlso (Not Equals(clause, ClauseType.Like)) AndAlso Not Equals(clause, ClauseType.NotLike)
        End Function
    End Class
End Namespace
