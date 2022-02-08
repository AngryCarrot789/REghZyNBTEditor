using System;
using System.Collections.Generic;
using REghZyIOWrapperV2.Streams;
using REghZyNBTEditor.Utilities;

namespace REghZyNBTEditor.NBT {
    public class NBTTagCompoundViewModel : NBTBaseViewModel {
        public override NBTType Type => NBTType.Integer;

        private NBTType tagType;
        public NBTType TagType {
            get => this.tagType;
            set => RaisePropertyChanged(ref this.tagType, value);
        }

        private Dictionary<string, NBTBaseViewModel> data;
        public Dictionary<string, NBTBaseViewModel> Data {
            get => this.data;
            set => RaisePropertyChanged(ref this.data, value);
        }

        public NBTTagCompoundViewModel() {
            this.data = new Dictionary<string, NBTBaseViewModel>();
        }

        public NBTTagCompoundViewModel(string name) : base(name) {
            this.data = new Dictionary<string, NBTBaseViewModel>();
        }

        public NBTTagCompoundViewModel(Dictionary<string, NBTBaseViewModel> data) {
            this.data = data;
        }

        public NBTTagCompoundViewModel(string name, Dictionary<string, NBTBaseViewModel> data) : base(name) {
            this.data = data;
        }

        public void SetTag(string tagName, NBTBaseViewModel nbt) {
            if (nbt.Name != tagName) {
                nbt.Name = tagName;
            }

            this.data[tagName] = nbt;
        }

        public override NBTBaseViewModel Copy() {
            NBTTagCompoundViewModel tag = new NBTTagCompoundViewModel(this.name);
            foreach (NBTBaseViewModel nbt in this.data.Values) {
                tag.SetTag(nbt.Name, nbt.Copy());
            }

            return tag;
        }

        public override void Read(IDataInput input) {
            this.data.Clear();
            while (true) {
                NBTBaseViewModel nbt = ReadNBT(input);
                if (nbt.Type.IsValid() && nbt.Type != NBTType.End) {
                    SetTag(nbt.Name, nbt);
                }
                else {
                    return;
                }
            }
        }

        public override void Write(IDataOutput output) {
            foreach (NBTBaseViewModel nbt in this.data.Values) {
                WriteNBT(nbt, output);
            }

            output.WriteByte(0);
        }
    }
}
