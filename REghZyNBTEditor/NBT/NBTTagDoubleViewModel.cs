using REghZyIOWrapperV2.Streams;

namespace REghZyNBTEditor.NBT {
    public class NBTTagDoubleViewModel : NBTBaseViewModel {
        public override NBTType Type => NBTType.Integer;

        private double value;
        public double Value {
            get => this.value;
            set => RaisePropertyChanged(ref this.value, value);
        }

        public NBTTagDoubleViewModel() {

        }

        public NBTTagDoubleViewModel(string name) : base(name) {

        }

        public NBTTagDoubleViewModel(double value) {
            this.value = value;
        }

        public NBTTagDoubleViewModel(string name, double value) : base(name) {
            this.value = value;
        }

        public override NBTBaseViewModel Copy() {
            return new NBTTagDoubleViewModel(this.name, this.value);
        }

        public override void Read(IDataInput input) {
            this.value = input.ReadDouble();
        }

        public override void Write(IDataOutput output) {
            output.WriteDouble(this.value);
        }
    }
}
