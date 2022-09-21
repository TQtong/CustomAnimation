using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Wpfnet5
{
    [TemplatePart(Name = "PART_CURR_Content", Type = typeof(ContentControl))]
    [TemplatePart(Name = "PART_NEXT_Content", Type = typeof(ContentControl))]
    [TemplatePart(Name = "PART_ListBox", Type = typeof(ListBox))]
    public class RollBox: ContentControl,INotifyPropertyChanged
    {
        public RollBox()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(5);
            dispatcherTimer.Tick += (object sender, EventArgs e) =>
            {
                Index++;
            };
            dispatcherTimer.Start();
        }

       


        /// <summary>
        /// ApplyTemplate 要比XAML赋值晚
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            CURR_Content = GetTemplateChild("PART_CURR_Content") as ContentControl;
            NEXT_Content = GetTemplateChild("PART_NEXT_Content") as ContentControl;
            PART_ListBox = GetTemplateChild("PART_ListBox") as ListBox;

            foreach(var item in Items)
            {
                PART_ListBox.Items.Add(new ListBoxItem()); 
            }

            Binding binding = new Binding();
            binding.Path = new PropertyPath("Index");
            binding.Source = this;
            binding.Mode = BindingMode.TwoWay;
            PART_ListBox.SetBinding(ListBox.SelectedIndexProperty, binding);
            Index = 0;

        }
        ContentControl CURR_Content;
        ContentControl NEXT_Content;
        ListBox PART_ListBox;
        public List<FrameworkElement> Items { get; set; } = new List<FrameworkElement>();


        int _Index=0;
        public int Index {
            get
            {
                return _Index;
            }
            set
            {
                preindex = _Index;
                if (value >= Items.Count)
                {
                    _Index = 0;
                }
                else
                    _Index = value;
                IndexChange();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Index"));
            }
        }

        int preindex=0;

        public event PropertyChangedEventHandler PropertyChanged;

        private void IndexChange()
        {
            if (Items.Count == 0)
                return;
            
            
            if(CURR_Content.Content==null)
            {
                CURR_Content.Content = Items[Index];
                return;//首次不需要动画
            }

            dhStart();



        }

        private void dhStart()
        {
            System.Diagnostics.Debug.WriteLine($"next{ Index}  curr{preindex}");
            NEXT_Content.Content = Items[Index];
            CURR_Content.Content = Items[preindex];

            ThicknessAnimation Curr_marginAnimation = new ThicknessAnimation();
            Curr_marginAnimation.From = new Thickness(0, 0, 0, 0);
            Curr_marginAnimation.To = new Thickness(-this.ActualWidth, 0, this.ActualWidth, 0);
            Curr_marginAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(1000));


            ThicknessAnimation Next_marginAnimation = new ThicknessAnimation();
            Next_marginAnimation.From = new Thickness(this.ActualWidth, 0, -this.ActualWidth, 0);
            Next_marginAnimation.To = new Thickness(0, 0, 0, 0);
            Next_marginAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(1000));

            NEXT_Content.BeginAnimation(ContentControl.MarginProperty, Next_marginAnimation);

            CURR_Content.BeginAnimation(ContentControl.MarginProperty, Curr_marginAnimation);

        }
    }
}
