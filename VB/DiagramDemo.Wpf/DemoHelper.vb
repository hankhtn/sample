Imports System.Collections
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports DevExpress.DemoData.Utils
Imports DevExpress.Diagram.Core

Namespace DevExpress.Diagram.Demos
    Friend NotInheritable Class DemoHelper

        Private Sub New()
        End Sub

        Public Shared Function CreatePredefinedSvgStencil(ByVal stencilId As String, ByVal stencilName As String, Optional ByVal visible As Boolean = False) As DiagramStencil
            Dim stencil As New DiagramStencil(stencilId, stencilName, visible)
            InitializePredefinedStencil(stencil)
            Return stencil
        End Function
        Private Shared Sub InitializePredefinedStencil(ByVal stencil As DiagramStencil)
            Const directoryPath As String = "images/officeplan"
            Dim assembly = GetType(DemoHelper).Assembly
            Dim filePaths = AssemblyHelper.GetResources(assembly).OfType(Of DictionaryEntry)().Select(Function(x) CStr(x.Key)).Where(Function(x) x.StartsWith(directoryPath)).OrderBy(Function(x) x)
            For Each filePath In filePaths
                Dim fileName As String = Regex.Match(filePath.Replace("%20", " "), "(?<=\/)[A-z0-9 ]*(?=\.svg)").Value
                Dim shapeId As String = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(fileName)
                Dim stream = AssemblyHelper.GetResourceStream(assembly, filePath, True)
                Dim sd = ShapeDescription.CreateSvgShape(shapeId, shapeId, stream, False)
                stencil.RegisterShape(sd)
            Next filePath
        End Sub
        Public Shared Sub InitializeSvgShape(ByVal stencil As DiagramStencil, ByVal shape As IDiagramShape)
            If shape IsNot Nothing AndAlso stencil.ContainsShape(shape.Shape) Then
                shape.CanEdit = False
            End If
        End Sub
        Public Shared Function CreateStencilFromFile(ByVal fileName As String, ByVal stencilId As String, ByVal stencilName As String, Optional ByVal visible As Boolean = False) As DiagramStencil
            Dim customShapesStencil As DiagramStencil = Nothing
            Using stream = File.OpenRead(fileName)
                customShapesStencil = DiagramStencil.Create(stencilId, stencilName, stream, Function(s) s, visible)
            End Using
            Return customShapesStencil
        End Function
        Public Shared Function CreateExtendedStencilCollection(ParamArray ByVal exStencils() As DiagramStencil) As DiagramStencilCollection
            Return New DiagramStencilCollection(exStencils.Concat(DiagramToolboxRegistrator.Stencils))
        End Function
    End Class
End Namespace
