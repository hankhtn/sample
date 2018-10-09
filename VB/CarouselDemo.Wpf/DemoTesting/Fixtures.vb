Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports System.Threading
Imports System.Windows.Threading
Imports DevExpress.Xpf.Core.Native
Imports System
Imports DevExpress.Xpf.Carousel
Imports System.Collections
Imports DevExpress.Xpf.Carousel.Helpers
Imports DevExpress.Xpf.Core
Imports System.Windows.Controls
Imports DevExpress.Xpf.Editors

Namespace CarouselDemo.Tests
    #Region "CarouselDemoModulesAccessor"
    Public Class CarouselDemoModulesAccessor
        Inherits DemoModulesAccessor(Of CarouselDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
        Public ReadOnly Property MovingPathModule() As MovingPath
            Get
                Return CType(DemoModule, MovingPath)
            End Get
        End Property
    End Class
    #End Region
    Public Class CarouselCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function CheckMemoryLeaks(ByVal moduleTyle As Type) As Boolean
            Return True
        End Function
    End Class
    Public MustInherit Class BaseCarouselTestingFixture
        Inherits BaseDemoTestingFixture

        Protected ReadOnly modulesAccessor As CarouselDemoModulesAccessor
        Public Sub New()
            modulesAccessor = New CarouselDemoModulesAccessor(Me)
        End Sub
    End Class
    Public Class CheckDemoOptionsFixture
        Inherits BaseCarouselTestingFixture

        Protected Overrides Sub CreateActions()
            MyBase.CreateActions()
            CheckModule_MovingPath()
        End Sub
        #Region "MovingPath module checking"
        Private Sub CheckModule_MovingPath()
            AddLoadModuleActions(GetType(MovingPath))
            Dispatch(AddressOf CheckDefaultProperties)
            Dispatch(AddressOf CheckPathList)
            Dispatch(AddressOf B152058_CheckPathPadding)
            Dispatch(AddressOf CheckDrawPath)
            Dispatch(AddressOf CheckStretchPath)
        End Sub
        Private Sub CheckDefaultProperties()
            Assert.AreEqual(False, modulesAccessor.MovingPathModule.carousel.PathVisible)
            Assert.AreEqual(PathSizingMode.Stretch, modulesAccessor.MovingPathModule.carousel.PathSizingMode)
        End Sub
        Private Sub CheckPathList()
            Dim index As Integer = 0
            For Each path In DirectCast(modulesAccessor.MovingPathModule.pathList.ItemsSource, IEnumerable)
                PathList_SetIndexAndAssert(index, index = 0)
                index += 1
            Next path
        End Sub
        Private Sub PathList_SetIndexAndAssert(ByVal index As Integer, ByVal equals As Boolean)
            Dim path As Object = modulesAccessor.MovingPathModule.carousel.GetValue(CarouselPanel.ItemMovingPathProperty)
            modulesAccessor.MovingPathModule.pathList.SelectedIndex = index
            Dim newPath As Object = modulesAccessor.MovingPathModule.carousel.GetValue(CarouselPanel.ItemMovingPathProperty)
            Assert.AreEqual(equals, Object.ReferenceEquals(path, newPath))
        End Sub
        Private Sub B152058_CheckPathPadding()
            ChangePathPaddingAndAssert(modulesAccessor.MovingPathModule.paddingTopSlider)
            ChangePathPaddingAndAssert(modulesAccessor.MovingPathModule.paddingLeftSlider)
            ChangePathPaddingAndAssert(modulesAccessor.MovingPathModule.paddingRightSlider)
            ChangePathPaddingAndAssert(modulesAccessor.MovingPathModule.paddingBottomSlider)
        End Sub
        Private Sub ChangePathPaddingAndAssert(ByVal slider As TrackBarEdit)
            Dim renderCount As Integer = CarouselPanelHelper.GetRenderCount(modulesAccessor.MovingPathModule.carousel)
            Dim pathPadding As Object = modulesAccessor.MovingPathModule.carousel.GetValue(CarouselPanel.PathPaddingProperty)
            slider.Value += 5
            UpdateLayoutAndDoEvents()
            Dim newPathPadding As Object = modulesAccessor.MovingPathModule.carousel.GetValue(CarouselPanel.PathPaddingProperty)
            Dim newRenderCount As Integer = CarouselPanelHelper.GetRenderCount(modulesAccessor.MovingPathModule.carousel)
            Assert.IsFalse(Object.ReferenceEquals(pathPadding, newPathPadding))
            Assert.AreEqual(1, newRenderCount - renderCount)
        End Sub
        Private Sub CheckDrawPath()
            modulesAccessor.MovingPathModule.checkBoxPathVisible.IsChecked = True
            Assert.IsTrue(modulesAccessor.MovingPathModule.carousel.PathVisible)
        End Sub
        Private Sub CheckStretchPath()
            modulesAccessor.MovingPathModule.checkBoxPathSizingMode.IsChecked = False
            Assert.AreEqual(PathSizingMode.Proportional, modulesAccessor.MovingPathModule.carousel.PathSizingMode)
        End Sub
        #End Region
    End Class
End Namespace
