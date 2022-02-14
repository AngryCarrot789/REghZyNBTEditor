using REghZyMVVM.IoC;
using REghZyNBTEditor.ViewModels;

namespace REghZyNBTEditor {
    public static class IoC {
        private static readonly SimpleIoC IOC = new SimpleIoC();

        static IoC() {
            MainViewModel = new MainViewModel();
        }

        public static MainViewModel MainViewModel {
            get => IOC.GetViewModel<MainViewModel>();
            set => IOC.SetViewModel(value);
        }

        public static ITreeViewSelector TreeSelector {
            get => IOC.GetService<ITreeViewSelector>();
            set => IOC.SetService(value);
        }
    }
}