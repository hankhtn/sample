using System.Globalization;
using DevExpress.Xpf.SpellChecker;

namespace RichEditDemo {
    public partial class SpellChecking : RichEditDemoModule {
        public SpellChecking() {
            InitializeComponent();
            LoadDictionaries(this.spellChecker.Dictionaries);
        }

        void LoadDictionaries(SpellCheckerDictionaryCollection dictionaries) {
            dictionaries.Add(SpellCheckerHelper.CreateHunspellDictionary(new CultureInfo("en-US")));
            dictionaries.Add(SpellCheckerHelper.CreateHunspellDictionary(new CultureInfo("ru-RU")));
            dictionaries.Add(SpellCheckerHelper.CreateHunspellDictionary(new CultureInfo("de-DE")));
        }
    }
}
