using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GridDemo {
    public abstract class LargeDataSourceObjectBase {
        protected class ValuesContainer<T> {
            Dictionary<int, T> modifiedValues;
            readonly Func<int, T> getDefaultValue;
            public ValuesContainer(Func<int, T> getDefaultValue) {
                this.getDefaultValue = getDefaultValue;
            }
            public T GetValue(int index) {
                if(modifiedValues == null)
                    return getDefaultValue(index);
                T value;
                if(modifiedValues.TryGetValue(index, out value))
                    return value;
                return getDefaultValue(index);
            }
            public void SetValue(int index, T value) {
                if(modifiedValues == null)
                    modifiedValues = new Dictionary<int, T>();
                modifiedValues[index] = value;
            }
        } 
        static readonly Priority[] Priorities = Enum.GetValues(typeof(Priority)).Cast<Priority>().ToArray();
        static LargeDataSourceObjectBase() {
            
        }

        static void GenerateCode() {
            var sb = new System.Text.StringBuilder();
            for(double i = 0; i < 1000d / 7d; i++) {
                var startIndex = i * 7 + 1;
                sb.AppendFormat(
@"
        [Display(Name = ""From ({0})"", Order = {0})]
        public string From{0} {{ get {{ return fromValues.GetValue({0}); }} set {{ fromValues.SetValue({0}, value); }} }}
        [Display(Name = ""To ({1})"", Order = {1})]
        public string To{1} {{ get {{ return toValues.GetValue({1}); }} set {{ toValues.SetValue({1}, value); }} }}
        [Display(Name = ""Sent ({2})"", Order = {2})]
        public DateTime Sent{2} {{ get {{ return sentValues.GetValue({2}); }} set {{ sentValues.SetValue({2}, value); }} }}
        [Display(Name = ""Has Attachment ({3})"", Order = {3})]
        public bool HasAttachment{3} {{ get {{ return hasAttachmentValues.GetValue({3}); }} set {{ hasAttachmentValues.SetValue({3}, value); }} }}
        [Display(Name = ""Size ({4})"", Order = {4})]
        public int Size{4} {{ get {{ return sizeValues.GetValue({4}); }} set {{ sizeValues.SetValue({4}, value); }} }}
        [Display(Name = ""Priority ({5})"", Order = {5}), GridEditor(TemplateKey = ""priorityColumn"")] 
        public Priority Priority{5} {{ get {{ return priorityValues.GetValue({5}); }} set {{ priorityValues.SetValue({5}, value); }} }}
        [Display(Name = ""Subject ({6})"", Order = {6}), GridEditor(TemplateKey = ""subjectEditor"")]
        public string Subject{6} {{ get {{ return subjectValues.GetValue({6}); }} set {{ subjectValues.SetValue({6}, value); }} }}
", startIndex, startIndex + 1, startIndex + 2, startIndex + 3, startIndex + 4, startIndex + 5, startIndex + 6);
            }
        }

        const int BaseColumnCount = 7;
        protected readonly ValuesContainer<string> fromValues;
        protected readonly ValuesContainer<string> toValues;
        protected readonly ValuesContainer<DateTime> sentValues;
        protected readonly ValuesContainer<bool> hasAttachmentValues;
        protected readonly ValuesContainer<int> sizeValues;
        protected readonly ValuesContainer<Priority> priorityValues;
        protected readonly ValuesContainer<string> subjectValues;
        public LargeDataSourceObjectBase(int id) {
            Id = id;
            fromValues = new ValuesContainer<string>(index => OutlookData.Users[GetPseudoRandomValue(Id, index, OutlookData.Users.Length)].Name);
            toValues = new ValuesContainer<string>(index => OutlookData.Users[GetPseudoRandomValue(Id, index + 5, OutlookData.Users.Length)].Name);
            sentValues = new ValuesContainer<DateTime>(index => DateTime.Today.AddDays(GetPseudoRandomValue(Id, index, 30)));
            hasAttachmentValues = new ValuesContainer<bool>(index => GetPseudoRandomValue(Id, index, 2) == 0 ? true : false);
            sizeValues = new ValuesContainer<int>(index => GetPseudoRandomValue(Id, index, 10000));
            priorityValues = new ValuesContainer<Priority>(index => Priorities[GetPseudoRandomValue(Id, index, Priorities.Length)]);
            subjectValues = new ValuesContainer<string>(index => OutlookDataGenerator.Subjects[GetPseudoRandomValue(Id, index, OutlookDataGenerator.Subjects.Length)]);
        }
        [Display(Name = "Id (0)", Order = 0)]
        public int Id { get; private set; }

        int GetPseudoRandomValue(int rowIndex, int columnIndex, int maxValue) {
            return (rowIndex + columnIndex) % maxValue;
        }
    }
}
