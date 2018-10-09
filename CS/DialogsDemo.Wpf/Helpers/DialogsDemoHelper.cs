using DevExpress.Internal;
using System.IO;

namespace DialogsDemo.Helpers {
    public static class DialogsDemoHelper {
        public static string GetDialogsDataPath(string relativePath) {
            return Path.GetFullPath(DataDirectoryHelper.GetFile("Dialogs\\" + relativePath, DataDirectoryHelper.DataFolderName));
        }
        public static string GetPhotosPath(string relativePath) {
            return Path.GetFullPath(DataDirectoryHelper.GetFile("Photos\\" + relativePath, DataDirectoryHelper.DataFolderName));
        }
        public static Stream GetDataStream(string dataFileName) {
            string path = GetDialogsDataPath(dataFileName);
            return File.OpenRead(path);
        }
    }
}
