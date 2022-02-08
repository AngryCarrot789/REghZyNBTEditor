using System;
using System.Net.Http;

namespace REghZyNBTEditor.NBT {
    public enum NBTType {
        End =       0,
        Byte =      1,
        Short =     2,
        Int =       3,
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

        public static NBTBaseViewModel CreateNBT(this NBTType type, string name, NBTCollectiveViewModel parent = null) {
            switch (type) {
                case NBTType.End:       return new NBTTagEndViewModel() { Parent = parent };
                case NBTType.Byte:      return new NBTTagByteViewModel(name) { Parent = parent };
                case NBTType.Short:     return new NBTTagShortViewModel(name) { Parent = parent };
                case NBTType.Int:       return new NBTTagIntViewModel(name) { Parent = parent };
                case NBTType.Long:      return new NBTTagLongViewModel(name) { Parent = parent };
                case NBTType.Float:     return new NBTTagFloatViewModel(name) { Parent = parent };
                case NBTType.Double:    return new NBTTagDoubleViewModel(name) { Parent = parent };
                case NBTType.ByteArray: return new NBTTagByteArrayViewModel(name) { Parent = parent };
                case NBTType.String:    return new NBTTagStringViewModel(name) { Parent = parent };
                case NBTType.List:      return new NBTTagListViewModel(name) { Parent = parent };
                case NBTType.Compound:  return new NBTTagCompoundViewModel(name) { Parent = parent };
                case NBTType.IntArray:  return new NBTTagIntArrayViewModel(name) { Parent = parent };
                default: throw new Exception("Unknown NBT type: " + type);
            }
        }

        public static string GetTagName(this NBTType type) {
            switch (type) {
                case NBTType.End:       return "TAG_End";
                case NBTType.Byte:      return "TAG_Byte";
                case NBTType.Short:     return "TAG_Short";
                case NBTType.Int:       return "TAG_Int";
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
