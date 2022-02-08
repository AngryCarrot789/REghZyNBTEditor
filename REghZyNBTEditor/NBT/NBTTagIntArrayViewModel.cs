using System;
using REghZyIOWrapperV2.Streams;
using REghZyNBTEditor.Utilities;

namespace REghZyNBTEditor.NBT {
    public class NBTTagIntArrayViewModel : NBTBaseViewModel {
        public override NBTType Type => NBTType.Integer;

        private int[] value;
        public int[] Value {
            get => this.value;
            set => RaisePropertyChanged(ref this.value, value);
        }

        public NBTTagIntArrayViewModel() {

        }

        public NBTTagIntArrayViewModel(string name) : base(name) {

        }

        public NBTTagIntArrayViewModel(int[] value) {
            this.value = value;
        }

        public NBTTagIntArrayViewModel(string name, int[] value) : base(name) {
            this.value = value;
        }

        public override NBTBaseViewModel Copy() {
            return new NBTTagIntArrayViewModel(this.name, ArrayUtils.Copy(this.value));
        }

        public override void Read(IDataInput input) {
            uint size = input.ReadInt();
            int[] array = this.value = new int[size];
            for (int i = 0; i < size; i++) {
                array[i] = (int) input.ReadInt();
            }
        }

        public override void Write(IDataOutput output) {
            int[] array = this.value;
            int len = array.Length;
            output.WriteInt((uint) len);
            for (int i = 0; i < len; i++) {
                output.WriteInt((uint) array[i]);
            }
        }
    }
}
