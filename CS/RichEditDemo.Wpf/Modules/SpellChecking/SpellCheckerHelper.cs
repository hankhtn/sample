using System;
using System.Globalization;
using System.IO;
using DevExpress.Utils.Zip;
using DevExpress.XtraSpellChecker;

namespace RichEditDemo {
    public class SpellCheckerHelper {
        public static HunspellDictionary CreateHunspellDictionary(CultureInfo culture) {
            string[] parts = culture.Name.Split('-');
            HunspellDictionary result = new HunspellDictionary();
            Stream zipFileStream = GetFileStream(String.Format("{0}_{1}.zip", parts[0], parts[1]));
            InternalZipFileCollection files = InternalZipArchive.Open(zipFileStream);
            Stream dictionaryStream = GetFileStream(files, ".dic");
            Stream grammarStream = GetFileStream(files, ".aff");
            try {
                result.LoadFromStream(dictionaryStream, grammarStream);
            }
            catch {
            }
            finally {
                dictionaryStream.Dispose();
                grammarStream.Dispose();
                zipFileStream.Dispose();
                DisposeZipFileStreams(files);
            }
            result.Culture = culture;
            return result;
        }
        static void DisposeZipFileStreams(InternalZipFileCollection files) {
            foreach (InternalZipFile file in files)
                file.FileDataStream.Dispose();
        }
        static Stream GetFileStream(InternalZipFileCollection files, string name) {
            Stream stream = files.Find(delegate (InternalZipFile file) {
                return file.FileName.IndexOf(name) >= 0;
            }).FileDataStream;
            try {
                return CreateMemoryStream(stream);
            }
            finally {
                stream.Close();
            }
        }
        static Stream GetFileStream(string relativeUri) {
            return new FileStream(DemoUtils.GetRelativePath(relativeUri), FileMode.Open, FileAccess.Read, FileShare.Read);
        }
        static Stream CreateMemoryStream(Stream stream) {
            MemoryStream result = new MemoryStream();
            for (; ; ) {
                int readedByte = stream.ReadByte();
                if (readedByte < 0)
                    break;
                result.WriteByte((byte)readedByte);
            }
            result.Flush();
            result.Seek(0, SeekOrigin.Begin);
            return result;
        }
    }
}
