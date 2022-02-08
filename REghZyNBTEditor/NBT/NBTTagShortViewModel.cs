﻿using REghZyIOWrapperV2.Streams;

namespace REghZyNBTEditor.NBT {
    public class NBTTagShortViewModel : NBTBaseViewModel {
        public override NBTType Type => NBTType.Short;

        private short value;
        public short Value {
            get => this.value;
            set => RaisePropertyChanged(ref this.value, value);
        }

        public NBTTagShortViewModel() {

        }

        public NBTTagShortViewModel(string name) : base(name) {

        }

        public NBTTagShortViewModel(short value) {
            this.value = value;
        }

        public NBTTagShortViewModel(string name, short value) : base(name) {
            this.value = value;
        }


        public override NBTBaseViewModel Copy() {
            return new NBTTagShortViewModel(this.name, this.value);
        }

        public override void Read(IDataInput input) {
            this.value = (short) input.ReadShort();
        }

        public override void Write(IDataOutput output) {
            output.WriteShort((ushort) this.value);
        }
    }
}