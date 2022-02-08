using System;
using System.Net.Http;

namespace REghZyNBTEditor.NBT {
    public enum NBTType {
        End =       0,
        Byte =      1,
        Short =     2,
        Integer =   3,
        Long =      4,
        Float =     5,
        Double =    6,
        ByteArray = 7,
        String =    8,
        List =      9,
        Compound =  10,
        IntArray =  11
    }

    public static class NBTTypeHelper {
        public static bool IsValid(this NBTType nbt) {
            int value = (int) nbt;
            return value >= 0 && value <= 11;
        }

        public static NBTBaseViewModel CreateNBT(this NBTType type, string name) {
            switch (type) {
                case NBTType.End:       return new NBTTagEndViewModel();
                case NBTType.Byte:      return new NBTTagByteViewModel(name);
                case NBTType.Short:     return new NBTTagShortViewModel(name);
                case NBTType.Integer:   return new NBTTagIntViewModel(name);
                case NBTType.Long:      return new NBTTagLongViewModel(name);
                case NBTType.Float:     return new NBTTagFloatViewModel(name);
                case NBTType.Double:    return new NBTTagDoubleViewModel(name);
                case NBTType.ByteArray: return new NBTTagByteArrayViewModel(name);
                case NBTType.String:    return new NBTTagStringViewModel(name);
                case NBTType.List:      return new NBTTagListViewModel(name);
                case NBTType.Compound:  return null; // return new NBTTagEndViewModel();
                case NBTType.IntArray:  return new NBTTagIntArrayViewModel(name);
                default: throw new Exception("Unknown NBT type: " + type);
            }
        }

        public static string GetTagName(this NBTType type) {
            switch (type) {
                case NBTType.End:       return "TAG_End";
                case NBTType.Byte:      return "TAG_Byte";
                case NBTType.Short:     return "TAG_Short";
                case NBTType.Integer:   return "TAG_Int";
                case NBTType.Long:      return "TAG_Long";
                case NBTType.Float:     return "TAG_Float";
                case NBTType.Double:    return "TAG_Double";
                case NBTType.ByteArray: return "TAG_Byte_Array";
                case NBTType.String:    return "TAG_String";
                case NBTType.List:      return "TAG_List";
                case NBTType.Compound:  return "TAG_Compound";
                case NBTType.IntArray:  return "TAG_Int_Array";
                default: throw new Exception("Unknown NBT type: " + type);
            }
        }
    }
}
