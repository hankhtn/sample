Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace ChartsDemo
    Friend Class PerformanceDataSource
        Inherits List(Of PerformanceDataItem)

        #Region "Data"
        Public Sub New()
            Add(New PerformanceDataItem(1, 0.21, 0.22, 0.21, 5, 5, 5))
            Add(New PerformanceDataItem(2, 0.31, 0.11, 0.02, 7, 7, 20))
            Add(New PerformanceDataItem(3, 0.11, 0.21, 0.35, 2, 12, 18))
            Add(New PerformanceDataItem(4, 0.13, 0.25, 0.29, 7, 25, 21))
            Add(New PerformanceDataItem(5, 0.02, 0.10, 0.15, 25, 25, 19))
            Add(New PerformanceDataItem(6, 0.05, 0.11, 0.21, 27, 20, 10))
            Add(New PerformanceDataItem(7, 0.11, 0.15, 0.23, 44, 17, 8))
            Add(New PerformanceDataItem(8, 0.15, 0.20, 0.30, 45, 24, 15))
            Add(New PerformanceDataItem(9, 0.18, 0.25, 0.36, 50, 29, 17))
            Add(New PerformanceDataItem(10, 0.23, 0.12, 0.38, 52, 25, 12))
            Add(New PerformanceDataItem(11, 0.21, 0.08, 0.36, 52, 28, 40))
            Add(New PerformanceDataItem(12, 0.16, 0.08, 0.37, 55, 29, 47))
            Add(New PerformanceDataItem(13, 0.22, 0.27, 0.33, 53, 25, 50))
            Add(New PerformanceDataItem(14, 0.25, 0.29, 0.22, 51, 28, 45))
            Add(New PerformanceDataItem(15, 0.22, 0.31, 0.19, 49, 30, 50))
            Add(New PerformanceDataItem(16, 0.23, 0.34, 0.15, 45, 42, 51))
            Add(New PerformanceDataItem(17, 0.25, 0.40, 0.03, 46, 45, 48))
            Add(New PerformanceDataItem(18, 0.32, 0.54, 0.04, 42, 40, 43))
            Add(New PerformanceDataItem(19, 0.30, 0.51, 0.03, 45, 20, 15))
            Add(New PerformanceDataItem(20, 0.31, 0.45, 0.07, 48, 21, 19))
            Add(New PerformanceDataItem(21, 0.25, 0.40, 0.05, 48, 35, 25))
            Add(New PerformanceDataItem(22, 0.10, 0.43, 0.07, 49, 33, 27))
            Add(New PerformanceDataItem(23, 0.05, 0.45, 0.15, 49, 35, 30))
            Add(New PerformanceDataItem(24, 0.03, 0.44, 0.21, 51, 37, 32))
            Add(New PerformanceDataItem(25, 0.01, 0.42, 0.23, 55, 40, 37))
            Add(New PerformanceDataItem(26, 0.01, 0.45, 0.21, 57, 43, 39))
            Add(New PerformanceDataItem(27, 0.01, 0.43, 0.22, 59, 50, 43))
            Add(New PerformanceDataItem(28, 0.01, 0.39, 0.25, 62, 51, 42))
            Add(New PerformanceDataItem(29, 0.03, 0.27, 0.20, 42, 31, 23))
            Add(New PerformanceDataItem(30, 0.07, 0.25, 0.14, 25, 20, 17))
            Add(New PerformanceDataItem(31, 0.05, 0.12, 0.09, 35, 25, 20))
            Add(New PerformanceDataItem(32, 0.03, 0.10, 0.05, 41, 29, 24))
            Add(New PerformanceDataItem(33, 0.05, 0.08, 0.06, 48, 32, 26))
            Add(New PerformanceDataItem(34, 0.02, 0.09, 0.06, 55, 37, 28))
            Add(New PerformanceDataItem(35, 0.05, 0.11, 0.07, 59, 38, 28))
            Add(New PerformanceDataItem(36, 0.03, 0.13, 0.05, 63, 39, 30))
            Add(New PerformanceDataItem(37, 0.02, 0.15, 0.03, 67, 43, 31))
            Add(New PerformanceDataItem(38, 0.05, 0.12, 0.07, 71, 50, 32))
            Add(New PerformanceDataItem(39, 0.07, 0.16, 0.12, 65, 43, 31))
            Add(New PerformanceDataItem(40, 0.09, 0.25, 0.18, 61, 39, 30))
            Add(New PerformanceDataItem(41, 0.09, 0.23, 0.19, 60, 38, 30))
            Add(New PerformanceDataItem(42, 0.10, 0.25, 0.20, 63, 37, 31))
            Add(New PerformanceDataItem(43, 0.11, 0.22, 0.18, 64, 35, 32))
            Add(New PerformanceDataItem(44, 0.13, 0.31, 0.19, 60, 36, 30))
            Add(New PerformanceDataItem(45, 0.17, 0.33, 0.22, 58, 35, 31))
            Add(New PerformanceDataItem(46, 0.23, 0.30, 0.27, 63, 32, 33))
            Add(New PerformanceDataItem(47, 0.20, 0.25, 0.30, 58, 29, 31))
            Add(New PerformanceDataItem(48, 0.17, 0.23, 0.35, 62, 28, 32))
            Add(New PerformanceDataItem(49, 0.15, 0.25, 0.37, 60, 26, 30))
            Add(New PerformanceDataItem(50, 0.12, 0.22, 0.40, 55, 23, 27))
            Add(New PerformanceDataItem(51, 0.11, 0.20, 0.42, 57, 21, 31))
            Add(New PerformanceDataItem(52, 0.09, 0.18, 0.45, 60, 20, 35))
            Add(New PerformanceDataItem(53, 0.08, 0.17, 0.46, 65, 19, 45))
            Add(New PerformanceDataItem(54, 0.05, 0.10, 0.52, 77, 17, 43))
            Add(New PerformanceDataItem(55, 0.03, 0.12, 0.55, 81, 18, 40))
            Add(New PerformanceDataItem(56, 0.05, 0.09, 0.53, 75, 17, 15))
            Add(New PerformanceDataItem(57, 0.07, 0.12, 0.47, 67, 18, 16))
            Add(New PerformanceDataItem(58, 0.03, 0.09, 0.35, 60, 19, 12))
            Add(New PerformanceDataItem(59, 0.05, 0.12, 0.23, 41, 10, 5))
            Add(New PerformanceDataItem(60, 0.03, 0.07, 0.10, 33, 5, 3))
        End Sub
        #End Region
    End Class
    Friend Class PerformanceDataItem

        Private second_Renamed As Integer

        Private process1CpuUsage_Renamed As Double

        Private process2CpuUsage_Renamed As Double

        Private process3CpuUsage_Renamed As Double

        Private process1Memory_Renamed As Double

        Private process2Memory_Renamed As Double

        Private process3Memory_Renamed As Double

        Public ReadOnly Property Second() As Integer
            Get
                Return second_Renamed
            End Get
        End Property
        Public ReadOnly Property Process1CpuUsage() As Double
            Get
                Return process1CpuUsage_Renamed
            End Get
        End Property
        Public ReadOnly Property Process2CpuUsage() As Double
            Get
                Return process2CpuUsage_Renamed
            End Get
        End Property
        Public ReadOnly Property Process3CpuUsage() As Double
            Get
                Return process3CpuUsage_Renamed
            End Get
        End Property
        Public ReadOnly Property Process1Memory() As Double
            Get
                Return process1Memory_Renamed
            End Get
        End Property
        Public ReadOnly Property Process2Memory() As Double
            Get
                Return process2Memory_Renamed
            End Get
        End Property
        Public ReadOnly Property Process3Memory() As Double
            Get
                Return process3Memory_Renamed
            End Get
        End Property

        Public Sub New(ByVal second As Integer, ByVal process1CpuUsage As Double, ByVal process2CpuUsage As Double, ByVal process3CpuUsage As Double, ByVal process1Memory As Double, ByVal process2Memory As Double, ByVal process3Memory As Double)
            Me.second_Renamed = second
            Me.process1CpuUsage_Renamed = process1CpuUsage
            Me.process2CpuUsage_Renamed = process2CpuUsage
            Me.process3CpuUsage_Renamed = process3CpuUsage
            Me.process1Memory_Renamed = process1Memory
            Me.process2Memory_Renamed = process2Memory
            Me.process3Memory_Renamed = process3Memory
        End Sub
    End Class
End Namespace
