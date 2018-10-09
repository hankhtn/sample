Imports System
Imports System.Windows
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Printing
Imports DevExpress.XtraPrinting.DataNodes

Namespace PrintingDemo.Tests
    Public Class CheckDemoOptionsFixture
        Inherits BaseDemoTestingFixture

        Private ReadOnly Property GroupedReportModule() As GroupsModule
            Get
                Return CType(DemoBaseTesting.CurrentDemoModule, GroupsModule)
            End Get
        End Property
        Private ReadOnly Property ColumnReportModule() As MultiColumnModule
            Get
                Return CType(DemoBaseTesting.CurrentDemoModule, MultiColumnModule)
            End Get
        End Property

        Protected Overrides Sub CreateActions()
            MyBase.CreateActions()
            CreateCheckGroupedReportModuleActions()
            CreateCheckMultiColumnReportModuleActions()
        End Sub

        Private Sub CreateCheckGroupedReportModuleActions()
            AddLoadModuleActions(GetType(GroupsModule))

            AddAction_ToggleCheckEdit(Function() GroupedReportModule.repeatHeaderEveryPage, True, Function() CType(GroupedReportModule.ViewModel.Link, CollectionViewLink).GroupInfos(0).RepeatHeaderEveryPage)
            WaitDocumentBuildCompleted()

            AddAction_ToggleCheckEdit(Function() GroupedReportModule.repeatHeaderEveryPage, False, Function() CType(GroupedReportModule.ViewModel.Link, CollectionViewLink).GroupInfos(0).RepeatHeaderEveryPage)
            WaitDocumentBuildCompleted()

            AddAction_ToggleCheckEdit(Function() GroupedReportModule.keepTogether, GroupUnion.WholePage, Function() CType(GroupedReportModule.ViewModel.Link, CollectionViewLink).GroupInfos(0).Union)
            WaitDocumentBuildCompleted()

            AddAction_ToggleCheckEdit(Function() GroupedReportModule.pageBreakAfter, True, Function() CType(GroupedReportModule.ViewModel.Link, CollectionViewLink).GroupInfos(0).PageBreakAfter)
            WaitDocumentBuildCompleted()
        End Sub

        Private Sub CreateCheckMultiColumnReportModuleActions()
            AddLoadModuleActions(GetType(MultiColumnModule))
            For Each pageNumberKind As PageNumberKind In DirectCast(DispatchExpr(Function() ColumnReportModule.pageNumberKindEdit.ItemsSource), System.Collections.IEnumerable)
                IAmAlive("Changing the number kind")
                Dim temp_pageNumberKind As PageNumberKind = pageNumberKind
                Dispatch(Sub() ColumnReportModule.pageNumberKindEdit.EditValue = temp_pageNumberKind)
                WaitDocumentBuildCompleted()

                For Each pageNumberAlignment As HorizontalAlignment In DirectCast(DispatchExpr(Function() ColumnReportModule.pageNumberAlignmentEdit.ItemsSource), System.Collections.IEnumerable)
                    Dim temp_pageNumberAlignment As HorizontalAlignment = pageNumberAlignment
                    Dispatch(Sub() ColumnReportModule.pageNumberAlignmentEdit.EditValue = temp_pageNumberAlignment)
                    WaitDocumentBuildCompleted()

                    For Each pageNumberLocation As PageNumberLocation In DirectCast(DispatchExpr(Function() ColumnReportModule.pageNumberLocationEdit.ItemsSource), System.Collections.IEnumerable)
                        Dim temp_pageNumberLocation As PageNumberLocation = pageNumberLocation
                        Dispatch(Sub() ColumnReportModule.pageNumberLocationEdit.EditValue = temp_pageNumberLocation)
                        WaitDocumentBuildCompleted()
                    Next pageNumberLocation
                Next pageNumberAlignment
            Next pageNumberKind

            Dispatch(Sub() ColumnReportModule.downThenAcross.IsChecked = True)
            WaitDocumentBuildCompleted()
            Dispatch(Sub() ColumnReportModule.acrossThenDown.IsChecked = True)
            WaitDocumentBuildCompleted()

            Dispatch(Sub() EditorsActions.SetEditValue(ColumnReportModule.pageNumberFormatEdit, "Page {test}"))
            WaitDocumentBuildCompleted()
        End Sub

        Private Sub AddAction_ToggleCheckEdit(ByVal getCheckEdit As Func(Of CheckEdit), ByVal expectedValue As Object, ByVal getActualValue As Func(Of Object))
            Dispatch(Sub() EditorsActions.ToggleCheckEdit(getCheckEdit()))
            Assert.AreEqual(expectedValue, DispatchExpr(getActualValue))
        End Sub

        Private Sub WaitDocumentBuildCompleted()
            DispatchBusyWait(DocumentBuildCompletedConditionFactory.Create(Me))
        End Sub
    End Class
End Namespace
