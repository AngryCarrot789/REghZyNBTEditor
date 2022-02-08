using System.Windows;
using REghZyThemes;

namespace REghZyNBTEditor {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void MenuItemDarkClick(object sender, RoutedEventArgs e) {
            ThemesController.SetTheme(ThemesController.ThemeTypes.Dark);
        }

        private void MenuItemLightClick(object sender, RoutedEventArgs e) {
            ThemesController.SetTheme(ThemesController.ThemeTypes.Light);
        }
    }
}
