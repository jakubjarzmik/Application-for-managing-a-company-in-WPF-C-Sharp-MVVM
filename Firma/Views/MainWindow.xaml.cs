﻿using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Firma.ViewModels;

namespace Firma.Views
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;
        private bool expanded = false;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = MainWindowViewModel.GetInstance();
            DataContext = _viewModel;
        }
        private void ExpandMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.IsConnected)
                if (!expanded)
                {
                    DoubleAnimation anim = new DoubleAnimation { To = 220, Duration = TimeSpan.FromMilliseconds(200) };
                    MenuPanel.BeginAnimation(WidthProperty, anim);
                    MenuColumn.Width = new GridLength(220);
                    ExpandMenuButtonImage.Source = new BitmapImage(new Uri("/Views/Content/Icons/chevron_left.png", UriKind.Relative));
                    expanded = true;
                }
                else
                {
                    DoubleAnimation anim = new DoubleAnimation { To = 0, Duration = TimeSpan.FromMilliseconds(200) };
                    MenuPanel.BeginAnimation(WidthProperty, anim);
                    MenuColumn.Width = new GridLength(0);
                    ExpandMenuButtonImage.Source = new BitmapImage(new Uri("/Views/Content/Icons/chevron_right.png", UriKind.Relative));
                    expanded = false;
                }
        }

        private void ExpandMenuButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!expanded)
            {
                ExpandMenuButtonImage.Source = new BitmapImage(new Uri("/Views/Content/Icons/chevron_right.png", UriKind.Relative));
            }
            else
            {
                ExpandMenuButtonImage.Source = new BitmapImage(new Uri("/Views/Content/Icons/chevron_left.png", UriKind.Relative));
            }
        }

        private void ExpandMenuButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!expanded)
            {
                ExpandMenuButtonImage.Source = new BitmapImage(new Uri("/Views/Content/Icons/chevron_right-white.png", UriKind.Relative));
            }
            else
            {
                ExpandMenuButtonImage.Source = new BitmapImage(new Uri("/Views/Content/Icons/chevron_left-white.png", UriKind.Relative));
            }
        }
        private void RibbonLoaded(object sender, RoutedEventArgs e)
        {
            Grid child = VisualTreeHelper.GetChild((DependencyObject)sender, 0) as Grid;
            if (child != null)
            {
                child.RowDefinitions[0].Height = new GridLength(0);
            }
        }
        private async void StatusBarLoaded(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => _viewModel.CheckDatabaseConnection());
        }
    }
}
