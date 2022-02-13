using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Input;
using REghZyMVVM.Views;
using REghZyNBTEditor.ViewModels;
using REghZyThemes;

namespace REghZyNBTEditor {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, BaseView<MainViewModel> {
        public MainViewModel Model {
            get => (MainViewModel) this.DataContext;
            set => this.DataContext = value;
        }

        public MainWindow() {
            InitializeComponent();
            // Dictionary<ushort, object> map = new Dictionary<ushort, object>();
            // map[25] = "hello";
            // if (map.TryGetValue(25, out object obj)) {
            //     MessageBox.Show(obj.ToString());
            // }
        }

        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e) {
            base.OnPreviewMouseDown(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e) {
            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e) {
            base.OnMouseLeave(e);
        }

        private void MenuItemDarkClick(object sender, RoutedEventArgs e) {
            ThemesController.SetTheme(ThemesController.ThemeTypes.Dark);
        }

        private void MenuItemLightClick(object sender, RoutedEventArgs e) {
            ThemesController.SetTheme(ThemesController.ThemeTypes.Light);
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            this.Model.SelectedItem = (NBT.NBTBaseViewModel) e.NewValue;
        }
    }
}
