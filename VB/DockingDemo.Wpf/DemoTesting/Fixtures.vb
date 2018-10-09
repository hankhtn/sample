Imports System
Imports System.Windows
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports DevExpress.Xpf.Docking
Imports DevExpress.Xpf.Layout.Core
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Bars

Namespace DockingDemo.Tests
    #Region "DockingDemoModulesAccessor"
    Public Class DockingDemoModulesAccessor
        Inherits DemoModulesAccessor(Of DockingDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
        Private manager As DockLayoutManager
        Public ReadOnly Property DockLayoutManager() As DockLayoutManager
            Get
                If manager Is Nothing Then
                    manager = TryCast(LayoutHelper.FindElement(DemoModule, AddressOf Criteria), DockLayoutManager)
                End If
                Return manager
            End Get
        End Property
        Private Function Criteria(ByVal element As FrameworkElement) As Boolean
            Return TypeOf element Is DevExpress.Xpf.Docking.DockLayoutManager
        End Function
        Public Sub ResetDockLayoutManager()
            manager = Nothing
        End Sub

        Public ReadOnly Property RowColumnLayoutModule() As PanelGroups
            Get
                Return TryCast(DemoModule, PanelGroups)
            End Get
        End Property
        Public ReadOnly Property FloatPanelsModule() As FloatingPanels
            Get
                Return TryCast(DemoModule, FloatingPanels)
            End Get
        End Property
        Public ReadOnly Property SerializationModule() As Serialization
            Get
                Return TryCast(DemoModule, Serialization)
            End Get
        End Property
        Public ReadOnly Property MvvmSerializationModule() As MVVMSerialization
            Get
                Return TryCast(DemoModule, MVVMSerialization)
            End Get
        End Property
        Public ReadOnly Property DocumentGroupsModule() As DocumentGroups
            Get
                Return TryCast(DemoModule, DocumentGroups)
            End Get
        End Property
        Public ReadOnly Property IDEWorkspacesModule() As IDEWorkspaces
            Get
                Return TryCast(DemoModule, IDEWorkspaces)
            End Get
        End Property
    End Class
    #End Region ' DockingDemoModulesAccessor
    Public MustInherit Class BaseDockingDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private ReadOnly modulesAccessor As DockingDemoModulesAccessor
        Public Sub New()
            modulesAccessor = New DockingDemoModulesAccessor(Me)
        End Sub
        Public ReadOnly Property DockLayoutManager() As DockLayoutManager
            Get
                Return modulesAccessor.DockLayoutManager
            End Get
        End Property
        Protected Sub ResetDockLayoutManager()
            modulesAccessor.ResetDockLayoutManager()
        End Sub

        Public ReadOnly Property RowColumnLayoutModule() As PanelGroups
            Get
                Return modulesAccessor.RowColumnLayoutModule
            End Get
        End Property
        Public ReadOnly Property SerializationModule() As Serialization
            Get
                Return modulesAccessor.SerializationModule
            End Get
        End Property
        Public ReadOnly Property MvvmSerializationModule() As MVVMSerialization
            Get
                Return modulesAccessor.MvvmSerializationModule
            End Get
        End Property
        Public ReadOnly Property FloatPanelsModule() As FloatingPanels
            Get
                Return modulesAccessor.FloatPanelsModule
            End Get
        End Property
        Public ReadOnly Property DocumentGroupsModule() As DocumentGroups
            Get
                Return modulesAccessor.DocumentGroupsModule
            End Get
        End Property
        Public ReadOnly Property IDEWorkspaces() As IDEWorkspaces
            Get
                Return modulesAccessor.IDEWorkspacesModule
            End Get
        End Property
    End Class

    Public Class DockingCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Sub CreateSwitchAllThemesActions()
            MyBase.CreateSwitchAllThemesActions()
        End Sub
    End Class

    Public Class CheckDemoOptionsFixture
        Inherits BaseDockingDemoTestingFixture

        #Region "Initialization"
        Protected Overrides Sub CreateActions()
            MyBase.CreateActions()
            CheckIDEWorkspacesModule()
            CheckSerializationModule()
            CheckMvvmSerializationModule()
            CheckDocumentGroupsModule()
            CheckRowColumnLayoutModule()
            CreateSetCurrentDemoActions(Nothing, False)
        End Sub
        Private Sub CheckRowColumnLayoutModule()
            AddLoadModuleActions(GetType(PanelGroups))
            Dispatch(AddressOf RowColumnLayoutTest)
        End Sub
        Private Sub CheckSerializationModule()
            AddLoadModuleActions(GetType(Serialization))
            Dispatch(AddressOf SerializationTest)
        End Sub
        Private Sub CheckMvvmSerializationModule()
            AddLoadModuleActions(GetType(MVVMSerialization))
            Dispatch(AddressOf MVVMSerializationTest)
        End Sub
        Private Sub CheckDocumentGroupsModule()
            AddLoadModuleActions(GetType(DocumentGroups))
            Dispatch(AddressOf DocumentGroupsTest)
        End Sub
        Private Sub CheckIDEWorkspacesModule()
            AddLoadModuleActions(GetType(IDEWorkspaces))
            Dispatch(AddressOf IDEWorkspacesTest)
        End Sub
        #End Region ' Initialization
        Private Sub IDEWorkspacesTest()

            Dim document1 As DocumentPanel = TryCast(IDEWorkspaces.DemoDockContainer.GetItem("document1"), DocumentPanel)
            Assert.IsNotNull(document1)
            Dim floatGroup As FloatGroup = IDEWorkspaces.DemoDockContainer.DockController.Float(document1)
            UpdateLayoutAndDoEvents()
            IDEWorkspaces.DemoDockContainer.DockController.Hide(floatGroup)
            UpdateLayoutAndDoEvents()
            IDEWorkspaces.DemoDockContainer.DockController.Close(floatGroup)
            UpdateLayoutAndDoEvents()
            WorkspaceManager.GetWorkspaceManager(IDEWorkspaces.DemoDockContainer).ApplyWorkspace("workspace2")
            UpdateLayoutAndDoEvents()
            Return
        End Sub
        Private Sub RowColumnLayoutTest()
            ResetDockLayoutManager()

            Assert.AreEqual(3, DockLayoutManager.LayoutRoot.Items.Count)
            Assert.IsTrue(DockLayoutManager.LayoutRoot.Orientation = System.Windows.Controls.Orientation.Horizontal)
            Assert.IsTrue(RowColumnLayoutModule.allowSplittersCheck.IsChecked.Value)
            Assert.AreEqual(0, RowColumnLayoutModule.orientationListBox.SelectedIndex)

            Assert.IsTrue(DockLayoutManager.LayoutRoot.IsSplittersEnabled)
            EditorsActions.ToggleCheckEdit(RowColumnLayoutModule.allowSplittersCheck)
            UpdateLayoutAndDoEvents()
            Assert.IsFalse(DockLayoutManager.LayoutRoot.IsSplittersEnabled)

            EditorsActions.ToggleCheckEdit(RowColumnLayoutModule.allowSplittersCheck)
            UpdateLayoutAndDoEvents()
            Assert.IsTrue(DockLayoutManager.LayoutRoot.IsSplittersEnabled)

            RowColumnLayoutModule.orientationListBox.SelectedIndex = 1
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(1, RowColumnLayoutModule.orientationListBox.SelectedIndex)
            Assert.IsTrue(DockLayoutManager.LayoutRoot.Orientation = System.Windows.Controls.Orientation.Vertical)

            RowColumnLayoutModule.orientationListBox.SelectedIndex = 0
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(0, RowColumnLayoutModule.orientationListBox.SelectedIndex)
            Assert.IsTrue(DockLayoutManager.LayoutRoot.Orientation = System.Windows.Controls.Orientation.Horizontal)
        End Sub
        Private Sub SerializationTest()
            ResetDockLayoutManager()

            Dim root As LayoutGroup = DockLayoutManager.LayoutRoot

            Assert.AreEqual(3, root.Items.Count)
            Assert.IsTrue(TypeOf root.Items(0) Is LayoutGroup)
            Assert.IsTrue(TypeOf root.Items(1) Is LayoutPanel)
            Assert.IsTrue(TypeOf root.Items(2) Is LayoutGroup)

            Dim panel1 As LayoutPanel = TryCast(CType(root.Items(0), LayoutGroup).Items(0), LayoutPanel)
            Dim panel2 As LayoutPanel = TryCast(CType(root.Items(0), LayoutGroup).Items(1), LayoutPanel)
            Dim panel3 As LayoutPanel = TryCast(CType(root.Items(2), LayoutGroup).Items(0), LayoutPanel)
            Dim panel4 As LayoutPanel = TryCast(CType(root.Items(2), LayoutGroup).Items(1), LayoutPanel)

            Assert.IsTrue(SerializationModule.serializeButton.IsEnabled)
            Assert.IsFalse(SerializationModule.deserializeButton.IsEnabled)
            Assert.AreEqual(SerializationModule.layoutSampleName.Items.Count, 4)
            Assert.AreEqual(SerializationModule.layoutSampleName.SelectedIndex, 0)

            SerializationModule.layoutSampleName.SelectedIndex = 1
            UIAutomationActions.ClickButton(SerializationModule.loadSampleLayoutButton)
            UpdateLayoutAndDoEvents()
            SerializationModule.layoutSampleName.SelectedIndex = 2
            UIAutomationActions.ClickButton(SerializationModule.loadSampleLayoutButton)
            UpdateLayoutAndDoEvents()
            SerializationModule.layoutSampleName.SelectedIndex = 3
            UIAutomationActions.ClickButton(SerializationModule.loadSampleLayoutButton)
            UpdateLayoutAndDoEvents()
            SerializationModule.layoutSampleName.SelectedIndex = 0
            UIAutomationActions.ClickButton(SerializationModule.loadSampleLayoutButton)
            UpdateLayoutAndDoEvents()

            UIAutomationActions.ClickButton(SerializationModule.serializeButton)
            UpdateLayoutAndDoEvents()
            Assert.IsTrue(SerializationModule.deserializeButton.IsEnabled)
            Assert.IsFalse(panel1.IsAutoHidden)
            DockLayoutManager.DockController.Hide(panel1)
            Assert.IsTrue(panel1.IsAutoHidden)
            UIAutomationActions.ClickButton(SerializationModule.deserializeButton)
            UpdateLayoutAndDoEvents()
            Assert.IsFalse(panel1.IsAutoHidden)
        End Sub
        Private Sub MVVMSerializationTest()
            ResetDockLayoutManager()

            Dim root As LayoutGroup = DockLayoutManager.LayoutRoot

            Assert.AreEqual(1, root.Items.Count)
            Assert.IsTrue(TypeOf root.Items(0) Is LayoutGroup)

            Assert.AreEqual(5, CType(root.Items(0), LayoutGroup).Items.Count)

            Dim panel1 As LayoutPanel = TryCast(CType(root.Items(0), LayoutGroup).Items(0), LayoutPanel)
            Dim panel2 As LayoutPanel = TryCast(CType(root.Items(0), LayoutGroup).Items(1), LayoutPanel)
            Dim panel3 As LayoutPanel = TryCast(CType(root.Items(0), LayoutGroup).Items(2), LayoutPanel)
            Dim panel4 As LayoutPanel = TryCast(CType(root.Items(0), LayoutGroup).Items(3), LayoutPanel)
            Dim panel5 As LayoutPanel = TryCast(CType(root.Items(0), LayoutGroup).Items(3), LayoutPanel)

            Assert.IsNotNull(panel1)
            Assert.IsNotNull(panel2)
            Assert.IsNotNull(panel3)
            Assert.IsNotNull(panel4)
            Assert.IsNotNull(panel5)

            Assert.IsTrue(MvvmSerializationModule.serializeButton.IsEnabled)
            Assert.IsFalse(MvvmSerializationModule.deserializeButton.IsEnabled)
            Assert.AreEqual(MvvmSerializationModule.layoutSampleName.Items.Count, 3)
            Assert.AreEqual(MvvmSerializationModule.layoutSampleName.SelectedIndex, 0)


            MouseActions.ClickElement(MvvmSerializationModule.serializeButton)
            UpdateLayoutAndDoEvents()
            Assert.IsTrue(MvvmSerializationModule.deserializeButton.IsEnabled)


            MvvmSerializationModule.layoutSampleName.SelectedIndex = 1
            UIAutomationActions.ClickButton(MvvmSerializationModule.loadSampleLayoutButton)
            UpdateLayoutAndDoEvents()
            MvvmSerializationModule.layoutSampleName.SelectedIndex = 2
            UIAutomationActions.ClickButton(MvvmSerializationModule.loadSampleLayoutButton)
            UpdateLayoutAndDoEvents()
            MvvmSerializationModule.layoutSampleName.SelectedIndex = 0
            UIAutomationActions.ClickButton(MvvmSerializationModule.loadSampleLayoutButton)
            UpdateLayoutAndDoEvents()

            MouseActions.ClickElement(MvvmSerializationModule.deserializeButton)
            UpdateLayoutAndDoEvents()

            root = DockLayoutManager.LayoutRoot
            Assert.AreEqual(1, root.Items.Count)
            Assert.IsTrue(TypeOf root.Items(0) Is LayoutGroup)
            Assert.AreEqual(5, CType(root.Items(0), LayoutGroup).Items.Count)

            panel1 = TryCast(CType(root.Items(0), LayoutGroup).Items(0), LayoutPanel)
            panel2 = TryCast(CType(root.Items(0), LayoutGroup).Items(1), LayoutPanel)
            panel3 = TryCast(CType(root.Items(0), LayoutGroup).Items(2), LayoutPanel)
            panel4 = TryCast(CType(root.Items(0), LayoutGroup).Items(3), LayoutPanel)
            panel5 = TryCast(CType(root.Items(0), LayoutGroup).Items(3), LayoutPanel)

            Assert.IsNotNull(panel1)
            Assert.IsNotNull(panel2)
            Assert.IsNotNull(panel3)
            Assert.IsNotNull(panel4)
            Assert.IsNotNull(panel5)
        End Sub
        Private Sub DocumentGroupsTest()
            ResetDockLayoutManager()
            Assert.AreEqual(DockLayoutManager.LayoutRoot.Items(0), DocumentGroupsModule.documentContainer)

            Dim dGroup As DocumentGroup = DocumentGroupsModule.documentContainer

            Assert.IsFalse(DocumentGroupsModule.fixedHeader.IsEnabled)
            Assert.IsFalse(DocumentGroupsModule.fixedHeader.IsChecked.Value)

            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.Default)
            DocumentGroupsModule.closeButtonComboBox.SelectedIndex = 1
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.InTabControlHeader)
            DocumentGroupsModule.closeButtonComboBox.SelectedIndex = 2
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.InAllTabPageHeaders)
            DocumentGroupsModule.closeButtonComboBox.SelectedIndex = 3
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.InActiveTabPageHeader)
            DocumentGroupsModule.closeButtonComboBox.SelectedIndex = 4
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.InAllTabPagesAndTabControlHeader)
            DocumentGroupsModule.closeButtonComboBox.SelectedIndex = 5
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.InActiveTabPageAndTabControlHeader)
            DocumentGroupsModule.closeButtonComboBox.SelectedIndex = 6
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.NoWhere)
            DocumentGroupsModule.closeButtonComboBox.SelectedIndex = 0
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(dGroup.ClosePageButtonShowMode, ClosePageButtonShowMode.Default)

            Assert.AreEqual(dGroup.CaptionOrientation, System.Windows.Controls.Orientation.Horizontal)
            DocumentGroupsModule.headerOrientationComboBox.SelectedIndex = 1
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(dGroup.CaptionOrientation, System.Windows.Controls.Orientation.Vertical)
            DocumentGroupsModule.headerOrientationComboBox.SelectedIndex = 0
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(dGroup.CaptionOrientation, System.Windows.Controls.Orientation.Horizontal)

            Assert.AreEqual(dGroup.CaptionLocation, CaptionLocation.Default)
            DocumentGroupsModule.headerLocationComboBox.SelectedIndex = 1
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(dGroup.CaptionLocation, CaptionLocation.Left)
            DocumentGroupsModule.headerLocationComboBox.SelectedIndex = 2
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(dGroup.CaptionLocation, CaptionLocation.Top)
            DocumentGroupsModule.headerLocationComboBox.SelectedIndex = 3
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(dGroup.CaptionLocation, CaptionLocation.Right)
            DocumentGroupsModule.headerLocationComboBox.SelectedIndex = 4
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(dGroup.CaptionLocation, CaptionLocation.Bottom)
            DocumentGroupsModule.headerLocationComboBox.SelectedIndex = 0
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(dGroup.CaptionLocation, CaptionLocation.Default)

            Assert.IsFalse(DocumentGroupsModule.headersAutoFill.IsChecked.Value)
            Assert.IsFalse(dGroup.TabHeadersAutoFill)
            Assert.AreEqual(dGroup.TabHeaderLayoutType, TabHeaderLayoutType.Scroll)
            DocumentGroupsModule.headerLayoutComboBox.SelectedIndex = 1
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(dGroup.TabHeaderLayoutType, TabHeaderLayoutType.Trim)
            DocumentGroupsModule.headerLayoutComboBox.SelectedIndex = 2
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(dGroup.TabHeaderLayoutType, TabHeaderLayoutType.Scroll)
            DocumentGroupsModule.headerLayoutComboBox.SelectedIndex = 3
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(dGroup.TabHeaderLayoutType, TabHeaderLayoutType.MultiLine)
            Assert.IsFalse(DocumentGroupsModule.headersAutoFill.IsChecked.Value)
            Assert.IsFalse(dGroup.TabHeadersAutoFill)
            Assert.IsTrue(DocumentGroupsModule.fixedHeader.IsEnabled)

            DocumentGroupsModule.headersAutoFill.IsChecked = True
            Assert.IsTrue(dGroup.TabHeadersAutoFill)
            UpdateLayoutAndDoEvents()

            DocumentGroupsModule.fixedHeader.IsChecked = True
            Assert.IsTrue(dGroup.FixedMultiLineTabHeaders)
            UpdateLayoutAndDoEvents()

            DocumentGroupsModule.headerLayoutComboBox.SelectedIndex = 0
            UpdateLayoutAndDoEvents()
            Assert.IsTrue(DocumentGroupsModule.headersAutoFill.IsChecked.Value)
            Assert.AreEqual(dGroup.TabHeaderLayoutType, TabHeaderLayoutType.Scroll)
            Assert.IsTrue(dGroup.TabHeadersAutoFill)
        End Sub
    End Class


End Namespace
