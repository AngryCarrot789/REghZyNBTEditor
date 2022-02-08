using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REghZyFramework.Utilities;
using REghZyNBTEditor.NBT;

namespace REghZyNBTEditor.ViewModels {
    public class NBTTreeItemViewModel : BaseViewModel {
        private bool isExpanded;
        private string name;

        public NBTType Type { get; }

        public string Name {
            get => this.name;
            set => RaisePropertyChanged(ref this.name, value);
        }

        public bool CanExpand {
            get {
                return this.Type == NBTType.Compound || this.Type == NBTType.List;
            }
        }

        public bool IsExpanded {
            get => this.isExpanded;
            set => RaisePropertyChanged(ref this.isExpanded, value);
        }

        public NBTTreeItemViewModel() {

        }
    }
}
