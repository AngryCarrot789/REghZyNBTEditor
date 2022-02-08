using REghZyIOWrapperV2.Streams;

namespace REghZyNBTEditor.NBT {
    public class NBTTagFloatViewModel : NBTBaseViewModel {
        public override NBTType Type => NBTType.Integer;

        private float value;
        public float Value {
            get => this.value;
            set => RaisePropertyChanged(ref this.value, value);
        }

        public NBTTagFloatViewModel() {

        }

        public NBTTagFloatViewModel(string name) : base(name) {

        }

        public NBTTagFloatViewModel(float value) {
            this.value = value;
        }

        public NBTTagFloatViewModel(string name, float value) : base(name) {
            this.value = value;
        }

        public override NBTBaseViewModel Copy() {
            return new NBTTagFloatViewModel(this.name, this.value);
        }

        public override void Read(IDataInput input) {
            this.value = input.ReadFloat();
        }

        public override void Write(IDataOutput output) {
            output.WriteFloat(this.value);
        }
    }
}
