using REghZyIOWrapperV2.Streams;
using REghZyNBTEditor.NBT.Base;

namespace REghZyNBTEditor.NBT {
    public class NBTTagDoubleViewModel : NBTPrimitiveViewModel {
        public override NBTType Type => NBTType.Double;

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

        protected override NBTBaseViewModel CopyInternal() {
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
