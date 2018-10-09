Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Management
Imports System.Windows.Forms

Namespace DevExpress.DevAV.Common.Utils
    Public Class DeviceDetector
        Public Enum ChassisTypes
            Other = 1
            Unknown
            Desktop
            LowProfileDesktop
            PizzaBox
            MiniTower
            Tower
            Portable
            Laptop
            Notebook
            Handheld
            DockingStation
            AllInOne
            SubNotebook
            SpaceSaving
            LunchBox
            MainSystemChassis
            ExpansionChassis
            SubChassis
            BusExpansionChassis
            PeripheralChassis
            StorageChassis
            RackMountChassis
            SealedCasePC
        End Enum
        Private Shared dellModel() As String = { "Venue 8 Pro 5830" }
        Private Shared dellModelKind() As KnonwnHardwareKind = { KnonwnHardwareKind.DellPro8 }
        Private Shared Sub ParseKindDell(ByVal res As HardwareInfo)
            ParseKindCore(res, dellModel, dellModelKind)
        End Sub
        Private Shared Function ParseKindCore(ByVal res As HardwareInfo, ByVal model() As String, ByVal kind() As KnonwnHardwareKind) As Boolean
            Dim i As Integer = Array.IndexOf(Of String)(model, res.Model)
            If i < 0 Then
                Return False
            End If
            res.Kind = kind(i)
            Return True
        End Function
        Private Shared msModel() As String = { "Surface with Windows 8 Pro", "Surface Pro 2", "Surface Pro 3" }
        Private Shared msModelKind() As KnonwnHardwareKind = { KnonwnHardwareKind.SurfacePro, KnonwnHardwareKind.SurfacePro2, KnonwnHardwareKind.SurfacePro3 }
        Private Shared Sub ParseKindMicrosoft(ByVal res As HardwareInfo)
            ParseKindCore(res, msModel, msModelKind)
        End Sub
        Public Enum KnonwnHardwareKind
            Unknown
            SurfacePro
            SurfacePro2
            SurfacePro3
            DellPro8
            DellPro10
        End Enum
        Public Class HardwareInfo
            Public Sub New()
                Kind = KnonwnHardwareKind.Unknown
                Manufacturer = ""
                Model = ""
            End Sub
            Public Property Kind() As KnonwnHardwareKind
            Public Property Manufacturer() As String
            Public Property Model() As String
            Public Overrides Function ToString() As String
                If Kind = KnonwnHardwareKind.Unknown Then
                    Return String.Format("Unknown: {0}/{1}", Manufacturer, Model)
                End If
                Return String.Format("{0}: {1}/{2}", Kind, Manufacturer, Model)
            End Function
        End Class
        Private Shared deviceHardwareInfo As HardwareInfo = Nothing

        Private Shared hasBattery_Renamed? As Boolean = Nothing

        Private Shared chassis_Renamed? As ChassisTypes = Nothing

        Private Shared hasTouchSupport_Renamed? As Boolean = Nothing

        Private Shared isWindows8_Renamed? As Boolean = Nothing
        Public Shared ReadOnly Property IsWindows8() As Boolean
            Get
                If isWindows8_Renamed Is Nothing Then
                    isWindows8_Renamed = DetectWindows8()
                End If
                Return isWindows8_Renamed.Value
            End Get
        End Property

        Public Shared Function DetectHardwareInfo() As HardwareInfo
            If deviceHardwareInfo Is Nothing Then
                deviceHardwareInfo = ParseHardwareInfo()
            End If
            Return deviceHardwareInfo
        End Function
        Private Shared Function DetectWindows8() As Boolean
            Try
                Dim win8version = New Version(6, 2, 9200, 0)

                If Environment.OSVersion.Platform = PlatformID.Win32NT AndAlso Environment.OSVersion.Version >= win8version Then
                    Return True
                End If
            Catch
            End Try
            Return False
        End Function
        Public Shared ReadOnly Property IsTablet() As Boolean
            Get
                If Not IsWindows8 Then
                    Return False
                End If

                If Not HasTouchSupport Then
                    Return False
                End If
                Return HasBattery
            End Get
        End Property
        Public Shared ReadOnly Property IsTabletChassis() As Boolean
            Get
                If Chassis = ChassisTypes.Handheld OrElse Chassis = ChassisTypes.Portable Then
                    Return True
                End If
                Return False
            End Get
        End Property
        Public Shared ReadOnly Property HasTouchSupport() As Boolean
            Get
                If hasTouchSupport_Renamed Is Nothing Then
                    hasTouchSupport_Renamed = CheckTouch()
                End If
                Return hasTouchSupport_Renamed.Value
            End Get
        End Property
        Private Shared Function CheckTouch() As Boolean
            Dim device = System.Windows.Input.Tablet.TabletDevices.Cast(Of System.Windows.Input.TabletDevice)().FirstOrDefault(Function(dev) dev.Type = System.Windows.Input.TabletDeviceType.Touch)
            If device Is Nothing Then
                Return False
            End If
            Return True
        End Function
        Public Shared ReadOnly Property Chassis() As ChassisTypes
            Get
                If chassis_Renamed Is Nothing Then
                    chassis_Renamed = GetCurrentChassisType()
                End If
                Return chassis_Renamed.Value
            End Get
        End Property

        Public Shared Function GetCurrentChassisType() As ChassisTypes
            Try
                Dim systemEnclosures = New ManagementClass("Win32_SystemEnclosure")
                For Each obj As ManagementObject In systemEnclosures.GetInstances()
                    For Each i As Integer In DirectCast(obj("ChassisTypes"), UInt16())
                        If i > 0 AndAlso i < 25 Then
                            Return CType(i, ChassisTypes)
                        End If
                    Next i
                Next obj
            Catch
            End Try
            Return ChassisTypes.Unknown
        End Function
        Public Shared ReadOnly Property HasBattery() As Boolean
            Get
                If hasBattery_Renamed Is Nothing Then
                    hasBattery_Renamed = CheckHasBattery()
                End If
                Return hasBattery_Renamed.Value
            End Get
        End Property

        Private Shared Function CheckHasBattery() As Boolean
            Try
                Dim query = New ObjectQuery("Select * FROM Win32_Battery")
                Dim searcher = New ManagementObjectSearcher(query)

                Dim collection = searcher.Get()
                Return collection.Count > 0
            Catch
            End Try
            Return False
        End Function
        Public Shared Function SuggestHybridDemoParameters(<System.Runtime.InteropServices.Out()> ByRef touchScale As Single, <System.Runtime.InteropServices.Out()> ByRef fontSize As Single) As Boolean
            touchScale = 2F
            fontSize = 11F
            Dim h = DetectHardwareInfo()
            Select Case h.Kind
                Case KnonwnHardwareKind.DellPro8
                    touchScale = 1.5F
                    fontSize = 10
                    Return True
                Case KnonwnHardwareKind.DellPro10, KnonwnHardwareKind.SurfacePro, KnonwnHardwareKind.SurfacePro2, KnonwnHardwareKind.SurfacePro3
                    touchScale = 2.5F
                    fontSize = 8.2F
                    Return True


            End Select
            If Screen.PrimaryScreen.WorkingArea.Width < 1500 OrElse Screen.PrimaryScreen.WorkingArea.Height < 800 Then
                touchScale = 1.5F
                fontSize = 10
            End If
            Return True

        End Function
        Private Shared Function ParseHardwareInfo() As HardwareInfo
            Dim res As New HardwareInfo()
            ParseHardwareInfoCore(res)
            Return res
        End Function
        Private Shared Function ParseHardwareInfoCore(ByVal res As HardwareInfo) As Boolean
            Try
                Dim query = New ObjectQuery("Select * FROM Win32_ComputerSystem")
                Dim searcher = New ManagementObjectSearcher(query)
                Dim collection = searcher.Get()
                For Each c In collection
                    res.Manufacturer = c("Manufacturer").ToString()
                    res.Model = c("Model").ToString()
                Next c
            Catch
                Return False
            End Try
            ParseKind(res)
            Return True

        End Function
        Private Shared Sub ParseKind(ByVal res As HardwareInfo)
            If res.Manufacturer = "Microsoft Corporation" Then
                ParseKindMicrosoft(res)
            End If
            If res.Manufacturer = "DellInc." Then
                ParseKindDell(res)
            End If
        End Sub
    End Class
End Namespace
