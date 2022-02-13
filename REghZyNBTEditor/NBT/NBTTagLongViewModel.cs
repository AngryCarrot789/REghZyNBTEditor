using REghZyIOWrapperV2.Streams;
using REghZyNBTEditor.NBT.Base;

namespace REghZyNBTEditor.NBT {
    public class NBTTagLongViewModel : NBTPrimitiveViewModel {
        public override NBTType Type => NBTType.Long;

        private long value;
        public long Value {
            get => this.value;
            set => RaisePropertyChanged(ref this.value, value);
        }

        public NBTTagLongViewModel() {

        }

        public NBTTagLongViewModel(string name) : base(name) {

        }

        public NBTTagLongViewModel(long value) {
            this.value = value;
        }

        public NBTTagLongViewModel(string name, long value) : base(name) {
            this.value = value;
        }

        protected override NBTBaseViewModel CopyInternal() {
            return new NBTTagLongViewModel(this.name, this.value);
        }

        public override void Read(IDataInput input) {
            this.value = (long) input.ReadLong();
        }

        public override void Write(IDataOutput output) {
            output.WriteLong((ulong) this.value);
        }
    }
}
