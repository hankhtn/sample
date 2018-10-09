using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ChartsDemo {
    [XmlRoot("Comments")]
    public class CommentsData : List<CommentsInfo> {
        static List<CommentsInfo> dataSource = null;
        public static List<CommentsInfo> DataSource {
            get {
                if(dataSource == null) {
                    var s = new XmlSerializer(typeof(CommentsData));
                    dataSource = (List<CommentsInfo>)s.Deserialize(DataLoader.LoadFromResources("/Data/CommentsData.xml"));
                }
                return dataSource;
            }
        }
    }
    public class CommentsInfo {
        public string Category { get; set; }
        public List<CommentInfo> Items { get; set; }
    }
    public class CommentInfo {
        public DateTime Date { get; set; }
        public int CommentCount { get; set; }
    }
}
