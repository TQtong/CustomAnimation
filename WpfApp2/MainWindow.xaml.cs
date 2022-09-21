using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private bool Animationed = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Animationed)
            {
                StartAnimationIn(t1, 0.5f);
                StartAnimationOut(t2, 0.5f);
            }
            else
            {
                StartAnimationIn(t2, 0.5f);
                StartAnimationOut(t1, 0.5f);
            }
            Animationed = !Animationed;
        }
        private async void StartAnimationIn(FrameworkElement element, float seconds)
        {
            var sb = new Storyboard();
            var offset = element.ActualHeight;
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0, -offset, -0, offset),
                To = new Thickness(0)
            };
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            sb.Children.Add(animation);
            var fadeIn = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 0,
                To = 1,
            };
            Storyboard.SetTargetProperty(fadeIn, new PropertyPath("Opacity"));
            sb.Children.Add(fadeIn);
            sb.Begin(element);
            element.Visibility = Visibility.Visible;
            await Task.Delay((int)(seconds * 1000));
        }
        private async void StartAnimationOut(FrameworkElement element, float seconds)
        {
            var sb = new Storyboard();
            var offset = element.ActualHeight;
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0),
                To = new Thickness(0, offset, 0, -offset)
            };
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            sb.Children.Add(animation);
            var fadeIn = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 1,
                To = 0,
            };
            Storyboard.SetTargetProperty(fadeIn, new PropertyPath("Opacity"));
            sb.Children.Add(fadeIn);
            sb.Begin(element);
            await Task.Delay((int)(seconds * 1000));
            element.Visibility = Visibility.Hidden;
        }
    }
}
