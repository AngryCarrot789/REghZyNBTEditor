using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using REghZyIOWrapperV2.Streams;
using REghZyMVVM.ViewModels;
using REghZyNBTEditor.NBT;

namespace REghZyNBTEditor.ViewModels {
    public class MainViewModel : BaseViewModel {
        private NBTBaseViewModel selectedItem;

        public ObservableCollection<NBTBaseViewModel> TreeItems { get; }

        public NBTBaseViewModel SelectedItem {
            get => this.selectedItem;
            set => RaisePropertyChanged(ref this.selectedItem, value);
        }

        public MainViewModel() {
            this.TreeItems = new ObservableCollection<NBTBaseViewModel>();
            using (FileStream stream = File.OpenRead("C:\\Users\\kettl\\Desktop\\level.dat")) {
                this.TreeItems.Add(NBTBaseViewModel.ReadNBT(new DataInputStream(new GZipStream(stream, CompressionMode.Decompress, false)), null).SetName("[ROOT]"));
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
    }
}
