using REghZyIOWrapperV2.Streams;

namespace REghZyNBTEditor.NBT {
    public class NBTTagByteViewModel : NBTBaseViewModel {
        public override NBTType Type => NBTType.Byte;

        private byte value;
        public byte Value {
            get => this.value;
            set => RaisePropertyChanged(ref this.value, value);
        }

        public NBTTagByteViewModel() {

        }

        public NBTTagByteViewModel(string name) : base(name) {

        }

        public NBTTagByteViewModel(byte value) {
            this.value = value;
        }

        public NBTTagByteViewModel(string name, byte value) : base(name) {
            this.value = value;
        }


        protected override NBTBaseViewModel CopyInternal() {
            return new NBTTagByteViewModel(this.name, this.value);
        }

        public override void Read(IDataInput input) {
            this.value = input.ReadByte();
        }

        public override void Write(IDataOutput output) {
            output.WriteByte(this.value);
        }
    }
}
