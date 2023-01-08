using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Firma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool expanded = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ExpandMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (!expanded)
            {
                DoubleAnimation anim = new DoubleAnimation { To = 220, Duration = TimeSpan.FromMilliseconds(200) };
                MenuPanel.BeginAnimation(WidthProperty, anim);
                MenuColumn.Width = new GridLength(220);
                ExpandMenuButtonImage.Source = new BitmapImage(new Uri("/Views/Content/Icons/chevron_left.png", UriKind.Relative));
                expanded= true;
            }
            else
            {
                DoubleAnimation anim = new DoubleAnimation { To = 0, Duration = TimeSpan.FromMilliseconds(200) };
                MenuPanel.BeginAnimation(WidthProperty, anim);
                MenuColumn.Width = new GridLength(0);
                ExpandMenuButtonImage.Source = new BitmapImage(new Uri("/Views/Content/Icons/chevron_right.png", UriKind.Relative));
                expanded= false;
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

    }
}
