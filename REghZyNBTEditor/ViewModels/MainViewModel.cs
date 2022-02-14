using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Windows.Controls;
using REghZyIOWrapperV2.Streams;
using REghZyMVVM.Utils;
using REghZyMVVM.ViewModels;
using REghZyNBTEditor.NBT;
using REghZyNBTEditor.NBT.Base;

namespace REghZyNBTEditor.ViewModels {
    public class MainViewModel : BaseViewModel {
        private NBTBaseViewModel selectedItem;
        private NBTBaseViewModel selectedPreview;

        public ObservableCollection<NBTBaseViewModel> TreeItems { get; }

        public NBTBaseViewModel SelectedItem {
            get => this.selectedItem;
            set => RaisePropertyChanged(ref this.selectedItem, value);
        }

        public NBTBaseViewModel SelectedPreview {
            get => this.selectedPreview;
            set => RaisePropertyChanged(ref this.selectedPreview, value);
        }

        public DataPreviewViewModel DataPreview { get; set; }

        public MainViewModel() {
            this.TreeItems = new ObservableCollection<NBTBaseViewModel>();
            using (FileStream stream = File.OpenRead("C:\\Users\\kettl\\Desktop\\level.dat")) {
                this.TreeItems.Add(NBTBaseViewModel.ReadNBT(new DataInputStream(new GZipStream(stream, CompressionMode.Decompress, false)), null).SetName("[ROOT]"));
            }

            this.DataPreview = new DataPreviewViewModel();
            if (TreeItems.Count > 0) {
                SelectedItem = TreeItems[0];
            }

            // this.TreeItems.Add(new NBTTagStringViewModel("Hello!!!!! string"));
            // this.TreeItems.Add(new NBTTagFloatViewModel("Hi!! float", 5.2f));
            // this.TreeItems.Add(new NBTTagIntArrayViewModel("sueeeed[] array int", new int[] { 25, 100, 420 }));
            // this.TreeItems.Add(new NBTTagListViewModel(
            //     "liiist",
            //     new List<NBTBaseViewModel>() {
            //         new NBTTagStringViewModel("hi lol string", "420"),
            //         new NBTTagIntViewModel("My int xd int", 420)
            //     }));
            // this.TreeItems.Add(new NBTTagListViewModel("empty list!!!"));
        }

        public void NavigateToSelectedPreview() {
            if (this.SelectedPreview is NBTCollectiveViewModel) {
                if (IoC.TreeSelector.TrySetExpand(this.SelectedPreview, true)) {
                    // if TrySelect works, the tree view logic should make this work
                    // this.SelectedItem = this.SelectedPreview;
                    IoC.TreeSelector.TrySelect(this.SelectedPreview);
                }
            }
        }
    }
}
