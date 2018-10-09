using DevExpress.Xpf.DemoBase;
using System.Collections.Generic;
using System.Data.SQLite;

namespace GridDemo {
    public class EntityFrameworkInstantFeedbackModeViewModel : InstantFeedbackModeViewModelBase<OutlookDataRecord> {
        #region DatabaseInfo
        public static readonly DatabaseInfo DatabaseInfo = new DatabaseInfo(
            OutlookDataContext.FileName, "OutlookDataRecords", typeof(OutlookDataRecord), CreateValues, (sql, connection) => new SQLiteCommand(sql, (SQLiteConnection)connection));
        static Dictionary<string, object> CreateValues() {
            var subject = OutlookDataGenerator.GetSubject();
            var hasAttachment = OutlookDataGenerator.GetHasAttachment();
            var size = OutlookDataGenerator.GetSize(hasAttachment);
            var from = OutlookDataGenerator.GetFrom();
            var sent = OutlookDataGenerator.GetSentDate();
            var dict = new Dictionary<string, object>();
            dict.Add("Subject", subject);
            dict.Add("HasAttachment", hasAttachment);
            dict.Add("Size", size);
            dict.Add("From", from);
            dict.Add("Sent", sent);
            return dict;
        }
        #endregion

        protected EntityFrameworkInstantFeedbackModeViewModel()
            : base(DatabaseInfo, () => new OutlookDataContext().OutlookDataRecords) {
        }
    }
}
