using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using REghZyNBTEditor.NBT;

namespace REghZyNBTEditor.Converters {
    public class NBTTypeToResourceLocationConverter : IValueConverter {
        public const string LOCATION = "/REghZyNBTEditor;component/Assets/";

        public static bool IS_DEBUG = false;

        static NBTTypeToResourceLocationConverter() {
#if DEBUG
            IS_DEBUG = true;
#endif
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == DependencyProperty.UnsetValue || value == null) {
                return "[WPF-Unknown]";
            }
            else if (value is NBTType type) {
                switch (type) {
                    case NBTType.End:
                        return LOCATION + "tagEnd.png";
                    case NBTType.Byte:
                        return LOCATION + "tagInt8.png";
                    case NBTType.Short:
                        return LOCATION + "tagInt16.png";
                    case NBTType.Int:
                        return LOCATION + "tagInt32.png";
                    case NBTType.Long:
                        return LOCATION + "tagInt64.png";
                    case NBTType.Float:
                        return LOCATION + "tagFloat32.png";
                    case NBTType.Double:
                        return LOCATION + "tagDouble64.png";
                    case NBTType.ByteArray:
                        return LOCATION + "tagArrayInt8.png";
                    case NBTType.String:
                        return LOCATION + "tagString.png";
                    case NBTType.List:
                        return LOCATION + "tagList.png";
                    case NBTType.Compound:
                        return LOCATION + "tagCompound.png";
                    case NBTType.IntArray:
                        return LOCATION + "tagArrayInt32.png";
                    default:
                        if (IS_DEBUG) {
                            return LOCATION + "tagEnd.png";
                        }

                        throw new Exception("Unknown NBT type: " + type);
                }

                // switch (type) {
                //     case NBTType.End:
                //         return LOCATION + "tagEnd.png";
                //     case NBTType.Byte:
                //         return LOCATION + "tagB8.png";
                //     case NBTType.Short:
                //         return LOCATION + "tagS16.png";
                //     case NBTType.Int:
                //         return LOCATION + "tagI32.png";
                //     case NBTType.Long:
                //         return LOCATION + "tagL64.png";
                //     case NBTType.Float:
                //         return LOCATION + "tagFloat.png";
                //     case NBTType.Double:
                //         return LOCATION + "tagDouble.png";
                //     case NBTType.ByteArray:
                //         return LOCATION + "tagBArray.png";
                //     case NBTType.String:
                //         return LOCATION + "tagString.png";
                //     case NBTType.List:
                //         return LOCATION + "tagList.png";
                //     case NBTType.Compound:
                //         return LOCATION + "tagCompound.png";
                //     case NBTType.IntArray:
                //         return LOCATION + "tagIArray.png";
                //     default:
                //         if (IS_DEBUG) {
                //             return LOCATION + "tagEnd.png";
                //         }
                // 
                //         throw new Exception("Unknown NBT type: " + type);
                // }
            }
            else {
                if (IS_DEBUG) {
                    return "Debug-NotInstance: " + value == null ? "(null)" : value.GetType().ToString();
                }

                throw new Exception("Object is not an instance of NBTType: " + value);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException("Cannot convert back from NBT image location... yet");
        }
    }
}
