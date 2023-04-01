using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;

using Avalonia.Flexbox.Demo.ViewModels;

namespace Avalonia.Flexbox.Demo
{
    public sealed partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnItemTapped(object? sender, RoutedEventArgs e)
        {
            if (sender is ListBoxItem control && control.DataContext is ItemViewModel item)
            {
                if (ViewModel.SelectedItem != null)
                {
                    ViewModel.SelectedItem.IsSelected = false;
                }

                if (ViewModel.SelectedItem == item)
                {
                    ViewModel.SelectedItem = null;
                }
                else
                {
                    ViewModel.SelectedItem = item;
                    ViewModel.SelectedItem.IsSelected = true;
                }
            }
        }
    }
}
