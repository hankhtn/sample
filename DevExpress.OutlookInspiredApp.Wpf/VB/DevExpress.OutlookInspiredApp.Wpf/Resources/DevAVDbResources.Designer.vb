Imports System

Namespace DevExpress.DevAV.Resources


    ''' <summary>
    '''   A strongly-typed resource class, for looking up localized strings, etc.
    ''' </summary>




    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>
    Public Class DevAVDbResources

        Private Shared resourceMan As Global.System.Resources.ResourceManager

        Private Shared resourceCulture As Global.System.Globalization.CultureInfo

        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>
        Friend Sub New()
        End Sub

        ''' <summary>
        '''   Returns the cached ResourceManager instance used by this class.
        ''' </summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>
        Public Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As New Global.System.Resources.ResourceManager("DevAVDbResources", GetType(DevAVDbResources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property

        ''' <summary>
        '''   Overrides the current thread's CurrentUICulture property for all
        '''   resource lookups using this strongly typed resource class.
        ''' </summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>
        Public Shared Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set(ByVal value As System.Globalization.CultureInfo)
                resourceCulture = value
            End Set
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to Task Attached File.
        ''' </summary>
        Public Shared ReadOnly Property TaskAttachedFile() As String
            Get
                Return ResourceManager.GetString("TaskAttachedFile", resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to Content.
        ''' </summary>
        Public Shared ReadOnly Property TaskAttachedFile_Content() As String
            Get
                Return ResourceManager.GetString("TaskAttachedFile_Content", resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to Employee Task.
        ''' </summary>
        Public Shared ReadOnly Property TaskAttachedFile_EmployeeTask() As String
            Get
                Return ResourceManager.GetString("TaskAttachedFile_EmployeeTask", resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to Id.
        ''' </summary>
        Public Shared ReadOnly Property TaskAttachedFile_Id() As String
            Get
                Return ResourceManager.GetString("TaskAttachedFile_Id", resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to Name.
        ''' </summary>
        Public Shared ReadOnly Property TaskAttachedFile_Name() As String
            Get
                Return ResourceManager.GetString("TaskAttachedFile_Name", resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to Attached Files.
        ''' </summary>
        Public Shared ReadOnly Property TaskAttachedFile_Plural() As String
            Get
                Return ResourceManager.GetString("TaskAttachedFile_Plural", resourceCulture)
            End Get
        End Property
    End Class
End Namespace
