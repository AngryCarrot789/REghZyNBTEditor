using REghZyIOWrapperV2.Streams;

namespace REghZyNBTEditor.NBT {
    public class NBTTagFloatViewModel : NBTBaseViewModel {
        public override NBTType Type => NBTType.Float;

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

        protected override NBTBaseViewModel CopyInternal() {
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
