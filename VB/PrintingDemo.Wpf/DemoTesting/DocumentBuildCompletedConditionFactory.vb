Imports System
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports DevExpress.Xpf.Printing
Imports DevExpress.Mvvm.Native
Imports DevExpress.Xpf.Printing.PreviewControl.Native
Imports DevExpress.Xpf.Printing.PreviewControl

Namespace PrintingDemo.Tests
    Friend NotInheritable Class DocumentBuildCompletedConditionFactory

        Private Sub New()
        End Sub


        Private Class ConditionAdapter
            Private fixture As BaseDemoTestingFixture

            Public Sub New(ByVal fixture As BaseDemoTestingFixture)
                If fixture Is Nothing Then
                    Throw New ArgumentNullException("fixture")
                End If

                Me.fixture = fixture
            End Sub

            Public Function EvaluateCondition() As Boolean
                If fixture.DemoBaseTesting.CurrentDemoModule Is Nothing Then
                    Return True
                End If
                Dim viewer As DocumentPreviewControl = BaseTestingFixture.HelperActions.FindElementByType(Of DocumentPreviewControl)(fixture.DemoBaseTesting.CurrentDemoModule)
                Dim model As IDocumentViewModel = viewer.Document
                Dim documentBuilt As Boolean = If(model Is Nothing, False, (Not model.IsCreating) AndAlso model.Pages.Count > 0)






                Return documentBuilt
            End Function
        End Class






        Public Shared Function Create(ByVal fixture As BaseDemoTestingFixture) As Func(Of Boolean)
            Dim conditionAdapter As New ConditionAdapter(fixture)
            Return Function() conditionAdapter.EvaluateCondition()
        End Function
    End Class
End Namespace
