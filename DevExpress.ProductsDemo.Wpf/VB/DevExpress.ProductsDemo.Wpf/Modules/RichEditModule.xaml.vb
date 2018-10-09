Imports System
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraSpellChecker
Imports DevExpress.Xpf.Ribbon
Imports DevExpress.Xpf.SpellChecker
Imports System.Windows.Controls
Imports System.Globalization
Imports DevExpress.XtraSpellChecker.Native
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.XtraRichEdit.SpellChecker
Imports System.Collections.Generic
Imports System.Data
Imports System.Windows
Imports System.Windows.Markup
Imports System.Windows.Data
Imports System.Linq
Imports DevExpress.Xpf.Grid

Namespace ProductsDemo.Modules
    Partial Public Class RichEditModule
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub richEdit_StartHeaderFooterEditing(ByVal sender As Object, ByVal e As HeaderFooterEditingEventArgs)
            catHeaderFooterTools.IsVisible = True
            ribbonControl.SelectedPage = pageHeaderFooterToolsInsert
        End Sub

        Private Sub richEdit_FinishHeaderFooterEditing(ByVal sender As Object, ByVal e As HeaderFooterEditingEventArgs)
            catHeaderFooterTools.IsVisible = False
        End Sub

        Private Sub richEdit_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim isSelectionInTable As Boolean = richEdit.IsSelectionInTable()
            If catTableTools.IsVisible <> isSelectionInTable Then
                catTableTools.IsVisible = isSelectionInTable
                If isSelectionInTable Then
                    ribbonControl.SelectedPage = pageTableToolsDesign
                End If
            End If

            Dim isSelectionInFloatingObject As Boolean = richEdit.IsFloatingObjectSelected
            If catPictureTools.IsVisible <> isSelectionInFloatingObject Then
                catPictureTools.IsVisible = isSelectionInFloatingObject
                If isSelectionInFloatingObject Then
                    ribbonControl.SelectedPage = pagePictureToolsFormat
                End If
            End If
        End Sub
    End Class
End Namespace
