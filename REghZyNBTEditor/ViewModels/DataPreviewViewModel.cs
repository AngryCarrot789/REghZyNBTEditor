using REghZyMVVM.ViewModels;
using REghZyNBTEditor.NBT;

namespace REghZyNBTEditor.ViewModels {
    public class DataPreviewViewModel : BaseViewModel {
        private NBTBaseViewModel selectedItem;

        public NBTBaseViewModel SelectedItem {
            get => this.selectedItem;
            set => RaisePropertyChanged(ref this.selectedItem, value);
        }
    }
}
