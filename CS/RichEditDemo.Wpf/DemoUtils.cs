using System.IO;
using DevExpress.DemoData.Helpers;

namespace RichEditDemo {
    public class DemoUtils {
        public static string GetRelativePath(string name) {
            name = "Data\\" + name;
            string path = DataFilesHelper.DataDirectory;
            string s = "\\";
            for (int i = 0; i <= 10; i++) {
                if (File.Exists(path + s + name))
                    return Path.GetFullPath(path + s + name);
                else
                    s += "..\\";
            }
            return "";
        }
    }
}
