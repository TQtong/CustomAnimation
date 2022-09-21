using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Wpfnet5
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

        private void bd_Loaded(object sender, RoutedEventArgs e)
        {
            ThicknessAnimation thicknessAnimation = new ThicknessAnimation();
            double aa = tb.ActualWidth - bd.ActualWidth;
            if (aa > 0)
            {
                thicknessAnimation.From = new Thickness(0, 0, 0, 0);
                thicknessAnimation.By = new Thickness(-aa-20, 0, 0, 0);
                thicknessAnimation.Duration = new Duration(TimeSpan.FromSeconds(10))
                {

                };
                thicknessAnimation.BeginTime = TimeSpan.FromSeconds(3);
                thicknessAnimation.Completed += (object? sender, EventArgs e) =>
                {
                    tb.BeginAnimation(TextBlock.MarginProperty, thicknessAnimation);
                };
                tb.BeginAnimation(TextBlock.MarginProperty, thicknessAnimation);
            }
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            PointAnimationUsingKeyFrames keyFrames = new PointAnimationUsingKeyFrames();
            keyFrames.Duration = new Duration(TimeSpan.FromSeconds(4));
            keyFrames.RepeatBehavior = RepeatBehavior.Forever;
            LinearPointKeyFrame lpk0 = new LinearPointKeyFrame(new Point(0, 0), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)));
            LinearPointKeyFrame lpk1 = new LinearPointKeyFrame(new Point(1, 0), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)));
            LinearPointKeyFrame lpk2 = new LinearPointKeyFrame(new Point(1, 1), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2)));
            LinearPointKeyFrame lpk3 = new LinearPointKeyFrame(new Point(0, 1), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(3)));
            LinearPointKeyFrame lpk4 = new LinearPointKeyFrame(new Point(0, 0), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(4)));
            keyFrames.KeyFrames.Add(lpk0);
            keyFrames.KeyFrames.Add(lpk1);
            keyFrames.KeyFrames.Add(lpk2);
            keyFrames.KeyFrames.Add(lpk3);
            keyFrames.KeyFrames.Add(lpk4);

            lgb.BeginAnimation(LinearGradientBrush.StartPointProperty, keyFrames);
        }
    }
}
