using System;
using REghZyIOWrapperV2.Streams;
using REghZyNBTEditor.Utilities;

namespace REghZyNBTEditor.NBT {
    public class NBTTagByteArrayViewModel : NBTBaseViewModel {
        public override NBTType Type => NBTType.Integer;

        private byte[] value;
        public byte[] Value {
            get => this.value;
            set => RaisePropertyChanged(ref this.value, value);
        }

        public NBTTagByteArrayViewModel() {

        }

        public NBTTagByteArrayViewModel(string name) : base(name) {

        }

        public NBTTagByteArrayViewModel(byte[] value) {
            this.value = value;
        }

        public NBTTagByteArrayViewModel(string name, byte[] value) : base(name) {
            this.value = value;
        }

        public override NBTBaseViewModel Copy() {
            return new NBTTagByteArrayViewModel(this.name, ArrayUtils.Copy(this.value));
        }

        public override void Read(IDataInput input) {
            uint size = input.ReadInt();
            this.value = new byte[size];
            input.Read(this.value, 0, (int) size);
        }

        public override void Write(IDataOutput output) {
            output.WriteInt((uint) this.value.Length);
            output.Write(this.value, 0, this.value.Length);
        }
    }
}
