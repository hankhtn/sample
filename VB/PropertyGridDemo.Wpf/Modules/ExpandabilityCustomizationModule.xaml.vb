Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
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
Imports DevExpress.Xpf.PropertyGrid

Namespace PropertyGridDemo
    Partial Public Class ExpandabilityCustomizationModule
        Inherits PropertyGridDemoModule

        Public Sub New()
            InitializeComponent()
            DataContext = New Container With {
                .Simple = New ClassWithProperties With {.Id = 0, .Name = "Apple"},
                .Expandable = New ClassWithProperties With {.Id = 1, .Name = "Pinapple"},
                .NotExpandable = New ClassWithProperties With {.Id = 2, .Name = "Orange"}
            }
        End Sub
        Private Sub PropertyGridControl_OnCustomExpand(ByVal sender As Object, ByVal args As CustomExpandEventArgs)
            If args.IsInitializing Then
                args.IsExpanded = True
            End If
        End Sub
    End Class
    Public Class Container
        Public Property Simple() As ClassWithProperties
        <TypeConverter(GetType(ExpandableObjectConverter))>
        Public Property Expandable() As ClassWithProperties
        <TypeConverter(GetType(NotExpandableConverter))>
        Public Property NotExpandable() As ClassWithProperties
    End Class
    Public Class ClassWithProperties
        Public Property Id() As Integer
        Public Property Name() As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
    Public Class NotExpandableConverter
        Inherits TypeConverter

        Public Overrides Function GetPropertiesSupported(ByVal context As ITypeDescriptorContext) As Boolean
            Return False
        End Function
    End Class
End Namespace
