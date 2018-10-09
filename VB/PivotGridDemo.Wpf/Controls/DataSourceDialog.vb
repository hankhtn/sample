Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports XmlaMetaGetter = DevExpress.XtraPivotGrid.Data.OLAPMetaGetter
Imports DevExpress.Xpf.Editors
Imports DevExpress.XtraPivotGrid
Imports DevExpress.Xpf.Core

Namespace PivotGridDemo

    Partial Public Class DataSourceDialog
        Inherits Control

        Private Const ConnectionStringName As String = "ConnectionString", CatalogsComboName As String = "CatalogsCombo", CubesComboName As String = "CubesCombo", UserName As String= "UserCombo", PasswordName As String = "PasswordCombo", ConnectButtonName As String = "Connect"

        Public Shared ReadOnly IsCatalogsRetrivingProperty As DependencyProperty
        Public Shared ReadOnly IsCubesRetrivingProperty As DependencyProperty
        Private metaGetter As New XmlaMetaGetter()

        Shared Sub New()
            Dim ownerType As Type = GetType(DataSourceDialog)
            IsCatalogsRetrivingProperty = DependencyProperty.Register("IsCatalogsRetriving", GetType(Boolean), ownerType, New PropertyMetadata(False))
            IsCubesRetrivingProperty = DependencyProperty.Register("IsCubesRetriving", GetType(Boolean), ownerType, New PropertyMetadata(False))
        End Sub

        Public Property IsCatalogsRetriving() As Boolean
            Get
                Return DirectCast(GetValue(IsCatalogsRetrivingProperty), Boolean)
            End Get
            Set(ByVal value As Boolean)
                SetValue(IsCatalogsRetrivingProperty, value)
            End Set
        End Property

        Public Property IsCubesRetriving() As Boolean
            Get
                Return DirectCast(GetValue(IsCubesRetrivingProperty), Boolean)
            End Get
            Set(ByVal value As Boolean)
                SetValue(IsCubesRetrivingProperty, value)
            End Set
        End Property

        Private Property CatalogsCombo() As ComboBoxEdit
        Private Property CubesCombo() As ComboBoxEdit
        Private Property ConnectionString() As TextEdit
        Private Property User() As TextEdit
        Private Property Password() As PasswordBoxEdit
        Private Property ConnectButton() As Button

        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            CatalogsCombo = TryCast(GetTemplateChild(CatalogsComboName), ComboBoxEdit)
            AddHandler CatalogsCombo.EditValueChanged, AddressOf OnCatalogsComboEditValueChanged
            CubesCombo = TryCast(GetTemplateChild(CubesComboName), ComboBoxEdit)
            ConnectionString = TryCast(GetTemplateChild(ConnectionStringName), TextEdit)
            User = TryCast(GetTemplateChild(UserName), TextEdit)
            Password = TryCast(GetTemplateChild(PasswordName), PasswordBoxEdit)
            ConnectButton = TryCast(GetTemplateChild(ConnectButtonName), Button)
            AddHandler ConnectButton.Click, AddressOf Connect
            ApplyPlatformTemplate()
        End Sub

        Private Sub Connect(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ClearCombo(CatalogsCombo)
            ClearCombo(CubesCombo)
            IsCatalogsRetriving = True
            IsCubesRetriving = False
            metaGetter.ConnectionString = GetConnectionStringCore()
            If Not metaGetter.Connected Then
                ShowMessage("Invalid cube.")
                IsCatalogsRetriving = False
                Return
            End If
            RetriveCatalogsAndCubes()
        End Sub

        Private Sub OnCatalogsComboEditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            IsCatalogsRetriving = False
            If IsCatalogEmpty() Then
                IsCubesRetriving = False
                Return
            End If
            IsCubesRetriving = True
            CubesCombo.Clear()
            metaGetter.ConnectionString = GetConnectionStringCore()
            RetriveCubes()
        End Sub

        Private Function CatalogOrCubeEmpty() As Boolean
            If IsCatalogEmpty() Then
                Return True
            End If
            Return CubesCombo.SelectedIndex < 0 OrElse String.IsNullOrEmpty(TryCast(CubesCombo.Items(CubesCombo.SelectedIndex), String))
        End Function
        Private Function IsCatalogEmpty() As Boolean
            Return CatalogsCombo.SelectedIndex < 0 OrElse String.IsNullOrEmpty(TryCast(CatalogsCombo.Items(CatalogsCombo.SelectedIndex), String))
        End Function
        Private Sub ClearCombo(ByVal edit As ComboBoxEdit)
            edit.Items.Clear()
            edit.EditValue = String.Empty
        End Sub

        Private Const CubeEditName As String = "CubeEdit", ServerRadioName As String = "AnalysisServerRadio", TextCubeFileName As String = "TextCubeFile", CubeRadioName As String = "CubeRadio"

        Private Function OpenCubeFile(ByVal fileName As String) As Boolean
            Try
                CubeEdit.Text = fileName
                metaGetter.ConnectionString = "Provider=msolap;Data Source=" & CubeEdit.Text
                RetriveCatalogsAndCubes()
                Return True
            Finally
                metaGetter.Connected = False
            End Try
        End Function

        Private Sub RetriveCatalogsAndCubes()
            If Not InitComboBox(metaGetter.GetCatalogs(), CatalogsCombo) Then
                ShowMessage("There is no catalogs in the cube file.")
            End If
            IsCatalogsRetriving = False
        End Sub

        Private Sub UpdateControls()
            Dim isServer As Boolean = AnalysisServerRadio.IsChecked.Value
            If CubeEdit.Text.Length > 0 Then
                ConnectButton.IsEnabled = True
            End If
            CatalogsCombo.IsEnabled = isServer
            CubesCombo.IsEnabled = isServer
            CubeEdit.Text = String.Empty
            CubeEdit.AllowDefaultButton = Not isServer
            ClearCombo(CatalogsCombo)
            ClearCombo(CubesCombo)
            TextCubeFile.Text = If(isServer, "Server", "Cube File")
        End Sub

        Public Function GetConnectionString() As String
            If CatalogOrCubeEmpty() Then
                Return Nothing
            End If
            Return GetConnectionStringCore() & ";Initial Catalog=" & CStr(CatalogsCombo.EditValue) & ";Cube Name=" & CStr(CubesCombo.EditValue)
        End Function

        Private Function GetConnectionStringCore() As String
            Return "Provider=msolap;Data Source=" & CubeEdit.Text
        End Function

        Private Sub ShowMessage(ByVal message As String)
            DXMessageBox.Show(message, Application.Current.MainWindow.Title, MessageBoxButton.OK, MessageBoxImage.Error)
        End Sub

        Private Function InitComboBox(ByVal items As List(Of String), ByVal edit As ComboBoxEdit) As Boolean
            ClearCombo(edit)
            If items IsNot Nothing AndAlso items.Count > 0 Then
                edit.Items.AddRange(items.ToArray())
                edit.SelectedItem = edit.Items(0)
                Return True
            End If
            Return False
        End Function

        Private Property CubeEdit() As ButtonEdit
        Private Property AnalysisServerRadio() As RadioButton
        Private Property CubeRadio() As RadioButton
        Private Property TextCubeFile() As TextBlock

        Private Sub ApplyPlatformTemplate()
            CubeEdit = TryCast(GetTemplateChild(CubeEditName), ButtonEdit)
            AddHandler CubeEdit.EditValueChanged, AddressOf CubeEdit_EditValueChanged
            ConnectButton.IsEnabled = False
            AnalysisServerRadio = TryCast(GetTemplateChild(ServerRadioName), RadioButton)
            CubeRadio = TryCast(GetTemplateChild(CubeRadioName), RadioButton)
            TextCubeFile = TryCast(GetTemplateChild(TextCubeFileName), TextBlock)
            AddHandler AnalysisServerRadio.Checked, AddressOf AnalysisServerRadio_Checked
            AddHandler CubeRadio.Checked, AddressOf CubeRadio_Checked
            AddHandler CubeEdit.DefaultButtonClick, AddressOf CubeEdit_DefaultButtonClick

        End Sub

        Private Sub CubeEdit_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            If CubeEdit.Text.Length > 0 Then
                ConnectButton.IsEnabled = True
            Else
                ConnectButton.IsEnabled = False
            End If
        End Sub

        Private Sub CubeEdit_DefaultButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim dialog As New Microsoft.Win32.OpenFileDialog()
            dialog.Filter = "Cube files (*.cub)|*.cub"
            If dialog.ShowDialog() = True Then
                OpenCubeFile(dialog.FileName)
            End If
        End Sub

        Private Sub CubeRadio_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateControls()
        End Sub

        Private Sub AnalysisServerRadio_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdateControls()
        End Sub

        Private Sub RetriveCubes()
            If Not InitComboBox(metaGetter.GetCubes(CatalogsCombo.SelectedItem.ToString()), CubesCombo) Then
                ShowMessage("There is no catalogs in the cube file.")
            End If
            IsCubesRetriving = False
        End Sub

    End Class
End Namespace
