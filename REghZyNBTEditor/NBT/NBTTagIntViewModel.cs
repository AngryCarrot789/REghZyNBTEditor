using REghZyIOWrapperV2.Streams;

namespace REghZyNBTEditor.NBT {
    public class NBTTagIntViewModel : NBTBaseViewModel {
        public override NBTType Type => NBTType.Integer;

        private int value;
        public int Value {
            get => this.value;
            set => RaisePropertyChanged(ref this.value, value);
        }

        public NBTTagIntViewModel() {

        }

        public NBTTagIntViewModel(string name) : base(name) {

        }

        public NBTTagIntViewModel(int value) {
            this.value = value;
        }

        public NBTTagIntViewModel(string name, int value) : base(name) {
            this.value = value;
        }

        public override NBTBaseViewModel Copy() {
            return new NBTTagIntViewModel(this.name, this.value);
        }

        public override void Read(IDataInput input) {
            this.value = (int) input.ReadInt();
        }

        public override void Write(IDataOutput output) {
            output.WriteInt((uint) this.value);
        }
    }
}
