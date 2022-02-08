using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REghZyFramework.Utilities;
using REghZyNBTEditor.NBT;

namespace REghZyNBTEditor.ViewModels {
    public class MainViewModel : BaseViewModel {
        public ObservableCollection<NBTBaseViewModel> TreeItems { get; }

        public MainViewModel() {
            this.TreeItems = new ObservableCollection<NBTBaseViewModel>();
            this.TreeItems.Add(new NBTTagStringViewModel("Hello!!!!!"));
            this.TreeItems.Add(new NBTTagFloatViewModel("Hi!!"));
            this.TreeItems.Add(new NBTTagIntArrayViewModel("susi[]", new int[] { 25, 100, 420}));
            this.TreeItems.Add(new NBTTagListViewModel(
                "susi[]", 
                new List<NBTBaseViewModel>() { 
                    new NBTTagStringViewModel("hi lol", "420"), 
                    new NBTTagIntViewModel("My int xd", 420) 
                }));
        }
    }
}
