Imports System.Windows
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports DevExpress.Xpf.Printing

Namespace PrintingDemo.Tests
    Public Class PrintingCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Sub CreateSetCurrentDemoActions(ByVal [module] As DemoModuleDescription, ByVal checkMemoryLeaks As Boolean)
            MyBase.CreateSetCurrentDemoActions([module], checkMemoryLeaks)
            DispatchBusyWait(DocumentBuildCompletedConditionFactory.Create(Me))
            Dispatch(Sub()
                If DemoBaseTesting.CurrentDemoModule IsNot Nothing Then
                    Dim activePreview As DocumentPreviewControl = TryCast(LayoutHelper.FindElement(DemoBaseTesting.CurrentDemoModule, AddressOf IsDocumentViewer), DocumentPreviewControl)
                    Assert.IsNotNull(activePreview)
                End If
            End Sub)
        End Sub

        Private Function IsDocumentViewer(ByVal element As FrameworkElement) As Boolean
            Return TypeOf element Is DocumentPreviewControl
        End Function

        Protected Overrides Function CheckMemoryLeaks(ByVal moduleType As System.Type) As Boolean
            Return True
        End Function
    End Class
End Namespace
