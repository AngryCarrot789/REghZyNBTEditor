using System;
using REghZyIOWrapperV2.Streams;
using REghZyNBTEditor.Utilities;

namespace REghZyNBTEditor.NBT {
    public class NBTTagIntArrayViewModel : NBTBaseViewModel {
        public override NBTType Type => NBTType.IntArray;

        private int[] data;
        public int[] Data {
            get => this.data;
            set => RaisePropertyChanged(ref this.data, value);
        }

        public NBTTagIntArrayViewModel() {

        }

        public NBTTagIntArrayViewModel(string name) : base(name) {

        }

        public NBTTagIntArrayViewModel(int[] value) {
            this.data = value;
        }

        public NBTTagIntArrayViewModel(string name, int[] value) : base(name) {
            this.data = value;
        }

        protected override NBTBaseViewModel CopyInternal() {
            return new NBTTagIntArrayViewModel(this.name, ArrayUtils.Copy(this.data));
        }

        public override void Read(IDataInput input) {
            uint size = input.ReadInt();
            int[] array = this.data = new int[size];
            for (int i = 0; i < size; i++) {
                array[i] = (int) input.ReadInt();
            }
        }

        public override void Write(IDataOutput output) {
            int[] array = this.data;
            int len = array.Length;
            output.WriteInt((uint) len);
            for (int i = 0; i < len; i++) {
                output.WriteInt((uint) array[i]);
            }
        }
    }
}
