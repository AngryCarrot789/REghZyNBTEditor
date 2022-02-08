using System;
using System.Collections.Generic;
using REghZyIOWrapperV2.Streams;
using REghZyNBTEditor.Utilities;

namespace REghZyNBTEditor.NBT {
    public class NBTTagListViewModel : NBTBaseViewModel {
        public override NBTType Type => NBTType.Integer;

        private NBTType tagType;
        public NBTType TagType {
            get => this.tagType;
            set => RaisePropertyChanged(ref this.tagType, value);
        }

        private List<NBTBaseViewModel> data;
        public List<NBTBaseViewModel> Data {
            get => this.data;
            set => RaisePropertyChanged(ref this.data, value);
        }

        public NBTTagListViewModel() {
            this.data = new List<NBTBaseViewModel>();
        }

        public NBTTagListViewModel(string name) : base(name) {
            this.data = new List<NBTBaseViewModel>();
        }

        public NBTTagListViewModel(List<NBTBaseViewModel> data) {
            this.data = data;
        }

        public NBTTagListViewModel(string name, List<NBTBaseViewModel> data) : base(name) {
            this.data = data;
        }

        public override NBTBaseViewModel Copy() {
            List<NBTBaseViewModel> list = new List<NBTBaseViewModel>(this.data);
            foreach (NBTBaseViewModel nbt in this.data) {
                list.Add(nbt.Copy());
            }

            return new NBTTagListViewModel(this.name, list);
        }

        public override void Read(IDataInput input) {
            this.TagType = (NBTType) input.ReadByte();
            uint size = input.ReadInt();
            this.data.Clear();
            for (uint i = 0; i < size; i++) {
                NBTBaseViewModel nbt = this.tagType.CreateNBT(null);
                nbt.Read(input);
                this.data.Add(nbt);
            }
        }

        public override void Write(IDataOutput output) {
            if (this.data.Count != 0) {
                NBTType newTagType = this.data[0].Type;
                if (newTagType != this.tagType) {
                    this.TagType = newTagType;
                }
            }
            else if (this.tagType != NBTType.Byte) {
                this.TagType = NBTType.Byte;
            }

            output.WriteEnum8(this.tagType);
            output.WriteInt((uint) this.data.Count);
            foreach(NBTBaseViewModel nbt in this.data) {
                nbt.Write(output);
            }
        }
    }
}
