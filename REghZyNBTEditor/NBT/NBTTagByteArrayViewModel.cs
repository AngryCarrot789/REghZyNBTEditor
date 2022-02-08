using System;
using REghZyIOWrapperV2.Streams;
using REghZyNBTEditor.Utilities;

namespace REghZyNBTEditor.NBT {
    public class NBTTagByteArrayViewModel : NBTBaseViewModel {
        public override NBTType Type => NBTType.ByteArray;

        private byte[] data;
        public byte[] Data {
            get => this.data;
            set => RaisePropertyChanged(ref this.data, value);
        }

        public NBTTagByteArrayViewModel() {

        }

        public NBTTagByteArrayViewModel(string name) : base(name) {

        }

        public NBTTagByteArrayViewModel(byte[] value) {
            this.data = value;
        }

        public NBTTagByteArrayViewModel(string name, byte[] value) : base(name) {
            this.data = value;
        }

        protected override NBTBaseViewModel CopyInternal() {
            return new NBTTagByteArrayViewModel(this.name, ArrayUtils.Copy(this.data));
        }

        public override void Read(IDataInput input) {
            uint size = input.ReadInt();
            this.data = new byte[size];
            input.Read(this.data, 0, (int) size);
        }

        public override void Write(IDataOutput output) {
            output.WriteInt((uint) this.data.Length);
            output.Write(this.data, 0, this.data.Length);
        }
    }
}
