Imports System.Globalization
Imports DevExpress.Xpf.SpellChecker

Namespace RichEditDemo
    Partial Public Class SpellChecking
        Inherits RichEditDemoModule

        Public Sub New()
            InitializeComponent()
            LoadDictionaries(Me.spellChecker.Dictionaries)
        End Sub

        Private Sub LoadDictionaries(ByVal dictionaries As SpellCheckerDictionaryCollection)
            dictionaries.Add(SpellCheckerHelper.CreateHunspellDictionary(New CultureInfo("en-US")))
            dictionaries.Add(SpellCheckerHelper.CreateHunspellDictionary(New CultureInfo("ru-RU")))
            dictionaries.Add(SpellCheckerHelper.CreateHunspellDictionary(New CultureInfo("de-DE")))
        End Sub
    End Class
End Namespace
