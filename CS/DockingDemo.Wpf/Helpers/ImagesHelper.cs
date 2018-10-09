using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DockingDemo.Helpers {
    public static class ImagesHelper {

        public static IEnumerable<ImageSource> NaturePhotos { get; private set; }
        public static ImageSource NaturePhoto1 { get { return NaturePhotos.ElementAt(0); } }
        public static ImageSource NaturePhoto2 { get { return NaturePhotos.ElementAt(1); } }
        public static ImageSource NaturePhoto3 { get { return NaturePhotos.ElementAt(2); } }
        public static ImageSource NaturePhoto4 { get { return NaturePhotos.ElementAt(3); } }
        public static ImageSource NaturePhoto5 { get { return NaturePhotos.ElementAt(4); } }
        public static ImageSource NaturePhoto6 { get { return NaturePhotos.ElementAt(5); } }
        public static ImageSource NaturePhoto7 { get { return NaturePhotos.ElementAt(6); } }
        public static ImageSource NaturePhoto8 { get { return NaturePhotos.ElementAt(7); } }
        public static ImageSource NaturePhoto9 { get { return NaturePhotos.ElementAt(8); } }
        public static ImageSource NaturePhoto10 { get { return NaturePhotos.ElementAt(9); } }
        public static ImageSource GetRandomNaturePhoto() {
            return NaturePhotos.ElementAt(rnd.Next(NaturePhotos.Count() - 1));
        }

        static ImagesHelper() {
            string natureUriPath = @"/DockingDemo;component/Images/Photos/Nature/";
            Func<string, ImageSource> getNatureImage = x => new BitmapImage(new Uri(natureUriPath + x, UriKind.Relative));
            var naturePhotos = new List<ImageSource>();
            naturePhotos.Add(getNatureImage("01.JPG"));
            naturePhotos.Add(getNatureImage("02.JPG"));
            naturePhotos.Add(getNatureImage("03.JPG"));
            naturePhotos.Add(getNatureImage("04.JPG"));
            naturePhotos.Add(getNatureImage("05.JPG"));
            naturePhotos.Add(getNatureImage("06.JPG"));
            naturePhotos.Add(getNatureImage("07.JPG"));
            naturePhotos.Add(getNatureImage("08.JPG"));
            naturePhotos.Add(getNatureImage("09.JPG"));
            naturePhotos.Add(getNatureImage("10.JPG"));
            NaturePhotos = naturePhotos;
        }
        static readonly Random rnd = new Random();
    }
}
