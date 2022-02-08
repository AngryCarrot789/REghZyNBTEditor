using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using REghZyNBTEditor.NBT;

namespace REghZyNBTEditor.Converters {
    public class PrimitiveNBTValuePreviewConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || value == DependencyProperty.UnsetValue) {
                return "WPF-Designer-Value";
            }
            else if (value is string) {
                return value;
            }
            else if (value is byte || value is short || value is int || value is long) {
                return value;
            }
            else if (value is float) {
                return Math.Round((float) value, 2);
            }
            else if (value is double) {
                return Math.Round((double) value, 2);
            }
            else if (value is byte[] arrayB) {
                return $"{arrayB.Length} bytes";
            }
            else if (value is int[] arrayI) {
                return $"{arrayI.Length} integers";
            }
            else if (value is List<NBTBaseViewModel> list) {
                return $"{list.Count} items";
            }
            else if (value is Dictionary<string, NBTBaseViewModel> map) {
                return $"{map.Count} entries";
            }
            else {
                return "ERROR: " + value == null ? "NULL" : value.GetType().ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
