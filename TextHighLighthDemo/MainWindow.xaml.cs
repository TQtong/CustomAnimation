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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextHighLighthDemo
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var backColor = System.Drawing.ColorTranslator.FromHtml("#037be2");
            var forColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            img.Source = FancyText.BitmapImageFromText("测试字体，微软雅黑", new System.Drawing.Font("Microsoft YaHei", 60, System.Drawing.FontStyle.Bold), forColor, backColor, 6);
            img1.Source = FancyText.BitmapImageFromText("外发光+描边", new System.Drawing.Font("Microsoft YaHei", 30, System.Drawing.FontStyle.Bold), forColor, backColor, 3);

        }
    }
}
