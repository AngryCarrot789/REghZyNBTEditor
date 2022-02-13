using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using REghZyNBTEditor.NBT;

namespace REghZyNBTEditor.Converters {
    public class NBTCollectionConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || value == DependencyProperty.UnsetValue) {
                return null;
            }
            else if (value is NBTTagCompoundViewModel compound) {
                return compound.Map.Values;
            }
            else if (value is NBTTagListViewModel list) {
                return list.Data;
            }
            else {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
