using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using REghZyFramework.Themes;
using REghZyMVVM.Views;
using REghZyNBTEditor.ViewModels;

namespace REghZyNBTEditor {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, BaseView<MainViewModel>, ITreeViewSelector {
        public MainViewModel Model {
            get => (MainViewModel) this.DataContext;
            set => this.DataContext = value;
        }

        public MainWindow() {
            InitializeComponent();
            this.Model = IoC.MainViewModel;
            IoC.TreeSelector = this;
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
            ThemesController.SetTheme(ThemeType.Dark);
        }

        private void MenuItemLightClick(object sender, RoutedEventArgs e) {
            ThemesController.SetTheme(ThemeType.Light);
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            this.Model.SelectedItem = (NBT.NBTBaseViewModel) e.NewValue;
        }

        public bool TrySetExpand(object value, bool isExpanded, bool force = false) {
            TreeView view = this.Tree;

            if (view.ItemContainerGenerator.ContainerFromItem(value) is TreeViewItem treeItem) {
                treeItem.IsExpanded = true;
                return true;
            }

            return TrySetExpand(view.ItemContainerGenerator, value, isExpanded, force);
        }

        private bool TrySetExpand(ItemContainerGenerator generator, object value, bool isExpanded, bool force = false) {
            foreach (object obj in generator.Items) {
                TreeViewItem item = (TreeViewItem) generator.ContainerFromItem(obj);
                if (item != null) {
                    if (obj == value || item == value) {
                        item.IsExpanded = isExpanded;
                        return true;
                    }
                    else if (TrySetExpand(item.ItemContainerGenerator, value, isExpanded, force)) {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool TrySelect(object value, bool force = false) {
            TreeView view = this.Tree;

            if (view.ItemContainerGenerator.ContainerFromItem(value) is TreeViewItem treeItem) {
                treeItem.IsExpanded = true;
                return true;
            }

            return TrySelect(view.ItemContainerGenerator, value, force);
        }

        private bool TrySelect(ItemContainerGenerator generator, object value, bool force = false) {
            foreach (object obj in generator.Items) {
                TreeViewItem item = (TreeViewItem) generator.ContainerFromItem(obj);
                if (item != null) {
                    if (obj == value || item == value) {
                        item.IsSelected = true;
                        return true;
                    }
                    else if (TrySelect(item.ItemContainerGenerator, value, force)) {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
