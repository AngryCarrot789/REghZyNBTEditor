using System.Collections.Generic;
using System.Collections.ObjectModel;
using REghZyIOWrapperV2.Streams;
using REghZyNBTEditor.NBT.Base;

namespace REghZyNBTEditor.NBT {
    public class NBTTagListViewModel : NBTCollectiveViewModel {
        public override NBTType Type => NBTType.List;

        private NBTType tagType;
        public NBTType TagType {
            get => this.tagType;
            set => RaisePropertyChanged(ref this.tagType, value);
        }

        private ObservableCollection<NBTBaseViewModel> data;
        public ObservableCollection<NBTBaseViewModel> Data {
            get => this.data;
            set => RaisePropertyChanged(ref this.data, value);
        }

        public NBTTagListViewModel() {
            this.data = new ObservableCollection<NBTBaseViewModel>();
        }

        public NBTTagListViewModel(string name) : base(name) {
            this.data = new ObservableCollection<NBTBaseViewModel>();
        }

        public NBTTagListViewModel(ObservableCollection<NBTBaseViewModel> data) {
            this.data = data;
        }

        public NBTTagListViewModel(string name, ObservableCollection<NBTBaseViewModel> data) : base(name) {
            this.data = data;
        }

        protected override NBTBaseViewModel CopyInternal() {
            ObservableCollection<NBTBaseViewModel> list = new ObservableCollection<NBTBaseViewModel>(this.data);
            foreach (NBTBaseViewModel nbt in this.data) {
                list.Add(nbt.CopyWithParent());
            }

            return new NBTTagListViewModel(this.name, list);
        }

        public override void Read(IDataInput input) {
            this.TagType = (NBTType) input.ReadByte();
            uint size = input.ReadInt();
            this.data.Clear();
            for (uint i = 0; i < size; i++) {
                NBTBaseViewModel nbt = this.tagType.CreateNBT(null, this);
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
