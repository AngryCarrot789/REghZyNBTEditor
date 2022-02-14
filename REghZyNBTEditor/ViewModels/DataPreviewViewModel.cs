using System.Collections.Generic;
using System.Collections.ObjectModel;
using REghZyMVVM.ViewModels;
using REghZyNBTEditor.NBT;

namespace REghZyNBTEditor.ViewModels {
    public class DataPreviewViewModel : BaseViewModel {
        private ICollection<NBTBaseViewModel> items;

        public ICollection<NBTBaseViewModel> Items {
            get => this.items;
            set => RaisePropertyChanged(ref this.items, value);
        }

        private NBTBaseViewModel selectedItem;

        public NBTBaseViewModel SelectedItem {
            get => this.selectedItem;
            set => RaisePropertyChanged(ref this.selectedItem, value);
        }
    }
}
