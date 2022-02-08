using System;
using System.Collections.Generic;
using REghZyIOWrapperV2.Streams;
using REghZyNBTEditor.Utilities;

namespace REghZyNBTEditor.NBT {
    public class NBTTagCompoundViewModel : NBTCollectiveViewModel {
        public override NBTType Type => NBTType.Compound;

        private NBTType tagType;
        public NBTType TagType {
            get => this.tagType;
            set => RaisePropertyChanged(ref this.tagType, value);
        }

        private Dictionary<string, NBTBaseViewModel> map;
        public Dictionary<string, NBTBaseViewModel> Map {
            get => this.map;
            set => RaisePropertyChanged(ref this.map, value);
        }

        public NBTTagCompoundViewModel() {
            this.map = new Dictionary<string, NBTBaseViewModel>();
        }

        public NBTTagCompoundViewModel(string name) : base(name) {
            this.map = new Dictionary<string, NBTBaseViewModel>();
        }

        public NBTTagCompoundViewModel(Dictionary<string, NBTBaseViewModel> map) {
            this.map = map;
        }

        public NBTTagCompoundViewModel(string name, Dictionary<string, NBTBaseViewModel> map) : base(name) {
            this.map = map;
        }

        public void SetTag(string tagName, NBTBaseViewModel nbt) {
            if (nbt.Name != tagName) {
                nbt.Name = tagName;
            }

            this.map[tagName] = nbt;
        }

        protected override NBTBaseViewModel CopyInternal() {
            NBTTagCompoundViewModel tag = new NBTTagCompoundViewModel(this.name);
            foreach (NBTBaseViewModel nbt in this.map.Values) {
                tag.SetTag(nbt.Name, nbt.CopyWithParent());
            }

            return tag;
        }

        public override void Read(IDataInput input) {
            this.map.Clear();
            while (true) {
                NBTBaseViewModel nbt = ReadNBT(input, this);
                if (nbt.Type.IsValid() && nbt.Type != NBTType.End) {
                    SetTag(nbt.Name, nbt);
                }
                else {
                    return;
                }
            }
        }

        public override void Write(IDataOutput output) {
            foreach (NBTBaseViewModel nbt in this.map.Values) {
                WriteNBT(nbt, output);
            }

            output.WriteByte(0);
        }
    }
}
