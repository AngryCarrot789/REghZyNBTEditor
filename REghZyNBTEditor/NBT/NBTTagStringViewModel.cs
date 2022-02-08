using REghZyIOWrapperV2.Streams;

namespace REghZyNBTEditor.NBT {
    public class NBTTagStringViewModel : NBTBaseViewModel {
        public override NBTType Type => NBTType.String;

        private string value;
        public string Value {
            get => this.value;
            set => RaisePropertyChanged(ref this.value, value);
        }

        public NBTTagStringViewModel() {

        }

        public NBTTagStringViewModel(string name) : base(name) {

        }

        public NBTTagStringViewModel(string name, string value) : base(name) {
            this.value = value;
        }

        protected override NBTBaseViewModel CopyInternal() {
            return new NBTTagStringViewModel(this.name, this.value);
        }

        public override void Read(IDataInput input) {
            int len = input.ReadShort();
            this.value = input.ReadStringUTF8(len);
        }

        public override void Write(IDataOutput output) {
            output.WriteShort((ushort) this.value.Length);
            output.WriteStringUTF8(this.value);
        }
    }
}
