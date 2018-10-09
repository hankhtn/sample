Imports DevExpress.Xpf.Ribbon
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.ComponentModel
Imports DevExpress.Xpf.Core
Imports System.Windows.Data
Imports DevExpress.Xpf.Bars

Namespace DevExpress.DevAV.Common.View
    Public Class ThemeSelectorBehavior
        Inherits RibbonGalleryItemThemeSelectorBehavior

        Protected Overrides Function CreateCollectionView() As ICollectionView
            Dim view As ICollectionView = CollectionViewSource.GetDefaultView(Theme.Themes.Where(Function(t) t.ShowInThemeSelector AndAlso (Not t.Name.EndsWith(ThemeManager.TouchDelimiterAndDefinition, StringComparison.InvariantCultureIgnoreCase)) AndAlso (Not t.Name.StartsWith("Office2013", StringComparison.InvariantCultureIgnoreCase)) AndAlso Not(t.Name.StartsWith("Office2016", StringComparison.InvariantCultureIgnoreCase) AndAlso (Not t.Name.EndsWith("SE", StringComparison.InvariantCulture)))).Select(Function(t) New ThemeViewModel(t)).ToArray())
            view.GroupDescriptions.Add(New PropertyGroupDescription("Theme.Category"))
            Return view
        End Function
    End Class
End Namespace
