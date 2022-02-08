using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using REghZyNBTEditor.NBT;

namespace REghZyNBTEditor.Converters {
    public class NBTTypeToIconConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == DependencyProperty.UnsetValue || value == null) {
                return "[WPF-Unknown]";
            }
            else if (value is NBTType type) {
                switch (type) {
                    case NBTType.End:
                        return "E";
                    case NBTType.Byte:
                        return "B";
                    case NBTType.Short:
                        return "S";
                    case NBTType.Integer:
                        return "I";
                    case NBTType.Long:
                        return "L";
                    case NBTType.Float:
                        return "F";
                    case NBTType.Double:
                        return "D";
                    case NBTType.ByteArray:
                        return "B[]";
                    case NBTType.String:
                        return "'S'";
                    case NBTType.List:
                        return "<>";
                    case NBTType.Compound:
                        return "{...}";
                    case NBTType.IntArray:
                        return "I[]";
                    default:
                        throw new Exception("Unknown NBT type: " + type);
                }
            }
            else {
                throw new Exception("Object is not an instance of NBTType: " + value);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == DependencyProperty.UnsetValue || value == null || value.Equals("")) {
                return NBTType.End;
            }
            else if (value is string type) {
                switch (type) {
                    case "E":
                        return NBTType.End;
                    case "B":
                        return NBTType.Byte;
                    case "S":
                        return NBTType.Short;
                    case "I":
                        return NBTType.Integer;
                    case "L":
                        return NBTType.Long;
                    case "F":
                        return NBTType.Float;
                    case "D":
                        return NBTType.Double;
                    case "B[]":
                        return NBTType.ByteArray;
                    case "'S'":
                        return NBTType.String;
                    case "<>":
                        return NBTType.List;
                    case "{...}":
                        return NBTType.Compound;
                    case "I[]":
                        return NBTType.IntArray;
                    default:
                        throw new Exception("Unknown NBT type: " + type);
                }
            }
            else {
                throw new Exception("Object is not an instance of NBTType: " + value);
            }
        }
    }
}
