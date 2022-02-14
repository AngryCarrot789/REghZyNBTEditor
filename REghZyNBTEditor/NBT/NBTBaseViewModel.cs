using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using REghZyIOWrapperV2.Streams;
using REghZyMVVM.Commands;
using REghZyMVVM.ViewModels;
using REghZyNBTEditor.NBT.Base;

namespace REghZyNBTEditor.NBT {
    public abstract class NBTBaseViewModel : BaseViewModel {
        protected NBTCollectiveViewModel parent;
        protected string name;
        protected bool isExpanded;

        /// <summary>
        /// The NBT type
        /// </summary>
        public abstract NBTType Type { get; }

        /// <summary>
        /// Name of this NBT
        /// </summary>
        public string Name {
            get => this.name;
            set => this.RaisePropertyChanged(ref this.name, value);
        }

        public bool IsExpanded {
            get => this.isExpanded;
            set {
                if (this.CanExpand) {
                    RaisePropertyChanged(ref this.isExpanded, value);
                }
            }
        }

        public ICommand ExpandCommand { get; set; }

        public bool CanExpand {
            get => this.Type == NBTType.List || this.Type == NBTType.Compound;
        }

        /// <summary>
        /// The parent of this NBT, which may be <see langword="null"/> if there is no parent
        /// </summary>
        public NBTCollectiveViewModel Parent {
            get => this.parent;
            set => RaisePropertyChanged(ref this.parent, value);
        }

        public NBTBaseViewModel() {
            this.ExpandCommand = new RelayCommand(() => {
                IoC.MainViewModel.NavigateToSelectedPreview();
                this.IsExpanded = true;
            });
        }

        public NBTBaseViewModel(string name) : this() {
            this.name = name;
        }


        public static NBTBaseViewModel ReadNBT(IDataInput input, NBTCollectiveViewModel parent) {
            NBTType type = (NBTType) input.ReadByte();
            if (!type.IsValid()) {
                throw new Exception("Invalid NBT with ID: " + type + (parent == null ? "" : (". Trace: " + parent.FormatParents())));
            }
            else if (type == NBTType.End) {
                return new NBTTagEndViewModel();
            }
            else {
                string name = input.ReadStringUTF8(input.ReadShort());
                NBTBaseViewModel nbt = type.CreateNBT(name, parent);
                nbt.Read(input);
                return nbt;
            }
        }

        public static void WriteNBT(NBTBaseViewModel nbt, IDataOutput output) {
            output.WriteEnum8(nbt.Type);
            if (nbt.Type != NBTType.End) {
                output.WriteShort((ushort) nbt.Name.Length);
                output.WriteStringUTF8(nbt.Name);
                nbt.Write(output);
            }
        }

        public abstract void Write(IDataOutput output);

        public abstract void Read(IDataInput input);

        public byte GetId() {
            return (byte) this.Type;
        }

        protected abstract NBTBaseViewModel CopyInternal();

        public NBTBaseViewModel Copy() {
            return this.CopyInternal();
        }

        public NBTBaseViewModel CopyWithParent() {
            NBTBaseViewModel copy = this.CopyInternal();
            copy.parent = this.parent;
            return copy;
        }

        public string FormatParents() {
            StringBuilder sb = new StringBuilder(128);
            List<string> names = new List<string>(32);
            for(NBTCollectiveViewModel parent = this.parent; parent != null; parent = parent.parent) {
                names.Add(parent.name);
            }

            for(int i = 0, lenIndex = names.Count - 1; i < lenIndex; i++) {
                sb.Append(names[i]).Append("/");
            }

            sb.Append(names[names.Count - 1]);
            return sb.ToString();
        }

        public NBTBaseViewModel SetName(string name) {
            this.name = name;
            return this;
        }
    }
}
