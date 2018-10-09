using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Data;

namespace WindowsUIDemo {
    public class OrderNameConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            if(values.Length != 2)
                return null;
            if(!(values[1] is ColumnSortOrder))
                return null;
            ColumnSortOrder order = (ColumnSortOrder)values[1];
            if(values[0].ToString() == "Received") {
                switch(order) {
                    case ColumnSortOrder.Ascending:
                        return "Oldest";
                    case ColumnSortOrder.Descending:
                        return "Newest";
                    default:
                        return null;
                }
            }
            switch(order) {
                case ColumnSortOrder.Ascending:
                    return "Ascending";
                case ColumnSortOrder.Descending:
                    return "Descending";
                default:
                    return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class MessageToFillConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var message = value as Message;
            if(message == null) return null;
            if (message.Employee.Image == null) {
                string name = message.From;
                if(string.IsNullOrEmpty(name))
                    return Colors.Black;
                byte color = (byte)(Math.Abs(name.GetHashCode()) % 5);
                switch(color) {
                    case 0:
                        return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF31669C"));
                    case 1:
                        return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF598A7A"));
                    case 2:
                        return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCCA464"));
                    case 3:
                        return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE06B4C"));
                    case 4:
                        return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF558AC0"));
                    default:
                        return new SolidColorBrush(Colors.Black);
                }
            } else {
                byte[] imageData = message.Employee.Image as byte[];
                if(imageData == null)
                    return null;
                BitmapImage biImg = new BitmapImage();
                MemoryStream ms = new MemoryStream(imageData);
                biImg.BeginInit();
                biImg.StreamSource = ms;
                biImg.EndInit();
                ImageSource imgSrc = biImg as ImageSource;

                return new ImageBrush(imgSrc);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class NameToInitialsConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string name = value as string;
            if(string.IsNullOrEmpty(name))
                return "JJ";
            int idx = name.IndexOf(' ');
            if(idx < 0 || name.Length <= idx + 1 || name[idx + 1] == '(')
                return name[0].ToString().ToUpper();
            return (name[0].ToString() + name[idx + 1].ToString()).ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class PriorityGroupValueConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is int) {
                int priority = (int)value;
                switch(priority) {
                    case 0:
                        return "Low priority";
                    case 1:
                        return "Normal priority";
                    case 2:
                        return "Hight priority";
                    default:
                        return null;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class AttachmentGroupValueConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is bool) {
                bool attachment = (bool)value;
                return attachment ? "Has Attachment" : "Hasn't Attachment";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class ByteImageConverter : IValueConverter {

        public int DecodePixelHeight { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            byte[] imageData = value as byte[];
            if(imageData == null)
                return null;
            BitmapImage biImg = new BitmapImage();
            RenderOptions.SetBitmapScalingMode(biImg, BitmapScalingMode.HighQuality);
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.DecodePixelHeight = DecodePixelHeight;
            biImg.StreamSource = ms;
            biImg.EndInit();
            ImageSource imgSrc = biImg as ImageSource;
            return imgSrc;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class NameToColorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string name = value as string;
            if(string.IsNullOrEmpty(name))
                return Colors.Black;
            byte color = (byte)(Math.Abs(name.GetHashCode()) % 5);
            switch(color) {
                case 0:
                    return (Color)ColorConverter.ConvertFromString("#FF31669C");
                case 1:
                    return (Color)ColorConverter.ConvertFromString("#FF598A7A");
                case 2:
                    return (Color)ColorConverter.ConvertFromString("#FFCCA464");
                case 3:
                    return (Color)ColorConverter.ConvertFromString("#FFE06B4C");
                case 4:
                    return (Color)ColorConverter.ConvertFromString("#FF558AC0");
                default:
                    return Colors.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

}
