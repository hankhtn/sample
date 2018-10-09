Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.RichEdit
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

Namespace DevExpress.DevAV.Views
    Partial Public Class MailMergeView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub
        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            richEdit.MailMergeOptions = RichEditMailMergeOptions
            If RichEditBehavior IsNot Nothing Then
                Interaction.GetBehaviors(richEdit).Add(RichEditBehavior)
            End If
        End Sub

        Public Shared ReadOnly RichEditDocumentSourceProperty As DependencyProperty = DependencyProperty.Register("RichEditDocumentSource", GetType(Object), GetType(MailMergeView), New PropertyMetadata(Nothing))
        Public Shared ReadOnly AdditionalParametersFormProperty As DependencyProperty = DependencyProperty.Register("AdditionalParametersForm", GetType(FrameworkElement), GetType(MailMergeView), New PropertyMetadata(Nothing))

        Public Property RichEditDocumentSource() As Object
            Get
                Return GetValue(RichEditDocumentSourceProperty)
            End Get
            Set(ByVal value As Object)
                SetValue(RichEditDocumentSourceProperty, value)
            End Set
        End Property
        Public Property AdditionalParametersForm() As FrameworkElement
            Get
                Return DirectCast(GetValue(AdditionalParametersFormProperty), FrameworkElement)
            End Get
            Set(ByVal value As FrameworkElement)
                SetValue(AdditionalParametersFormProperty, value)
            End Set
        End Property

        Public Property RichEditBehavior() As Behavior
        Public Property RichEditMailMergeOptions() As DXRichEditMailMergeOptions
    End Class
End Namespace
