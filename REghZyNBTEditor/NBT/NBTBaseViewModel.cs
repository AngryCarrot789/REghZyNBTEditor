using System;
using REghZyFramework.Utilities;
using REghZyIOWrapperV2.Streams;

namespace REghZyNBTEditor.NBT {
    public abstract class NBTBaseViewModel : BaseViewModel {
        protected string name;

        /// <summary>
        /// The NBT type
        /// </summary>
        public abstract NBTType Type { get; }

        /// <summary>
        /// Name of this NBT
        /// </summary>
        public string Name {
            get => this.name;
            set => this.RaisePropertyChanged(ref this.name, value);
        }

        public NBTBaseViewModel() {

        }

        public NBTBaseViewModel(string name) {
            this.name = name;
        }

        public static NBTBaseViewModel ReadNBT(IDataInput input) {
            NBTType type = (NBTType) input.ReadByte();
            if (!type.IsValid()) {
                throw new Exception("Invalid NBT with ID: " + type);
            }
            else if (type == NBTType.End) {
                return new NBTTagEndViewModel();
            }
            else {
                string name = input.ReadStringUTF8(input.ReadShort());
                NBTBaseViewModel nbt = type.CreateNBT(name);
                nbt.Read(input);
                return nbt;
            }
        }

        public static void WriteNBT(NBTBaseViewModel nbt, IDataOutput output) {
            output.WriteEnum8(nbt.Type);
            if (nbt.Type != NBTType.End) {
                output.WriteShort((ushort) nbt.Name.Length);
                output.WriteStringUTF8(nbt.Name);
                nbt.Write(output);
            }
        }

        public abstract void Write(IDataOutput output);

        public abstract void Read(IDataInput input);

        public byte GetId() {
            return (byte) this.Type;
        }

        public abstract NBTBaseViewModel Copy();
    }
}
