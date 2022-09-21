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

namespace Wpfnet5
{
    /// <summary>
    /// CircleStamp.xaml 的交互逻辑
    /// </summary>
    public partial class CircleStamp : UserControl
    {
        public CircleStamp()
        {
            InitializeComponent();
            
            
        }

        List<FrameworkElement> LabelChild = new List<FrameworkElement>();
        private void DrawText(string text)
        {
            LabelChild.ForEach((a) =>
            {
                grid.Children.Remove(a);
            });
            LabelChild.Clear();

            if (string.IsNullOrWhiteSpace(text))
                return;


            int len = text.Length;


            for(int i=0;i< text.Length;i++)
            {
                Label lb = new Label()
                {
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Width = 50,
                    Height = 50,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    RenderTransformOrigin = new Point(0.5, 2.5),
                    FontSize = 24,
                    RenderTransform = new RotateTransform(i * 360 / text.Length),
                    Content = text.Substring(i, 1),
                    Foreground= Foreground
                };
                LabelChild.Add(lb);
                grid.Children.Add(lb);
            }


        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("TextProperty", typeof(string), typeof(CircleStamp), new PropertyMetadata(null, TextChangedCallback));


        /// <summary>
        /// 1值变化处理函数的调用，在构造函数CircleStamp()之后才调用到，所以构造函数中Text不会有值
        /// 2在写代码的过程中，只要改变Text的值，就会出发一次这个函数
        /// 3再编译完成后运行程序的过程中，构造函数执行完后，（框架处理XAML中的赋值语句）会调用一次这个函数
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        public static void TextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //DrawText 虽然是私有的，但是因为TextChangedCallback在类里，所以任然可以通过对象调用
            (d as CircleStamp).DrawText((d as CircleStamp).Text);
        }




    }
}
